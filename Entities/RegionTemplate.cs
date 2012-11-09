using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

using Piranha.Data;

namespace Piranha.Models
{
	/// <summary>
	/// Active record for the region template.
	/// </summary>
	[PrimaryKey(Column="regiontemplate_id")]
	public class RegionTemplate : PiranhaRecord<RegionTemplate>
	{
		#region Fields
		[Column(Name="regiontemplate_id")]
		public override Guid Id { get ; set ; }

		[Column(Name="regiontemplate_template_id")]
		[Required()]
		public Guid TemplateId { get ; set ; }

		[Column(Name="regiontemplate_internal_id")]
		[Required(ErrorMessageResourceType=typeof(Piranha.Resources.Regions), ErrorMessageResourceName="InternalIdRequired")]
		public string InternalId { get ; set ; }

		[Column(Name="regiontemplate_seqno")]
		[Required()]
		public int Seqno { get ; set ; }

		[Column(Name="regiontemplate_name")]
		[Required(ErrorMessageResourceType=typeof(Piranha.Resources.Regions), ErrorMessageResourceName="NameRequired")]
		public string Name { get ; set ; }

		[Column(Name="regiontemplate_description")]
		public string Description { get ; set ; }

		[Column(Name="regiontemplate_type")]
		[Required(ErrorMessageResourceType=typeof(Piranha.Resources.Regions), ErrorMessageResourceName="TypeRequired")]
		public string Type { get ; set ; }

		/// <summary>
		/// Gets/sets the created date.
		/// </summary>
		[Column(Name="regiontemplate_created")]
		public override DateTime Created { get ; set ; }

		/// <summary>
		/// Gets/sets the updated date.
		/// </summary>
		[Column(Name="regiontemplate_updated")]
		public override DateTime Updated { get ; set ; }

		/// <summary>
		/// Gets/sets the user id that created the record.
		/// </summary>
		[Column(Name="regiontemplate_created_by")]
		public override Guid CreatedBy { get ; set ; }

		/// <summary>
		/// Gets/sets the user id that created the record.
		/// </summary>
		[Column(Name="regiontemplate_updated_by")]
		public override Guid UpdatedBy { get ; set ; }
		#endregion

		#region Static accessors
		/// <summary>
		/// Gets the region templates for the given template.
		/// </summary>
		/// <param name="templateid">The template id</param>
		/// <returns>The region templates</returns>
		public static List<RegionTemplate> GetByTemplateId(Guid templateid) {
			return Get("regiontemplate_template_id = @0", templateid, new Params() { OrderBy = "regiontemplate_seqno ASC" }) ;
		}
		#endregion
	}
}
