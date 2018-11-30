using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GraphLite
{	
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
		[JsonProperty("userState")]
		public string UserState { get; set; }
		[JsonProperty("userStateChangedOn")]
		public DateTimeOffset? UserStateChangedOn { get; set; }
		[JsonProperty("userType")]
		public string UserType { get; set; }
	}
}