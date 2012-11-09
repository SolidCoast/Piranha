using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Piranha.Rest.DataContracts
{
	[DataContract()]
	public class Content
	{
		[DataMember()]
		public Guid Id { get ; set ; }
		[DataMember()]
		public Guid ParentId { get ; set ; }
		[DataMember()]
		public string Filename { get ; set ; }
		[DataMember()]
		public string Name { get ; set ; }
		[DataMember()]
		public string DisplayName { get ; set ; }
		[DataMember()]
		public string Description { get ; set ; }
		[DataMember()]
		public string Type { get ; set ; }
		[DataMember()]
		public int Size { get ; set ; }
		[DataMember()]
		public string ThumbnailUrl { get ; set ; }
		[DataMember()]
		public string ContentUrl { get ; set ; }
		[DataMember()]
		public string Created { get ; set ; }
		[DataMember()]
		public string Updated { get ; set ; }
	}
}
