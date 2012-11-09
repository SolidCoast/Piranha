using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;

namespace Piranha.Web
{
	/// <summary>
	/// Gravatar helper.
	/// </summary>
	public class GravatarHelper {
		/// <summary>
		/// Gets the image URL for the gravatar with the given email
		/// </summary>
		/// <param name="email">The gravatar email</param>
		/// <param name="size">Optional size</param>
		/// <returns>The image URL</returns>
		public IHtmlString Image(string email, int size = 0) {
			string hash = FormsAuthentication.HashPasswordForStoringInConfigFile(email.Trim().ToLower(), "MD5") ;
			return new HtmlString("http://www.gravatar.com/avatar/" + hash.ToLower() +
				(size > 0 ? "?s=" + size : "")) ;
		}
	}
}
