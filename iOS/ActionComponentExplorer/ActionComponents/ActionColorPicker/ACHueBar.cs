using System;
using UIKit;
using CoreImage;
using CoreGraphics;
using System.Runtime.InteropServices;
using CoreGraphics;
using Foundation;

namespace ActionComponents
{
	/// <summary>
	/// The Action Color Hue Bar presents a hue selection bar to the user that they can interact with to select the 
	/// current color. This control is typically used with an <c>ACColorCube</c> to present the full color selection
	/// UI.
	/// </summary>
	[Register("ACHueBar")]
	public class ACHueBar : UIImageView
	{
		#region Private Variables
		private nfloat _hue;
		private nfloat _indicatorSize = 24f;
		private ACColorIndicator _indicator;
		#endregion

		#region Computed Properties
		/// <summary>
		/// Gets or sets the hue.
		/// </summary>
		/// <value>The hue.</value>
		public nfloat Hue {
			get { return _hue; }
			set {
				_hue = value;
				Redraw ();
				RaiseHueChanged (_hue);
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACHueBar"/> class.
		/// </summary>
		public ACHueBar () : base()
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACHueBar"/> class.
		/// </summary>
		/// <param name="coder">Coder.</param>
		public ACHueBar (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACHueBar"/> class.
		/// </summary>
		/// <param name="flag">Flag.</param>
		public ACHueBar (NSObjectFlag flag) : base(flag)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACHueBar"/> class.
		/// </summary>
		/// <param name="bounds">Bounds.</param>
		public ACHueBar (CGRect bounds) : base(bounds)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACHueBar"/> class.
		/// </summary>
		/// <param name="ptr">Ptr.</param>
		public ACHueBar (IntPtr ptr) : base(ptr)
		{
			Initialize ();
		}

		/// <summary>
		/// Initialize this instance.
		/// </summary>
		private void Initialize(){

			// Initialize
			this.UserInteractionEnabled = true;
			this._hue = 0;

			// Fill background
			HSVColor hsv = new HSVColor (0.0f, 1.0f, 1.0f);
			Image = HSVImage.HueBarImage (ACHueBarComponentIndex.ComponentIndexHue, hsv);

			// Add indicator
			_indicator = new ACColorIndicator (new CGRect (0, 0, _indicatorSize, _indicatorSize));
			AddSubview (_indicator);

			// Do initial draw
			Redraw ();
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Redraw the Hue Bar using the current <c>Hue</c> value.
		/// </summary>
		public void Redraw() {

			// Set color
			HSVColor hsv = new HSVColor (Hue, 1.0f, 1.0f);
			_indicator.Color = hsv.Color; //UIColor.FromHSB(Hue, 1.0f, 1.0f);

			// Calculate indicator location
			nfloat x = Hue * Bounds.Width;
			_indicator.Center = new CGPoint (x, Bounds.GetMidY ());
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Tracks the indicator touch.
		/// </summary>
		/// <param name="pt">Point.</param>
		private void TrackIndicatorTouch(CGPoint pt) {

			// Calculate percentage
			nfloat percent = pt.X / Bounds.Width;

			// Set new value
			Hue = HSVImage.Pin (0f, percent, 1f);

		}
		#endregion

		#region Override Methods
		/// <summary>
		/// Toucheses the began.
		/// </summary>
		/// <param name="touches">Touches.</param>
		/// <param name="evt">Evt.</param>
		public override void TouchesBegan (NSSet touches, UIEvent evt)
		{
			// Save starting location
			var pt = (touches.AnyObject as UITouch).LocationInView(this);

			#if TRIAL
			ACToast.MakeText("ACHueBar by Appracatappra, LLC.", ACToastLength.Short).Show();
			#endif

			// Track user interaction
			TrackIndicatorTouch (pt);

			// Pass call to base object
			if (! ExclusiveTouch) 
				base.TouchesBegan (touches, evt);
		}

		/// <summary>
		/// Toucheses the moved.
		/// </summary>
		/// <param name="touches">Touches.</param>
		/// <param name="evt">Evt.</param>
		public override void TouchesMoved (NSSet touches, UIEvent evt)
		{
			// Save starting location
			var pt = (touches.AnyObject as UITouch).LocationInView(this);

			// Track user interaction
			TrackIndicatorTouch (pt);
			//Pass call to base object
			if (! ExclusiveTouch) 
				base.TouchesMoved(touches, evt);
		}
		#endregion

		#region Events
		/// <summary>
		/// Hue changed delegate.
		/// </summary>
		public delegate void HueChangedDelegate(nfloat hue);

		/// <summary>
		/// Occurs when hue changed.
		/// </summary>
		public event HueChangedDelegate HueChanged;

		/// <summary>
		/// Raises the hue changed.
		/// </summary>
		/// <param name="hue">Hue.</param>
		internal void RaiseHueChanged(nfloat hue) {
			// Inform caller
			if (this.HueChanged != null)
				this.HueChanged (hue);
		}
		#endregion
	}
}

