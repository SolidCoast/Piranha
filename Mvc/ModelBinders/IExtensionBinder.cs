using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Piranha.Extend ;

namespace Piranha.Mvc.ModelBinders
{
	/// <summary>
	/// Binds an IRegion object.
	/// </summary>
	public class IExtensionBinder : DefaultModelBinder {
		/// <summary>
		/// Creates the appropriate region body.
		/// </summary>
		/// <param name="cc">The controller context</param>
		/// <param name="bc">The binding context</param>
		/// <param name="t">The current type being created.</param>
		/// <returns>The model</returns>
		protected override object CreateModel(ControllerContext cc, ModelBindingContext bc, Type t) {
			return Activator.CreateInstance(GetBodyType(bc)) ;
		}

		/// <summary>
		/// Binds the current region body.
		/// </summary>
		/// <param name="cc">The controller context</param>
		/// <param name="bc">The binding context</param>
		/// <returns>The bound model</returns>
		public override object BindModel(ControllerContext cc, ModelBindingContext bc) {
			// Set up correct meta data.
			bc.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, GetBodyType(bc)) ;

			// Use the default binding.
			return base.BindModel(cc, bc);
		}

		#region Private methods
		/// <summary>
		/// Gets the body type for the current region.
		/// </summary>
		/// <param name="bc">The binding context</param>
		/// <returns>The body type</returns>
		private Type GetBodyType(ModelBindingContext bc) {
			var typeparam = bc.ModelName.Substring(0, bc.ModelName.LastIndexOf('.')) + ".Type" ;
			string typename = (string)bc.ValueProvider.GetValue(typeparam).ConvertTo(typeof(string)) ;
			return ExtensionManager.ExtensionTypes[typename] ;
		}
		#endregion
	}
}