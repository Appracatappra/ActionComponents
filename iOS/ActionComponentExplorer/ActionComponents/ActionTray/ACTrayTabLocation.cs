using System;

namespace ActionComponents
{
	/// <summary>
	/// Controls where the <c>dragTab</c> is located on the <see cref="ActionComponents.ACTray"/> 
	/// </summary>
	/// <remarks>This location also interacts with the <see cref="ActionComponents.ACTrayOrientation"/> </remarks>
	public enum ACTrayTabLocation
	{
		/// <summary>
		/// The <c>dragTab</c> will be either on the top or left side of the <see cref="ActionComponents.ACTray"/> based on its
		/// <see cref="ActionComponents.ACTrayOrientation"/> 
		/// </summary>
		TopOrLeft,
		/// <summary>
		/// The <c>dragTab</c> will be in the middle of the <see cref="ActionComponents.ACTray"/> 
		/// </summary>
		Middle,
		/// <summary>
		/// The <c>dragTab</c> will be either on the bottom or the right side of the <see cref="ActionComponents.ACTray"/> based on its
		/// <see cref="ActionComponents.ACTrayOrientation"/> 
		/// </summary>
		BottomOrRight,
		/// <summary>
		/// The <c>dragTab</c> position will be based on a user defined <c>tabOffset</c> on the side of the <see cref="ActionComponents.ACTray"/> 
		/// </summary>
		/// <remarks>Based on the <see cref="ActionComponents.ACTray"/>'s <see cref="ActionComponents.ACTrayOrientation"/>, the
		/// <c>tabOffset</c> will be measured from the top or left side of the tray</remarks>
		Custom
	}
}

