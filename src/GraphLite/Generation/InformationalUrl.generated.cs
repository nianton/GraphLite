using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GraphLite
{
	public partial class InformationalUrl
	{ 
		[JsonProperty("termsOfService")]
		public string TermsOfService { get; set; }
		[JsonProperty("support")]
		public string Support { get; set; }
		[JsonProperty("privacy")]
		public string Privacy { get; set; }
		[JsonProperty("marketing")]
		public string Marketing { get; set; }
	}
}