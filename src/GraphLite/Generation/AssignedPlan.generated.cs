using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GraphLite
{
	public partial class AssignedPlan
	{ 
		[JsonProperty("assignedTimestamp")]
		public DateTimeOffset? AssignedTimestamp { get; set; }
		[JsonProperty("capabilityStatus")]
		public string CapabilityStatus { get; set; }
		[JsonProperty("service")]
		public string Service { get; set; }
		[JsonProperty("servicePlanId")]
		public Guid? ServicePlanId { get; set; }
	}
}