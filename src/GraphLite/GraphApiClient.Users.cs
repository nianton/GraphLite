using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GraphLite
{
    public partial class GraphApiClient
    {
        /// <summary>
        /// Get user's thumbnail data.
        /// </summary>
        /// <param name="userObjectId">The user's object identifier</param>
        /// <returns>The thumbnail image bytes asynchronously.</returns>
        public async Task<byte[]> UserGetThumbnailAsync(string userObjectId)
        {
            var resource = $"users/{userObjectId}/thumbnailPhoto";
            var responseMessage = await DoExecuteRequest(HttpMethod.Get, resource, acceptedContentTypes: new[] { "image/jpeg", "image/jpg", "image/png", "image/gif" });
            var responseData = await responseMessage.Content.ReadAsByteArrayAsync();
            return responseData;
        }

        /// <summary>
        /// Updates the user's thumbnail. Note: Max size allowed for the thumbnail is 100kb.
        /// </summary>
        /// <param name="userObjectId">The user's object identifier</param>
        /// <param name="imageData">The thumbnail's image data.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task UserUpdateThumbnailAsync(string userObjectId, byte[] imageData, string contentType = "image/jpeg")
        {
            if (imageData == null)
                throw new ArgumentNullException(nameof(imageData));

            if (imageData.Length > MaxThumbnailPhotoSize)
                throw new ArgumentOutOfRangeException(nameof(imageData), $"The max size allowed for thumbnail photo is {MaxThumbnailPhotoSize} bytes.");

            var resource = $"users/{userObjectId}/thumbnailPhoto";
            var responseMessage = await DoExecuteRequest(HttpMethod.Put, resource, body: imageData, contentType: contentType);
            var response = await responseMessage.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Resets user's password.
        /// </summary>
        /// <param name="userObjectId">The user's object identifier</param>
        /// <param name="newPassword">The new password.</param>
        /// <param name="forceChangePasswordNextLogin">Whether the user should be forced to change password on next login.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task UserResetPasswordAsync(string userObjectId, string newPassword, bool forceChangePasswordNextLogin)
        {
            var body = new
            {
                passwordProfile = new PasswordProfile
                {
                    ForceChangePasswordNextLogin = forceChangePasswordNextLogin,
                    Password = newPassword
                }
            };

            var resource = $"users/{userObjectId}";
            var responseMessage = await DoExecuteRequest(HttpMethodPatch, resource, body: body);
            var response = await responseMessage.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Retrieves the user's member groups.
        /// </summary>
        /// <param name="userObjectId"></param>
        /// <returns></returns>
        public async Task<List<string>> UserGetMemberGroupsAsync(string userObjectId)
        {
            var body = new { securityEnabledOnly = false };
            var result = await ExecuteRequest<ODataResponse<string>>(HttpMethod.Post, $"users/{userObjectId}/getMemberGroups", body: body);
            return result.Value;
        }

        /// <summary>
        /// Retrieves the user by the signin name.
        /// </summary>
        /// <param name="signinName"></param>
        /// <returns></returns>
        public async Task<User> UserGetBySigninNameAsync(string signinName)
        {
            var query = $"$filter=signInNames/any(x: x/value eq '{signinName}')";
            var result = await ExecuteRequest<ODataResponse<User>>(HttpMethod.Get, $"users", query);
            return result.Value.SingleOrDefault();
        }

        /// <summary>
        /// Invalidated user's refresh tokens.
        /// </summary>
        /// <param name="userObjectId">The user's object identifier.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task UserInvalidateRefreshTokensAsync(string userObjectId)
        {
            var resource = $"users/{userObjectId}/invalidateAllRefreshTokens";
            var responseMessage = await DoExecuteRequest(HttpMethod.Post, resource);
            var response = await responseMessage.Content.ReadAsStringAsync();
        }

        public async Task<UserQuery> UserQueryCreateAsync()
        {
            await EnsureInitAsync();
            return new UserQuery(_b2cExtensionsApplicationId);
        }

        private async Task ValidateUserAsync(User user)
        {
            if (user.ExtendedProperties == null || !user.ExtendedProperties.Any())
            {
                // No extended properties to validate.
                return;
            }

            // Ensure initialization (access to b2c extension app/properties)
            await EnsureInitAsync();
            ((IExtensionsApplicationAware)user).SetExtensionsApplicationId(_b2cExtensionsApplicationId);

            var invalidExtensionProperties = user.ExtendedProperties.Keys
                .Where(key => key.StartsWith("extension_"))
                .Where(key => !_b2cExtensionsApplicationProperties.Any(exp => string.Equals(exp.Name, key, StringComparison.OrdinalIgnoreCase)));

            if (invalidExtensionProperties.Any())
            {
                var prefix = $"extension_{_b2cExtensionsApplicationId.Replace("-", string.Empty)}_";
                var message = $"User validation failed: The following properties do not exist on the current tenant: "
                    + string.Join(", ", invalidExtensionProperties.Select(exp => exp.Replace(prefix, string.Empty)));

                throw new InvalidOperationException(message);
            }
        }
    }
}