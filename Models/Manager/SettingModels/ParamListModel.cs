using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Piranha.Data;

namespace Piranha.Models.Manager.SettingModels
{
	/// <summary>
	/// Settings list model for the manager area.
	/// </summary>
	public class ParamListModel
	{
		#region Properties
		/// <summary>
		/// Gets/sets the available params.
		/// </summary>
		public List<SysParam> Params { get ; set ; }
		#endregion

		/// <summary>
		/// Default constructor. Creates a new list model.
		/// </summary>
		public ParamListModel() {
			Params = new List<SysParam>() ;
		}

		/// <summary>
		/// Gets all available data.
		/// </summary>
		/// <returns>The model</returns>
		public static ParamListModel Get() {
			ParamListModel m = new ParamListModel() ;

			m.Params = SysParam.Get(new Params() { OrderBy = "sysparam_name" }) ;

			return m ;
		}
	}
}
