using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;

using Piranha.Extend;

namespace Piranha.Models
{
	/// <summary>
	/// Page model for the from cms application.
	/// </summary>
	public class PageModel
	{
		#region Properties
		/// <summary>
		/// Gets the page.
		/// </summary>
		public IPage Page { get ; set ; }

		/// <summary>
		/// Gets the available html regions for the page.
		/// </summary>
		public dynamic Regions { get ; set ; }

		/// <summary>
		/// Gets the available properties.
		/// </summary>
		public dynamic Properties { get ; set ; }

		/// <summary>
		/// Gets the available extensions.
		/// </summary>
		public dynamic Extensions { get ; set ; }

		/// <summary>
		/// Gets the available attachments.
		/// </summary>
		public List<Content> Attachments { get ; set ; }
		#endregion

		/// <summary>
		/// Default constructor. Creates a new empty page model.
		/// </summary>
		public PageModel() {
			Regions       = new ExpandoObject() ;
			Properties    = new ExpandoObject() ;
			Extensions    = new ExpandoObject() ;
			Attachments   = new List<Content>() ;
		}

		#region Static accessors
		/// <summary>
		/// Gets the page model for the given page.
		/// </summary>
		/// <param name="p">The page record</param>
		/// <returns>The model</returns>
		public static PageModel Get(Page p) {
			return Get<PageModel>(p) ;
		}

		/// <summary>
		/// Gets the page model for the given page.
		/// </summary>
		/// <param name="p">The page record</param>
		/// <returns>The model</returns>
		public static T Get<T>(Page p) where T : PageModel {
			T m = Activator.CreateInstance<T>() ;

			m.Page = p ;
			m.Init() ;
			return m ;
		}

		/// <summary>
		/// Gets the page model for the startpage.
		/// </summary>
		/// <returns>The model</returns>
		public static PageModel GetByStartpage() {
			return GetByStartpage<PageModel>() ;
		}

		/// <summary>
		/// Gets tne page model for the startpage.
		/// </summary>
		/// <typeparam name="T">The model type</typeparam>
		/// <returns>The model</returns>
		public static T GetByStartpage<T>() where T : PageModel {
			T m = Activator.CreateInstance<T>() ;

			m.Page = Models.Page.GetStartpage() ;
			m.Init() ;
			return m ;
		}

		/// <summary>
		/// Gets the page model for the specified permalink.
		/// </summary>
		/// <param name="permalink">The permalink</param>
		/// <returns>The model</returns>
		public static PageModel GetByPermalink(string permalink) {
			return GetByPermalink<PageModel>(permalink) ;
		}

		/// <summary>
		/// Gets the page model for the specified permalink.
		/// </summary>
		/// <typeparam name="T">The model type</typeparam>
		/// <param name="permalink">The permalink</param>
		/// <returns>The model</returns>
		public static T GetByPermalink<T>(string permalink) where T : PageModel {
			T m = Activator.CreateInstance<T>() ;

			m.Page = Models.Page.GetByPermalink(permalink) ;
			m.Init() ;
			return m ;
		}
		
		/// <summary>
		/// Gets the page model for the specified page id.
		/// </summary>
		/// <param name="id">The page id</param>
		/// <returns>The model</returns>
		public static PageModel GetById(Guid id) {
			PageModel m = new PageModel() {
				Page = Models.Page.GetSingle(id)
			} ;
			m.Init() ;
			return m ;
		}

		/// <summary>
		/// Gets the page model for the current route. This method is only for MVC use.
		/// </summary>
		/// <typeparam name="T">The page model type</typeparam>
		/// <param name="route">Optional route. Overrides RouteData if provided</param>
		/// <returns>The model</returns>
		public static T GetByRoute<T>(string route = "") where T : PageModel {
			RouteData rd = RouteTable.Routes.GetRouteData(new HttpContextWrapper(HttpContext.Current)) ;

			string controller = (string)rd.Values["controller"] ;
			string action     = (string)rd.Values["action"] ;

			if (route == "")
				route = controller + (action.ToLower() != "index" ? "/" + action : "") ;

			if (controller.ToLower() != "home") {
				T m = Activator.CreateInstance<T>() ;
				m.Page = Models.Page.GetSingle("page_controller = @0 OR (page_controller is NULL AND pagetemplate_controller = @0)", route) ;

				if (m.Page.GroupId != Guid.Empty) {
					if (!HttpContext.Current.User.Identity.IsAuthenticated || !HttpContext.Current.User.IsMember(m.Page.GroupId))
						throw new UnauthorizedAccessException("The current user doesn't have access to the requested page.") ;
				}

				m.Init() ;
				return m ;
			}
			throw new InvalidOperationException("GetByRoute() is only applicable for custom controllers.") ;
		}
		#endregion

		#region Helper methods
		/// <summary>
		/// Gets the region with the given internal id.
		/// </summary>
		/// <typeparam name="T">The target type</typeparam>
		/// <param name="internalId">The internal id</param>
		/// <returns>The region</returns>
		public T Region<T>(string internalId) {
			if (((IDictionary<string,object>)Regions).ContainsKey(internalId))
				return (T)((Dictionary<string,object>)Regions)[internalId] ;
			return default(T) ;
		}

		/// <summary>
		/// Gets the extension with the given internal id.
		/// </summary>
		/// <typeparam name="T">The target type</typeparam>
		/// <param name="internalId">The internal id</param>
		/// <returns>The extension</returns>
		public T Extension<T>(string internalId) {
			if (((IDictionary<string,object>)Extensions).ContainsKey(internalId))
				return (T)((Dictionary<string,object>)Extensions)[internalId] ;
			return default(T) ;
		}
		#endregion

		/// <summary>
		/// Gets the associated regions for the current page
		/// </summary>
		protected void Init() {
			PageTemplate pt = PageTemplate.GetSingle(((Page)Page).TemplateId) ;

			// Regions
			var regions = RegionTemplate.GetByTemplateId(pt.Id) ;
			if (regions.Count > 0) {
				foreach (var rt in regions)
					((IDictionary<string, object>)Regions).Add(rt.InternalId,
						Activator.CreateInstance(ExtensionManager.ExtensionTypes[rt.Type])) ;
				Piranha.Models.Region.GetContentByPageId(Page.Id, Page.IsDraft).ForEach(reg => {
					if (((IDictionary<string, object>)Regions).ContainsKey(reg.InternalId))
						((IDictionary<string, object>)Regions)[reg.InternalId] = reg.Body ;
				}) ;
			}
			// Properties
			if (pt.Properties.Count > 0) {
				foreach (string str in pt.Properties)
					((IDictionary<string, object>)Properties).Add(str, "") ;
				Property.GetContentByParentId(Page.Id, Page.IsDraft).ForEach(pr => {
					if (((IDictionary<string, object>)Properties).ContainsKey(pr.Name))
						((IDictionary<string, object>)Properties)[pr.Name] = pr.Value ;
				});
			}
			// Attachments
			foreach (var guid in ((Models.Page)Page).Attachments) {
				var a = Models.Content.GetSingle(guid) ;
				if (a != null)
					Attachments.Add(a) ;
			}
			// Extensions
			foreach (var ext in ((Page)Page).GetExtensions()) {
				((IDictionary<string, object>)Extensions)[ExtensionManager.GetInternalIdByType(ext.Type)] = ext.Body ;
			}
		}
	}
}
