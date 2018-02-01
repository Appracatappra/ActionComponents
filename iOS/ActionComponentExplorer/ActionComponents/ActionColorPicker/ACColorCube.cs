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
	/// The Action Color Cube displays a color selection cube in the app's user interface that allows the user to
	/// move a pointer around to select the <c>Hue</c>, <c>Saturation</c> and <c>Brightness</c> of a generated color.
	/// </summary>
	[Register("ACColorCube")]
	public class ACColorCube : UIImageView 
	{
		#region Private Variables
		private nfloat _hue;
		private nfloat _saturation;
		private nfloat _brightness;
		private HSVColor _hsv;
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
				MoveIndicator ();
			}
		}

		/// <summary>
		/// Gets or sets the saturation.
		/// </summary>
		/// <value>The saturation.</value>
		public nfloat Saturation {
			get { return _saturation; }
			set {
				_saturation = value;
				MoveIndicator ();
			}
		}

		/// <summary>
		/// Gets or sets the brightness.
		/// </summary>
		/// <value>The brightness.</value>
		public nfloat Brightness {
			get { return _brightness; }
			set {
				_brightness = value;
				MoveIndicator ();
			}
		}

		/// <summary>
		/// Gets the HSV color
		/// </summary>
		/// <value>The HS.</value>
		public HSVColor HSV {
			get { return _hsv; }
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACColorCube"/> class.
		/// </summary>
		public ACColorCube () : base()
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACColorCube"/> class.
		/// </summary>
		/// <param name="coder">Coder.</param>
		public ACColorCube (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACColorCube"/> class.
		/// </summary>
		/// <param name="flag">Flag.</param>
		public ACColorCube (NSObjectFlag flag) : base(flag)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACColorCube"/> class.
		/// </summary>
		/// <param name="bounds">Bounds.</param>
		public ACColorCube (CGRect bounds) : base(bounds)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACColorCube"/> class.
		/// </summary>
		/// <param name="ptr">Ptr.</param>
		public ACColorCube (IntPtr ptr) : base(ptr)
		{
			Initialize ();
		}

		/// <summary>
		/// Initialize this instance.
		/// </summary>
		private void Initialize(){

			// Initialize
			this.UserInteractionEnabled = true;
			this._hue = 0f;
			this._saturation = 1f;
			this._brightness = 1f;

			// Add indicator
			_indicator = new ACColorIndicator (new CGRect (0, 0, _indicatorSize, _indicatorSize));
			AddSubview (_indicator);

			// Do initial draw
			Redraw ();
			MoveIndicator ();
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Redraws the color cube using the current <c>Hue</c>, <c>Saturation</c> and <c>Brightness</c> values.
		/// </summary>
		public void Redraw() {

			// Is there already an image?
			if (Image != null) {
				// Yes, release it's memory
				Image.Dispose ();
				Image = null;
			}

			// Set new image background
			Image = HSVImage.SaturationBrightnessSquareImage (Hue);

		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Moves the indicator.
		/// </summary>
		private void MoveIndicator() {

			// Calculate a new color for the new location
			_hsv = new HSVColor (Hue, Saturation, Brightness);
			_indicator.Color = _hsv.Color;

			// Inform caller of color change
			RaiseColorChanged (HSV.Color);

			// Move indicator
			_indicator.Center = new CGPoint(Saturation * Bounds.Width, Bounds.Height - (Brightness * Bounds.Height));
		}

		/// <summary>
		/// Tracks the indicator touch.
		/// </summary>
		/// <param name="pt">Point.</param>
		private void TrackIndicatorTouch(CGPoint pt) {

			// Set saturation
			nfloat percent = pt.X / Bounds.Width;
			_saturation = HSVImage.Pin (0f, percent, 1f);

			// Set brightness
			percent = 1.0f - (pt.Y / Bounds.Height);
			_brightness = HSVImage.Pin (0f, percent, 1f);

			// Update indicator
			MoveIndicator ();

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

		/// <summary>
		/// Toucheses the ended.
		/// </summary>
		/// <param name="touches">Touches.</param>
		/// <param name="evt">Evt.</param>
		public override void TouchesEnded (NSSet touches, UIEvent evt)
		{
			#if TRIAL
			ACToast.MakeText("ACColorCube by Appracatappra, LLC.", ACToastLength.Short).Show();
			#endif

			//Pass call to base object
			if (! ExclusiveTouch) 
				base.TouchesMoved(touches, evt);
		}
		#endregion

		#region Events
		/// <summary>
		/// Color changed delegate.
		/// </summary>
		public delegate void ColorChangedDelegate(UIColor color);

		/// <summary>
		/// Occurs when color changed.
		/// </summary>
		public event ColorChangedDelegate ColorChanged;

		/// <summary>
		/// Raises the color changed.
		/// </summary>
		/// <param name="color">Color.</param>
		internal void RaiseColorChanged(UIColor color) {
			// Inform caller
			if (this.ColorChanged != null)
				this.ColorChanged (color);
		}
		#endregion
	}
}

