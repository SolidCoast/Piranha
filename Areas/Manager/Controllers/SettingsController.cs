// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingsController.cs" company="">
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
    using Piranha.Models.Manager.SettingModels;
    using Piranha.Resources;
    using Piranha.WebPages;

    /// <summary>
    /// Settings controller for the manager area.
    /// </summary>
    public class SettingsController : ManagerController
    {
        #region Public Methods and Operators

        /// <summary>
        /// Edits or creates a new group
        /// </summary>
        /// <param name="id">
        /// The group id
        /// </param>
        /// <returns>
        /// </returns>
        [Access(Function = "ADMIN_ACCESS")]
        public ActionResult Access(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                ViewBag.Title = Settings.EditTitleExistingAccess;
                return View(@"~/Areas/Manager/Views/Settings/Access.cshtml", AccessEditModel.GetById(new Guid(id)));
            }
            else
            {
                ViewBag.Title = Settings.EditTitleNewAccess;
                return View(@"~/Areas/Manager/Views/Settings/Access.cshtml", new AccessEditModel());
            }
        }

        /// <summary>
        /// Saves the access
        /// </summary>
        /// <param name="am">
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        [Access(Function = "ADMIN_ACCESS")]
        public ActionResult Access(AccessEditModel am)
        {
            if (am.Access.IsNew)
            {
                ViewBag.Title = Settings.EditTitleNewAccess;
            }
            else
            {
                ViewBag.Title = Settings.EditTitleExistingAccess;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (am.SaveAll())
                    {
                        ModelState.Clear();
                        ViewBag.Title = Settings.EditTitleExistingAccess;
                        SuccessMessage(Settings.MessageAccessSaved);
                    }
                    else
                    {
                        ErrorMessage(Settings.MessageAccessNotSaved);
                    }
                }
                catch (Exception e)
                {
                    ErrorMessage(e.ToString());
                }
            }

            return View(@"~/Areas/Manager/Views/Settings/Access.cshtml", am);
        }

        /// <summary>
        /// Gets the access list.
        /// </summary>
        /// <returns>
        /// </returns>
        [Access(Function = "ADMIN_ACCESS")]
        public ActionResult AccessList()
        {
            return View(@"~/Areas/Manager/Views/Settings/AccessList.cshtml", AccessListModel.Get());
        }

        /// <summary>
        /// Deletes the specified group
        /// </summary>
        /// <param name="id">
        /// The access id
        /// </param>
        /// <returns>
        /// </returns>
        [Access(Function = "ADMIN_ACCESS")]
        public ActionResult DeleteAccess(string id)
        {
            AccessEditModel am = AccessEditModel.GetById(new Guid(id));

            ViewBag.SelectedTab = "access";
            if (am.DeleteAll())
            {
                SuccessMessage(Settings.MessageAccessDeleted);
            }
            else
            {
                ErrorMessage(Settings.MessageAccessNotDeleted);
            }

            return AccessList();
        }

        /// <summary>
        /// Deletes the specified group
        /// </summary>
        /// <param name="id">
        /// The group id
        /// </param>
        /// <returns>
        /// </returns>
        [Access(Function = "ADMIN_GROUP")]
        public ActionResult DeleteGroup(string id)
        {
            GroupEditModel gm = GroupEditModel.GetById(new Guid(id));

            ViewBag.SelectedTab = "groups";
            if (gm.DeleteAll())
            {
                SuccessMessage(Settings.MessageGroupDeleted);
            }
            else
            {
                ErrorMessage(Settings.MessageGroupNotDeleted);
            }

            return GroupList();
        }

        /// <summary>
        /// Deletes the specified param
        /// </summary>
        /// <param name="id">
        /// The param
        /// </param>
        /// <returns>
        /// </returns>
        [Access(Function = "ADMIN_PARAM")]
        public ActionResult DeleteParam(string id)
        {
            ParamEditModel pm = ParamEditModel.GetById(new Guid(id));

            ViewBag.SelectedTab = "params";
            if (pm.DeleteAll())
            {
                SuccessMessage(Settings.MessageParamDeleted);
            }
            else
            {
                ErrorMessage(Settings.MessageParamNotDeleted);
            }

            return ParamList();
        }

        /// <summary>
        /// Deletes the specified user
        /// </summary>
        /// <param name="id">
        /// The user id
        /// </param>
        /// <returns>
        /// </returns>
        [Access(Function = "ADMIN_USER")]
        public ActionResult DeleteUser(string id)
        {
            UserEditModel um = UserEditModel.GetById(new Guid(id));

            ViewBag.SelectedTab = "users";
            if (um.DeleteAll())
            {
                SuccessMessage(Settings.MessageUserDeleted);
            }
            else
            {
                ErrorMessage(Settings.MessageUserNotDeleted);
            }

            return UserList();
        }

        /// <summary>
        /// Generates a new random password for the given user.
        /// </summary>
        /// <param name="id">
        /// The user id
        /// </param>
        /// <returns>
        /// </returns>
        [Access(Function = "ADMIN_USER")]
        public ActionResult GeneratePassword(string id)
        {
            SysUserPassword password = SysUserPassword.GetSingle(new Guid(id));
            string newpwd = SysUserPassword.GeneratePassword();

            password.Password = password.PasswordConfirm = newpwd;
            password.Save();
            InformationMessage(Settings.MessageNewPassword + newpwd);

            return User(id);
        }

        /// <summary>
        /// Edits or creates a new group
        /// </summary>
        /// <param name="id">
        /// The group id
        /// </param>
        /// <returns>
        /// </returns>
        [Access(Function = "ADMIN_GROUP")]
        public ActionResult Group(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                ViewBag.Title = Settings.EditTitleExistingGroup;
                return View(@"~/Areas/Manager/Views/Settings/Group.cshtml", GroupEditModel.GetById(new Guid(id)));
            }
            else
            {
                ViewBag.Title = Settings.EditTitleNewGroup;
                return View(@"~/Areas/Manager/Views/Settings/Group.cshtml", new GroupEditModel());
            }
        }

        /// <summary>
        /// Saves the group
        /// </summary>
        /// <param name="gm">
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        [Access(Function = "ADMIN_GROUP")]
        public ActionResult Group(GroupEditModel gm)
        {
            if (gm.Group.IsNew)
            {
                ViewBag.Title = Settings.EditTitleNewGroup;
            }
            else
            {
                ViewBag.Title = Settings.EditTitleExistingGroup;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (gm.SaveAll())
                    {
                        ModelState.Clear();
                        ViewBag.Title = Settings.EditTitleExistingGroup;
                        SuccessMessage(Settings.MessageGroupSaved);
                    }
                    else
                    {
                        ErrorMessage(Settings.MessageGroupNotSaved);
                    }
                }
                catch (Exception e)
                {
                    ErrorMessage(e.ToString());
                }
            }

            gm.Refresh();
            return View(@"~/Areas/Manager/Views/Settings/Group.cshtml", gm);
        }

        /// <summary>
        /// Gets the group list.
        /// </summary>
        /// <returns>
        /// </returns>
        [Access(Function = "ADMIN_GROUP")]
        public ActionResult GroupList()
        {
            return View(@"~/Areas/Manager/Views/Settings/GroupList.cshtml", GroupListModel.Get());
        }

        /// <summary>
        /// Edits or creates a new parameter
        /// </summary>
        /// <param name="id">
        /// Parameter id
        /// </param>
        /// <returns>
        /// </returns>
        [Access(Function = "ADMIN_PARAM")]
        public ActionResult Param(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                ViewBag.Title = Settings.EditTitleExistingParam;
                return View(@"~/Areas/Manager/Views/Settings/Param.cshtml", ParamEditModel.GetById(new Guid(id)));
            }
            else
            {
                ViewBag.Title = Settings.EditTitleNewParam;
                return View(@"~/Areas/Manager/Views/Settings/Param.cshtml", new ParamEditModel());
            }
        }

        /// <summary>
        /// Edits or creates a new parameter
        /// </summary>
        /// <param name="pm">
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        [Access(Function = "ADMIN_PARAM")]
        public ActionResult Param(ParamEditModel pm)
        {
            if (pm.Param.IsNew)
            {
                ViewBag.Title = Settings.EditTitleNewParam;
            }
            else
            {
                ViewBag.Title = Settings.EditTitleExistingParam;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (pm.SaveAll())
                    {
                        ModelState.Clear();
                        ViewBag.Title = Settings.EditTitleExistingParam;
                        SuccessMessage(Settings.MessageParamSaved);
                    }
                    else
                    {
                        ErrorMessage(Settings.MessageParamNotSaved);
                    }
                }
                catch (Exception e)
                {
                    ErrorMessage(e.ToString());
                }
            }

            return View(@"~/Areas/Manager/Views/Settings/Param.cshtml", pm);
        }

        /// <summary>
        /// Gets the param list.
        /// </summary>
        /// <returns>
        /// </returns>
        [Access(Function = "ADMIN_PARAM")]
        public ActionResult ParamList()
        {
            return View(@"~/Areas/Manager/Views/Settings/ParamList.cshtml", ParamListModel.Get());
        }

        /// <summary>
        /// Edits or creates a new user.
        /// </summary>
        /// <param name="id">
        /// The user id
        /// </param>
        /// <returns>
        /// </returns>
        [Access(Function = "ADMIN_USER")]
        public new ActionResult User(string id)
        {
            var m = new UserEditModel();

            if (!string.IsNullOrEmpty(id))
            {
                ViewBag.Title = Settings.EditTitleExistingUser;
                m = UserEditModel.GetById(new Guid(id));
            }
            else
            {
                ViewBag.Title = Settings.EditTitleNewUser;
            }

            // Executes the user list loaded hook, if registered
            if (Hooks.Manager.UserEditModelLoaded != null)
            {
                Hooks.Manager.UserEditModelLoaded(this, Manager.GetActiveMenuItem(), m);
            }

            return View(@"~/Areas/Manager/Views/Settings/User.cshtml", m);
        }

        /// <summary>
        /// Saves the model
        /// </summary>
        /// <param name="um">
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        [Access(Function = "ADMIN_USER")]
        public new ActionResult User(UserEditModel um)
        {
            if (um.User.IsNew)
            {
                ViewBag.Title = Settings.EditTitleNewUser;
            }
            else
            {
                ViewBag.Title = Settings.EditTitleExistingUser;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (um.SaveAll())
                    {
                        ModelState.Clear();
                        ViewBag.Title = Settings.EditTitleExistingUser;
                        SuccessMessage(Settings.MessageUserSaved);
                    }
                    else
                    {
                        ErrorMessage(Settings.MessageUserNotSaved);
                    }
                }
                catch (Exception e)
                {
                    ErrorMessage(e.ToString());
                }
            }

            return View(@"~/Areas/Manager/Views/Settings/User.cshtml", um);
        }

        /// <summary>
        /// Gets the list of all users.
        /// </summary>
        /// <returns>
        /// </returns>
        [Access(Function = "ADMIN_USER")]
        public ActionResult UserList()
        {
            UserListModel m = UserListModel.Get();
            ViewBag.Title = Settings.ListTitleUsers;

            // Executes the user list loaded hook, if registered
            if (Hooks.Manager.UserListModelLoaded != null)
            {
                Hooks.Manager.UserListModelLoaded(this, Manager.GetActiveMenuItem(), m);
            }

            return View(@"~/Areas/Manager/Views/Settings/UserList.cshtml", m);
        }

        #endregion
    }
}