using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

using Piranha.Models;

namespace Piranha.Mvc
{
	/// <summary>
	/// View helper.
	/// </summary>
	public class PiranhaHelper : Web.UiHelper
	{
		#region Members
		private WebViewPage Parent ;
		private SinglePageController Controller ;
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
			get { return (Page)Parent.ViewBag.Page ; }
		}

		/// <summary>
		/// Gets the current post.
		/// </summary>
		protected override Post CurrentPost {
			get { return (Post)Parent.ViewBag.Post ; }
		}
		#endregion

		/// <summary>
		/// Default constructor. Creates a new helper.
		/// </summary>
		/// <param name="parent">The parent view.</param>
		public PiranhaHelper(WebViewPage parent) {
			Parent = parent ;
		}

		/// <summary>
		/// Default constructor. Creates a new helper.
		/// </summary>
		/// <param name="parent">The parent controller.</param>
		public PiranhaHelper(SinglePageController controller) {
			Controller = controller ;
		}
		
		/// <summary>
		/// Gets the permalink action URL for the MVC controller.
		/// </summary>
		/// <param name="action">The action to execute</param>
		/// <returns>The url</returns>
		public override IHtmlString FormAction(string action) {
			Page page = Parent != null ? Parent.Page.Current : Controller.page ;

			if (page != null)
				return new HtmlString(Permalink(page.Permalink) + "/" + action) ;
			return new HtmlString(action) ; 
		}

		/// <summary>
		/// Converts the virtual path to an application url.
		/// </summary>
		/// <param name="virtualpath">The virtual path</param>
		/// <returns>An applicaiton url</returns>
		protected override string Url(string virtualpath) {
			return Parent != null ? Parent.Url.Content(virtualpath) : Controller.Url.Content(virtualpath) ;
		}

		public string Action(string actionName) {
			var routedata = Parent != null ? Parent.Request.RequestContext.RouteData : Controller.Request.RequestContext.RouteData ;

			string area = (string)routedata.Values["area"] ;
			string controller = (string)routedata.Values["controller"] ;

			var page = GetPageByRoute(area, controller, actionName) ;
			if (page != null)
				return Permalink(page.Permalink).ToString() ;
			return "" ;
		}

		public string Action(string actionName, string controllerName) {
			var routedata = Parent != null ? Parent.Request.RequestContext.RouteData : Controller.Request.RequestContext.RouteData ;

			string area = (string)routedata.Values["area"] ;

			var page = GetPageByRoute(area, controllerName, actionName) ;
			if (page != null)
				return Permalink(page.Permalink).ToString() ;
			return "" ;
		}

		#region Private methods
		/// <summary>
		/// Gets the permalink from it's view template
		/// </summary>
		/// <param name="area"></param>
		/// <param name="controller"></param>
		/// <param name="action"></param>
		/// <returns></returns>
		private Page GetPageByRoute(string area, string controller, string action) {
			Page page = null ;

			page = Page.GetSingle("page_controller = @0", "~/" + (!String.IsNullOrEmpty(area) ? area + "/" : "") + controller + "/" + action) ;
			if (page != null)
				return page ;
			page = Page.GetSingle("page_controller = @0", "~/" + (!String.IsNullOrEmpty(area) ? area + "/" : "") + controller) ;

			return page ;
		}
		#endregion
	}
}
