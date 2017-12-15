using System;
namespace ActionComponents
{
	/// <summary>
	/// Defines the type of a Nav Bar Button.
	/// </summary>
	public enum ACNavBarButtonType
	{
		/// <summary>
		/// The button loads the attached view when it is selected.
		/// </summary>
		View,

		/// <summary>
		/// The button loads the attached view when it is selected and automatically disposes of the view from memory
		/// when the button is unselected.
		/// </summary>
		AutoDisposingView,

		/// <summary>
		/// The button is a tool that performs an action when pressed.
		/// </summary>
		Tool,

		/// <summary>
		/// The button is part of a selection group. Only one item in the group can be selected at any time.
		/// </summary>
		Selection,

		/// <summary>
		/// The button is a notification displayed to the user.
		/// </summary>
		Notification
	}
}
