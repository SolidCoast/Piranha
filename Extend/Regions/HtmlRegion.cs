using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Piranha.Extend.Regions
{
	/// <summary>
	/// Standard html region.
	/// </summary>
	[Extension(Name="Html", Type=ExtensionType.Region)]
	public class HtmlRegion : HtmlString, IExtension
	{
		/// <summary>
		/// Creates an empty html region.
		/// </summary>
		public HtmlRegion() : base("") {}

		/// <summary>
		/// Creates an html region from the given string.
		/// </summary>
		/// <param name="str">The string</param>
		public HtmlRegion(string str) : base(str) {}
	}
}
