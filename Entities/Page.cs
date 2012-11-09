using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

using Piranha.Data;
using Piranha.WebPages;

namespace Piranha.Models
{
	/// <summary>
	/// Active record for a page.
	/// 
	/// Changes made to records of this type are logged.
	/// </summary>
	[PrimaryKey(Column="page_id,page_draft")]
	[Join(TableName="pagetemplate", ForeignKey="page_template_id", PrimaryKey="pagetemplate_id")]
	[Join(TableName="permalink", ForeignKey="page_permalink_id", PrimaryKey="permalink_id")]
	public class Page : DraftRecord<Page>, IPage, ISitemap, ICacheRecord<Page>
	{
		#region Fields
		/// <summary>
		/// Gets/sets the id.
		/// </summary>
		[Column(Name="page_id")]
		public override Guid Id { get ; set ; }

		/// <summary>
		/// Gets/sets weather this is a draft.
		/// </summary>
		[Column(Name="page_draft")]
		public override bool IsDraft { get ; set ; }

		/// <summary>
		/// Gets/sets the template id.
		/// </summary>
		[Column(Name="page_template_id")]
		public Guid TemplateId { get ; set ; }

		/// <summary>
		/// Gets/sets the group needed to view the page.
		/// </summary>
		[Column(Name="page_group_id")]
		[Display(ResourceType=typeof(Piranha.Resources.Page), Name="Visibility")]
		public Guid GroupId { get ; set ; }

		/// <summary>
		/// Gets/sets the optional group ids for which this page is disabled.
		/// </summary>
		[Column(Name="page_group_disable_id", Json=true, OnLoad="OnGuidListLoad")]
		[Display(ResourceType=typeof(Piranha.Resources.Page), Name="DisableGroups")]
		public List<Guid> DisabledGroups { get ; set ; }

		/// <summary>
		/// Gets/sets the parent id.
		/// </summary>
		[Column(Name="page_parent_id")]
		[Display(ResourceType=typeof(Piranha.Resources.Page), Name="ParentId")]
		public Guid ParentId { get ; set ; }
		
		/// <summary>
		/// Gets/sets the permalink id.
		/// </summary>
		[Column(Name="page_permalink_id")]
		public Guid PermalinkId { get ; set ; }

		/// <summary>
		/// Gets/sets the seqno specifying the structural position.
		/// </summary>
		[Column(Name="page_seqno")]
		[Display(ResourceType=typeof(Piranha.Resources.Page), Name="Seqno")]
		public int Seqno { get ; set ; }

		/// <summary>
		/// Gets/sets the title.
		/// </summary>
		[Column(Name="page_title")]
		[Display(ResourceType=typeof(Piranha.Resources.Page), Name="Title")]
		[Required(ErrorMessageResourceType=typeof(Piranha.Resources.Page), ErrorMessageResourceName="TitleRequired")]
		[StringLength(128, ErrorMessageResourceType=typeof(Piranha.Resources.Page), ErrorMessageResourceName="TitleLength")]
		public string Title { get ; set ; }

		/// <summary>
		/// Gets/sets the optional navigation title.
		/// </summary>
		[Column(Name="page_navigation_title")]
		[Display(ResourceType=typeof(Piranha.Resources.Page), Name="NavigationTitle")]
		[StringLength(128, ErrorMessageResourceType=typeof(Piranha.Resources.Page), ErrorMessageResourceName="NavigationTitleLength")]
		public string NavigationTitle { get ; set ; }

		/// <summary>
		/// Gets/sets the permalink.
		/// </summary>
		[Column(Name="permalink_name", ReadOnly=true, Table="permalink")]
		[Display(ResourceType=typeof(Piranha.Resources.Page), Name="Permalink")]
		public string Permalink { get ; set ; }

		/// <summary>
		/// Gets/sets weather the page should be visible in menus or not.
		/// </summary>
		[Column(Name="page_is_hidden")]
		[Display(ResourceType=typeof(Piranha.Resources.Page), Name="Hidden")]
		public bool IsHidden { get ; set ; }

		/// <summary>
		/// Gets/sets the meta keywords.
		/// </summary>
		[Column(Name="page_keywords")]
		[Display(ResourceType=typeof(Piranha.Resources.Page), Name="Keywords")]
		[StringLength(255, ErrorMessageResourceType=typeof(Piranha.Resources.Page), ErrorMessageResourceName="KeywordsLength")]
		public string Keywords { get ; set ; }

		/// <summary>
		/// Gets/sets the meta description.
		/// </summary>
		[Column(Name="page_description")]
		[Display(ResourceType=typeof(Piranha.Resources.Page), Name="Description")]
		[StringLength(255, ErrorMessageResourceType=typeof(Piranha.Resources.Page), ErrorMessageResourceName="DescriptionLength")]
		public string Description { get ; set ; }

