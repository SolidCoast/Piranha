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
	/// Standard page class for an archive.
	/// </summary>
	public abstract class Archive : Archive<ArchiveModel> {}

	/// <summary>
	/// Page class for an archive where the model is of the generic type T.
	/// </summary>
	/// <typeparam name="T">The model type</typeparam>
	public abstract class Archive<T> : ContentPage<T> where T : ArchiveModel {
		/// <summary>
		/// Initializes the web page
		/// </summary>
		protected override void InitializePage() {
			string permalink = UrlData.Count > 0 ? UrlData[0] : "" ;


			// Load the current page model
			if (!String.IsNullOrEmpty(permalink))
				InitModel(ArchiveModel.GetByCategoryName<T>(permalink)) ;
			else InitModel(ArchiveModel.Get<T>()) ;

			// Cache management
			DateTime mod = GetLastModified() ;
			ClientCache.HandleClientCache(HttpContext.Current, Model.Category != null ? 
				Model.Category.Id.ToString() : Guid.Empty.ToString(), mod) ;

			base.InitializePage() ;
		}

		/// <summary>
		/// Gets the lastest modification date for caching.
		/// </summary>
		/// <returns></returns>
		protected virtual DateTime GetLastModified() {
			DateTime mod = DateTime.MinValue ;

			Model.Archive.ForEach(p => {
				if (p.Updated > mod)
					mod = p.Updated ;
			});
			Model.Content.ForEach(c => {
				if (c.Updated > mod)
					mod = c.Updated ;
			});
			return mod ;
		}

		#region Private methods
		/// <summary>
		/// Initializes the instance from the given model.
		/// </summary>
		/// <param name="pm">The page model</param>
		protected virtual void InitModel(T pm) {
			Model = pm ;

			Page.Current = null ;
		}
		#endregion
	}
}
