using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Piranha.Extend.Regions
{
	/// <summary>
	/// Standard image region.
	/// </summary>
	[Extension(Name="Image", Type=ExtensionType.Region)]
	public class ImageRegion : IExtension
	{
		/// <summary>
		/// Gets/sets the id of the image.
		/// </summary>
		public Guid Id { get ; set ; }

		/// <summary>
		/// Gets/sets the image title.
		/// </summary>
		public string Name { get ; set ; }

		/// <summary>
		/// Gets/sets the decriptionof the image.
		/// </summary>
		public string Description { get ; set ; }
	}
}