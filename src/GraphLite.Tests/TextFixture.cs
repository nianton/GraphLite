using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphLite.Tests
{
    public class TestFixture : IDisposable
    {
        public TestsConfig Config { get; set; }
        public GraphApiClient Client { get; set; }

        public string TestUserObjectId => TestUser.ObjectId;

        public User TestUser { get; set; }

        public string TestGroupObjectId { get; set; }

        public TestFixture()
        {
            Config = TestsConfig.Create();

            Client = new GraphApiClient(
                Config.ApplicationId,
                Config.ApplicationSecret,
                Config.Tenant
            );

            // Wrapped in Task.Run as non-deadlocking synchronous call.
            Task.Run(() => InitAsync()).Wait();
        }

        private async Task InitAsync()
        {
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
            Task.Run(() => DisposeAsync()).Wait();
        }
    }
}