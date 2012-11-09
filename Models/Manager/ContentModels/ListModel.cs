using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Piranha.Data;

namespace Piranha.Models.Manager.ContentModels
{
	/// <summary>
	/// List model for the content view in the manager area.
	/// </summary>
	public class ListModel
	{
		#region Properties
		/// <summary>
		/// Gets/sets the available content.
		/// </summary>
		public List<Piranha.Models.Content> Content { get ; set ; }
		#endregion

		/// <summary>
		/// Default constructor. Creates a new model.
		/// </summary>
		public ListModel() {
			Content = new List<Piranha.Models.Content>() ;
		}

		/// <summary>
		/// Gets all available content.
		/// </summary>
		/// <returns>A list of content records</returns>
		public static ListModel Get() {
			ListModel lm = new ListModel() ;
			lm.Content = GetStructure() ;
			lm.Content.AddRange(Piranha.Models.Content.Get("content_folder = 0 AND content_parent_id IS NULL", 
				new Params() { OrderBy = "COALESCE(content_name, content_filename) ASC" })) ;
			return lm ;
		}

		/// <summary>
		/// Gets the recursive folder structure for the available content.
		/// </summary>
		/// <returns></returns>
		private static List<Content> GetStructure() {
			var content = Piranha.Models.Content.Get("content_folder = 1 OR content_parent_id IS NOT NULL",
				new Params() { OrderBy = "content_parent_id, COALESCE(content_name, content_filename)" }) ;
			return SortStructure(content, Guid.Empty) ;
		}

		/// <summary>
		/// Sorts the given list into a recursive structure.
		/// </summary>
		/// <param name="content">The available content</param>
		/// <param name="parentid">The current parent id</param>
		/// <returns>The sorted structure</returns>
		private static List<Content> SortStructure(List<Content> content, Guid parentid) {
			var ret = content.Where(c => c.ParentId == parentid && c.IsFolder).ToList() ;
			ret.AddRange(content.Where(c => c.ParentId == parentid && !c.IsFolder)) ;

			foreach (var c in ret)
				if (c.IsFolder)
					c.ChildContent = SortStructure(content, c.Id) ;
			return ret ;
		}
	}
}
