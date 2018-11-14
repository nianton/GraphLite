using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GraphLite.Tests
{
    [TestCaseOrderer("GraphLite.Tests.TestNameCaseOrderer", "GraphLite.Tests")]
    public class GraphClientWithExternalAuthProviderTests : IClassFixture<WithExternalAuthTestFixture>
    {
        private const string CustomPropertyName = "TaxRegistrationNumber";
        private readonly GraphApiClient _client;
        private readonly WithExternalAuthTestFixture _fixture;

        public GraphClientWithExternalAuthProviderTests(WithExternalAuthTestFixture fixture)
        {
            _fixture = fixture;
            _client = fixture.Client;
        }

        [Fact]
        public void TestGetUsers()
        {
            var users = _client.UserGetAllAsync().Result;
            Assert.NotEmpty(users);
        }

        [Fact]
        public void TestGet2Users()
        {
            var r = _client.UserGetListAsync(top: 2).Result;
            Assert.NotNull(r);
            Assert.Equal(2, r.Items.Count);
        }


        [Fact]
        public async Task TestSingleInitialization()
        {
            var t1 = _client.EnsureInitAsync();
            var t2 = _client.EnsureInitAsync();
            var t3 = _client.EnsureInitAsync();
            await Task.WhenAll(t1, t2, t3);
        }


        [Fact]
        public async Task TestGetAllUsers()
        {
            var totalCount = 0;
            var progress = new Progress<IList<User>>(users => { totalCount += users.Count; });

            var allUsers = await _client.UserGetAllAsync(itemsPerPage: 10, progress: progress);
            Assert.NotNull(allUsers);
            Assert.Equal(totalCount, allUsers.Count);
        }

        [Fact]
        public void TestGetExtensionsApp()
        {
            var r = _client.GetB2cExtensionsApplicationAsync().Result;
            Assert.NotNull(r);
        }

        [Fact]
        public void TestGetSpecificUser()
        {
            var r = _client.UserGetAsync(_fixture.TestUserObjectId).Result;
            Assert.NotNull(r);
        }

        [Fact]
        public void TestUpdateSpecificUser()
        {
            var user = _client.UserGetAsync(_fixture.TestUserObjectId).Result;
            user.ExtendedProperties.Remove("thumbnailPhoto@odata.mediaEditLink");
            user.SetExtendedProperty(CustomPropertyName, "000111000");
            _client.UserUpdateAsync(user.ObjectId, user.ExtendedProperties).Wait();
            Assert.NotNull(user);
        }

        [Fact]
        public void TestGetUserBySignInName()
        {
            var user = _client.UserGetBySigninNameAsync(_fixture.TestUser.SignInNames.First().Value).Result;
            Assert.NotNull(user);
            Assert.Equal(_fixture.TestUserObjectId, user.ObjectId);
        }

        [Fact]
        public void TestUpdateSpecificUserAlt()
        {
            var r = _client.UserGetAsync(_fixture.TestUserObjectId).Result;
            r.ExtendedProperties.Remove("thumbnailPhoto@odata.mediaEditLink");
            r.SetExtendedProperty(CustomPropertyName, DateTime.Now.ToString("HHmmsstttt"));
            _client.UserUpdateAsync(r.ObjectId, r.ExtendedProperties).Wait();
            Assert.NotNull(r);
        }

        [Fact]
        public void TestApplicationExtensions()
        {
            var app = _client.GetB2cExtensionsApplicationAsync().Result;
            var extensions = _client.GetApplicationExtensionsAsync(app.ObjectId).Result;
            Assert.NotNull(extensions);
        }

        [Fact]
        public void TestUpdateSpecificUserThumbnail()
        {
            var r = _client.UserGetAsync(_fixture.TestUserObjectId).Result;
            var thumb = File.ReadAllBytes("thumbnails/random-thumbnail-400.jpg");
            _client.UserUpdateThumbnailAsync(r.ObjectId, thumb).Wait();
            Assert.True(true);
        }

        [Fact]
        public void TestGetSpecificUserThumbnail()
        {
            var user = _client.UserGetAsync(_fixture.TestUserObjectId).Result;
            if (user.ThumbnailContentType != null)
            {
                var r2 = _client.UserGetThumbnailAsync(user.ObjectId).Result;
                File.WriteAllBytes("test.jpg", r2);
                Assert.NotNull(r2);
            }
            else
            {
                Assert.Null(user.ThumbnailContentType);
            }
        }

        [Fact]
        [Priority(1000)] // To make sure that it runs last (deletes the test user)
        public async Task TestDeleteSpecificUser()
        {
            var userId = _fixture.TestUserObjectId;
            var r = await _client.UserGetAsync(userId);
            await _client.UserDeleteAsync(r.ObjectId);
            var deleted = await _client.UserGetAsync(userId);
            Assert.Null(deleted);
        }

        [Fact]
        public async Task TestGetUsersByObjectIds()
        {
            var userId = _fixture.TestUserObjectId;
            var users = await _client.UserGetByObjectIdsAsync(userId);
            Assert.NotEmpty(users);
        }

        [Fact]
        public void TestGetGroups()
        {
            var groups = _client.GroupGetListAsync().Result;
            Assert.NotEmpty(groups.Items);
        }

        [Fact]
        public void TestGetGroupMembers()
        {
            var memberIds = _client.GroupGetMembersAsync(_fixture.TestGroupObjectId).Result;
            Assert.NotEmpty(memberIds);
        }

        [Fact]
        public async Task TestGetMemberGroups()
        {
            var userId = _fixture.TestUserObjectId;
            var user = await _client.UserGetAsync(userId);
            var groupIds = _client.UserGetMemberGroupsAsync(user.ObjectId).Result;
            Assert.NotEmpty(groupIds);
        }

        [Fact]
        public void TestIsGroupMember()
        {
            var isMember = _client.IsMemberOfGroupAsync(_fixture.TestGroupObjectId, _fixture.TestUserObjectId).Result;
            Assert.True(isMember);
        }

        [Fact]
        public async Task TestUserResetPassword()
        {
            var userId = _fixture.TestUserObjectId;
            var user = await _client.UserGetAsync(userId);
            _client.UserResetPasswordAsync(user.ObjectId, "Test1234!!", true).Wait();
        }

        [Fact]
        public void TestCreateUser()
        {
            var id = $"{Guid.NewGuid()}";

            var user = new User
            {
                CreationType = "LocalAccount",
                AccountEnabled = true,
                GivenName = $"John-{id}",
                Surname = $"Smith-{id}",
                DisplayName = $"John Smith {id}",
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

            var newUser = _client.UserCreateAsync(user).Result;
            Assert.NotNull(newUser);
        }

        [Fact]
        public async Task TestCreateUserWithInvalidExtensionProperties()
        {
            var id = $"{Guid.NewGuid()}";

            var user = new User
            {
                CreationType = "LocalAccount",
                AccountEnabled = true,
                GivenName = $"John-{id}",
                Surname = $"Smith-{id}",
                DisplayName = $"John Smith {id}",
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

            user.SetExtendedProperty("MyCustomName", "Nikos");

            await Assert.ThrowsAsync<InvalidOperationException>(() => _client.UserCreateAsync(user));
        }
    }
}