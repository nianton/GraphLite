using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GraphLite
{
	public partial class ParentalControlSettings
	{ 
		[JsonProperty("countriesBlockedForMinors")]
		public List<string> CountriesBlockedForMinors { get; set; }
		[JsonProperty("legalAgeGroupRule")]
		public string LegalAgeGroupRule { get; set; }
	}
}