using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphLite.Reporting
{
    public partial class DailyTenantCountSummary
    {
        [JsonProperty("TimeStamp")]
        public DateTimeOffset TimeStamp { get; set; }

        [JsonProperty("TotalUserCount")]
        public long TotalUserCount { get; set; }

        [JsonProperty("LocalUserCount")]
        public long LocalUserCount { get; set; }

        [JsonProperty("OtherUserCount")]
        public long OtherUserCount { get; set; }

        [JsonProperty("AlternateIdUserCount")]
        public long AlternateIdUserCount { get; set; }

        [JsonProperty("AlternateIdUserBreakDown")]
        public Dictionary<string, int> AlternateIdUserBreakDown { get; set; }
    }
}
