using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphLite
{
    public class Link
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
