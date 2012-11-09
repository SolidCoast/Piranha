using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using Piranha.Data;
using Piranha.Data.Updates;
using Piranha.Models;

namespace Piranha.Areas.Manager.Controllers
{
	public class InstallModel {
		[Required(ErrorMessage="Du måste välja ett användarnamn.")]
		public string UserLogin { get ; set ; }

		[Required(ErrorMessage="Du måste ange en e-post adress.")]
		public string UserEmail { get ; set ; }

		[Required(ErrorMessage="Du måste ange ett lösenord")]
		public string Password { get ; set ; }

		[Compare("Password", ErrorMessage="Lösenorden matchar inte.")]
		public string PasswordConfirm { get ; set ; }

		public string InstallType { get ; set ; }
	}

	/// <summary>
	/// Login controller for the manager interface.
	/// </summary>
    public class InstallController : Controller
    {
		/// <summary>
		/// Default action
		/// </summary>
        public ActionResult Index() {
			// Check for existing installation.
			try {
				if (Data.Database.InstalledVersion < Data.Database.CurrentVersion)
					return RedirectToAction("update", "install") ;
				return RedirectToAction("index", "account") ;
			} catch {}
			return View("Index");
        }

		/// <summary>
		/// Shows the update page.
		/// </summary>
		/// <returns></returns>
		public ActionResult Update() {
			if (Data.Database.InstalledVersion < Data.Database.CurrentVersion)
				return View("Update") ;
			return RedirectToAction("index", "account") ;
		}

		/// <summary>
		/// Updates the database.
		/// </summary>
		[HttpPost()]
		public ActionResult RunUpdate(LoginModel m) {
			// Authenticate the user
			if (ModelState.IsValid) {
				SysUser user = SysUser.Authenticate(m.Login, m.Password) ;
				if (user != null) {
					FormsAuthentication.SetAuthCookie(user.Id.ToString(), m.RememberMe) ;
					HttpContext.Session[PiranhaApp.USER] = user ;
					return RedirectToAction("ExecuteUpdate") ;
				} else {
					ViewBag.Message = @Piranha.Resources.Account.MessageLoginFailed ;
					ViewBag.MessageCss = "error" ;
					return Update() ;
				}
			} else {
				ViewBag.Message = @Piranha.Resources.Account.MessageLoginEmptyFields ;
				ViewBag.MessageCss = "" ;
				return Update() ;
			}
		}

		[HttpGet()]
		public ActionResult ExecuteUpdate() {
			if (User.Identity.IsAuthenticated && User.HasAccess("ADMIN")) {
				// Execute all incremental updates in a transaction.
				using (IDbTransaction tx = Database.OpenTransaction()) {
					for (int n = Data.Database.InstalledVersion + 1; n <= Data.Database.CurrentVersion; n++) {
						// Read embedded create script
						Stream str = Assembly.GetExecutingAssembly().GetManifestResourceStream(Database.ScriptRoot + ".Updates." +
							n.ToString() + ".sql") ;
						String sql = new StreamReader(str).ReadToEnd() ;
						str.Close() ;

						// Split statements and execute
						string[] stmts = sql.Split(new char[] { ';' }) ;
						foreach (string stmt in stmts) {
							if (!String.IsNullOrEmpty(stmt.Trim()))
								SysUser.Execute(stmt.Trim(), tx) ;
						}

						// Check for update class
						var utype = Type.GetType("Piranha.Data.Updates.Update" + n.ToString()) ;
						if (utype != null) {
							IUpdate update = (IUpdate)Activator.CreateInstance(utype) ;
							update.Execute(tx) ;
						}
					}
					// Now lets update the database version.
					SysUser.Execute("UPDATE sysparam SET sysparam_value = @0 WHERE sysparam_name = 'SITE_VERSION'", 
						tx, Data.Database.CurrentVersion) ;
					SysParam.InvalidateParam("SITE_VERSION") ;
					tx.Commit() ;
				}
				return RedirectToAction("index", "account") ;
			} else return RedirectToAction("update") ;
		}

		/// <summary>
		/// Creates a new site installation.
		/// </summary>
		/// <param name="m">The model</param>
		[HttpPost()]
		public ActionResult Create(InstallModel m) {
			if (m.InstallType == "SCHEMA" || ModelState.IsValid) {
				// Read embedded create script
				Stream str = Assembly.GetExecutingAssembly().GetManifestResourceStream(Database.ScriptRoot + ".Create.sql") ;
				String sql = new StreamReader(str).ReadToEnd() ;
				str.Close() ;

				// Read embedded data script
				str = Assembly.GetExecutingAssembly().GetManifestResourceStream(Database.ScriptRoot + ".Data.sql") ;
				String data = new StreamReader(str).ReadToEnd() ;
				str.Close() ;

				// Split statements and execute
				string[] stmts = sql.Split(new char[] { ';' }) ;
				using (IDbTransaction tx = Database.OpenTransaction()) {
					// Create database from script
					foreach (string stmt in stmts) {
						if (!String.IsNullOrEmpty(stmt.Trim()))
							SysUser.Execute(stmt, tx) ;
					}
					tx.Commit() ;
				}

				if (m.InstallType.ToUpper() == "FULL") {
					// Split statements and execute
					stmts = data.Split(new char[] { ';' }) ;
					using (IDbTransaction tx = Database.OpenTransaction()) {
						// Create user
						SysUser usr = new SysUser() {
							Login = m.UserLogin,
							Email = m.UserEmail,
							GroupId = new Guid("7c536b66-d292-4369-8f37-948b32229b83"),
							CreatedBy = new Guid("ca19d4e7-92f0-42f6-926a-68413bbdafbc"),
							UpdatedBy = new Guid("ca19d4e7-92f0-42f6-926a-68413bbdafbc"),
							Created = DateTime.Now,
							Updated = DateTime.Now
						} ;
						usr.Save(tx) ;

						// Create user password
						SysUserPassword pwd = new SysUserPassword() {
							Id = usr.Id,
							Password = m.Password,
							IsNew = false
						} ;
						pwd.Save(tx) ;

						// Create default data
						foreach (string stmt in stmts) {
							if (!String.IsNullOrEmpty(stmt.Trim()))
								SysUser.Execute(stmt, tx) ;
						}		
						tx.Commit() ;
					}	
				}
				return RedirectToAction("index", "account") ;
			}
			return Index() ;
		}
    }
}
