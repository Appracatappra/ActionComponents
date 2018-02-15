using System;
using System.Threading;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.Animation;
using Foundation;
using CoreGraphics;
using UIKit;
using ActionComponents;

// Mappings "Unified" iOS types to Android types
using nfloat = global::System.Single;
using nint = global::System.Int32;
using nuint = global::System.UInt32;

namespace Foundation
{
	/// <summary>
	/// Simulates an iOS <c>NSString</c> to ease the porting of UI code from iOS to Android. A <c>NSString</c> can be
	/// implicitly converted to and from a standard C# <c>String</c>.
	/// </summary>
	public class NSString
	{
		#region Override Operators
		/// <summary>
		/// Converts the <c>NSString</c> to a <c>String</c>.
		/// </summary>
		/// <returns>The implicitly converted <c>String</c>.</returns>
		/// <param name="text">Text.</param>
		public static implicit operator String(NSString text)
		{
			return text.Text;
		}

		/// <summary>
		/// Converts the <c>String</c> to a <c>NSString</c>.
		/// </summary>
		/// <returns>The implicitly converted <c>NSString</c>.</returns>
		/// <param name="text">Text.</param>
		public static implicit operator NSString(String text)
		{
			return new NSString(text);
		}
		#endregion

		#region Computed Properties
		/// <summary>
		/// Gets or sets the text of the string.
		/// </summary>
		/// <value>The text.</value>
		public String Text { get; set; } = "";
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Foundation.NSString"/> class.
		/// </summary>
		public NSString()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Foundation.NSString"/> class.
		/// </summary>
		/// <param name="text">Text.</param>
		public NSString(String text)
		{
			// Initialize
			this.Text = text;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Draws the string into the canvas from the current context of the <c>UIGraphics</c> class using the 
		/// given properties.
		/// </summary>
		/// <param name="rect">Rect.</param>
		/// <param name="font">Font.</param>
		/// <param name="mode">Mode.</param>
		/// <param name="alignment">Alignment.</param>
		public void DrawString(CGRect rect, UIFont font, UILineBreakMode mode, UITextAlignment alignment) {
			DrawString(rect, font, mode, alignment, TextBlockAlignment.Top);
		}

		/// <summary>
		/// Draws the string into the canvas from the current context of the <c>UIGraphics</c> class using the 
		/// given properties.
		/// </summary>
		/// <param name="rect">Rect.</param>
		/// <param name="font">Font.</param>
		/// <param name="mode">Mode.</param>
		/// <param name="alignment">Alignment.</param>
		/// <param name="verticalAlignment">vertical alignment.</param>
		public void DrawString(CGRect rect, UIFont font, UILineBreakMode mode, UITextAlignment alignment, TextBlockAlignment verticalAlignment) {
			var context = UIGraphics.CurrentContext;

			// Is a context open?
			if (context != null) {
				var canvas = UIGraphics.DrawCanvas;

				// Set the text color
				font.TextColor = context.CurrentPaint.Color;

				// Get the alignment mode
				var textAlign = TextBlockAlignment.Right;
				switch(alignment) {
					case UITextAlignment.Center:
						textAlign = TextBlockAlignment.Center;
						break;
					case UITextAlignment.Justified:
					case UITextAlignment.Left:
					case UITextAlignment.Natural:
						textAlign = TextBlockAlignment.Left;
						break;
					case UITextAlignment.Right:
						textAlign = TextBlockAlignment.Right;
						break;
				}

				// Take action based on the line break mode
				switch(mode) {
					case UILineBreakMode.CharacterWrap:
					case UILineBreakMode.WordWrap:
						ActionCanvasExtensions.DrawTextBlockInCanvas(canvas, Text, rect, font.FontPaint, textAlign);
						break;
					default:
						ActionCanvasExtensions.DrawTextAligned(canvas, Text, rect, font.FontPaint, textAlign, verticalAlignment);
						break;
				}
			}
		}

		/// <summary>
		/// Draws the string into the current graphics context inside of the given rect with the given attributes.
		/// </summary>
		/// <param name="rect">Rect.</param>
		/// <param name="attributes">Attributes.</param>
		public void DrawString(CGRect rect, UIStringAttributes attributes) {
			var context = UIGraphics.CurrentContext;

			// Is a context open?
			if (context != null)
			{
				var canvas = UIGraphics.DrawCanvas;
				var font = attributes.Font;

				// Set the text color
				font.TextColor = attributes.ForegroundColor;

				// Get the alignment mode
				var textAlign = TextBlockAlignment.Right;
				switch (attributes.ParagraphStyle.Alignment)
				{
					case UITextAlignment.Center:
						textAlign = TextBlockAlignment.Center;
						break;
					case UITextAlignment.Justified:
					case UITextAlignment.Left:
					case UITextAlignment.Natural:
						textAlign = TextBlockAlignment.Left;
						break;
					case UITextAlignment.Right:
						textAlign = TextBlockAlignment.Right;
						break;
				}

				// Take action based on the line break mode
				switch (attributes.ParagraphStyle.LineBreakMode)
				{
					case UILineBreakMode.CharacterWrap:
					case UILineBreakMode.WordWrap:
						ActionCanvasExtensions.DrawTextBlockInCanvas(canvas, Text, rect, font.FontPaint, textAlign);
						break;
					default:
						ActionCanvasExtensions.DrawTextAligned(canvas, Text, rect, font.FontPaint, textAlign, attributes.ParagraphStyle.VerticalAlignment);
						break;
				}
			}
		}

		/// <summary>
		/// Gets the bounding rect for the string.
		/// </summary>
		/// <returns>The bounding rect.</returns>
		/// <param name="size">Size.</param>
		/// <param name="options">Options.</param>
		/// <param name="attributes">Attributes.</param>
		/// <param name="location">Location.</param>
		public CGRect GetBoundingRect(CGSize size, NSStringDrawingOptions options, UIStringAttributes attributes, CGPoint location) {
			var rect = new CGRect();

			// Get text size
			rect.Size = UIFont.StringSize(Text, attributes.Font, size, UILineBreakMode.WordWrap);

			// Assemble results
			if (location != null) {
				rect.Location = location;
			}

			// Return results
			return rect;
		}
		#endregion
	}
}
