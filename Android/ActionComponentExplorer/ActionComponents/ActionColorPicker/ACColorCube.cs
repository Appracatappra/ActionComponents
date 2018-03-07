using System;
using System.Threading;

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
using Android.Animation;

namespace ActionComponents
{
	/// <summary>
	/// The Action Color Cube displays a color selection cube in the app's user interface that allows the user to
	/// move a pointer around to select the <c>Hue</c>, <c>Saturation</c> and <c>Brightness</c> of a generated color.
	/// </summary>
	public class ACColorCube : ACView
	{
		#region Private Variables
		private Bitmap _background;
		private Bitmap _imageCache;
		private float _hue;
		private float _saturation = 1f;
		private float _brightness = 1f;
		private HSVColor _hsv;
		private int _indicatorSize = 24;
		private ACColorIndicator _indicator;
		#endregion

		#region Computed Properties
		/// <summary>
		/// Gets or sets the hue.
		/// </summary>
		/// <value>The hue.</value>
		public float Hue
		{
			get { return _hue; }
			set
			{
				_hue = value;
				Redraw();
				MoveIndicator();
			}
		}

		/// <summary>
		/// Gets or sets the saturation.
		/// </summary>
		/// <value>The saturation.</value>
		public float Saturation
		{
			get { return _saturation; }
			set
			{
				_saturation = value;
				MoveIndicator();
			}
		}

		/// <summary>
		/// Gets or sets the brightness.
		/// </summary>
		/// <value>The brightness.</value>
		public float Brightness
		{
			get { return _brightness; }
			set
			{
				_brightness = value;
				MoveIndicator();
			}
		}

		/// <summary>
		/// Gets the HSV color
		/// </summary>
		/// <value>The HS.</value>
		public HSVColor HSV
		{
			get { return _hsv; }
		}

		/// <summary>
		/// Gets or sets the width.
		/// </summary>
		/// <value>The width.</value>
		public new int Width
		{
			get
			{
				var w = ACView.GetViewWidth(this);
				if (w <= 0)
				{
					return 256;
				}
				else
				{
					return w;
				}
			}
			set { ACView.SetViewWidth(this, value); }
		}

