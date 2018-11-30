using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GraphLite
{
	public partial class AppRole
	{ 
		[JsonProperty("allowedMemberTypes")]
		public List<string> AllowedMemberTypes { get; set; }
		[JsonProperty("description")]
		public string Description { get; set; }
		[JsonProperty("displayName")]
		public string DisplayName { get; set; }
		[JsonProperty("id")]
		public Guid Id { get; set; }
		[JsonProperty("isEnabled")]
		public bool IsEnabled { get; set; }
		[JsonProperty("value")]
		public string Value { get; set; }
	}
}