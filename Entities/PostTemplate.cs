using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;

using Piranha.Data;

namespace Piranha.Models
{
	/// <summary>
	/// Active record for a post template.
	/// 
	/// Changes made to records of this type are logged.
	/// </summary>
	[PrimaryKey(Column="posttemplate_id")]
	public class PostTemplate : PiranhaRecord<PostTemplate>
	{
		#region Fields
		/// <summary>
		/// Gets/sets the id.
		/// </summary>
		[Column(Name="posttemplate_id")]
		public override Guid Id { get ; set ; }

		/// <summary>
		/// Gets/sets the name.
		/// </summary>
		[Column(Name="posttemplate_name")]
		[Display(ResourceType=typeof(Piranha.Resources.Global), Name="Name")]
		[Required(ErrorMessageResourceType=typeof(Piranha.Resources.Global), ErrorMessageResourceName="NameRequired")]
		public string Name { get ; set ; }

		/// <summary>
		/// Gets/sets the description.
		/// </summary>
		[Column(Name="posttemplate_description")]
		[Display(ResourceType=typeof(Piranha.Resources.Post), Name="Description")]
		public string Description { get ; set ; }

		/// <summary>
		/// Gets/sets the description.
		/// </summary>
		[Column(Name="posttemplate_preview")]
		[Display(ResourceType=typeof(Piranha.Resources.Post), Name="HtmlPreview")]
		public HtmlString Preview { get ; set ; }

		/// <summary>
		/// Gets/sets the associated properties.
		/// </summary>
		[Column(Name="posttemplate_properties", Json=true)]
		public List<string> Properties { get ; set ; }

		/// <summary>
		/// Gets/sets the optional controller for the template.
		/// </summary>
		[Column(Name="posttemplate_controller")]
		[Display(ResourceType=typeof(Piranha.Resources.Post), Name="Template")]
		public string Controller { get ; set ; }

		/// <summary>
		/// Gets/sets wether the controller can be overridden by the implementing post.
		/// </summary>
		[Column(Name="posttemplate_controller_show")]
		public bool ShowController { get ; set ; }

		/// <summary>
		/// Gets/sets the optional controller for the template.
		/// </summary>
		[Column(Name="posttemplate_archive_controller")]
		[Display(ResourceType=typeof(Piranha.Resources.Post), Name="ArchiveTemplate")]
		public string ArchiveController { get ; set ; }

		/// <summary>
		/// Gets/sets wether the controller can be overridden by the implementing post.
		/// </summary>
		[Column(Name="posttemplate_archive_controller_show")]
		public bool ShowArchiveController { get ; set ; }

		/// <summary>
		/// Gets/sets weather posts of this type should be exported as rss.
		/// </summary>
		[Column(Name="posttemplate_rss")]
		[Display(ResourceType=typeof(Piranha.Resources.Template), Name="AllowRss")]
		public bool AllowRss { get ; set ; }

		/// <summary>
		/// Gets/sets the created date.
		/// </summary>
		[Column(Name="posttemplate_created")]
		public override DateTime Created { get ; set ; }

		/// <summary>
		/// Gets/sets the updated date.
		/// </summary>
		[Column(Name="posttemplate_updated")]
		public override DateTime Updated { get ; set ; }

		/// <summary>
		/// Gets/sets the user id that created the record.
		/// </summary>
		[Column(Name="posttemplate_created_by")]
		public override Guid CreatedBy { get ; set ; }

		/// <summary>
		/// Gets/sets the user id that created the record.
		/// </summary>
		[Column(Name="posttemplate_updated_by")]
		public override Guid UpdatedBy { get ; set ; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public PostTemplate() : base() {
			Properties = new List<string>() ;
			LogChanges = true ;
		}
	}
}
