using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Piranha.Extend
{
	/// <summary>
	/// The extension manager handles all regions, properties & plugins.
	/// </summary>
	public static class ExtensionManager
	{
		#region Members
		/// <summary>
		/// Gets the available extension attributes.
		/// </summary>
		private static Dictionary<string, ExtensionAttribute> ExtensionAttributes = new Dictionary<string,ExtensionAttribute>() ;
		#endregion

		#region Properties
		/// <summary>
		/// Gets the available extension types.
		/// </summary>
		public static Dictionary<string, Type> ExtensionTypes { get ; private set ; }

		/// <summary>
		/// Gets the currently available extensions.
		/// </summary>
		public static List<Extension> Extensions { get ; private set ; }
		#endregion

		/// <summary>
		/// Initializes the extension manager. This method should be invoked
		/// on application start.
		/// </summary>
		internal static void Init() {
			// Create the extension list.
			ExtensionTypes = new Dictionary<string, Type>() ;
			Extensions = new List<Extension>() ;

			// Get all loaded assemblies
			foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies()) {
				// Iterate all types
				foreach (var type in assembly.GetTypes()) {
					// Get all extensions.
					if (typeof(IExtension).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract) {
						var attr = type.GetCustomAttribute<ExtensionAttribute>(false) ;
						if (attr != null) {
							if (attr.Type != ExtensionType.NotSet) {
								ExtensionAttributes.Add(type.FullName, attr) ;
								ExtensionTypes.Add(type.FullName, type) ;
								Extensions.Add(new Extension() {
									ExtensionType = attr.Type,
									Name = attr.Name,
									Type = type
								});
								// Check if the extension implements the ensure method
								var m = type.GetMethod("Ensure") ;
								if (m != null) {
									Data.Database.LoginSys() ;
									m.Invoke(Activator.CreateInstance(type), null) ;
									Data.Database.Logout() ;
								}
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// Gets the name for the given extension type.
		/// </summary>
		/// <param name="type">The type</param>
		/// <returns>The name</returns>
		public static string GetExtensionNameByType(string type) {
			if (ExtensionAttributes.ContainsKey(type))
				return ExtensionAttributes[type].Name ;
			return "" ;
		}

		/// <summary>
		/// Gets the internal id for the given extension type.
		/// </summary>
		/// <param name="type">The type</param>
		/// <returns>The internal id</returns>
		public static string GetInternalIdByType(string type) {
			if (ExtensionAttributes.ContainsKey(type))
				return ExtensionAttributes[type].InternalId != null ? ExtensionAttributes[type].InternalId : 
					ExtensionAttributes[type].Name.Replace(" ", "").Replace(".", "") ;
			return "" ;
		}

		/// <summary>
		/// Gets the icon path for the given extension type.
		/// </summary>
		/// <param name="type">The type</param>
		/// <returns>The icon path</returns>
		public static string GetIconPathByType(string type) {
			if (ExtensionAttributes.ContainsKey(type))
				return ExtensionAttributes[type].IconPath != null ? ExtensionAttributes[type].IconPath : "" ;
			return "" ;
		}

		/// <summary>
		/// Gets the extensions available for the given type.
		/// </summary>
		/// <param name="type">The extension type</param>
		/// <param name="draft">Weather the entity is draft or not</param>
		/// <returns>A list of extensions</returns>
		public static List<Models.Extension> GetByType(ExtensionType type, bool draft = false) {
			var ext = new List<Models.Extension>() ;

			Extensions.Where(extension => extension.ExtensionType.HasFlag(type)).ToList().ForEach(e => {
				ext.Add(new Models.Extension() {
					IsDraft = draft,
					Type = e.Type.ToString(),
					Body = (IExtension)Activator.CreateInstance(e.Type)
				}) ;
			});
			return ext ;
		}

		/// <summary>
		/// Gets the extensions available for the given type and entity.
		/// </summary>
		/// <param name="type">The extension type</param>
		/// <param name="id">The entity id</param>
		/// <param name="draft">Weather the entity is draft or not</param>
		/// <returns>A list of extensions</returns>
		public static List<Models.Extension> GetByTypeAndEntity(ExtensionType type, Guid id, bool draft) {
			var ret = new List<Models.Extension>() ;
			var tmp = GetByType(type, draft) ;

			foreach (var e in tmp) {
				var ext = Models.Extension.GetSingle("extension_type = @0 AND extension_parent_id = @1 AND extension_draft = @2",
					e.Type, id, draft) ;
				if (ext != null)
					ret.Add(ext) ;
				else ret.Add(e) ;
			}
			return ret ;
		}
	}
}
