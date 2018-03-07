using System;
using System.Drawing;
using System.ComponentModel;

using Foundation;
using UIKit;
using CoreGraphics;


namespace ActionComponents 
{
	[Register("ACOval")]
	public class ACOval : ACView 
	{
		#region Private Variables
		private ACOvalAppearance _appearance;
		private ACOvalAppearance _appearanceDisabled;
		private ACOvalAppearance _appearanceTouched;
		private string _text = "";
		private bool _touched = false;
		#endregion

		#region Computed Properties
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="ActionComponents.ACOval"/>
		/// is enabled.
		/// </summary>
		/// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
		public new bool Enabled {
			get { return base.Enabled; }
			set {
				base.Enabled = value;
				Redraw ();
			}
		}

		/// <summary>
		/// Gets or sets the appearance for this <see cref="ActionComponents.ACOval"/>.
		/// </summary>
		/// <value>The appearance.</value>
		public new ACOvalAppearance Appearance {
			get { return _appearance; }
			set {
				_appearance = value;

				// Wireup
				_appearance.AppearanceModified += () => {
					Redraw();
				};

				// Redraw component
				Redraw ();
			}
		}

		/// <summary>
		/// Gets or sets the disabled appearance for this <see cref="ActionComponents.ACOval"/>.
		/// </summary>
		/// <value>The appearance disabled.</value>
		public ACOvalAppearance AppearanceDisabled {
			get { return _appearanceDisabled; }
			set {
				_appearanceDisabled = value;

				// Wireup
				_appearanceDisabled.AppearanceModified += () => {
					if (!Enabled) {
						Redraw();
					}
				};

				// Not enabled?
				if (!Enabled) {
					Redraw ();
				}
			}
		}

		/// <summary>
		/// Gets or sets the touched appearance for this <see cref="ActionComponents.ACOval"/>.
		/// </summary>
		/// <value>The appearance touched.</value>
		public ACOvalAppearance AppearanceTouched {
			get { return _appearanceTouched; }
			set {
				_appearanceTouched = value;

				// Wireup 
				_appearanceTouched.AppearanceModified += () => {
					if (_touched) {
						Redraw();
					}
				};

				// Redraw required?
				if (_touched) {
					Redraw();
				}
			}
		}

		/// <summary>
		/// Gets or sets the text value for this <see cref="ActionComponents.ACOval"/>.
		/// </summary>
		/// <value>The text.</value>
		public string Text {
			get { return _text; }
			set {
				_text = value;
				Redraw ();
			}
		}

		/// <summary>
		/// Shortcut to set the image of all appearance types for this <see cref="ActionComponents.ACOval"/>.
		/// </summary>
		/// <value>The image.</value>
		public UIImage Image {
			get { return Appearance.Image; }
			set {
				// Save the new image to all appearance types
				Appearance.Image = value;
				AppearanceDisabled.Image = value;
				AppearanceTouched.Image = value;
			}
		}

		/// <summary>
		/// Shortcut to set the image placement of all appearance types for this <see cref="ActionComponents.ACOval"/>.
		/// </summary>
		/// <value>The image placement.</value>
		public ACOvalImagePlacement ImagePlacement {
			get { return Appearance.ImagePlacement; }
			set {
				// Save placement to all appearance types
				Appearance.ImagePlacement = value;
				AppearanceDisabled.ImagePlacement = value;
				AppearanceTouched.ImagePlacement = value;
			}
		}

		/// <summary>
		/// Shortcut to set the image placement of all appearance types for this <see cref="ActionComponents.ACOval"/>.
		/// </summary>
		/// <value>The image x.</value>
		public nfloat ImageX {
			get { return Appearance.ImageX; }
			set {
				// Save position to all appearance types
				Appearance.ImageX = value;
				AppearanceDisabled.ImageX = value;
				AppearanceTouched.ImageX = value;
			}
		}

