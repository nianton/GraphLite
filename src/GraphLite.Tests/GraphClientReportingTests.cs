using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GraphLite.Tests
{
    public class GraphClientReportingTests : IClassFixture<TestFixture>
    {
        readonly TestFixture _fixture;
        readonly IReportingClient _reportingClient;

        public GraphClientReportingTests(TestFixture fixture)
        {
            _fixture = fixture;
            _reportingClient = fixture.Client.Reporting;
        }

        [Fact]
        public void TestTenantUserCounts()
        {
            var userCounts = _reportingClient.GetTenantUserCountSummariesAsync().Result;
            Assert.NotEmpty(userCounts);
        }

        [Fact]
        public void TestGetDailySummariesCounts()
        {
            var dailySummaries = _reportingClient.GetAuthenticationCountSummariesAsync().Result;
            Assert.NotEmpty(dailySummaries);
        }

        [Fact]
        public void TestGetMfaRequestCountSummaries()
        {
            var mfaRequestSummaries = _reportingClient.GetMfaRequestCountSummariesAsync().Result;
            Assert.NotNull(mfaRequestSummaries);
        }

        [Fact]
        public void TestGetAuthenticationCount()
        {
            var userCounts = _reportingClient.GetAuthenticationCountAsync().Result;
            Assert.NotNull(userCounts);
        }
    }
}
