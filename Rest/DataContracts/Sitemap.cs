using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Piranha.Rest.DataContracts
{
	[DataContract()]
	public class Sitemap
	{
		[DataMember()]
		public Guid Id { get ; set ; }
		[DataMember()]
		public string Title { get ; set ; }
		[DataMember()]
		public string Permalink { get ; set ; }
		[DataMember()]
		public bool IsHidden { get ; set ; }
		[DataMember()]
		public bool HasChildren { get ; set ; }
		[DataMember()]
		public List<Sitemap> ChildNodes { get ; set ; }
		[DataMember()]
		public string LastPublished { get ; set ; }
	}
}