		[Column(Name="page_attachments", Json = true, OnLoad="OnAttachmentsLoad")]
		public List<Guid> Attachments { get ; set ; }

		/// <summary>
		/// Gets/sets the custom controller.
		/// </summary>
		[Column(Name="page_controller")]
		[Display(ResourceType=typeof(Piranha.Resources.Page), Name="Template")]
		[StringLength(128, ErrorMessageResourceType=typeof(Piranha.Resources.Page), ErrorMessageResourceName="TemplateLength")]
		public string PageController { get ; set ; }

		/// <summary>
		/// Gets/sets the custom redirect.
		/// </summary>
		[Column(Name="page_redirect")]
		[Display(ResourceType=typeof(Piranha.Resources.Page), Name="Redirect")]
		[StringLength(128, ErrorMessageResourceType=typeof(Piranha.Resources.Page), ErrorMessageResourceName="RedirectLength")]
		public string PageRedirect { get ; set ; }

		/// <summary>
		/// Gets/sets the custom controller.
		/// </summary>
		[Column(Name="pagetemplate_controller", ReadOnly=true, Table="pagetemplate")]
		public string TemplateController { get ; private set ; }

		/// <summary>
		/// Gets/sets the custom controller.
		/// </summary>
		[Column(Name="pagetemplate_redirect", ReadOnly=true, Table="pagetemplate")]
		public string TemplateRedirect { get ; private set ; }

		/// <summary>
		/// Gets/sets the template name.
		/// </summary>
		[Column(Name="pagetemplate_name", Table="pagetemplate")]
		public string TemplateName { get ; private set ; }

		/// <summary>
		/// Gets/sets the created date.
		/// </summary>
		[Column(Name="page_created")]
		public override DateTime Created { get ; set ; }

		/// <summary>
		/// Gets/sets the updated date.
		/// </summary>
		[Column(Name="page_updated")]
		public override DateTime Updated { get ; set ; }

		/// <summary>
		/// Gets/sets the published date.
		/// </summary>
		[Column(Name="page_published")]
		public override DateTime Published { get ; set ; }

		/// <summary>
		/// Gets/sets the last published date.
		/// </summary>
		[Column(Name="page_last_published")]
		public override DateTime LastPublished { get ; set ; }

		/// <summary>
		/// Gets/sets the user id that created the record.
		/// </summary>
		[Column(Name="page_created_by")]
		public override Guid CreatedBy { get ; set ; }

		/// <summary>
		/// Gets/sets the user id that created the record.
		/// </summary>
		[Column(Name="page_updated_by")]
		public override Guid UpdatedBy { get ; set ; }
		#endregion

		#region Properties
		/// <summary>
		/// Gets the controller for the page.
		/// </summary>
		public string Controller { 
			get { return !String.IsNullOrEmpty(PageController) ? PageController : TemplateController ; }
		}

		/// <summary>
		/// Gets the redirect for the page.
		/// </summary>
		public string Redirect {
			get { return !String.IsNullOrEmpty(PageRedirect) ? PageRedirect : TemplateRedirect ; }
		}

		/// <summary>
		/// Gets weather the page is published or not.
		/// </summary>
		public bool IsPublished {
			get { return Published != DateTime.MinValue && Published < DateTime.Now ; }
		}

		/// <summary>
		/// Gets weather the page is the site startpage.
		/// </summary>
		public bool IsStartpage {
			get { return ParentId == Guid.Empty && Seqno == 1 ; }
		}

		/// <summary>
		/// Gets the page cache object.
		/// </summary>
		private static Dictionary<Guid, Page> Cache {
			get {
				if (HttpContext.Current != null) {
					if (HttpContext.Current.Cache[typeof(Page).Name] == null)
						HttpContext.Current.Cache[typeof(Page).Name] = new Dictionary<Guid, Page>() ;
					return (Dictionary<Guid, Page>)HttpContext.Current.Cache[typeof(Page).Name] ;
				}
				return new Dictionary<Guid,Page>() ;
			}
		}

		/// <summary>
		/// Gets the page cache object by permalink.
		/// </summary>
		private static Dictionary<string, Page> PermalinkCache {
			get {
				if (HttpContext.Current != null) {
					if (HttpContext.Current.Cache[typeof(Page).Name + "_Permalink"] == null)
						HttpContext.Current.Cache[typeof(Page).Name + "_Permalink"] = new Dictionary<string, Page>() ;
					return (Dictionary<string, Page>)HttpContext.Current.Cache[typeof(Page).Name + "_Permalink"] ;
				}
				return new Dictionary<string,Page>() ;
			}
		}

