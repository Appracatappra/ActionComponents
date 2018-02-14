using System;
namespace ActionComponents
{
	/// <summary>
	/// Simulates the iOS <c>NSWritingDirection</c> enumeration for ease of porting UI code from iOS to Android.
	/// </summary>
	public enum NSWritingDirection
	{
		/// <summary>
		/// The left to right.
		/// </summary>
		LeftToRight,

		/// <summary>
		/// The writing direction is set by the script style.
		/// </summary>
		Natural,

		/// <summary>
		/// The right to left.
		/// </summary>
		RightToLeft
	}
}
