using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GraphLite
{
	public partial class ProvisionedPlan
	{ 
		[JsonProperty("capabilityStatus")]
		public string CapabilityStatus { get; set; }
		[JsonProperty("provisioningStatus")]
		public string ProvisioningStatus { get; set; }
		[JsonProperty("service")]
		public string Service { get; set; }
	}
}