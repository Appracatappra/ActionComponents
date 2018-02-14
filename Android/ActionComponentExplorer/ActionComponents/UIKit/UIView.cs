using System;
using System.Threading;
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
using Android.Animation;
using Foundation;
using CoreGraphics;
using ActionComponents;

// Mappings "Unified" iOS types to Android types
using nfloat = global::System.Single;
using nint = global::System.Int32;
using nuint = global::System.UInt32;

namespace UIKit
{
	/// <summary>
	/// Base class used for components that want to render themselves and respond to events.
	/// </summary>
	public class UIView : RelativeLayout
	{
		#region Private Variables
		private Bitmap _imageCache;
		private bool _clipsToBounds = false;
		#endregion

		#region Computed Properties
		/// <summary>
		/// Gets or sets the frame.
		/// </summary>
		/// <value>The frame.</value>
		public CGRect Frame
		{
			get
			{
				var left = ACView.GetViewLeftMargin(this);
				var top = ACView.GetViewTopMargin(this);
				var width = ACView.GetViewWidth(this);
				var height = ACView.GetViewHeight(this);
				return new CGRect(left, top, width, height);
			}
			set
			{
				ACView.SetViewLeftMargin(this, (int)value.X);
				ACView.SetViewTopMargin(this, (int)value.Y);
				ACView.SetViewWidth(this, (int)value.Width);
				ACView.SetViewHeight(this, (int)value.Height);
			}
		}

		/// <summary>
		/// Gets or sets the bounds.
		/// </summary>
		/// <value>The bounds.</value>
		public CGRect Bounds
		{
			get { return Frame; }
			set { Frame = value; }
		}

		/// <summary>
		/// Gets or sets the draw canvas.
		/// </summary>
		/// <value>The draw canvas.</value>
		public Canvas DrawCanvas { get; set; } = null;

