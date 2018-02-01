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
	/// Represents a color selection indicator inside of an <c>ActionColorCube</c> or <c>ActionHueBar</c>. The uses can
	/// drag the indicator around to select a color.
	/// </summary>
	public class ACColorIndicator : ACView
	{
		#region Private Variables
		private Bitmap _imageCache;
		private Color _color;
		#endregion

		#region Computed Properties
		/// <summary>
		/// Gets or sets the color.
		/// </summary>
		/// <value>The color.</value>
		public Color CurrentColor
		{
			get { return _color; }
			set
			{
				_color = value;
				Redraw();
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACColorIndicator"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		public ACColorIndicator(Context context) : base(context)
		{
			Initialize();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACColorIndicator"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attr">Attr.</param>
		public ACColorIndicator(Context context, IAttributeSet attr) : base(context, attr)
		{
			Initialize();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACColorIndicator"/> class.
		/// </summary>
		/// <param name="javaReference">Java reference.</param>
		/// <param name="transfer">Transfer.</param>
		public ACColorIndicator(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
		{
			Initialize();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACColorIndicator"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attr">Attr.</param>
		/// <param name="defStyle">Def style.</param>
		public ACColorIndicator(Context context, IAttributeSet attr, int defStyle) : base(context, attr, defStyle)
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

			//Setup initial layout position and size 
			var layout = new RelativeLayout.LayoutParams(24, 24);
			layout.TopMargin = 0;
			layout.RightMargin = 0;
			this.LayoutParameters = layout;

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

		#region Private Drawing Methods
		/// <summary>
		/// Draws the oval.
		/// </summary>
		/// <param name="canvas">Canvas.</param>
		/// <param name="appearance">Appearance.</param>
		private void DrawOval(Canvas canvas) {

			// Define bounds
			int x = 0;
			int y = 0;
			int width = this.Width;
			int height = this.Height;

			// Fill body
			var oval = new ShapeDrawable (new OvalShape ());
			oval.Paint.Color = CurrentColor;
			oval.SetBounds (x,y,width,height);
			oval.Draw (canvas);

			// Outer body frame
			var ovalFrame = new ShapeDrawable (new OvalShape ());
			ovalFrame.Paint.Color=Color.Black;
			ovalFrame.Paint.SetStyle (Paint.Style.Stroke);
			ovalFrame.Paint.StrokeWidth = 2;
			ovalFrame.SetBounds (x,y,width,height);
			ovalFrame.Draw (canvas);

			// Inner body frame
			ovalFrame = new ShapeDrawable(new OvalShape());
			ovalFrame.Paint.Color = Color.White;
			ovalFrame.Paint.SetStyle(Paint.Style.Stroke);
			ovalFrame.Paint.StrokeWidth = 2;
			ovalFrame.SetBounds(x + 2, y + 2, width - 4, height - 4);
			ovalFrame.Draw(canvas);

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
			DrawOval(canvas);

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
			// Pass through touch events
			return false;
		}
		#endregion
	}
}
