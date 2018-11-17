using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web;

namespace GraphLite
{
	
	public partial class B2CPolicy
	{
		[JsonProperty("id")]
		public string Id { get; set; }
		[JsonProperty("type")]
		public string Type { get; set; }
		[JsonProperty("multifactorAuthenticationEnabled")]
		public bool MultifactorAuthenticationEnabled { get; set; }
		[JsonProperty("tokenLifetimeConfiguration")]
		public TokenLifetimeConfiguration TokenLifetimeConfiguration { get; set; }
		[JsonProperty("singleSignOnSessionConfiguration")]
		public SingleSignOnSessionConfiguration SingleSignOnSessionConfiguration { get; set; }
		[JsonProperty("tokenClaimsConfiguration")]
		public TokenClaimsConfiguration TokenClaimsConfiguration { get; set; }
		[JsonProperty("uiCustomizations")]
		public List<UiCustomization> UiCustomizations { get; set; }
	}
	
	public partial class IdentityProvider
	{
		[JsonProperty("id")]
		public string Id { get; set; }
		[JsonProperty("type")]
		public string Type { get; set; }
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("clientId")]
		public string ClientId { get; set; }
		[JsonProperty("clientSecret")]
		public string ClientSecret { get; set; }
	}
	
	public partial class B2CUserAttribute
	{
		[JsonProperty("id")]
		public string Id { get; set; }
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("adminHelpText")]
		public string AdminHelpText { get; set; }
		[JsonProperty("isBuiltInType")]
		public bool IsBuiltInType { get; set; }
		[JsonProperty("dataType")]
		public string DataType { get; set; }
		[JsonProperty("canChangeUserInputType")]
		public bool CanChangeUserInputType { get; set; }
	}
	
	public partial class TrustFrameworkPolicy
	{
		[JsonProperty("id")]
		public string Id { get; set; }
	}
	
	public partial class TrustFrameworkKeySet
	{
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("id")]
		public string Id { get; set; }
		[JsonProperty("keys")]
		public List<TrustFrameworkKey> Keys { get; set; }
	}
	public partial class TokenLifetimeConfiguration
	{ 
		[JsonProperty("accessIdTokenLifetime")]
		public int AccessIdTokenLifetime { get; set; }
		[JsonProperty("refreshTokenLifetime")]
		public int RefreshTokenLifetime { get; set; }
		[JsonProperty("refreshTokenExpires")]
		public bool RefreshTokenExpires { get; set; }
		[JsonProperty("rollingRefreshTokenLifetime")]
		public int RollingRefreshTokenLifetime { get; set; }
	}

	public partial class SingleSignOnSessionConfiguration
	{ 
		[JsonProperty("sessionExpirationTime")]
		public int SessionExpirationTime { get; set; }
		[JsonProperty("sessionExpirationType")]
		public string SessionExpirationType { get; set; }
		[JsonProperty("sessionScopeType")]
		public string SessionScopeType { get; set; }
		[JsonProperty("enforceIdTokenHintOnLogout")]
		public bool EnforceIdTokenHintOnLogout { get; set; }
	}

	public partial class TokenClaimsConfiguration
	{ 
		[JsonProperty("acrClaimPatternType")]
		public string AcrClaimPatternType { get; set; }
		[JsonProperty("issClaimPatternType")]
		public string IssClaimPatternType { get; set; }
		[JsonProperty("tfpClaimPatternType")]
		public string TfpClaimPatternType { get; set; }
		[JsonProperty("subClaimPatternType")]
		public string SubClaimPatternType { get; set; }
	}

	public partial class UiCustomization
	{ 
		[JsonProperty("type")]
		public string Type { get; set; }
		[JsonProperty("pageUri")]
		public string PageUri { get; set; }
		[JsonProperty("isCustomUx")]
		public bool? IsCustomUx { get; set; }
	}

	public partial class TrustFrameworkKey
	{ 
		[JsonProperty("k")]
		public string K { get; set; }
		[JsonProperty("kty")]
		public string Kty { get; set; }
		[JsonProperty("use")]
		public string Use { get; set; }
		[JsonProperty("exp")]
		public object Exp { get; set; }
		[JsonProperty("nbf")]
		public object Nbf { get; set; }
		[JsonProperty("kid")]
		public string Kid { get; set; }
		[JsonProperty("e")]
		public string E { get; set; }
		[JsonProperty("n")]
		public string N { get; set; }
		[JsonProperty("d")]
		public string D { get; set; }
		[JsonProperty("p")]
		public string P { get; set; }
		[JsonProperty("q")]
		public string Q { get; set; }
		[JsonProperty("dp")]
		public string Dp { get; set; }
		[JsonProperty("dq")]
		public string Dq { get; set; }
		[JsonProperty("qi")]
		public string Qi { get; set; }
	}

	public partial class B2cAdminApiClient : BaseApiClient
	{
        /// <summary>
        /// Get a list of b2CPolicies asynchronously.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="top">How many items will included.</param>
        /// <param name="skipToken">The skip token (returned from a previous query if not all items were included).</param>
		public async Task<GraphList<B2CPolicy>> B2CPolicyGetListAsync(string query = null, int? top = null, string skipToken = null)
		{
			var qs = HttpUtility.ParseQueryString(query ?? string.Empty);
			if (top != null) {
				qs.Add("$top", $"{top}");
			}

			if (skipToken != null) {
				qs.Add("$skipToken", $"X'{skipToken}'");
			}

			var result = await ExecuteRequest<ODataResponse<B2CPolicy>>(HttpMethod.Get, "b2CPolicies", qs.ToString());
            var graphList = new GraphList<B2CPolicy>(result.Value, result.GetSkipToken());
			return graphList;
		}

		/// <summary>
        /// Get a list of b2CPolicies asynchronously.
        /// </summary>
        /// <param name="query">The OData query.</param>
		public async Task<GraphList<B2CPolicy>> B2CPolicyGetListAsync(ODataQuery<B2CPolicy> query)
		{
			var qs = query?.ToString() ?? string.Empty;
			var result = await ExecuteRequest<ODataResponse<B2CPolicy>>(HttpMethod.Get, "b2CPolicies", qs);
            var graphList = new GraphList<B2CPolicy>(result.Value, result.GetSkipToken());
			return graphList;
		}

		/// <summary>
        /// Get all b2CPolicies asynchronously (use with caution).
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="itemsPerPage">How many items will included per page.</param>
        /// <param name="progresss">The progress reporter, will be called for every page of results fetched.</param>
		public async Task<List<B2CPolicy>> B2CPolicyGetAllAsync(string query = null, int? itemsPerPage = null, IProgress<IList<B2CPolicy>> progress = null) 
		{
			var allItems = new List<B2CPolicy>();

			var qs = HttpUtility.ParseQueryString(query ?? string.Empty);
			if (itemsPerPage != null) {
				qs.Add("$top", $"{itemsPerPage}");
			}
			
			var result = await ExecuteRequest<ODataResponse<B2CPolicy>>(HttpMethod.Get, "b2CPolicies", qs.ToString());
			
			progress?.Report(result.Value);
			allItems.AddRange(result.Value);
			
			while (result.OdataNextLink != null) {				
				qs["$skiptoken"] = result.GetSkipToken();				
				result = await ExecuteRequest<ODataResponse<B2CPolicy>>(HttpMethod.Get, "b2CPolicies", qs.ToString());
			
				progress?.Report(result.Value);
				allItems.AddRange(result.Value);
			}

			return allItems;
		}
	
		/// <summary>
        /// Get a list of b2CPolicies asynchronously for the specified identifiers.
        /// </summary>
        /// <param name="b2cpolicyObjectIds">The b2cpolicy object identifiers.</param>
		public async Task<List<B2CPolicy>> B2CPolicyGetByObjectIdsAsync(params string[] b2cpolicyObjectIds) 
		{
			var body = new
            {
                objectIds = b2cpolicyObjectIds,
                types = new[] { "b2cpolicy" }
            };

			var result = await ExecuteRequest<ODataResponse<B2CPolicy>>(HttpMethod.Post, "getObjectsByObjectIds", body: body);
			return result.Value;
		}
		
		/// <summary>
        /// Gets a specific B2CPolicy instance.
        /// </summary>
        /// <param name="b2cpolicyObjectId">The b2cpolicy's object identifier.</param>
		public async Task<B2CPolicy> B2CPolicyGetAsync(string b2cpolicyObjectId)
		{
			try 
			{
				var result = await ExecuteRequest<B2CPolicy>(HttpMethod.Get, $"b2CPolicies/{b2cpolicyObjectId}");            
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
        /// Creates a B2CPolicy instance.
        /// </summary>
        /// <param name="b2cpolicy">The B2CPolicy to be created.</param>
		public async Task<B2CPolicy> B2CPolicyCreateAsync(B2CPolicy b2cpolicy)
		{
			var result = await ExecuteRequest<B2CPolicy>(HttpMethod.Post, $"b2CPolicies", body: b2cpolicy);
			return result;
		}
		
		/// <summary>
        /// Updates a B2CPolicy instance.
        /// </summary>
		/// <param name="b2cpolicyObjectId">The b2cpolicy's object identifier.</param>
        /// <param name="modelChanges">The object containing the partial updates for the given B2CPolicy.</param>
		public async Task B2CPolicyUpdateAsync(string b2cpolicyObjectId, object modelChanges)
		{
			var result = await ExecuteRequest(HttpMethodPatch, $"b2CPolicies/{b2cpolicyObjectId}", body: modelChanges);
		}

		/// <summary>
        /// Deletes a B2CPolicy instance.
        /// </summary>
		/// <param name="b2cpolicyObjectId">The b2cpolicy's object identifier.</param>
		public async Task B2CPolicyDeleteAsync(string b2cpolicyObjectId) 
		{
			var result = await ExecuteRequest(HttpMethod.Delete, $"b2CPolicies/{b2cpolicyObjectId}");
		}

        /// <summary>
        /// Get a list of identityProviders asynchronously.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="top">How many items will included.</param>
        /// <param name="skipToken">The skip token (returned from a previous query if not all items were included).</param>
		public async Task<GraphList<IdentityProvider>> IdentityProviderGetListAsync(string query = null, int? top = null, string skipToken = null)
		{
			var qs = HttpUtility.ParseQueryString(query ?? string.Empty);
			if (top != null) {
				qs.Add("$top", $"{top}");
			}

			if (skipToken != null) {
				qs.Add("$skipToken", $"X'{skipToken}'");
			}

			var result = await ExecuteRequest<ODataResponse<IdentityProvider>>(HttpMethod.Get, "identityProviders", qs.ToString());
            var graphList = new GraphList<IdentityProvider>(result.Value, result.GetSkipToken());
			return graphList;
		}

		/// <summary>
        /// Get a list of identityProviders asynchronously.
        /// </summary>
        /// <param name="query">The OData query.</param>
		public async Task<GraphList<IdentityProvider>> IdentityProviderGetListAsync(ODataQuery<IdentityProvider> query)
		{
			var qs = query?.ToString() ?? string.Empty;
			var result = await ExecuteRequest<ODataResponse<IdentityProvider>>(HttpMethod.Get, "identityProviders", qs);
            var graphList = new GraphList<IdentityProvider>(result.Value, result.GetSkipToken());
			return graphList;
		}

		/// <summary>
        /// Get all identityProviders asynchronously (use with caution).
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="itemsPerPage">How many items will included per page.</param>
        /// <param name="progresss">The progress reporter, will be called for every page of results fetched.</param>
		public async Task<List<IdentityProvider>> IdentityProviderGetAllAsync(string query = null, int? itemsPerPage = null, IProgress<IList<IdentityProvider>> progress = null) 
		{
			var allItems = new List<IdentityProvider>();

			var qs = HttpUtility.ParseQueryString(query ?? string.Empty);
			if (itemsPerPage != null) {
				qs.Add("$top", $"{itemsPerPage}");
			}
			
			var result = await ExecuteRequest<ODataResponse<IdentityProvider>>(HttpMethod.Get, "identityProviders", qs.ToString());
			
			progress?.Report(result.Value);
			allItems.AddRange(result.Value);
			
			while (result.OdataNextLink != null) {				
				qs["$skiptoken"] = result.GetSkipToken();				
				result = await ExecuteRequest<ODataResponse<IdentityProvider>>(HttpMethod.Get, "identityProviders", qs.ToString());
			
				progress?.Report(result.Value);
				allItems.AddRange(result.Value);
			}

			return allItems;
		}
	
		/// <summary>
        /// Get a list of identityProviders asynchronously for the specified identifiers.
        /// </summary>
        /// <param name="identityproviderObjectIds">The identityprovider object identifiers.</param>
		public async Task<List<IdentityProvider>> IdentityProviderGetByObjectIdsAsync(params string[] identityproviderObjectIds) 
		{
			var body = new
            {
                objectIds = identityproviderObjectIds,
                types = new[] { "identityprovider" }
            };

			var result = await ExecuteRequest<ODataResponse<IdentityProvider>>(HttpMethod.Post, "getObjectsByObjectIds", body: body);
			return result.Value;
		}
		
		/// <summary>
        /// Gets a specific IdentityProvider instance.
        /// </summary>
        /// <param name="identityproviderObjectId">The identityprovider's object identifier.</param>
		public async Task<IdentityProvider> IdentityProviderGetAsync(string identityproviderObjectId)
		{
			try 
			{
				var result = await ExecuteRequest<IdentityProvider>(HttpMethod.Get, $"identityProviders/{identityproviderObjectId}");            
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
        /// Creates a IdentityProvider instance.
        /// </summary>
        /// <param name="identityprovider">The IdentityProvider to be created.</param>
		public async Task<IdentityProvider> IdentityProviderCreateAsync(IdentityProvider identityprovider)
		{
			var result = await ExecuteRequest<IdentityProvider>(HttpMethod.Post, $"identityProviders", body: identityprovider);
			return result;
		}
		
		/// <summary>
        /// Updates a IdentityProvider instance.
        /// </summary>
		/// <param name="identityproviderObjectId">The identityprovider's object identifier.</param>
        /// <param name="modelChanges">The object containing the partial updates for the given IdentityProvider.</param>
		public async Task IdentityProviderUpdateAsync(string identityproviderObjectId, object modelChanges)
		{
			var result = await ExecuteRequest(HttpMethodPatch, $"identityProviders/{identityproviderObjectId}", body: modelChanges);
		}

		/// <summary>
        /// Deletes a IdentityProvider instance.
        /// </summary>
		/// <param name="identityproviderObjectId">The identityprovider's object identifier.</param>
		public async Task IdentityProviderDeleteAsync(string identityproviderObjectId) 
		{
			var result = await ExecuteRequest(HttpMethod.Delete, $"identityProviders/{identityproviderObjectId}");
		}

        /// <summary>
        /// Get a list of b2CUserAttributes asynchronously.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="top">How many items will included.</param>
        /// <param name="skipToken">The skip token (returned from a previous query if not all items were included).</param>
		public async Task<GraphList<B2CUserAttribute>> B2CUserAttributeGetListAsync(string query = null, int? top = null, string skipToken = null)
		{
			var qs = HttpUtility.ParseQueryString(query ?? string.Empty);
			if (top != null) {
				qs.Add("$top", $"{top}");
			}

			if (skipToken != null) {
				qs.Add("$skipToken", $"X'{skipToken}'");
			}

			var result = await ExecuteRequest<ODataResponse<B2CUserAttribute>>(HttpMethod.Get, "b2CUserAttributes", qs.ToString());
            var graphList = new GraphList<B2CUserAttribute>(result.Value, result.GetSkipToken());
			return graphList;
		}

		/// <summary>
        /// Get a list of b2CUserAttributes asynchronously.
        /// </summary>
        /// <param name="query">The OData query.</param>
		public async Task<GraphList<B2CUserAttribute>> B2CUserAttributeGetListAsync(ODataQuery<B2CUserAttribute> query)
		{
			var qs = query?.ToString() ?? string.Empty;
			var result = await ExecuteRequest<ODataResponse<B2CUserAttribute>>(HttpMethod.Get, "b2CUserAttributes", qs);
            var graphList = new GraphList<B2CUserAttribute>(result.Value, result.GetSkipToken());
			return graphList;
		}

		/// <summary>
        /// Get all b2CUserAttributes asynchronously (use with caution).
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="itemsPerPage">How many items will included per page.</param>
        /// <param name="progresss">The progress reporter, will be called for every page of results fetched.</param>
		public async Task<List<B2CUserAttribute>> B2CUserAttributeGetAllAsync(string query = null, int? itemsPerPage = null, IProgress<IList<B2CUserAttribute>> progress = null) 
		{
			var allItems = new List<B2CUserAttribute>();

			var qs = HttpUtility.ParseQueryString(query ?? string.Empty);
			if (itemsPerPage != null) {
				qs.Add("$top", $"{itemsPerPage}");
			}
			
			var result = await ExecuteRequest<ODataResponse<B2CUserAttribute>>(HttpMethod.Get, "b2CUserAttributes", qs.ToString());
			
			progress?.Report(result.Value);
			allItems.AddRange(result.Value);
			
			while (result.OdataNextLink != null) {				
				qs["$skiptoken"] = result.GetSkipToken();				
				result = await ExecuteRequest<ODataResponse<B2CUserAttribute>>(HttpMethod.Get, "b2CUserAttributes", qs.ToString());
			
				progress?.Report(result.Value);
				allItems.AddRange(result.Value);
			}

			return allItems;
		}
	
		/// <summary>
        /// Get a list of b2CUserAttributes asynchronously for the specified identifiers.
        /// </summary>
        /// <param name="b2cuserattributeObjectIds">The b2cuserattribute object identifiers.</param>
		public async Task<List<B2CUserAttribute>> B2CUserAttributeGetByObjectIdsAsync(params string[] b2cuserattributeObjectIds) 
		{
			var body = new
            {
                objectIds = b2cuserattributeObjectIds,
                types = new[] { "b2cuserattribute" }
            };

			var result = await ExecuteRequest<ODataResponse<B2CUserAttribute>>(HttpMethod.Post, "getObjectsByObjectIds", body: body);
			return result.Value;
		}
		
		/// <summary>
        /// Gets a specific B2CUserAttribute instance.
        /// </summary>
        /// <param name="b2cuserattributeObjectId">The b2cuserattribute's object identifier.</param>
		public async Task<B2CUserAttribute> B2CUserAttributeGetAsync(string b2cuserattributeObjectId)
		{
			try 
			{
				var result = await ExecuteRequest<B2CUserAttribute>(HttpMethod.Get, $"b2CUserAttributes/{b2cuserattributeObjectId}");            
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
        /// Creates a B2CUserAttribute instance.
        /// </summary>
        /// <param name="b2cuserattribute">The B2CUserAttribute to be created.</param>
		public async Task<B2CUserAttribute> B2CUserAttributeCreateAsync(B2CUserAttribute b2cuserattribute)
		{
			var result = await ExecuteRequest<B2CUserAttribute>(HttpMethod.Post, $"b2CUserAttributes", body: b2cuserattribute);
			return result;
		}
		
		/// <summary>
        /// Updates a B2CUserAttribute instance.
        /// </summary>
		/// <param name="b2cuserattributeObjectId">The b2cuserattribute's object identifier.</param>
        /// <param name="modelChanges">The object containing the partial updates for the given B2CUserAttribute.</param>
		public async Task B2CUserAttributeUpdateAsync(string b2cuserattributeObjectId, object modelChanges)
		{
			var result = await ExecuteRequest(HttpMethodPatch, $"b2CUserAttributes/{b2cuserattributeObjectId}", body: modelChanges);
		}

		/// <summary>
        /// Deletes a B2CUserAttribute instance.
        /// </summary>
		/// <param name="b2cuserattributeObjectId">The b2cuserattribute's object identifier.</param>
		public async Task B2CUserAttributeDeleteAsync(string b2cuserattributeObjectId) 
		{
			var result = await ExecuteRequest(HttpMethod.Delete, $"b2CUserAttributes/{b2cuserattributeObjectId}");
		}

	}
}
