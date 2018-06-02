using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GraphLite
{
    public partial class GraphApiClient
    {
        public async Task<Application> GetB2cExtensionsApplicationAsync()
        {
            var result = await ApplicationGetListAsync($"$filter=startswith(displayName, '{B2cExtensionsApplicationName}')");
            return result.Items.Single();
        }

        private async Task<string> GetB2cExtensionsApplicationMetadataAsync()
        {
            if (_b2cExtensionsApplicationId == null)
            {
                var extensionsApp = await GetB2cExtensionsApplicationAsync();
                _b2cExtensionsApplicationId = extensionsApp.AppId;
                _b2cExtensionsApplicationProperties = await GetApplicationExtensionsAsync(extensionsApp.ObjectId);
            }

            return _b2cExtensionsApplicationId;
        }

        public async Task<List<ExtensionProperty>> GetApplicationExtensionsAsync(string appObjectId)
        {
            var result = await ExecuteRequest<ODataResponse<ExtensionProperty>>(HttpMethod.Get, $"applications/{appObjectId}/extensionProperties");
            return result.Value;
        }
    }
}
