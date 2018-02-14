using System;
using System.Collections.Generic;
using Foundation;
using Android.Views;
using CoreGraphics;

namespace UIKit
{
	/// <summary>
	/// UIEvents contain a collection of all the touches (UITouch) taking place on the screen.
	/// </summary>
	public class UIEvent
	{
		#region Computed Properties
		public MotionEvent Motion { get; set; }
		#endregion

		#region Constructors
		public UIEvent()
		{
		}

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
