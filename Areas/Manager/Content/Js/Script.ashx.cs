// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Script.ashx.cs" company="">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Piranha.Areas.Manager.Content.Js
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Web;

    using Piranha.Web;

    /// <summary>
    /// Summary description for Css
    /// </summary>
    public class Script : IHttpHandler
    {
        #region Constants

        /// <summary>
        /// </summary>
        private const string resource = "Piranha.Areas.Manager.Content.Js.jquery.manager.js";
        #endregion

        #region Public Properties

        /// <summary>
        /// </summary>
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Process the request
        /// </summary>
        /// <param name="context">
        /// </param>
        public void ProcessRequest(HttpContext context)
        {
            DateTime mod = new FileInfo(Assembly.GetExecutingAssembly().Location).LastWriteTime;

            if (!ClientCache.HandleClientCache(context, resource, mod))
            {
                var io = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(resource));
                context.Response.ContentType = "text/javascript";
#if DEBUG
                context.Response.Write(io.ReadToEnd());
#else
				context.Response.Write(JavaScriptCompressor.Compress(io.ReadToEnd())) ;
#endif
                io.Close();
            }
        }

        #endregion
    }
}