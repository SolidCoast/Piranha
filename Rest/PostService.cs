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
	/// ReST API for post.
	/// </summary>
	[ServiceContract()]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
	public class PostService : BaseService
	{
		/// <summary>
		/// Gets the post specified by the given id.
		/// </summary>
		/// <param name="id">The post id</param>
		/// <returns>The post</returns>
		[OperationContract()]
		[WebGet(UriTemplate="get/{id}")]
		public Stream Get(string id) {
			return Serialize(GetInternal(id)) ;
		}

		/// <summary>
		/// Gets the post specified by the given id.
		/// </summary>
		/// <param name="id">The post id</param>
		/// <returns>The post</returns>
		internal Post GetInternal(string id) {
			try {
				Models.PostModel pm = Models.PostModel.GetById(new Guid(id)) ;

				if (pm != null) {
					Post post = new Post() {
						Id = pm.Post.Id,
						Title = pm.Post.Title,
						Permalink = pm.Post.Permalink,
						Excerpt = pm.Post.Excerpt,
						Body = pm.Post.Body.ToHtmlString(),
						Created = pm.Post.Created.ToString(),
						Updated = pm.Post.Updated.ToString(),
						Published = pm.Post.Published.ToString(),
						LastPublished = pm.Post.LastPublished.ToString()
					} ;

					// Categories
					foreach (var cat in pm.Categories)
						post.Categories.Add(new Category() {
							Id = cat.Id,
							Permalink = cat.Permalink,
							Name = cat.Name,
							Description = cat.Description,
							Created = cat.Created.ToString(),
							Updated = cat.Updated.ToString()
						}) ;

					// Properties
					foreach (var key in ((IDictionary<string, object>)pm.Properties).Keys)
						post.Properties.Add(new Property() { Name = key, Value = (string)
							((string)((IDictionary<string, object>)pm.Properties)[key]) }) ;

					// Attachments
					foreach (var content in pm.Attachments)
						post.Attachments.Add(new Attachment() { Id = content.Id, IsImage = content.IsImage }) ;

					// Extensions
					foreach (var key in ((IDictionary<string, object>)pm.Extensions).Keys)
						post.Extensions.Add(new Extension() { Name = key, Body =
							((IDictionary<string, object>)pm.Extensions)[key] }) ;
					return post ;
				}
			} catch {}
			return null ;
		}
	}
}