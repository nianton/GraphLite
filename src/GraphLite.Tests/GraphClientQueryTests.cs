using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GraphLite.Tests
{
    [TestCaseOrderer("GraphLite.Test-s.TestNameCaseOrderer", "GraphLite.Tests")]
    public class GraphClientQueryTests : IClassFixture<TestFixture>
    {
        readonly GraphApiClient _client;
        readonly TestFixture _fixture;

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
        public void TestFetchByUserName()
        {
            var signinName = _fixture.TestUser.SignInNames[0].Value;
            var user = _client.UserGetBySigninNameAsync(signinName).Result;
            Assert.Equal(_fixture.TestUserObjectId, user.ObjectId);
        }


        [Fact]
        public void TestFetchByUserNameViaStandardQuery()
        {
            var signinName = _fixture.TestUser.SignInNames[0].Value;
            var query = new ODataQuery<User>().Where(u => u.SignInNames, sn => sn.Value, signinName, ODataOperator.Equals);
            var users = _client.UserGetListAsync(query).Result;
            Assert.Single(users.Items);
            Assert.Equal(_fixture.TestUserObjectId, users.Items[0].ObjectId);
        }
    }
}