		/// <summary>
		/// Gets or sets the color of the background.
		/// </summary>
		/// <value>The color of the background.</value>
		public ACColor BackgroundColor {
			get {
				if (Background is ColorDrawable) {
					var backgroundColor = (Background as ColorDrawable).Color;
					return backgroundColor;
				} else {
					return ACColor.White;
				}
			}
			set {
				SetBackgroundColor(value);
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="T:UIKit.UIView"/> user interaction enabled.
		/// </summary>
		/// <value><c>true</c> if user interaction enabled; otherwise, <c>false</c>.</value>
		public bool UserInteractionEnabled { get; set; } = false;

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="T:UIKit.UIView"/> multiple touch enabled.
		/// </summary>
		/// <value><c>true</c> if multiple touch enabled; otherwise, <c>false</c>.</value>
		public bool MultipleTouchEnabled { get; set; } = false;

		/// <summary>
		/// Gets or sets the last touch point.
		/// </summary>
		/// <value>The last touch point.</value>
		public CGPoint LastTouchPoint { get; set; } = new CGPoint(0, 0);

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="T:UIKit.UIView"/> clips to bounds.
		/// </summary>
		/// <value><c>true</c> if clips to bounds; otherwise, <c>false</c>.</value>
		public bool ClipsToBounds {
			get { return _clipsToBounds; }
			set {
				_clipsToBounds = value;
				SetClipChildren(value);
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="T:UIKit.UIView"/> exclusive touch.
		/// </summary>
		/// <value><c>true</c> if exclusive touch; otherwise, <c>false</c>.</value>
		public bool ExclusiveTouch { get; set; } = true;

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="T:UIKit.UIView"/> cache view drawing to improve
		/// redraw performance.
		/// </summary>
		/// <value><c>true</c> if cache view drawing; otherwise, <c>false</c>.</value>
		public bool CacheViewDrawing { get; set; } = true;

		/// <summary>
		/// Gets the subviews.
		/// </summary>
		/// <value>The subviews.</value>
		public List<UIView> Subviews {
			get {
				var subviews = new List<UIView>();

				// Find all subviews
				for (int n = 0; n < ChildCount; ++n) {
					var child = GetChildAt(n);
					if (child is UIView) {
						subviews.Add((UIView)child);
					}
				}

				// Return list
				return subviews;
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:UIKit.UIView"/> class.
		/// </summary>
		public UIView() : base(Android.App.Application.Context) {
			// Init
			InitializeView();
		} 

		/// <summary>
		/// Initializes a new instance of the <see cref="T:UIKit.UIView"/> class.
		/// </summary>
		/// <param name="rect">Rect.</param>
		public UIView(CGRect rect) : base(Android.App.Application.Context)
		{
			// Init
			InitializeView();

			// Set initial size
			this.Frame = rect;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:UIKit.UIView"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		public UIView(Context context) : base(context)
		{
			InitializeView();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:UIKit.UIView"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attr">Attr.</param>
		public UIView(Context context, IAttributeSet attr) : base(context,attr)
		{
			InitializeView();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:UIKit.UIView"/> class.
		/// </summary>
		/// <param name="javaReference">Java reference.</param>
		/// <param name="transfer">Transfer.</param>
		public UIView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference,transfer)
		{
			InitializeView();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:UIKit.UIView"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attr">Attr.</param>
		/// <param name="defStyle">Def style.</param>
		public UIView(Context context, IAttributeSet attr, int defStyle) : base(context,attr,defStyle)
		{
			InitializeView();
		}

		/// <summary>
		/// Initialize this instance.
		/// </summary>
		internal void InitializeView()
		{
			// Does this view already have any layout parameters?
			if (this.LayoutParameters == null) {
				var layout = new RelativeLayout.LayoutParams(24, 24);
				layout.TopMargin = 0;
				layout.RightMargin = 0;
				this.LayoutParameters = layout;
			}
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Sets the needs display flag.
		/// </summary>
		public void SetNeedsDisplay()
		{
			// Clear buffer
			if (_imageCache != null)
			{
				_imageCache.Dispose();
				_imageCache = null;
			}

			// Force a redraw
			this.Invalidate();
		}

		/// <summary>
		/// Sets the needs layout.
		/// </summary>
		public void SetNeedsLayout() {

			// Call the layout routines
			LayoutSubviews();
		}

		/// <summary>
		/// Draw the specified rect.
		/// </summary>
		/// <returns>The draw.</returns>
		/// <param name="rect">Rect.</param>
		public virtual void Draw(CGRect rect) {
			
		}

		/// <summary>
		/// Lays out any sub view in the control.
		/// </summary>
		public virtual void LayoutSubviews() {
			
		}

		/// <summary>
		/// Sent when one or more fingers touches the screen.
		/// </summary>
		/// <param name="touches">Touches.</param>
		/// <param name="evt">Evt.</param>
		public virtual void TouchesBegan(NSSet touches, UIEvent evt)
		{
		}

		/// <summary>
		/// Sent when the <c>ACTile</c> is being dragged
		/// </summary>
		/// <param name="touches">Touches.</param>
		/// <param name="evt">Evt.</param>
		public virtual void TouchesMoved(NSSet touches, UIEvent evt)
		{
		}

		/// <Docs>Set containing the touches.</Docs>
		/// <summary>
		/// Send when one or more fingers are lifted from the screen.
		/// </summary>
		/// <param name="touches">Touches.</param>
		/// <param name="evt">Evt.</param>
		public virtual void TouchesEnded(NSSet touches, UIEvent evt)
		{
		}

		/// <summary>
		/// Adds the subview.
		/// </summary>
		/// <param name="view">View.</param>
		public virtual void AddSubview(UIView view) {
			AddView(view);
		}

		/// <summary>
		/// Invokes the given action on the main UI thread.
		/// </summary>
		/// <param name="action">Action.</param>
		public void InvokeOnMainThread(Action action) {
			// Get the current activity
			var activity = (Activity)Context;

			// Run the action on the main thread
			activity.RunOnUiThread(action);
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Populates the image cache by calling the synthetic Draw routine.
		/// </summary>
		/// <returns>The image cache.</returns>
		private Bitmap PopulateImageCache()
		{

			// Create a temporary canvas
			DrawCanvas = new Canvas();

			// Clear existing image cache
			if (_imageCache != null) {
				_imageCache.Dispose();
				_imageCache = null;
			}

			// Create bitmap storage and assign to canvas
			var controlBitmap = Bitmap.CreateBitmap(this.Width, this.Height, Bitmap.Config.Argb8888);
			DrawCanvas.SetBitmap(controlBitmap);

			// Call the built in draw routine
			var drawRect = new CGRect(0, 0, Frame.Width, Frame.Height);
			Draw(drawRect);

			// Release the canvas
			DrawCanvas.Dispose();
			DrawCanvas = null;

			// Return new cache
			return controlBitmap;
		}
		#endregion

		#region Override Methods
		/// <summary>
		/// Called when the view needs to draw itself.
		/// </summary>
		/// <param name="canvas">Canvas.</param>
		protected override void OnDraw(Canvas canvas)
		{
			// Call base
			base.OnDraw(canvas);

			// Cache view drawing?
			if (CacheViewDrawing) {
				// Restoring image from cache?
				if (_imageCache == null) _imageCache = PopulateImageCache();

				// Draw cached image to canvas
				canvas.DrawBitmap(_imageCache, 0, 0, null);
			} else {
				// Save reference to canvas
				DrawCanvas = canvas;

				// Call the built in draw routine
				var drawRect = new CGRect(0, 0, Frame.Width, Frame.Height);
				Draw(drawRect);
			}
		}

		/// <summary>
		/// Called when the view is being laid out.
		/// </summary>
		/// <param name="changed">If set to <c>true</c> changed.</param>
		/// <param name="left">Left.</param>
		/// <param name="top">Top.</param>
		/// <param name="right">Right.</param>
		/// <param name="bottom">Bottom.</param>
		protected override void OnLayout(bool changed, nint left, nint top, nint right, nint bottom)
		{
			base.OnLayout(changed, left, top, right, bottom);

			// Call UIView layout routines
			if (changed) LayoutSubviews();
		}

		/// <summary>
		/// Handle the touch event.
		/// </summary>
		/// <returns><c>true</c>, if touch event was oned, <c>false</c> otherwise.</returns>
		/// <param name="e">E.</param>
		public override bool OnTouchEvent(MotionEvent e)
		{
			// Save the last touch point
			LastTouchPoint.X = e.GetX();
			LastTouchPoint.Y = e.GetY();

			// Are we handling touch events?
			if (UserInteractionEnabled) {
				// Yes, build required events
				var touchEvent = new UIEvent(e);
				var touches = touchEvent.TouchesForView(this);

				// Take action based on the event type
				switch (e.Action)
				{
					case MotionEventActions.Down:
						// Call UIKit touch handler
						TouchesBegan(touches, touchEvent);

						// Inform system that we've handled this event 
						if (ExclusiveTouch) {
							return true;
						}
						break;
					case MotionEventActions.Move:
						// Call UIKit touch handler
						TouchesMoved(touches, touchEvent);

						// Inform system that we've handled this event 
						if (ExclusiveTouch)
						{
							return true;
						}
						break;
					case MotionEventActions.Up:
					case MotionEventActions.Cancel:
						// Call UIKit touch handler
						TouchesEnded(touches, touchEvent);

						// Inform system that we've handled this event 
						if (ExclusiveTouch)
						{
							return true;
						}
						break;
				}
			}

			// Not handled by this routine
			return base.OnTouchEvent(e);
		}
		#endregion
	}
}
