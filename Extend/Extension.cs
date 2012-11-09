using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Piranha.Extend
{
	/// <summary>
	/// Class used by the ExtensionManager to store the currently 
	/// available extensions.
	/// </summary>
	public sealed class Extension
	{
		/// <summary>
		/// The extension name.
		/// </summary>
		public string Name { get ; internal set ; }

		/// <summary>
		/// The extension type.
		/// </summary>
		public Type Type { get ; internal set ; }

		/// <summary>
		/// Gets/sets the extension type.
		/// </summary>
		internal ExtensionType ExtensionType { get ; set ; }
	}
}
