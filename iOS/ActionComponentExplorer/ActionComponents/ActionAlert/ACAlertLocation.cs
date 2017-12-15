using System;

namespace ActionComponents
{
	/// <summary>
	/// Defines the location that a <see cref="ActionComponents.ACAlert"/> will be displayed at. 
	/// </summary>
	public enum ACAlertLocation
	{
		/// <summary>
		/// The <see cref="ActionComponents.ACAlert"/> will be displayed at the top of the screen 
		/// </summary>
		Top,
		/// <summary>
		/// The <see cref="ActionComponents.ACAlert"/> will be displayed in the middle of the screen
		/// </summary>
		Middle,
		/// <summary>
		/// The <see cref="ActionComponents.ACAlert"/> will be displayed at the bottom of the screen
		/// </summary>
		Bottom,
		/// <summary>
		/// The <see cref="ActionComponents.ACAlert"/> will be displayed initially at the top left corner of the screen
		/// but can be positioned anywhere via code.
		/// </summary>
		Custom
	}
}

