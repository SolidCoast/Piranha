using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Piranha.Mvc
{
	/// <summary>
	/// Base view page for all Piranha MVC views.
	/// </summary>
	/// <typeparam name="T">The model type</typeparam>
	public abstract class BaseViewPage<T> : WebViewPage<T>
	{
		/// <summary>
		/// Gets the UI helper.
		/// </summary>
		public PiranhaHelper UI { get ; private set ; }

		/// <summary>
		/// Default constructor. Creates a new view page.
		/// </summary>
		public BaseViewPage() : base() {
			UI = new PiranhaHelper(this) ;
		}
	}
}
