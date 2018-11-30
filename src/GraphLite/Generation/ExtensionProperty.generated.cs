using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GraphLite
{	
	public partial class ExtensionProperty : DirectoryObject
	{
		[JsonProperty("appDisplayName")]
		public string AppDisplayName { get; set; }
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("dataType")]
		public string DataType { get; set; }
		[JsonProperty("isSyncedFromOnPremises")]
		public bool? IsSyncedFromOnPremises { get; set; }
		[JsonProperty("targetObjects")]
		public List<string> TargetObjects { get; set; }
	}
}