using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel.Web;
using System.Text;
using System.Linq;
using System.Web.Script.Serialization;

namespace Piranha.Rest
{
	public abstract class BaseService
	{
		protected Stream Serialize(object obj) {
			var js = new JavaScriptSerializer() ;
			var mem = new MemoryStream(Encoding.UTF8.GetBytes(js.Serialize(obj))) ;
			WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8" ;
			return mem ;
		}
	}
}