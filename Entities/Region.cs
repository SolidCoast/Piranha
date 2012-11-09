using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

using Piranha.Data;
using Piranha.Extend;

namespace Piranha.Models
{
	/// <summary>
	/// Active record for a page region.
	/// </summary>
	[PrimaryKey(Column="region_id,region_draft")]
	[Join(TableName="regiontemplate", ForeignKey="region_regiontemplate_id", PrimaryKey="regiontemplate_id")]
	public class Region : PiranhaRecord<Region>
	{
		#region Members
		private IExtension body = null ;
		#endregion

		#region Fields
		/// <summary>
		/// Gets/sets the id.
		/// </summary>
		[Column(Name="region_id")]
		public override Guid Id { get ; set ; }

		/// <summary>
		/// Gets/sets weather this is a draft.
		/// </summary>
		[Column(Name="region_draft")]
		public bool IsDraft { get ; set ; }

		/// <summary>
		/// Gets/sets the parent id.
		/// </summary>
		[Column(Name="region_page_id")]
		public Guid PageId { get ; set ; }

		/// <summary>
		/// Gets/sets weather this is a page draft.
		/// </summary>
		[Column(Name="region_page_draft")]
		public bool IsPageDraft { get ; set ; }

		/// <summary>
		/// Gets/sets the id of the region template.
		/// </summary>
		[Column(Name="region_regiontemplate_id")]
		public Guid RegiontemplateId { get ; set ; }

		/// <summary>
		/// Gets/sets the internal id.
		/// </summary>
		[Column(Name="regiontemplate_internal_id", Table="regiontemplate")]
		public string InternalId { get ; set ; }

		/// <summary>
		/// Gets/sets the name.
		/// </summary>
		[Column(Name="regiontemplate_name", Table="regiontemplate")]
		public string Name { get ; set ; }

		/// <summary>
		/// Gets/sets the value type.
		/// </summary>
		[Column(Name="regiontemplate_type", Table="regiontemplate")]
		public string Type { get ; set ; }

		/// <summary>
		/// Gets/sets the internal body json data.
		/// </summary>
		[Column(Name="region_body", OnSave="OnBodySave")]
		private string InternalBody { get ; set ; }

		/// <summary>
		/// Gets/sets the deserialized json body.
		/// </summary>
		[AllowHtml()]
		public IExtension Body {
			get {
				if (body == null)
					body = GetBody() ;
				return body ;
			}
			set {
				body = value ;
			}
		}

		/// <summary>
		/// Gets/sets the created date.
		/// </summary>
		[Column(Name="region_created")]
		public override DateTime Created { get ; set ; }

		/// <summary>
		/// Gets/sets the updated date.
		/// </summary>
		[Column(Name="region_updated")]
		public override DateTime Updated { get ; set ; }

		/// <summary>
		/// Gets/sets the user id that created the record.
		/// </summary>
		[Column(Name="region_created_by")]
		public override Guid CreatedBy { get ; set ; }

		/// <summary>
		/// Gets/sets the user id that created the record.
		/// </summary>
		[Column(Name="region_updated_by")]
		public override Guid UpdatedBy { get ; set ; }
		#endregion

		#region Static accessors
		/// <summary>
		/// Gets all regions associated with the given page id of the given state.
		/// </summary>
		/// <param name="id">The page id</param>
		/// <param name="draft">Weather this is a draft</param>
		/// <returns>The regions</returns>
		public static List<Region> GetByPageId(Guid id, bool draft = false) {
			return Get("region_page_id = @0 AND region_draft = @1", id, draft) ;
		}

		/// <summary>
		/// Gets the name & body for all regions associated with the given page 
		/// id of the given state.
		/// </summary>
		/// <param name="id">The page id</param>
		/// <param name="draft">Weather this is a draft</param>
		/// <returns>The regions</returns>
		public static List<Region> GetContentByPageId(Guid id, bool draft = false) {
			return GetFields("regiontemplate_internal_id, regiontemplate_type, region_body", "region_page_id = @0 AND region_draft = @1", id, draft) ;
		}

		/// <summary>
		/// Gets all regions associated with the given page regardless of state.
		/// </summary>
		/// <param name="id">The page id</param>
		/// <returns>The regions</returns>
		internal static List<Region> GetAllByPageId(Guid id, bool draft = false) {
			return Get("region_page_id = @0", id, draft) ;
		}
		#endregion

		#region Event handlers
		/// <summary>
		/// Before saving the Internal body, serialize the body object.
		/// </summary>
		/// <param name="str">The current data</param>
		/// <returns>The data to save</returns>
		protected string OnBodySave(string str) {
			var js = new JavaScriptSerializer() ;
			if (Body is HtmlString)
				return ((HtmlString)Body).ToHtmlString() ;
			return js.Serialize(Body) ;
		}
		#endregion

		#region Private methods
		/// <summary>
		/// Gets the Json deserialized body for the region.
		/// </summary>
		/// <returns>The body</returns>
		private IExtension GetBody() {
			var js = new JavaScriptSerializer() ;

			if (!String.IsNullOrEmpty(InternalBody)) {
				if (typeof(HtmlString).IsAssignableFrom(ExtensionManager.ExtensionTypes[Type]))
					return (IExtension)Activator.CreateInstance(ExtensionManager.ExtensionTypes[Type], InternalBody) ;
				return (IExtension)js.Deserialize(InternalBody, ExtensionManager.ExtensionTypes[Type]) ;
			}
			return (IExtension)Activator.CreateInstance(ExtensionManager.ExtensionTypes[Type]) ;
		}
		#endregion
	}
}
