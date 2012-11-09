using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

using Piranha.Data;

namespace Piranha.Models.Manager.ContentModels
{
	/// <summary>
	/// Edit model for the content record.
	/// </summary>
	public class EditModel
	{
		#region Properties
		/// <summary>
		/// Gets/sets the content record.
		/// </summary>
		public Models.Content Content { get ; set ; }

		/// <summary>
		/// Gets/sets the categories associated with the post.
		/// </summary>
		public List<Guid> ContentCategories { get ; set ; }

		/// <summary>
		/// Gets/sets the available categories.
		/// </summary>
		public MultiSelectList Categories { get ; set ; }

		/// <summary>
		/// Gets/sets the available folders.
		/// </summary>
		public IList<Placement> Folders { get ; set ; }

		/// <summary>
		/// Gets/sets the available extensions.
		/// </summary>
		public List<Extension> Extensions { get ; set ; }

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
		public EditModel() : this(false, Guid.Empty) {}

		/// <summary>
		/// Default constructor. Creates a new model.
		/// </summary>
		/// <param name="isfolder">Weather this is a folder or not.</param>
		public EditModel(bool isfolder, Guid parentid) {
			Content = new Piranha.Models.Content() { IsFolder = isfolder, ParentId = parentid } ;
			ContentCategories = new List<Guid>() ;
			Categories = new MultiSelectList(Category.GetFields("category_id, category_name", 
				new Params() { OrderBy = "category_name" }), "Id", "Name") ;
			var folders = Content.GetFields("content_id, content_name", "content_folder=1", new Params() { OrderBy = "content_name" }) ;
			folders.Insert(0, new Content()) ;
			Extensions = Content.GetExtensions() ;
			Folders = SortFolders(Content.GetFolderStructure()) ;
			Folders.Insert(0, new Placement() { Text = "", Value = Guid.Empty }) ;
		}

		/// <summary>
		/// Gets the edit model for the content with the given id.
		/// </summary>
		/// <param name="id">The content id</param>
		/// <returns>The model</returns>
		public static EditModel GetById(Guid id) {
			EditModel em = new EditModel() ;
			em.Content = Piranha.Models.Content.GetSingle(id) ;
			Relation.GetFieldsByDataId("relation_related_id", id, false).ForEach(r => em.ContentCategories.Add(r.RelatedId)) ;
			em.Categories = new MultiSelectList(Category.GetFields("category_id, category_name", 
				new Params() { OrderBy = "category_name" }), "Id", "Name", em.ContentCategories) ;
			var folders = Content.GetFields("content_id, content_name", "content_folder=1 AND content_id != @0", id, new Params() { OrderBy = "content_name" }) ;
			folders.Insert(0, new Content()) ;
			em.Extensions = em.Content.GetExtensions() ;

			return em ;
		}

		/// <summary>
		/// Saves the edit model.
		/// </summary>
		public bool SaveAll() {
			var context = HttpContext.Current ;
			var hasfile = UploadedFile != null ;
			byte[] data = null ;
			WebClient web = new WebClient() ;


			if (!hasfile && !String.IsNullOrEmpty(FileUrl))
				data = web.DownloadData(FileUrl) ;

			if (hasfile || data != null) {
				// Check if this is an image
				try {
					Image img = null ;

					if (hasfile) {
						img = Image.FromStream(UploadedFile.InputStream) ;
					} else {
						MemoryStream mem = new MemoryStream(data) ;
						img = Image.FromStream(mem) ;
					}

					// Image img = Image.FromStream(UploadedFile.InputStream) ;
					try {
						// Resize the image according to image max width
						int max = Convert.ToInt32(SysParam.GetByName("IMAGE_MAX_WIDTH").Value) ;
						if (max > 0)
							img = Drawing.ImageUtils.Resize(img, max) ;
					} catch {}
					Content.IsImage = true ;
					Content.Width = img.Width ;
					Content.Height = img.Height ;
				} catch {
					Content.IsImage = false ;
				}
				if (hasfile) {
					Content.Filename = UploadedFile.FileName ;
					Content.Type = UploadedFile.ContentType ;
					Content.Size = UploadedFile.ContentLength ;
				} else {
					Content.Filename = FileUrl.Substring(FileUrl.LastIndexOf('/') + 1) ;
					Content.Type = web.ResponseHeaders["Content-Type"] ;
					Content.Size = Convert.ToInt32(web.ResponseHeaders["Content-Length"]) ;
				}
			}

			if (Content.Save()) {
				// Save related information
				Relation.DeleteByDataId(Content.Id) ;
				List<Relation> relations = new List<Relation>() ;
				ContentCategories.ForEach(c => relations.Add(new Relation() { 
					DataId = Content.Id, RelatedId = c, IsDraft = false, Type = Relation.RelationType.CONTENTCATEGORY })
					) ;
				relations.ForEach(r => r.Save()) ;

				// Save extensions
				foreach (var ext in Extensions) {
					ext.ParentId = Content.Id ;
					ext.Save() ;
				}

				// Save the physical file
				if (hasfile || data != null) {
					string path = context.Server.MapPath("~/App_Data/content") ;
					if (File.Exists(Content.PhysicalPath)) {
						File.Delete(Content.PhysicalPath) ;
						Content.DeleteCache() ;
					}
					if (hasfile) {
						UploadedFile.SaveAs(Content.PhysicalPath) ;
					} else {
						FileStream writer = new FileStream(Content.PhysicalPath, FileMode.Create) ;
						BinaryWriter binary = new BinaryWriter(writer) ;
						binary.Write(data) ;
						binary.Flush() ;
						binary.Close() ;
					}
				}
				// Reset file url
				FileUrl = "" ;

				// Delete possible old thumbnails
				Content.DeleteCache() ;

				return true ;
			}
			return false ;
		}

		/// <summary>
		/// Deletes the specified content and its related file.
		/// </summary>
		public bool DeleteAll() {
			using (IDbTransaction tx = Database.OpenTransaction()) {
				try {
					File.Delete(HttpContext.Current.Server.MapPath("~/App_Data/Content/" + Content.Id)) ;
					Content.Delete(tx) ;
					tx.Commit() ;
					return true ;
				} catch { tx.Rollback() ; }
			}
			return false ;
		}

		/// <summary>
		/// Refreshes the current object.
		/// </summary>
		public void Refresh() {
			if (!Content.IsNew) {
				Relation.GetFieldsByDataId("relation_related_id", Content.Id).ForEach(r => ContentCategories.Add(r.RelatedId)) ;
				Categories = new MultiSelectList(Category.GetFields("category_id, category_name", 
					new Params() { OrderBy = "category_name" }), "Id", "Name", ContentCategories) ;
			}
		}

		#region Private methods
		/// <summary>
		/// Flattens the folder structure.
		/// </summary>
		/// <param name="media"></param>
		/// <param name="level"></param>
		/// <returns></returns>
		private List<Placement> SortFolders(List<Content> media, int level = 1) {
			var ret = new List<Placement>() ;

			foreach (var m in media) {
				var prefix = "" ;
				for (int n = 1; n < level; n++)
					prefix += "&nbsp;&nbsp;&nbsp;" ;
				ret.Add(new Placement() {
					Text = prefix + m.Name,
					Value = m.Id,
					Level = level
				}) ;
				ret.AddRange(SortFolders(m.ChildContent, level + 1)) ;
			}
			return ret ;
		}
		#endregion
	}
}
