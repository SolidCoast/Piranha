using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Piranha.Models;

namespace Piranha.WebPages.RequestHandlers
{
	/// <summary>
	/// Generates the sitemap.xml from all published pages & posts.
	/// </summary>
	public class SitemapHandler : IRequestHandler
	{
		#region Members
		private const string xmlStart = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">\r\n" ;
		private const string xmlEnd = "</urlset>\r\n" ;
		private const string xmlUrl = "\t<url>\n\t\t<loc>{0}</loc>\n\t\t<lastmod>{1}</lastmod>\n\t</url>\n" ;
		#endregion

		/// <summary>
		/// Handles the sitemap request.
		/// </summary>
		/// <param name="context">The current http context</param>
		/// <param name="args">Optional url params</param>
		public virtual void HandleRequest(HttpContext context, params string[] args) {
			var sitemap = Sitemap.GetStructure() ;
			var posts = Post.GetFields("post_last_published, permalink_name", "post_draft = 0") ;

			context.Response.StatusCode = 200 ;
			context.Response.ContentType = "text/xml" ;
			context.Response.Write(xmlStart) ;
			WriteNodes(context, sitemap) ;
			WritePosts(context, posts) ;
			context.Response.Write(xmlEnd) ;
			context.Response.End() ;
		}

		#region Private methods
		/// <summary>
		/// Writes the sitemap to the sitemap.
		/// </summary>
		/// <param name="context">The http context</param>
		/// <param name="nodes">The sitemap nodes</param>
		private void WriteNodes(HttpContext context, List<Sitemap> nodes) {
			foreach (var node in nodes) {
				if (String.IsNullOrEmpty(node.Redirect)) {
					context.Response.Write(String.Format(xmlUrl, 
						GetPermalink(node.Permalink), node.LastPublished.ToString("yyyy-MM-dd"))) ;
				}
				if (node.Pages.Count > 0)
					WriteNodes(context, node.Pages) ;
			}
		}

		/// <summary>
		/// Writes all posts to the sitemap.
		/// </summary>
		/// <param name="context">The http context</param>
		/// <param name="posts">The posts</param>
		private void WritePosts(HttpContext context, List<Post> posts) {
			foreach (var post in posts) {
				context.Response.Write(String.Format(xmlUrl, 
					GetPermalink(post.Permalink), post.LastPublished.ToString("yyyy-MM-dd"))) ;
			}
		}

		/// <summary>
		/// Generates the correct url.
		/// </summary>
		/// <param name="permalink">The permalink name</param>
		/// <returns>The url</returns>
		private string GetPermalink(string permalink) {
			return WebPiranha.GetSiteUrl() + "/" + (!WebPiranha.PrefixlessPermalinks ? WebPiranha.GetUrlPrefixForHandlerId("PERMALINK") + "/" : "") + permalink ;
		}
		#endregion
	}
}