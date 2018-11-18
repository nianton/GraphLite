using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GraphLite
{
	public partial class OptionalClaims
	{ 
		[JsonProperty("idToken")]
		public List<OptionalClaim> IdToken { get; set; }
		[JsonProperty("accessToken")]
		public List<OptionalClaim> AccessToken { get; set; }
		[JsonProperty("saml2Token")]
		public List<OptionalClaim> Saml2Token { get; set; }
	}
}