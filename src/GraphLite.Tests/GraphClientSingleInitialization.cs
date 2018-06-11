using Microsoft.Extensions.Configuration;
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
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var configuration = builder.Build();

            _client = new GraphApiClient(
                configuration["applicationId"],
                configuration["applicationSecret"],
                configuration["tenant"]
            );           
        }

        public async Task MultipleInitializationCallsAreEvaluatedOnce()
        {
            var t1 = _client.EnsureInitAsync();
            var t2 = _client.EnsureInitAsync();
            var t3 = _client.EnsureInitAsync();

            Task.WaitAll(t1, t2, t3);
        }
    }
}
