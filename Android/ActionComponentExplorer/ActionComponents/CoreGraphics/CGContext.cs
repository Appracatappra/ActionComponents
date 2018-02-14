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
	/// Represents a simulated iOS <c>CGContext</c> used to ease the porting of UI code from iOS to Android.
	/// </summary>
	public class CGContext
	{
		#region Computed Properties
		/// <summary>
		/// Gets or sets the current view that this context belongs to.
		/// </summary>
		/// <value>The view.</value>
		public UIView View { get; set; } = null;

		/// <summary>
		/// Gets or sets the current paint.
		/// </summary>
		/// <value>The current paint.</value>
		public Paint CurrentPaint { get; set; } = new Paint();

		/// <summary>
		/// Gets or sets the previous paint.
		/// </summary>
		/// <value>The previous paint.</value>
		public Paint PreviousPaint { get; set; } = new Paint();

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="T:UIKit.CGContext"/> has shadow.
		/// </summary>
		/// <value><c>true</c> if has shadow; otherwise, <c>false</c>.</value>
		public bool HasShadow { get; set; } = false;

		/// <summary>
		/// Gets or sets the shadow offset.
		/// </summary>
		/// <value>The shadow offset.</value>
		public CGSize ShadowOffset { get; set; } = new CGSize(0, 0);

		/// <summary>
		/// Gets or sets the shadow blur radius.
		/// </summary>
		/// <value>The shadow blur radius.</value>
		public nint ShadowBlurRadius { get; set; } = 1;

		/// <summary>
		/// Gets or sets the color of the shadow.
		/// </summary>
		/// <value>The color of the shadow.</value>
		public ACColor ShadowColor { get; set; } = ACColor.FromRGBA(50, 50, 50, 50);
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:UIKit.CGContext"/> class.
		/// </summary>
		/// <param name="view">View.</param>
		public CGContext(UIView view)
		{
			// Initialize
			this.View = view;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Saves the state.
		/// </summary>
		public void SaveState() {
			// Save the current pain state
			PreviousPaint = CurrentPaint;
		}

		/// <summary>
		/// Restores the state.
		/// </summary>
		public void RestoreState() {
			CurrentPaint = PreviousPaint;
			HasShadow = false;
		}

		/// <summary>
		/// Sets the shadow for the current context.
		/// </summary>
		/// <param name="offset">Offset.</param>
		/// <param name="blurRadius">Blur radius.</param>
		/// <param name="color">Color.</param>
		public void SetShadow(CGSize offset, nint blurRadius, ACColor color) {
			HasShadow = true;
			ShadowOffset = offset;
			ShadowBlurRadius = blurRadius;
			ShadowColor = color;
		}

		/// <summary>
		/// Clips drawing to the given rect.
		/// </summary>
		/// <param name="rect">Rect.</param>
		public void ClipToRect(CGRect rect) {
			
		}
		#endregion
	}
}
