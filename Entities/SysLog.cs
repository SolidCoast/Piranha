using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Piranha.Data;

namespace Piranha.Models
{
	/// <summary>
	/// Active record for a log entry.
	/// </summary>
	[PrimaryKey(Column="syslog_id")]
	public class SysLog : PiranhaRecord<SysLog>
	{
		#region Fields
		/// <summary>
		/// Gets/sets the id.
		/// </summary>
		[Column(Name="syslog_id")]
		public override Guid Id { get ; set ; }

		/// <summary>
		/// Gets/sets the id the log is related to.
		/// </summary>
		[Column(Name="syslog_parent_id")]
		public Guid ParentId { get ; set ; }

		/// <summary>
		/// Gets/sets the type of entity the log is related to.
		/// </summary>
		[Column(Name="syslog_parent_type")]
		public string ParentType { get ; set ; }

		/// <summary>
		/// Gets/sets the action the log is related to (INSERT/UPDATE/DELETE).
		/// </summary>
		[Column(Name="syslog_action")]
		public string Action { get ; set ; }

		/// <summary>
		/// Gets/sets the created date.
		/// </summary>
		[Column(Name="syslog_created")]
		public override DateTime Created { get ; set ; }

		/// <summary>
		/// Gets/sets the updated date.
		/// </summary>
		[Column(Name="syslog_updated")]
		public override DateTime Updated { get ; set ; }

		/// <summary>
		/// Gets/sets the id of the user who created the log.
		/// </summary>
		[Column(Name="syslog_created_by")]
		public override Guid CreatedBy { get ; set ; }

		/// <summary>
		/// Gets/sets the id of the user who last updated the log.
		/// </summary>
		[Column(Name="syslog_updated_by")]
		public override Guid UpdatedBy { get ; set ; }
		#endregion
	}
}
