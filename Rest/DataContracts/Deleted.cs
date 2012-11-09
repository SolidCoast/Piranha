using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Piranha.Rest.DataContracts
{
	[DataContract()]
	public class Deleted
	{
		[DataMember()]
		public List<DeletedItem> Pages { get ; set ; }
		[DataMember()]
		public List<DeletedItem> Content { get ; set ; }
		[DataMember()]
		public List<DeletedItem> Categories { get ; set ; }
	}

	[DataContract()]
	public class DeletedItem
	{
		[DataMember()]
		public Guid Id { get ; set ; }
		[DataMember()]
		public string Deleted { get ; set ; }
	}
}
