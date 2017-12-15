using System;
using System.Drawing;

using Foundation;
using UIKit;
using CoreGraphics;

namespace ActionComponents
{
	/// <summary>
	/// The <see cref="ActionComponents.ACAlertOverlay"/> is used to obscure the background for modal
	/// <see cref="ActionComponents.ACAlert"/>s and keep touch events from bubbling down. 
	/// </summary>
	public class ACAlertOverlay : UIView
	{
		#region Private variables
		private ACAlert _alert;
		#endregion 

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACAlertOverlay"/> class.
		/// </summary>
		/// <param name="alert">Alert.</param>
		public ACAlertOverlay (ACAlert alert)
		{
			//Initialize
			this._alert = alert;
			this.Frame = new CGRect(0,0,iOSDevice.RotatedScreenBounds.Width,iOSDevice.RotatedScreenBounds.Height);
			this.UserInteractionEnabled = true;
			this.ExclusiveTouch = true;

			// Set the AutoresizingMask to anchor the view to the top left but
			// allow height and width to grow
			this.AutoresizingMask = (UIViewAutoresizing.FlexibleRightMargin | UIViewAutoresizing.FlexibleBottomMargin | UIViewAutoresizing.FlexibleHeight | UIViewAutoresizing.FlexibleWidth);

			//Adjust appearance
			AdjustAppearance ();
		}
		#endregion 

		#region Public Methods
		/// <summary>
		/// Adjusts the appearance.
		/// </summary>
		public void AdjustAppearance() {
			this.BackgroundColor = _alert.appearance.overlay;
			this.Alpha = _alert.appearance.overlayAlpha;
		}
		#endregion 

		#region Override Methods
		/// <Docs>Set containing the touches.</Docs>
		/// <summary>
		/// Sent when one or more fingers touches the screen.
		/// </summary>
		/// <param name="touches">Touches.</param>
		/// <param name="evt">Evt.</param>
		public override void TouchesBegan (NSSet touches, UIEvent evt)
		{
			//Inform caller of event
			RaiseTouched ();

			//Pass call to base object
			base.TouchesBegan (touches, evt);
		}
		#endregion

		#region Events
		/// <summary>
		/// Occurs when this <see cref="ActionComponents.ACAlertOverlay"/> is touched 
		/// </summary>
		public delegate void ACAlertOverlayTouchedDelegate ();
		public event ACAlertOverlayTouchedDelegate Touched;

		/// <summary>
		/// Raises the touched event
		/// </summary>
		private void RaiseTouched(){
			if (this.Touched != null)
				this.Touched ();
		}
		#endregion 
	}
}

