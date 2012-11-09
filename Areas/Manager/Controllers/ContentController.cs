using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Piranha.Models.Manager.ContentModels;

namespace Piranha.Areas.Manager.Controllers
{
    public class ContentController : ManagerController
    {
		/// <summary>
		/// Gets the list view.
		/// </summary>
		[Access(Function="ADMIN_CONTENT")]
		public ActionResult Index(string id = "") {
			try {
				if (!String.IsNullOrEmpty(id))
					ViewBag.Expanded = new Guid(id) ;
				else ViewBag.Expanded = Guid.Empty ;
			} catch {
				ViewBag.Expanded = Guid.Empty ;
			}
			
            return View("Index", ListModel.Get());
        }

		/// <summary>
		/// Gets the popup list.
		/// </summary>
		public ActionResult Popup(string id = "") {
			return View("Popup", PopupModel.Get(id)) ;
		}

		/// <summary>
		/// Uploads a new content object from the popup dialog.
		/// </summary>
		/// <param name="m">The popup module</param>
		/// <returns>A json result</returns>
		[HttpPost()]
		[Access(Function="ADMIN_CONTENT")]
		public JsonResult Popup(PopupModel m) {
			if (ModelState.IsValid) {
				EditModel em = new EditModel() ;
				em.Content = m.NewContent ;
				em.FileUrl = m.FileUrl ;
				em.UploadedFile = m.UploadedFile ;

				if (em.SaveAll())
					return new JsonResult() { Data = true } ;
			}
			return new JsonResult() { Data = false } ;
		}

		/// <summary>
		/// Gets the files list.
		/// </summary>
		/// <returns></returns>
		[Access(Function="ADMIN_CONTENT")]
		public ActionResult Uploads() {
			return View("Uploads", UploadModel.Get()) ;
		}

		/// <summary>
		/// Inserts a new media object.
		/// </summary>
		/// <param name="parentid">The optional parent folder id.</param>
		[Access(Function="ADMIN_CONTENT")]
		public ActionResult Insert(string id) {
			return EditInternal(new EditModel(false, !String.IsNullOrEmpty(id) ? new Guid(id) : Guid.Empty), true) ;
		}

		/// <summary>
		/// Inserts a new media folder object.
		/// </summary>
		/// <param name="parentid">The optional parent folder id.</param>
		[Access(Function="ADMIN_CONTENT")]
		public ActionResult InsertFolder(string id) {
			return EditInternal(new EditModel(true, !String.IsNullOrEmpty(id) ? new Guid(id) : Guid.Empty), true) ;
		}

		/// <summary>
		/// Edits or inserts a new content model.
		/// </summary>
		/// <param name="id">The id of the content</param>
		[Access(Function="ADMIN_CONTENT")]
		public ActionResult Edit(string id) {
			return EditInternal(EditModel.GetById(new Guid(id))) ;
		}

		/// <summary>
		/// Insert or update of a media object.
		/// </summary>
		/// <param name="m">The new or existing model</param>
		/// <param name="insert">Weather to insert or update</param>
		/// <returns></returns>
		private ActionResult EditInternal(EditModel m, bool insert = false) {
			ViewBag.Folder = m.Content.IsFolder ;

			if (insert) 
				ViewBag.Title = Piranha.Resources.Content.EditTitleNew ;
			else if (m.Content.IsImage)
				ViewBag.Title = Piranha.Resources.Content.EditTitleExistingImage ;
			else if (m.Content.IsFolder)
				ViewBag.Title = Piranha.Resources.Content.EditTitleExistingFolder ;
			else ViewBag.Title = Piranha.Resources.Content.EditTitleExistingDocument ;

			return View("Edit", m) ;
		}

		/// <summary>
		/// Saves the current edit model.
		/// </summary>
		/// <param name="m">The model</param>
		[HttpPost()]
		[Access(Function="ADMIN_CONTENT")]
		public ActionResult Edit(EditModel m) {
			if (m.SaveAll()) {
				ViewBag.Folder = m.Content.IsFolder ;
				if (m.Content.IsImage) {
					ViewBag.Title = Piranha.Resources.Content.EditTitleExistingImage ;
					SuccessMessage(Piranha.Resources.Content.MessageImageSaved) ;
				} else if (m.Content.IsFolder) {
					ViewBag.Title = Piranha.Resources.Content.EditTitleExistingFolder ;
					SuccessMessage(Piranha.Resources.Content.MessageFolderSaved) ;
				} else {
					ViewBag.Title = Piranha.Resources.Content.EditTitleExistingDocument ;
					SuccessMessage(Piranha.Resources.Content.MessageDocumentSaved) ;
				}
				ModelState.Clear() ;
				m.Refresh() ;
				return View("Edit", m) ;
			} else {
				ViewBag.Title = Piranha.Resources.Content.EditTitleNew ;
				ErrorMessage(Piranha.Resources.Content.MessageNotSaved) ;
				return View("Edit", m) ;
			}
		}

		/// <summary>
		/// Deletes the specified content record.
		/// </summary>
		/// <param name="id">The content id</param>
		[Access(Function="ADMIN_CONTENT")]
		public ActionResult Delete(string id) {
			EditModel m = EditModel.GetById(new Guid(id)) ;

			if (m.DeleteAll()) {
				if (m.Content.IsImage)
					SuccessMessage(Piranha.Resources.Content.MessageImageDeleted) ;
				else if (m.Content.IsFolder)
					SuccessMessage(Piranha.Resources.Content.MessageFolderDeleted) ;
				else SuccessMessage(Piranha.Resources.Content.MessageDocumentDeleted) ;
			} else {
				if (m.Content.IsImage)
					ErrorMessage(Piranha.Resources.Content.MessageImageNotDeleted) ;
				else if (m.Content.IsFolder)
					ErrorMessage(Piranha.Resources.Content.MessageFolderNotDeleted) ;
				else ErrorMessage(Piranha.Resources.Content.MessageDocumentNotDeleted) ;
			} 
			return Index() ;
		}
    }
}
