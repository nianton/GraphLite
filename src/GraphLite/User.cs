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
        const string _extensionsApplicationPlaceholder = "EXTENSION_B2C_APP";
        private string _extensionsApplicationId;

        [JsonProperty("odata.metadata")]
        protected string OdataMetadata { get; set; }

        [JsonProperty("odata.type")]
        public string OdataType { get; set; }

        [JsonProperty("thumbnailPhoto@odata.mediaContentType")]
        public string ThumbnailContentType { get; set; }

        [JsonProperty("thumbnailPhoto@odata.mediaEditLink")]
        public string ThumbnailEditLink { get; set; }

        [JsonExtensionData]
        public IDictionary<string, JToken> ExtendedProperties { get; set; }

        public IReadOnlyDictionary<string, object> GetExtendedProperties()
        {
            var prefix = GetExtendedPropertyPrefix();
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

            ExtendedProperties[$"{GetExtendedPropertyPrefix()}{name}"] = JToken.FromObject(value);
            
        }

        public TValue GetExtendedProperty<TValue>(string name)
        {
            var key = $"{GetExtendedPropertyPrefix()}{name}";
            var token = default(JToken);
            if (ExtendedProperties?.TryGetValue(key, out token) == true) {
                return token.ToObject<TValue>();
            }

            return default(TValue);
        }

        private string GetExtendedPropertyPrefix()
        {
            var ext = _extensionsApplicationId ?? _extensionsApplicationPlaceholder;
            return $"extension_{ext.Replace("-", string.Empty)}_";
        }

        void IExtensionsApplicationAware.SetExtensionsApplicationId(string appId)
        {
            _extensionsApplicationId = appId;
            if (ExtendedProperties == null || !ExtendedProperties.Any())
                return;

            foreach (var key in ExtendedProperties.Keys.ToList())
            {
                if (key.Contains(_extensionsApplicationPlaceholder))
                {
                    var value = ExtendedProperties[key];
                    var newKey = key.Replace(_extensionsApplicationPlaceholder, _extensionsApplicationId.Replace("-", string.Empty));

                    ExtendedProperties.Remove(key);                    
                    ExtendedProperties[newKey] = value;
                }
            }
        }
    }
}