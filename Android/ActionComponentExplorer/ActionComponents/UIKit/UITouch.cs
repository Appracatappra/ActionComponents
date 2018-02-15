using System;
using CoreGraphics;

namespace UIKit
{
	/// <summary>
	/// Represents a simulated iOS <c>UITouch</c> used to ease the porting of UI code from iOS to Android. NOTE: Only a
	/// small percentage of <c>UITouch</c> has been ported to support the Action Components.
	/// </summary>
	public class UITouch
	{
		#region Private Properties
		private CGPoint location;
		#endregion

		#region Public Properties

		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:UIKit.UITouch"/> class.
		/// </summary>
		/// <param name="location">Location.</param>
		public UITouch(CGPoint location)
		{
			// Initialize
			this.location = location;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Gets the precise location.
		/// </summary>
		/// <returns>The precise location.</returns>
		/// <param name="view">View.</param>
		public CGPoint GetPreciseLocation(UIView view) {
			return location;
		}

		/// <summary>
		/// Gets the precise previous location.
		/// </summary>
		/// <returns>The precise previous location.</returns>
		/// <param name="view">View.</param>
		public CGPoint GetPrecisePreviousLocation(UIView view) {
			return view.LastTouchPoint;
		}

		/// <summary>
		/// Locations the in view.
		/// </summary>
		/// <returns>The in view.</returns>
		/// <param name="view">View.</param>
		public CGPoint LocationInView(UIView view) {
			return location;
		}

		/// <summary>
		/// Previouses the location in view.
		/// </summary>
		/// <returns>The location in view.</returns>
		/// <param name="view">View.</param>
		public CGPoint PreviousLocationInView(UIView view) {
			return view.LastTouchPoint;
		}
  		#endregion
	}
}
