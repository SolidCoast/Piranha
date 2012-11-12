// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountController.cs" company="">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Piranha.Areas.Manager.Controllers
{
    using System.Diagnostics;
    using System.Reflection;
    using System.Web.Mvc;
    using System.Web.Security;

    using Piranha.Data;
    using Piranha.Models;
    using Piranha.Resources;
    using Piranha.WebPages;

    /// <summary>
    /// Login controller for the manager interface.
    /// </summary>
    public class AccountController : Controller
    {
        #region Public Methods and Operators

        /// <summary>
        /// Default action
        /// </summary>
        /// <returns>
        /// </returns>
        public ActionResult Index()
        {
            // Check for existing installation.
            try
            {
                if (Database.InstalledVersion < Database.CurrentVersion)
                {
                    return RedirectToAction("update", "install");
                }

                // Get current assembly version
                ViewBag.Version = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;

                // Check if user is logged in and has permissions to the manager
                if (User.Identity.IsAuthenticated && User.HasAccess("ADMIN"))
                {
                    Manager.MenuItem startpage = Manager.Menu[0].Items[0];
                    return RedirectToAction(startpage.Action, startpage.Controller);
                }

                return View("Index");
            }
            catch {}
            return RedirectToAction("index", "install");
        }

        /// <summary>
        /// Logs in the provided user.
        /// </summary>
        /// <param name="m">
        /// The model
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        public ActionResult Login(LoginModel m)
        {
            // Authenticate the user
            if (ModelState.IsValid)
            {
                SysUser user = SysUser.Authenticate(m.Login, m.Password);
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.Id.ToString(), m.RememberMe);
                    HttpContext.Session[PiranhaApp.USER] = user;

                    // Redirect after logon
                    Manager.MenuItem startpage = Manager.Menu[0].Items[0];

                    return RedirectToAction(startpage.Action, startpage.Controller);
                }
                else
                {
                    ViewBag.Message = Account.MessageLoginFailed;
                    ViewBag.MessageCss = "error";
                }
            }
            else
            {
                ViewBag.Message = Account.MessageLoginEmptyFields;
                ViewBag.MessageCss = string.Empty;
            }

            return Index();
        }

        /// <summary>
        /// Logs out the current user.
        /// </summary>
        /// <returns>
        /// </returns>
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();

            return RedirectToAction("index");
        }

        #endregion
    }
}