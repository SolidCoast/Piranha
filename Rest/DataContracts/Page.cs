using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Piranha.Rest.DataContracts
{
	[DataContract()]
	public class Page
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
		public List<Region> Regions { get ; set ; }
		[DataMember()]
		public List<Property> Properties { get ; set ; }
		[DataMember()]
		public List<Attachment> Attachments { get ; set ; }
		[DataMember()]
		public List<Extension> Extensions { get ; set ; }
		[DataMember()]
		public string Created { get ; set ; }
		[DataMember()]
		public string Updated { get ; set ; }
		[DataMember()]
		public string Published { get ; set ; }
		[DataMember()]
		public string LastPublished { get ; set ; }

		/// <summary>
		/// Default constructor.
		/// </summary>
		public Page() {
			Regions = new List<Region>() ;
			Properties = new List<Property>() ;
			Attachments = new List<Attachment>() ;
			Extensions = new List<Extension>() ;
		}
	}
}
