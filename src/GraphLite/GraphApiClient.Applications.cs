using System;
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
            var result = await ApplicationGetListAsync($"$filter=startswith(displayName, '{B2cExtensionsApplicationName}')");
            return result.Items.Single();
        }

        /// <summary>
        /// Gets the application extensions asynchronously.
        /// </summary>
        /// <param name="appObjectId">The application object identifier.</param>
        /// <returns>The list of extension properties defined.</returns>
        public async Task<List<ExtensionProperty>> GetApplicationExtensionsAsync(string appObjectId)
        {
            if (string.Equals(appObjectId, _b2cExtensionsObjectId) && _b2cExtensionsApplicationProperties != null)
                return _b2cExtensionsApplicationProperties.ToList();

            var result = await ExecuteRequest<ODataResponse<ExtensionProperty>>(HttpMethod.Get, $"applications/{appObjectId}/extensionProperties");
            return result.Value;
        }

        /// <summary>
        /// Adds the application extension asynchronous.
        /// </summary>
        /// <param name="appObjectId">The application object identifier.</param>
        /// <param name="extensionProperty">The extension property.</param>
        /// <returns>The created extension property.</returns>
        public async Task<ExtensionProperty> ApplicationAddExtensionPropertyAsync(string propertyName)
        {
            await EnsureInitAsync();
            var propertyExists = _b2cExtensionsApplicationProperties.Any(exp => string.Equals(propertyName, exp.GetSimpleName(), StringComparison.OrdinalIgnoreCase));
            if (propertyExists)
                throw new InvalidOperationException($"Extension property named: '{propertyName}' already exists");

            var extensionProperty = new ExtensionProperty
            {
                DataType = "String",
                Name = propertyName,
                TargetObjects = new List<string> { "User" }
            };

            var result = await ExecuteRequest<ExtensionProperty>(HttpMethod.Post, $"applications/{_b2cExtensionsObjectId}/extensionProperties", body: extensionProperty);
            _b2cExtensionsApplicationProperties.Add(result);
            return result;
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
                _b2cExtensionsApplicationProperties = await GetApplicationExtensionsAsync(extensionsApp.ObjectId);
            }

            return _b2cExtensionsApplicationId;
        }
    }
}
