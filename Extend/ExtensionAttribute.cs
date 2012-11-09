using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Piranha.Extend
{
	/// <summary>
	/// Attribute to define the meta data for an extension.
	/// </summary>
	public class ExtensionAttribute : Attribute
	{
		/// <summary>
		/// Gets/sets the name of the extension.
		/// </summary>
		public string Name { get ; set ; }

		/// <summary>
		/// Gets/sets the internal id of the extension.
		/// </summary>
		public string InternalId { get ; set ; }

		/// <summary>
		/// Gets/sets the possible icon path used in the
		/// template preview in the manager interface.
		/// </summary>
		public string IconPath { get ; set ; }

		/// <summary>
		/// Gets/sets what kind of extension this is.
		/// </summary>
		public ExtensionType Type { get ; set ; }
		
		/// <summary>
		/// Default constructor. Creates a new attribute.
		/// </summary>
		public ExtensionAttribute() {
			Type = ExtensionType.NotSet ;
		}
	}
}
