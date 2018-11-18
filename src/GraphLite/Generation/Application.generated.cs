using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GraphLite
{	
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
}