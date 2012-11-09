using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

using Piranha.Models;
using Piranha.Web;

namespace Piranha.Mvc
{
	public abstract class SinglePage<T> : BaseViewPage<T> where T : Models.PageModel
	{
		protected override void InitializePage() {
			ViewBag.Page = Model.Page ;
			Page.Current = Model.Page ;
			ViewBag.Post = null ;
			Page.CurrentPost = null ;

			base.InitializePage();
		}
	}

	public abstract class SinglePage : SinglePage<Models.PageModel> {}
}
