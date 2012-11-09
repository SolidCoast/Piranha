using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

using Piranha.Data;
using Piranha.Models;

namespace Piranha.Data.Updates
{
	/// <summary>
	/// Update 13.
	/// 
	/// This update creates region templates for all existing page templates and
	/// copies their existing data.
	/// </summary>
	public class Update13 : IUpdate
	{
		/// <summary>
		/// Executes the current update.
		/// </summary>
		public void Execute(IDbTransaction tx) {
			foreach (var ptemplate in PageTemplate.Get(tx)) {
				for (int n = 0; n < ptemplate.PageRegions.Count; n++) {
					// Add region templates
					var rtemplate = new RegionTemplate() {
						TemplateId = ptemplate.Id,
						InternalId = ptemplate.PageRegions[n],
						Name = ptemplate.PageRegions[n],
						Type = typeof(Extend.Regions.HtmlRegion).FullName,
						Seqno = n + 1
					} ;
					rtemplate.Save(tx) ;

					// Set region template id for all related ids
					Region.Execute("UPDATE region SET region_regiontemplate_id = @0 WHERE region_name = @1 AND region_page_id IN " +
						"(SELECT page_id FROM page WHERE page_template_id = @2)", tx, rtemplate.Id, rtemplate.InternalId, rtemplate.TemplateId) ;
				}
			}
		}
	}
}