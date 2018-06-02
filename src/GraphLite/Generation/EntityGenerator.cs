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
	
	public partial class DirectoryObject
	{
		[JsonProperty("objectType")]
		public string ObjectType { get; set; }
		[JsonProperty("objectId")]
		public string ObjectId { get; set; }
		[JsonProperty("deletionTimestamp")]
		public DateTimeOffset? DeletionTimestamp { get; set; }
	}
	
	public partial class User : DirectoryObject
	{
		[JsonProperty("accountEnabled")]
		public bool? AccountEnabled { get; set; }
		[JsonProperty("ageGroup")]
		public string AgeGroup { get; set; }
		[JsonProperty("assignedLicenses")]
		public List<AssignedLicense> AssignedLicenses { get; set; }
		[JsonProperty("assignedPlans")]
		public List<AssignedPlan> AssignedPlans { get; set; }
		[JsonProperty("city")]
		public string City { get; set; }
		[JsonProperty("companyName")]
		public string CompanyName { get; set; }
		[JsonProperty("consentProvidedForMinor")]
		public string ConsentProvidedForMinor { get; set; }
		[JsonProperty("country")]
		public string Country { get; set; }
		[JsonProperty("createdDateTime")]
		public DateTimeOffset? CreatedDateTime { get; set; }
		[JsonProperty("creationType")]
		public string CreationType { get; set; }
		[JsonProperty("department")]
		public string Department { get; set; }
		[JsonProperty("dirSyncEnabled")]
		public bool? DirSyncEnabled { get; set; }
		[JsonProperty("displayName")]
		public string DisplayName { get; set; }
		[JsonProperty("employeeId")]
		public string EmployeeId { get; set; }
		[JsonProperty("facsimileTelephoneNumber")]
		public string FacsimileTelephoneNumber { get; set; }
		[JsonProperty("givenName")]
		public string GivenName { get; set; }
		[JsonProperty("immutableId")]
		public string ImmutableId { get; set; }
		[JsonProperty("isCompromised")]
		public bool? IsCompromised { get; set; }
		[JsonProperty("jobTitle")]
		public string JobTitle { get; set; }
		[JsonProperty("lastDirSyncTime")]
		public DateTimeOffset? LastDirSyncTime { get; set; }
		[JsonProperty("legalAgeGroupClassification")]
		public string LegalAgeGroupClassification { get; set; }
		[JsonProperty("mail")]
		public string Mail { get; set; }
		[JsonProperty("mailNickname")]
		public string MailNickname { get; set; }
		[JsonProperty("mobile")]
		public string Mobile { get; set; }
		[JsonProperty("onPremisesDistinguishedName")]
		public string OnPremisesDistinguishedName { get; set; }
		[JsonProperty("onPremisesSecurityIdentifier")]
		public string OnPremisesSecurityIdentifier { get; set; }
		[JsonProperty("otherMails")]
		public List<string> OtherMails { get; set; }
		[JsonProperty("passwordPolicies")]
		public string PasswordPolicies { get; set; }
		[JsonProperty("passwordProfile")]
		public PasswordProfile PasswordProfile { get; set; }
		[JsonProperty("physicalDeliveryOfficeName")]
		public string PhysicalDeliveryOfficeName { get; set; }
		[JsonProperty("postalCode")]
		public string PostalCode { get; set; }
		[JsonProperty("preferredLanguage")]
		public string PreferredLanguage { get; set; }
		[JsonProperty("provisionedPlans")]
		public List<ProvisionedPlan> ProvisionedPlans { get; set; }
		[JsonProperty("provisioningErrors")]
		public List<ProvisioningError> ProvisioningErrors { get; set; }
		[JsonProperty("proxyAddresses")]
		public List<string> ProxyAddresses { get; set; }
		[JsonProperty("refreshTokensValidFromDateTime")]
		public DateTimeOffset? RefreshTokensValidFromDateTime { get; set; }
		[JsonProperty("showInAddressList")]
		public bool? ShowInAddressList { get; set; }
		[JsonProperty("signInNames")]
		public List<SignInName> SignInNames { get; set; }
		[JsonProperty("sipProxyAddress")]
		public string SipProxyAddress { get; set; }
		[JsonProperty("state")]
		public string State { get; set; }
		[JsonProperty("streetAddress")]
		public string StreetAddress { get; set; }
		[JsonProperty("surname")]
		public string Surname { get; set; }
		[JsonProperty("telephoneNumber")]
		public string TelephoneNumber { get; set; }
		[JsonProperty("thumbnailPhoto")]
		public object ThumbnailPhoto { get; set; }
		[JsonProperty("usageLocation")]
		public string UsageLocation { get; set; }
		[JsonProperty("userIdentities")]
		public List<UserIdentity> UserIdentities { get; set; }
		[JsonProperty("userPrincipalName")]
		public string UserPrincipalName { get; set; }
		[JsonProperty("userType")]
		public string UserType { get; set; }
	}
	
	public partial class ExtensionProperty : DirectoryObject
	{
		[JsonProperty("appDisplayName")]
		public string AppDisplayName { get; set; }
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("dataType")]
		public string DataType { get; set; }
		[JsonProperty("isSyncedFromOnPremises")]
		public bool? IsSyncedFromOnPremises { get; set; }
		[JsonProperty("targetObjects")]
		public List<string> TargetObjects { get; set; }
	}
	
	public partial class Application : DirectoryObject
	{
		[JsonProperty("acceptMappedClaims")]
		public bool? AcceptMappedClaims { get; set; }
		[JsonProperty("addIns")]
		public List<AddIn> AddIns { get; set; }
		[JsonProperty("appId")]
		public string AppId { get; set; }
		[JsonProperty("appRoles")]
		public List<AppRole> AppRoles { get; set; }
		[JsonProperty("availableToOtherTenants")]
		public bool? AvailableToOtherTenants { get; set; }
		[JsonProperty("displayName")]
		public string DisplayName { get; set; }
		[JsonProperty("errorUrl")]
		public string ErrorUrl { get; set; }
		[JsonProperty("groupMembershipClaims")]
		public string GroupMembershipClaims { get; set; }
		[JsonProperty("homepage")]
		public string Homepage { get; set; }
		[JsonProperty("identifierUris")]
		public List<string> IdentifierUris { get; set; }
		[JsonProperty("informationalUrls")]
		public InformationalUrl InformationalUrls { get; set; }
		[JsonProperty("isDeviceOnlyAuthSupported")]
		public bool? IsDeviceOnlyAuthSupported { get; set; }
		[JsonProperty("keyCredentials")]
		public List<KeyCredential> KeyCredentials { get; set; }
		[JsonProperty("knownClientApplications")]
		public List<Guid> KnownClientApplications { get; set; }
		[JsonProperty("logoutUrl")]
		public string LogoutUrl { get; set; }
		[JsonProperty("logo")]
		public object Logo { get; set; }
		[JsonProperty("logoUrl")]
		public string LogoUrl { get; set; }
		[JsonProperty("mainLogo")]
		public object MainLogo { get; set; }
		[JsonProperty("oauth2AllowIdTokenImplicitFlow")]
		public bool Oauth2AllowIdTokenImplicitFlow { get; set; }
		[JsonProperty("oauth2AllowImplicitFlow")]
		public bool Oauth2AllowImplicitFlow { get; set; }
		[JsonProperty("oauth2AllowUrlPathMatching")]
		public bool Oauth2AllowUrlPathMatching { get; set; }
		[JsonProperty("oauth2Permissions")]
		public List<OAuth2Permission> Oauth2Permissions { get; set; }
		[JsonProperty("oauth2RequirePostResponse")]
		public bool Oauth2RequirePostResponse { get; set; }
		[JsonProperty("optionalClaims")]
		public OptionalClaims OptionalClaims { get; set; }
		[JsonProperty("orgRestrictions")]
		public List<Guid> OrgRestrictions { get; set; }
		[JsonProperty("parentalControlSettings")]
		public ParentalControlSettings ParentalControlSettings { get; set; }
		[JsonProperty("passwordCredentials")]
		public List<PasswordCredential> PasswordCredentials { get; set; }
		[JsonProperty("publicClient")]
		public bool? PublicClient { get; set; }
		[JsonProperty("publisherDomain")]
		public string PublisherDomain { get; set; }
		[JsonProperty("recordConsentConditions")]
		public string RecordConsentConditions { get; set; }
		[JsonProperty("replyUrls")]
		public List<string> ReplyUrls { get; set; }
		[JsonProperty("requiredResourceAccess")]
		public List<RequiredResourceAccess> RequiredResourceAccess { get; set; }
		[JsonProperty("samlMetadataUrl")]
		public string SamlMetadataUrl { get; set; }
		[JsonProperty("signInAudience")]
		public string SignInAudience { get; set; }
		[JsonProperty("tokenEncryptionKeyId")]
		public Guid? TokenEncryptionKeyId { get; set; }
	}
	
	public partial class Group : DirectoryObject
	{
		[JsonProperty("description")]
		public string Description { get; set; }
		[JsonProperty("dirSyncEnabled")]
		public bool? DirSyncEnabled { get; set; }
		[JsonProperty("displayName")]
		public string DisplayName { get; set; }
		[JsonProperty("lastDirSyncTime")]
		public DateTimeOffset? LastDirSyncTime { get; set; }
		[JsonProperty("mail")]
		public string Mail { get; set; }
		[JsonProperty("mailNickname")]
		public string MailNickname { get; set; }
		[JsonProperty("mailEnabled")]
		public bool? MailEnabled { get; set; }
		[JsonProperty("onPremisesDomainName")]
		public string OnPremisesDomainName { get; set; }
		[JsonProperty("onPremisesNetBiosName")]
		public string OnPremisesNetBiosName { get; set; }
		[JsonProperty("onPremisesSamAccountName")]
		public string OnPremisesSamAccountName { get; set; }
		[JsonProperty("onPremisesSecurityIdentifier")]
		public string OnPremisesSecurityIdentifier { get; set; }
		[JsonProperty("provisioningErrors")]
		public List<ProvisioningError> ProvisioningErrors { get; set; }
		[JsonProperty("proxyAddresses")]
		public List<string> ProxyAddresses { get; set; }
		[JsonProperty("securityEnabled")]
		public bool? SecurityEnabled { get; set; }
	}
	public partial class AssignedLicense
	{ 
		[JsonProperty("disabledPlans")]
		public List<Guid> DisabledPlans { get; set; }
		[JsonProperty("skuId")]
		public Guid? SkuId { get; set; }
	}

	public partial class AssignedPlan
	{ 
		[JsonProperty("assignedTimestamp")]
		public DateTimeOffset? AssignedTimestamp { get; set; }
		[JsonProperty("capabilityStatus")]
		public string CapabilityStatus { get; set; }
		[JsonProperty("service")]
		public string Service { get; set; }
		[JsonProperty("servicePlanId")]
		public Guid? ServicePlanId { get; set; }
	}

	public partial class PasswordProfile
	{ 
		[JsonProperty("password")]
		public string Password { get; set; }
		[JsonProperty("forceChangePasswordNextLogin")]
		public bool? ForceChangePasswordNextLogin { get; set; }
		[JsonProperty("enforceChangePasswordPolicy")]
		public bool? EnforceChangePasswordPolicy { get; set; }
	}

	public partial class ProvisionedPlan
	{ 
		[JsonProperty("capabilityStatus")]
		public string CapabilityStatus { get; set; }
		[JsonProperty("provisioningStatus")]
		public string ProvisioningStatus { get; set; }
		[JsonProperty("service")]
		public string Service { get; set; }
	}

	public partial class ProvisioningError
	{ 
		[JsonProperty("errorDetail")]
		public string ErrorDetail { get; set; }
		[JsonProperty("resolved")]
		public bool? Resolved { get; set; }
		[JsonProperty("service")]
		public string Service { get; set; }
		[JsonProperty("timestamp")]
		public DateTimeOffset? Timestamp { get; set; }
	}

	public partial class SignInName
	{ 
		[JsonProperty("type")]
		public string Type { get; set; }
		[JsonProperty("value")]
		public string Value { get; set; }
	}

	public partial class UserIdentity
	{ 
		[JsonProperty("issuer")]
		public string Issuer { get; set; }
		[JsonProperty("issuerUserId")]
		public object IssuerUserId { get; set; }
	}

	public partial class AddIn
	{ 
		[JsonProperty("id")]
		public Guid? Id { get; set; }
		[JsonProperty("type")]
		public string Type { get; set; }
		[JsonProperty("properties")]
		public List<KeyValue> Properties { get; set; }
	}

	public partial class AppRole
	{ 
		[JsonProperty("allowedMemberTypes")]
		public List<string> AllowedMemberTypes { get; set; }
		[JsonProperty("description")]
		public string Description { get; set; }
		[JsonProperty("displayName")]
		public string DisplayName { get; set; }
		[JsonProperty("id")]
		public Guid Id { get; set; }
		[JsonProperty("isEnabled")]
		public bool IsEnabled { get; set; }
		[JsonProperty("value")]
		public string Value { get; set; }
	}

	public partial class InformationalUrl
	{ 
		[JsonProperty("termsOfService")]
		public string TermsOfService { get; set; }
		[JsonProperty("support")]
		public string Support { get; set; }
		[JsonProperty("privacy")]
		public string Privacy { get; set; }
		[JsonProperty("marketing")]
		public string Marketing { get; set; }
	}

	public partial class KeyCredential
	{ 
		[JsonProperty("customKeyIdentifier")]
		public object CustomKeyIdentifier { get; set; }
		[JsonProperty("endDate")]
		public DateTimeOffset? EndDate { get; set; }
		[JsonProperty("keyId")]
		public Guid? KeyId { get; set; }
		[JsonProperty("startDate")]
		public DateTimeOffset? StartDate { get; set; }
		[JsonProperty("type")]
		public string Type { get; set; }
		[JsonProperty("usage")]
		public string Usage { get; set; }
		[JsonProperty("value")]
		public object Value { get; set; }
	}

	public partial class OAuth2Permission
	{ 
		[JsonProperty("adminConsentDescription")]
		public string AdminConsentDescription { get; set; }
		[JsonProperty("adminConsentDisplayName")]
		public string AdminConsentDisplayName { get; set; }
		[JsonProperty("id")]
		public Guid Id { get; set; }
		[JsonProperty("isEnabled")]
		public bool IsEnabled { get; set; }
		[JsonProperty("type")]
		public string Type { get; set; }
		[JsonProperty("userConsentDescription")]
		public string UserConsentDescription { get; set; }
		[JsonProperty("userConsentDisplayName")]
		public string UserConsentDisplayName { get; set; }
		[JsonProperty("value")]
		public string Value { get; set; }
	}

	public partial class OptionalClaims
	{ 
		[JsonProperty("idToken")]
		public List<OptionalClaim> IdToken { get; set; }
		[JsonProperty("accessToken")]
		public List<OptionalClaim> AccessToken { get; set; }
		[JsonProperty("saml2Token")]
		public List<OptionalClaim> Saml2Token { get; set; }
	}

	public partial class ParentalControlSettings
	{ 
		[JsonProperty("countriesBlockedForMinors")]
		public List<string> CountriesBlockedForMinors { get; set; }
		[JsonProperty("legalAgeGroupRule")]
		public string LegalAgeGroupRule { get; set; }
	}

	public partial class PasswordCredential
	{ 
		[JsonProperty("customKeyIdentifier")]
		public object CustomKeyIdentifier { get; set; }
		[JsonProperty("endDate")]
		public DateTimeOffset? EndDate { get; set; }
		[JsonProperty("keyId")]
		public Guid? KeyId { get; set; }
		[JsonProperty("startDate")]
		public DateTimeOffset? StartDate { get; set; }
		[JsonProperty("value")]
		public string Value { get; set; }
	}

	public partial class RequiredResourceAccess
	{ 
		[JsonProperty("resourceAppId")]
		public string ResourceAppId { get; set; }
		[JsonProperty("resourceAccess")]
		public List<ResourceAccess> ResourceAccess { get; set; }
	}

	public partial class KeyValue
	{ 
		[JsonProperty("key")]
		public string Key { get; set; }
		[JsonProperty("value")]
		public string Value { get; set; }
	}

	public partial class OptionalClaim
	{ 
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("source")]
		public string Source { get; set; }
		[JsonProperty("essential")]
		public bool Essential { get; set; }
		[JsonProperty("additionalProperties")]
		public List<string> AdditionalProperties { get; set; }
	}

	public partial class ResourceAccess
	{ 
		[JsonProperty("id")]
		public Guid Id { get; set; }
		[JsonProperty("type")]
		public string Type { get; set; }
	}

	public partial class GraphApiClient
	{

		public async Task<GraphList<User>> GetUsersAsync(string query = null, int? top = null, string skipToken = null)
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

		public async Task<List<User>> GetAllUsersAsync(string query = null, int? itemsPerPage = null, IProgress<IList<User>> progress = null) 
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
	
		public async Task<List<User>> GetUsersByObjectIdsAsync(params string[] userObjectIds) 
		{
			var body = new
            {
                objectIds = userObjectIds,
                types = new[] { "user" }
            };

			var result = await ExecuteRequest<ODataResponse<User>>(HttpMethod.Post, "getObjectsByObjectIds", body: body);
			return result.Value;
		}

		public async Task<User> GetUserAsync(string userObjectId)
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

		public async Task<User> CreateUserAsync(User user)
		{
			var result = await ExecuteRequest<User>(HttpMethod.Post, $"users", body: user);
			return result;
		}
		
		public async Task UpdateUserAsync(string userObjectId, object modelChanges)
		{
			var result = await ExecuteRequest(HttpMethodPatch, $"users/{userObjectId}", body: modelChanges);
		}

		public async Task DeleteUserAsync(string userObjectId) 
		{
			var result = await ExecuteRequest(HttpMethod.Delete, $"users/{userObjectId}");
		}

		public async Task<GraphList<Group>> GetGroupsAsync(string query = null, int? top = null, string skipToken = null)
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

		public async Task<List<Group>> GetAllGroupsAsync(string query = null, int? itemsPerPage = null, IProgress<IList<Group>> progress = null) 
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
	
		public async Task<List<Group>> GetGroupsByObjectIdsAsync(params string[] groupObjectIds) 
		{
			var body = new
            {
                objectIds = groupObjectIds,
                types = new[] { "group" }
            };

			var result = await ExecuteRequest<ODataResponse<Group>>(HttpMethod.Post, "getObjectsByObjectIds", body: body);
			return result.Value;
		}

		public async Task<Group> GetGroupAsync(string groupObjectId)
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

		public async Task<Group> CreateGroupAsync(Group group)
		{
			var result = await ExecuteRequest<Group>(HttpMethod.Post, $"groups", body: group);
			return result;
		}
		
		public async Task UpdateGroupAsync(string groupObjectId, object modelChanges)
		{
			var result = await ExecuteRequest(HttpMethodPatch, $"groups/{groupObjectId}", body: modelChanges);
		}

		public async Task DeleteGroupAsync(string groupObjectId) 
		{
			var result = await ExecuteRequest(HttpMethod.Delete, $"groups/{groupObjectId}");
		}

		public async Task<GraphList<Application>> GetApplicationsAsync(string query = null, int? top = null, string skipToken = null)
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

		public async Task<List<Application>> GetAllApplicationsAsync(string query = null, int? itemsPerPage = null, IProgress<IList<Application>> progress = null) 
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
	
		public async Task<List<Application>> GetApplicationsByObjectIdsAsync(params string[] applicationObjectIds) 
		{
			var body = new
            {
                objectIds = applicationObjectIds,
                types = new[] { "application" }
            };

			var result = await ExecuteRequest<ODataResponse<Application>>(HttpMethod.Post, "getObjectsByObjectIds", body: body);
			return result.Value;
		}

		public async Task<Application> GetApplicationAsync(string applicationObjectId)
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

		public async Task<Application> CreateApplicationAsync(Application application)
		{
			var result = await ExecuteRequest<Application>(HttpMethod.Post, $"applications", body: application);
			return result;
		}
		
		public async Task UpdateApplicationAsync(string applicationObjectId, object modelChanges)
		{
			var result = await ExecuteRequest(HttpMethodPatch, $"applications/{applicationObjectId}", body: modelChanges);
		}

		public async Task DeleteApplicationAsync(string applicationObjectId) 
		{
			var result = await ExecuteRequest(HttpMethod.Delete, $"applications/{applicationObjectId}");
		}

	}
}
