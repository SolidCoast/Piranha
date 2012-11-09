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
	public abstract class SinglePost : SinglePost<PostModel> {}

	/// <summary>
	/// Page class for a single page where the model is of the generic type T.
	/// </summary>
	/// <typeparam name="T">The model type</typeparam>
	public abstract class SinglePost<T> : ContentPage<T> where T : PostModel
	{
		#region Members
		private Piranha.Models.Post post ;
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

			// Load the current post
			if (!String.IsNullOrEmpty(permalink))
				post = Post.GetByPermalink(permalink, draft) ;

			// Cache management
			DateTime mod = GetLastModified() ;
			DateTime tmod = TemplateCache.GetLastModified(!String.IsNullOrEmpty(post.Controller) ?
				post.Controller : "~/post.cshtml") ;
			mod = tmod > mod ? tmod : mod ;
			ClientCache.HandleClientCache(HttpContext.Current, WebPiranha.GetCulturePrefix() + post.Id.ToString(), mod) ;

			// Load the model if the post wasn't cached
			if (!cached)
				InitModel(PostModel.Get<T>(post)) ;

			// Execute hook, if it exists
			if (Hooks.Model.PostModelLoaded != null)
				Hooks.Model.PostModelLoaded(Model) ;

			base.InitializePage() ;
		}

		/// <summary>
		/// Gets the lastest modification date for caching.
		/// </summary>
		/// <returns></returns>
		protected virtual DateTime GetLastModified() {
			if (post == null)
				throw new ArgumentNullException();
			return post.Updated ;
		}

		#region Private methods
		/// <summary>
		/// Initializes the instance from the given model.
		/// </summary>
		/// <param name="pm">The page model</param>
		protected virtual void InitModel(T pm) {
			Model = pm ;

			Page.Current = null ;
			Page.CurrentPost = Model.Post ;
		}
		#endregion
	}
}
