using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GraphLite
{
	public partial class AssignedLicense
	{ 
		[JsonProperty("disabledPlans")]
		public List<Guid> DisabledPlans { get; set; }
		[JsonProperty("skuId")]
		public Guid? SkuId { get; set; }
	}
}