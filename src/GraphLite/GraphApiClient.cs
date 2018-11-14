using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Local

namespace GraphLite
{
    public partial class GraphApiClient
    {
        private const string Bearer = "Bearer";

        /// <summary>
        /// Patch http method.
        /// </summary>
        private static readonly HttpMethod HttpMethodPatch = new HttpMethod("PATCH");

        /// <summary>
        /// The initialization semaphore (ensures a single execution of <see cref="EnsureInitAsync"/> for many concurrent calls).
        /// </summary>
        private readonly SemaphoreSlim _initializationSemaphore = new SemaphoreSlim(1, 1);

        /// <summary>
        /// The access token semaphore (ensures a single execution of <see cref="EnsureAuthorizationHeader(HttpRequestMessage)"/> for many concurrent calls).
        /// </summary>
        private readonly SemaphoreSlim _accessTokenSemaphore = new SemaphoreSlim(1, 1);

        private readonly string _tenant;
        private string _b2cExtensionsApplicationId;
        private List<ExtensionProperty> _b2cExtensionsApplicationProperties;
        private readonly Func<string, Task<TokenWrapper>> _authorizationCallback;

        /// <summary>
        /// Initializes an instance of the GraphApiClient with the necessary application credentials.
        /// </summary>
        /// <param name="applicationId">The application identifier.</param>
        /// <param name="applicationSecret">The application secret.</param>
        /// <param name="tenant">The B2C tenant e.g. 'mytenant.onmicrosoft.com'</param>
        public GraphApiClient(string applicationId, string applicationSecret, string tenant)
            : this(tenant, s => GraphLiteConfiguration.Client.EnsureAccessTokenAsync(s, tenant, new NetworkCredential(applicationId, applicationSecret)))
        {
            InputValidator.ValidateInput(applicationId, applicationSecret, tenant);
        }

        /// <summary>
        ///  Initializes an instance of the GraphApiClient with authorization callback.
        /// </summary>
        /// <param name="tenant">The B2C tenant e.g. 'mytenant.onmicrosoft.com'</param>
        /// <param name="authorizationCallback">Callback to provide the content of the http Authorize header and the expiry time for the token</param>
        public GraphApiClient(string tenant, Func<string, Task<TokenWrapper>> authorizationCallback)
        {
            InputValidator.ValidateInput(tenant, authorizationCallback);
            _tenant = tenant;
            _authorizationCallback = authorizationCallback;
            Reporting = new ReportingClient(this);
        }

        /// <summary>
        /// Gets the base URL.
        /// </summary>
        protected string BaseUrl => string.Format(GraphLiteConfiguration.BaseUrlFormat, _tenant);

        /// <summary>
        /// Gets the reporting client.
        /// </summary>
        public IReportingClient Reporting { get; }

        private TokenWrapper Token { get; set; }

        /// <summary>
        /// Ensures that the client is initialized with the necessary B2C extension application metadata.
        /// This is required to handle extension properties for the B2C Users.
        /// </summary>
        /// <returns>A task that indicates the asynchronous operation.</returns>
        public async Task EnsureInitAsync()
        {
            if (!string.IsNullOrEmpty(_b2cExtensionsApplicationId))
                return;

            await _initializationSemaphore.WaitAsync();
            try
            {
                if (!string.IsNullOrEmpty(_b2cExtensionsApplicationId))
                    return;

                await EnsureB2cExtensionsApplicationMetadataAsync();
            }
            finally
            {
                _initializationSemaphore.Release();
            }
        }

        private async Task<TResult> ExecuteRequest<TResult>(HttpMethod method, string resource, string query = null, object body = null, string apiVersion = null)
        {
            var response = await ExecuteRequest(method, resource, query, body, apiVersion);
            var result = JsonConvert.DeserializeObject<TResult>(response);
            if (!(result is ODataResponse<Application>) && !(result is ODataResponse<ExtensionProperty>) && result is IExtensionsApplicationAware appAware)
            {
                await EnsureInitAsync();
                appAware.SetExtensionsApplicationId(_b2cExtensionsApplicationId);
            }

            return result;
        }

