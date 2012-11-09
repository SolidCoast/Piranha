using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Piranha.Models.Manager.CategoryModels;

namespace Piranha.Areas.Manager.Controllers
{
	/// <summary>
	/// Manager area controller for the category entity.
	/// </summary>
	public class CategoryController : ManagerController
	{
		/// <summary>
		/// Gets the list view for the categories.
		/// </summary>
		[Access(Function="ADMIN_CATEGORY")]
		public ActionResult Index() {
			return View("Index", ListModel.Get()) ;
		}

		/// <summary>
		/// Edits or inserts a new category.
		/// </summary>
		/// <param name="id">The category id</param>
		[Access(Function="ADMIN_CATEGORY")]
		public ActionResult Edit(string id = "") {
			EditModel m = new EditModel() ;

			if (id != "") {
				m = EditModel.GetById(new Guid(id)) ;
				ViewBag.Title = Piranha.Resources.Category.EditTitleExisting ;
			} else {
				ViewBag.Title = Piranha.Resources.Category.EditTitleNew ;
			}
			return View("Edit", m) ;
		}

		/// <summary>
		/// Saves the given model.
		/// </summary>
		/// <param name="m">The model</param>
		/// <returns></returns>
		[HttpPost()]
		[Access(Function="ADMIN_CATEGORY")]
		public ActionResult Edit(EditModel m) {
			if (ModelState.IsValid) {
				if (m.SaveAll()) {
					ViewBag.Title = Piranha.Resources.Category.EditTitleExisting ;
					SuccessMessage(Piranha.Resources.Category.MessageSaved) ;
					ModelState.Clear() ;
				} else {
					ViewBag.Title = Piranha.Resources.Category.EditTitleNew ;
					ErrorMessage(Piranha.Resources.Category.MessageNotSaved) ;
				}
			}
			return View("Edit", m) ;
		}

		/// <summary>
		/// Deletes the category with the given id.
		/// </summary>
		/// <param name="id">The category id</param>
		[Access(Function="ADMIN_CATEGORY")]
		public ActionResult Delete(string id) {
			EditModel m = EditModel.GetById(new Guid(id)) ;

			if (m.DeleteAll())
				SuccessMessage(Piranha.Resources.Category.MessageDeleted) ;
			else ErrorMessage(Piranha.Resources.Category.MessageNotDeleted) ;
			return  RedirectToAction("index") ;
		}
	}
}
