using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace GraphLite.Reporting
{

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
