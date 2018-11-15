using GraphLite.Reporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GraphLite
{
    public partial class GraphApiClient
    {
        internal class ReportingClient : IReportingClient
        {
            private readonly GraphApiClient _client;

            public ReportingClient(GraphApiClient client)
            {
                _client = client;
            }

            /// <summary>
            /// Gets the tenant user count summaries asynchronously.
            /// </summary>
            /// <param name="dateFilter">The date filter.</param>
            /// <param name="operator">The operator.</param>
            /// <returns></returns>
            public async Task<IList<DailyTenantCountSummary>> GetTenantUserCountSummariesAsync(DateTimeOffset? dateFilter = null, ODataOperator @operator = ODataOperator.GreaterThan)
            {
                var resource = "reports/tenantUserCount";
                var query = string.Empty;
                if (dateFilter.HasValue)
                {
                    query = new ODataQuery<DailyTenantCountSummary>()
                        .Where(x => x.TimeStamp, dateFilter.Value, @operator)
                        .ToString();
                }

                var result = await _client.ExecuteRequest<ODataResponse<DailyTenantCountSummary>>(HttpMethod.Get, resource, query, apiVersion: DefaultReportingApiVersion);
                return result.Value;
            }

            /// <summary>
            /// The number of authentications within a time period. The default is the last 30 days. 
            /// </summary>
            /// <param name="startTimeStamp">The start time stamp.</param>
            /// <param name="endTimeStamp">The end time stamp.</param>
            /// <returns></returns>
            public async Task<PeriodAuthenticationCounts> GetAuthenticationCountAsync(DateTimeOffset? startTimeStamp = null, DateTimeOffset? endTimeStamp = null)
            {
                var resource = "reports/b2cAuthenticationCount";
                var query = new ODataQuery<PeriodAuthenticationCounts>();
                if (startTimeStamp.HasValue)
                {
                    query = query.Where(x => x.StartTimeStamp, startTimeStamp.Value, ODataOperator.GreaterThan);
                }

                if (endTimeStamp.HasValue)
                {
                    query = query.Where(x => x.EndTimeStamp, endTimeStamp.Value, ODataOperator.LessThan);
                }

                // The actual expected filter for this call is on 'TimeStamp' property (even though the returned entity has different properties).
                var qs = query.ToString()
                    .Replace("StartTimeStamp", "TimeStamp")
                    .Replace("EndTimeStamp", "TimeStamp");

                var result = await _client.ExecuteRequest<ODataResponse<PeriodAuthenticationCounts>>(HttpMethod.Get, resource, qs, apiVersion: DefaultReportingApiVersion);
                return result.Value.Single();
            }


            /// <summary>
            /// The number of MFA requests within a time period. The default is the last 30 days. 
            /// </summary>
            /// <param name="startTimeStamp">The start time stamp.</param>
            /// <param name="endTimeStamp">The end time stamp.</param>
            /// <returns></returns>
            public async Task<PeriodMfaRequestCounts> GetMfaRequestCountAsync(DateTimeOffset? startTimeStamp = null, DateTimeOffset? endTimeStamp = null)
            {
                var resource = "reports/b2cMfaRequestCount";
                var query = new ODataQuery<PeriodMfaRequestCounts>();
                if (startTimeStamp.HasValue)
                {
                    query = query.Where(x => x.StartTimeStamp, startTimeStamp.Value, ODataOperator.GreaterThan);
                }

                if (endTimeStamp.HasValue)
                {
                    query = query.Where(x => x.EndTimeStamp, endTimeStamp.Value, ODataOperator.LessThan);
                }

                // The actual expected filter for this call is on 'TimeStamp' property (even though the returned entity has different properties).
                var qs = query.ToString()
                    .Replace("StartTimeStamp", "TimeStamp")
                    .Replace("EndTimeStamp", "TimeStamp");

                var result = await _client.ExecuteRequest<ODataResponse<PeriodMfaRequestCounts>>(HttpMethod.Get, resource, qs, apiVersion: DefaultReportingApiVersion);
                return result.Value.Single();
            }

            /// <summary>
            /// Gets the summaries of billable authentications over the last 30 days, by day and type of authentication flow.
            /// </summary>
            /// <returns></returns>
            public async Task<IList<DailyAuthenticationCountSummary>> GetAuthenticationCountSummariesAsync()
            {
                var resource = "reports/b2cAuthenticationCountSummary";
                var result = await _client.ExecuteRequest<ODataResponse<DailyAuthenticationCountSummary>>(HttpMethod.Get, resource, apiVersion: DefaultReportingApiVersion);
                return result.Value;
            }

            /// <summary>
            /// Gets the summaries of the daily number of multi-factor authentications, by day and type (SMS or voice).
            /// </summary>
            /// <returns></returns>
            public async Task<IList<DailyAuthenticationCountSummary>> GetMfaRequestCountSummariesAsync()
            {
                var resource = "reports/b2cMfaRequestCountSummary";
                var result = await _client.ExecuteRequest<ODataResponse<DailyAuthenticationCountSummary>>(HttpMethod.Get, resource, apiVersion: DefaultReportingApiVersion);
                return result.Value;
            }
        }
    }
}
