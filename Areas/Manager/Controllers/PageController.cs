using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Piranha.Models;
using Piranha.Models.Manager.PageModels;

namespace Piranha.Areas.Manager.Controllers
{
    public class PageController : ManagerController
    {
		/// <summary>
		/// Default controller. Gets the page list.
		/// </summary>
		[Access(Function="ADMIN_PAGE")]
        public ActionResult Index(string id = "") {
			try {
				var param = Models.SysParam.GetByName("SITEMAP_EXPANDED_LEVELS") ;
				ViewBag.Levels = param != null ? Convert.ToInt32(param.Value) : 0 ;

				if (!String.IsNullOrEmpty(id))
					ViewBag.Expanded = new Guid(id) ;
				else ViewBag.Expanded = Guid.Empty ;
			} catch {
				ViewBag.Levels = 0 ;
				ViewBag.Expanded = Guid.Empty ;
			}
			var m = ListModel.Get() ;
			ViewBag.Title = @Piranha.Resources.Page.ListTitle ;

			// Executes the page list loaded hook, if registered
			if (WebPages.Hooks.Manager.PageListModelLoaded != null)
				WebPages.Hooks.Manager.PageListModelLoaded(this, WebPages.Manager.GetActiveMenuItem(), m) ;

			return View(@"~/Areas/Manager/Views/Page/Index.cshtml", m) ;
        }

		/// <summary>
		/// Opens the edit view for the selected page.
		/// </summary>
		/// <param name="id">The page id</param>
		[Access(Function="ADMIN_PAGE")]
		public ActionResult Edit(string id) {
			EditModel pm = EditModel.GetById(new Guid(id)) ;

			ViewBag.Title = Piranha.Resources.Page.EditTitleExisting ;

			// Executes the page list loaded hook, if registered
			if (WebPages.Hooks.Manager.PageEditModelLoaded != null)
				WebPages.Hooks.Manager.PageEditModelLoaded(this, WebPages.Manager.GetActiveMenuItem(), pm) ;

			return View(@"~/Areas/Manager/Views/Page/Edit.cshtml", pm) ;
		}

		/// <summary>
		/// Saves the currently edited page.
		/// </summary>
		/// <param name="pm">The page model</param>
		[HttpPost(), ValidateInput(false)]
		[Access(Function="ADMIN_PAGE")]
		public ActionResult Edit(bool draft, EditModel pm) {
			if (ModelState.IsValid) {
				try {
					if (pm.SaveAll(draft)) {
						ModelState.Clear() ;
						if (!draft)
							SuccessMessage(Piranha.Resources.Page.MessagePublished) ;
						else SuccessMessage(Piranha.Resources.Page.MessageSaved) ;
					} else ErrorMessage(Piranha.Resources.Page.MessageNotSaved) ;
				} catch (DuplicatePermalinkException) {
					// Manually set the duplicate error.
					ModelState.AddModelError("Permalink", @Piranha.Resources.Global.PermalinkDuplicate) ;
					// If this is the default permalink, remove the model state so it will be shown.
					if (Permalink.Generate(pm.Page.Title) == pm.Permalink.Name)
						ModelState.Remove("Permalink.Name") ;
				} catch (Exception e) {
					ErrorMessage(e.ToString()) ;
				}
			}
			pm.Refresh();

			if (pm.Page.IsNew)
				ViewBag.Title = Piranha.Resources.Page.EditTitleNew + pm.Template.Name.ToLower() ;
			else ViewBag.Title = Piranha.Resources.Page.EditTitleExisting ;

			return View(@"~/Areas/Manager/Views/Page/Edit.cshtml", pm) ;
		}

		/// <summary>
		/// Creates a new page.
		/// </summary>
		/// <param name="im">The insert model</param>
		[HttpPost()]
		[Access(Function="ADMIN_PAGE")]
		public ActionResult Insert(InsertModel im) {
			EditModel pm = EditModel.CreateByTemplateAndPosition(im.TemplateId, im.ParentId, im.Seqno) ;
			ViewBag.Title = Piranha.Resources.Page.EditTitleNew + pm.Template.Name.ToLower() ;

			// Executes the page list loaded hook, if registered
			if (WebPages.Hooks.Manager.PageEditModelLoaded != null)
				WebPages.Hooks.Manager.PageEditModelLoaded(this, WebPages.Manager.GetActiveMenuItem(), pm) ;

			return View(@"~/Areas/Manager/Views/Page/Edit.cshtml", pm) ;
		}

		/// <summary>
		/// Deletes the page specified by the given id.
		/// </summary>
		/// <param name="id">The page id</param>
		[Access(Function="ADMIN_PAGE")]
		public ActionResult Delete(string id) {
			EditModel pm = EditModel.GetById(new Guid(id), true) ;

			try {
				if (pm.DeleteAll())
					SuccessMessage(Piranha.Resources.Page.MessageDeleted) ;
				else ErrorMessage(Piranha.Resources.Page.MessageNotDeleted) ;
			} catch (Exception e) {
				ErrorMessage(e.ToString()) ;
			}

			return Index() ;
		}

		/// <summary>
		/// Reverts to latest published verison.
		/// </summary>
		/// <param name="id">The page id.</param>
		[Access(Function="ADMIN_PAGE")]
		public ActionResult Revert(string id) {
			EditModel.Revert(new Guid(id)) ;

			SuccessMessage(Piranha.Resources.Page.MessageReverted) ;

			return Edit(id) ;
		}

		/// <summary>
		/// Unpublishes the specified page.
		/// </summary>
		/// <param name="id">The page id</param>
		[Access(Function="ADMIN_PAGE")]
		public ActionResult Unpublish(string id) {
			EditModel.Unpublish(new Guid(id)) ;

			SuccessMessage(Piranha.Resources.Page.MessageUnpublished) ;

			return Edit(id) ;
		}

		/// <summary>
		/// Renders the sibling select list from the given input parameters.
		/// </summary>
		public ActionResult Siblings(string page_id, string page_parentid, string page_seqno, string parentid) {
			return View(EditModel.BuildSiblingPages(new Guid(page_id), new Guid(page_parentid), Convert.ToInt32(page_seqno), new Guid(parentid))) ;
		}

		/// <summary>
		/// Gets the grouplist for the given group and page.
		/// </summary>
		/// <param name="page_id">The page id.</param>
		/// <param name="group_id">The group id.</param>
		public ActionResult GroupList(string page_id, string group_id) {
			var page = Models.Page.GetSingle(new Guid(page_id), true) ;
			var groups = SysGroup.GetParents(new Guid(group_id)) ;
			groups.Reverse() ;

			return View("Partial/GroupList", new GroupListModel() { 
				Groups = groups, Page = page }) ;
		}
    }
}
