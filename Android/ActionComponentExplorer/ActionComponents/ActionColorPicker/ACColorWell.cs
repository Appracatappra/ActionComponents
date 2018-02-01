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
	/// <summary>
	/// The Action Color Well displays a framed color well that shows the currently selected color and allows the user
	/// to tap the well which raises the <c>Touched</c> event.
	/// </summary>
	public class ACColorWell : ACView
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
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACColorWell"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		public ACColorWell(Context context) : base(context)
		{
			Initialize();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACColorWell"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attr">Attr.</param>
		public ACColorWell(Context context, IAttributeSet attr) : base(context,attr)
		{
			Initialize();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACColorWell"/> class.
		/// </summary>
		/// <param name="javaReference">Java reference.</param>
		/// <param name="transfer">Transfer.</param>
		public ACColorWell(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference,transfer)
		{
			Initialize();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACColorWell"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attr">Attr.</param>
		/// <param name="defStyle">Def style.</param>
		public ACColorWell(Context context, IAttributeSet attr, int defStyle) : base(context,attr,defStyle)
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

			#if TRIAL
				Android.Widget.Toast.MakeText(this.Context, "ACColorWell by Appracatappra, LLC.", Android.Widget.ToastLength.Short).Show();
			#endif

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
		/// Draws the well.
		/// </summary>
		/// <param name="canvas">Canvas.</param>
		/// <param name="appearance">Appearance.</param>
		private void DrawWell(Canvas canvas)
		{

			// Define bounds
			int x = 0;
			int y = 0;
			int width = this.Width;
			int height = this.Height;

			// Fill body
			var body = new ShapeDrawable(new RectShape());
			body.Paint.Color = CurrentColor;
			body.SetBounds(x, y, width, height);
			body.Draw(canvas);

			// Outer body frame
			var outerFrame = new ShapeDrawable(new RectShape());
			outerFrame.Paint.Color = Color.Black;
			outerFrame.Paint.SetStyle(Paint.Style.Stroke);
			outerFrame.Paint.StrokeWidth = 2;
			outerFrame.SetBounds(x, y, width, height);
			outerFrame.Draw(canvas);

			// Inner body frame
			var innerFrame = new ShapeDrawable(new RectShape());
			innerFrame.Paint.Color = Color.White;
			innerFrame.Paint.SetStyle(Paint.Style.Stroke);
			innerFrame.Paint.StrokeWidth = 2;
			innerFrame.SetBounds(x + 2, y + 2, width - 4, height - 4);
			innerFrame.Draw(canvas);

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
			DrawWell(canvas);

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
		#endregion
	}
}
