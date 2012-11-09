using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

using Piranha.Models;

namespace Piranha.Web
{
	/// <summary>
	/// HtmlHelper extensions for the Piranha application.
	/// </summary>
	public static class PiranhaHelper
	{
		/// <summary>
		/// Url helper for generating the url for the given permalink.
		/// </summary>
		/// <param name="helper">The url helper</param>
		/// <param name="permalink">The permalink</param>
		/// <param name="draft">Weather to generate a link to the draft</param>
		/// <returns>An action url</returns>
		public static string GetPermalink(this UrlHelper helper, string permalink, bool draft = false) {
			if (draft)
				return helper.Content("~/" + WebPages.WebPiranha.GetUrlPrefixForHandlerId("DRAFT") + "/" + permalink.ToLower()) ;
			return helper.Content("~/" + (!WebPages.WebPiranha.PrefixlessPermalinks ? 
				WebPages.WebPiranha.GetUrlPrefixForHandlerId("PERMALINK").ToLower() + "/" : "") + permalink.ToLower()) ;
		}

		/// <summary>
		/// Url helper for generating the preview url for the given page id.
		/// </summary>
		/// <param name="helper">The url helper</param>
		/// <param name="id">The page id</param>
		/// <returns></returns>
		public static string GetPreviewlink(this UrlHelper helper, Guid id) {
			return helper.Content("~/" + WebPages.WebPiranha.GetUrlPrefixForHandlerId("PREVIEW") + "/" + id.ToString()) ;
		}

		/// <summary>
		/// Generates an image tag for the specified thumbnail.
		/// </summary>
		/// <param name="id">The content id</param>
		/// <param name="size">Optional size</param>
		/// <returns>The url</returns>
		public static string GetThumbnailUrl(this UrlHelper helper, Guid id, int size = 0) {
			Content cnt = Models.Content.GetSingle(id) ;
			
			if (cnt != null)
				return helper.Content("~/" + 
					WebPages.WebPiranha.GetUrlPrefixForHandlerId("THUMBNAIL") + "/" + 
					id.ToString() + (size > 0 ? "/" + size.ToString() : "")) ;
			return "" ;
		}

		/// <summary>
		/// Generats the base url for all thumbnails.
		/// </summary>
		/// <param name="helper">The url helper</param>
		/// <returns>The base url</returns>
		public static string GetThumbnailBaseUrl(this UrlHelper helper) {
			return helper.Content("~/" + WebPages.WebPiranha.GetUrlPrefixForHandlerId("THUMBNAIL")) ;
		}
	}
}
