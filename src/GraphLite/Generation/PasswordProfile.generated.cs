using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GraphLite
{
	public partial class PasswordProfile
	{ 
		[JsonProperty("password")]
		public string Password { get; set; }
		[JsonProperty("forceChangePasswordNextLogin")]
		public bool? ForceChangePasswordNextLogin { get; set; }
		[JsonProperty("enforceChangePasswordPolicy")]
		public bool? EnforceChangePasswordPolicy { get; set; }
	}
}