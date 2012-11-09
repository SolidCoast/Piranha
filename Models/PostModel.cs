using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

using Piranha.Data;
using Piranha.Extend;

namespace Piranha.Models
{
	public class PostModel
	{
		#region Properties
		/// <summary>
		/// Gets/sets the post.
		/// </summary>
		public IPost Post { get ; set ; }
	
		/// <summary>
		/// Gets/sets the post categories.
		/// </summary>
		public List<Category> Categories { get ; set ; }

		/// <summary>
		/// Gets the available properties.
		/// </summary>
		public dynamic Properties { get ; private set ; }

		/// <summary>
		/// Gets the available extensions.
		/// </summary>
		public dynamic Extensions { get ; private set ; }

		/// <summary>
		/// Gets the available attachments.
		/// </summary>
		public List<Content> Attachments { get ; set ; }

		/// <summary>
		/// Gets the current page.
		/// </summary>
		public Page Page { get { return null ; } }
		#endregion

		/// <summary>
		/// Default constructor. Creates an empty model.
		/// </summary>
		public PostModel() {
			Properties = new ExpandoObject() ;
			Extensions = new ExpandoObject() ;
			Attachments   = new List<Content>() ;
		}

		/// <summary>
		/// Gets the post model for the given id.
		/// </summary>
		/// <param name="id">The post id</param>
		/// <returns>The model</returns>
		public static PostModel GetById(Guid id) {
			PostModel m = new PostModel() {
				Post = Models.Post.GetSingle(id)
			} ;
			m.GetRelated() ;
			return m ;
		}

		/// <summary>
		/// Gets the post model for the given permalink.
		/// </summary>
		/// <param name="permalink">The permalink</param>
		/// <param name="draft">Weather to load the draft</param>
		/// <returns>The model</returns>
		public static PostModel GetByPermalink(string permalink, bool draft = false) {
			return GetByPermalink<PostModel>(permalink, draft) ;
		}

		/// <summary>
		/// Gets the post model for the given permalink.
		/// </summary>
		/// <param name="permalink">The permalink</param>
		/// <param name="draft">Weather to load the draft</param>
		/// <typeparam name="T">The model type</typeparam>
		/// <returns>The model</returns>
		public static T GetByPermalink<T>(string permalink, bool draft = false) where T : PostModel {
			T m = Activator.CreateInstance<T>() ;

			m.Post = Models.Post.GetByPermalink(permalink, draft) ;
			m.GetRelated() ;
			return m ;
		}

		/// <summary>
		/// Gets the page model for the given page.
		/// </summary>
		/// <param name="p">The page record</param>
		/// <returns>The model</returns>
		public static T Get<T>(Post p) where T : PostModel {
			T m = Activator.CreateInstance<T>() ;

			m.Post = p ;
			m.GetRelated() ;
			return m ;
		}

		/// <summary>
		/// Gets the related information for the post.
		/// </summary>
		private void GetRelated() {
			PostTemplate pt = PostTemplate.GetSingle(((Post)Post).TemplateId) ;

			// Get categories
			Categories = Category.GetByPostId(Post.Id) ;

			// Properties
			if (pt.Properties.Count > 0) {
				foreach (string str in pt.Properties)
					((IDictionary<string, object>)Properties).Add(str, "") ;
				Property.GetContentByParentId(Post.Id, ((Post)Post).IsDraft).ForEach(pr => {
					if (((IDictionary<string, object>)Properties).ContainsKey(pr.Name))
						((IDictionary<string, object>)Properties)[pr.Name] = pr.Value ;
				});
			}
			// Attachments
			((Models.Post)Post).Attachments.ForEach(a => Attachments.Add(Models.Content.GetSingle(a))) ;
			// Extensions
			foreach (var ext in ((Post)Post).GetExtensions()) {
				((IDictionary<string, object>)Extensions)[ExtensionManager.GetInternalIdByType(ext.Type)] = ext.Body ;
			}
		}
	}
}
