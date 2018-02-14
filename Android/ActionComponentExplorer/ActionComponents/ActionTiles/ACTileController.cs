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
using UIKit;

// Mappings "Unified" iOS types to Android types
using nfloat = global::System.Single;
using nint = global::System.Int32;
using nuint = global::System.UInt32;

namespace ActionComponents
{
	/// <summary>
	/// Tracks the state of the live update timer.
	/// </summary>
	public class TimerState
	{
		#region Computed Properties
		/// <summary>
		/// Gets or sets the counter.
		/// </summary>
		/// <value>The counter.</value>
		public int Counter { get; set; } = 0;

		/// <summary>
		/// Gets or sets the live update timer.
		/// </summary>
		/// <value>The live update timer.</value>
		public Timer LiveUpdateTimer { get; set; }
		#endregion
	}

	public class ACTileController : UIView
	{
		#region Private Variables
		private UIScrollView _scrollView;
		private ACTileControllerAppearance _appearance;
		private ACTileControllerScrollDirection _scrollDirection = ACTileControllerScrollDirection.Vertical;
		private bool _suspendUpdates = false;
		private bool _setScrollDirectionFromDeviceOrientation = false;
		private ACTileNavigationBar _navigationBar;
		private List<ACTileGroup> _groups = new List<ACTileGroup>();
		private ACTileGroupAppearance _groupAppearance;
		private ACTileAppearance _tileAppearance;
		private bool _liveUpdate = false;
		private int _liveUpdateFrequency = 5000;
		private TimerState _updateTimer;
		private ACTileLiveUpdate _liveUpdateAction;
		private bool _liveUpdateRunning = false;
		private bool _bringToFrontOnTouched;
		private CGPoint _startLocation;
		#endregion

		#region Computed Properties
		/// <summary>
		/// Gets or sets the [OPTIONAL] tag that can be assosciated with this ACTileController. 
		/// </summary>
		/// <value>The tag.</value>
		public Object tag { get; set; }

		/// <summary>
		/// Gets all of the <c>ACTileGroups</c> controlled by this <c>ACTileController</c>.
		/// </summary>
		/// <value>The groups in this controller.</value>
		public List<ACTileGroup> groups {
			get { return _groups; }
		}

		/// <summary>
		/// Gets or sets the .ACTileLiveUpdate action that will be performed via an automatic update
		/// kicked off by the <c>liveUpdateTimer</c> in the parent ACTileController 
		/// </summary>
		/// <value>The live update action.</value>
		public ACTileLiveUpdate liveUpdateAction
		{
			get { return _liveUpdateAction; }
			set { _liveUpdateAction = value; }
		}

