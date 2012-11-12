// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TemplateController.cs" company="">
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

    using Piranha.Models;
    using Piranha.Models.Manager.TemplateModels;
    using Piranha.Resources;

    /// <summary>
    /// </summary>
    public class TemplateController : ManagerController
    {
        #region Public Methods and Operators

        /// <summary>
        /// Deletes the specified page template.
        /// </summary>
        /// <param name="id">
        /// The template id
        /// </param>
        /// <returns>
        /// </returns>
        [Access(Function = "ADMIN_PAGE_TEMPLATE")]
        public ActionResult DeletePage(string id)
        {
            PageEditModel pm = PageEditModel.GetById(new Guid(id));

            if (pm.DeleteAll())
            {
                SuccessMessage(Template.MessagePageDeleted);
            }
            else
            {
                ErrorMessage(Template.MessagePageNotDeleted);
            }

            return RedirectToAction("pagelist");
        }

        /// <summary>
        /// Deletes the specified post template.
        /// </summary>
        /// <param name="id">
        /// The template id
        /// </param>
        /// <returns>
        /// </returns>
        [Access(Function = "ADMIN_POST_TEMPLATE")]
        public ActionResult DeletePost(string id)
        {
            PostEditModel pm = PostEditModel.GetById(new Guid(id));

            if (pm.DeleteAll())
            {
                SuccessMessage(Template.MessagePostDeleted);
            }
            else
            {
                ErrorMessage(Template.MessagePostNotDeleted);
            }

            return RedirectToAction("postlist");
        }

        /// <summary>
        /// Opens the insert or edit view for the template depending on
        /// weather a template id was passed to the action.
        /// </summary>
        /// <param name="id">
        /// The template id
        /// </param>
        /// <returns>
        /// </returns>
        [Access(Function = "ADMIN_PAGE_TEMPLATE")]
        public ActionResult Page(string id = "")
        {
            var m = new PageEditModel();

            if (id != string.Empty)
            {
                m = PageEditModel.GetById(new Guid(id));
                ViewBag.Title = Template.EditPageTitleExisting;
            }
            else
            {
                ViewBag.Title = Template.EditPageTitleNew;
            }

            return View("PageEdit", m);
        }

        /// <summary>
        /// Saves the current template.
        /// </summary>
        /// <param name="m">
        /// The model
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        [ValidateInput(false)]
        [Access(Function = "ADMIN_PAGE_TEMPLATE")]
        public ActionResult Page(PageEditModel m)
        {
            if (ModelState.IsValid)
            {
                if (m.SaveAll())
                {
                    ModelState.Clear();
                    ViewBag.Title = Template.EditPageTitleExisting;
                    SuccessMessage(Template.MessagePageSaved);
                }
                else
                {
                    ErrorMessage(Template.MessagePageNotSaved);
                }
            }
            else
            {
                if (m.Template.IsNew)
                {
                    ViewBag.Title = Template.EditPageTitleNew;
                }
                else
                {
                    ViewBag.Title = Template.EditPageTitleExisting;
                }
            }

            return View("PageEdit", m);
        }

        /// <summary>
        /// Gets the list of all page templates.
        /// </summary>
        /// <returns>
        /// </returns>
        [Access(Function = "ADMIN_PAGE_TEMPLATE")]
        public ActionResult PageList()
        {
            return View("PageList", PageListModel.Get());
        }

        /// <summary>
        /// Opens the insert or edit view for the template depending on
        /// weather a template id was passed to the action.
        /// </summary>
        /// <param name="id">
        /// The template id
        /// </param>
        /// <returns>
        /// </returns>
        [Access(Function = "ADMIN_POST_TEMPLATE")]
        public ActionResult Post(string id = "")
        {
            var m = new PostEditModel();

            if (id != string.Empty)
            {
                m = PostEditModel.GetById(new Guid(id));
                ViewBag.Title = Template.EditPostTitleExisting;
            }
            else
            {
                ViewBag.Title = Template.EditPostTitleNew;
            }

            return View("PostEdit", m);
        }

        /// <summary>
        /// Saves the current template.
        /// </summary>
        /// <param name="m">
        /// The model
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        [ValidateInput(false)]
        [Access(Function = "ADMIN_POST_TEMPLATE")]
        public ActionResult Post(PostEditModel m)
        {
            ViewBag.Title = Template.EditPostTitleNew;

            if (ModelState.IsValid)
            {
                if (m.SaveAll())
                {
                    ModelState.Clear();
                    ViewBag.Title = Template.EditPostTitleExisting;
                    SuccessMessage(Template.MessagePostSaved);
                }
                else
                {
                    ErrorMessage(Template.MessagePostNotSaved);
                }
            }

            return View("PostEdit", m);
        }

        /// <summary>
        /// Gets the list of post templates.
        /// </summary>
        /// <returns>
        /// </returns>
        [Access(Function = "ADMIN_POST_TEMPLATE")]
        public ActionResult PostList()
        {
            return View("PostList", PostListModel.Get());
        }

        /// <summary>
        /// Creates a new region template row from the given data.
        /// </summary>
        /// <param name="m">
        /// The model
        /// </param>
        /// <returns>
        /// The region template
        /// </returns>
        [HttpPost]
        public ActionResult RegionTemplate(RegionInsertModel m)
        {
            var region = new RegionTemplate { TemplateId = m.TemplateId, Name = m.Name, InternalId = m.InternalId, Type = m.Type, Seqno = m.Seqno };
            return View("Region", region);
        }

        #endregion
    }
}