		/// <summary>
		/// Gets the page cache object by permalink id.
		/// </summary>
		private static Dictionary<Guid, Page> PermalinkIdCache {
			get {
				if (HttpContext.Current != null) {
					if (HttpContext.Current.Cache[typeof(Page).Name + "_PermalinkId"] == null)
						HttpContext.Current.Cache[typeof(Page).Name + "_PermalinkId"] = new Dictionary<Guid, Page>() ;
					return (Dictionary<Guid, Page>)HttpContext.Current.Cache[typeof(Page).Name + "_PermalinkId"] ;
				}
				return new Dictionary<Guid,Page>() ;
			}
		}
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public Page() : base() {
			ExtensionType = Extend.ExtensionType.Page ;
			DisabledGroups = new List<Guid>() ;
			IsDraft = true ;
			Seqno   = 1 ;
			Attachments = new List<Guid>() ;
			LogChanges = true ;
		}

		#region Static accessors
		/// <summary>
		/// Gets a single page.
		/// </summary>
		/// <param name="id">The page id</param>
		/// <returns>The page</returns>
		public static Page GetSingle(Guid id) {
			if (!Cache.ContainsKey(id)) {
				Page p = Page.GetSingle("page_id = @0 AND page_draft = 0", id) ;
				
				if (p != null) {
					Cache[p.Id] = p ;
					PermalinkCache[p.Permalink] = p ;
					PermalinkIdCache[p.PermalinkId] = p ;
				} else return null ;
			}
			return Cache[id] ;
		}

		/// <summary>
		/// Gets a single page.
		/// </summary>
		/// <param name="id">The page id</param>
		/// <param name="draft">Page status</param>
		/// <returns>The page</returns>
		public static Page GetSingle(Guid id, bool draft) {
			if (!draft)
				return GetSingle(id) ;
			return GetSingle("page_id = @0 AND page_draft = @1", id, draft) ;
		}

		/// <summary>
		/// Gets the site startpage
		/// </summary>
		/// <param name="draft">Weather to get the current draft</param>
		/// <returns>The startpage</returns>
		public static Page GetStartpage(bool draft = false) {
			if (!Cache.ContainsKey(Guid.Empty))
				Cache[Guid.Empty] = Page.GetSingle("page_parent_id IS NULL and page_seqno = 1 AND page_draft = @0", draft) ;
			return Cache[Guid.Empty] ;
		}

		/// <summary>
		/// Gets the page specified by the given permalink.
		/// </summary>
		/// <param name="permalink">The permalink</param>
		/// <param name="draft">Weather to get the current draft</param>
		/// <returns>The page</returns>
		public static Page GetByPermalink(string permalink, bool draft = false) {
			if (!draft) {
				if (!PermalinkCache.ContainsKey(permalink.ToLower())) {
					Page p = Page.GetSingle("permalink_name = @0 AND page_draft = @1", permalink, draft) ;

					if (p != null) {
						Cache[p.Id] = p ;
						PermalinkCache[p.Permalink] = p ;
						PermalinkIdCache[p.PermalinkId] = p ;
					}
				}
				return PermalinkCache.ContainsKey(permalink) ? PermalinkCache[permalink.ToLower()] : null ;
			}
			return Page.GetSingle("permalink_name = @0 AND page_draft = @1", permalink, draft) ;
		}

		/// <summary>
		/// Gets the page specified by the given permalink.
		/// </summary>
		/// <param name="permalink">The permalink</param>
		/// <param name="draft">Weather to get the current draft</param>
		/// <returns>The page</returns>
		public static Page GetByPermalinkId(Guid permalinkid, bool draft = false) {
			if (!draft) {
				if (!PermalinkIdCache.ContainsKey(permalinkid)) {
					Page p = Page.GetSingle("page_permalink_id = @0 AND page_draft = @1", permalinkid, draft) ;

					if (p != null) {
						Cache[p.Id] = p ;
						PermalinkCache[p.Permalink] = p ;
						PermalinkIdCache[p.PermalinkId] = p ;
					}
				}
				return PermalinkIdCache.ContainsKey(permalinkid) ? PermalinkIdCache[permalinkid] : null ;
			}
			return Page.GetSingle("page_permalink_id = @0 AND page_draft = @1", permalinkid, draft) ;
		}
		#endregion

		/// <summary>
		/// Saves the current record to the database.
		/// </summary>
		/// <param name="tx">Optional transaction</param>
		/// <returns>Wether the operation was successful</returns>
		public override bool Save(IDbTransaction tx = null) {
			// Move seqno & save, we need a transaction for this
			IDbTransaction t = tx != null ? tx : Database.OpenConnection().BeginTransaction() ;

			// We only move pages around as drafts. When we publish we
			// simply change states.
			if (IsDraft) {
				if (IsNew) {
					MoveSeqno(Id, ParentId, Seqno, true, t) ;
				} else {
					Page old = GetSingle(Id, true) ;
					Page pub = GetSingle(Id, false) ;

					if (old.ParentId != ParentId || old.Seqno != Seqno) {
						MoveSeqno(Id, old.ParentId, old.Seqno + 1, false, t) ;
						MoveSeqno(Id, ParentId, Seqno, true, t) ;
						if (pub != null) {
							pub.ParentId = ParentId ;
							pub.Seqno = Seqno ;
							pub.Save(t) ;
						}
					}
				}
			}
			return base.Save(tx) ;
		}

