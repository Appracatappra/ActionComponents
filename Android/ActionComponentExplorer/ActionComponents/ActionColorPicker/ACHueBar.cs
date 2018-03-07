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
	/// The Action Color Hue Bar presents a hue selection bar to the user that they can interact with to select the 
	/// current color. This control is typically used with an <c>ACColorCube</c> to present the full color selection
	/// UI.
	/// </summary>
	public class ACHueBar : ACView
	{
		#region Private Variables
		private Bitmap _background;
		private Bitmap _imageCache;
		private float _hue;
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
				RaiseHueChanged(_hue);
			}
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
			set {ACView.SetViewWidth(this, value);}
		}

		/// <summary>
		/// Gets or sets the height.
		/// </summary>
		/// <value>The height.</value>
		public new int Height {
			get {
				var h = ACView.GetViewHeight(this);
				if (h <= 0) {
					return 50;
				} else {
					return h;
				}
			}
			set {ACView.SetViewHeight(this, value);}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACHueBar"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		public ACHueBar(Context context) : base(context)
		{
			Initialize();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACHueBar"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attr">Attr.</param>
		public ACHueBar(Context context, IAttributeSet attr) : base(context, attr)
		{
			Initialize();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACHueBar"/> class.
		/// </summary>
		/// <param name="javaReference">Java reference.</param>
		/// <param name="transfer">Transfer.</param>
		public ACHueBar(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
		{
			Initialize();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACHueBar"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attr">Attr.</param>
		/// <param name="defStyle">Def style.</param>
		public ACHueBar(Context context, IAttributeSet attr, int defStyle) : base(context, attr, defStyle)
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

			// Generate background
			HSVColor hsv = new HSVColor(0.0f, 1.0f, 1.0f);
			_background = HSVImage.HueBarImage(ACHueBarComponentIndex.ComponentIndexHue, hsv);

			// Insert indicator
			_indicator = new ACColorIndicator(this.Context);
			_indicator.Resize(_indicatorSize, _indicatorSize);
			AddView(_indicator);

			// Do initial draw
			// Redraw();
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
		private void MoveIndicator() {
			// Calculate indicator location
			float posn = Hue * ((float)Width);
			int x = ((int)posn) - (_indicatorSize / 2);
			int y = (Height / 2) - (_indicatorSize / 2);
			_indicator.MoveToPoint(x, y);
		}

		/// <summary>
		/// Tracks the indicator touch.
		/// </summary>
		/// <param name="pt">Point.</param>
		private void TrackIndicatorTouch(Point pt)
		{

			// Calculate percentage
			float percent = ((float)pt.X) / ((float)Width);

			// Set new value
			Hue = HSVImage.Pin(0f, percent, 1f);

		}
		#endregion

		#region Private Drawing Methods
		/// <summary>
		/// Draws the Hue Bar.
		/// </summary>
		/// <param name="canvas">Canvas.</param>
		/// <param name="appearance">Appearance.</param>
		private void DrawHueBar(Canvas canvas)
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

			// Create bitmap storage and assign to canvas
			var controlBitmap = Bitmap.CreateBitmap(this.Width, this.Height, Bitmap.Config.Argb8888);
			canvas.SetBitmap(controlBitmap);

			// Draw a standard oval
			DrawHueBar(canvas);

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
					Android.Widget.Toast.MakeText(this.Context, "ACHueBar by Appracatappra, LLC.", Android.Widget.ToastLength.Short).Show();

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
		/// Hue changed delegate.
		/// </summary>
		public delegate void HueChangedDelegate(float hue);

		/// <summary>
		/// Occurs when hue changed.
		/// </summary>
		public event HueChangedDelegate HueChanged;

		/// <summary>
		/// Raises the hue changed.
		/// </summary>
		/// <param name="hue">Hue.</param>
		internal void RaiseHueChanged(float hue)
		{
			// Inform caller
			if (this.HueChanged != null)
				this.HueChanged(hue);
		}
		#endregion
	}
}
