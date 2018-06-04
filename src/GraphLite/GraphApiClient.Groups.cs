using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GraphLite
{
    public partial class GraphApiClient
    {
        public async Task<List<string>> GroupGetMembersAsync(string groupObjectId)
        {
            var result = await ExecuteRequest<ODataResponse<Link>>(HttpMethod.Get, $"groups/{groupObjectId}/$links/members");
            return result.Value.ConvertAll(l => l.Url);
        }

        public async Task<bool> IsMemberOfGroupAsync(string groupObjectId, string userObjectId)
        {
            var body = new { groupId = groupObjectId, memberId = userObjectId };
            var result = await ExecuteRequest<ODataSingleResponse<bool>>(HttpMethod.Post, $"isMemberOf", body: body);
            return result.Value;
        }

        public async Task GroupAddMemberAsync(string groupObjectId, string userObjectId)
        {
            var body = new Link { Url = GetDirectoryObjectUrl(userObjectId) };
            var result = await ExecuteRequest(HttpMethod.Post, $"groups/{groupObjectId}/$links/members", body: body);
        }

        public async Task GroupRemoveMemberAsync(string groupObjectId, string userObjectId)
        {
            var result = await ExecuteRequest(HttpMethod.Delete, $"groups/{groupObjectId}/$links/{userObjectId}");
        }
    }
}
