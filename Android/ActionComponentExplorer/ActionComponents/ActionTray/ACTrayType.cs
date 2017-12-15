using System;

namespace ActionComponents
{
	/// <summary>
	/// Defines the user's interaction with the <see cref="ActionComponents.ACTray"/> 
	/// </summary>
	public enum ACTrayType
	{
		/// <summary>
		/// The <see cref="ActionComponents.ACTray"/> can be dragged by its <c>dragTab</c> between
		/// the specified <c>openedPosition</c> and <c>closedPosition</c>
		/// </summary>
		/// <remarks>Double tapping the <c>dragTab</c> will snap the <see cref="ActionComponents.ACTray"/> between its 
		/// <c>openedPosition</c> and <c>closedPosition</c></remarks>
		Draggable,
		/// <summary>
		/// When the user taps the <see cref="ActionComponents.ACTray"/>'s <c>dragTab</c>, the tray will snap between
		/// its <c>openedPosition</c> and <c>closedPosition</c>
		/// </summary>
		Popup,
		/// <summary>
		/// When the user taps the <see cref="ActionComponents.ACTray"/>'s <c>dragTab</c>, the tray will snap between
		/// its <c>openedPostion</c> and <c>closedPostion</c>. If the tray is open and the user taps anywhere within its <c>content</c> area,
		/// the tray will automatically close.
		/// </summary>
		AutoClosingPopup
	}
}

