using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.WebPages;

using Piranha.Models;

namespace Piranha.WebPages
{
	/// <summary>
	/// Base class for Razor syntax web pages.
	/// </summary>
	public abstract class BasePage : WebPage
	{
		#region Properties
		/// <summary>
		/// Gets the helper for the piranha methods.
		/// </summary>
		public PiranhaHelper UI { get ; private set ; }

		/// <summary>
		/// Gets the helper for the piranha site.
		/// </summary>
		public Web.SiteHelper Site { get ; private set ; }
		#endregion

		/// <summary>
		/// Default constructor. Creates a new page.
		/// </summary>
		public BasePage() : base() {
			UI = new PiranhaHelper(this, Html) ;
			Site = new Web.SiteHelper() ;
		}

		/// <summary>
		/// Initializes the web page.
		/// </summary>
		protected override void InitializePage() {
			base.InitializePage() ;

			ExecutePage() ;

			if (IsPost) {
			    if (Request.Form.AllKeys.Contains("piranha_form_action")) {
			        MethodInfo m = GetType().GetMethod(Request.Form["piranha_form_action"],
			            BindingFlags.Public|BindingFlags.NonPublic|BindingFlags.Instance|BindingFlags.IgnoreCase);
			        if (m != null) {
			            // Check for access rules
			            var access = m.GetCustomAttribute<AccessAttribute>(true) ;
			            if (access != null) {
			                if (!User.HasAccess(access.Function)) {
			                    SysParam param = SysParam.GetByName("LOGIN_PAGE") ;
			                    if (param != null)
			                        Response.Redirect(param.Value) ;
			                    else Response.Redirect("~/") ;
			                }
			            }
			            // Bind model
			            List<object> args = new List<object>() ;
			            foreach (var param in m.GetParameters())
			                args.Add(ModelBinder.BindModel(param.ParameterType, param.Name)) ;
			            m.Invoke(this, args.ToArray()) ;
			        }
			    }
			}
		}

		/// <summary>
		/// Executes any page specific server-side code.
		/// </summary>
		protected virtual void ExecutePage() {}
	}
}
