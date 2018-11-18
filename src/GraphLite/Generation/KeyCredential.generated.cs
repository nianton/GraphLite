using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GraphLite
{
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
}