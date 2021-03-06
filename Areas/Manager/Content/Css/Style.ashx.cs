﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Style.ashx.cs" company="">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Piranha.Areas.Manager.Content.Css
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Reflection;
    using System.Web;

    using Piranha.Web;

    /// <summary>
    /// Summary description for Css
    /// </summary>
    public class Style : IHttpHandler
    {
        #region Constants

        /// <summary>
        /// </summary>
        private const string resource = "Piranha.Areas.Manager.Content.Css.Style.css";

        /// <summary>
        /// </summary>
        private const string theme = "Piranha.Areas.Manager.Content.Css.Theme.css";
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
            if (File.Exists(context.Server.MapPath("~/Areas/Manager/Content/Css/Style.css")))
            {
                var file = new FileInfo(context.Server.MapPath("~/Areas/Manager/Content/Css/Style.css"));
                mod = file.LastWriteTime > mod ? file.LastWriteTime : mod;
            }

            if (!ClientCache.HandleClientCache(context, resource, mod))
            {
                var io = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(resource));
                context.Response.ContentType = "text/css";
#if DEBUG
                context.Response.Write(io.ReadToEnd());
#else
				context.Response.Write(CssCompressor.Compress(io.ReadToEnd()).Replace("\n", string.Empty)) ;
#endif
                io.Close();

                // Now apply standard theme
                if (ConfigurationManager.AppSettings["disable_manager_theme"] != "1")
                {
                    io = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(theme));
#if DEBUG
                    context.Response.Write(io.ReadToEnd());
#else
					context.Response.Write(CssCompressor.Compress(io.ReadToEnd()).Replace("\n", string.Empty)) ;
#endif
                    io.Close();
                }

                // Now check for application specific styles
                if (File.Exists(context.Server.MapPath("~/Areas/Manager/Content/Css/Style.css")))
                {
                    io = new StreamReader(context.Server.MapPath("~/Areas/Manager/Content/Css/Style.css"));
#if DEBUG
                    context.Response.Write(io.ReadToEnd());
#else
					context.Response.Write(CssCompressor.Compress(io.ReadToEnd()).Replace("\n", string.Empty)) ;
#endif
                    io.Close();
                }
            }
        }

        #endregion
    }
}