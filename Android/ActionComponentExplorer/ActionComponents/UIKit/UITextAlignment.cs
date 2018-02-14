using System;
namespace ActionComponents
{
	/// <summary>
	/// Simulates a iOS <c>UITextAlignment</c> enumeration to ease the porting of UI code from iOS to Android.
	/// </summary>
	public enum UITextAlignment
	{
		/// <summary>
		/// Centers the text.
		/// </summary>
		Center,

		/// <summary>
		/// Equally space the text across the available space on a line.
		/// </summary>
		Justified,

		/// <summary>
		/// Left-align the text.
		/// </summary>
		Left,

		/// <summary>
		/// Alignment is based on the text style.
		/// </summary>
		Natural,

		/// <summary>
		/// Right-aling the text.
		/// </summary>
		Right
	}
}
