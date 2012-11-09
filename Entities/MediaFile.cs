using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

using Piranha.Web;

namespace Piranha.Models
{
	/// <summary>
	/// Abstract base class for all media files.
	/// </summary>
	/// <typeparam name="T">The entity type</typeparam>
	public abstract class MediaFile<T> : PiranhaRecord<T>
	{
		#region Members
		protected string BasePath = "" ;
		protected string CachePath = "" ;

		private static bool VerifiedPaths = false ;
		#endregion

		#region Fields
		/// <summary>
		/// Gets/sets the filename.
		/// </summary>
		public abstract string Filename { get ; set ; }

		/// <summary>
		/// Gets/sets the type of the media file.
		/// </summary>
		public abstract string Type { get ; set ; }
		#endregion

		#region Properties
		/// <summary>
		/// Gets the virtual path for the media file.
		/// </summary>
		public string VirtualPath { 
			get { return BasePath + Id ; }
		}

		/// <summary>
		/// Gets the virtual path for the cached media file.
		/// </summary>
		public string VirtualCachePath {
			get { return CachePath + Id ; }
		}

		/// <summary>
		/// Gets the physical path for the media file.
		/// </summary>
		public string PhysicalPath {
			get { return HttpContext.Current.Server.MapPath(VirtualPath) ; }
		}

		/// <summary>
		/// Gets the physical path for the cached media file.
		/// </summary>
		public string PhysicalCachePath {
			get { return HttpContext.Current.Server.MapPath(VirtualCachePath) ; }
		}
		#endregion

		/// <summary>
		/// Default constructor. Creates a media file with the given base path.
		/// </summary>
		/// <param name="basePath">The virtual base path.</param>
		/// <param name="cachePath">The virtual cache path for resized media.</param>
		protected MediaFile(string basePath, string cachePath) : base() {
			BasePath = basePath + (!basePath.EndsWith("/") ? "/" : "") ;
			CachePath = cachePath + (!cachePath.EndsWith("/") ? "/" : "") ;

			if (!VerifiedPaths) {
				// Verify paths
				if (!Directory.Exists(HttpContext.Current.Server.MapPath(BasePath)))
					Directory.CreateDirectory(HttpContext.Current.Server.MapPath(BasePath)) ;
				if (!Directory.Exists(HttpContext.Current.Server.MapPath(CachePath)))
					Directory.CreateDirectory(HttpContext.Current.Server.MapPath(CachePath)) ;

				VerifiedPaths = true ;
			}
		}

		/// <summary>
		/// Gets the physical media related to the content record and writes it to
		/// the given http response.
		/// </summary>
		/// <param name="response">The http response</param>
		public void GetMedia(HttpContext context, int? width = null, int? height = null) {
			if (!ClientCache.HandleClientCache(context, Id.ToString(), Updated)) {
				Image img = null ;

				try {
					// Try to create image from file.
					img = Image.FromFile(PhysicalPath) ;
				} catch {}

				if (img != null) {
					width = width.HasValue && width.Value < img.Width ? width : img.Width ;
					if (!height.HasValue)
						height = Convert.ToInt32(((double)width / img.Width) * img.Height) ;

					if (File.Exists(GetCachePath(width.Value, height.Value))) {
						// Return generated & cached resized image
						WriteFile(context.Response, GetCachePath(width.Value, height.Value)) ;
					} else if (File.Exists(PhysicalPath)) {
						int orgWidth = img.Width, orgHeight = img.Height ;

						img = Drawing.ImageUtils.Resize(img, width.Value, height.Value) ;
						if (img.Width != orgWidth || img.Height != orgHeight)
							img.Save(GetCachePath(width.Value, height.Value), img.RawFormat) ;

						WriteFile(context.Response, GetCachePath(width.Value, height.Value)) ;
					}
				}
				WriteFile(context.Response, PhysicalPath) ;
			}
		}

		/// <summary>
		/// Deletes the media file record and all related files.
		/// </summary>
		/// <param name="tx">Optional database transaction</param>
		/// <returns>Weather the action was a success</returns>
		public override bool Delete(System.Data.IDbTransaction tx = null) {
			if (base.Delete(tx)) {
				// Delete original file
				if (File.Exists(PhysicalPath))
					File.Delete(PhysicalPath) ;

				// Delete Cache
				DeleteCache() ;
				
				return true;
			}
			return false;
		}

		/// <summary>
		/// Deletes all cached versions of the media file.
		/// </summary>
		public void DeleteCache() {
			DirectoryInfo dir = new DirectoryInfo(CachePath) ;

			foreach (FileInfo file in dir.GetFiles(Id.ToString() + "*")) 
				file.Delete() ;
		}

		#region Protected methods
		/// <summary>
		/// Gets the physical path for the cached file with the given dimensions.
		/// </summary>
		/// <param name="width">The image width</param>
		/// <param name="height">The image height</param>
		/// <returns>The virtual path</returns>
		protected string GetCachePath(int width, int height) {
			return PhysicalCachePath + "-" + width.ToString() + "x" + height.ToString() ;
		}

		/// <summary>
		/// Gets the physical path for the cached thumbnail with the given size.
		/// </summary>
		/// <param name="size"></param>
		/// <returns></returns>
		protected string GetCacheThumbPath(int size) {
			return PhysicalCachePath + "-" + size.ToString() ;
		}

		/// <summary>
		/// Writes the given file to the http response
		/// </summary>
		/// <param name="response"></param>
		/// <param name="path"></param>
		protected void WriteFile(HttpResponse response, string path) {
			if (File.Exists(path)) {
				response.StatusCode = 200 ;
				response.ContentType = Type ;
				response.WriteFile(path) ;
				response.End() ;
			} else {
				response.StatusCode = 404 ;
			}
		}
		#endregion
	}
}