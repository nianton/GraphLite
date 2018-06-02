using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GraphLite
{
    public partial class GraphApiClient
    {
        public async Task<byte[]> GetUserThumbnailAsync(string userObjectId)
        {
            var resource = $"users/{userObjectId}/thumbnailPhoto";
            var responseMessage = await DoExecuteRequest(HttpMethod.Get, resource, acceptedContentTypes: new[] { "image/jpeg", "image/jpg", "image/png", "image/gif" });
            var responseData = await responseMessage.Content.ReadAsByteArrayAsync();
            return responseData;
        }

        /// <summary>
        /// Updates the user's thumbnail. Note: Max size allowed for the thumbnail is 100kb.
        /// </summary>
        /// <param name="userObjectId"></param>
        /// <param name="imageData"></param>
        /// <returns></returns>
        public async Task UpdateUserThumbnailAsync(string userObjectId, byte[] imageData, string contentType = "image/jpeg")
        {
            if (imageData == null)
                throw new ArgumentNullException(nameof(imageData));

            if (imageData.Length > MaxThumbnailPhotoSize)
                throw new ArgumentOutOfRangeException(nameof(imageData), $"The max size allowed for thumbnail photo is {MaxThumbnailPhotoSize} bytes.");

            var resource = $"users/{userObjectId}/thumbnailPhoto";
            var responseMessage = await DoExecuteRequest(HttpMethod.Put, resource, body: imageData, contentType: contentType);
            var response = await responseMessage.Content.ReadAsStringAsync();
        }

        public async Task ResetUserPasswordAsync(string userObjectId, string newPassword, bool forceChangePasswordNextLogin)
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

        public async Task InvalidateUserRefreshTokens(string userObjectId)
        {
            var resource = $"users/{userObjectId}/invalidateAllRefreshTokens";
            var responseMessage = await DoExecuteRequest(HttpMethod.Post, resource);
            var response = await responseMessage.Content.ReadAsStringAsync();
        }
    }
}
