using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Piranha.Data.Updates
{
	/// <summary>
	/// Interface for defining an update.
	/// </summary>
	public interface IUpdate
	{
		/// <summary>
		/// Executes the current update.
		/// </summary>
		void Execute(IDbTransaction tx) ;
	}
}