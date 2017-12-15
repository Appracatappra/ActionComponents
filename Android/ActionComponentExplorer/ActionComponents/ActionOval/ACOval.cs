using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;


namespace ActionComponents
{
	public class ACOval : ACView
	{
		#region Private Variables
		private Bitmap _imageCache;
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
		public bool Enabled {
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
		public ACOvalAppearance Appearance {
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
		public Bitmap Image {
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
		public int ImageX {
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
		public int ImageY {
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
		public int ImageWidth {
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
		public int ImageHeight {
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
		/// <param name="context">Context.</param>
		public ACOval (Context context) : base(context)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACOval"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attr">Attr.</param>
		public ACOval (Context context, IAttributeSet attr) : base(context,attr)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACOval"/> class.
		/// </summary>
		/// <param name="javaReference">Java reference.</param>
		/// <param name="transfer">Transfer.</param>
		public ACOval (IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference,transfer)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACOval"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attr">Attr.</param>
		/// <param name="defStyle">Def style.</param>
		public ACOval (Context context, IAttributeSet attr, int defStyle) : base(context,attr,defStyle)
		{
			Initialize ();
		}

		/// <summary>
		/// Initialize this instance.
		/// </summary>
		private void Initialize() {

			// Create a new appearance
			this.Appearance = new ACOvalAppearance ();
			this.AppearanceDisabled = new ACOvalAppearance (Color.LightGray, Color.White);
			this.AppearanceTouched = new ACOvalAppearance ();

			// Set initial properties
			this.SetBackgroundColor (Color.Argb (0, 0, 0, 0));

			// Wireup events
			this.Touched += (view) => {
				// Mark as touched and redraw
				_touched = true;
				Redraw();

				#if TRIAL 
				Android.Widget.Toast.MakeText(this.Context, "ActionOval by Appracatappra", Android.Widget.ToastLength.Short).Show();
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

			//Clear buffer
			if (_imageCache!=null) {
				_imageCache.Dispose();
				_imageCache=null;
			}

			//Force a redraw
			this.Invalidate();

		}
		#endregion

		#region Private Draw Methods
		/// <summary>
		/// Gets the appearance based on current property states
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
		/// <param name="canvas">Canvas.</param>
		/// <param name="appearance">Appearance.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		private void DrawText(Canvas canvas, ACOvalAppearance appearance, int x, int y, int width, int height) {

			// Create a brush for the text
			Paint titlePaint = new Paint ();
			titlePaint.Color=appearance.FontColor;
			titlePaint.SetStyle (Paint.Style.Fill);
			titlePaint.AntiAlias = true;
			titlePaint.TextSize = appearance.FontSize;
			titlePaint.FakeBoldText = true;

			ActionCanvasExtensions.DrawTextAligned (canvas, Text, x, y, width, height, titlePaint, TextBlockAlignment.Center, TextBlockAlignment.Center);
		}

		/// <summary>
		/// Draws the oval.
		/// </summary>
		/// <param name="canvas">Canvas.</param>
		/// <param name="appearance">Appearance.</param>
		private void DrawOval(Canvas canvas, ACOvalAppearance appearance) {

			// Define bounds
			int x = 5;
			int y = 5;
			int width = this.Width - 10;
			int height = this.Height - 10;

			// Fill body
			var oval = new ShapeDrawable (new OvalShape ());
			oval.Paint.Color = appearance.FillColor;
			oval.SetBounds (x,y,width,height);
			oval.Draw (canvas);

			// Frame body
			var ovalFrame = new ShapeDrawable (new OvalShape ());
			ovalFrame.Paint.Color=appearance.BorderColor;
			ovalFrame.Paint.SetStyle (Paint.Style.Stroke);
			ovalFrame.Paint.StrokeWidth = appearance.BorderWidth;
			ovalFrame.SetBounds (x,y,width,height);
			ovalFrame.Draw (canvas);

			// Has text?
			if (Text != "")
				DrawText (canvas, appearance, x, y, width, height);

		}

		/// <summary>
		/// Draws the oval pict.
		/// </summary>
		/// <param name="canvas">Canvas.</param>
		/// <param name="appearance">Appearance.</param>
		private void DrawOvalPict(Canvas canvas, ACOvalAppearance appearance) {
			Bitmap image = null;
			BitmapShader shader = null;
			Paint iPaint=new Paint();
			Rect imageRect = null;

			// Define bounds
			int x = 5;
			int y = 5;
			int width = this.Width - 10;
			int height = this.Height - 10;

			// Create image shader
			switch(appearance.ImagePlacement) {
			case ACOvalImagePlacement.TopLeft:
				// Fill oval with image
				image = appearance.Image;
				shader = new BitmapShader (image, Shader.TileMode.Clamp, Shader.TileMode.Clamp);
				break;
			case ACOvalImagePlacement.ScaleToFit:
				// Fill oval with image
				image = Bitmap.CreateScaledBitmap (appearance.Image, width, height,	true);
				shader = new BitmapShader (image, Shader.TileMode.Clamp, Shader.TileMode.Clamp);
				break;
			case ACOvalImagePlacement.Center:
				// Calculate center position for the image
				image = appearance.Image;
				int ix = (x/2) + ((width / 2) - (image.Width / 2));
				int iy = (y/2) + ((height / 2) - (image.Height / 2));
				imageRect = new Rect (ix, iy, ix + image.Width, iy + image.Height);
				break;
			case ACOvalImagePlacement.FreeForm:
				// Calculate the freeform position of the image
				image = Bitmap.CreateScaledBitmap (appearance.Image, appearance.ImageWidth, appearance.ImageHeight,	true);
				imageRect = new Rect (appearance.ImageX, appearance.ImageY, appearance.ImageX + appearance.ImageWidth, appearance.ImageY + appearance.ImageHeight);
				break;
			}

			// Fill body
			var oval = new ShapeDrawable (new OvalShape ());
			oval.Paint.Color = appearance.FillColor;
			oval.Paint.AntiAlias = true;
			if (shader!=null) oval.Paint.SetShader (shader);
			oval.SetBounds (x,y,width,height);
			oval.Draw (canvas);

			// Draw an image?
			if (imageRect != null) {
				// Yes, draw an image in the oval
				canvas.DrawBitmap (image, null, imageRect, iPaint);
			}

			// Frame body
			var ovalFrame = new ShapeDrawable (new OvalShape ());
			ovalFrame.Paint.Color=appearance.BorderColor;
			ovalFrame.Paint.SetStyle (Paint.Style.Stroke);
			ovalFrame.Paint.StrokeWidth = appearance.BorderWidth;
			ovalFrame.SetBounds (x,y,width,height);
			ovalFrame.Draw (canvas);

			// Has text?
			if (Text != "")
				DrawText (canvas, appearance, x, y, width, height);

		}

		/// <summary>
		/// Populates the image cache.
		/// </summary>
		/// <returns>The image cache.</returns>
		private Bitmap PopulateImageCache(){

			// Get the correct appearance state
			var appearance = GetAppearance ();

			// Create a temporary canvas
			var canvas=new Canvas();

			// Create bitmap storage and assign to canvas
			var controlBitmap=Bitmap.CreateBitmap (this.Width,this.Height,Bitmap.Config.Argb8888);
			canvas.SetBitmap (controlBitmap);

			// Has bitmap?
			if (appearance.Image != null) {
				// Draw a picture oval
				DrawOvalPict (canvas, appearance);
			} else {
				// Draw a standard oval
				DrawOval (canvas, appearance);
			}

			// Return new cache
			return controlBitmap;
		}
		#endregion

		#region Override Methods
		/// <Docs>the canvas on which the background will be drawn</Docs>
		/// <remarks>Implement this to do your drawing.</remarks>
		/// <format type="text/html">[Android Documentation]</format>
		/// <since version="Added in API level 1"></since>
		/// <summary>
		/// Raises the draw event.
		/// </summary>
		/// <param name="canvas">Canvas.</param>
		protected override void OnDraw (Canvas canvas)
		{
			//Call base
			base.OnDraw (canvas);

			//Restoring image from cache?
			if (_imageCache==null) _imageCache=PopulateImageCache();

			//Draw cached image to canvas
			canvas.DrawBitmap (_imageCache,0,0,null);
		}
		#endregion
	}
}

