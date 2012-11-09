using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Piranha.Mvc
{
	/// <summary>
	/// Custom view engine that adds the Extensions path for partial views.
	/// </summary>
	public class ManagerViewEngine : RazorViewEngine
	{
		#region Members
		private static string[] ExtensionsFolder = new[] { "~/Areas/Manager/Views/Extensions/{0}.cshtml" };
		#endregion

		/// <summary>
		/// Create the view engine.
		/// </summary>
		public ManagerViewEngine () {
			base.PartialViewLocationFormats = base.PartialViewLocationFormats.Union(ExtensionsFolder).ToArray();
		}
	}
}