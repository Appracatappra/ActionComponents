using System;

namespace ActionComponents
{
	/// <summary>
	/// Defines the type of a given <see cref="ActionComponents.ACNavBarButton"/> 
	/// </summary>
	/// <remarks>The <see cref="ActionComponents.ACNavBarButtonType"/> controlls how the button is displayed in the 
	/// parent <see cref="ActionComponents.ACNavBar"/> and what happens when the user touches the button</remarks>
	public enum ACNavBarButtonType{
		/// <summary>
		/// Handles showing an attached <c>View</c> created by the user and moves the <see cref="ActionComponents.ACNavBarPointer"/> to
		/// mark this button as selected
		/// </summary>
		/// <remarks>If no view is attached to the <see cref="ActionComponents.ACNavBarButton"/> and it is touched by the user a <c>RequestNewView</c> event will
		/// be raised. If a view is attached it will be automatically displayed.</remarks>
		View,
		/// <summary>
		/// Handles showing an attached <c>View</c> created by the user, moves the <see cref="ActionComponents.ACNavBarPointer"/> to mark this
		/// button as selected and automatically disposes of the attached view when the button loses focus
		/// </summary>
		/// <remarks>When the user touches this button a <c>RequestNewView</c> event is raised. When this button is unselected the attached <c>UIView</c> is removed
		/// from the superview and disposed of to automatically release memory.</remarks>
		AutoDisposingView,
		/// <summary>
		/// NavBar button tool does not handle views or move the pointer but responds to touch
		/// </summary>
		/// <remarks>This type of button does not controll an attached <c>View</c> but simply raises a <c>Touched</c> event when touched
		/// by the user.</remarks>
		Tool,
		/// <summary>
		/// NavBar notification button does not handle views or touch and is for display only
		/// </summary>
		/// <remarks>This type of button is for display ONLY and neither controlls an attached <c>View</c> nor responds to user touches.</remarks>
		Notification
	}
}

