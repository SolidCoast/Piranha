using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Piranha.Data;

namespace Piranha.Models.Manager.SettingModels
{
	/// <summary>
	/// User list model for the manager area.
	/// </summary>
	public class UserListModel
	{
		#region Properties
		/// <summary>
		/// Gets/sets the available users.
		/// </summary>
		public List<SysUser> Users { get ; set ; }
		#endregion

		/// <summary>
		/// Default constructor. Creates a new list model.
		/// </summary>
		public UserListModel() {
			Users = new List<SysUser>() ;
		}

		/// <summary>
		/// Gets all available data.
		/// </summary>
		/// <returns>The model</returns>
		public static UserListModel Get() {
			UserListModel m = new UserListModel() ;

			m.Users = SysUser.Get(new Params() { OrderBy = "sysuser_login" }) ;

			return m ;
		}
	}
}
