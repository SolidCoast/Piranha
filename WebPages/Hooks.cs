using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Piranha.WebPages
{
	/// <summary>
	/// The different hook in the core that the application can attach
	/// itself to.
	/// </summary>
	public static class Hooks {
		/// <summary>
		/// The different hooks available for the breadcrumb.
		/// </summary>
		public static class Breadcrumb {
			/// <summary>
			/// Renders the start of the breadcrumb.
			/// </summary>
			public static Delegates.BreadcrumbStartHook RenderStart ;

			/// <summary>
			/// Renders the end of the breadcrumb.
			/// </summary>
			public static Delegates.BreadcrumbEndHook RenderEnd ;

			/// <summary>
			/// Renders a breadcrumb item.
			/// </summary>
			public static Delegates.BreadcrumbItemHook RenderItem ;

			/// <summary>
			/// Renders the currently active breadcrumb item.
			/// </summary>
			public static Delegates.BreadcrumbItemHook RenderActiveItem ;
		}

		/// <summary>
		/// The different hooks available for the head.
		/// </summary>
		public static class Head {
			/// <summary>
			/// Renders optional information in the head.
			/// </summary>
			public static Delegates.HeadHook Render ;
		}

		/// <summary>
		/// The different hooks availble for the menu.
		/// </summary>
		public static class Menu {
			/// <summary>
			/// Renders the start of a menu level.
			/// </summary>
			public static Delegates.MenuLevelHook RenderLevelStart ;

			/// <summary>
			/// Renders the end of a menu level.
			/// </summary>
			public static Delegates.MenuLevelHook RenderLevelEnd ;

			/// <summary>
			/// Renders the start of a menu item.
			/// </summary>
			public static Delegates.MenuItemHook RenderItemStart ;

			/// <summary>
			/// Renders the end of a menu item.
			/// </summary>
			public static Delegates.MenuItemHook RenderItemEnd ;

			/// <summary>
			/// Renders the menu item link.
			/// </summary>
			public static Delegates.MenuItemLinkHook RenderItemLink ;
		}

		/// <summary>
		/// The different model hooks available for the application.
		/// </summary>
		public static class Model { 
			/// <summary>
			/// Executed after the page model is loaded.
			/// </summary>
			public static Delegates.ModelLoadedHook<Models.PageModel> PageModelLoaded ;

			/// <summary>
			/// Executed after the page model is loaded.
			/// </summary>
			public static Delegates.ModelLoadedHook<Models.PostModel> PostModelLoaded ;
		}

		/// <summary>
		/// The different hooks available for the manager area.
		/// </summary>
		public static class Manager {
			/// <summary>
			/// Executed after the post list model is loaded but before the view is called.
			/// </summary>
			public static Delegates.ManagerModelLoadedHook<Models.Manager.PostModels.ListModel> PostListModelLoaded ;

			/// <summary>
			/// Executed after the post edit model is loaded but before the view is called.
			/// </summary>
			public static Delegates.ManagerModelLoadedHook<Models.Manager.PostModels.EditModel> PostEditModelLoaded ;

			/// <summary>
			/// Executed after the page list model is loaded but before the view is called.
			/// </summary>
			public static Delegates.ManagerModelLoadedHook<Models.Manager.PageModels.ListModel> PageListModelLoaded ;

			/// <summary>
			/// Executed after the page edit model is loaded but before the view is called.
			/// </summary>
			public static Delegates.ManagerModelLoadedHook<Models.Manager.PageModels.EditModel> PageEditModelLoaded ;

			/// <summary>
			/// Executed after the user list model is loaded but before the view is called.
			/// </summary>
			public static Delegates.ManagerModelLoadedHook<Models.Manager.SettingModels.UserListModel> UserListModelLoaded ;

			/// <summary>
			/// Executed after the user edit model is loaded but before the view is called.
			/// </summary>
			public static Delegates.ManagerModelLoadedHook<Models.Manager.SettingModels.UserEditModel> UserEditModelLoaded ;
		}
	}
}
