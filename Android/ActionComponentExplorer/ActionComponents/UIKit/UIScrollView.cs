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
	/// Simulates the iOS <c>UIScrollView</c> to ease porting UI code from iOS to Android. NOTE: Only a small percentage
	/// of <c>UIScrollView</c> has been ported to support the Action Components.
	/// </summary>
	public class UIScrollView : UIView
	{
		#region Private Variables
		private CGPoint _startLocation = new CGPoint();
		private CGSize _contentSize = new CGSize();
		private CGPoint _contentOffset = new CGPoint();
		private UIView _contentView;
		private bool _dragging = false;
		private bool _redrawing = false;
		#endregion

		#region Computed Properties
		/// <summary>
		/// Gets or sets the size of the content.
		/// </summary>
		/// <value>The size of the content.</value>
		public CGSize ContentSize {
			get { return _contentSize; }
			set {
				_contentSize = value;
				ValidateOffsets();
				ValidateContentSize();
				SetNeedsLayout();
			}
		}

		/// <summary>
		/// Gets or sets the content offset.
		/// </summary>
		/// <value>The content offset.</value>
		public CGPoint ContentOffset {
			get { return _contentOffset; }
			set {
				_contentOffset = value;
				ValidateOffsets();
				ValidateContentSize();
				SetNeedsLayout();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="T:UIKit.UIScrollView"/> is bounces.
		/// </summary>
		/// <value><c>true</c> if bounces; otherwise, <c>false</c>.</value>
		public bool Bounces { get; set; } = false;

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="T:UIKit.UIScrollView"/> always bounce vertical.
		/// </summary>
		/// <value><c>true</c> if always bounce vertical; otherwise, <c>false</c>.</value>
		public bool AlwaysBounceVertical { get; set; } = false;

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="T:UIKit.UIScrollView"/> always bounce horizontal.
		/// </summary>
		/// <value><c>true</c> if always bounce horizontal; otherwise, <c>false</c>.</value>
		public bool AlwaysBounceHorizontal { get; set; } = false;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:UIKit.UIScrollView"/> class.
		/// </summary>
		public UIScrollView() : base(Android.App.Application.Context)
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:UIKit.UIScrollView"/> class.
		/// </summary>
		/// <param name="rect">Rect.</param>
		public UIScrollView(CGRect rect) : base(Android.App.Application.Context)
		{
			// Init
			Initialize();

			// Set initial size
			this.Frame = rect;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:UIKit.UIScrollView"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		public UIScrollView(Context context) : base(context)
		{
			Initialize();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:UIKit.UIScrollView"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		public UIScrollView(Context context, CGRect rect) : base(context)
		{
			Initialize();

			// Set initial size
			this.Frame = rect;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:UIKit.UIScrollView"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attr">Attr.</param>
		public UIScrollView(Context context, IAttributeSet attr) : base(context, attr)
		{
			Initialize();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:UIKit.UIScrollView"/> class.
		/// </summary>
		/// <param name="javaReference">Java reference.</param>
		/// <param name="transfer">Transfer.</param>
		public UIScrollView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
		{
			Initialize();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:UIKit.UIScrollView"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attr">Attr.</param>
		/// <param name="defStyle">Def style.</param>
		public UIScrollView(Context context, IAttributeSet attr, int defStyle) : base(context, attr, defStyle)
		{
			Initialize();
		}

		/// <summary>
		/// Initialize this instance.
		/// </summary>
		internal void Initialize()
		{
			// Setup properties
			this.UserInteractionEnabled = true;
			this.MultipleTouchEnabled = true;
			this.ExclusiveTouch = true;
			this.ClipsToBounds = true;

			// Create content view
			_contentView = new UIView(this.Context);
			_contentView.ExclusiveTouch = false;
			_contentView.ClipsToBounds = true;
			_contentView.CacheViewDrawing = false;

			// Add content view to self
			AddView(_contentView);
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Validates the offsets.
		/// </summary>
		private void ValidateOffsets() {
			// Verify horizontal offset
			if (ContentSize.Width <= Frame.Width) {
				_contentOffset.X = 0;
			} else if (_contentOffset.X > ContentSize.Width - Frame.Width) {
				_contentOffset.X = ContentSize.Width - Frame.Width;
			} else if (_contentOffset.X < 0) {
				_contentOffset.X = 0;
			}

			// Verify vertical offset
			if (ContentSize.Height <= Frame.Height) {
				_contentOffset.Y = 0;
			} else if (_contentOffset.Y > ContentSize.Height - Frame.Height) {
				_contentOffset.Y = ContentSize.Height - Frame.Height;
			} else if (_contentOffset.Y < 0) {
				_contentOffset.Y = 0;
			}
		}

		/// <summary>
		/// Validates the size of the content.
		/// </summary>
		private void ValidateContentSize() {

			// Make content view fill scroll container if the scroll contents are smaller than will
			// than the scroll view's size
			var contentWidth = (ContentSize.Width > Frame.Width) ? ContentSize.Width : Frame.Width;
			var contentHeight = (ContentSize.Height > Frame.Height) ? ContentSize.Height : Frame.Height;

			// Move the container into position
			_contentView.Frame = new CGRect(0f - ContentOffset.X, 0f - ContentOffset.Y, contentWidth, contentHeight);
		}

		private void RedrawChildViews(UIView parentView) {

			// Force all child items to redraw
			foreach (UIView view in parentView.Subviews)
			{
				// Force the view to update
				view.SetNeedsDisplay();

				// Cascade redraw
				RedrawChildViews(view);
			}
		}
		#endregion

		#region Public Methods
		public void Redraw() {

			// Redrawing?
			if (_redrawing) {
				// Yes, abort we are already drawing on another thread.
				return;
			}

			var visibleRect = new CGRect(ContentOffset.X, ContentOffset.Y, ContentOffset.X + Frame.Width, ContentOffset.Y + Frame.Height);
			_redrawing = true;

			// Force all child items to redraw
			foreach (UIView view in _contentView.Subviews)
			{
				// Inside visible area?
				if (view.Frame.IntersectsWith(visibleRect)) {
					InvokeOnMainThread(() => {
						// Force the view to update
						view.SetNeedsDisplay();

						// Update any children
						if (!visibleRect.Contains(view.Frame)) {
							RedrawChildViews(view);
						}
					});
				}
			}

			// Finished redrawing
			_redrawing = false;
		}
		#endregion

		#region Override Methods
		/// <Docs>Set containing the touches.</Docs>
		/// <summary>
		/// Sent when one or more fingers touches the screen.
		/// </summary>
		/// <param name="touches">Touches.</param>
		/// <param name="evt">Evt.</param>
		public override void TouchesBegan(NSSet touches, UIEvent evt)
		{
			// Save starting location
			var pt = (touches.AnyObject as UITouch).LocationInView(this);
			_startLocation = pt;

			// Pass call to base object
			base.TouchesBegan(touches, evt);
		}

		/// <Docs>Set containing the touches.</Docs>
		/// <summary>
		/// Sent when the <c>ACTile</c> is being dragged
		/// </summary>
		/// <param name="touches">Touches.</param>
		/// <param name="evt">Evt.</param>
		public override void TouchesMoved(NSSet touches, UIEvent evt)
		{
			// Get new location
			var pt = (touches.AnyObject as UITouch).LocationInView(this);
			_dragging = true;

			// Adjust offsets
			ContentOffset.X += _startLocation.X - pt.X;
			ContentOffset.Y += _startLocation.Y - pt.Y;
			_startLocation = pt;

			// Verify and reflow
			ValidateOffsets();
			ValidateContentSize();
			Redraw();

			// Inform caller of event
			RaiseScrolled();

			// Pass call to base object
			base.TouchesMoved(touches, evt);
		}

		/// <Docs>Set containing the touches.</Docs>
		/// <summary>
		/// Send when one or more fingers are lifted from the screen.
		/// </summary>
		/// <param name="touches">Touches.</param>
		/// <param name="evt">Evt.</param>
		public override void TouchesEnded(NSSet touches, UIEvent evt)
		{
			// Clear starting point
			_startLocation = new CGPoint(0, 0);

			// Completing a drag operation?
			if (_dragging) {
				RedrawChildViews(_contentView);
				_dragging = false;
			}

			// Pass call to base object
			base.TouchesEnded(touches, evt);
		}

		/// <summary>
		/// Adds the subview.
		/// </summary>
		/// <param name="view">View.</param>
		public override void AddSubview(UIView view)
		{
			// Add view to the content container
			_contentView.AddSubview(view);

			// Update layout
			LayoutSubviews();
		}

		/// <summary>
		/// Layouts the subviews.
		/// </summary>
		public override void LayoutSubviews()
		{
			base.LayoutSubviews();

			// Move the content view
			if (_contentView != null) {
				_contentView.Frame.Location = new CGPoint(0f - ContentOffset.X, 0f - ContentOffset.Y);
			}

		}
		#endregion

		#region Events
		/// <summary>
		///Occurs when the scroll view is scrolled.
		/// </summary>
		public delegate void UIScrolledDelegate(UIScrollView scrollview);
		public event UIScrolledDelegate Scrolled;

		/// <summary>
		/// Raises the scrolled event.
		/// </summary>
		internal void RaiseScrolled() {
			if (this.Scrolled != null) this.Scrolled(this);
		}
		#endregion
	}
}
