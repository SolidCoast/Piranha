using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

using Piranha.Models;

namespace Piranha.Web
{
	/// <summary>
	/// Class that handles client cache.
	/// </summary>
	public static class ClientCache
	{
		#region Properties
		/// <summary>
		/// Gets/sets the global last modification date.
		/// </summary>
		public static DateTime SiteLastModified {
			get {
				try {
					return DateTime.Parse(SysParam.GetByName("SITE_LAST_MODIFIED").Value) ;
				} catch {}
				return DateTime.MinValue ;
			}
		}
		#endregion

		/// <summary>
		/// Updates the global last modified date for the site.
		/// </summary>
		/// <param name="tx">Optional transaction</param>
		public static void SetSiteLastModified(IDbTransaction tx = null) {
			SysParam.Execute("UPDATE sysparam SET sysparam_value = @0 WHERE sysparam_name = @1", tx,
				DateTime.Now, "SITE_LAST_MODIFIED") ;
			SysParam.InvalidateParam("SITE_LAST_MODIFIED") ;
		}

		/// <summary>
		/// Checks request headers against the given etag and last modification data and
		/// sets the correct response headers. Returns weather the file is client cached 
		/// or should be loaded/rendered.
		/// </summary>
		/// <param name="context">The current context</param>
		/// <param name="id">The entity id</param>
		/// <param name="modified">Last nodification</param>
		/// <returns>If the file is cached</returns>
		public static bool HandleClientCache(HttpContext context, string id, DateTime modified, bool noexpire = false) {
#if !DEBUG
			// Get expire & maxage
			int expires = 30, maxage = 30 ;
			try {
				expires = Convert.ToInt32(SysParam.GetByName("CACHE_PUBLIC_EXPIRES").Value) ;
				maxage = Convert.ToInt32(SysParam.GetByName("CACHE_PUBLIC_MAXAGE").Value) ;
			} catch {}

			// Handle cache
			if (!context.Request.IsLocal && expires > 0) {
				try {
					modified = modified > SiteLastModified ? modified : SiteLastModified ;
				} catch {}
				string etag = GenerateETag(id, modified) ;

				context.Response.Cache.SetETag(etag) ;
				context.Response.Cache.SetLastModified(modified <= DateTime.Now ? modified : DateTime.Now) ;	
				context.Response.Cache.SetCacheability(System.Web.HttpCacheability.ServerAndPrivate) ;
				if (!noexpire) {
					context.Response.Cache.SetExpires(DateTime.Now.AddMinutes(expires)) ;
					context.Response.Cache.SetMaxAge(new TimeSpan(0, maxage, 0)) ;
				} else {
					context.Response.Cache.SetExpires(DateTime.Now) ;
				}
				if (IsCached(context, modified, etag)) {
					context.Response.StatusCode = 304 ;
					context.Response.SuppressContent = true ;
					context.Response.End() ;
					return true ;
				}
			} else {
				context.Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache) ;
			}
#else
			context.Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache) ;
#endif
			return false ;
		}

		/// <summary>
		/// Generates an unique entity tag.
		/// </summary>
		/// <param name="name">Object name</param>
		/// <param name="modified">Last modified date</param>
		/// <returns>The entity tag</returns>
		public static string GenerateETag(string name, DateTime modified) {
			UTF8Encoding encoder = new UTF8Encoding() ;
			MD5CryptoServiceProvider crypto = new MD5CryptoServiceProvider() ;

			string str = name + modified.ToLongTimeString() ;
			byte[] bts = crypto.ComputeHash(encoder.GetBytes(str)) ;
			return Convert.ToBase64String(bts, 0, bts.Length);
		}

		/// <summary>
		/// Check if the page is client cached.
		/// </summary>
		/// <param name="modified">Last modification date</param>
		/// <param name="entitytag">Entity tag</param>
		private static bool IsCached(HttpContext context, DateTime modified, string entitytag) {
			// Check If-None-Match
			string etag = context.Request.Headers["If-None-Match"] ;
			if (!String.IsNullOrEmpty(etag))
				if (etag == entitytag)
					return true ;

			// Check If-Modified-Since
			string mod = context.Request.Headers["If-Modified-Since"] ;
			if (!String.IsNullOrEmpty(mod))
				try {
					DateTime since ;
					if (DateTime.TryParse(mod, out since))
						return since >= modified ;
				} catch {}
			return false ;
		}
	}
}
