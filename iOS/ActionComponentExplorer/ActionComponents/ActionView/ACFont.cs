using System;
using Foundation;
using UIKit;
using CoreGraphics;

namespace ActionComponents
{
	/// <summary>
	/// This class contains a set of utility routines for working with fonts.
	/// </summary>
	public class ACFont
	{
		#region Static Methods
		/// <summary>
		/// Acts as a replacement for the <c>UIKit.UIView.StringSize</c> method deprecated in iOS7. This function 
		/// calculates the width and height of a single line of the specified string if it were rendered with the specified: 
		/// font, width constraint, and line-break mode.
		/// </summary>
		/// <returns>The height and width constrained to the given maximum sizes.</returns>
		/// <param name="text">The text to measure.</param>
		/// <param name="font">The font that will be used to draw the string.</param>
		/// <param name="contrainedTo">The maximum width and height.</param>
		/// <param name="breakMode">The line breaking mode.</param>
		public static CGSize StringSize(string text, UIFont font, CGSize contrainedTo, UILineBreakMode breakMode) {
			var str = new NSString(text);
			var attributes = new UIStringAttributes();

			// Configure
			attributes.Font = font;
			var results = str.GetBoundingRect(contrainedTo, NSStringDrawingOptions.UsesLineFragmentOrigin, attributes, null);

			// Return results
			return new CGSize(results.Width, results.Height);
		}
		#endregion

	}
}
