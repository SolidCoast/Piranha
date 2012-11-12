// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ManagerController.cs" company="">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Piranha.Areas.Manager.Controllers
{
    using System.Reflection;
    using System.Web.Mvc;

    /// <summary>
    /// Controller for the manager area with built in access control.
    /// </summary>
    public abstract class ManagerController : Controller
    {
        #region Methods

        /// <summary>
        /// Adds an error message to the current view.
        /// </summary>
        /// <param name="msg">
        /// </param>
        protected void ErrorMessage(string msg)
        {
            ViewBag.MessageCss = "error";
            ViewBag.Message = msg;
        }

        /// <summary>
        /// Adds an information message to the current view.
        /// </summary>
        /// <param name="msg">
        /// </param>
        protected void InformationMessage(string msg)
        {
            ViewBag.MessageCss = string.Empty;
            ViewBag.Message = msg;
        }

        /// <summary>
        /// Do additional security checks for the manager area.
        /// </summary>
        /// <param name="filterContext">
        /// </param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (User.Identity.IsAuthenticated && User.HasAccess("ADMIN"))
            {
                // Get methodinfo for current action.
                MethodInfo m = null;

                try
                {
                    m = GetType().GetMethod(filterContext.ActionDescriptor.ActionName);
                }
                catch
                {
                    // If this fails we have multiple actions with the same name. We'll have to try and
                    // match it on FormMethod.
                    GetType().GetMethods().Each((i, method) =>{
                        if (method.Name == filterContext.ActionDescriptor.ActionName)
                        {
                            if (Request.HttpMethod == "POST")
                            {
                                if (PiranhaApp.GetCustomAttribute<HttpPostAttribute>(method, true) != null)
                                {
                                    m = method;
                                }
                            }
                            else if (Request.HttpMethod == "GET")
                            {
                                if (PiranhaApp.GetCustomAttribute<HttpGetAttribute>(method, true) != null || PiranhaApp.GetCustomAttribute<HttpPostAttribute>(method, true) == null)
                                {
                                    m = method;
                                }
                            }
                        }
                    });
                }

                if (m != null)
                {
                    var attr = PiranhaApp.GetCustomAttribute<AccessAttribute>(m, true);
                    if (attr != null)
                    {
                        if (!User.HasAccess(attr.Function))
                        {
                            filterContext.Result = RedirectToAction("index", "account");
                        }
                    }
                }

                base.OnActionExecuting(filterContext);
            }
            else
            {
                filterContext.Result = RedirectToAction("index", "account");
            }
        }

        /// <summary>
        /// Adds a success message to the current view.
        /// </summary>
        /// <param name="msg">
        /// The message
        /// </param>
        protected void SuccessMessage(string msg)
        {
            ViewBag.MessageCss = "success";
            ViewBag.Message = msg;
        }

        #endregion
    }
}