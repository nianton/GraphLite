using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphLite
{
    internal interface IExtensionsApplicationAware
    {
        void SetExtensionsApplicationId(string appId);
    }

    public partial class User : IExtensionsApplicationAware
    {
        private string _extensionsApplicationId;

        [JsonProperty("odata.metadata")]
        protected string OdataMetadata { get; set; }

        [JsonProperty("odata.type")]
        public string OdataType { get; set; }

        [JsonProperty("thumbnailPhoto@odata.mediaContentType")]
        public string ThumbnailContentType { get; set; }

        [JsonExtensionData]
        public IDictionary<string, JToken> ExtendedProperties { get; set; }

        public IDictionary<string, object> GetExtendedProperties()
        {
            var prefix = $"extension_{_extensionsApplicationId.Replace("-", string.Empty)}_";
            var extendedProperties = ExtendedProperties
                .Where(kvp => kvp.Key.StartsWith(prefix))
                .ToDictionary(
                    kvp => kvp.Key.Replace(prefix, string.Empty),
                    kvp => kvp.Value.ToObject<object>());
            return extendedProperties;
        }

        public void SetExtendedProperty(string name, object value)
        {
            if (ExtendedProperties == null)
                ExtendedProperties = new Dictionary<string, JToken>();

            ExtendedProperties[$"extension_{_extensionsApplicationId.Replace("-", string.Empty)}_{name}"] = JToken.FromObject(value);
        }

        public TValue GetExtendedProperty<TValue>(string name)
        {
            var key = $"extension_{_extensionsApplicationId.Replace("-", string.Empty)}_{name}";
            var token = default(JToken);
            if (ExtendedProperties?.TryGetValue(key, out token) == true) {
                return token.ToObject<TValue>();
            }

            return default(TValue);
        }

        void IExtensionsApplicationAware.SetExtensionsApplicationId(string appId)
        {
            _extensionsApplicationId = appId;
        }
    }
}