using Newtonsoft.Json;

namespace GraphLite
{
    public class ErrorResponse
    {
        [JsonProperty("odata.error")]
        public ODataError Error { get; set; }
    }

    public class ODataError
    {
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("message")]
        public ODataErrorMessage Message { get; set; }
    }

    public class ODataErrorMessage
    {
        [JsonProperty("lang")]
        public string Lang { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}