using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Web;

using Piranha.Web;

namespace Piranha.Areas.Manager.Content.Css
{
	/// <summary>
	/// Summary description for Css
	/// </summary>
	public class Style : IHttpHandler
	{
		#region Members
		private const string resource = "Piranha.Areas.Manager.Content.Css.Style.css" ;
		private const string theme    = "Piranha.Areas.Manager.Content.Css.Theme.css" ;
		#endregion

		#region Properties
		public bool IsReusable {
			get { return false ; }
		}
		#endregion

		/// <summary>
		/// Process the request
		/// </summary>
		public void ProcessRequest(HttpContext context) {
			DateTime mod = new FileInfo(Assembly.GetExecutingAssembly().Location).LastWriteTime ;
			if (File.Exists(context.Server.MapPath("~/Areas/Manager/Content/Css/Style.css"))) {
				FileInfo file = new FileInfo(context.Server.MapPath("~/Areas/Manager/Content/Css/Style.css")) ;
				mod = file.LastWriteTime > mod ? file.LastWriteTime : mod ;
			}

			if (!ClientCache.HandleClientCache(context, resource, mod)) {
				StreamReader io = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(resource)) ;
				context.Response.ContentType = "text/css" ;
#if DEBUG
				context.Response.Write(io.ReadToEnd()) ;
#else
				context.Response.Write(CssCompressor.Compress(io.ReadToEnd()).Replace("\n","")) ;
#endif
				io.Close() ;

				// Now apply standard theme
				if (ConfigurationManager.AppSettings["disable_manager_theme"] != "1") {
					io = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(theme)) ;
#if DEBUG
					context.Response.Write(io.ReadToEnd()) ;
#else
					context.Response.Write(CssCompressor.Compress(io.ReadToEnd()).Replace("\n","")) ;
#endif
					io.Close() ;
				}

				// Now check for application specific styles
				if (File.Exists(context.Server.MapPath("~/Areas/Manager/Content/Css/Style.css"))) {
					io = new StreamReader(context.Server.MapPath("~/Areas/Manager/Content/Css/Style.css")) ;
#if DEBUG
					context.Response.Write(io.ReadToEnd()) ;
#else
					context.Response.Write(CssCompressor.Compress(io.ReadToEnd()).Replace("\n","")) ;
#endif
					io.Close() ;
				}
			}
		}
	}
}