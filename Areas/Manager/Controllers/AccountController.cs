using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using Piranha.Models;

namespace Piranha.Areas.Manager.Controllers
{
	/// <summary>
	/// Login controller for the manager interface.
	/// </summary>
    public class AccountController : Controller
    {
		/// <summary>
		/// Default action
		/// </summary>
        public ActionResult Index()
        {
			// Check for existing installation.
			try {
				if (Data.Database.InstalledVersion < Data.Database.CurrentVersion)
					return RedirectToAction("update", "install") ;

				// Get current assembly version
				ViewBag.Version = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion ;

				// Check if user is logged in and has permissions to the manager
				if (User.Identity.IsAuthenticated && User.HasAccess("ADMIN")) {
					var startpage = WebPages.Manager.Menu[0].Items[0] ;
					return RedirectToAction(startpage.Action, startpage.Controller) ;
				}
	            return View("Index") ;
			} catch {}
			return RedirectToAction("index", "install") ;
        }

		/// <summary>
		/// Logs in the provided user.
		/// </summary>
		/// <param name="m">The model</param>
		[HttpPost()]
		public ActionResult Login(LoginModel m) {
			// Authenticate the user
			if (ModelState.IsValid) {
				SysUser user = SysUser.Authenticate(m.Login, m.Password) ;
				if (user != null) {
					FormsAuthentication.SetAuthCookie(user.Id.ToString(), m.RememberMe) ;
					HttpContext.Session[PiranhaApp.USER] = user ;

					// Redirect after logon
					var startpage = WebPages.Manager.Menu[0].Items[0] ;

					return RedirectToAction(startpage.Action, startpage.Controller) ;
				} else {
					ViewBag.Message = @Piranha.Resources.Account.MessageLoginFailed ;
					ViewBag.MessageCss = "error" ;
				}
			} else {
				ViewBag.Message = @Piranha.Resources.Account.MessageLoginEmptyFields ;
				ViewBag.MessageCss = "" ;
			}
			return Index() ;
		}

		/// <summary>
		/// Logs out the current user.
		/// </summary>
		public ActionResult Logout() {
			FormsAuthentication.SignOut() ;
			Session.Clear() ;
			Session.Abandon() ;

			return RedirectToAction("index") ;
		}
    }
}
