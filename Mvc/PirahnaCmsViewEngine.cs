using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Piranha.Mvc
{
    using System.Reflection;

    using RazorGenerator.Mvc;

    /// <summary>
	/// Custom view engine that adds the Extensions path for partial views.
	/// </summary>
    public class PirahnaCmsViewEngine : PrecompiledMvcEngine
	{
		#region Members
        public static readonly string[] ExtensionsFolder = new[] { "~/Areas/Manager/Views/Extensions/{0}.cshtml" };
		#endregion

		/// <summary>
		/// Create the view engine.
		/// </summary>
        public PirahnaCmsViewEngine(Assembly assembly)
            : base(assembly)
		{
            base.PartialViewLocationFormats = base.PartialViewLocationFormats.Union(ExtensionsFolder).ToArray();
		}
	}
}