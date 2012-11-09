using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Piranha.Models;

namespace Piranha.Web
{
	/// <summary>
	/// The site helper exports basic information to the views.
	/// </summary>
	public class SiteHelper
	{
		/// <summary>
		/// Gets the site title.
		/// </summary>
		public string SiteTitle {
			get {
				var p = SysParam.GetByName("SITE_TITLE") ;
				if (p != null)
					return p.Value ;
				return "" ;
			}
		}

		/// <summary>
		/// GEts the site description.
		/// </summary>
		public string SiteDescription {
			get {
				var p = SysParam.GetByName("SITE_DESCRIPTION") ;
				if (p != null)
					return p.Value ;
				return "" ;
			}
		}
	}
}