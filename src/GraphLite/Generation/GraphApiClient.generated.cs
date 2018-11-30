using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace GraphLite
{
	public partial class GraphApiClient
	{

        /// <summary>
        /// Get a list of Users asynchronously.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="top">How many items will included.</param>
        /// <param name="skipToken">The skip token (returned from a previous query if not all items were included).</param>
		public async Task<GraphList<User>> UserGetListAsync(string query = null, int? top = null, string skipToken = null)
		{
			var qs = HttpUtility.ParseQueryString(query ?? string.Empty);
			if (top != null) {
				qs.Add("$top", $"{top}");
			}

			if (skipToken != null) {
				qs.Add("$skipToken", $"X'{skipToken}'");
			}

			var result = await ExecuteRequest<ODataResponse<User>>(HttpMethod.Get, "users", qs.ToString());
            var graphList = new GraphList<User>(result.Value, result.GetSkipToken());
			return graphList;
		}

		/// <summary>
        /// Get a list of Users asynchronously.
        /// </summary>
        /// <param name="query">The OData query.</param>
		public async Task<GraphList<User>> UserGetListAsync(ODataQuery<User> query)
		{
			var qs = query?.ToString() ?? string.Empty;
			var result = await ExecuteRequest<ODataResponse<User>>(HttpMethod.Get, "users", qs);
            var graphList = new GraphList<User>(result.Value, result.GetSkipToken());
			return graphList;
		}

		/// <summary>
        /// Get all Users asynchronously (use with caution).
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="itemsPerPage">How many items will included per page.</param>
        /// <param name="progresss">The progress reporter, will be called for every page of results fetched.</param>
		public async Task<List<User>> UserGetAllAsync(string query = null, int? itemsPerPage = null, IProgress<IList<User>> progress = null) 
		{
			var allItems = new List<User>();

			var qs = HttpUtility.ParseQueryString(query ?? string.Empty);
			if (itemsPerPage != null) {
				qs.Add("$top", $"{itemsPerPage}");
			}
			
			var result = await ExecuteRequest<ODataResponse<User>>(HttpMethod.Get, "users", qs.ToString());
			
			progress?.Report(result.Value);
			allItems.AddRange(result.Value);
			
			while (result.OdataNextLink != null) {				
				qs["$skiptoken"] = result.GetSkipToken();				
				result = await ExecuteRequest<ODataResponse<User>>(HttpMethod.Get, "users", qs.ToString());
			
				progress?.Report(result.Value);
				allItems.AddRange(result.Value);
			}

			return allItems;
		}
	
		/// <summary>
        /// Get a list of Users asynchronously for the specified identifiers.
        /// </summary>
        /// <param name="userObjectIds">The user object identifiers.</param>
		public async Task<List<User>> UserGetByObjectIdsAsync(params string[] userObjectIds) 
		{
			var body = new
            {
                objectIds = userObjectIds,
                types = new[] { "user" }
            };

			var result = await ExecuteRequest<ODataResponse<User>>(HttpMethod.Post, "getObjectsByObjectIds", body: body);
			return result.Value;
		}
		
		/// <summary>
        /// Gets a specific User instance.
        /// </summary>
        /// <param name="userObjectId">The user's object identifier.</param>
		public async Task<User> UserGetAsync(string userObjectId)
		{
			try 
			{
				var result = await ExecuteRequest<User>(HttpMethod.Get, $"users/{userObjectId}");            
				return result;
			}
			catch (GraphApiException gaex) 
			{
				if (gaex.StatusCode == HttpStatusCode.NotFound) 
				{
					return null;
				}

				throw;
			}
		}

		/// <summary>
        /// Creates a User instance.
        /// </summary>
        /// <param name="user">The User to be created.</param>
		public async Task<User> UserCreateAsync(User user)
		{
			await ValidateUserAsync(user);
			var result = await ExecuteRequest<User>(HttpMethod.Post, $"users", body: user);
			return result;
		}
		
		/// <summary>
        /// Updates a User instance.
        /// </summary>
		/// <param name="userObjectId">The user's object identifier.</param>
        /// <param name="modelChanges">The object containing the partial updates for the given User.</param>
		[Obsolete("Use the newly introduced strongly typed alternatives")]
		public async Task UserUpdateAsync(string userObjectId, object modelChanges)
		{
			var result = await ExecuteRequest(HttpMethodPatch, $"users/{userObjectId}", body: modelChanges);
		}

		/// <summary>
        /// Updates a User instance.
        /// </summary>
		/// <param name="userObjectId">The user's object identifier.</param>
        /// <param name="modelChanges">The object containing the partial updates for the given User.</param>
		public async Task UserUpdateAsync(string userObjectId, Action<User> action)
		{
			var user = new User();			
			if (user is IExtensionsApplicationAware extAppAware)
            {
                await EnsureInitAsync();
                extAppAware.SetExtensionsApplicationId(_b2cExtensionsApplicationId);
            }

			var modelChanges = user.Changes(action);
			var result = await ExecuteRequest(HttpMethodPatch, $"users/{userObjectId}", body: modelChanges);
		}

		/// <summary>
        /// Updates a User instance.
        /// </summary>
		/// <param name="userObjectId">The user's object identifier.</param>
        /// <param name="modelChanges">The object containing the partial updates for the given User.</param>
		public async Task UserUpdateAsync(User user, Action<User> action)
		{
			var userObjectId = user.ObjectId;
			if (user is IExtensionsApplicationAware extAppAware)
            {
                await EnsureInitAsync();
                extAppAware.SetExtensionsApplicationId(_b2cExtensionsApplicationId);
            }

			var modelChanges = user.Changes(action);
			var result = await ExecuteRequest(HttpMethodPatch, $"users/{userObjectId}", body: modelChanges);
		}

		/// <summary>
        /// Deletes a User instance.
        /// </summary>
		/// <param name="userObjectId">The user's object identifier.</param>
		public async Task UserDeleteAsync(string userObjectId) 
		{
			var result = await ExecuteRequest(HttpMethod.Delete, $"users/{userObjectId}");
		}

        /// <summary>
        /// Get a list of Groups asynchronously.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="top">How many items will included.</param>
        /// <param name="skipToken">The skip token (returned from a previous query if not all items were included).</param>
		public async Task<GraphList<Group>> GroupGetListAsync(string query = null, int? top = null, string skipToken = null)
		{
			var qs = HttpUtility.ParseQueryString(query ?? string.Empty);
			if (top != null) {
				qs.Add("$top", $"{top}");
			}

			if (skipToken != null) {
				qs.Add("$skipToken", $"X'{skipToken}'");
			}

			var result = await ExecuteRequest<ODataResponse<Group>>(HttpMethod.Get, "groups", qs.ToString());
            var graphList = new GraphList<Group>(result.Value, result.GetSkipToken());
			return graphList;
		}

		/// <summary>
        /// Get a list of Groups asynchronously.
        /// </summary>
        /// <param name="query">The OData query.</param>
		public async Task<GraphList<Group>> GroupGetListAsync(ODataQuery<Group> query)
		{
			var qs = query?.ToString() ?? string.Empty;
			var result = await ExecuteRequest<ODataResponse<Group>>(HttpMethod.Get, "groups", qs);
            var graphList = new GraphList<Group>(result.Value, result.GetSkipToken());
			return graphList;
		}

		/// <summary>
        /// Get all Groups asynchronously (use with caution).
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="itemsPerPage">How many items will included per page.</param>
        /// <param name="progresss">The progress reporter, will be called for every page of results fetched.</param>
		public async Task<List<Group>> GroupGetAllAsync(string query = null, int? itemsPerPage = null, IProgress<IList<Group>> progress = null) 
		{
			var allItems = new List<Group>();

			var qs = HttpUtility.ParseQueryString(query ?? string.Empty);
			if (itemsPerPage != null) {
				qs.Add("$top", $"{itemsPerPage}");
			}
			
			var result = await ExecuteRequest<ODataResponse<Group>>(HttpMethod.Get, "groups", qs.ToString());
			
			progress?.Report(result.Value);
			allItems.AddRange(result.Value);
			
			while (result.OdataNextLink != null) {				
				qs["$skiptoken"] = result.GetSkipToken();				
				result = await ExecuteRequest<ODataResponse<Group>>(HttpMethod.Get, "groups", qs.ToString());
			
				progress?.Report(result.Value);
				allItems.AddRange(result.Value);
			}

			return allItems;
		}
	
		/// <summary>
        /// Get a list of Groups asynchronously for the specified identifiers.
        /// </summary>
        /// <param name="groupObjectIds">The group object identifiers.</param>
		public async Task<List<Group>> GroupGetByObjectIdsAsync(params string[] groupObjectIds) 
		{
			var body = new
            {
                objectIds = groupObjectIds,
                types = new[] { "group" }
            };

			var result = await ExecuteRequest<ODataResponse<Group>>(HttpMethod.Post, "getObjectsByObjectIds", body: body);
			return result.Value;
		}
		
		/// <summary>
        /// Gets a specific Group instance.
        /// </summary>
        /// <param name="groupObjectId">The group's object identifier.</param>
		public async Task<Group> GroupGetAsync(string groupObjectId)
		{
			try 
			{
				var result = await ExecuteRequest<Group>(HttpMethod.Get, $"groups/{groupObjectId}");            
				return result;
			}
			catch (GraphApiException gaex) 
			{
				if (gaex.StatusCode == HttpStatusCode.NotFound) 
				{
					return null;
				}

				throw;
			}
		}

		/// <summary>
        /// Creates a Group instance.
        /// </summary>
        /// <param name="group">The Group to be created.</param>
		public async Task<Group> GroupCreateAsync(Group group)
		{
			var result = await ExecuteRequest<Group>(HttpMethod.Post, $"groups", body: group);
			return result;
		}
		
		/// <summary>
        /// Updates a Group instance.
        /// </summary>
		/// <param name="groupObjectId">The group's object identifier.</param>
        /// <param name="modelChanges">The object containing the partial updates for the given Group.</param>
		[Obsolete("Use the newly introduced strongly typed alternatives")]
		public async Task GroupUpdateAsync(string groupObjectId, object modelChanges)
		{
			var result = await ExecuteRequest(HttpMethodPatch, $"groups/{groupObjectId}", body: modelChanges);
		}

		/// <summary>
        /// Updates a Group instance.
        /// </summary>
		/// <param name="groupObjectId">The group's object identifier.</param>
        /// <param name="modelChanges">The object containing the partial updates for the given Group.</param>
		public async Task GroupUpdateAsync(string groupObjectId, Action<Group> action)
		{
			var group = new Group();			
			if (group is IExtensionsApplicationAware extAppAware)
            {
                await EnsureInitAsync();
                extAppAware.SetExtensionsApplicationId(_b2cExtensionsApplicationId);
            }

			var modelChanges = group.Changes(action);
			var result = await ExecuteRequest(HttpMethodPatch, $"groups/{groupObjectId}", body: modelChanges);
		}

		/// <summary>
        /// Updates a Group instance.
        /// </summary>
		/// <param name="groupObjectId">The group's object identifier.</param>
        /// <param name="modelChanges">The object containing the partial updates for the given Group.</param>
		public async Task GroupUpdateAsync(Group group, Action<Group> action)
		{
			var groupObjectId = group.ObjectId;
			if (group is IExtensionsApplicationAware extAppAware)
            {
                await EnsureInitAsync();
                extAppAware.SetExtensionsApplicationId(_b2cExtensionsApplicationId);
            }

			var modelChanges = group.Changes(action);
			var result = await ExecuteRequest(HttpMethodPatch, $"groups/{groupObjectId}", body: modelChanges);
		}

		/// <summary>
        /// Deletes a Group instance.
        /// </summary>
		/// <param name="groupObjectId">The group's object identifier.</param>
		public async Task GroupDeleteAsync(string groupObjectId) 
		{
			var result = await ExecuteRequest(HttpMethod.Delete, $"groups/{groupObjectId}");
		}

        /// <summary>
        /// Get a list of Applications asynchronously.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="top">How many items will included.</param>
        /// <param name="skipToken">The skip token (returned from a previous query if not all items were included).</param>
		public async Task<GraphList<Application>> ApplicationGetListAsync(string query = null, int? top = null, string skipToken = null)
		{
			var qs = HttpUtility.ParseQueryString(query ?? string.Empty);
			if (top != null) {
				qs.Add("$top", $"{top}");
			}

			if (skipToken != null) {
				qs.Add("$skipToken", $"X'{skipToken}'");
			}

			var result = await ExecuteRequest<ODataResponse<Application>>(HttpMethod.Get, "applications", qs.ToString());
            var graphList = new GraphList<Application>(result.Value, result.GetSkipToken());
			return graphList;
		}

		/// <summary>
        /// Get a list of Applications asynchronously.
        /// </summary>
        /// <param name="query">The OData query.</param>
		public async Task<GraphList<Application>> ApplicationGetListAsync(ODataQuery<Application> query)
		{
			var qs = query?.ToString() ?? string.Empty;
			var result = await ExecuteRequest<ODataResponse<Application>>(HttpMethod.Get, "applications", qs);
            var graphList = new GraphList<Application>(result.Value, result.GetSkipToken());
			return graphList;
		}

		/// <summary>
        /// Get all Applications asynchronously (use with caution).
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="itemsPerPage">How many items will included per page.</param>
        /// <param name="progresss">The progress reporter, will be called for every page of results fetched.</param>
		public async Task<List<Application>> ApplicationGetAllAsync(string query = null, int? itemsPerPage = null, IProgress<IList<Application>> progress = null) 
		{
			var allItems = new List<Application>();

			var qs = HttpUtility.ParseQueryString(query ?? string.Empty);
			if (itemsPerPage != null) {
				qs.Add("$top", $"{itemsPerPage}");
			}
			
			var result = await ExecuteRequest<ODataResponse<Application>>(HttpMethod.Get, "applications", qs.ToString());
			
			progress?.Report(result.Value);
			allItems.AddRange(result.Value);
			
			while (result.OdataNextLink != null) {				
				qs["$skiptoken"] = result.GetSkipToken();				
				result = await ExecuteRequest<ODataResponse<Application>>(HttpMethod.Get, "applications", qs.ToString());
			
				progress?.Report(result.Value);
				allItems.AddRange(result.Value);
			}

			return allItems;
		}
	
		/// <summary>
        /// Get a list of Applications asynchronously for the specified identifiers.
        /// </summary>
        /// <param name="applicationObjectIds">The application object identifiers.</param>
		public async Task<List<Application>> ApplicationGetByObjectIdsAsync(params string[] applicationObjectIds) 
		{
			var body = new
            {
                objectIds = applicationObjectIds,
                types = new[] { "application" }
            };

			var result = await ExecuteRequest<ODataResponse<Application>>(HttpMethod.Post, "getObjectsByObjectIds", body: body);
			return result.Value;
		}
		
		/// <summary>
        /// Gets a specific Application instance.
        /// </summary>
        /// <param name="applicationObjectId">The application's object identifier.</param>
		public async Task<Application> ApplicationGetAsync(string applicationObjectId)
		{
			try 
			{
				var result = await ExecuteRequest<Application>(HttpMethod.Get, $"applications/{applicationObjectId}");            
				return result;
			}
			catch (GraphApiException gaex) 
			{
				if (gaex.StatusCode == HttpStatusCode.NotFound) 
				{
					return null;
				}

				throw;
			}
		}

		/// <summary>
        /// Creates a Application instance.
        /// </summary>
        /// <param name="application">The Application to be created.</param>
		public async Task<Application> ApplicationCreateAsync(Application application)
		{
			var result = await ExecuteRequest<Application>(HttpMethod.Post, $"applications", body: application);
			return result;
		}
		
		/// <summary>
        /// Updates a Application instance.
        /// </summary>
		/// <param name="applicationObjectId">The application's object identifier.</param>
        /// <param name="modelChanges">The object containing the partial updates for the given Application.</param>
		[Obsolete("Use the newly introduced strongly typed alternatives")]
		public async Task ApplicationUpdateAsync(string applicationObjectId, object modelChanges)
		{
			var result = await ExecuteRequest(HttpMethodPatch, $"applications/{applicationObjectId}", body: modelChanges);
		}

		/// <summary>
        /// Updates a Application instance.
        /// </summary>
		/// <param name="applicationObjectId">The application's object identifier.</param>
        /// <param name="modelChanges">The object containing the partial updates for the given Application.</param>
		public async Task ApplicationUpdateAsync(string applicationObjectId, Action<Application> action)
		{
			var application = new Application();			
			if (application is IExtensionsApplicationAware extAppAware)
            {
                await EnsureInitAsync();
                extAppAware.SetExtensionsApplicationId(_b2cExtensionsApplicationId);
            }

			var modelChanges = application.Changes(action);
			var result = await ExecuteRequest(HttpMethodPatch, $"applications/{applicationObjectId}", body: modelChanges);
		}

		/// <summary>
        /// Updates a Application instance.
        /// </summary>
		/// <param name="applicationObjectId">The application's object identifier.</param>
        /// <param name="modelChanges">The object containing the partial updates for the given Application.</param>
		public async Task ApplicationUpdateAsync(Application application, Action<Application> action)
		{
			var applicationObjectId = application.ObjectId;
			if (application is IExtensionsApplicationAware extAppAware)
            {
                await EnsureInitAsync();
                extAppAware.SetExtensionsApplicationId(_b2cExtensionsApplicationId);
            }

			var modelChanges = application.Changes(action);
			var result = await ExecuteRequest(HttpMethodPatch, $"applications/{applicationObjectId}", body: modelChanges);
		}

		/// <summary>
        /// Deletes a Application instance.
        /// </summary>
		/// <param name="applicationObjectId">The application's object identifier.</param>
		public async Task ApplicationDeleteAsync(string applicationObjectId) 
		{
			var result = await ExecuteRequest(HttpMethod.Delete, $"applications/{applicationObjectId}");
		}

	}
}