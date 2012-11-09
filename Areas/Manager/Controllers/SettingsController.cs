using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Piranha;
using Piranha.Data;
using Piranha.Models;
using Piranha.Models.Manager.SettingModels;

namespace Piranha.Areas.Manager.Controllers
{
	/// <summary>
	/// Settings controller for the manager area.
	/// </summary>
    public class SettingsController : ManagerController
    {
		#region User actions
		/// <summary>
		/// Gets the list of all users.
		/// </summary>
		/// <returns></returns>
		[Access(Function="ADMIN_USER")]
		public ActionResult UserList() {
			var m = UserListModel.Get() ;
			ViewBag.Title = @Piranha.Resources.Settings.ListTitleUsers ;

			// Executes the user list loaded hook, if registered
			if (WebPages.Hooks.Manager.UserListModelLoaded != null)
				WebPages.Hooks.Manager.UserListModelLoaded(this, WebPages.Manager.GetActiveMenuItem(), m) ;

            return View(@"~/Areas/Manager/Views/Settings/UserList.cshtml", m);
		}

		/// <summary>
		/// Edits or creates a new user.
		/// </summary>
		/// <param name="id">The user id</param>
		[Access(Function="ADMIN_USER")]
		public new ActionResult User(string id) {
			var m = new UserEditModel() ;

			if (!String.IsNullOrEmpty(id)) {
				ViewBag.Title = Piranha.Resources.Settings.EditTitleExistingUser ;
				m = UserEditModel.GetById(new Guid(id)) ;
			} else {
				ViewBag.Title = Piranha.Resources.Settings.EditTitleNewUser ;
			}

			// Executes the user list loaded hook, if registered
			if (WebPages.Hooks.Manager.UserEditModelLoaded != null)
				WebPages.Hooks.Manager.UserEditModelLoaded(this, WebPages.Manager.GetActiveMenuItem(), m) ;
	
			return View(@"~/Areas/Manager/Views/Settings/User.cshtml", m) ;
		}

		/// <summary>
		/// Saves the model
		/// </summary>
		/// <param name="em">The model</param>
		[HttpPost()]
		[Access(Function="ADMIN_USER")]
		public new ActionResult User(UserEditModel um) {
			if (um.User.IsNew)
				ViewBag.Title = Piranha.Resources.Settings.EditTitleNewUser ;
			else ViewBag.Title = Piranha.Resources.Settings.EditTitleExistingUser ;

			if (ModelState.IsValid) {
				try {
					if (um.SaveAll()) {
						ModelState.Clear() ;
						ViewBag.Title = Piranha.Resources.Settings.EditTitleExistingUser ;
						SuccessMessage(Piranha.Resources.Settings.MessageUserSaved) ;
					} else ErrorMessage(Piranha.Resources.Settings.MessageUserNotSaved) ;
				} catch (Exception e) {
					ErrorMessage(e.ToString()) ;
				}
			}
			return View(@"~/Areas/Manager/Views/Settings/User.cshtml", um) ;
		}

		/// <summary>
		/// Deletes the specified user
		/// </summary>
		/// <param name="id">The user id</param>
		[Access(Function="ADMIN_USER")]
		public ActionResult DeleteUser(string id) {
			UserEditModel um = UserEditModel.GetById(new Guid(id)) ;
			
			ViewBag.SelectedTab = "users" ;
			if (um.DeleteAll())
				SuccessMessage(Piranha.Resources.Settings.MessageUserDeleted) ;
			else ErrorMessage(Piranha.Resources.Settings.MessageUserNotDeleted) ;
			
			return UserList() ;
		}

		/// <summary>
		/// Generates a new random password for the given user.
		/// </summary>
		/// <param name="id">The user id</param>
		[Access(Function="ADMIN_USER")]
		public ActionResult GeneratePassword(string id) {
			SysUserPassword password = SysUserPassword.GetSingle(new Guid(id)) ;
			string newpwd = SysUserPassword.GeneratePassword() ;

			password.Password = password.PasswordConfirm = newpwd ;
			password.Save() ;
			InformationMessage(Piranha.Resources.Settings.MessageNewPassword + newpwd) ;

			return User(id) ;
		}
		#endregion

