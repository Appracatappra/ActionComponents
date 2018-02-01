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
	/// The Action HSB Color Picker View Controller creates a color selection dialog box that fully handles the 
	/// process of showing a currently selected color and allowing the user the select a new color.
	/// </summary>
	[Register("ACColorPickerViewController")]
	public class ACColorPickerViewController : UIViewController
	{
		#region Private Variables
		private ACColorPickerView _picker;
		#endregion

		#region Computed Properties
		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>The title.</value>
		public string Title {
			get { return _picker.Title; }
			set { _picker.Title = value; }
		}

		/// <summary>
		/// Gets or sets the color.
		/// </summary>
		/// <value>The color.</value>
		public UIColor Color {
			get { return _picker.Color; }
			set { _picker.Color = value; }
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACColorPickerViewController"/> class.
		/// </summary>
		public ACColorPickerViewController () : base()
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACColorPickerViewController"/> class.
		/// </summary>
		/// <param name="coder">Coder.</param>
		public ACColorPickerViewController (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACColorPickerViewController"/> class.
		/// </summary>
		/// <param name="flag">Flag.</param>
		public ACColorPickerViewController (NSObjectFlag flag) : base(flag)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACColorPickerViewController"/> class.
		/// </summary>
		/// <param name="ptr">Ptr.</param>
		public ACColorPickerViewController (IntPtr ptr) : base(ptr)
		{
			Initialize ();
		}

		/// <summary>
		/// Initialize this instance.
		/// </summary>
		private void Initialize(){

			// Add new picker
			_picker = new ACColorPickerView (View.Frame);
			View.AddSubview (_picker);

			// Wireup events
			_picker.SelectionFinished += (color) => {
				// Inform caller
				RaiseSelectionFinished(color);
			};
		}
		#endregion

		#region Override Methods
		/// <summary>
		/// Handles the view initially being loaded.
		/// </summary>
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

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

