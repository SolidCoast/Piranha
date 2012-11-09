using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Xml;

using Piranha.Data;
using Piranha.Models;

namespace Piranha.WebPages.RequestHandlers
{
	public class RssHandler : IRequestHandler
	{
		/// <summary>
		/// Handles the current request.
		/// </summary>
		/// <param name="context">The current context</param>
		/// <param name="args">Optional url arguments passed to the handler</param>
		public virtual void HandleRequest(System.Web.HttpContext context, params string[] args) {
			var top = Convert.ToInt32(SysParam.GetByName("RSS_NUM_POSTS").Value) ;
			var posts = Post.Get("post_draft = 0 AND post_rss = 1 AND post_template_id IN (SELECT posttemplate_id FROM posttemplate WHERE posttemplate_rss = 1)", new Params() { OrderBy = "post_published DESC", Top = top }) ;

			Web.RssHelper.Generate(context, 
				SysParam.GetByName("SITE_TITLE").Value,
				SysParam.GetByName("SITE_DESCRIPTION").Value,
				posts) ;
		}
	}
}
