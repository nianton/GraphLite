using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GraphLite
{
    public partial class GraphApiClient
    {
        /// <summary>
        /// Gets the B2C extensions application asynchronously.
        /// </summary>
        /// <returns></returns>
        public async Task<Application> GetB2cExtensionsApplicationAsync()
        {
            var result = await ApplicationGetListAsync($"$filter=startswith(displayName, '{GraphLiteConfiguration.B2cExtensionsApplicationName}')");
            return result.Items.Single();
        }

        /// <summary>
        /// Gets the application extensions asynchronously.
        /// </summary>
        /// <param name="appObjectId">The application object identifier.</param>
        /// <returns>The list of extension properties defined.</returns>
        public async Task<List<ExtensionProperty>> GetApplicationExtensionsAsync(string appObjectId)
        {
            var result = await ExecuteRequest<ODataResponse<ExtensionProperty>>(HttpMethod.Get, $"applications/{appObjectId}/extensionProperties");
            return result.Value;
        }

        /// <summary>
        /// Gets the B2C extensions application metadata asynchronously.
        /// </summary>
        /// <returns></returns>
        private async Task<string> EnsureB2cExtensionsApplicationMetadataAsync()
        {
            if (_b2cExtensionsApplicationId == null)
            {
                var extensionsApp = await GetB2cExtensionsApplicationAsync();
                _b2cExtensionsApplicationId = extensionsApp.AppId;
                _b2cExtensionsObjectId = extensionsApp.ObjectId;
                //_b2cExtensionsApplicationId = _b2cExtensionsObjectId;
                _b2cExtensionsApplicationProperties = await GetApplicationExtensionsAsync(extensionsApp.ObjectId);
            }

            return _b2cExtensionsApplicationId;
        }
    }
}
