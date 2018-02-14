using System;
namespace ActionComponents
{
	/// <summary>
	/// Simulates the iOS <c>NSStringDrawingOptions</c> to ease in the porting of UI code from iOS to Android.
	/// </summary>
	public enum NSStringDrawingOptions
	{
		/// <summary>
		/// The disable screen font substitution. NOTE: macOS only.
		/// </summary>
		DisableScreenFontSubstitution,

		/// <summary>
		/// The one shot. NOTE: macOS only.
		/// </summary>
		OneShot,

		/// <summary>
		/// The truncates last visible line.
		/// </summary>
		TruncatesLastVisibleLine,

		/// <summary>
		/// The uses device metrics.
		/// </summary>
		UsesDeviceMetrics,

		/// <summary>
		/// The uses font leading.
		/// </summary>
		UsesFontLeading,

		/// <summary>
		/// The uses line fragment origin.
		/// </summary>
		UsesLineFragmentOrigin
	}
}
