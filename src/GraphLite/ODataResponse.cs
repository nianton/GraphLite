using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GraphLite
{
    public partial class ODataResponse<TValue> : IODataResponse, IExtensionsApplicationAware
    {
        [JsonProperty("odata.metadata")]
        public string OdataMetadata { get; set; }

        [JsonProperty("odata.nextLink")]
        public string OdataNextLink { get; set; }

        [JsonProperty("value")]
        public List<TValue> Value { get; set; }

        public string GetSkipToken()
        {
            var query = HttpUtility.ParseQueryString(OdataNextLink?.Split('?').ElementAtOrDefault(1) ?? string.Empty);
            return query["$skiptoken"];
        }

        void IExtensionsApplicationAware.SetExtensionsApplicationId(string appId)
        {
            foreach (var item in Value?.OfType<IExtensionsApplicationAware>())
            {
                item.SetExtensionsApplicationId(appId);
            }
        }
    }

    public partial class ODataSingleResponse<TValue> : IODataResponse
    {
        [JsonProperty("odata.metadata")]
        public string OdataMetadata { get; set; }

        [JsonProperty("value")]
        public TValue Value { get; set; }
    }
}
