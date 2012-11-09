using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Piranha.WebPages
{
	public static class Manager
	{
		#region Inner classes
		/// <summary>
		/// A menu group on the top level in the manager interface.
		/// </summary>
		public class MenuGroup
		{
			#region Properties
			/// <summary>
			/// The internal, non translatable textual id.
			/// </summary>
			public string InternalId { get ; set ; }

			/// <summary>
			/// The name of the group.
			/// </summary>
			public string Name { get ; set ; }

			/// <summary>
			/// The menu items this group contains.
			/// </summary>
			public IList<MenuItem> Items { get ; set ; }

			/// <summary>
			/// Gets the name of the current controller.
			/// </summary>
			private string ControllerName {
				get { return HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString() ; }
			}
			#endregion

			/// <summary>
			/// Default constructor.
			/// </summary>
			public MenuGroup() {
				Items = new List<MenuItem>() ;
			}

			/// <summary>
			/// Checks if the current user has access to the group.
			/// </summary>
			/// <returns>Weather the current user has access.</returns>
			public bool HasAccess() {
				return ItemsForUser().Count > 0 ;
			}

			/// <summary>
			/// Checks if the current group is active.
			/// </summary>
			/// <returns>Weather the group is active</returns>
			public bool IsActive() {
				var controller = ControllerName ;
				
				foreach (var item in ItemsForUser())
					if (item.Controller == controller)
						return true ;
				return false ;
			}

			/// <summary>
			/// Gets the items available for the current user.
			/// </summary>
			/// <returns>The menu items</returns>
			public IList<MenuItem> ItemsForUser() {
				var ret = new List<MenuItem>() ;

				foreach (var item in Items)
					if (!String.IsNullOrEmpty(item.Permission)) {
						if (HttpContext.Current.User.HasAccess(item.Permission))
							ret.Add(item) ;
					} else ret.Add(item) ;
				return ret ;
			}
		}

		/// <summary>
		/// A menu item in the manager interface.
		/// </summary>
		public class MenuItem
		{
			#region Properties
			/// <summary>
			/// The internal, non translatable textual id.
			/// </summary>
			public string InternalId { get ; set ; }

			/// <summary>
			/// Gets/sets the item name.
			/// </summary>
			public string Name { get ; set ; }

			/// <summary>
			/// Gets/sets the item action.
			/// </summary>
			public string Action { get ; set ; }

			/// <summary>
			/// Gets/sets the item controller.
			/// </summary>
			public string Controller { get ; set ; }

			/// <summary>
			/// Gets/sets the permission needed to access the item.
			/// </summary>
			public string Permission { get ; set ; }

			/// <summary>
			/// Gets/sets the selected actions for this item if several items share
			/// the same controller.
			/// </summary>
			public string SelectedActions { get ; set ; }
			#endregion

			/// <summary>
			/// Gets weather this menu item is currently active.
			/// </summary>
			/// <returns>Weather the item is active</returns>
			public bool IsActive() {
				var controller = HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString();
				var action = HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString();
				if (String.IsNullOrEmpty(action))
					action = "index" ;

				if (Controller == controller) {
					if (!String.IsNullOrEmpty(SelectedActions)) {
						string[] actions = SelectedActions.Split(new char[] { ',' }) ;

						foreach (var a in actions)
							if (a.Trim() == action)
								return true ;
					} else return true ;
				}
				return false ;
			}
		}
		#endregion

		/// <summary>
		/// The default manager menu.
		/// </summary>
		public static List<MenuGroup> Menu = new List<MenuGroup>() {
			new MenuGroup() { InternalId = "Content", Name = @Resources.Global.MenuContent, Items = new List<MenuItem>() {
				new MenuItem() { InternalId = "Pages", Name = @Resources.Tabs.ContentPages, Action = "index", Controller = "page", Permission = "ADMIN_PAGE" },
				new MenuItem() { InternalId = "Posts", Name = @Resources.Tabs.ContentPosts, Action = "index", Controller = "post", Permission = "ADMIN_POST" },
				new MenuItem() { InternalId = "Media", Name = @Resources.Tabs.ContentMedia, Action = "index", Controller = "content", Permission = "ADMIN_CONTENT" }
			}},
			new MenuGroup() { InternalId = "Settings", Name = @Resources.Global.MenuSettings, Items = new List<MenuItem>() {
				new MenuItem() { InternalId = "PageTypes", Name = @Resources.Tabs.SettingsPageTypes, Action = "pagelist", Controller = "template", 
					Permission = "ADMIN_PAGE_TEMPLATE", SelectedActions = "pagelist, page" },
				new MenuItem() { InternalId = "PostTypes", Name = @Resources.Tabs.SettingsPostTypes, Action = "postlist", Controller = "template", 
					Permission = "ADMIN_POST_TEMPLATE", SelectedActions = "postlist, post" },
				new MenuItem() { InternalId = "Categories", Name = @Resources.Tabs.SettingsCategories, Action = "index", Controller = "category", 
					Permission = "ADMIN_CATEGORY" }
			}},
			new MenuGroup() { InternalId = "System", Name = @Resources.Global.MenuSystem, Items = new List<MenuItem>() {
				new MenuItem() { InternalId = "Users", Name = @Resources.Tabs.SystemUsers, Action = "userlist", Controller = "settings", 
					Permission = "ADMIN_USER", SelectedActions = "userlist, user" },
				new MenuItem() { InternalId = "Groups", Name = @Resources.Tabs.SystemGroups, Action = "grouplist", Controller = "settings", 
					Permission = "ADMIN_GROUP", SelectedActions = "grouplist, group" },
				new MenuItem() { InternalId = "Permissions", Name = @Resources.Tabs.SystemAccess, Action = "accesslist", Controller = "settings", 
					Permission = "ADMIN_ACCESS", SelectedActions = "accesslist, access" },
				new MenuItem() { InternalId = "Parameters", Name = @Resources.Tabs.SystemParams, Action = "paramlist", Controller = "settings", 
					Permission = "ADMIN_PARAM", SelectedActions = "paramlist, param" }
			}}
		} ;

		/// <summary>
		/// Gets the currently active menu item.
		/// </summary>
		/// <returns>The menu item</returns>
		public static MenuItem GetActiveMenuItem() {
			foreach (var group in Menu) {
				if (group.IsActive()) {
					foreach (var item in group.Items)
						if (item.IsActive())
							return item ;
				}
			}
			return null ;
		}
	}
}