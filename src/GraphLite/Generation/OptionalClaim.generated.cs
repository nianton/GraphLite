using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GraphLite
{
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
}