        private async Task<string> ExecuteRequest(HttpMethod method, string resource, string query = null, object body = null, string apiVersion = null)
        {
            var responseMessage = await DoExecuteRequest(method, resource, query, body, apiVersion: apiVersion);
            var response = await responseMessage.Content.ReadAsStringAsync();
            return response;
        }

        private async Task<byte[]> ExecuteRequestAsByteArray(HttpMethod method, string resource, string query = null, object body = null, string apiVersion = null)
        {
            var responseMessage = await DoExecuteRequest(method, resource, query, body, apiVersion: apiVersion);
            var response = await responseMessage.Content.ReadAsByteArrayAsync();
            return response;
        }

        private async Task<HttpResponseMessage> DoExecuteRequest(HttpMethod method, string resource, string query = null, object body = null, string contentType = null, string[] acceptedContentTypes = null, string apiVersion = null)
        {

            apiVersion = apiVersion ?? GraphLiteConfiguration.DefaultGraphApiVersion;

            var url = $"{BaseUrl}/{resource}?api-version={apiVersion}";
            if (!string.IsNullOrWhiteSpace(query))
            {
                url += $"&{query}";
            }
            var requestMessage = new HttpRequestMessage(method, url);


            if (body != null && (method == HttpMethod.Post || method == HttpMethod.Put || method == HttpMethodPatch))
            {
                var content = default(HttpContent);
                if (body is Stream bodyStream)
                {
                    content = new StreamContent(bodyStream);
                }
                else if (body is byte[] bodyArray)
                {
                    content = new ByteArrayContent(bodyArray);
                }
                else
                {
                    var json = JsonConvert.SerializeObject(body, new JsonSerializerSettings() { NullValueHandling = method == HttpMethod.Post ? NullValueHandling.Ignore : NullValueHandling.Include });
                    content = new StringContent(json, Encoding.UTF8, "application/json");
                }

                requestMessage.Content = content;
            }

            if (!string.IsNullOrEmpty(contentType))
            {
                requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            }

            if (acceptedContentTypes != null)
            {
                foreach (var mediaType in acceptedContentTypes)
                {
                    requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
                }
            }
            await EnsureAuthorizationHeader(requestMessage);
            var responseMessage = await GraphLiteConfiguration.Client.SendAsync(requestMessage);

            if (!responseMessage.IsSuccessStatusCode)
            {
                var error = await responseMessage.Content.ReadAsStringAsync();
                var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(error);
                throw new GraphApiException(requestMessage, responseMessage, errorResponse);
            }

            return responseMessage;
        }

        /// <summary>
        /// Ensures the authorization header contains a valid access token.
        /// </summary>
        /// <param name="client">The HTTP client.</param>
        /// <returns>A task that indicates the asynchronous call.</returns>
        private async Task EnsureAuthorizationHeader(HttpRequestMessage client)
        {
            await EnsureAccessToken();
            client.Headers.Accept.Clear();
            client.Headers.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            if (Token.Token.StartsWith(Bearer, StringComparison.InvariantCultureIgnoreCase))
                client.Headers.Authorization = AuthenticationHeaderValue.Parse(Token.Token);
            else
                client.Headers.Authorization = new AuthenticationHeaderValue(Bearer, Token.Token);

        }

        private async Task EnsureAccessToken()
        {
            if (DateTimeOffset.Now > Token.Expiry.GetValueOrDefault())
            {
                await _accessTokenSemaphore.WaitAsync();
                try
                {
                    if (DateTimeOffset.Now > Token.Expiry.GetValueOrDefault())
                    {
                        Token = await _authorizationCallback.Invoke(GraphLiteConfiguration.AzureGraphResourceId);
                    }
                }
                finally
                {
                    _accessTokenSemaphore.Release();
                }
            }
        }

        private string GetDirectoryObjectUrl(string directoryObjectId)
        {
            return $"{BaseUrl}/directoryObjects/{directoryObjectId}";
        }
    }
}