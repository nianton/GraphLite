# Usage Reporting

GraphApiClient exposes a **.Reporting** property which exposes the Usage Reporting functionality available on AAD B2C. The interface implemented by the reporting client is:

```csharp
public interface IReportingClient
{
    Task<IList<DailyTenantCountSummary>> GetTenantUserCountSummariesAsync(DateTimeOffset? dateFilter, ODataOperator? @operator);
    Task<PeriodAuthenticationCounts> GetAuthenticationCountAsync(DateTimeOffset? startTimeStamp, DateTimeOffset? endTimeStamp);
    Task<IList<DailyAuthenticationCountSummary>> GetAuthenticationCountSummariesAsync();
    Task<IList<DailyAuthenticationCountSummary>> GetMfaRequestCountSummariesAsync();
}
```

## Get Tenant User Counts

Retrieves the  tenant user counts per day for the last 30 days, broken down by the identity providers. Optionally, a timestamp filter can be used to further filter the returned resultset.

```csharp
// Get the daily user counts for the last week.
var lastWeek = DateTimeOffset.UtcNow.Date.AddDays(-7);
var userCounts = await _reportingClient.GetTenantUserCountSummariesAsync(lastWeek, ODataOperator.GreaterThan);
```