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
	/// Presents a color picker view inside of a <c>ACColorPickerViewController</c> presenting a single instance
	/// color selector.
	/// </summary>
	[Register("ACColorPickerView")]
	public class ACColorPickerView : UIView
	{
		#region Private Variables
		private UINavigationBar _navBar;
		private UINavigationItem _item;
		private ACColorWell _previousColor;
		private ACColorWell _newColor;
		private ACColorCube _colorCube;
		private ACHueBar _hueBar;
		private UIColor _color;
		private float _inset = 32f;
		#endregion

		#region Computed Properties
		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>The title.</value>
		public string Title {
			get { return _item.Title; }
			set { _item.Title = value; }
		}

		/// <summary>
		/// Gets or sets the color.
		/// </summary>
		/// <value>The color.</value>
		public UIColor Color {
			get { return _color; }
			set {
				_color = value;

				// Update wells
				_previousColor.Color = _color;
				_newColor.Color = _color;

				// Update Elements
				HSVColor hsv = new HSVColor (_color);
				_hueBar.Hue = hsv.Hue;
				_colorCube.Saturation = hsv.Saturation;
				_colorCube.Brightness = hsv.Value;
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACColorPickerView"/> class.
		/// </summary>
		public ACColorPickerView () : base()
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACColorPickerView"/> class.
		/// </summary>
		/// <param name="coder">Coder.</param>
		public ACColorPickerView (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACColorPickerView"/> class.
		/// </summary>
		/// <param name="flag">Flag.</param>
		public ACColorPickerView (NSObjectFlag flag) : base(flag)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACColorPickerView"/> class.
		/// </summary>
		/// <param name="bounds">Bounds.</param>
		public ACColorPickerView (CGRect bounds) : base(bounds)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACColorPickerView"/> class.
		/// </summary>
		/// <param name="ptr">Ptr.</param>
		public ACColorPickerView (IntPtr ptr) : base(ptr)
		{
			Initialize ();
		}

		/// <summary>
		/// Initialize this instance.
		/// </summary>
		private void Initialize(){

			// Initialize
			this.BackgroundColor = UIColor.DarkGray;
			this.UserInteractionEnabled = true;

			// Insert Navigation bar
			_navBar = new UINavigationBar (new CGRect (0, 20, Bounds.Width, 44));
			AddSubview (_navBar);

			// Set default title
			_item = new UINavigationItem ("Select Color");
			_navBar.PushNavigationItem (_item, false);

			// Add the done button
			UIBarButtonItem button = new UIBarButtonItem ("Done", UIBarButtonItemStyle.Plain, (sender, e) => {
				RaiseSelectionFinished (Color);
			});
			_item.RightBarButtonItem = button;

			// Calculate bounds
			nfloat contentWidth = Bounds.Width - (_inset * 2);
			nfloat wellWidth = (contentWidth / 2) - (_inset / 4);
			nfloat wellX = (_inset + contentWidth) - wellWidth;

			// Add Previous Color Well
			_previousColor = new ACColorWell (new CGRect (_inset, 75, wellWidth, 50));
			AddSubview (_previousColor);

			// Add new Color Well
			_newColor = new ACColorWell (new CGRect (wellX, 75, wellWidth, 50));
			AddSubview (_newColor);

			// Add new Color Cube
			_colorCube = new ACColorCube (new CGRect (_inset, 145, contentWidth, contentWidth));
			AddSubview (_colorCube);

			// Add color Bar
			_hueBar = new ACHueBar (new CGRect (_inset, 165 + contentWidth, contentWidth, 50));
			AddSubview (_hueBar);

			// Wireup events
			_hueBar.HueChanged += (hue) => {
				// Adjust hue on cube
				_colorCube.Hue =hue;
			};

			_colorCube.ColorChanged += (color) => {
				// Update new color
				_newColor.Color = color;
				_color = color;
			};
				
			_previousColor.Touched += () => {
				Color = _previousColor.Color;
			};
		}
		#endregion

		#region Events
		/// <summary>
		/// Selection finished delegate.
		/// </summary>
		public delegate void SelectionFinishedDelegate(UIColor color);

		/// <summary>
		/// Occurs when selection finished.
		/// </summary>
		public event SelectionFinishedDelegate SelectionFinished;

		/// <summary>
		/// Raises the selection finished.
		/// </summary>
		internal void RaiseSelectionFinished(UIColor color) {
			// Inform caller
			if (this.SelectionFinished != null)
				this.SelectionFinished (color);
		}
		#endregion
	}
}

