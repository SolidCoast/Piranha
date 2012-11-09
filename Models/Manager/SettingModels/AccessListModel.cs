using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Piranha.Data;

namespace Piranha.Models.Manager.SettingModels
{
	/// <summary>
	/// Access list model for the manager area.
	/// </summary>
	public class AccessListModel
	{
		#region Properties
		/// <summary>
		/// Gets/sets the available access rights.
		/// </summary>
		public List<SysAccess> Access { get ; set ; }
		#endregion

		/// <summary>
		/// Default constructor. Creates a new list model.
		/// </summary>
		public AccessListModel() {
			Access = new List<SysAccess>() ;
		}

		/// <summary>
		/// Gets all available data.
		/// </summary>
		/// <returns>The model</returns>
		public static AccessListModel Get() {
			AccessListModel m = new AccessListModel() ;

			m.Access = SysAccess.Get(new Params() { OrderBy = "sysaccess_function" }) ;

			return m ;
		}
	}
}
