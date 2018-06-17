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

            TestUser = CreateTestUser();
            TestUser = Client.UserCreateAsync(TestUser).Result;

            var group = CreateTestGroup();
            group = Client.GroupCreateAsync(group).Result;
            TestGroupObjectId = group.ObjectId;

            Client.GroupAddMemberAsync(group.ObjectId, TestUserObjectId).Wait();
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