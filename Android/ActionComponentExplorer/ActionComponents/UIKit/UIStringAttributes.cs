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
	/// Simulates the iOS <c>UIStringAttributes</c> to ease the porting of UI code from iOS to Android. Note: Only a
	/// small percentage of <c>UIStringAttributes</c> has been ported to support Action Components.
	/// </summary>
	public class UIStringAttributes
	{
		#region Computed Properties
		/// <summary>
		/// Gets or sets the font.
		/// </summary>
		/// <value>The font.</value>
		public UIFont Font { get; set; } = UIFont.SystemFontOfSize(12f);

		/// <summary>
		/// Gets or sets the color of the foreground.
		/// </summary>
		/// <value>The color of the foreground.</value>
		public ACColor ForegroundColor { get; set; } = ACColor.Black;

		/// <summary>
		/// Gets or sets the color of the background.
		/// </summary>
		/// <value>The color of the background.</value>
		public ACColor BackgroundColor { get; set; } = ACColor.Clear;

		/// <summary>
		/// Gets or sets the paragraph style.
		/// </summary>
		/// <value>The paragraph style.</value>
		public NSMutableParagraphStyle ParagraphStyle { get; set; } = new NSMutableParagraphStyle();
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:UIKit.UIStringAttributes"/> class.
		/// </summary>
		public UIStringAttributes()
		{
		}
		#endregion
	}
}
