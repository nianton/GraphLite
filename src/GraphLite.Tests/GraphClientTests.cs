using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GraphLite.Tests
{
    [TestCaseOrderer("GraphLite.Tests.TestNameCaseOrderer", "GraphLite.Tests")]
    [Collection(TestFixtureCollection.Name)]
    public class GraphClientTests
    {
        private readonly GraphApiClient _client;
        private readonly TestClientFixture _fixture;
        private string _extensionPropertyName;

        public GraphClientTests(TestClientFixture fixture)
        {
            _fixture = fixture;
            _client = fixture.Client;
            _extensionPropertyName = _fixture.ExtensionPropertyName;
        }

        [Fact]
        public async Task TestGetUsers()
        {
            var users = await _client.UserGetAllAsync();
            Assert.NotEmpty(users);
        }

        [Fact]
        public async Task TestGet2Users()
        {
            var r = await _client.UserGetListAsync(top: 2);
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
        public async Task TestGetExtensionsApp()
        {
            var r = await _client.GetB2cExtensionsApplicationAsync();
            Assert.NotNull(r);
        }

        [Fact]
        public async Task TestGetSpecificUser()
        {
            var r = await _client.UserGetAsync(_fixture.TestUserObjectId);
            Assert.NotNull(r);
        }

        [Fact]
        public async Task TestUpdateSpecificUser()
        {
            var user = await _client.UserGetAsync(_fixture.TestUserObjectId);
            var postalCodeValue = DateTime.Now.ToString("yyMMddHHmmss");
            var telephoneNumberValue = "123334524352345";
            var changes = new
            {
                PostalCode = postalCodeValue,
                TelephoneNumber = telephoneNumberValue
            };

            await _client.UserUpdateAsync(user.ObjectId, changes);
            var updatedUser = await _client.UserGetAsync(user.ObjectId);

            Assert.NotNull(updatedUser);
            Assert.Equal(postalCodeValue, updatedUser.PostalCode);
            Assert.Equal(telephoneNumberValue, updatedUser.TelephoneNumber);
        }

        [Fact]
        public async Task TestGetUserBySignInName()
        {
            var user = await _client.UserGetBySigninNameAsync(_fixture.TestUser.SignInNames.First().Value);
            Assert.NotNull(user);
            Assert.Equal(_fixture.TestUserObjectId, user.ObjectId);
        }

        [Fact]
        public async Task TestApplicationExtensions()
        {
            var app = await _client.GetB2cExtensionsApplicationAsync();
            var extensions = _client.GetApplicationExtensionsAsync(app.ObjectId);
            Assert.NotNull(extensions);
        }

        [Fact]
        public async Task TestUpdateSpecificUserThumbnail()
        {
            var r = await _client.UserGetAsync(_fixture.TestUserObjectId);
            var thumb = File.ReadAllBytes("thumbnails/random-thumbnail-400.jpg");
            await _client.UserUpdateThumbnailAsync(r.ObjectId, thumb);
            Assert.True(true);
        }

        [Fact]
        public async Task TestGetSpecificUserThumbnail()
        {
            var user = await _client.UserGetAsync(_fixture.TestUserObjectId);
            if (user.ThumbnailContentType != null)
            {
                var r2 = await _client.UserGetThumbnailAsync(user.ObjectId);
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
        public async Task TestGetGroups()
        {
            var groups = await _client.GroupGetListAsync();
            Assert.NotEmpty(groups.Items);
        }

        [Fact]
        public async Task TestGetGroupMembers()
        {
            var memberIds = await _client.GroupGetMembersAsync(_fixture.TestGroupObjectId);
            Assert.NotEmpty(memberIds);
        }

        [Fact]
        public async Task TestGetMemberGroups()
        {
            var userId = _fixture.TestUserObjectId;
            var user = await _client.UserGetAsync(userId);
            var groupIds = await _client.UserGetMemberGroupsAsync(user.ObjectId);
            Assert.NotEmpty(groupIds);
        }

        [Fact]
        public async Task TestIsGroupMember()
        {
            var isMember = await _client.IsMemberOfGroupAsync(_fixture.TestGroupObjectId, _fixture.TestUserObjectId);
            Assert.True(isMember);
        }

        [Fact]
        public async Task TestUserResetPassword()
        {
            var userId = _fixture.TestUserObjectId;
            var user = await _client.UserGetAsync(userId);
            await _client.UserResetPasswordAsync(user.ObjectId, "Test1234!!", true);
        }

        [Fact]
        public async Task TestCreateUser()
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
                         Value = $"sample-user-{id}@mydomain.com"
                     }
                },
                PasswordProfile = new PasswordProfile
                {
                    EnforceChangePasswordPolicy = false,
                    ForceChangePasswordNextLogin = false,
                    Password = "123abC!!"
                }
            };

            var newUser = await _client.UserCreateAsync(user);
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
                         Value = $"sample-user-{id}@mydomain.com"
                     }
                },
                PasswordProfile = new PasswordProfile
                {
                    EnforceChangePasswordPolicy = false,
                    ForceChangePasswordNextLogin = false,
                    Password = "123abC!!"
                }
            };

            user.SetExtendedProperty("MyNonExistingProperty", "Nikos");

            await Assert.ThrowsAsync<InvalidOperationException>(() => _client.UserCreateAsync(user));
        }
    }
}
