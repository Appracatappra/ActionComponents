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
	/// Simulates several features of an iOS <c>UIImage</c> to make porting UI code from iOS to Android
	/// easier. 
	/// </summary>
	public class UIImage
	{
		#region Override Operators
		/// <summary>
		/// Converts the <c>UIImage</c> to a <c>Bitmap</c>.
		/// </summary>
		/// <returns>The implicitly converted <c>Bitmap</c>.</returns>
		/// <param name="image">Image.</param>
		public static implicit operator Bitmap(UIImage image) {
			return image.Image;
		}

		/// <summary>
		/// Converts the <c>Bitmap</c> to a <c>UIImage</c>.
		/// </summary>
		/// <returns>The implicitly converted <c>UIImage</c>.</returns>
		/// <param name="image">Image.</param>
		public static implicit operator UIImage(Bitmap image) {
			return new UIImage(image);
		}
		#endregion

		#region Static Methods
		/// <summary>
		/// Loads the given image from the Resources > Drawable folder.
		/// </summary>
		/// <returns>The requested <c>UIImage</c>.</returns>
		/// <param name="resources">Resources.</param>
		/// <param name="resourceID">Resource identifier.</param>
		public static UIImage FromResources(Android.Content.Res.Resources resources, int resourceID) {
			return new UIImage(resources, resourceID);
		}
		#endregion

		#region Computed Properties
		/// <summary>
		/// Gets or sets the image bitmap.
		/// </summary>
		/// <value>The image bitmap.</value>
		public Bitmap Image { get; set; } = null;

		/// <summary>
		/// Gets or sets the default image paint used to draw the image.
		/// </summary>
		/// <value>The image paint.</value>
		public Paint ImagePaint { get; set; } = new Paint();

		/// <summary>
		/// Gets the image size.
		/// </summary>
		/// <value>The size.</value>
		public CGSize Size {
			get {
				var size = new CGSize(0, 0);
					
				// Is a canvas open?
				if (UIGraphics.DrawCanvas != null && Image != null) {
					size.Width = Image.GetScaledWidth(UIGraphics.DrawCanvas);
					size.Height = Image.GetScaledHeight(UIGraphics.DrawCanvas);
				}

				// Return results
				return size;
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:UIKit.UIImage"/> class.
		/// </summary>
		public UIImage()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:UIKit.UIImage"/> class.
		/// </summary>
		/// <param name="image">Image.</param>
		public UIImage(Bitmap image)
		{
			// Initialize
			this.Image = image;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:UIKit.UIImage"/> class.
		/// </summary>
		/// <param name="resources">Resources.</param>
		/// <param name="resourceID">Resource identifier.</param>
		public UIImage(Android.Content.Res.Resources resources, int resourceID)
		{
			// Initialize
			this.Image = BitmapFactory.DecodeResource(resources, resourceID);
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Draws the image into the currently active canvas as specified in the global <c>UIGraphics</c> class.
		/// </summary>
		/// <param name="rect">The location and size to draw the image.</param>
		public void Draw(CGRect rect) {
			var canvas = UIGraphics.DrawCanvas;
			if (canvas != null) {
				var img = Bitmap.CreateScaledBitmap(Image, (int)rect.Width, (int)rect.Height, true);
				canvas.DrawBitmap(img, null, (RectF)rect, ImagePaint);
			}
		}
		#endregion
	}
}
