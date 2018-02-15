using System;
using System.Collections.Generic;
using Foundation;
using Android.Views;
using CoreGraphics;

namespace UIKit
{
	/// <summary>
	/// Represents a simulated iOS <c>UIEvent</c> used to ease the porting of UI code from iOS to Android. A <c>UIEvent</c>
	/// holds information about user interaction. NOTE: Only a small percentage of the <c>UIEvent</c> features have been
	/// ported to support the Action Components.
	/// </summary>
	public class UIEvent
	{
		#region Computed Properties
		/// <summary>
		/// Gets or sets the motion event that was the basis of the <c>UIEvent</c>.
		/// </summary>
		/// <value>The motion.</value>
		public MotionEvent Motion { get; set; }
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:UIKit.UIEvent"/> class.
		/// </summary>
		public UIEvent()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:UIKit.UIEvent"/> class.
		/// </summary>
		/// <param name="motion">Motion.</param>
		public UIEvent(MotionEvent motion)
		{
			// Initialize
			Motion = motion;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Fetches the UITouch for the specified view from the UIEvent.
		/// </summary>
		/// <returns>A set of touches for the view</returns>
		/// <param name="view">View.</param>
		public NSSet TouchesForView(UIView view) {
			var location = new CGPoint(Motion.GetX(), Motion.GetY());
			var touch = new UITouch(location);
			return new NSSet(touch);
		}
		#endregion
	}
}
