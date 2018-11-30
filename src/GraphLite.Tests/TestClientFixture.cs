using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GraphLite.Tests
{
    public class TestClientFixture : IDisposable
    {
        public TestClientFixture()
        {
            Config = TestsConfig.Create();

            // Wrapped in Task.Run as non-deadlocking synchronous call.
            Task.Run(InitAsync).Wait();
        }

        public TestsConfig Config { get; set; }

        public GraphApiClient Client { get; set; }

        public string TestUserObjectId => TestUser.ObjectId;

        public User TestUser { get; set; }

        public string TestGroupObjectId { get; set; }

        public string ExtensionPropertyName { get; set; }

        private async Task InitAsync()
        {
            Client = new GraphApiClient(
                Config.ApplicationId,
                Config.ApplicationSecret,
                Config.Tenant
            );

            await Client.EnsureInitAsync();
            var b2cApp = await Client.GetB2cExtensionsApplicationAsync();

            var extProperties = await Client.GetApplicationExtensionsAsync(b2cApp.ObjectId);
            ExtensionPropertyName = extProperties.FirstOrDefault()?.GetSimpleName();

            TestUser = CreateTestUser();
            TestUser = await Client.UserCreateAsync(TestUser);

            var group = CreateTestGroup();
            group = await Client.GroupCreateAsync(group);
            TestGroupObjectId = group.ObjectId;

            await Client.GroupAddMemberAsync(group.ObjectId, TestUserObjectId);
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
                         Value = $"testuser-{id}@gmail.com"
                     }
                },
                PasswordProfile = new PasswordProfile
                {
                    EnforceChangePasswordPolicy = false,
                    ForceChangePasswordNextLogin = false,
                    Password = "123abC!!"
                }
            };

            if (!string.IsNullOrEmpty(ExtensionPropertyName))
                user.SetExtendedProperty(ExtensionPropertyName, "123123123");

            return user;
        }

        private async Task DisposeAsync()
        {
            var testUser = await Client.UserGetAsync(TestUserObjectId);
            if (testUser != null)
                await Client.UserDeleteAsync(TestUserObjectId);

            await Client.GroupDeleteAsync(TestGroupObjectId);
        }

        public void Dispose()
        {
            // Wrapped in Task.Run as non-deadlocking synchronous call.
            Task.Run(DisposeAsync).Wait();
        }
    }

    [CollectionDefinition(Name)]
    public class TestFixtureCollection : ICollectionFixture<TestClientFixture>
    {
        public const string Name = "TestClientCollection";
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}