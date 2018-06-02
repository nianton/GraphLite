using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GraphLite
{
    public partial class GraphApiClient
    {
        /// <summary>
        /// The predefined B2C application responsible the tenant's management.
        /// </summary>
        public const string B2cExtensionsApplicationName = "b2c-extensions-app";

        /// <summary>
        /// Max thumbnail photo size in bytes.
        /// </summary>
        public const int MaxThumbnailPhotoSize = 100_000;

        /// <summary>
        /// Patch http method.
        /// </summary>
        private static readonly HttpMethod HttpMethodPatch = new HttpMethod("PATCH");

        private readonly string _tenant;
        private HttpClient _client;
        private string _accessToken;
        private DateTimeOffset? _accessTokenExpiresOn;
        private string _b2cExtensionsApplicationId;
        private List<ExtensionProperty> _b2cExtensionsApplicationProperties;
        private NetworkCredential _credential;

        /// <summary>
        /// Initializes an instance of the GraphApiClient with the necessary application credentials.
        /// </summary>
        /// <param name="applicationId">The application identifier.</param>
        /// <param name="applicationSecret">The application secret.</param>
        /// <param name="tenant">The B2C tenant e.g. 'mytenant.onmicrosoft.com'</param>
        public GraphApiClient(string applicationId, string applicationSecret, string tenant)
        {
            _tenant = tenant;
            _credential = new NetworkCredential(applicationId, applicationSecret);
        }

        public string BaseUrl { get; set; }

        private async Task<TResult> ExecuteRequest<TResult>(HttpMethod method, string resource, string query = null, object body = null)
        {
            var response = await ExecuteRequest(method, resource, query, body);           
            var result = JsonConvert.DeserializeObject<TResult>(response);
            if (!(result is ODataResponse<Application>) && !(result is ODataResponse<ExtensionProperty>) && result is IExtensionsApplicationAware appAware)
            {
                var appId = await GetB2cExtensionsApplicationMetadataAsync();
                appAware.SetExtensionsApplicationId(appId);
            }

            return result;
        }

        private async Task<string> ExecuteRequest(HttpMethod method, string resource, string query = null, object body = null)
        {
            var responseMessage = await DoExecuteRequest(method, resource, query, body);
            var response = await responseMessage.Content.ReadAsStringAsync();
            return response;
        }

        private async Task<byte[]> ExecuteRequestAsByteArray(HttpMethod method, string resource, string query = null, object body = null)
        {
            var responseMessage = await DoExecuteRequest(method, resource, query, body);
            var response = await responseMessage.Content.ReadAsByteArrayAsync();
            return response;
        }

        private async Task<HttpResponseMessage> DoExecuteRequest(HttpMethod method, string resource, string query = null, object body = null, string contentType = null, string[] acceptedContentTypes = null)
        {
            var client = GetClient();
            await EnsureAuthorizationHeader(client);

            var url = $"https://graph.windows.net/{_tenant}/{resource}?api-version=1.6";
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

            var responseMessage = await client.SendAsync(requestMessage);

            if (!responseMessage.IsSuccessStatusCode)
            {
                var error = await responseMessage.Content.ReadAsStringAsync();
                var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(error);
                throw new GraphApiException(requestMessage, responseMessage, errorResponse);
            }

            return responseMessage;
        }

        private async Task EnsureAuthorizationHeader(HttpClient client)
        {
            if (DateTimeOffset.Now > _accessTokenExpiresOn.GetValueOrDefault())
            {
                await EnsureAccessTokenAsync();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
        }

        private async Task EnsureAccessTokenAsync()
        {
            var client = GetClient();
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"https://login.windows.net/{_tenant}/oauth2/token");
            var requestParameters = new Dictionary<string, string>();
            var contentParameters = new Dictionary<string, string>
            {
                { "client_id", _credential.UserName },
                { "client_secret", _credential.Password },
                { "grant_type", "client_credentials" },
                { "scope", "offline_access" },
                { "resource", "https://graph.windows.net" }
            };

            var content = new FormUrlEncodedContent(contentParameters);
            requestMessage.Content = content;
            var responseMessage = await client.SendAsync(requestMessage);
            var response = await responseMessage.Content.ReadAsStringAsync();

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorResponse = JsonConvert.DeserializeObject<ErrorInfo>(response);
                throw new GraphAuthException(requestMessage, responseMessage, errorResponse);
            }

            var authTokenResponse = JsonConvert.DeserializeObject<AuthTokenResponse>(response);
            
            _accessTokenExpiresOn = DateTimeOffset.Now.Add(TimeSpan.FromSeconds(authTokenResponse.ExpiresIn));
            _accessToken = authTokenResponse.AccessToken;
        }


        private string GetDirectoryObjectUrl(string directoryObjectId)
        {
            return $"https://graph.windows.net/{_tenant}/directoryObjects/{directoryObjectId}";
        }


        private HttpClient GetClient()
        {
            if (_client != null)
                return _client;

            _client = new HttpClient();
            //_client.BaseAddress = new Uri(BaseUrl);

            return _client;
        }
    }
}