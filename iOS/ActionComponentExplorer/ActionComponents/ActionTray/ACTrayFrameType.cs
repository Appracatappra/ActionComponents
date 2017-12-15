using System;

namespace ActionComponents
{
	/// <summary>
	/// Controls the frame drawn around a <see cref="ActionComponents.ACTray"/> 
	/// </summary>
	/// <remarks>The frame interacts with the <see cref="ActionComponents.ACTrayOrientation"/> property</remarks>
	public enum ACTrayFrameType
	{
		/// <summary>
		/// No frame will be drawn around the <see cref="ActionComponents.ACTray"/> 
		/// </summary>
		/// <remarks>The <c>dragTab</c> will be drawn in the <see cref="ActionComponents.ACTray"/>'s 
		/// <see cref="ActionComponents.ACTrayAppearance"/> <c>background</c> color </remarks>
		None,
		/// <summary>
		/// Only the edge of the <see cref="ActionComponents.ACTray"/> containing the <c>dragTab</c>
		/// will be framed
		/// </summary>
		/// <remarks>The <see cref="ActionComponents.ACTrayOrientation"/> controls which edge gets the frame </remarks>
		EdgeOnly,
		/// <summary>
		/// All edges of the <see cref="ActionComponents.ACTray"/> except the back one will receive the frame 
		/// </summary>
		/// <remarks>The <see cref="ActionComponents.ACTrayOrientation"/> defines the back edge of the 
		/// <see cref="ActionComponents.ACTray"/> </remarks>
		EdgeAndSides,
		/// <summary>
		/// All edges of the <see cref="ActionComponents.ACTray"/> will receive the frame 
		/// </summary>
		FullFrame
	}
}