		/// <summary>
		/// Saves and publishes the current record.
		/// </summary>
		/// <param name="tx">Optional transaction</param>
		/// <returns>Weather the operation was successful</returns>
		public override bool SaveAndPublish(IDbTransaction tx = null) {
			// Move seqno & save, we need a transaction for this
			IDbTransaction t = tx != null ? tx : Database.OpenConnection().BeginTransaction() ;

			if (IsNew) {
				MoveSeqno(Id, ParentId, Seqno, true) ;
			} else {
				using (IDbTransaction itx = Database.OpenTransaction()) {
					Page old = GetSingle(Id, true) ;
					if (old.ParentId != ParentId || old.Seqno != Seqno) {
						MoveSeqno(Id, old.ParentId, old.Seqno + 1, false, itx) ;
						MoveSeqno(Id, ParentId, Seqno, true, itx) ;
						itx.Commit() ;
					}
				}
			}
			return base.SaveAndPublish(tx);
		}

		/// <summary>
		/// Deletes the current page.
		/// </summary>
		/// <param name="tx">Optional transaction</param>
		/// <returns>Weather the operation was successful</returns>
		public override bool Delete(IDbTransaction tx = null) {
			// Move seqno & delete. We need a transaction for this
			IDbTransaction t = tx != null ? tx : Database.OpenConnection().BeginTransaction() ;

			try {
				// Only move pages around when we're deleting the draft so we don't get
				// multiple move operations in the site tree.
				if (IsDraft)
					MoveSeqno(Id, ParentId, Seqno + 1, false, t) ;
				Web.ClientCache.SetSiteLastModified(t) ;
				if (base.Delete(t)) {
					if (tx == null) 
						t.Commit() ;
					return true ;
				} else {
					if (tx == null) 
						t.Rollback() ;
					return false ;
				}
			} catch {
				if (tx == null) t.Rollback() ;
				throw ;
			}
		}
	
		/// <summary>
		/// Moves the seqno around so that a page can be inserted into the structure.
		/// </summary>
		/// <param name="parentid">The parent id</param>
		/// <param name="seqno">The seqno</param>
		/// <param name="inc">Weather to increase or decrease</param>
		internal static void MoveSeqno(Guid pageid, Guid parentid, int seqno, bool inc, IDbTransaction tx = null) {
			if (parentid != Guid.Empty)
				Execute("UPDATE page SET page_seqno = page_seqno " + (inc ? "+ 1" : "- 1") +
					" WHERE page_parent_id = @0 AND page_seqno >= @1", tx, parentid, seqno) ;
			else Execute("UPDATE page SET page_seqno = page_seqno " + (inc ? "+ 1" : "- 1") +
				" WHERE page_parent_id IS NULL AND page_seqno >= @0", tx, seqno) ;
		}

		/// <summary>
		/// Invalidates the current record from the cache.
		/// </summary>
		/// <param name="record">The record</param>
		public void InvalidateRecord(Page record) {
			if (Cache.ContainsKey(record.Id))
				Cache.Remove(record.Id) ;
			// If we click save & publish right away the permalink is not created yet.
			if (record.Permalink != null && PermalinkCache.ContainsKey(record.Permalink))
				PermalinkCache.Remove(record.Permalink) ;
			if (record.Permalink != null && PermalinkIdCache.ContainsKey(record.PermalinkId))
				PermalinkIdCache.Remove(record.PermalinkId) ;
			if (record.IsStartpage && Cache.ContainsKey(Guid.Empty))
				Cache.Remove(Guid.Empty) ;

			// Invalidate public sitemap
			if (!record.IsDraft)
				Sitemap.InvalidateCache() ;
		}

		#region Handlers
		/// <summary>
		/// Create an empty attachment list if it is null in the database.
		/// </summary>
		/// <param name="lst">The attachments</param>
		/// <returns>The attachments, or a default list</returns>
		protected List<Guid> OnAttachmentsLoad(List<Guid> lst) {
			if (lst != null)
				return lst ;
			return new List<Guid>() ;
		}

		/// <summary>
		/// Create an empty guid list if it is null in the database.
		/// </summary>
		/// <param name="lst">The attachments</param>
		/// <returns>The guid list, or a default list</returns>
		protected List<Guid> OnGuidListLoad(List<Guid> lst) {
			if (lst != null)
				return lst ;
			return new List<Guid>() ;
		}
		#endregion
	}
}
