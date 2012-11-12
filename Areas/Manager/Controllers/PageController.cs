// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PageController.cs" company="">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Piranha.Areas.Manager.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    using Piranha.Models;
    using Piranha.Models.Manager.PageModels;
    using Piranha.Resources;
    using Piranha.WebPages;

    using Page = Piranha.Resources.Page;

    /// <summary>
    /// </summary>
    public class PageController : ManagerController
    {
        #region Public Methods and Operators

        /// <summary>
        /// Deletes the page specified by the given id.
        /// </summary>
        /// <param name="id">
        /// The page id
        /// </param>
        /// <returns>
        /// </returns>
        [Access(Function = "ADMIN_PAGE")]
        public ActionResult Delete(string id)
        {
            var pm = EditModel.GetById(new Guid(id), true);

            try
            {
                if (pm.DeleteAll())
                {
                    SuccessMessage(Page.MessageDeleted);
                }
                else
                {
                    ErrorMessage(Page.MessageNotDeleted);
                }
            }
            catch (Exception e)
            {
                ErrorMessage(e.ToString());
            }

            return Index();
        }

        /// <summary>
        /// Opens the edit view for the selected page.
        /// </summary>
        /// <param name="id">
        /// The page id
        /// </param>
        /// <returns>
        /// </returns>
        [Access(Function = "ADMIN_PAGE")]
        public ActionResult Edit(string id)
        {
            EditModel pm = EditModel.GetById(new Guid(id));

            ViewBag.Title = Page.EditTitleExisting;

            // Executes the page list loaded hook, if registered
            if (Hooks.Manager.PageEditModelLoaded != null)
            {
                Hooks.Manager.PageEditModelLoaded(this, Manager.GetActiveMenuItem(), pm);
            }

            return View(@"~/Areas/Manager/Views/Page/Edit.cshtml", pm);
        }

        /// <summary>
        /// Saves the currently edited page.
        /// </summary>
        /// <param name="draft">
        /// </param>
        /// <param name="pm">
        /// The page model
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        [ValidateInput(false)]
        [Access(Function = "ADMIN_PAGE")]
        public ActionResult Edit(bool draft, EditModel pm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (pm.SaveAll(draft))
                    {
                        ModelState.Clear();
                        if (!draft)
                        {
                            SuccessMessage(Page.MessagePublished);
                        }
                        else
                        {
                            SuccessMessage(Page.MessageSaved);
                        }
                    }
                    else
                    {
                        ErrorMessage(Page.MessageNotSaved);
                    }
                }
                catch (DuplicatePermalinkException)
                {
                    // Manually set the duplicate error.
                    ModelState.AddModelError("Permalink", Global.PermalinkDuplicate);

                    // If this is the default permalink, remove the model state so it will be shown.
                    if (Permalink.Generate(pm.Page.Title) == pm.Permalink.Name)
                    {
                        ModelState.Remove("Permalink.Name");
                    }
                }
                catch (Exception e)
                {
                    ErrorMessage(e.ToString());
                }
            }

            pm.Refresh();

            if (pm.Page.IsNew)
            {
                ViewBag.Title = Page.EditTitleNew + pm.Template.Name.ToLower();
            }
            else
            {
                ViewBag.Title = Page.EditTitleExisting;
            }

            return View(@"~/Areas/Manager/Views/Page/Edit.cshtml", pm);
        }

        /// <summary>
        /// Gets the grouplist for the given group and page.
        /// </summary>
        /// <param name="page_id">
        /// The page id.
        /// </param>
        /// <param name="group_id">
        /// The group id.
        /// </param>
        /// <returns>
        /// </returns>
        public ActionResult GroupList(string page_id, string group_id)
        {
            Models.Page page = Models.Page.GetSingle(new Guid(page_id), true);
            List<SysGroup> groups = SysGroup.GetParents(new Guid(group_id));
            groups.Reverse();

            return View("Partial/GroupList", new GroupListModel { Groups = groups, Page = page });
        }

        /// <summary>
        /// Default controller. Gets the page list.
        /// </summary>
        /// <param name="id">
        /// </param>
        /// <returns>
        /// </returns>
        [Access(Function = "ADMIN_PAGE")]
        public ActionResult Index(string id = "")
        {
            try
            {
                SysParam param = SysParam.GetByName("SITEMAP_EXPANDED_LEVELS");
                ViewBag.Levels = param != null ? Convert.ToInt32(param.Value) : 0;

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
                ViewBag.Levels = 0;
                ViewBag.Expanded = Guid.Empty;
            }

            ListModel m = ListModel.Get();
            ViewBag.Title = Page.ListTitle;

            // Executes the page list loaded hook, if registered
            if (Hooks.Manager.PageListModelLoaded != null)
            {
                Hooks.Manager.PageListModelLoaded(this, Manager.GetActiveMenuItem(), m);
            }

            return View(@"~/Areas/Manager/Views/Page/Index.cshtml", m);
        }

        /// <summary>
        /// Creates a new page.
        /// </summary>
        /// <param name="im">
        /// The insert model
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        [Access(Function = "ADMIN_PAGE")]
        public ActionResult Insert(InsertModel im)
        {
            EditModel pm = EditModel.CreateByTemplateAndPosition(im.TemplateId, im.ParentId, im.Seqno);
            ViewBag.Title = Page.EditTitleNew + pm.Template.Name.ToLower();

            // Executes the page list loaded hook, if registered
            if (Hooks.Manager.PageEditModelLoaded != null)
            {
                Hooks.Manager.PageEditModelLoaded(this, Manager.GetActiveMenuItem(), pm);
            }

            return View(@"~/Areas/Manager/Views/Page/Edit.cshtml", pm);
        }

        /// <summary>
        /// Reverts to latest published verison.
        /// </summary>
        /// <param name="id">
        /// The page id.
        /// </param>
        /// <returns>
        /// </returns>
        [Access(Function = "ADMIN_PAGE")]
        public ActionResult Revert(string id)
        {
            EditModel.Revert(new Guid(id));

            SuccessMessage(Page.MessageReverted);

            return Edit(id);
        }

        /// <summary>
        /// Renders the sibling select list from the given input parameters.
        /// </summary>
        /// <param name="page_id">
        /// </param>
        /// <param name="page_parentid">
        /// </param>
        /// <param name="page_seqno">
        /// </param>
        /// <param name="parentid">
        /// </param>
        /// <returns>
        /// </returns>
        public ActionResult Siblings(string page_id, string page_parentid, string page_seqno, string parentid)
        {
            return View(EditModel.BuildSiblingPages(new Guid(page_id), new Guid(page_parentid), Convert.ToInt32(page_seqno), new Guid(parentid)));
        }

        /// <summary>
        /// Unpublishes the specified page.
        /// </summary>
        /// <param name="id">
        /// The page id
        /// </param>
        /// <returns>
        /// </returns>
        [Access(Function = "ADMIN_PAGE")]
        public ActionResult Unpublish(string id)
        {
            EditModel.Unpublish(new Guid(id));

            SuccessMessage(Page.MessageUnpublished);

            return Edit(id);
        }

        #endregion
    }
}