		#region Group actions
		/// <summary>
		/// Gets the group list.
		/// </summary>
		[Access(Function="ADMIN_GROUP")]
        public ActionResult GroupList() {
            return View(@"~/Areas/Manager/Views/Settings/GroupList.cshtml", GroupListModel.Get());
        }

		/// <summary>
		/// Edits or creates a new group
		/// </summary>
		/// <param name="id">The group id</param>
		[Access(Function="ADMIN_GROUP")]
		public ActionResult Group(string id) {
			if (!String.IsNullOrEmpty(id)) {
				ViewBag.Title = Piranha.Resources.Settings.EditTitleExistingGroup ;
				return View(@"~/Areas/Manager/Views/Settings/Group.cshtml", GroupEditModel.GetById(new Guid(id))) ;
			} else {
				ViewBag.Title = Piranha.Resources.Settings.EditTitleNewGroup ;
				return View(@"~/Areas/Manager/Views/Settings/Group.cshtml", new GroupEditModel()) ;
			}
		}

		/// <summary>
		/// Saves the group
		/// </summary>
		/// <param name="gd">The model</param>
		[HttpPost()]
		[Access(Function="ADMIN_GROUP")]
		public ActionResult Group(GroupEditModel gm) {
			if (gm.Group.IsNew)
				ViewBag.Title = Piranha.Resources.Settings.EditTitleNewGroup ;
			else ViewBag.Title = Piranha.Resources.Settings.EditTitleExistingGroup ;

			if (ModelState.IsValid) {
				try {
					if (gm.SaveAll()) {
						ModelState.Clear() ;
						ViewBag.Title = Piranha.Resources.Settings.EditTitleExistingGroup ;
						SuccessMessage(Piranha.Resources.Settings.MessageGroupSaved) ;
					} else ErrorMessage(Piranha.Resources.Settings.MessageGroupNotSaved) ;
				} catch (Exception e) {
					ErrorMessage(e.ToString()) ;
				}
			}
			gm.Refresh() ;
			return View(@"~/Areas/Manager/Views/Settings/Group.cshtml", gm) ;
		}

		/// <summary>
		/// Deletes the specified group
		/// </summary>
		/// <param name="id">The group id</param>
		[Access(Function="ADMIN_GROUP")]
		public ActionResult DeleteGroup(string id) {
			GroupEditModel gm = GroupEditModel.GetById(new Guid(id)) ;
			
			ViewBag.SelectedTab = "groups" ;
			if (gm.DeleteAll())
				SuccessMessage(Piranha.Resources.Settings.MessageGroupDeleted) ;
			else ErrorMessage(Piranha.Resources.Settings.MessageGroupNotDeleted) ;
			
			return GroupList() ;
		}
		#endregion

		#region Access actions
		/// <summary>
		/// Gets the access list.
		/// </summary>
		[Access(Function="ADMIN_ACCESS")]
        public ActionResult AccessList() {
            return View(@"~/Areas/Manager/Views/Settings/AccessList.cshtml", AccessListModel.Get());
        }

		/// <summary>
		/// Edits or creates a new group
		/// </summary>
		/// <param name="id">The group id</param>
		[Access(Function="ADMIN_ACCESS")]
		public ActionResult Access(string id) {
			if (!String.IsNullOrEmpty(id)) {
				ViewBag.Title = Piranha.Resources.Settings.EditTitleExistingAccess ;
				return View(@"~/Areas/Manager/Views/Settings/Access.cshtml", AccessEditModel.GetById(new Guid(id))) ;
			} else {
				ViewBag.Title = Piranha.Resources.Settings.EditTitleNewAccess ;
				return View(@"~/Areas/Manager/Views/Settings/Access.cshtml", new AccessEditModel()) ;
			}
		}

