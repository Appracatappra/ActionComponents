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
	/// <see cref="ActionComponents.ACSlider"/> is a custom slider control designed to operate like the brightness and 
	/// contrast sliders built into the iPhone Control Center. The <c>FillPercent</c> property gets or sets the percentage
	/// that the slider is filled (from 0% to 100%). If the user taps of drags in the control (from top to bottom) the 
	/// <c>FillPercent</c> will be adjusted accordingly and the <c>ValueChanged</c>,<c>Touched</c>, <c>Moved</c>, and/or 
	/// <c>Released</c> events will be raised.
	/// </summary>
	/// <remarks>The <see cref="ActionComponents.ACSlider"/> is designed to be drawn vertically and the minimum width 
	/// should not be less than 50 pixels.</remarks>
	public class ACSlider : ACView
	{
		#region Private Variables
		private new bool dragging = false;
		private Bitmap _imageCache;
		private Point startLocation = new Point(0, 0);
		private float fillPercent = 50f;
		private Color borderColor = Color.Rgb(104, 104, 104);
		private Color bodyColor = Color.Rgb(157, 157, 157);
		private Color fillColor = Color.White;
		private Bitmap icon = null;
		#endregion

		#region Computed Properties
		/// <summary>
		/// Gets or sets the fill percent from 0% to 100%.
		/// </summary>
		/// <value>The fill percent.</value>
		public float FillPercent
		{
			get { return fillPercent; }
			set
			{
				if (value < 0f)
				{
					fillPercent = 0f;
				}
				else if (value > 100f)
				{
					fillPercent = 100f;
				}
				else
				{
					fillPercent = value;
				}
				Redraw();
			}
		}

		/// <summary>
		/// Gets or sets the color of the border.
		/// </summary>
		/// <value>The color of the border.</value>
		public Color BorderColor
		{
			get { return borderColor; }
			set
			{
				borderColor = value;
				Redraw();
			}
		}

		/// <summary>
		/// Gets or sets the color of the body.
		/// </summary>
		/// <value>The color of the body.</value>
		public Color BodyColor
		{
			get { return bodyColor; }
			set
			{
				bodyColor = value;
				Redraw();
			}
		}

		/// <summary>
		/// Gets or sets the color of the fill.
		/// </summary>
		/// <value>The color of the fill.</value>
		public Color FillColor
		{
			get { return fillColor; }
			set
			{
				fillColor = value;
				Redraw();
			}
		}

		/// <summary>
		/// Gets or sets the optional icon displayed at the bottom of the control.
		/// </summary>
		/// <value>The icon.</value>
		public Bitmap Icon
		{
			get { return icon; }
			set
			{
				icon = value;
				Redraw();
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACSlider"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		public ACSlider(Context context) : base(context)
		{
			Initialize();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACSlider"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attr">Attr.</param>
		public ACSlider(Context context, IAttributeSet attr) : base(context, attr)
		{
			Initialize();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACSlider"/> class.
		/// </summary>
		/// <param name="javaReference">Java reference.</param>
		/// <param name="transfer">Transfer.</param>
		public ACSlider(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
		{
			Initialize();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACSlider"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attr">Attr.</param>
		/// <param name="defStyle">Def style.</param>
		public ACSlider(Context context, IAttributeSet attr, int defStyle) : base(context, attr, defStyle)
		{
			Initialize();
		}

		/// <summary>
		/// Initialize this instance.
		/// </summary>
		internal void Initialize()
		{

			// Set initial properties
			this.SetBackgroundColor(Color.Argb(0, 0, 0, 0));
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Redraw this <see cref="ActionComponents.ACOval"/>.
		/// </summary>
		public void Redraw()
		{

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
		/// <summary>
		/// Draws the slider into the given canvas.
		/// </summary>
		/// <param name="canvas">Canvas.</param>
		private void DrawSlider(Canvas canvas)
		{

			//// Variable Declarations
			var paint = new Paint();
			var boundary = new RectF(1, 1, Width - 2, Height - 2);
			var thumbVisible = fillPercent > 7.0f;
			var fillRadius = 10.0f;
			var fillVisible = fillPercent > 0.0f;
			var sliderHeight = boundary.Height();
			var changeRatio = (sliderHeight - 4.0f) / 100.0f;
			var fillOffset = 2.0f + (100.0f - fillPercent) * changeRatio;
			var thumbOffset = fillOffset + 7.0f;
			var fillHeight = sliderHeight - 3.0f - fillOffset;
			var fillX = 2.0f;
			var fillWidth = boundary.Width() - 3.0f;

			// Draw the main body
			var mainBodyPath = new Path();
			mainBodyPath.AddRoundRect(boundary, fillRadius, fillRadius, Path.Direction.Cw);

			paint.Reset();
			paint.Flags = PaintFlags.AntiAlias;
			paint.SetStyle(Paint.Style.Fill);
			paint.Color = BodyColor;
			canvas.DrawPath(mainBodyPath, paint);

			paint.Reset();
			paint.Flags = PaintFlags.AntiAlias;
			paint.SetStyle(Paint.Style.Stroke);
			paint.Color = BorderColor;
			paint.StrokeWidth = 1f;
			paint.StrokeMiter = fillRadius;
			canvas.Save();
			canvas.DrawPath(mainBodyPath, paint);
			canvas.Restore();

			// Adjust fill parameters based on fill percent
			if (FillPercent < 3.0f)
			{
				fillRadius = 2.0f;
				fillX = 10.0f;
				fillWidth = boundary.Width() - 16.0f;
			}
			else if (FillPercent < 7.0f)
			{
				fillRadius = 10.0f;
				fillX = 4.0f;
				fillWidth = boundary.Width() - 7.0f;
			}

			// Draw filled area
			if (fillVisible)
			{
				var filledAreaRect = new RectF(fillX, fillOffset, fillWidth, fillOffset + fillHeight);
				var filledAreaPath = new Path();
				filledAreaPath.AddRoundRect(filledAreaRect, fillRadius, fillRadius, Path.Direction.Cw);

				paint.Reset();
				paint.Flags = PaintFlags.AntiAlias;
				paint.SetStyle(Paint.Style.Fill);
				paint.Color = FillColor;
				canvas.DrawPath(filledAreaPath, paint);
			}

			// Draw Thumb
			if (thumbVisible)
			{
				var dragThumbLeft = (boundary.Width() / 2f) - 11f;
				var dragThumbRect = new RectF(dragThumbLeft, thumbOffset, dragThumbLeft + 22f, thumbOffset + 3.5f);
				var dragThumbPath = new Path();
				dragThumbPath.AddRoundRect(dragThumbRect, 2f, 2f, Path.Direction.Cw);

				paint.Reset();
				paint.Flags = PaintFlags.AntiAlias;
				paint.SetStyle(Paint.Style.Fill);
				paint.Color = BodyColor;
				canvas.DrawPath(dragThumbPath, paint);

				paint.Reset();
				paint.Flags = PaintFlags.AntiAlias;
				paint.SetStyle(Paint.Style.Stroke);
				paint.Color = BorderColor;
				paint.StrokeWidth = 1f;
				paint.StrokeMiter = fillRadius;
				canvas.Save();
				canvas.DrawPath(dragThumbPath, paint);
				canvas.Restore();
			}

			// Draw Icon
			if (Icon != null)
			{
				var iconScale = boundary.Width() * 0.30f;
				var iconLeft = (boundary.Width() / 2f) - (iconScale / 2f);
				var iconTop = boundary.Height() - 15f - iconScale;
				var iconRect = new RectF(iconLeft, iconTop, iconLeft + iconScale, iconTop + iconScale);
				var iconPath = new Path();
				iconPath.AddRect(iconRect, Path.Direction.Cw);

				// Create scaled image
				var image = Bitmap.CreateScaledBitmap(Icon, (int)iconScale, (int)iconScale, true);
				var shader = new BitmapShader(image, Shader.TileMode.Clamp, Shader.TileMode.Clamp);

				paint.Reset();
				paint.Flags = PaintFlags.AntiAlias;
				paint.SetStyle(Paint.Style.Fill);
				paint.SetShader(shader);
				canvas.DrawPath(iconPath, paint);

				canvas.DrawBitmap(image, null, iconRect, paint);
			}
		}

		/// <summary>
		/// Populates the image cache with any changes to the control's image.
		/// </summary>
		/// <returns>The image cache.</returns>
		private Bitmap PopulateImageCache()
		{

			// Create a temporary canvas
			var canvas = new Canvas();

			// Create bitmap storage and assign to canvas
			var controlBitmap = Bitmap.CreateBitmap(this.Width, this.Height, Bitmap.Config.Argb8888);
			canvas.SetBitmap(controlBitmap);

			// Draw a slider
			DrawSlider(canvas);

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
			var changeRatio = (Height - 4.0f) / 100.0f;

			//Take action based on the event type
			switch (e.Action)
			{
				case MotionEventActions.Down:
					//Are we already dragging?
					if (dragging)
						return true;

					//Save the starting location
					startLocation.X = x;
					startLocation.Y = y;

					//Inform caller of event
					RaiseTouched();

					//Inform system that we've handled this event 
					return true;
				case MotionEventActions.Move:
					//Move view
					dragging = true;

					// Calculate new percentage
					FillPercent = (float)((Height - 4.0f - y) / changeRatio);
					RaiseValueChanged();

					//Inform caller of event
					RaiseMoved();

					return true;
				case MotionEventActions.Up:

					// Was a drag in progress?
					if (!dragging)
					{
						// no, Calculate new percentage
						FillPercent = (float)((Height - 4.0f - y) / changeRatio);
						RaiseValueChanged();
					}

					//Clear any drag action
					dragging = false;

					//Inform caller of event
					RaiseReleased();

#if TRIAL
					Android.Widget.Toast.MakeText(this.Context, "ACSlider by Appracatappra, LLC.", Android.Widget.ToastLength.Short).Show();
#else
					AppracatappraLicenseManager.ValidateLicense(this.Context);
#endif

					//Inform system that we've handled this event 
					break;
				case MotionEventActions.Cancel:
					//Clear any drag action
					dragging = false;
					break;
			}

			// Not handled by this routine
			return base.OnTouchEvent(e);
		}
		#endregion

		#region Events
		/// <summary>
		/// Occurs when this <see cref="ActionComponents.ACSlider"/> is touched. 
		/// </summary>
		public delegate void ACSliderTouchedDelegate(ACSlider view);
		public event ACSliderTouchedDelegate Touched;

		/// <summary>
		/// Raises the touched event
		/// </summary>
		private void RaiseTouched()
		{
			if (this.Touched != null)
				this.Touched(this);
		}

		/// <summary>
		/// Occurs when this <see cref="ActionComponents.ACSlider"/> is moved.
		/// </summary>
		public delegate void ACSliderMovedDelegate(ACSlider view);
		public event ACSliderMovedDelegate Moved;

		/// <summary>
		/// Raises the moved event
		/// </summary>
		private void RaiseMoved()
		{
			if (this.Moved != null)
				this.Moved(this);
		}

		/// <summary>
		/// Occurs when this <see cref="ActionComponents.ACSlider"/> is released. 
		/// </summary>
		public delegate void ACSliderReleasedDelegate(ACSlider view);
		public event ACSliderReleasedDelegate Released;

		/// <summary>
		/// Raises the released.
		/// </summary>
		private void RaiseReleased()
		{
			if (this.Released != null)
				this.Released(this);
		}

		/// <summary>
		/// Occurs when this <see cref="ActionComponents.ACSlider"/> value changes.
		/// </summary>
		public delegate void ACSliderValueChanged(int fillPercent);
		public event ACSliderValueChanged ValueChanged;

		/// <summary>
		/// Raises the value changed event.
		/// </summary>
		private void RaiseValueChanged()
		{
			if (this.ValueChanged != null)
			{
				this.ValueChanged((int)FillPercent);
			}
		}
		#endregion
	}
}
