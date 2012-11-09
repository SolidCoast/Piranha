using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;

using Piranha.Data;

namespace Piranha.Models
{
	[PrimaryKey(Column="permalink_id")]
	public class Permalink : PiranhaRecord<Permalink>, ICacheRecord<Permalink>
	{
		#region Inner classes
		public enum PermalinkType {
			PAGE, POST, CATEGORY
		}
		#endregion

		#region Fields
		[Column(Name="permalink_id")]
		[Required()]
		public override Guid Id { get ; set ; }

		[Column(Name="permalink_namespace_id")]
		public Guid NamespaceId { get ; set ; }

		[Column(Name="permalink_type")]
		public PermalinkType Type { get ; set ; }

		[Column(Name="permalink_name")]
		public string Name { get ; set ; }

		[Column(Name="permalink_created")]
		public override DateTime Created { get ; set ; }

		[Column(Name="permalink_updated")]
		public override DateTime Updated { get ; set ; }

		[Column(Name="permalink_created_by")]
		public override Guid CreatedBy { get ; set ; }

		[Column(Name="permalink_updated_by")]
		public override Guid UpdatedBy { get ; set ; }
		#endregion

		#region Properties
		/// <summary>
		/// Gets the permalink cache object.
		/// </summary>
		private static Dictionary<Guid, Dictionary<string, Permalink>> Cache {
			get {
				if (HttpContext.Current != null) {
					if (HttpContext.Current.Cache[typeof(Permalink).Name] == null) {
						var cache = new Dictionary<Guid, Dictionary<string, Permalink>>() ;
						cache.Add(new Guid("8FF4A4B4-9B6C-4176-AAA2-DB031D75AC03"), new Dictionary<string,Permalink>()) ;
						cache.Add(new Guid("AE46C4C4-20F7-4582-888D-DFC148FE9067"), new Dictionary<string,Permalink>()) ;
						HttpContext.Current.Cache[typeof(Permalink).Name] = cache ;
					}
					return (Dictionary<Guid, Dictionary<string, Permalink>>)HttpContext.Current.Cache[typeof(Permalink).Name] ;
				}
				return new Dictionary<Guid,Dictionary<string,Permalink>>() ;
			}
		}

		/// <summary>
		/// Gets the permalink id cache object.
		/// </summary>
		private static Dictionary<Guid, Permalink> IdCache {
			get {
				if (HttpContext.Current != null) {
					if (HttpContext.Current.Cache[typeof(Permalink).Name + "_Id"] == null)
						HttpContext.Current.Cache[typeof(Permalink).Name + "_Id"] = new Dictionary<Guid, Permalink>() ;
					return (Dictionary<Guid, Permalink>)HttpContext.Current.Cache[typeof(Permalink).Name + "_Id"] ;
				}
				return new Dictionary<Guid,Permalink>() ;
			}
		} 
		#endregion

		#region Static accessors
		/// <summary>
		/// Gets the permalink with the given id.
		/// </summary>
		/// <param name="id">The id</param>
		/// <returns>The permalink</returns>
		public static Permalink GetSingle(Guid id) {
			if (!IdCache.ContainsKey(id))
				IdCache[id] = GetSingle((object)id) ;
			return IdCache[id] ;
		}

		/// <summary>
		/// Gets the permalink with the given name.
		/// </summary>
		/// <param name="namespaceid">The namespace id</param>
		/// <param name="name">The permalink name</param>
		/// <returns>The permalink</returns>
		public static Permalink GetByName(Guid namespaceid, string name) {
			if (Cache.ContainsKey(namespaceid)) {
				if (!Cache[namespaceid].ContainsKey(name)) {
					Cache[namespaceid][name] = GetSingle("permalink_name = @0 AND permalink_namespace_id = @1", name, namespaceid) ;
					if (Cache[namespaceid][name] != null)
						IdCache[Cache[namespaceid][name].Id] = Cache[namespaceid][name] ;
				}
				return Cache[namespaceid][name] ;
			}
			return null ;
		}
		#endregion

		/// <summary>
		/// Saves the current entity.
		/// </summary>
		/// <param name="tx">Optional transaction</param>
		/// <returns>Weather the operation succeeded</returns>
		public override bool Save(System.Data.IDbTransaction tx = null) {
			// Check for duplicates 
			if (Permalink.GetSingle("permalink_name = @0 AND permalink_namespace_id = @2" + (!IsNew ? " AND permalink_id != @1" : ""), Name, Id, NamespaceId) != null)
 				throw new DuplicatePermalinkException() ;
			return base.Save(tx);
		}

		/// <summary>
		/// Converts the given string to a web safe permalink.
		/// </summary>
		/// <param name="str">The string</param>
		/// <returns>A permalink</returns>
		public static string Generate(string str) {
			return Regex.Replace(str.ToLower().Replace(" ", "-").Replace("å", "a").Replace("ä", "a").Replace("ö", "o"),
				@"[^a-z0-9-]", "") ;
		}

		/// <summary>
		/// Invalidates the current record from the cache.
		/// </summary>
		/// <param name="record">The record</param>
		public void InvalidateRecord(Permalink record) {
			if (Cache.ContainsKey(NamespaceId)) {
				if (Cache[NamespaceId].ContainsKey(record.Name))
					Cache[NamespaceId].Remove(record.Name) ;
			}
			if (IdCache.ContainsKey(Id))
				IdCache.Remove(Id) ;
		}
	}
}
