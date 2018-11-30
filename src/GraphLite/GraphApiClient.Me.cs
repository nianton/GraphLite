using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GraphLite
{
    public partial class GraphApiClient
    {
        public async Task MeChangePassword(ChangePasswordRequest passwordRequest)
        {
            var resource = $"me/changePassword";
            var responseMessage = await DoExecuteRequest(HttpMethod.Post, resource);
            var response = await responseMessage.Content.ReadAsStringAsync();
        }

    }

    public class ChangePasswordRequest
    {
        [JsonProperty("currentPassword")]
        public string CurrentPassword { get; set; }

        [JsonProperty("newPassword")]
        public string NewPassword { get; set; }
    }
}
