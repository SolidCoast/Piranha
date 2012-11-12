// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContentController.cs" company="">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Piranha.Areas.Manager.Controllers
{
    using System;
    using System.Web.Mvc;

    using Piranha.Models.Manager.ContentModels;

    /// <summary>
    /// </summary>
    public class ContentController : ManagerController
    {
        #region Public Methods and Operators

        /// <summary>
        /// Deletes the specified content record.
        /// </summary>
        /// <param name="id">
        /// The content id
        /// </param>
        /// <returns>
        /// </returns>
        [Access(Function = "ADMIN_CONTENT")]
        public ActionResult Delete(string id)
        {
            EditModel m = EditModel.GetById(new Guid(id));

            if (m.DeleteAll())
            {
                if (m.Content.IsImage)
                {
                    SuccessMessage(Resources.Content.MessageImageDeleted);
                }
                else if (m.Content.IsFolder)
                {
                    SuccessMessage(Resources.Content.MessageFolderDeleted);
                }
                else
                {
                    SuccessMessage(Resources.Content.MessageDocumentDeleted);
                }
            }
            else
            {
                if (m.Content.IsImage)
                {
                    ErrorMessage(Resources.Content.MessageImageNotDeleted);
                }
                else if (m.Content.IsFolder)
                {
                    ErrorMessage(Resources.Content.MessageFolderNotDeleted);
                }
                else
                {
                    ErrorMessage(Resources.Content.MessageDocumentNotDeleted);
                }
            }

            return Index();
        }

        /// <summary>
        /// Edits or inserts a new content model.
        /// </summary>
        /// <param name="id">
        /// The id of the content
        /// </param>
        /// <returns>
        /// </returns>
        [Access(Function = "ADMIN_CONTENT")]
        public ActionResult Edit(string id)
        {
            return EditInternal(EditModel.GetById(new Guid(id)));
        }

        /// <summary>
        /// Saves the current edit model.
        /// </summary>
        /// <param name="m">
        /// The model
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        [Access(Function = "ADMIN_CONTENT")]
        public ActionResult Edit(EditModel m)
        {
            if (m.SaveAll())
            {
                ViewBag.Folder = m.Content.IsFolder;
                if (m.Content.IsImage)
                {
                    ViewBag.Title = Resources.Content.EditTitleExistingImage;
                    SuccessMessage(Resources.Content.MessageImageSaved);
                }
                else if (m.Content.IsFolder)
                {
                    ViewBag.Title = Resources.Content.EditTitleExistingFolder;
                    SuccessMessage(Resources.Content.MessageFolderSaved);
                }
                else
                {
                    ViewBag.Title = Resources.Content.EditTitleExistingDocument;
                    SuccessMessage(Resources.Content.MessageDocumentSaved);
                }

                ModelState.Clear();
                m.Refresh();
                return View("Edit", m);
            }
            else
            {
                ViewBag.Title = Resources.Content.EditTitleNew;
                ErrorMessage(Resources.Content.MessageNotSaved);
                return View("Edit", m);
            }
        }

        /// <summary>
        /// Gets the list view.
        /// </summary>
        /// <param name="id">
        /// </param>
        /// <returns>
        /// </returns>
        [Access(Function = "ADMIN_CONTENT")]
        public ActionResult Index(string id = "")
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    ViewBag.Expanded = new Guid(id);
                }
                else
                {
                    ViewBag.Expanded = Guid.Empty;
                }
            }
            catch
            {
                ViewBag.Expanded = Guid.Empty;
            }

            return View("Index", ListModel.Get());
        }

        /// <summary>
        /// Inserts a new media object.
        /// </summary>
        /// <param name="id">
        /// </param>
        /// <returns>
        /// </returns>
        [Access(Function = "ADMIN_CONTENT")]
        public ActionResult Insert(string id)
        {
            return EditInternal(new EditModel(false, !string.IsNullOrEmpty(id) ? new Guid(id) : Guid.Empty), true);
        }

        /// <summary>
        /// Inserts a new media folder object.
        /// </summary>
        /// <param name="id">
        /// </param>
        /// <returns>
        /// </returns>
        [Access(Function = "ADMIN_CONTENT")]
        public ActionResult InsertFolder(string id)
        {
            return EditInternal(new EditModel(true, !string.IsNullOrEmpty(id) ? new Guid(id) : Guid.Empty), true);
        }

        /// <summary>
        /// Gets the popup list.
        /// </summary>
        /// <param name="id">
        /// </param>
        /// <returns>
        /// </returns>
        public ActionResult Popup(string id = "")
        {
            return View("Popup", PopupModel.Get(id));
        }

        /// <summary>
        /// Uploads a new content object from the popup dialog.
        /// </summary>
        /// <param name="m">
        /// The popup module
        /// </param>
        /// <returns>
        /// A json result
        /// </returns>
        [HttpPost]
        [Access(Function = "ADMIN_CONTENT")]
        public JsonResult Popup(PopupModel m)
        {
            if (ModelState.IsValid)
            {
                var em = new EditModel();
                em.Content = m.NewContent;
                em.FileUrl = m.FileUrl;
                em.UploadedFile = m.UploadedFile;

                if (em.SaveAll())
                {
                    return new JsonResult { Data = true };
                }
            }

            return new JsonResult { Data = false };
        }

        /// <summary>
        /// Gets the files list.
        /// </summary>
        /// <returns>
        /// </returns>
        [Access(Function = "ADMIN_CONTENT")]
        public ActionResult Uploads()
        {
            return View("Uploads", UploadModel.Get());
        }

        #endregion

        #region Methods

        /// <summary>
        /// Insert or update of a media object.
        /// </summary>
        /// <param name="m">
        /// The new or existing model
        /// </param>
        /// <param name="insert">
        /// Weather to insert or update
        /// </param>
        /// <returns>
        /// </returns>
        private ActionResult EditInternal(EditModel m, bool insert = false)
        {
            ViewBag.Folder = m.Content.IsFolder;

            if (insert)
            {
                ViewBag.Title = Resources.Content.EditTitleNew;
            }
            else if (m.Content.IsImage)
            {
                ViewBag.Title = Resources.Content.EditTitleExistingImage;
            }
            else if (m.Content.IsFolder)
            {
                ViewBag.Title = Resources.Content.EditTitleExistingFolder;
            }
            else
            {
                ViewBag.Title = Resources.Content.EditTitleExistingDocument;
            }

            return View("Edit", m);
        }

        #endregion
    }
}