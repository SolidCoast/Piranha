using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.WebPages;
using System.Web.WebPages.Html;

using Piranha.Models;

namespace Piranha.WebPages
{
	/// <summary>
	/// View helper class.
	/// </summary>
	public class PiranhaHelper : Web.UiHelper
	{
		#region Members
		private HtmlHelper Html ;
		private WebPage Parent ;
		#endregion

		#region Properties
		/// <summary>
		/// Gets the current http context.
		/// </summary>
		protected override HttpContextBase Context {
			get { return Parent.Context ; }
		}

		/// <summary>
		/// Gets the current page.
		/// </summary>
		protected override Page CurrentPage {
			get { return (Page)Parent.Page.Current ; }
		}

		/// <summary>
		/// Gets the current post.
		/// </summary>
		protected override Post CurrentPost {
			get { return (Post)Parent.Page.CurrentPost ; }
		}
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="html"></param>
		public PiranhaHelper(WebPage parent, HtmlHelper html) {
			Parent = parent ;
			Html = html ;
		}

		/// <summary>
		/// Converts the virtual path to an application url.
		/// </summary>
		/// <param name="virtualpath">The virtual path</param>
		/// <returns>An applicaiton url</returns>
		protected override string Url(string virtualpath) {
			return Parent.Href(virtualpath) ;
		}
	}
}
