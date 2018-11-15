using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GraphLite
{
    /// <summary>
    /// Default implementation of AuthProvider, used directly by GraphApiClient when constructed with credentials.
    /// </summary>
    /// <seealso cref="GraphLite.IAuthProvider" />
    internal sealed class DefaultAuthProvider : IAuthProvider
    {
        private readonly HttpClient _client;
        private readonly string _tenant;
        private readonly NetworkCredential _credential;

        public DefaultAuthProvider(HttpClient client, string tenant, string applicationId, string applicationSecret) 
        {            
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            if (string.IsNullOrWhiteSpace(tenant))
                throw new ArgumentNullException(nameof(tenant));

            if (string.IsNullOrWhiteSpace(applicationId))
                throw new ArgumentNullException(nameof(applicationId));

            if (!Guid.TryParse(applicationId, out _))
                throw new ArgumentException("Invalid format, expected Guid.", nameof(applicationId));

            if (string.IsNullOrWhiteSpace(applicationSecret))
                throw new ArgumentNullException(nameof(applicationSecret));

            _client = client;
            _tenant = tenant;

            // Storing the credential in a NetworkCredential object to store the application secret behind a SecureString.
            _credential = new NetworkCredential(applicationId, applicationSecret);
        }

        public async Task<TokenWrapper> GetAccessTokenAsync(string resource)
        {
            var requestMessage = CreateAuthenticationRequest(resource);
            var response = await SendAuthenticationRequest(requestMessage);
            var token = ConvertToInternalToken(response);
            return token;
        }

        private static TokenWrapper ConvertToInternalToken(string response)
        {
            var authTokenResponse = JsonConvert.DeserializeObject<AuthTokenResponse>(response);
            var token = new TokenWrapper
            {
                Expiry = DateTimeOffset.Now.Add(TimeSpan.FromSeconds(authTokenResponse.ExpiresIn)),
                Token = authTokenResponse.AccessToken
            };

            return token;
        }

        private async Task<string> SendAuthenticationRequest(HttpRequestMessage requestMessage)
        {
            var responseMessage = await _client.SendAsync(requestMessage);
            var response = await responseMessage.Content.ReadAsStringAsync();
            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorResponse = JsonConvert.DeserializeObject<ErrorInfo>(response);
                throw new GraphAuthException(requestMessage, responseMessage, errorResponse);
            }

            return response;
        }

        private HttpRequestMessage CreateAuthenticationRequest(string resource)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, GraphLiteConfiguration.AuthTokenEndpoint(_tenant));
            var contentParameters = CreateRequestParameters(resource);
            var content = new FormUrlEncodedContent(contentParameters);
            requestMessage.Content = content;
            return requestMessage;
        }

        private Dictionary<string, string> CreateRequestParameters(string resource)
        {
            var contentParameters = new Dictionary<string, string>
            {
                {"client_id", _credential.UserName},
                {"client_secret", _credential.Password},
                {"grant_type", "client_credentials"},
                {"scope", "offline_access"},
                {"resource", resource }
            };

            return contentParameters;
        }
    }
}