using GraphLite.Reporting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphLite
{
    /// <summary>
    /// AAD B2C Usage Reporting client
    /// </summary>
    public interface IReportingClient
    {
        /// <summary>
        /// Gets the tenant user count summaries asynchronously.
        /// </summary>
        /// <param name="dateFilter">The date filter.</param>
        /// <param name="operator">The operator.</param>
        Task<IList<DailyTenantCountSummary>> GetTenantUserCountSummariesAsync(DateTimeOffset? dateFilter = null, ODataOperator @operator = ODataOperator.GreaterThan);

        /// <summary>
        /// The number of authentications within a time period asynchronously. The default is the last 30 days. 
        /// </summary>
        /// <param name="startTimeStamp">The start time stamp.</param>
        /// <param name="endTimeStamp">The end time stamp.</param>
        Task<PeriodAuthenticationCounts> GetAuthenticationCountAsync(DateTimeOffset? startTimeStamp = null, DateTimeOffset? endTimeStamp = null);

        /// <summary>
        /// The number of MFA requests within a time period. The default is the last 30 days. 
        /// </summary>
        /// <param name="startTimeStamp">The start time stamp.</param>
        /// <param name="endTimeStamp">The end time stamp.</param>
        Task<PeriodMfaRequestCounts> GetMfaRequestCountAsync(DateTimeOffset? startTimeStamp = null, DateTimeOffset? endTimeStamp = null);

        /// <summary>
        /// Gets the summaries of billable authentications over the last 30 days, by day and type of authentication flow.
        /// </summary>
        Task<IList<DailyAuthenticationCountSummary>> GetAuthenticationCountSummariesAsync();

        /// <summary>
        /// Gets the summaries of billable MFA requests over the last 30 days, by day and type of MFA request.
        /// </summary>
        Task<IList<DailyAuthenticationCountSummary>> GetMfaRequestCountSummariesAsync();
    }
}
