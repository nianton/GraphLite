using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GraphLite
{
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
}