using System;

namespace ActionComponents
{
	/// <summary>
	/// Defines the type of <see cref="ActionComponents.ACAlertButtonAppearance"/> being specified 
	/// </summary>
	internal enum ACAlertButtonApperanceType
	{
		/// <summary>
		/// The <see cref="ActionComponents.ACAlertButtonAppearance"/> that defines the normal state of the
		/// <see cref="ActionComponents.ACAlertButton"/>  
		/// </summary>
		Normal,
		/// <summary>
		/// The <see cref="ActionComponents.ACAlertButtonAppearance"/> that defines the touched state of the
		/// <see cref="ActionComponents.ACAlertButton"/>  
		/// </summary>
		Touched,
		/// <summary>
		/// The <see cref="ActionComponents.ACAlertButtonAppearance"/> that defines the highlighted state of the
		/// <see cref="ActionComponents.ACAlertButton"/>  
		/// </summary>
		Highlighted,

		/// <summary>
		/// The <see cref="ActionComponents.ACAlertButtonAppearance"/> that defines the disabled state of the
		/// <see cref="ActionComponents.ACAlertButton"/> 
		/// </summary>
		Disabled
	}
}

