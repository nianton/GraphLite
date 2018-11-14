using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GraphLite
{
    internal static class DefaultAuthProvider
    {
        internal static async Task<TokenWrapper> EnsureAccessTokenAsync(this HttpClient client,string resource, string tenant, NetworkCredential credential)
        {
            var requestMessage = CreateAuthenticationRequest(resource, tenant, credential);
            var response = await SendAuthenticationRequest(client, requestMessage);
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

        private static async Task<string> SendAuthenticationRequest(HttpClient client, HttpRequestMessage requestMessage)
        {
            var responseMessage = await client.SendAsync(requestMessage);
            var response = await responseMessage.Content.ReadAsStringAsync();
            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorResponse = JsonConvert.DeserializeObject<ErrorInfo>(response);
                throw new GraphAuthException(requestMessage, responseMessage, errorResponse);
            }
            return response;
        }

        private static HttpRequestMessage CreateAuthenticationRequest(string resource, string tenant,
            NetworkCredential credential)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, GraphLiteConfiguration.AuthTokenEndpoint(tenant));
            var contentParameters = ConstructRequestParameters(resource, credential);
            var content = new FormUrlEncodedContent(contentParameters);
            requestMessage.Content = content;
            return requestMessage;
        }

        private static Dictionary<string, string> ConstructRequestParameters(string resource, NetworkCredential credential)
        {
            var contentParameters = new Dictionary<string, string>
            {
                {"client_id", credential.UserName},
                {"client_secret", credential.Password},
                {"grant_type", "client_credentials"},
                {"scope", "offline_access"},
                {"resource", resource}
            };
            return contentParameters;
        }
    }
}