using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GraphLite
{	
	public partial class DirectoryObject
	{
		[JsonProperty("objectType")]
		public string ObjectType { get; set; }
		[JsonProperty("objectId")]
		public string ObjectId { get; set; }
		[JsonProperty("deletionTimestamp")]
		public DateTimeOffset? DeletionTimestamp { get; set; }
	}
}