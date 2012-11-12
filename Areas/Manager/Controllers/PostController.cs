// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PostController.cs" company="">
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

    using Piranha.Models.Manager.PostModels;
    using Piranha.Resources;
    using Piranha.WebPages;

    /// <summary>
    /// </summary>
    public class PostController : ManagerController
    {
        #region Public Methods and Operators

        /// <summary>
        /// Deletes the post.
        /// </summary>
        /// <param name="id">
        /// The post id
        /// </param>
        /// <returns>
        /// </returns>
        [Access(Function = "ADMIN_POST")]
        public ActionResult Delete(string id)
        {
            EditModel pm = EditModel.GetById(new Guid(id));

            if (pm.DeleteAll())
            {
                SuccessMessage(Post.MessageDeleted);
            }
            else
            {
                ErrorMessage(Post.MessageNotDeleted);
            }

            return Index();
        }

        /// <summary>
        /// Edits the post with the given id.
        /// </summary>
        /// <param name="id">
        /// The post id
        /// </param>
        /// <returns>
        /// </returns>
        [Access(Function = "ADMIN_POST")]
        public ActionResult Edit(string id)
        {
            EditModel m = EditModel.GetById(new Guid(id));
            ViewBag.Title = Post.EditTitleExisting;

            // Executes the post edit loaded hook, if registered
            if (Hooks.Manager.PostEditModelLoaded != null)
            {
                Hooks.Manager.PostEditModelLoaded(this, Manager.GetActiveMenuItem(), m);
            }

            return View(@"~/Areas/Manager/Views/Post/Edit.cshtml", m);
        }

        /// <summary>
        /// Saves the model.
        /// </summary>
        /// <param name="draft">
        /// </param>
        /// <param name="m">
        /// The model
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        [ValidateInput(false)]
        [Access(Function = "ADMIN_POST")]
        public ActionResult Edit(bool draft, EditModel m)
        {
            if (ModelState.IsValid)
            {
                if (m.SaveAll(draft))
                {
                    ModelState.Clear();
                    if (!draft)
                    {
                        SuccessMessage(Post.MessagePublished);
                    }
                    else
                    {
                        SuccessMessage(Post.MessageSaved);
                    }
                }
                else
                {
                    ErrorMessage(Post.MessageNotSaved);
                }
            }

            m.Refresh();

            if (m.Post.IsNew)
            {
                ViewBag.Title = Post.EditTitleNew + m.Template.Name.ToLower();
            }
            else
            {
                ViewBag.Title = Post.EditTitleExisting;
            }

            return View(@"~/Areas/Manager/Views/Post/Edit.cshtml", m);
        }

        /// <summary>
        /// Default constructor. Gets the post list.
        /// </summary>
        /// <returns>
        /// </returns>
        [Access(Function = "ADMIN_POST")]
        public ActionResult Index()
        {
            ListModel m = ListModel.Get();
            ViewBag.Title = Post.ListTitle;

            // Executes the post list loaded hook, if registered
            if (Hooks.Manager.PostListModelLoaded != null)
            {
                Hooks.Manager.PostListModelLoaded(this, Manager.GetActiveMenuItem(), m);
            }

            return View(@"~/Areas/Manager/Views/Post/Index.cshtml", m);
        }

        /// <summary>
        /// Creates a new post.
        /// </summary>
        /// <param name="im">
        /// The insert model
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        [Access(Function = "ADMIN_POST")]
        public ActionResult Insert(InsertModel im)
        {
            EditModel pm = EditModel.CreateByTemplate(im.TemplateId);

            ViewBag.Title = Post.EditTitleNew + pm.Template.Name.ToLower();

            // Executes the post edit loaded hook, if registered
            if (Hooks.Manager.PostEditModelLoaded != null)
            {
                Hooks.Manager.PostEditModelLoaded(this, Manager.GetActiveMenuItem(), pm);
            }

            return View(@"~/Areas/Manager/Views/Post/Edit.cshtml", pm);
        }

        /// <summary>
        /// Reverts to latest published verison.
        /// </summary>
        /// <param name="id">
        /// The post id.
        /// </param>
        /// <returns>
        /// </returns>
        [Access(Function = "ADMIN_POST")]
        public ActionResult Revert(string id)
        {
            EditModel.Revert(new Guid(id));

            SuccessMessage(Post.MessageReverted);

            return Edit(id);
        }

        /// <summary>
        /// Unpublishes the specified page.
        /// </summary>
        /// <param name="id">
        /// The post id
        /// </param>
        /// <returns>
        /// </returns>
        [Access(Function = "ADMIN_POST")]
        public ActionResult Unpublish(string id)
        {
            EditModel.Unpublish(new Guid(id));

            SuccessMessage(Post.MessageUnpublished);

            return Edit(id);
        }

        #endregion
    }
}