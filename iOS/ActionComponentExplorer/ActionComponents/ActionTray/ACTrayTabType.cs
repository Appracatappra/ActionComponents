using System;

namespace ActionComponents
{
	/// <summary>
	/// Defines the type of <c>dragTab</c> for the given <see cref="ActionComponents.ACTray"/> 
	/// </summary>
	/// <remarks>the <c>appearance</c> property of the <see cref="ActionComponents.ACTray"/> controls the look/feel of the <c>dragTab</c> </remarks>
	public enum ACTrayTabType
	{
		/// <summary>
		/// A <c>Plain</c> <c>dragTab</c> contains no grip area, icon or text
		/// </summary>
		Plain,
		/// <summary>
		/// The <c>dragTab</c> contains only a three line grip area
		/// </summary>
		GripOnly,
		/// <summary>
		/// The <c>dragTab</c> contains a three line grip and a title
		/// </summary>
		GripAndTitle,
		/// <summary>
		/// The <c>dragTab</c> contains only a title
		/// </summary>
		TitleOnly,
		/// <summary>
		/// The <c>dragTab</c> contains only an icon
		/// </summary>
		IconOnly,
		/// <summary>
		/// The <c>dragTab</c> contains an icon and a title
		/// </summary>
		IconAndTitle,
		/// <summary>
		/// Allow the user to custom draw the <c>dragTab</c> inside the specified <c>dragTabRectangleF</c> and <see cref="ActionComponents.ACTray"/> 
		/// </summary>
		CustomDrawn
	}
}

