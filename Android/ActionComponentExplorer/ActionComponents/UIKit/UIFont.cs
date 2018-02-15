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
using ActionComponents;

// Mappings "Unified" iOS types to Android types
using nfloat = global::System.Single;
using nint = global::System.Int32;
using nuint = global::System.UInt32;

namespace UIKit
{
	/// <summary>
	/// Simulates a iOS <c>UIFont</c> to ease the porting of UI code from iOS to Android. A <c>UIFont</c> holds information
	/// about the font selected for an upcoming draw operation and contains a <c>Paint</c> object that will be used in
	/// the drawing process. NOTE: Only a small percentage of <c>UIFont</c> has been ported to support the Action
	/// Components.
	/// </summary>
	public class UIFont
	{
		#region Static Properties
		/// <summary>
		/// Measures the size of the current string for the given font, available size and line break mode.
		/// </summary>
		/// <returns>The size.</returns>
		/// <param name="text">Text.</param>
		/// <param name="font">Font.</param>
		/// <param name="size">Size.</param>
		/// <param name="mode">Mode.</param>
		public static CGSize StringSize(string text, UIFont font, CGSize size, UILineBreakMode mode)
		{
			var metrics = font.FontPaint.GetFontMetricsInt();
			var lineHeight = -metrics.Ascent + metrics.Descent;
			var maxLines = size.Height / lineHeight;

			// Set minimum line
			if (maxLines < 1) maxLines = 1;

			// Calculate new height
			size.Height = ActionCanvasExtensions.TextHeight(text, (int)size.Width, (int)maxLines, font.FontPaint);

			// Return results
			return size;
		}

		/// <summary>
		/// Returns a system font of the given size.
		/// </summary>
		/// <returns>The font of size.</returns>
		/// <param name="size">Size.</param>
		public static UIFont SystemFontOfSize(nfloat size) {
			var font = new UIFont();
			font.TextSize = size;
			font.TextBold = false;
			return font;
		}

		/// <summary>
		/// Returns a bold system font of the given size.
		/// </summary>
		/// <returns>The system font of size.</returns>
		/// <param name="size">Size.</param>
		public static UIFont BoldSystemFontOfSize(nfloat size)
		{
			var font = new UIFont();
			font.TextSize = size;
			font.TextBold = true;
			return font;
		}
		#endregion

		#region Computed Properties
		/// <summary>
		/// Gets or sets the font paint.
		/// </summary>
		/// <value>The font paint.</value>
		public Paint FontPaint { get; set; } = new Paint();

		/// <summary>
		/// Gets or sets the size of the text.
		/// </summary>
		/// <value>The size of the text.</value>
		public nfloat TextSize {
			get { return FontPaint.TextSize; }
			set { FontPaint.TextSize = value; }
		}

		/// <summary>
		/// Gets or sets the color of the text.
		/// </summary>
		/// <value>The color of the text.</value>
		public ACColor TextColor {
			get { return FontPaint.Color; }
			set {
				FontPaint.Color = value;
				FontPaint.SetStyle(Paint.Style.Fill);
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="T:UIKit.UIFont"/> text is bold.
		/// </summary>
		/// <value><c>true</c> if text bold; otherwise, <c>false</c>.</value>
		public bool TextBold {
			get { return FontPaint.FakeBoldText; }
			set { FontPaint.FakeBoldText = value; }
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:UIKit.UIFont"/> class.
		/// </summary>
		public UIFont()
		{
			// Initialize
			FontPaint.AntiAlias = true;
		}
		#endregion
	}
}
