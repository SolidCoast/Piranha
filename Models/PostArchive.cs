using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Piranha.Data;
using Piranha.WebPages;

namespace Piranha.Models
{
	/// <summary>
	/// Record for the post archive.
	/// </summary>
	[Table(Name="post")]
	public class PostArchive : ActiveRecord<PostArchive>
	{
		#region Members
		private static string Select = 
			"SELECT	" +
			"CONVERT(INT, SUBSTRING(CONVERT(NCHAR(10), post_published, 120), 1, 4)) AS [Year]," +
			"CONVERT(INT, SUBSTRING(CONVERT(NCHAR(10), post_published, 120), 6, 2)) AS [Month]," +
			"COUNT(*) AS [Count] " +
			"FROM post " +
			"WHERE post_draft = 0 {0} " +
			"GROUP BY SUBSTRING(CONVERT(NCHAR(10), post_published, 120), 1, 4)," +
			"SUBSTRING(CONVERT(NCHAR(10), post_published, 120), 6, 2)" ;
		#endregion

		#region Fields
		/// <summary>
		/// Gets/sets the year.
		/// </summary>
		[Column(ReadOnly=true)]
		public int Year { get ; set ; }

		/// <summary>
		/// Gets/sets the month.
		/// </summary>
		[Column(ReadOnly=true)]
		public int Month { get ; set ; }

		/// <summary>
		/// Gets/sets the article count.
		/// </summary>
		[Column(ReadOnly=true)]
		public int Count { get ; set ; }
		#endregion

		#region Properties
		/// <summary>
		/// Gets/sets the date representation of the current archive record.
		/// </summary>
		private DateTime Date { get ; set ; }

		/// <summary>
		/// Gets name of the month.
		/// </summary>
		public string MonthName {
			get { return CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(Date.ToString("MMMM")) ; }
		}
		#endregion

		#region Static accessors
		/// <summary>
		/// Gets the current post archive.
		/// </summary>
		/// <returns>The archive</returns>
		public static List<PostArchive> Get() {
			return GetByTemplateId(Guid.Empty) ;
		}

		/// <summary>
		/// Gets the current post archive for the given post type.
		/// </summary>
		/// <param name="id">The template id</param>
		/// <returns>The archive</returns>
		public static List<PostArchive> GetByTemplateId(Guid id) {
			List<PostArchive> archive = null ;

			string where = id != Guid.Empty ?
				"AND post_template_id = @0 " : "" ;
			if (id != Guid.Empty)
				archive = Query(String.Format(Select, where), id) ;
			archive = Query(String.Format(Select, "")) ;

			archive.ForEach(a => a.Date = new DateTime(a.Year, a.Month, 1)) ;

			return archive ;
		}
		#endregion
	}
}
