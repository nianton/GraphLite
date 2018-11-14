using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GraphLite.Tests
{
    public class WithExternalAuthTestFixture : IDisposable
    {
        private readonly HttpClient _client;
        public TestsConfig Config { get; set; }
        public GraphApiClient Client { get; set; }

        public string TestUserObjectId => TestUser.ObjectId;

        public User TestUser { get; set; }

        public string TestGroupObjectId { get; set; }

        public WithExternalAuthTestFixture()
        {
            Config = TestsConfig.Create();
            _client = new HttpClient();
            Client = new GraphApiClient(
               Config.Tenant, async resource => await EnsureAccessTokenAsync(resource));

            TestUser = CreateTestUser();
            TestUser = Client.UserCreateAsync(TestUser).Result;

            var group = CreateTestGroup();
            group = Client.GroupCreateAsync(group).Result;
            TestGroupObjectId = group.ObjectId;

            Client.GroupAddMemberAsync(group.ObjectId, TestUserObjectId).Wait();
        }
        private string AuthTokenEndpoint => $"https://login.windows.net/{Config.Tenant}/oauth2/token";
        private async Task<TokenWrapper> EnsureAccessTokenAsync(string resource)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, AuthTokenEndpoint);
            var contentParameters = new Dictionary<string, string>
            {
                { "client_id", Config.ApplicationId },
                { "client_secret",Config.ApplicationSecret },
                { "grant_type", "client_credentials" },
                { "scope", "offline_access" },
                { "resource", resource }
            };

            var content = new FormUrlEncodedContent(contentParameters);
            requestMessage.Content = content;
            var responseMessage = await _client.SendAsync(requestMessage);
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
                Token = authTokenResponse.AccessToken
            };
            return token;
        }
        private Group CreateTestGroup()
        {
            var group = new Group
            {
                SecurityEnabled = true,
                DisplayName = $"TEST-GROUP{TestUserObjectId}",
                MailNickname = $"TEST-GROUP{TestUserObjectId}",
                MailEnabled = false
            };

            return group;
        }

        private User CreateTestUser()
        {
            var id = $"{Guid.NewGuid()}";

            var user = new User()
            {
                CreationType = "LocalAccount",
                AccountEnabled = true,
                GivenName = "John",
                Surname = "Smith",
                DisplayName = $"testuser-{id}@gmail.com",
                SignInNames = new List<SignInName>
                {
                    new SignInName()
                    {
                        Type = "emailAddress",
                        Value = $"nian.t.o.n-{id}@gmail.com"
                    }
                },
                PasswordProfile = new PasswordProfile
                {
                    EnforceChangePasswordPolicy = false,
                    ForceChangePasswordNextLogin = false,
                    Password = "123abC!!"
                }
            };

            user.SetExtendedProperty("TaxRegistrationNumber", "123123123");
            return user;
        }

        public void Dispose()
        {
            var testUser = Client.UserGetAsync(TestUserObjectId).Result;
            if (testUser != null)
                Client.UserDeleteAsync(TestUserObjectId).Wait();

            Client.GroupDeleteAsync(TestGroupObjectId).Wait();
        }
    }
}