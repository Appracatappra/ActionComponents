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
	/// The Action Color Well displays a framed color well that shows the currently selected color and allows the user
	/// to tap the well which raises the <c>Touched</c> event.
	/// </summary>
	[Register("ACColorWell")]
	public class ACColorWell : UIView 
	{
		#region Private Variables
		private UIColor _color;
		#endregion

		#region Computed Properties
		/// <summary>
		/// Gets or sets the color.
		/// </summary>
		/// <value>The color.</value>
		public UIColor Color {
			get { return _color; }
			set {
				_color = value;
				Redraw ();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="T:ActionComponents.ACColorWell"/> will
		/// automatically present a HSB Color Picker to select a new color.
		/// </summary>
		/// <value><c>true</c> if auto present a HSB Color Picker; otherwise, <c>false</c>.</value>
		/// <remarks>The <c>ParentViewController</c> property MUST be set to the <c>UIViewController</c> that
		/// the <c>ACColorWell</c> is hosted in before the HSB Color Picker can be automatically 
		/// presented.</remarks>
		public bool AutoPresentHSBColorPicker { get; set; } = true;

		/// <summary>
		/// Gets or sets the color picker title that is display when the <c>AutoPresentHSBColorPicker</c> property
		/// is set to <c>true</c>.
		/// </summary>
		/// <value>The color picker title.</value>
		public string ColorPickerTitle { get; set; } = "Select a Color";

		/// <summary>
		/// Gets or sets the parent view controller of the Color Well. This property will need to be set before the
		/// <c>AutoPresentHSBColorPicker</c> will work correctly.
		/// </summary>
		/// <value>The parent view controller.</value>
		public UIViewController ParentViewController { get; set; } = null;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACColorWell"/> class.
		/// </summary>
		public ACColorWell () : base()
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACColorWell"/> class.
		/// </summary>
		/// <param name="coder">Coder.</param>
		public ACColorWell (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACColorWell"/> class.
		/// </summary>
		/// <param name="flag">Flag.</param>
		public ACColorWell (NSObjectFlag flag) : base(flag)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACColorWell"/> class.
		/// </summary>
		/// <param name="bounds">Bounds.</param>
		public ACColorWell (CGRect bounds) : base(bounds)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACColorWell"/> class.
		/// </summary>
		/// <param name="ptr">Ptr.</param>
		public ACColorWell (IntPtr ptr) : base(ptr)
		{
			Initialize ();
		}

		/// <summary>
		/// Initialize this instance.
		/// </summary>
		private void Initialize(){

			// Initialize
			this.UserInteractionEnabled = true;
			this._color = UIColor.Gray;

			// Do initial draw
			Redraw ();
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Redraw the color well.
		/// </summary>
		public void Redraw() {
			SetNeedsDisplay ();
		}
		#endregion

		#region Override Methods
		/// <summary>
		/// Draw the specified rect.
		/// </summary>
		/// <param name="rect">Rect.</param>
		public override void Draw (CGRect rect)
		{
			base.Draw (rect);

			// Fill
			var fillPath = UIBezierPath.FromRect(rect);
			Color.SetFill ();
			fillPath.Fill();

			// Outter Frame
			var oFrame = UIBezierPath.FromRect(rect);
			UIColor.Black.SetStroke ();
			oFrame.LineWidth = 2;
			oFrame.Stroke ();

			// Inner Frame
			var iFrame = UIBezierPath.FromRect(new CGRect(2,2,rect.Width-4,rect.Height-4));
			UIColor.White.SetStroke ();
			iFrame.LineWidth = 2;
			iFrame.Stroke ();
		}

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
			ACToast.MakeText("ACColorWell by Appracatappra, LLC.", ACToastLength.Short).Show();
#else
			AppracatappraLicenseManager.ValidateLicense();
#endif

			// Raise touched event
			RaiseTouched ();

			// Automatically display a HSB picker?
			if (AutoPresentHSBColorPicker && ParentViewController != null) {
				// Yes, build a picker and display it.
				var picker = new ACColorPickerViewController();
				picker.Title = ColorPickerTitle;
				picker.Color = Color;

				// Wireup events
				picker.SelectionFinished += (color) => {
					
					ParentViewController.DismissViewController(true, null);
					Color = color;
					RaiseColorChanged();
				};

				// Display
				ParentViewController.PresentViewController(picker, true, null);
			}

			// Pass call to base object
			if (! ExclusiveTouch) 
				base.TouchesBegan (touches, evt);
		}
		#endregion

		#region Events
		/// <summary>
		/// Touched delegate.
		/// </summary>
		public delegate void TouchedDelegate();

		/// <summary>
		/// Occurs when touched.
		/// </summary>
		public event TouchedDelegate Touched;

		/// <summary>
		/// Raises the touched.
		/// </summary>
		internal void RaiseTouched() {
			// Inform caller
			if (this.Touched != null)
				this.Touched ();
		}

		/// <summary>
		/// Color changed delegate.
		/// </summary>
		public delegate void ColorChangedDelegate(UIColor newColor);

		/// <summary>
		/// Occurs when color is changed.
		/// </summary>
		public event ColorChangedDelegate ColorChanged;

		/// <summary>
		/// Raises the color changed event.
		/// </summary>
		internal void RaiseColorChanged() {
			if (this.ColorChanged != null) this.ColorChanged(Color);
		}
		#endregion
	}
}

