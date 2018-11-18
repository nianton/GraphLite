using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GraphLite
{	
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
}