		/// <summary>
		/// Saves the access
		/// </summary>
		/// <param name="gd">The model</param>
		[HttpPost()]
		[Access(Function="ADMIN_ACCESS")]
		public ActionResult Access(AccessEditModel am) {
			if (am.Access.IsNew)
				ViewBag.Title = Piranha.Resources.Settings.EditTitleNewAccess ;
			else ViewBag.Title = Piranha.Resources.Settings.EditTitleExistingAccess ;

			if (ModelState.IsValid) {
				try {
					if (am.SaveAll()) {
						ModelState.Clear() ;
						ViewBag.Title = Piranha.Resources.Settings.EditTitleExistingAccess ;
						SuccessMessage(Piranha.Resources.Settings.MessageAccessSaved) ;
					} else ErrorMessage(Piranha.Resources.Settings.MessageAccessNotSaved) ;
				} catch (Exception e) {
					ErrorMessage(e.ToString()) ;
				}
			}
			return View(@"~/Areas/Manager/Views/Settings/Access.cshtml", am) ;
		}

		/// <summary>
		/// Deletes the specified group
		/// </summary>
		/// <param name="id">The access id</param>
		[Access(Function="ADMIN_ACCESS")]
		public ActionResult DeleteAccess(string id) {
			AccessEditModel am = AccessEditModel.GetById(new Guid(id)) ;
			
			ViewBag.SelectedTab = "access" ;
			if (am.DeleteAll())
				SuccessMessage(Piranha.Resources.Settings.MessageAccessDeleted) ;
			else ErrorMessage(Piranha.Resources.Settings.MessageAccessNotDeleted) ;

			return AccessList() ;
		}
		#endregion

		#region Param actions
		/// <summary>
		/// Gets the param list.
		/// </summary>
		[Access(Function="ADMIN_PARAM")]
        public ActionResult ParamList() {
            return View(@"~/Areas/Manager/Views/Settings/ParamList.cshtml", ParamListModel.Get());
        }

		/// <summary>
		/// Edits or creates a new parameter
		/// </summary>
		/// <param name="id">Parameter id</param>
		[Access(Function="ADMIN_PARAM")]
		public ActionResult Param(string id) {
			if (!String.IsNullOrEmpty(id)) {
				ViewBag.Title = Piranha.Resources.Settings.EditTitleExistingParam ;
				return View(@"~/Areas/Manager/Views/Settings/Param.cshtml", ParamEditModel.GetById(new Guid(id))) ;
			} else {
				ViewBag.Title = Piranha.Resources.Settings.EditTitleNewParam ;
				return View(@"~/Areas/Manager/Views/Settings/Param.cshtml", new ParamEditModel()) ;
			}
		}

		/// <summary>
		/// Edits or creates a new parameter
		/// </summary>
		/// <param name="id">Parameter id</param>
		[HttpPost()]
		[Access(Function="ADMIN_PARAM")]
		public ActionResult Param(ParamEditModel pm) {
			if (pm.Param.IsNew)
				ViewBag.Title = Piranha.Resources.Settings.EditTitleNewParam ;
			else ViewBag.Title = Piranha.Resources.Settings.EditTitleExistingParam ;

			if (ModelState.IsValid) {
				try {
					if (pm.SaveAll()) {
						ModelState.Clear() ;
						ViewBag.Title = Piranha.Resources.Settings.EditTitleExistingParam ;
						SuccessMessage(Piranha.Resources.Settings.MessageParamSaved) ;
					} else ErrorMessage(Piranha.Resources.Settings.MessageParamNotSaved) ;
				} catch (Exception e) {
					ErrorMessage(e.ToString()) ;
				}
			}
			return View(@"~/Areas/Manager/Views/Settings/Param.cshtml", pm) ;
		}


		/// <summary>
		/// Deletes the specified param
		/// </summary>
		/// <param name="id">The param</param>
		[Access(Function="ADMIN_PARAM")]
		public ActionResult DeleteParam(string id) {
			ParamEditModel pm = ParamEditModel.GetById(new Guid(id)) ;
			
			ViewBag.SelectedTab = "params" ;
			if (pm.DeleteAll())
				SuccessMessage(Piranha.Resources.Settings.MessageParamDeleted) ;
			else ErrorMessage(Piranha.Resources.Settings.MessageParamNotDeleted) ;

			return ParamList() ;
		}
		#endregion
	}
}
