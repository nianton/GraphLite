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
            TestUser = Task.Run(() => Client.UserCreateAsync(TestUser)).Result;

            var group = CreateTestGroup();
            group = Task.Run(() => Client.GroupCreateAsync(group)).Result;
            TestGroupObjectId = group.ObjectId;

            Task.Run(() => Client.GroupAddMemberAsync(group.ObjectId, TestUserObjectId)).Wait();
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
            var testUser = Task.Run(() => Client.UserGetAsync(TestUserObjectId)).Result;
            if (testUser != null)
                Task.Run(() => Client.UserDeleteAsync(TestUserObjectId)).Wait();

            Task.Run(() => Client.GroupDeleteAsync(TestGroupObjectId)).Wait();
        }
    }
}