		/// <summary>
		/// Gets a value indicating whether this
		/// <c>ACTileController</c> is running a live update
		/// </summary>
		/// <value><c>true</c> if live update running; otherwise, <c>false</c>.</value>
		public bool liveUpdateRunning
		{
			get { return _liveUpdateRunning; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="T:ActionComponents.ACTileController"/> sets scroll
		/// direction from device orientation.
		/// </summary>
		/// <value><c>true</c> if sets scroll direction from device orientation; otherwise, <c>false</c>.</value>
		public bool setScrollDirectionFromDeviceOrientation
		{
			get { return _setScrollDirectionFromDeviceOrientation; }
			set
			{
				_setScrollDirectionFromDeviceOrientation = value;

				// Calculate orientation
				if (_setScrollDirectionFromDeviceOrientation)
				{
					CalculateScrollDirection();

					// Should the screen refresh?
					if (!_suspendUpdates)
					{
						ResizeElements();
						Redraw();
					}
				}
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <c>ACTileController</c> suspends updates while <c>ACTileGroup</c>s and/or
		/// <c>ACTile</c>s are being added, edited or removed.  
		/// </summary>
		/// <value><c>true</c> if suspend updates; otherwise, <c>false</c>.</value>
		/// <remarks>You should set this property to <c>true</c> when adding or removing large sums of <c>ACTileGroup</c>s
		/// or <c>ACTile</c>s.</remarks>
		public bool suspendUpdates
		{
			get { return _suspendUpdates; }
			set
			{
				_suspendUpdates = value;

				// Should the screen refresh?
				if (!_suspendUpdates)
				{
					ResizeElements();
					Redraw();
				}
			}
		}

		/// <summary>
		/// Gets the <c>ACTileNavigationBar</c> attached to this <c>ACTileController</c>  
		/// </summary>
		/// <value>The navigation bar.</value>
		public ACTileNavigationBar navigationBar
		{
			get { return _navigationBar; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <c>ACTileController</c> does automatic "live updating" of the <c>ACTile</c>s
		/// inside it's <c>ACTileGroup</c>s  
		/// </summary>
		/// <value><c>true</c> if live update; otherwise, <c>false</c>.</value>
		public bool liveUpdate
		{
			get { return _liveUpdate; }
			set
			{
				_liveUpdate = value;

				//Starting or stopping updates?
				if (_liveUpdate)
				{
					//Is there currently a timer?
					if (_updateTimer == null)
					{
						// No, start a new timer
						var timerDelegate = new TimerCallback(AnimationTimeLoop);
						_updateTimer = new TimerState();
						_updateTimer.LiveUpdateTimer = new Timer(timerDelegate, _updateTimer, 0, _liveUpdateFrequency);
					}
				}
				else
				{
					//Is there currently a timer?
					if (_updateTimer != null)
					{
						//Yes, stop the time and release it
						_updateTimer.LiveUpdateTimer.Dispose();
						_updateTimer = null;
					}
				}
			}
		}


		/// <summary>
		/// Gets or sets the "live update" frequency for <c>ACTile</c>s in this 
		/// <c>ACTileController</c>'s <c>ACTileGroup</c>s
		/// </summary>
		/// <value>The live update frequency.</value>
		public int liveUpdateFrequency
		{
			get { return _liveUpdateFrequency; }
			set
			{
				_liveUpdateFrequency = value;

				//Starting or stopping updates?
				if (_liveUpdate)
				{
					//Is there currently a timer?
					if (_updateTimer != null)
					{
						//Yes, stop the time and release it
						_updateTimer.LiveUpdateTimer.Dispose();
						_updateTimer = null;
					}

					//Start a new timer
					var timerDelegate = new TimerCallback(AnimationTimeLoop);
					_updateTimer = new TimerState();
					_updateTimer.LiveUpdateTimer = new Timer(timerDelegate, _updateTimer, 0, _liveUpdateFrequency);
				}
			}
		}

		/// <summary>
		/// Gets or sets the default <c>ACTileGroupAppearance</c> for this 
		/// <c>ACTileController</c> 
		/// </summary>
		/// <value>The group appearance.</value>
		public ACTileGroupAppearance groupAppearance
		{
			get { return _groupAppearance; }
			set
			{
				_groupAppearance = value;
				CascadeAppearanceChange();
			}
		}

		/// <summary>
		/// Gets or sets the default <c>ACTileAppearance</c> for this
		/// <c>ACTileController</c> 
		/// </summary>
		/// <value>The tile appearance.</value>
		public ACTileAppearance tileAppearance
		{
			get { return _tileAppearance; }
			set
			{
				_tileAppearance = value;
				CascadeTileAppearanceChange();
			}
		}

		/// <summary>
		/// Gets or sets the <c>ACTileControllerScrollDirection</c> for this
		/// <c>ACTileController</c>  
		/// </summary>
		/// <value>The scroll direction.</value>
		/// <remarks>The default scroll direction will match the device's orientation</remarks>
		public ACTileControllerScrollDirection scrollDirection
		{
			get { return _scrollDirection; }
			set
			{
				_scrollDirection = value;

				// Should the screen refresh?
				if (!_suspendUpdates)
				{
					ResizeElements();
					Redraw();
				}
			}
		}

		/// <summary>
		/// Gets or sets the <c>ACTileControllerAppearance</c> fro this <c>ACTileController</c>  
		/// </summary>
		/// <value>The appearance.</value>
		public ACTileControllerAppearance appearance
		{
			get { return _appearance; }
			set
			{
				_appearance = value;

				//Wire-up events
				_appearance.AppearanceModified += () =>
				{
					ResizeElements();
					Redraw();
				};

				// Should the screen refresh?
				if (!_suspendUpdates)
				{
					ResizeElements();
					Redraw();
				}
			}
		}

		/// <summary>
		/// Gets the <c>UIScrollView</c> inside this <c>ACTileController</c> 
		/// </summary>
		/// <value>The scroll view.</value>
		public UIScrollView scrollView
		{
			get { return _scrollView; }
		}

		/// <summary>
		/// Gets or sets the navigation bar title.
		/// </summary>
		/// <value>The title.</value>
		public string title
		{
			get { return navigationBar.title; }
			set { navigationBar.title = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <c>ACTile</c>
		/// is enabled.
		/// </summary>
		/// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
		/// <remarks>Enabling/disabling a <c>ACTile</c> automatically changes the value of it's
		/// <c>UserInteractionEnabled</c> flag</remarks>
		public bool Enabled
		{
			get { return UserInteractionEnabled; }
			set { UserInteractionEnabled = value; }
		}

		/// <summary>
		/// Gets or sets the <c>ACTileGroup</c> at the specified index.
		/// </summary>
		/// <param name="index">Index.</param>
		public ACTileGroup this[int index]
		{
			get
			{
				return _groups[index];
			}

			set
			{
				_groups[index] = value;
			}
		}

		/// <summary>
		/// Gets the number of <c>ACTileGroup</c>s contained in this <c>ACTileController</c>  
		/// </summary>
		/// <value>The count.</value>
		public int Count
		{
			get { return _groups.Count; }
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACTileController"/> class.
		/// </summary>
		public ACTileController() : base(Android.App.Application.Context)
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACTileController"/> class.
		/// </summary>
		/// <param name="rect">Rect.</param>
		public ACTileController(CGRect rect) : base(Android.App.Application.Context)
		{
			// Init
			Initialize();

			// Set initial size
			this.Frame = rect;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACTileController"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		public ACTileController(Context context) : base(context)
		{
			Initialize();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACTileController"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attr">Attr.</param>
		public ACTileController(Context context, IAttributeSet attr) : base(context, attr)
		{
			Initialize();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACTileController"/> class.
		/// </summary>
		/// <param name="javaReference">Java reference.</param>
		/// <param name="transfer">Transfer.</param>
		public ACTileController(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
		{
			Initialize();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACTileController"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attr">Attr.</param>
		/// <param name="defStyle">Def style.</param>
		public ACTileController(Context context, IAttributeSet attr, int defStyle) : base(context, attr, defStyle)
		{
			Initialize();
		}

		/// <summary>
		/// Initialize this instance.
		/// </summary>
		private void Initialize()
		{

			//Insert an embedded scroll view controller
			this._scrollView = new UIScrollView(this.Context, this.Frame);
			this.scrollView.BackgroundColor = ACColor.Clear;
			this.scrollView.ContentSize = this.Frame.Size;
			this.scrollView.UserInteractionEnabled = true;
			this.scrollView.Bounces = true;
			AddSubview(this.scrollView);

			//Add a navigation bar
			this._navigationBar = new ACTileNavigationBar(this.Context, this);
			AddSubview(this._navigationBar);

			//Set defaults
			this.BackgroundColor = ACColor.Clear;
			this.appearance = new ACTileControllerAppearance();
			this.groupAppearance = new ACTileGroupAppearance();
			this.tileAppearance = new ACTileAppearance();
			this.UserInteractionEnabled = true;
			this.MultipleTouchEnabled = true;

			// Wire-up Navigation Bar events
			navigationBar.BarShown += (navigationBar) =>
			{
				ResizeElements();
				Redraw();
			};

			navigationBar.BarHidden += (navigationBar) =>
			{
				ResizeElements();
				Redraw();
			};

			// Wire-up events
			appearance.AppearanceModified += () => {
				ResizeElements();
				Redraw();
			};

			scrollView.Scrolled += (scroller) => {
				if (scrollDirection == ACTileControllerScrollDirection.Vertical && navigationBar.appearance.autoDarkenBackground) {
					// Have we moved pasted the threshold?
					if ((scrollView.ContentOffset.Y > navigationBar.appearance.barHeight - 20f) &&
					    !navigationBar.appearance._darkened){
						// Yes, darken bar
						navigationBar.appearance._darkened = true;
						navigationBar.appearance.RaiseAppearanceModified();
					} else if ((scrollView.ContentOffset.Y < navigationBar.appearance.barHeight - 20f) &&
					           navigationBar.appearance._darkened) {
						navigationBar.appearance._darkened = false;
						navigationBar.appearance.RaiseAppearanceModified();
					}
				}
			};
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Calculates the scroll direction.
		/// </summary>
		private void CalculateScrollDirection()
		{
			//Set the default scroll direction
			// TODO: Auto set this direction
			scrollDirection = ACTileControllerScrollDirection.Vertical;
		}

		/// <summary>
		/// Resizes the elements.
		/// </summary>
		private void ResizeElements()
		{
			var barHeight = (navigationBar.hidden ? 0 : navigationBar.appearance.totalHeight);
			nfloat x = 0, leftPad = 0;
			nfloat y = 0, rightPad = 0;

			// Take action based on the scroll direction
			switch(scrollDirection) {
				case ACTileControllerScrollDirection.Horizontal:
					x = appearance.groupGap;
					_scrollView.Frame = new CGRect(appearance.indentLeft,
					                               barHeight + appearance.indentTop,
					                               Frame.Width - (appearance.indentLeft + appearance.indentRight),
					                               Frame.Height - (barHeight + appearance.indentTop + appearance.indentBottom));
					break;
				case ACTileControllerScrollDirection.Vertical:
					y = barHeight + appearance.groupGap;
					_scrollView.Frame = new CGRect(appearance.indentLeft + appearance.groupGap + leftPad,
												   appearance.indentTop,
					                               Frame.Width - (appearance.indentLeft + leftPad + appearance.indentRight + rightPad + (appearance.groupGap * 2f)),
												   Frame.Height - (appearance.indentTop + appearance.indentBottom));
					break;
			}

			// Recalculate the size of all groups in this controller
			foreach (ACTileGroup group in _groups)
			{
				group.RecalculateSize();

				//Take action based on the scroll direction of this controller
				switch (scrollDirection)
				{
					case ACTileControllerScrollDirection.Horizontal:
						group.MoveToPoint(x, y);
						x += group.Frame.Width + appearance.groupGap;
						break;
					case ACTileControllerScrollDirection.Vertical:
						group.MoveToPoint(x, y);
						y += group.Frame.Height + appearance.groupGap;
						break;
				}
			}

			// Include buffer for bounce?
			var buffer = 0f;
			if  (scrollView.Bounces) {
				buffer = 100f;
			}

			// Adjust scroll size based on scroll direction
			switch (scrollDirection)
			{
				case ACTileControllerScrollDirection.Horizontal:
					scrollView.ContentSize = new CGSize(x + appearance.indentRight + buffer, scrollView.Frame.Height);
					scrollView.AlwaysBounceVertical = false;
					scrollView.AlwaysBounceHorizontal = true;
					break;
				case ACTileControllerScrollDirection.Vertical:
					scrollView.ContentSize = new CGSize(scrollView.Frame.Width, y + appearance.indentBottom + buffer);
					scrollView.AlwaysBounceVertical = true;
					scrollView.AlwaysBounceHorizontal = false;
					break;
			}
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Adds a new group to the tile controller with the given properties.
		/// </summary>
		/// <returns>The new group that was created.</returns>
		/// <param name="groupType">The type of group to create.</param>
		/// <param name="title">The title of the new group.</param>
		/// <param name="footer">The footer for the new group.</param>
		public ACTileGroup AddGroup(ACTileGroupType groupType, string title, string footer) {
			var group = new ACTileGroup(this, groupType, title, footer);
			group.defaultTileAppearance = tileAppearance.Clone();

			// Add to collection
			AddGroup(group);

			// Return new group
			return group;
		}

		/// <summary>
		/// Adds a new group to the tile controller with the given properties.
		/// </summary>
		/// <param name="group">Group.</param>
		public void AddGroup(ACTileGroup group) {

			// Save group and add to scrollview
			_groups.Add(group);
			_scrollView.AddSubview(group);

			// Wireup events
			group.TileTouched += (g, tile) => {
				RaiseTileTouched(g, tile);
			};

			//Update display
			Redraw();
		}

		/// <summary>
		/// Removes the <c>ACTilegGroup</c> at index.
		/// </summary>
		/// <param name="index">Index.</param>
		public void RemoveGroupAt(int index)
		{
			_groups.RemoveAt(index);
			ResizeElements();
			Redraw();
		}

		/// <summary>
		/// Removes all <c>ACTileGroup</c>s 
		/// </summary>
		public void ClearGroups()
		{
			_groups.Clear();
			ResizeElements();
			Redraw();
		}

		/// <summary>
		/// Redraw this instance.
		/// </summary>
		public void Redraw()
		{
			// Is updating suspended?
			if (suspendUpdates)
				return;

			// Force component to update view
			SetNeedsLayout();
			SetNeedsDisplay();
		}

		/// <summary>
		/// Moves this <c>ACTile</c> to the given point and honors any
		/// <c>ACTileDragConstraint</c>s applied to the <c>X</c> or <c>Y</c> axis 
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public void MoveToPoint(float x, float y)
		{
			//Create a new point and move to it
			MoveToPoint(new CGPoint(x, y));
		}

		/// <summary>
		/// Moves this <c>ACTile</c> to the given point and honors any
		/// <c>ACTileDragConstraint</c>s applied to the <c>X</c> or <c>Y</c> axis 
		/// </summary>
		/// <param name="pt">Point.</param>
		public void MoveToPoint(CGPoint pt)
		{
			//Move to the given location
			Frame = new CGRect(pt.X, pt.Y, Frame.Width, Frame.Height);
		}

		/// <summary>
		/// Resize this <c>ACTile</c> to the specified width and height.
		/// </summary>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		public void Resize(float width, float height)
		{
			//Resize this view
			Frame = new CGRect(Frame.Left, Frame.Top, width, height);
		}

		/// <summary>
		/// Tests to see if the given x and y coordinates are inside this <c>ACTile</c> 
		/// </summary>
		/// <returns><c>true</c>, if the point was inside, <c>false</c> otherwise.</returns>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public bool PointInside(nfloat x, nfloat y)
		{
			//Is the give x inside
			if (x >= Frame.X && x <= (Frame.X + Frame.Width))
			{
				if (y >= Frame.Y && y <= (Frame.Y + Frame.Height))
				{
					//Inside
					return true;
				}
			}

			//Not inside
			return false;
		}

		/// <summary>
		/// Test to see if the given point is inside this <c>ACTile</c> 
		/// </summary>
		/// <returns><c>true</c>, if point was inside, <c>false</c> otherwise.</returns>
		/// <param name="pt">Point.</param>
		public bool PointInside(CGPoint pt)
		{
			return PointInside(pt.X, pt.Y);
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Animations the time loop.
		/// </summary>
		private void AnimationTimeLoop(Object state)
		{
			// Get the current activity
			var activity = (Activity)Context;

			// Run the update on the main UI thread.
			activity.RunOnUiThread(() =>
			{
				// Inform caller that we're started a live update
				RaiseLiveUpdating();

				// Perform any updates on the groups
				foreach (ACTileGroup group in _groups)
				{
					// Does this group contain a live update action?
					if (group.liveUpdateAction != null)
					{
						// Yes, call it into action
						group.liveUpdateAction.PerformUpdate();
					}

					// Check all tiles in the group for a live update
					group.LiveUpdateTiles();

					// Tell the group that it is live updating
					group.RaiseLiveUpdating();
				}

				// Kickoff the live update action for the controller itself
				if (liveUpdateAction != null)
				{
					liveUpdateAction.PerformUpdate();
				}
			});

		}

		/// <summary>
		/// Cascades the appearance change to every <c>ACTileGroup</c> in this controller
		/// </summary>
		private void CascadeAppearanceChange()
		{

			//Pass change down to each group
			foreach (ACTileGroup group in _groups)
			{
				group.appearance = groupAppearance;
			}

		}

		/// <summary>
		/// Cascades the tile appearance change to every <c>ACTileGroup</c> in this controller
		/// </summary>
		private void CascadeTileAppearanceChange()
		{

			//Pass change down to each group
			foreach (ACTileGroup group in _groups)
			{
				group.defaultTileAppearance = tileAppearance;
			}

		}
		#endregion

		#region Override Methods
		/// <summary>
		/// Layouts the subviews.
		/// </summary>
		public override void LayoutSubviews()
		{
			base.LayoutSubviews();

			// Force a redraw
			//Redraw();

			// Adjust scroll direction?
			if (setScrollDirectionFromDeviceOrientation)
			{
				CalculateScrollDirection();
			}

			// Adjust the navigation bar
			navigationBar.Resize(Frame.Width);

			// Resize all sub elements
			ResizeElements();
		}

		/// <Docs>Set containing the touches.</Docs>
		/// <summary>
		/// Sent when one or more fingers touches the screen.
		/// </summary>
		/// <param name="touches">Touches.</param>
		/// <param name="evt">Evt.</param>
		public override void TouchesBegan(NSSet touches, UIEvent evt)
		{
			
			//Save starting location
			var pt = (touches.AnyObject as UITouch).LocationInView(this);
			_startLocation = pt;

			//Automatically bring view to front?
			//if (_bringToFrontOnTouched && this.Superview != null) this.Superview.BringSubviewToFront(this);

			//Inform caller of event
			RaiseTouched();

			//Pass call to base object
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
			//Pass call to base object
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
			//Clear starting point
			_startLocation = new CGPoint(0, 0);

			//Inform caller of event
			RaiseReleased();

			//Pass call to base object
			base.TouchesEnded(touches, evt);
		}

		/// <summary>
		/// Draw the specified rect.
		/// </summary>
		/// <param name="rect">Rect.</param>
		public override void Draw(CGRect rect)
		{
			base.Draw(rect);

			//// General Declarations
			var context = UIGraphics.GetCurrentContext(this);

			//// Backdrop Drawing
			var backdropPath = UIBezierPath.FromRect(rect);

			//Fill with color
			appearance.background.SetFill();
			backdropPath.Fill();

			// Get background image
			var image = appearance.backgroundImage;

			//Is there an image?
			if (image != null)
			{
				var imageRect = rect;
				nfloat r = 0;

				// Landscape, protrait or square image?
				if (image.Size.Width > image.Size.Height) {
					// Landscape image moving into what state?
					r = image.Size.Width / image.Size.Height;
					imageRect = new CGRect(0, 0, rect.Height * r, rect.Height);

					// Does it fill the width?
					if (imageRect.Width < rect.Width) {
						r = rect.Width / imageRect.Width;
						imageRect = new CGRect(0, 0, imageRect.Width * r, imageRect.Height * r);
					}
				} else {
					// Portrait or square
					r = image.Size.Height / image.Size.Width;
					imageRect = new CGRect(0, 0, rect.Width, rect.Width * r);

					// Does it fill the height?
					if (imageRect.Height < rect.Height) {
						r = rect.Height / imageRect.Height;
						imageRect = new CGRect(0, 0, imageRect.Width * r, imageRect.Height * r);
					}
				}

				//Draw background image
				context.SaveState();
				backdropPath.AddClip();
				image.Draw(imageRect);
				context.RestoreState();
			}
			appearance.border.SetStroke();
			backdropPath.LineWidth = 1;
			backdropPath.Stroke();

		}
		#endregion

		#region Events
		/// <summary>
		/// Occurs when this <c>ACTileController</c> is touched 
		/// </summary>
		public delegate void ACTileControllerTouchedDelegate(ACTileController view);
		public event ACTileControllerTouchedDelegate Touched;

		/// <summary>
		/// Raises the touched event
		/// </summary>
		private void RaiseTouched()
		{
			if (this.Touched != null)
				this.Touched(this);
		}

		/// <summary>
		/// Occurs when this <c>ACTileController</c> is moved
		/// </summary>
		public delegate void ACTileControllerMovedDelegate(ACTileController view);
		public event ACTileControllerMovedDelegate Moved;

		/// <summary>
		/// Raises the moved event
		/// </summary>
		private void RaiseMoved()
		{
			if (this.Moved != null)
				this.Moved(this);
		}

		/// <summary>
		/// Occurs when this <c>ACTileController</c> is released 
		/// </summary>
		public delegate void ACTileControllerReleasedDelegate(ACTileController view);
		public event ACTileControllerReleasedDelegate Released;

		/// <summary>
		/// Raises the released.
		/// </summary>
		private void RaiseReleased()
		{
			if (this.Released != null)
				this.Released(this);
		}

		/// <summary>
		/// Occurs when live updating has been kicked off by the <c>ACTileController</c> 
		/// </summary>
		public delegate void ACTileControllerLiveUpdatingDelegate(ACTileController controller);
		public event ACTileControllerLiveUpdatingDelegate LiveUpdating;

		/// <summary>
		/// Raises the live updating.
		/// </summary>
		internal void RaiseLiveUpdating()
		{
			if (this.LiveUpdating != null)
				this.LiveUpdating(this);
		}

		/// <summary>
		/// Occurs when a tile is touched in this group.
		/// </summary>
		public event ACTileGroup.ACTileGroupTileTouchedDelegate TileTouched;

		/// <summary>
		/// Raises the tile touched event.
		/// </summary>
		/// <param name="tile">Tile.</param>
		internal void RaiseTileTouched(ACTileGroup group, ACTile tile)
		{
			if (this.TileTouched != null)
			{
				this.TileTouched(group, tile);
			}
		}
		#endregion
	}
}
