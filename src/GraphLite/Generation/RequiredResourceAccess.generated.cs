using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GraphLite
{
	public partial class RequiredResourceAccess
	{ 
		[JsonProperty("resourceAppId")]
		public string ResourceAppId { get; set; }
		[JsonProperty("resourceAccess")]
		public List<ResourceAccess> ResourceAccess { get; set; }
	}
}