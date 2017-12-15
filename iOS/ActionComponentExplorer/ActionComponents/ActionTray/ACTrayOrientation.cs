using System;

namespace ActionComponents
{
	/// <summary>
	/// Defines the orientation of the <see cref="ActionComponents.ACTray"/> within the screen
	/// </summary>
	/// <remarks>This property also controls how the <see cref="ActionComponents.ACTray"/> responds to user interaction and where
	/// the <c>dragTab</c> is located</remarks>
	public enum ACTrayOrientation{
		/// <summary>
		/// The <see cref="ActionComponents.ACTray"/> is at the top of the screen 
		/// </summary>
		/// <remarks>The <c>dragTab</c> will be drawn at the bottom of the <see cref="ActionComponents.ACTray"/> </remarks>
		Top,
		/// <summary>
		/// The <see cref="ActionComponents.ACTray"/> is at the bottom of the screen
		/// </summary>
		/// <remarks>The <c>dragTab</c> will be drawn at the top of the <see cref="ActionComponents.ACTray"/> </remarks>
		Bottom,
		/// <summary>
		/// The <see cref="ActionComponents.ACTray"/> is on the left side of the screen 
		/// </summary>
		/// <remarks>The <c>dragTab</c> will be drawn on the right side of the <see cref="ActionComponents.ACTray"/> </remarks>
		Left,
		/// <summary>
		/// The <see cref="ActionComponents.ACTray"/> is on the ride side of the screen
		/// </summary>
		/// <remarks>The <c>dragTab</c> will be drawn on the left side of the <see cref="ActionComponents.ACTray"/> </remarks>
		Right
	}
}

