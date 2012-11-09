using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

using Piranha.Data;
using Piranha.Extend;

namespace Piranha.Models
{
	/// <summary>
	/// The extension record contains data for an extension attached
	/// to any entity in the database. It's body is stored as JSON.
	/// </summary>
	[PrimaryKey(Column="extension_id,extension_draft")]
	public class Extension : PiranhaRecord<Extension>
	{
		#region Members
		private IExtension body = null ;
		#endregion

		#region Fields
		/// <summary>
		/// Gets/sets the id of the extension.
		/// </summary>
		[Column(Name="extension_id")]
		public override Guid Id { get ; set ; }

		/// <summary>
		/// Gets/sets weather this is a draft or not.
		/// </summary>
		[Column(Name="extension_draft")]
		public bool IsDraft { get ; set ; }

		/// <summary>
		/// Gets/sets the parent id.
		/// </summary>
		[Column(Name="extension_parent_id")]
		public Guid ParentId { get ; set ; }

		/// <summary>
		/// Gets/sets the private Json serialized body.
		/// </summary>
		[Column(Name="extension_body", OnSave="OnBodySave")]
		private string InternalBody { get ; set ; }

		/// <summary>
		/// Gets/sets the body of the extension.
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
		/// Gets/sets the type.
		/// </summary>
		[Column(Name="extension_type")]
		public string Type { get ; set ; }

		/// <summary>
		/// Gets/sets when the extension was initially created.
		/// </summary>
		[Column(Name="extension_created")]
		public override DateTime Created { get ; set ; }

		/// <summary>
		/// Gets/sets when the extension was last updated.
		/// </summary>
		[Column(Name="extension_updated")]
		public override DateTime Updated { get ; set ; }

		/// <summary>
		/// Gets/sets the user who initially created the extension.
		/// </summary>
		[Column(Name="extension_created_by")]
		public override Guid CreatedBy { get ; set ; }

		/// <summary>
		/// Gets/sets the user who last updated the extension.
		/// </summary>
		[Column(Name="extension_updated_by")]
		public override Guid UpdatedBy { get ; set ; }
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
			if (!String.IsNullOrEmpty(Type)) {
				var js = new JavaScriptSerializer() ;

				if (!String.IsNullOrEmpty(InternalBody)) {
					if (typeof(HtmlString).IsAssignableFrom(ExtensionManager.ExtensionTypes[Type]))
						return (IExtension)Activator.CreateInstance(ExtensionManager.ExtensionTypes[Type], InternalBody) ;
					return (IExtension)js.Deserialize(InternalBody, ExtensionManager.ExtensionTypes[Type]) ;
				}
				return (IExtension)Activator.CreateInstance(ExtensionManager.ExtensionTypes[Type]) ;
			}
			return null;
		}
		#endregion
	}
}