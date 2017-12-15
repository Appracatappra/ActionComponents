using System;
namespace ActionComponents
{
	/// <summary>
	/// Sets the state of a Nav Bar Button.
	/// </summary>
	public enum ACNavBarButtonState
	{
		/// <summary>
		/// The button is enabled.
		/// </summary>
		Enabled,

		/// <summary>
		/// The button is disabled.
		/// </summary>
		Disabled,

		/// <summary>
		/// The button is hidden.
		/// </summary>
		Hidden,

		/// <summary>
		/// The button is selected.
		/// </summary>
		Selected,

		/// <summary>
		/// The button is part of a selection group and is the currently selected button in the group.
		/// </summary>
		GroupSelected
	}
}
