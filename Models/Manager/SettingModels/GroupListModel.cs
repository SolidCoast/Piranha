using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Piranha.Data;

namespace Piranha.Models.Manager.SettingModels
{
	/// <summary>
	/// Group list model for the manager area.
	/// </summary>
	public class GroupListModel
	{
		#region Properties
		/// <summary>
		/// Gets/sets the available groups.
		/// </summary>
		public List<SysGroup> Groups { get ; set ; }
		#endregion

		/// <summary>
		/// Default constructor. Creates a new list model.
		/// </summary>
		public GroupListModel() {
			Groups = new List<SysGroup>() ;
		}

		/// <summary>
		/// Gets all available data.
		/// </summary>
		/// <returns>The model</returns>
		public static GroupListModel Get() {
			GroupListModel m = new GroupListModel() ;

			m.Groups = SysGroup.GetStructure().Flatten() ;

			return m ;
		}
	}
}