		/// <summary>
		/// Shortcut to set the image placement of all appearance types for this <see cref="ActionComponents.ACOval"/>.
		/// </summary>
		/// <value>The image y.</value>
		public nfloat ImageY {
			get { return Appearance.ImageY; }
			set {
				// Save position to all appearance types
				Appearance.ImageY = value;
				AppearanceDisabled.ImageY = value;
				AppearanceTouched.ImageY = value;
			}
		}

		/// <summary>
		/// Shortcut to set the image width of all appearance types for this <see cref="ActionComponents.ACOval"/>.
		/// </summary>
		/// <value>The width of the image.</value>
		public nfloat ImageWidth {
			get { return Appearance.ImageWidth; }
			set {
				// Save width to all appearance types
				Appearance.ImageWidth = value;
				AppearanceDisabled.ImageWidth = value;
				AppearanceTouched.ImageWidth = value;
			}
		}

		/// <summary>
		/// Shortcut to set the image height of all appearance types for this <see cref="ActionComponents.ACOval"/>.
		/// </summary>
		/// <value>The height of the image.</value>
		public nfloat ImageHeight {
			get { return Appearance.ImageHeight; }
			set {
				// Save height to all appearance types
				Appearance.ImageHeight = value;
				AppearanceDisabled.ImageHeight = value;
				AppearanceTouched.ImageHeight = value;
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACOval"/> class.
		/// </summary>
		public ACOval () : base()
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACOval"/> class.
		/// </summary>
		/// <param name="coder">Coder.</param>
		public ACOval (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACOval"/> class.
		/// </summary>
		/// <param name="flag">Flag.</param>
		public ACOval (NSObjectFlag flag) : base(flag)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACOval"/> class.
		/// </summary>
		/// <param name="bounds">Bounds.</param>
		public ACOval (CGRect bounds) : base(bounds)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACOval"/> class.
		/// </summary>
		/// <param name="ptr">Ptr.</param>
		public ACOval (IntPtr ptr) : base(ptr)
		{
			Initialize ();
		}

		/// <summary>
		/// Initialize this instance.
		/// </summary>
		private void Initialize(){

			// Create a new appearance
			this.Appearance = new ACOvalAppearance ();
			this.AppearanceDisabled = new ACOvalAppearance (UIColor.LightGray, UIColor.White);
			this.AppearanceTouched = new ACOvalAppearance ();

			// Set initial properties
			this.BackgroundColor = UIColor.Clear;

			// Wireup events
			this.Touched += (view) =>
			{
				// Mark as touched and redraw
				_touched = true;
				Redraw();

#if TRIAL
				ACToast.MakeText("ACOval by Appracatappra, LLC.", ACToastLength.Short).Show();
#else
				AppracatappraLicenseManager.ValidateLicense();
#endif
			};

			this.Released += (view) => {
				// Mark as released and redraw
				_touched = false;
				Redraw();
			};

		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Redraw this <see cref="ActionComponents.ACOval"/>.
		/// </summary>
		public void Redraw(){
			//Force the component to redraw
			SetNeedsDisplay ();
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Gets the appearance.
		/// </summary>
		/// <returns>The appearance.</returns>
		private ACOvalAppearance GetAppearance() {

			// Enabled?
			if (Enabled) {
				// Yes, is the oval being touched?
				if (_touched) {
					// Yes, return the touched appearance
					return AppearanceTouched;
				} else {
					// No, return the normal appearance
					return Appearance;
				}
			} else {
				// No, return the disabled appearance.
				return AppearanceDisabled;
			}

		}

		/// <summary>
		/// Draws the text.
		/// </summary>
		/// <param name="frame">Frame.</param>
		private void DrawText(CGRect frame) {

			// Get the correct appearance state
			var appearance = GetAppearance ();

			// Text Drawing
			CGRect textRect = new CGRect(frame.GetMinX() + 5.0f, frame.GetMinY() + 5.0f, frame.Width-10.0f, frame.Height-10.0f);
			{
				appearance.FontColor.SetFill();
				var textFont = UIFont.FromName(appearance.FontName,appearance.FontSize);
				textRect.Offset(0.0f, (textRect.Height - new NSString(Text).StringSize(textFont, textRect.Size).Height) / 2.0f);
				new NSString(Text).DrawString(textRect, textFont, UILineBreakMode.WordWrap, UITextAlignment.Center);
			}
		}

		/// <summary>
		/// Draws the oval.
		/// </summary>
		/// <param name="frame">Frame.</param>
		/// <param name="borderWidth">Border width.</param>
		private void DrawOval(CGRect frame, nfloat borderWidth)
		{
			// Get the correct appearance state
			var appearance = GetAppearance ();

			// Oval 2 Drawing
			var oval2Path = UIBezierPath.FromOval(new CGRect(frame.GetMinX() + 5.0f, frame.GetMinY() + 5.0f, frame.Width - 10.0f, frame.Height - 10.0f));
			appearance.FillColor.SetFill();
			oval2Path.Fill();

			// Draw Text
			if (Text != "")
				DrawText (frame);

			// Draw border?
			if (appearance.HasBorder) {
				// Yes
				appearance.BorderColor.SetStroke ();
				oval2Path.LineWidth = borderWidth;
				oval2Path.Stroke ();
			}
		}

		/// <summary>
		/// Draws the oval pict.
		/// </summary>
		/// <param name="frame">Frame.</param>
		/// <param name="borderWidth">Border width.</param>
		private void DrawOvalPict(CGRect frame, nfloat borderWidth)
		{
			// Get the correct appearance state
			var appearance = GetAppearance ();

			// General Declarations
			var context = UIGraphics.GetCurrentContext();

			// Image Declarations
			var image = appearance.Image;

			// PictFrame Drawing
			CGRect pictFrameRect = new CGRect(frame.GetMinX() + 5.0f, frame.GetMinY() + 5.0f, frame.Width - 10.0f, frame.Height - 10.0f);
			var pictFramePath = UIBezierPath.FromOval(pictFrameRect);
			appearance.FillColor.SetFill();
			pictFramePath.Fill();
			context.SaveState();
			pictFramePath.AddClip();

			// Draw image in oval based on the placement setting
			switch (appearance.ImagePlacement) {
			case ACOvalImagePlacement.TopLeft:
				// Pin to top left corner
				image.Draw(new CGRect((float)Math.Floor(pictFrameRect.GetMinX() + 0.5f), (float)Math.Floor(pictFrameRect.GetMinY() + 0.5f), image.Size.Width, image.Size.Height));
				break;
			case ACOvalImagePlacement.ScaleToFit:
				// Scale to fit the oval
				image.Draw(new CGRect(0f,0f, frame.Width,frame.Height));
				break;
			case ACOvalImagePlacement.FreeForm:
				// User defined placement and size
				image.Draw(new CGRect((appearance.ImageX + 0.5f), (appearance.ImageY + 0.5f), appearance.ImageWidth, appearance.ImageHeight));
				break;
			case ACOvalImagePlacement.Center:
				// Center in the oval
				image.Draw(new CGRect((frame.Width/2f)-(image.Size.Width/2f), (frame.Height/2f)-(image.Size.Height/2f), image.Size.Width, image.Size.Height));
				break;
			}

			context.RestoreState();

			// Draw Text
			if (Text != "")
				DrawText (frame);

			// Draw border?
			if (appearance.HasBorder) {
				// Yes
				appearance.BorderColor.SetStroke ();
				pictFramePath.LineWidth = borderWidth;
				pictFramePath.Stroke ();
			}
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

			// Get the correct appearance state
			var appearance = GetAppearance ();

			// Filled with an image?
			if (appearance.Image != null) {
				// Yes, draw image in the oval
				DrawOvalPict (rect, appearance.BorderWidth);
			} else {
				// No, draw a filled oval
				DrawOval (rect, appearance.BorderWidth);
			}
		}
		#endregion
	}
}

