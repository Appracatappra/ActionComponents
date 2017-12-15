using System;

namespace ActionComponents
{
	/// <summary>
	/// Defines the type of <see cref="ActionComponents.ACAlert"/> that will be created and displayed 
	/// </summary>
	public enum ACAlertType
	{
		/// <summary>
		/// A standard <see cref="ActionComponents.ACAlert"/> that contains an optional <c>title</c>, <c>description</c> and/or <c>icon</c>. The
		/// alert can also contain one or more <see cref="ActionComponents.ACAlertButton"/>s used to interact with the end user 
		/// </summary>
		Default,
		/// <summary>
		/// Like the <c>Default</c> <see cref="ActionComponents.ACAlert"/> but includes an <c>Activity Indicator</c> in place of the 
		/// optional <c>icon</c>.
		/// </summary>
		ActivityAlert,
		/// <summary>
		/// Displays a medium sized <see cref="ActionComponents.ACAlert"/> with an embedded <c>Activity Indicator</c> with an optional title and/or
		/// <c>Cancel</c> <see cref="ActionComponents.ACAlertButton"/> 
		/// </summary>
		ActivityAlertMedium,
		/// <summary>
		/// Like the <c>Default</c> <see cref="ActionComponents.ACAlert"/> but includes a <c>Progress Indicator</c>.
		/// </summary>
		ProgressAlert,
		/// <summary>
		/// Allows you to insert a custom subview into the <see cref="ActionComponents.ACAlert"/>
		/// </summary>
		Subview
	}
}

