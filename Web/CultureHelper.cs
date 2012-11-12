using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Configuration;

namespace Piranha.Web
{
	/// <summary>
	/// Culture helper class.
	/// </summary>
	public class CultureHelper {
		/// <summary>
		/// Gets the name of the current UI culture.
		/// </summary>
		/// <returns>The current UI culture</returns>
		public static string CurrentUiCulture {
			get {
				return CultureInfo.CurrentUICulture.Name ;
			}
		}

		/// <summary>
		/// Gets the default ui culture as specified in the current web.config
		/// </summary>
		public static string DefaultUiCulture {
			get {
				var gs = (GlobalizationSection)WebConfigurationManager.GetSection("system.web/globalization") ;
				return gs.UICulture ;
			}
		}
	}
}
