using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphLite.Reporting
{
    public class DailyAuthenticationCountSummary
    {
        [JsonProperty("Date")]
        public DateTimeOffset Date { get; set; }

        [JsonProperty("AuthenticationCount")]
        public long AuthenticationCount { get; set; }

        [JsonProperty("AuthFlowTypeBreakDown")]
        public AuthFlowTypeBreakDown AuthFlowTypeBreakDown { get; set; }
    }    
}
