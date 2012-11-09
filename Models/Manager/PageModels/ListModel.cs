﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

using Piranha.Data;

namespace Piranha.Models.Manager.PageModels
{
	/// <summary>
	/// Page list model for the manager area.
	/// </summary>
	public class ListModel 
	{
		#region Properties
		/// <summary>
		/// Gets/sets the pages.
		/// </summary>
		public List<Sitemap> Pages { get ; set ; }

		/// <summary>
		/// Gets/sets the sitemap.
		/// </summary>
		public List<Sitemap> SiteMap { get ; set ; }

		/// <summary>
		/// Gets/sets the page templates.
		/// </summary>
		public List<PageTemplate> Templates { get ; set ; }
		#endregion

		/// <summary>
		/// Default constructor, creates a new model.
		/// </summary>
		public ListModel() {
			Pages = new List<Sitemap>() ;
			Templates = PageTemplate.GetFields("pagetemplate_id, pagetemplate_name, pagetemplate_preview, pagetemplate_description",
				new Params() { OrderBy = "pagetemplate_name ASC" }) ;
		}

		/// <summary>
		/// Gets the list model for all available pages.
		/// </summary>
		/// <returns>The model.</returns>
		public static ListModel Get() {
			ListModel m = new ListModel() ;
			m.SiteMap = Sitemap.GetStructure(false) ;
			m.Pages = Sitemap.GetStructure(false).Flatten() ;

			return m ;
		}
	}
}
