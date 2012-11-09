using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Piranha.Web
{
	/// <summary>
	/// The template cache keeps track on the last modification date of the
	/// actual templates used to render the page.
	/// </summary>
	internal static class TemplateCache
	{
		#region Members
		private static Dictionary<string, DateTime> Templates = new Dictionary<string,DateTime>() ;
		#endregion

		/// <summary>
		/// Gets the last modification date for the current template.
		/// </summary>
		/// <param name="template">The template.</param>
		/// <returns>The modification date, DateTime.MinValue if the physical file could not be resolved.</returns>
		public static DateTime GetLastModified(string template) {
			try {
				if (!Templates.ContainsKey(template)) {
					var path = "" ;

					if (!template.StartsWith("~/"))
						path = "~/templates/" + template + ".cshtml" ;
					else path = template ;

					if (File.Exists(HttpContext.Current.Server.MapPath(path)))
						Templates[template] = File.GetLastWriteTime(HttpContext.Current.Server.MapPath(path)) ;
					else Templates[template] = DateTime.MinValue ;
				}
				return (DateTime)Templates[template] ;
			} catch {}
			return DateTime.MinValue ;
		}

		/// <summary>
		/// Clears the current template cache.
		/// </summary>
		public static void Clear() {
			Templates.Clear() ;
		}
	}
}
