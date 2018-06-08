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
    }
}
