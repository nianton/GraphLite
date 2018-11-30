using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GraphLite
{
	public partial class UserIdentity
	{ 
		[JsonProperty("issuer")]
		public string Issuer { get; set; }
		[JsonProperty("issuerUserId")]
		public object IssuerUserId { get; set; }
	}
}