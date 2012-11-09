using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

using Piranha.Models;

namespace Piranha.WebPages.RequestHandlers
{
	public class UploadHandler : IRequestHandler
	{
		/// <summary>
		/// Handles the current request.
		/// </summary>
		/// <param name="context">The current context</param>
		/// <param name="args">Optional url arguments passed to the handler</param>
		public virtual void HandleRequest(HttpContext context, params string[] args) {
			if (args != null && args.Length > 0) {
				//
				// Uploaded content
				//
				Upload upload = Upload.GetSingle(new Guid(args[0])) ;

				if (upload != null) {
					int? width = null ;
					int? height = null ;

					if (args.Length > 1)
						width = Convert.ToInt32(args[1]) ;
					if (args.Length > 2)
						height = Convert.ToInt32(args[2]) ;

					if (height.HasValue)
						upload.GetMedia(context, width, height) ;
					else if (width.HasValue)
						upload.GetMedia(context, width) ;
					else upload.GetMedia(context) ;
				}
			}
		}
	}
}
