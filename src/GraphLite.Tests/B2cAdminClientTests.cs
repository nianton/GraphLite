using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GraphLite.Tests
{
    public class B2cAdminClientTests
    {
        private readonly B2cAdminApiClient _client;
        private readonly TestClientFixture _fixture;
        private string _extensionPropertyName;
        private readonly TestsConfig _config = TestsConfig.Create();

        public B2cAdminClientTests()
        {
            _client = new B2cAdminApiClient(_config.ApplicationId, _config.ApplicationSecret, _config.Tenant);
        }

        [Fact]
        public async Task TestClient()
        {
            var list = await _client.B2CPolicyGetListAsync();
            Assert.NotEmpty(list.Items);
        }
    }
}
