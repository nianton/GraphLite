using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GraphLite
{
    public abstract class BaseApiClient
    {
        /// <summary>
        /// The bearer token scheme.
        /// </summary>
        private const string BearerTokenScheme = "Bearer";

        /// <summary>
        /// Patch http method.
        /// </summary>
        protected static readonly HttpMethod HttpMethodPatch = new HttpMethod("PATCH");

        /// <summary>
        /// The authentication provider.
        /// </summary>
        protected IAuthProvider AuthProvider { get; set; }

        /// <summary>
        /// Gets the base URL.
        /// </summary>
        protected string BaseUrl { get; set; }

        protected abstract string AuthResource { get; }
        
        /// <summary>
        /// The http client instance.
        /// </summary>
        protected static readonly HttpClient HttpClient = new HttpClient();
        private TokenWrapper _tokenWrapper;

        /// <summary>
        /// The access token semaphore (ensures a single execution of <see cref="EnsureAuthorizationHeader(HttpRequestMessage)"/> for many concurrent calls).
        /// </summary>
        private readonly SemaphoreSlim _accessTokenSemaphore = new SemaphoreSlim(1, 1);

        protected virtual async Task<TResult> ExecuteRequest<TResult>(HttpMethod method, string resource, string query = null, object body = null, string apiVersion = null)
        {
            var response = await ExecuteRequest(method, resource, query, body, apiVersion);
            var result = JsonConvert.DeserializeObject<TResult>(response);
            return result;
        }

        protected async Task<string> ExecuteRequest(HttpMethod method, string resource, string query = null, object body = null, string apiVersion = null)
        {
            var responseMessage = await DoExecuteRequest(method, resource, query, body, apiVersion: apiVersion);
            var response = await responseMessage.Content.ReadAsStringAsync();
            return response;
        }

        protected async Task<byte[]> ExecuteRequestAsByteArray(HttpMethod method, string resource, string query = null, object body = null, string apiVersion = null)
        {
            var responseMessage = await DoExecuteRequest(method, resource, query, body, apiVersion: apiVersion);
            var response = await responseMessage.Content.ReadAsByteArrayAsync();
            return response;
        }

        protected async Task<HttpResponseMessage> DoExecuteRequest(HttpMethod method, string resource, string query = null, object body = null, string contentType = null, string[] acceptedContentTypes = null, string apiVersion = null)
        {
            var url = $"{BaseUrl}/{resource}";
            
            if (!string.IsNullOrWhiteSpace(apiVersion))
                url += $"?api-version={apiVersion}";
            
            if (!string.IsNullOrWhiteSpace(query))
                url += url.Contains("?") ? $"&{query}" : $"?{query}";

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
            var responseMessage = await HttpClient.SendAsync(requestMessage);

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
            client.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.Headers.Authorization = _tokenWrapper.AccessToken.StartsWith(BearerTokenScheme, StringComparison.InvariantCultureIgnoreCase)
                ? AuthenticationHeaderValue.Parse(_tokenWrapper.AccessToken)
                : new AuthenticationHeaderValue(BearerTokenScheme, _tokenWrapper.AccessToken);
        }

        private async Task EnsureAccessToken()
        {
            if (DateTimeOffset.Now > _tokenWrapper.Expiry.GetValueOrDefault())
            {
                await _accessTokenSemaphore.WaitAsync();
                try
                {
                    if (DateTimeOffset.Now > _tokenWrapper.Expiry.GetValueOrDefault())
                    {
                        _tokenWrapper = await AuthProvider.GetAccessTokenAsync(AuthResource);
                    }
                }
                finally
                {
                    _accessTokenSemaphore.Release();
                }
            }
        }

    }
}
