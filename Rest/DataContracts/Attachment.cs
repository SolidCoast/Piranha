using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Piranha.Rest.DataContracts
{
	[DataContract()]
	public class Attachment
	{
		[DataMember()]
		public Guid Id { get ; set ; }
		[DataMember()]
		public bool IsImage { get ; set ; }
	}
}
