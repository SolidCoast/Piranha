using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web;

using Piranha.Data;
using Piranha.Rest.DataContracts;

namespace Piranha.Rest
{
	/// <summary>
	/// ReST API for pages.
	/// </summary>
	[ServiceContract()]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
	public class PageService : BaseService
	{
		/// <summary>
		/// Gets the page specified by the given id.
		/// </summary>
		/// <param name="id">The page id</param>
		/// <returns>The page</returns>
		[OperationContract()]
		[WebGet(UriTemplate="get/{id}")]
		public Stream Get(string id) {
			return Serialize(GetInternal(id)) ;
		}

		/// <summary>
		/// Gets the page specified by the given id as xml.
		/// </summary>
		/// <param name="id">The page id</param>
		/// <returns>The page</returns>
		[OperationContract()]
		[WebGet(UriTemplate="get/xml/{id}", ResponseFormat=WebMessageFormat.Xml)]
		public Page GetXml(string id) {
			return GetInternal(id) ;
		}

		/// <summary>
		/// Gets the page specified by the given id.
		/// </summary>
		/// <param name="id">The page id</param>
		/// <returns>The page.</returns>
		internal Page GetInternal(string id) {
			try {
				Models.PageModel pm = Models.PageModel.GetById(new Guid(id)) ;

				if (pm != null && (pm.Page.GroupId == Guid.Empty || HttpContext.Current.User.IsMember(pm.Page.GroupId))) {
					// Page data
					Page page = new Page() {
						Id = pm.Page.Id,
						Title = pm.Page.Title,
						Permalink = pm.Page.Permalink,
						IsHidden = ((Piranha.Models.Page)pm.Page).IsHidden,
						Created = pm.Page.Created.ToString(),
						Updated = pm.Page.Updated.ToString(),
						Published = pm.Page.Published.ToString(),
						LastPublished = pm.Page.LastPublished.ToString()
					} ;

					// Regions
					foreach (var key in ((IDictionary<string, object>)pm.Regions).Keys) {
						if (((IDictionary<string, object>)pm.Regions)[key] is HtmlString)
							page.Regions.Add(new Region() { Name = key, Body = 
								((HtmlString)((IDictionary<string, object>)pm.Regions)[key]).ToHtmlString() }) ;
						else
							page.Regions.Add(new Region() { Name = key, Body = 
								((IDictionary<string, object>)pm.Regions)[key] }) ;
					}

					// Properties
					foreach (var key in ((IDictionary<string, object>)pm.Properties).Keys)
						page.Properties.Add(new Property() { Name = key, Value = (string)
							((string)((IDictionary<string, object>)pm.Properties)[key]) }) ;

					// Attachments
					foreach (var content in pm.Attachments)
						page.Attachments.Add(new Attachment() { Id = content.Id, IsImage = content.IsImage }) ;

					// Extensions
					foreach (var key in ((IDictionary<string, object>)pm.Extensions).Keys)
						page.Extensions.Add(new Extension() { Name = key, Body =
							((IDictionary<string, object>)pm.Extensions)[key] }) ;

					return page ;
				}
			} catch {}
			return null ;
		}
	}
}
