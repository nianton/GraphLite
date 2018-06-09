using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

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

    public class AuthFlowTypeBreakDown
    {
        [JsonProperty("implicit")]
        public long Implicit { get; set; }

        [JsonProperty("hybrid")]
        public long Hybrid { get; set; }

        [JsonProperty("confclient-authcode")]
        public long ConfidentialClientAuthcode { get; set; }

        [JsonProperty("publicclient-refreshtoken")]
        public long PublicClientRefreshtoken { get; set; }
    }
}
