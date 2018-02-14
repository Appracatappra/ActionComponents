using System;
namespace UIKit
{
	/// <summary>
	/// Specifies how a line will be drawn when using the simulated <c>UIKit</c> text drawing methods to port
	/// UI code from iOS to Android.
	/// </summary>
	public enum UILineBreakMode
	{
		/// <summary>
		/// Wraps the characters to the next line.
		/// </summary>
		CharacterWrap,

		/// <summary>
		/// Clips any characters that don't fit
		/// </summary>
		Clip,

		/// <summary>
		/// Trims off the start of the string.
		/// </summary>
		HeadTruncation,

		/// <summary>
		/// Trims out the middle of the string.
		/// </summary>
		MiddleTruncation,

		/// <summary>
		/// Trims off the tail of the string.
		/// </summary>
		TailTruncation,

		/// <summary>
		/// The word wrap.
		/// </summary>
		WordWrap
	}
}
