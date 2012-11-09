using System;
using System.IO;
using System.Reflection;
using System.Web;

using Piranha.Web;

namespace Piranha.Areas.Manager.Content.Js
{
	/// <summary>
	/// Summary description for Css
	/// </summary>
	public class Script : IHttpHandler
	{
		#region Members
		private const string resource = "Piranha.Areas.Manager.Content.Js.jquery.manager.js" ;
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

			if (!ClientCache.HandleClientCache(context, resource, mod)) {
				StreamReader io = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(resource)) ;
				context.Response.ContentType = "text/javascript" ;
#if DEBUG
				context.Response.Write(io.ReadToEnd()) ;
#else
				context.Response.Write(JavaScriptCompressor.Compress(io.ReadToEnd())) ;
#endif
				io.Close() ;
			}
		}
	}
}