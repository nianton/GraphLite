using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GraphLite.Tests
{
    public class GraphClientQueryTests : IClassFixture<TestFixture>
    {
        private readonly GraphApiClient _client;
        private readonly TestFixture _fixture;

        public GraphClientQueryTests(TestFixture fixture)
        {
            _fixture = fixture;
            _client = fixture.Client;
        }

        [Fact]
        public void TestQueryInOperator()
        {
            var userQuery = new ODataQuery<User>()
                .WhereIn(u => u.UserPrincipalName, "another@gmail.com", "another2@gmail.com")
                .Top(20)
                .OrderBy(u => u.MailNickname);

            var expected = "$top=20&$orderby=mailNickname&$filter=(userPrincipalName eq 'another@gmail.com' or userPrincipalName eq 'another2@gmail.com')";
            var actualDecoded = System.Net.WebUtility.UrlDecode(userQuery.ToString());
            Assert.Equal(expected, actualDecoded);
        }

        [Fact]
        public void TestQueryTwoFilterClauses()
        {
            var userQuery = new ODataQuery<User>()
                .WhereIn(u => u.UserPrincipalName, "another@gmail.com", "another2@gmail.com")
                .Where(u => u.GivenName, "nikos", ODataOperator.GreaterThanEquals)
                .Top(20)
                .OrderBy(u => u.MailNickname);

            var expected = "$top=20&$orderby=mailNickname&$filter=(userPrincipalName eq 'another@gmail.com' or userPrincipalName eq 'another2@gmail.com') and givenName ge 'nikos'";
            var actualDecoded = System.Net.WebUtility.UrlDecode(userQuery.ToString());
            Assert.Equal(expected, actualDecoded);
        }


        [Fact]
        public async Task TestQueryByExtensionProperty()
        {
            var userQuery = await _client.UserQueryCreateAsync();
            var extPropertyName = "TaxRegistrationNumber";

            userQuery
                .WhereExtendedProperty(extPropertyName, "1235453", ODataOperator.Equals)
                .Where(u => u.GivenName, "nikos", ODataOperator.GreaterThanEquals)
                .Top(20)
                .OrderBy(u => u.MailNickname);

            var extApp = await _client.GetB2cExtensionsApplicationAsync();
            var extAppId = extApp.AppId;
            var expected = $"$top=20&$orderby=mailNickname&$filter=extension_{extAppId.Replace("-", string.Empty)}_{extPropertyName} eq '1235453' and givenName ge 'nikos'";
            var actualDecoded = System.Net.WebUtility.UrlDecode(userQuery.ToString());
            Assert.Equal(expected, actualDecoded);
        }

        [Fact]
        public async Task TestFetchByUserName()
        {
            var signinName = _fixture.TestUser.SignInNames[0].Value;
            var user = await _client.UserGetBySigninNameAsync(signinName);
            Assert.Equal(_fixture.TestUserObjectId, user.ObjectId);
        }



        [Fact]
        public async Task TestFetchByExtensionProperty()
        {
            var extPropertyName = "TaxRegistrationNumber";
            var userQuery = await _client.UserQueryCreateAsync();
            userQuery
                .WhereExtendedProperty(extPropertyName, _fixture.TestUser.GetExtendedProperties()[extPropertyName], ODataOperator.Equals);

            var users = await _client.UserGetListAsync(userQuery);
            Assert.NotEmpty(users.Items);
            Assert.Contains(users.Items, item => item.ObjectId == _fixture.TestUserObjectId);
        }


        [Fact]
        public async Task TestFetchByExtensionPropertyGreaterThan()
        {
            var extPropertyName = "TaxRegistrationNumber";
            var userQuery = await _client.UserQueryCreateAsync();
            userQuery.WhereExtendedProperty(extPropertyName, "1", ODataOperator.GreaterThan);

            await Assert.ThrowsAsync<GraphApiException>(() => _client.UserGetListAsync(userQuery));
        }

        [Fact]
        public async Task TestFetchByUserNameViaStandardQuery()
        {
            var signinName = _fixture.TestUser.SignInNames[0].Value;
            var query = new ODataQuery<User>().Where(u => u.SignInNames, sn => sn.Value, signinName, ODataOperator.Equals);
            var users = await _client.UserGetListAsync(query);
            Assert.Single(users.Items);
            Assert.Equal(_fixture.TestUserObjectId, users.Items[0].ObjectId);
        }
    }
}
