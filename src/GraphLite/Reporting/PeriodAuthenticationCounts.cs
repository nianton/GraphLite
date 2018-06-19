using Newtonsoft.Json;
using System;

namespace GraphLite.Reporting
{
    public class PeriodAuthenticationCounts
    {
        [JsonProperty("StartTimeStamp")]
        public DateTimeOffset StartTimeStamp { get; set; }

        [JsonProperty("AuthenticationCount")]
        public long AuthenticationCount { get; set; }

        [JsonProperty("EndTimeStamp")]
        public DateTimeOffset EndTimeStamp { get; set; }

        [JsonProperty("AuthFlowTypeBreakDown")]
        public AuthFlowTypeBreakDown AuthFlowTypeBreakDown { get; set; }
    }
}
