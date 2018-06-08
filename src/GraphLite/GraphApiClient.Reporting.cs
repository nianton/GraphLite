using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GraphLite
{
    public interface IReportingClient
    {
        Task<IList<DailyTenantCountSummary>> GetTenantUserCountSummariesAsync(DateTimeOffset? dateFilter = null, ODataOperator @operator = ODataOperator.GreaterThan);
    }

    public partial class GraphApiClient
    {
        internal class ReportingClient : IReportingClient
        {
            private readonly GraphApiClient _client;

            public ReportingClient(GraphApiClient client)
            {
                _client = client;
            }

            public async Task<IList<DailyTenantCountSummary>> GetTenantUserCountSummariesAsync(DateTimeOffset? dateFilter = null, ODataOperator @operator = ODataOperator.GreaterThan)
            {
                var query = string.Empty;
                if (dateFilter.HasValue)
                {
                    query = new ODataQuery<DailyTenantCountSummary>()
                        .Where(x => x.TimeStamp, dateFilter.Value, @operator)
                        .ToString();
                }
                
                var result = await _client.ExecuteRequest<ODataResponse<DailyTenantCountSummary>>(HttpMethod.Get, $"reports/tenantUserCount", query, apiVersion: "beta");
                return result.Value;
            }
        }
    }
}
