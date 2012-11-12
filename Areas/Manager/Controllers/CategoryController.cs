// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CategoryController.cs" company="">
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

    using Piranha.Models.Manager.CategoryModels;
    using Piranha.Resources;

    /// <summary>
    /// Manager area controller for the category entity.
    /// </summary>
    public class CategoryController : ManagerController
    {
        #region Public Methods and Operators

        /// <summary>
        /// Deletes the category with the given id.
        /// </summary>
        /// <param name="id">
        /// The category id
        /// </param>
        /// <returns>
        /// </returns>
        [Access(Function = "ADMIN_CATEGORY")]
        public ActionResult Delete(string id)
        {
            EditModel m = EditModel.GetById(new Guid(id));

            if (m.DeleteAll())
            {
                SuccessMessage(Category.MessageDeleted);
            }
            else
            {
                ErrorMessage(Category.MessageNotDeleted);
            }

            return RedirectToAction("index");
        }

        /// <summary>
        /// Edits or inserts a new category.
        /// </summary>
        /// <param name="id">
        /// The category id
        /// </param>
        /// <returns>
        /// </returns>
        [Access(Function = "ADMIN_CATEGORY")]
        public ActionResult Edit(string id = "")
        {
            var m = new EditModel();

            if (id != string.Empty)
            {
                m = EditModel.GetById(new Guid(id));
                ViewBag.Title = Category.EditTitleExisting;
            }
            else
            {
                ViewBag.Title = Category.EditTitleNew;
            }

            return View("Edit", m);
        }

        /// <summary>
        /// Saves the given model.
        /// </summary>
        /// <param name="m">
        /// The model
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        [Access(Function = "ADMIN_CATEGORY")]
        public ActionResult Edit(EditModel m)
        {
            if (ModelState.IsValid)
            {
                if (m.SaveAll())
                {
                    ViewBag.Title = Category.EditTitleExisting;
                    SuccessMessage(Category.MessageSaved);
                    ModelState.Clear();
                }
                else
                {
                    ViewBag.Title = Category.EditTitleNew;
                    ErrorMessage(Category.MessageNotSaved);
                }
            }

            return View("Edit", m);
        }

        /// <summary>
        /// Gets the list view for the categories.
        /// </summary>
        /// <returns>
        /// </returns>
        [Access(Function = "ADMIN_CATEGORY")]
        public ActionResult Index()
        {
            return View("Index", ListModel.Get());
        }

        #endregion
    }
}