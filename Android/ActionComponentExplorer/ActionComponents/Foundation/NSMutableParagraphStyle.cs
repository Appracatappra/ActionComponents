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
using UIKit;

// Mappings "Unified" iOS types to Android types
using nfloat = global::System.Single;
using nint = global::System.Int32;
using nuint = global::System.UInt32;

namespace Foundation
{
	/// <summary>
	/// Simulates an iOS <c>NSMutableParagraphStyle</c> to ease the porting of UI code from iOS to Android.
	/// </summary>
	public class NSMutableParagraphStyle
	{
		#region Computed Properties
		/// <summary>
		/// Gets or sets the alignment.
		/// </summary>
		/// <value>The alignment.</value>
		public UITextAlignment Alignment { get; set; } = UITextAlignment.Left;

		/// <summary>
		/// Gets or sets the vertical alignment.
		/// </summary>
		/// <value>The vertical alignment.</value>
		public TextBlockAlignment VerticalAlignment { get; set; } = TextBlockAlignment.Top;

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="T:Foundation.NSMutableParagraphStyle"/> allows default
		/// tightening for truncation.
		/// </summary>
		/// <value><c>true</c> if allows default tightening for truncation; otherwise, <c>false</c>.</value>
		public bool AllowsDefaultTighteningForTruncation { get; set; } = true;

		/// <summary>
		/// Gets or sets the base writing direction.
		/// </summary>
		/// <value>The base writing direction.</value>
		public NSWritingDirection BaseWritingDirection { get; set; } = NSWritingDirection.LeftToRight;

		/// <summary>
		/// Gets or sets the default tab interval.
		/// </summary>
		/// <value>The default tab interval.</value>
		public nfloat DefaultTabInterval { get; set; } = 8f;

		/// <summary>
		/// Gets or sets the first line head indent.
		/// </summary>
		/// <value>The first line head indent.</value>
		public nfloat FirstLineHeadIndent { get; set; } = 0f;

		/// <summary>
		/// Gets or sets the head indent.
		/// </summary>
		/// <value>The head indent.</value>
		public nfloat HeadIndent { get; set; } = 0f;

		/// <summary>
		/// Gets or sets the hyphenation factor.
		/// </summary>
		/// <value>The hyphenation factor.</value>
		public int HyphenationFactor { get; set; } = 1;

		/// <summary>
		/// Gets or sets the line break mode.
		/// </summary>
		/// <value>The line break mode.</value>
		public UILineBreakMode LineBreakMode { get; set; } = UILineBreakMode.Clip;

		/// <summary>
		/// Gets or sets the line height multiple.
		/// </summary>
		/// <value>The line height multiple.</value>
		public nfloat LineHeightMultiple { get; set; } = 1f;

		/// <summary>
		/// Gets or sets the line spacing.
		/// </summary>
		/// <value>The line spacing.</value>
		public nfloat LineSpacing { get; set; } = 5f;

		/// <summary>
		/// Gets or sets the maximum height of the line.
		/// </summary>
		/// <value>The maximum height of the line.</value>
		public nfloat MaximumLineHeight { get; set; } = 1000f;

		/// <summary>
		/// Gets or sets the minimum height of the line.
		/// </summary>
		/// <value>The minimum height of the line.</value>
		public nfloat MinimumLineHeight { get; set; } = 5f;

		/// <summary>
		/// Gets or sets the paragraph spacing.
		/// </summary>
		/// <value>The paragraph spacing.</value>
		public nfloat ParagraphSpacing { get; set; } = 10f;

		/// <summary>
		/// Gets or sets the paragraph spacing befor.
		/// </summary>
		/// <value>The paragraph spacing befor.</value>
		public nfloat ParagraphSpacingBefor { get; set; } = 0f;

		/// <summary>
		/// Gets or sets the tab stops.
		/// </summary>
		/// <value>The tab stops.</value>
		public nfloat[] TabStops { get; set; }

		/// <summary>
		/// Gets or sets the tail indent.
		/// </summary>
		/// <value>The tail indent.</value>
		public nfloat TailIndent { get; set; } = 0f;
		#endregion

		#region Constructors
		public NSMutableParagraphStyle()
		{
		}
		#endregion
	}
}
