using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GraphLite.Tests
{
    [Collection(TestFixtureCollection.Name)]
    public class GraphClientReportingTests
    {
        readonly TestClientFixture _fixture;
        readonly IReportingClient _reportingClient;

        public GraphClientReportingTests(TestClientFixture fixture)
        {
            _fixture = fixture;
            _reportingClient = fixture.Client.Reporting;
        }

        [Fact]
        public async Task TestTenantUserCounts()
        {
            var userCounts = await _reportingClient.GetTenantUserCountSummariesAsync();
            Assert.NotEmpty(userCounts);
        }

        [Fact]
        public async Task TestGetDailySummariesCounts()
        {
            var dailySummaries = await _reportingClient.GetAuthenticationCountSummariesAsync();
            Assert.NotNull(dailySummaries);
        }

        [Fact]
        public async Task TestGetMfaRequestCount()
        {
            var mfaRequestCounts = await _reportingClient.GetMfaRequestCountAsync();
            Assert.NotNull(mfaRequestCounts);
        }

        [Fact]
        public async Task TestGetMfaRequestCountSummaries()
        {
            var mfaRequestSummaries = await _reportingClient.GetMfaRequestCountSummariesAsync();
            Assert.NotNull(mfaRequestSummaries);
        }

        [Fact]
        public async Task TestGetAuthenticationCount()
        {
            var userCounts = await _reportingClient.GetAuthenticationCountAsync();
            Assert.NotNull(userCounts);
        }
    }
}
