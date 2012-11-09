using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web;

using Piranha.Data;
using Piranha.Rest.DataContracts;

namespace Piranha.Rest
{
	/// <summary>
	/// ReST API for syncing changes.
	/// </summary>
	[ServiceContract()]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
	public class ChangeService : BaseService
	{
		[OperationContract()]
		[WebGet(UriTemplate="get/{date}/{time}", ResponseFormat=WebMessageFormat.Json)]
		public Stream GetChanges(string date, string time) {
			return Serialize(GetChangesInternal(date + FormatTime(time))) ;
		}

		internal Changes GetChangesInternal(string date) {
			Changes changes = new Changes() ;
			DateTime latest = Convert.ToDateTime(date) ;

			// Check if we have pages publised after the given date. If so return the sitemap
			if (Models.Page.GetScalar("SELECT COUNT(page_id) FROM page WHERE page_last_published > @0", latest) > 0)
				changes.Sitemap = new SitemapServices().Get() ;

			// Get all pages last published after the given date.
			Models.Page.GetFields("page_id", "page_last_published > @0 AND page_draft = 0", latest).ForEach(p => 
				changes.Pages.Add(new PageService().GetInternal(p.Id.ToString()))) ;

			// Get all posts last published after the given date.
			Models.Post.GetFields("post_id", "post_last_published > @0 AND post_draft = 0", latest).ForEach(p =>
				changes.Posts.Add(new PostService().GetInternal(p.Id.ToString()))) ;

			// Get all categories updated after the given date.
			new CategoryService().Get().Where(c => Convert.ToDateTime(c.Updated) > latest).
				Each((i, c) => changes.Categories.Add(c)) ;

			// Get all content updated after the given date.
			Models.Content.GetFields("content_id", "content_updated > @0 AND content_folder = 0", latest).ForEach(c =>
				changes.Content.Add(new ContentService().Get(c.Id.ToString()))) ;

			// Get all deleted content
			string query = "syslog_parent_type = @0 AND syslog_action = @1 AND syslog_created > @2" ;
			changes.Deleted.Pages = Piranha.Models.SysLog.Get(query, "PAGE", "DEPUBLISH", latest).
				Select(l => new DeletedItem() { Id = l.ParentId, Deleted = l.Created.ToString() }).ToList() ;
			changes.Deleted.Content = Piranha.Models.SysLog.Get(query, "CONTENT", "DELETE", latest).
				Select(l => new DeletedItem() { Id = l.ParentId, Deleted = l.Created.ToString() }).ToList() ;
			changes.Deleted.Categories = Piranha.Models.SysLog.Get(query, "CATEGORY", "DELETE", latest).
				Select(l => new DeletedItem() { Id = l.ParentId, Deleted = l.Created.ToString() }).ToList() ;

			// Set the timespage
			changes.Timestamp = DateTime.Now.ToString() ;

			return changes ;
		}

		private string FormatTime(string str) {
			if (!String.IsNullOrEmpty(str))
				return " " + str.Replace('-', ':') ;
			return "" ;
		}
	}
}
