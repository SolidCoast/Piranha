using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

using Piranha.Models;
using Piranha.Web;

namespace Piranha.WebPages
{
	/// <summary>
	/// Standard page class for a single page.
	/// </summary>
	public abstract class SinglePage : SinglePage<PageModel> {}

	/// <summary>
	/// Page class for a single page where the model is of the generic type T.
	/// </summary>
	/// <typeparam name="T">The model type</typeparam>
	public abstract class SinglePage<T> : ContentPage<T> where T : PageModel
	{
		#region Members
		private Models.Page page = null ;
		#endregion

		/// <summary>
		/// Initializes the web page
		/// </summary>
		protected override void InitializePage() {
			string permalink = UrlData.Count > 0 ? UrlData[0] : "" ;
			bool   draft = false ;
			bool   cached = false ;

			// Check if we want to see the draft
			if (User.HasAccess("ADMIN_PAGE")) {
				if (!String.IsNullOrEmpty(Request["draft"])) {
					try {
						draft = Convert.ToBoolean(Request["draft"]) ;
					} catch {}
				}
			}

            int segments = 0;
			// Accept permalinks with '/' in them
			for (int n = 0; n < UrlData.Count; n++) {
				var perm = Permalink.GetByName(new Guid("8FF4A4B4-9B6C-4176-AAA2-DB031D75AC03"), UrlData.ToArray().Subset(0, UrlData.Count - n).Implode("/")) ;
                segments = UrlData.Count - n;
				if (perm != null) {
					permalink = perm.Name ;
					break ;
				}
			}

			// Load the current page
			if (!String.IsNullOrEmpty(permalink))
				page = Models.Page.GetByPermalink(permalink, draft) ;
			else page = Models.Page.GetStartpage(draft) ;

			// Check permissions
			if (page.GroupId != Guid.Empty) {
				if (!User.IsMember(page.GroupId)) {
					SysParam param = SysParam.GetByName("LOGIN_PAGE") ;
					if (param != null)
						Response.Redirect(param.Value) ;
					else Response.Redirect("~/") ;
				}
				Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache) ;
			} else {
				// Only cache public pages
				DateTime mod = GetLastModified(page) ;
				DateTime tmod = TemplateCache.GetLastModified(!String.IsNullOrEmpty(page.Controller) ?
					page.Controller : "~/page.cshtml") ;
				mod = tmod > mod ? tmod : mod ;
				cached = ClientCache.HandleClientCache(HttpContext.Current, WebPiranha.GetCulturePrefix() + page.Id.ToString(), mod) ;
			}
			// Check for disabled groups
			if (page.DisabledGroups.Contains(User.GetProfile().GroupId)) {
				SysParam param = SysParam.GetByName("LOGIN_PAGE") ;
				if (param != null)
					Response.Redirect(param.Value) ;
				else Response.Redirect("~/") ;
			}
			// Load the model if the page wasn't cached
			if (!cached)
				InitModel(PageModel.Get<T>(page)) ;

			// Execute hook, if it exists
			if (Hooks.Model.PageModelLoaded != null)
				Hooks.Model.PageModelLoaded(Model) ;

			base.InitializePage() ;
		}

		/// <summary>
		/// Gets the lastest modification date for caching.
		/// </summary>
		/// <returns></returns>
		protected virtual DateTime GetLastModified(Models.Page page) {
			return page.Updated ;
		}

		#region Private methods
		/// <summary>
		/// Initializes the instance from the given model.
		/// </summary>
		/// <param name="pm">The page model</param>
		protected virtual void InitModel(T pm) {
			Model = pm ;

			Page.Current = Model.Page ;
			Page.CurrentPost = null ;
		}
		#endregion
	}
}
