using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Web;

using Piranha.Models;

namespace Piranha.Web
{
	/// <summary>
	/// Helper for generating rss feeds.
	/// </summary>
	public static class RssHelper
	{
		/// <summary>
		/// Outputs the rss feed on the given http context
		/// </summary>
		/// <param name="context">The current context</param>
		/// <param name="title">The feed title</param>
		/// <param name="description">The feed description</param>
		/// <param name="posts">The posts to output</param>
		public static void Generate(HttpContext context, string title, string description, IList<Post> posts) {
			// Read configuration
			var exerpt = SysParam.GetByName("RSS_USE_EXCERPT").Value == "1" ;

			// Create the feed
			context.Response.Clear() ;
			context.Response.ContentType = "text/xml" ;
			context.Response.ContentEncoding = Encoding.UTF8 ;
			var feed = new XmlTextWriter(context.Response.OutputStream, Encoding.UTF8) ;

			// Open the feed
			feed.WriteStartDocument() ;
			feed.WriteStartElement("rss") ;
			feed.WriteAttributeString("version", "2.0") ;
			feed.WriteStartElement("channel") ;
			feed.WriteElementString("title", title) ;
			feed.WriteElementString("link", WebPages.WebPiranha.GetSiteUrl()) ;
			feed.WriteElementString("description", description) ;
			feed.WriteElementString("generator", Generator()) ;

			foreach (var post in posts) {
				feed.WriteStartElement("item") ;
				feed.WriteElementString("title", post.Title) ;
				if (exerpt)
					feed.WriteElementString("description", post.Excerpt) ;
				else
					feed.WriteElementString("description", post.Body.ToString()) ;
				feed.WriteElementString("link", PublicLink(context.Request, post.Permalink)) ;
				feed.WriteElementString("pubDate", post.Published.ToLongDateString()) ;
				feed.WriteEndElement();
			}

			// Close the feed
			feed.WriteEndElement() ;
			feed.WriteEndElement() ;
			feed.WriteEndDocument() ;
			feed.Flush() ;
			feed.Close() ;
			context.Response.End() ;
		}

		#region Private methods
		/// <summary>
		/// Gets the public link for the permalink.
		/// </summary>
		/// <param name="request">The request object</param>
		/// <param name="permalink">The permalink</param>
		/// <returns>The public link</returns>
		private static string PublicLink(HttpRequest request, string permalink) {
			return WebPages.WebPiranha.GetSiteUrl() + "/" +
				WebPages.WebPiranha.GetUrlPrefixForHandlerId("PERMALINK") + "/" + permalink ;
		}

		/// <summary>
		/// Gets the generator name.
		/// </summary>
		/// <returns>The generator.</returns>
		private static string Generator() {
			return "Piranha v" + FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion ;
		}
		#endregion
	}
}
