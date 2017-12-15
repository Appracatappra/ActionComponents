using System;

namespace ActionComponents
{
	/// <summary>
	/// Defines all of the states a <see cref="ActionComponents.ACNavBarButton"/>  can be in
	/// </summary>
	/// <remarks>A botton's state can only be read and not directly set by the user. It is controlled by the parent 
	/// <see cref="ActionComponents.ACNavBar"/> </remarks>
	public enum ACNavBarButtonState {
		/// <summary>
		/// The button is enables and will respond to touch events
		/// </summary>
		Enabled,
		/// <summary>
		/// The button is disabled and will not respond to touch events
		/// </summary>
		Disabled,
		/// <summary>
		/// The button is hidden
		/// </summary>
		Hidden,
		/// <summary>
		/// The button is currently selected.
		/// </summary>
		/// <remarks>NOTE: Only one button can be selected at a given time</remarks>
		Selected
	}
}

