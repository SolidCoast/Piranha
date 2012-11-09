using System;
using System.IO;
using System.Reflection;
using System.Web;

namespace Piranha.Web
{
	public class ResourceHandler : IHttpHandler
	{
		/// <summary>
		/// Gets weather this handler is reusable or not.
		/// </summary>
		public bool IsReusable { 
			get { return false ; } 
		}

		/// <summary>
		/// Processes the http request.
		/// </summary>
		/// <param name="context">The current context</param>
		public void ProcessRequest(HttpContext context) {
			ResourcePathProvider res = new ResourcePathProvider() ;

			if (res.FileExists(context.Request.Path)) {
				var file = res.GetFile(context.Request.Path) ;

				DateTime mod = new FileInfo(Assembly.GetExecutingAssembly().Location).LastWriteTime ;

				WriteFile(context, file.VirtualPath, file.Name, mod, file.Open()) ;
			} else if (File.Exists(context.Server.MapPath(context.Request.Path))) {
				FileInfo file = new FileInfo(context.Server.MapPath(context.Request.Path)) ;

				WriteFile(context, context.Request.Path, file.Name, file.LastWriteTime, file.Open(FileMode.Open)) ;
			} else {
				context.Response.StatusCode = 404 ;
			}
		}

		/// <summary>
		/// Writes to the output stream.
		/// </summary>
		/// <param name="context">The current http context</param>
		/// <param name="path">The virtual path</param>
		/// <param name="name">The filename</param>
		/// <param name="mod">The latest modification date</param>
		/// <param name="stream">The file stream</param>
		public void WriteFile(HttpContext context, string path, string name, DateTime mod, Stream stream) {
			if (!ClientCache.HandleClientCache(context, path, mod)) {
				if (name.EndsWith(".js")) {
					context.Response.ContentType = "text/javascript" ;
				} else if (name.EndsWith(".css")) {
					context.Response.ContentType = "text/css" ;
				} else if (name.EndsWith(".png")) {
					context.Response.ContentType = "image/png" ;
				} else if (name.EndsWith(".eot")) {
					context.Response.ContentType = "application/vnd.ms-fontobject" ;
				} else if (name.EndsWith(".ttf")) {
					context.Response.ContentType = "application/octet-stream" ;
				} else if (name.EndsWith(".svg")) {
					context.Response.ContentType = "image/svg+xml" ;
				} else if (name.EndsWith(".woff")) {
					context.Response.ContentType = "application/x-woff" ;
				}
				byte[] bytes = new byte[stream.Length] ;
				stream.Read(bytes, 0, Convert.ToInt32(stream.Length)) ;
#if DEBUG
				context.Response.BinaryWrite(bytes) ;
#else
				context.Response.BinaryWrite(bytes) ;
#endif
			}
			stream.Close() ;
		}
	}
}
