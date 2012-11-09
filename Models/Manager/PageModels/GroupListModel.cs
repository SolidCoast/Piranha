using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Piranha.Models;

namespace Piranha.Models.Manager.PageModels
{
	public class GroupListModel
	{
		public List<SysGroup> Groups { get ; set ; }
		public Page Page { get ; set ; }
	}
}