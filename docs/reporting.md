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
var userCounts = await client.Reporting.GetTenantUserCountSummariesAsync(lastWeek, ODataOperator.GreaterThan);
```

## Get Authentication Counts

Summary of the daily number of billable authentications over the last 30 days, by day and type of authentication flow.

[TODO:// Documentation]

## Get Daily Authentication Count summaries

[TODO:// Documentation]

## Get Daily MFA Request Counts summaries

[TODO:// Documentation]


[<< Go back](./)