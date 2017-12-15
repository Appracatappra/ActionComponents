using System;

namespace ActionComponents
{
	/// <summary>
	/// Used by the <c>ActionCanvasExtension</c> <c>DrawTextBlockInCanvas</c> method to specify the alignment for the text drawn.
	/// </summary>
	public enum TextBlockAlignment
	{
		/// <summary>
		/// The text will be left justified
		/// </summary>
		Left,
		/// <summary>
		/// The text will be centered
		/// </summary>
		Center,
		/// <summary>
		/// The text will be right justified
		/// </summary>
		Right,

		/// <summary>
		/// The text will be aligned with the top of the block
		/// </summary>
		Top,

		/// <summary>
		/// The text will be aligned with the bottom of the block
		/// </summary>
		Bottom
	}
}

