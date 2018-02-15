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
	/// Simulates a iOS <c>UIGraphics</c> static global class to ease the porting of UI code from iOS to Android. NOTE:
	/// Only a small percentage of <c>UIGraphics</c> has been ported to support the Action Components.
	/// </summary>
	public static class UIGraphics
	{
		#region Static Variable Storage
		/// <summary>
		/// Gets or sets the current context.
		/// </summary>
		/// <value>The current context.</value>
		public static CGContext CurrentContext { get; set; } = null;

		/// <summary>
		/// Gets the draw canvas.
		/// </summary>
		/// <value>The draw canvas.</value>
		public static Canvas DrawCanvas
		{
			get {
				if (CurrentContext == null) {
					return null;
				} else {
					return CurrentContext.View.DrawCanvas;
				}
			}
		}
		#endregion

		#region Static Methods
		/// <summary>
		/// Gets the current context. NOTE: Unlike the iOS version, the developer needs to pass a reference to the
		/// <c>UIView</c> that is requesting the context.
		/// </summary>
		/// <returns>The current context.</returns>
		/// <param name="view">View.</param>
		public static CGContext GetCurrentContext(UIView view) {
			CurrentContext = new CGContext(view);
			return CurrentContext;
		}

		/// <summary>
		/// Releases the current context and frees memory..
		/// </summary>
		public static void ReleaseCurrentContext() {
			// Release the current context
			CurrentContext = null;
		}
		#endregion
	}
}
