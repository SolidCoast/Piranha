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
		public string CurrentUICulture {
			get {
				return CultureInfo.CurrentUICulture.Name ;
			}
		}

		/// <summary>
		/// Gets the default ui culture as specified in the current web.config
		/// </summary>
		public string DefaultUICulture {
			get {
				GlobalizationSection gs = (GlobalizationSection)WebConfigurationManager.GetSection("system.web/globalization") ;
				return gs.UICulture ;
			}
		}

		/// <summary>
		/// Gets weather the current ui culture is the default culture.
		/// </summary>
		public bool IsDefaultCulture {
			get {
				return CurrentUICulture == DefaultUICulture ;
			}
		}
	}
}
