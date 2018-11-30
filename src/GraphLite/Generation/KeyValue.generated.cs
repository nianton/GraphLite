using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GraphLite
{
	public partial class KeyValue
	{ 
		[JsonProperty("key")]
		public string Key { get; set; }
		[JsonProperty("value")]
		public string Value { get; set; }
	}
}