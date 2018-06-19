using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GraphLite.Tests
{

    public class GraphClientSingleInitialization
    {
        private readonly GraphApiClient _client;

        public GraphClientSingleInitialization()
        {
            var config = TestsConfig.Create();

            _client = new GraphApiClient(
                config.ApplicationId,
                config.ApplicationSecret,
                config.Tenant
            );
        }

        public Task MultipleInitializationCallsAreEvaluatedOnce()
        {
            var t1 = _client.EnsureInitAsync();
            var t2 = _client.EnsureInitAsync();
            var t3 = _client.EnsureInitAsync();

            return Task.WhenAll(t1, t2, t3);
        }
    }
}