		/// <summary>
		/// Gets or sets the height.
		/// </summary>
		/// <value>The height.</value>
		public new int Height
		{
			get
			{
				var h = ACView.GetViewHeight(this);
				if (h <= 0)
				{
					return 256;
				}
				else
				{
					return h;
				}
			}
			set { ACView.SetViewHeight(this, value); }
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACHueBar"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		public ACColorCube(Context context) : base(context)
		{
			Initialize();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACHueBar"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attr">Attr.</param>
		public ACColorCube(Context context, IAttributeSet attr) : base(context, attr)
		{
			Initialize();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACHueBar"/> class.
		/// </summary>
		/// <param name="javaReference">Java reference.</param>
		/// <param name="transfer">Transfer.</param>
		public ACColorCube(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
		{
			Initialize();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACHueBar"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attr">Attr.</param>
		/// <param name="defStyle">Def style.</param>
		public ACColorCube(Context context, IAttributeSet attr, int defStyle) : base(context, attr, defStyle)
		{
			Initialize();
		}

		/// <summary>
		/// Initialize this instance.
		/// </summary>
		private void Initialize()
		{
			// Set initial properties
			this.SetBackgroundColor(Color.Argb(0, 0, 0, 0));

			// Insert indicator
			_indicator = new ACColorIndicator(this.Context);
			_indicator.Resize(_indicatorSize, _indicatorSize);
			AddView(_indicator);

		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Redraw this <see cref="ActionComponents.ACOval"/>.
		/// </summary>
		public void Redraw()
		{

			// Set color
			HSVColor hsv = new HSVColor(Hue * 360f, 1.0f, 1.0f);
			_indicator.CurrentColor = hsv.RawColor;

			// Move the indicator
			MoveIndicator();

			//Clear buffer
			if (_imageCache != null)
			{
				_imageCache.Dispose();
				_imageCache = null;
			}

			//Force a redraw
			this.Invalidate();

		}
		#endregion

		#region Private Methods
		private void MoveIndicator()
		{
			// Calculate a new color for the new location
			_hsv = new HSVColor(Hue * 360f, Saturation, Brightness);
			_indicator.CurrentColor = _hsv.RawColor;

			// Calculate touch locations
			float tx = Saturation * ((float)Width);
			float ty = Brightness * ((float)Height);

			// Move Indicator
			int x = ((int)tx) - (_indicatorSize / 2);
			int y = (Height - ((int)ty)) - (_indicatorSize / 2);
			_indicator.MoveToPoint(x, y);

			// Inform caller of color change
			RaiseColorChanged(HSV.RawColor);
		}

		/// <summary>
		/// Tracks the indicator touch.
		/// </summary>
		/// <param name="pt">Point.</param>
		private void TrackIndicatorTouch(Point pt)
		{

			// Set saturation
			float percent = ((float)pt.X) / ((float)Width);
			_saturation = HSVImage.Pin(0f, percent, 1f);

			// Set brightness
			percent = 1.0f - (((float)pt.Y) / ((float)Height));
			_brightness = HSVImage.Pin(0f, percent, 1f);

			// Update indicator
			MoveIndicator();

		}
		#endregion

		#region Private Drawing Methods
		/// <summary>
		/// Draws the Hue Bar.
		/// </summary>
		/// <param name="canvas">Canvas.</param>
		/// <param name="appearance">Appearance.</param>
		private void DrawColorCube(Canvas canvas)
		{

			// Define bounds
			int x = 0;
			int y = 0;
			int width = this.Width;
			int height = this.Height;

			// Fill body
			var iPaint = new Paint();
			var iRect = new Rect(x, y, width, height);
			canvas.DrawBitmap(_background, null, iRect, iPaint);

			// Outer body frame
			var frame = new ShapeDrawable(new RectShape());
			frame.Paint.Color = Color.Black;
			frame.Paint.SetStyle(Paint.Style.Stroke);
			frame.Paint.StrokeWidth = 2;
			frame.SetBounds(x, y, width, height);
			frame.Draw(canvas);

		}

		/// <summary>
		/// Populates the image cache.
		/// </summary>
		/// <returns>The image cache.</returns>
		private Bitmap PopulateImageCache()
		{

			// Create a temporary canvas
			var canvas = new Canvas();

			// Set new image background
			_background = HSVImage.SaturationBrightnessSquareImage(Hue);

			// Create bitmap storage and assign to canvas
			var controlBitmap = Bitmap.CreateBitmap(this.Width, this.Height, Bitmap.Config.Argb8888);
			canvas.SetBitmap(controlBitmap);

			// Draw a standard oval
			DrawColorCube(canvas);

			// Return new cache
			return controlBitmap;
		}
		#endregion

		#region Override Methods
		/// <summary>
		/// Ons the layout.
		/// </summary>
		/// <param name="changed">If set to <c>true</c> changed.</param>
		/// <param name="l">L.</param>
		/// <param name="t">T.</param>
		/// <param name="r">The red component.</param>
		/// <param name="b">The blue component.</param>
		protected override void OnLayout(bool changed, int l, int t, int r, int b)
		{
			base.OnLayout(changed, l, t, r, b);
			if (changed) MoveIndicator();
		}

		/// <Docs>the canvas on which the background will be drawn</Docs>
		/// <remarks>Implement this to do your drawing.</remarks>
		/// <format type="text/html">[Android Documentation]</format>
		/// <since version="Added in API level 1"></since>
		/// <summary>
		/// Raises the draw event.
		/// </summary>
		/// <param name="canvas">Canvas.</param>
		protected override void OnDraw(Canvas canvas)
		{
			//Call base
			base.OnDraw(canvas);

			//Restoring image from cache?
			if (_imageCache == null) _imageCache = PopulateImageCache();

			//Draw cached image to canvas
			canvas.DrawBitmap(_imageCache, 0, 0, null);
		}

		/// <summary>
		/// Raises the touch event event.
		/// </summary>
		/// <param name="e">E.</param>
		public override bool OnTouchEvent(MotionEvent e)
		{
			int x = (int)e.GetX();
			int y = (int)e.GetY();
			var pt = new Point(x, y);

			//Take action based on the event type
			switch (e.Action)
			{
				case MotionEventActions.Down:
					// Track user interaction
					TrackIndicatorTouch(pt);

					//Inform system that we've handled this event 
					return true;
				case MotionEventActions.Move:
					// Track user interaction
					TrackIndicatorTouch(pt);

					//Inform system that we've handled this event 
					return true;
				case MotionEventActions.Up:
#if TRIAL
					Android.Widget.Toast.MakeText(this.Context, "ACColorCube by Appracatappra, LLC.", Android.Widget.ToastLength.Short).Show();
#else
					AppracatappraLicenseManager.ValidateLicense(this.Context);
#endif

					//Inform system that we've handled this event 
					break;
				case MotionEventActions.Cancel:
					break;
			}

			// Not handled by this routine
			return base.OnTouchEvent(e);
		}
		#endregion

		#region Events
		/// <summary>
		/// Color changed delegate.
		/// </summary>
		public delegate void ColorChangedDelegate(Color newColor);

		/// <summary>
		/// Occurs when color changed.
		/// </summary>
		public event ColorChangedDelegate ColorChanged;

		/// <summary>
		/// Raises the color changed.
		/// </summary>
		/// <param name="color">Color.</param>
		internal void RaiseColorChanged(Color newColor)
		{
			// Inform caller
			if (this.ColorChanged != null)
				this.ColorChanged(newColor);
		}
		#endregion
	}
}
