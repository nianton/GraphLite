using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GraphLite
{
	public partial class SignInName
	{ 
		[JsonProperty("type")]
		public string Type { get; set; }
		[JsonProperty("value")]
		public string Value { get; set; }
	}
}