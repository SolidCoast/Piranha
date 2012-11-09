using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Piranha.Models
{
	/// <summary>
	/// Class for defining a name in both singular and plural.
	/// </summary>
	public class ComplexName {
		/// <summary>
		/// Gets/sets the name in its singular form.
		/// </summary>
		[Display(ResourceType=typeof(Piranha.Resources.Global), Name="NameInSingular")]
		[Required(ErrorMessageResourceType=typeof(Piranha.Resources.Global), ErrorMessageResourceName="NameRequired")]
		public string Singular { get ; set ; }

		/// <summary>
		/// Gets/sets the name in its plural form.
		/// </summary>
		[Display(ResourceType=typeof(Piranha.Resources.Global), Name="NameInPlural")]
		[Required(ErrorMessageResourceType=typeof(Piranha.Resources.Global), ErrorMessageResourceName="NameRequired")]
		public string Plural { get ; set ; }
	}
}
