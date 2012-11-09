using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

using Piranha.Data;

namespace Piranha.Models.Manager.ContentModels
{
	/// <summary>
	/// List model for the content view in the manager area.
	/// </summary>
	public class PopupModel
	{
		#region Properties
		/// <summary>
		/// Gets/sets the available content.
		/// </summary>
		public List<Piranha.Models.Content> Content { get ; set ; }

		/// <summary>
		/// Gets/sets the new content for uploading.
		/// </summary>
		public Piranha.Models.Content NewContent { get ; set ; }

		/// <summary>
		/// Gets/sets the available folders.
		/// </summary>
		public SelectList Folders { get ; set ; }

		/// <summary>
		/// Gets/sets the optional file.
		/// </summary>
		public HttpPostedFileBase UploadedFile { get ; set ; }

		/// <summary>
		/// Gets/sets the url to get the file from.
		/// </summary>
		[Display(Name="FromUrl", ResourceType=typeof(Piranha.Resources.Content))]
		public string FileUrl { get ; set ; }
		#endregion

		/// <summary>
		/// Default constructor. Creates a new model.
		/// </summary>
		public PopupModel() {
			Content = new List<Piranha.Models.Content>() ;
			NewContent = new Piranha.Models.Content() ;
			var folders = Piranha.Models.Content.GetFields("content_id, content_name", "content_folder=1", new Params() { OrderBy = "content_name" }) ;
			folders.Insert(0, new Piranha.Models.Content()) ;
			Folders = new SelectList(folders, "Id", "Name") ;
		}

		/// <summary>
		/// Gets all available content.
		/// </summary>
		/// <returns>A list of content records</returns>
		public static PopupModel Get(string id = "") {
			PopupModel lm = new PopupModel() ;

			if (!String.IsNullOrEmpty(id))
				lm.Content = Piranha.Models.Content.GetStructure(new Guid(id), true) ;
			else lm.Content = Piranha.Models.Content.GetStructure() ;

			return lm ;
		}
	}
}
