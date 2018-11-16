using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace GraphLite.Tests
{
    public class GraphClientInitializationTests
    {
        private readonly TestsConfig Config = TestsConfig.Create();
        private string AuthTokenEndpoint => $"https://login.windows.net/{Config.Tenant}/oauth2/token";

        [Fact]
        public async Task TestWithCredentials()
        {
            var client = new GraphApiClient(Config.ApplicationId, Config.ApplicationSecret, Config.Tenant);
            await client.EnsureInitAsync();

            // If something is wrong with init call (authorization issues etc), exception will be thrown.
            Assert.True(true);
        }

        [Fact]
        public async Task TestWithAuthDelegate()
        {
            var client = new GraphApiClient(Config.Tenant, GetAccessTokenAsync);
            await client.EnsureInitAsync();

            // If something is wrong with init call (authorization issues etc), exception will be thrown.
            Assert.True(true);
        }

        private async Task<TokenWrapper> GetAccessTokenAsync(string resource)
        {
            var httpclient = new HttpClient();
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, AuthTokenEndpoint);
            var contentParameters = new Dictionary<string, string>
            {
                { "client_id", Config.ApplicationId },
                { "client_secret", Config.ApplicationSecret },
                { "grant_type", "client_credentials" },
                { "scope", "offline_access" },
                { "resource", resource }
            };

            var content = new FormUrlEncodedContent(contentParameters);
            requestMessage.Content = content;
            var responseMessage = await httpclient.SendAsync(requestMessage);
            var response = await responseMessage.Content.ReadAsStringAsync();

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorResponse = JsonConvert.DeserializeObject<ErrorInfo>(response);
                throw new GraphAuthException(requestMessage, responseMessage, errorResponse);
            }

            var authTokenResponse = JsonConvert.DeserializeObject<AuthTokenResponse>(response);
            var token = new TokenWrapper
            {
                Expiry = DateTimeOffset.Now.Add(TimeSpan.FromSeconds(authTokenResponse.ExpiresIn)),
                AccessToken = authTokenResponse.AccessToken
            };

            return token;
        }
    }
}
