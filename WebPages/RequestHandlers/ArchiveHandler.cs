using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Piranha.WebPages.RequestHandlers
{
	public class ArchiveHandler : IRequestHandler
	{
		public void HandleRequest(System.Web.HttpContext context, params string[] args) {
			context.RewritePath("~/archive" + (args.Length > 0 ? "/" : "") + args.Implode("/"), false) ;
		}
	}
}
