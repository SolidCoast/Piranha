using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Web;

namespace Piranha.Models.Manager.ContentModels
{
	public class UploadModel
	{
		#region Inner classes
		/// <summary>
		/// Definition for an uploaded file
		/// </summary>
		public class UploadedFile {
			/// <summary>
			/// Gets/sets the file name.
			/// </summary>
			public string Name { get ; set ; }

			/// <summary>
			/// Gets/sets the absolute path for the file.
			/// </summary>
			public string Path { get ; set ; }

			/// <summary>
			/// Gets/sets the file size.
			/// </summary>
			public long Size { get ; set ; }

			/// <summary>
			/// Gets/sets the file date.
			/// </summary>
			public DateTime Date { get ; set ; }
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets the uploaded files.
		/// </summary>
		public List<UploadedFile> Files { get ; private set ; }
		#endregion

		/// <summary>
		/// Default constructor. Creates a new model.
		/// </summary>
		public UploadModel() {
			Files = new List<UploadedFile>() ;
		}

		/// <summary>
		/// Gets the model for the currently available files
		/// </summary>
		/// <returns></returns>
		public static UploadModel Get() {
			UploadModel m = new UploadModel() ;
			DirectoryInfo dir = new DirectoryInfo(UploadDir()) ;

			foreach (FileInfo file in dir.GetFiles()) {
				if (file.Name.ToLower() != "index.html" && !file.Name.StartsWith("."))
					m.Files.Add(new UploadedFile() {
						Name = file.Name,
						Path = file.FullName,
						Size = file.Length,
						Date = file.LastWriteTime
					}) ;
			}
			return m ;
		}

		/// <summary>
		/// Gets the physical path for the upload directory.
		/// </summary>
		/// <returns></returns>
		private static string UploadDir() {
			return HttpContext.Current.Server.MapPath("~/App_Data/Uploads/") ;
		}
	}
}
