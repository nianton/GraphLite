using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GraphLite.Reporting
{
    public class PeriodMfaRequestCounts
    {
        [JsonProperty("StartTimeStamp")]
        public DateTimeOffset StartTimeStamp { get; set; }

        [JsonProperty("EndTimeStamp")]
        public DateTimeOffset EndTimeStamp { get; set; }

        [JsonProperty("MfaCount")]
        public long MfaCount { get; set; }

        [JsonProperty("MfaTypeBreakDown")]
        public Dictionary<string, long> MfaTypeBreakDown { get; set; }
    }
}
