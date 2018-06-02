using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GraphLite
{
    public partial class GraphApiClient
    {
        public async Task<List<string>> GetGroupMembers(string groupObjectId)
        {
            var result = await ExecuteRequest<ODataResponse<Link>>(HttpMethod.Get, $"groups/{groupObjectId}/$links/members");
            return result.Value.ConvertAll(l => l.Url);
        }

        public async Task<List<string>> GetMemberGroups(string userObjectId)
        {
            var body = new { securityEnabledOnly = false };
            var result = await ExecuteRequest<ODataResponse<string>>(HttpMethod.Post, $"users/{userObjectId}/getMemberGroups", body: body);
            return result.Value;
        }

        public async Task<bool> IsMemberOfGroup(string groupObjectId, string userObjectId)
        {
            var body = new { groupId = groupObjectId, memberId = userObjectId };
            var result = await ExecuteRequest<ODataSingleResponse<bool>>(HttpMethod.Post, $"isMemberOf", body: body);
            return result.Value;
        }

        public async Task AddGroupMember(string groupObjectId, string userObjectId)
        {
            var body = new Link { Url = GetDirectoryObjectUrl(userObjectId) };
            var result = await ExecuteRequest(HttpMethod.Post, $"groups/{groupObjectId}/$links/members", body: body);
        }

        public async Task RemoveGroupMember(string groupObjectId, string userObjectId)
        {
            var result = await ExecuteRequest(HttpMethod.Delete, $"groups/{groupObjectId}/$links/{userObjectId}");
        }
    }
}
