using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

using Piranha.WebPages;

namespace Piranha
{
	/// <summary>
	/// The different delegates used by the framework.
	/// </summary>
	public static class Delegates
	{
		public delegate void BreadcrumbStartHook(Web.UiHelper ui, StringBuilder str) ;
		public delegate void BreadcrumbEndHook(Web.UiHelper ui, StringBuilder str) ;
		public delegate void BreadcrumbItemHook(Web.UiHelper ui, StringBuilder str, Models.Sitemap page) ;

		public delegate void HeadHook(Web.UiHelper ui, StringBuilder str, Models.Page page, Models.Post post) ;

		public delegate void MenuItemHook(Web.UiHelper ui, StringBuilder str, Models.Sitemap page, bool active, bool activechild) ;
		public delegate void MenuItemLinkHook(Web.UiHelper ui, StringBuilder str, Models.Sitemap page) ;
		public delegate void MenuLevelHook(Web.UiHelper ui, StringBuilder str, string cssclass) ;

		public delegate void ModelLoadedHook<T>(T model) ;
		public delegate void ManagerModelLoadedHook<T>(Controller controller, Manager.MenuItem menu, T model) ;
	}
}
