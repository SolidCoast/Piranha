using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Piranha.Data;

namespace Piranha.Models
{
	/// <summary>
	/// Archive model for the cms application.
	/// </summary>
	public class ArchiveModel
	{
		#region Properties
		/// <summary>
		/// Gets/sets the post archive.
		/// </summary>
		public List<PostArchive> PostArchive { get ; set ; }

		/// <summary>
		/// Gets/sets the posts in the current archive.
		/// </summary>
		public List<Post> Archive { get ; set ; }

		/// <summary>
		/// Gets/sets the content in the current archive.
		/// </summary>
		public List<Content> Content { get ; set ; }

		/// <summary>
		/// Gets/sets the archive category.
		/// </summary>
		public Category Category { get ; set ; }
		#endregion

		/// <summary>
		/// Default constructor. Creates a new archive model.
		/// </summary>
		public ArchiveModel() {
			Archive = new List<Post>() ;
			Content = new List<Content>() ;
			PostArchive = new List<PostArchive>() ;
		}

		/// <summary>
		/// Gets the archive model for the entire site.
		/// </summary>
		/// <returns>The model</returns>
		public static ArchiveModel Get() {
			return Get<ArchiveModel>() ;
		}

		/// <summary>
		/// Gets the archive model for the entire site.
		/// </summary>
		/// <typeparam name="T">The model type</typeparam>
		/// <returns>The model</returns>
		public static T Get<T>() where T : ArchiveModel {
			T m = Activator.CreateInstance<T>() ;
			m.GetRelated() ;
			return m ;
		}

		/// <summary>
		/// Gets the archive model for the given permalink.
		/// </summary>
		/// <param name="category">The category name</param>
		/// <returns>The model</returns>
		public static ArchiveModel GetByCategoryName(string category) {
			return GetByCategoryName<ArchiveModel>(category) ;
		}

		/// <summary>
		/// Gets the archive model for the given permalink.
		/// </summary>
		/// <param name="category">The category name</param>
		/// <typeparam name="T">The model type</typeparam>
		/// <returns>The model</returns>
		public static T GetByCategoryName<T>(string category) where T : ArchiveModel {
			T m = Activator.CreateInstance<T>() ;

			m.Category = Category.GetByPermalink(category) ;
			m.GetRelated() ;
			return m ;
		}

		/// <summary>
		/// Gets the related information for the archive.
		/// </summary>
		private void GetRelated() {
			// Get posts
			if (Category != null)
				Archive = Models.Post.GetByCategoryId(Category.Id) ;
			else Archive = Models.Post.Get(new Params() { OrderBy = "post_published DESC" }) ;

			// Get content
			if (Category != null)
				Content = Models.Content.GetByCategoryId(Category.Id) ;

			// Gets the archive
			if (Category != null)
				PostArchive = Models.PostArchive.Get() ;
			else PostArchive = Models.PostArchive.Get() ;
		}
	}
}
