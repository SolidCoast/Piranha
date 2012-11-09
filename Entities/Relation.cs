using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Piranha.Data;

namespace Piranha.Models
{
	/// <summary>
	/// The relation is a way of connecting information from different tables to
	/// each other in a fairly loosely coupled way.
	/// </summary>
	[PrimaryKey(Column="relation_id")]
	public class Relation : GuidRecord<Relation>
	{
		#region Inner classes
		/// <summary>
		/// Defines the different types of relations.
		/// </summary>
		public enum RelationType {
			POSTCATEGORY, CONTENTCATEGORY
		}
		#endregion

		#region Fields
		/// <summary>
		/// Gets/sets the id.
		/// </summary>
		[Column(Name="relation_id")]
		public override Guid Id { get ; set ; }

		/// <summary>
		/// Gets/sets weather this is a draft or not.
		/// </summary>
		[Column(Name="relation_draft")]
		public bool IsDraft { get ; set ; }

		/// <summary>
		/// Gets/sets the type.
		/// </summary>
		[Column(Name="relation_type")]
		public RelationType Type { get ; set ; }

		/// <summary>
		/// Gets/sets the main data for the relation.
		/// </summary>
		[Column(Name="relation_data_id")]
		public Guid DataId { get ; set ; }

		/// <summary>
		/// Gets/sets the related data for the relation.
		/// </summary>
		[Column(Name="relation_related_id")]
		public Guid RelatedId { get ; set ; }
		#endregion

		/// <summary>
		/// Default constructor. Creates a new relation.
		/// </summary>
		public Relation() {
			IsDraft = true ;
		}

		/// <summary>
		/// Gets all relations for the given data id.
		/// </summary>
		/// <param name="id">The main object id</param>
		/// <returns>A list of relations</returns>
		public static List<Relation> GetByDataId(Guid id, bool draft = true) {
			return GetFieldsByDataId("*", id, draft) ;
		}

		/// <summary>
		/// Gets the selected fields for the matching relations.
		/// </summary>
		/// <param name="fields">The fields</param>
		/// <param name="id">The main object id.</param>
		/// <returns>A list of relations</returns>
		public static List<Relation> GetFieldsByDataId(string fields, Guid id, bool draft = true) {
			return GetFields(fields, "relation_data_id = @0 AND relation_draft = @1", id, draft) ;
		}

		/// <summary>
		/// Gets the available relations for the given type and related id.
		/// </summary>
		/// <param name="type">The relation type</param>
		/// <param name="id">The relation data</param>
		/// <returns>The relations</returns>
		public static List<Relation> GetByTypeAndRelatedId(RelationType type, Guid id, bool draft = true) {
			return Relation.Get("relation_type = @0 AND relation_related_id = @1 AND relation_draft = @2", type, id, draft) ;
		}

		/// <summary>
		/// Deletes all of the relations for the given object id.
		/// </summary>
		/// <param name="id">The main object id</param>
		/// <param name="tx">Optional data transaction</param>
		public static  void DeleteByDataId(Guid id, IDbTransaction tx = null, bool? draft = null) {
			if (!draft.HasValue)
				Relation.Execute("DELETE FROM relation WHERE relation_data_id = @0", tx, id) ;
			else Relation.Execute("DELETE FROM relation WHERE relation_data_id = @0 AND relation_draft = @1", tx, id, draft.Value) ;
		}
	}
}
