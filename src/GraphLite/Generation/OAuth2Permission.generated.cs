using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GraphLite
{
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
}