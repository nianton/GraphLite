using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace GraphLite.Tests
{
    public class TestFixture : IDisposable
    {
        public IConfigurationRoot Configuration { get; set; }

        public GraphApiClient Client { get; set; }

        public string TestUserObjectId { get; set; }

        public string TestGroupObjectId { get; set; }

        public TestFixture()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();

            Client = new GraphApiClient(
                Configuration["applicationId"],
                Configuration["applicationSecret"],
                Configuration["tenant"]
            );

            var testUser = CreateTestUser();
            testUser = Client.UserCreateAsync(testUser).Result;

            TestUserObjectId = testUser.ObjectId;

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

            var user = new User
            {
                CreationType = "LocalAccount",
                AccountEnabled = true,
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