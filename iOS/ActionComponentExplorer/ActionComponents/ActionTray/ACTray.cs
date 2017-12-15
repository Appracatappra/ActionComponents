using System;
using System.Drawing;
using System.IO;
using System.ComponentModel;

using Foundation;
using UIKit;
using CoreGraphics;
using ObjCRuntime;

namespace ActionComponents
{
	/// <summary>
	/// The <see cref="ActionComponents.ACTray"/> is a dockable, customizable, slide-out <see cref="MonoTouch.UIKit.UIView"/> controller
	/// with a <c>dragTab</c> that can be attached to the edge of any parent <see cref="MonoTouch.UIKit.UIView"/>. The <see cref="ActionComponents.ACTray"/> handles
	/// sliding the attached <see cref="MonoTouch.UIKit.UIView"/> based on the <see cref="ActionComponents.ACTrayType"/>. The <see cref="ActionComponents.ACTray"/> 
	/// can either be created in a .xib file in Xcode or manually in C# code.
	/// </summary>
	/// <description>The <see cref="ActionComponents.ACTray"/> can be styled by setting its <see cref="ActionComponents.ACTrayAppearance"/>
	/// and <see cref="ActionComponents.ACTrayTabType"/> properties. You can also control the position of the <c>dragTab</c> by setting the tray's
	/// <see cref="ActionComponents.ACTrayTabLocation"/>, <c>tabOffset</c> and <c>tabWidth</c> properties.</description>
	/// <remarks>WARNING! You MUST manually set the <see cref="ActionComponents.ACTray"/>'s <see cref="ActionComponents.ACTrayOrientation"/> 
	/// when the <see cref="MonoTouch.UIKit.UIView"/> first loads or the tray will not display or behave correctly.</remarks>
	[Register("ACTray")]
	public class ACTray : UIView
	{
		#region Private Variables
		private ACTrayOrientation _orientation;
		private ACTrayTabLocation _tabLocation;
		private ACTrayTabType _tabType;
		private ACTrayFrameType _frameType;
		private NSObject _orientationObserver;
		private nfloat _tabOffset=0f;
		private nfloat _tabWidth = 100f;
		private UIImage _icon;
		private string _title="";
		private nfloat _openedPosition=100f;
		private nfloat _closedPosition = 0;
		private UITapGestureRecognizer _doubleTap; 
		private CGPoint _startLocation;
		private bool _dragging;
		private CGRect _contentArea;
		private CGRect _thumbHotspot;
		private bool _hideBodyShadow=false;
		private bool _isOpened=true;
		private bool _animating=false;
		#endregion

		#region Public Properties
		/// <summary>
		/// [OPTIONAL] Tag to hold user information about this control
		/// </summary>
		public object tag;

		/// <summary>
		/// If <c>true</c> the <see cref="ActionComponents.ACTray"/> becomes the top most view
		/// when it is touched by the user
		/// </summary>
		public bool bringToFrontOnTouch=false;
		#endregion 

		#region Computed Properties
		/// <summary>
		/// Gets a value indicating whether this <see cref="ActionComponents.ACTray"/> is closed.
		/// </summary>
		/// <value><c>true</c> if is closed; otherwise, <c>false</c>.</value>
		public bool isClosed {
			get{
				//If draggable calculate state
				if (trayType == ACTrayType.Draggable) {
					//Take action based on the tray's orientation
					switch (_orientation) {
					case ACTrayOrientation.Left:
					case ACTrayOrientation.Right:
						return (Frame.X == _closedPosition);
					case ACTrayOrientation.Bottom:
					case ACTrayOrientation.Top:
						return (Frame.Y == _closedPosition);
					}
				}

				//Default
				return !_isOpened;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="ActionComponents.ACTray"/> is opened.
		/// </summary>
		/// <value><c>true</c> if is opened; otherwise, <c>false</c>.</value>
		public bool isOpened{
			get{
				//If draggable calculate state
				if (trayType == ACTrayType.Draggable) {
					//Take action based on the tray's orientation
					switch (_orientation) {
					case ACTrayOrientation.Left:
					case ACTrayOrientation.Right:
						return (Frame.X == _openedPosition);
					case ACTrayOrientation.Bottom:
					case ACTrayOrientation.Top:
						return (Frame.Y == _openedPosition);
					}
				}
				
				//Default
				return _isOpened;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="ActionComponents.ACTray"/> is dragging.
		/// </summary>
		/// <value><c>true</c> if dragging; otherwise, <c>false</c>.</value>
		public bool dragging{
			get{ return _dragging;}
		}

		/// <summary>
		/// Gets or sets the orientation of this <see cref="ActionComponents.ACTray"/> on the screen
		/// </summary>
		/// <value>The tray's orientation.</value>
		/// <remarks>This property controls how the <see cref="ActionComponents.ACTray"/> responds to user
		/// interaction and where the <c>dragTab</c> is located</remarks>
		[Export("orientation"), Browsable(true), DisplayName("Tray Orientation")]
		public ACTrayOrientation orientation{
			get{ return _orientation;}
			set{
				//Save value
				_orientation=value;

				//Automatically set the open and closed positions based on my location
				CalculateOpenAndClosedPositions ();

				//Force a redraw
				this.SetNeedsDisplay ();
			}
		}

		/// <summary>
		/// Gets the current amount of the <see cref="ActionComponents.ACTray"/>'s <c>ContentArea</c> that is 
		/// currently visible on screen
		/// </summary>
		/// <value>The amount of the <c>ContentArea</c> that is currently visible</value>
		/// <remarks>NOTE: This amount excludes the always visible <c>DragTab</c> </remarks>
		public nfloat amountVisible{
			get {
				//Take action based on the orientation of the ActionTray
				switch(_orientation){
				case ACTrayOrientation.Top:
					return (Frame.Height-34f)+Frame.Top;
				case ACTrayOrientation.Bottom:
					return _closedPosition-Frame.Top;
				case ACTrayOrientation.Left:
					return (Frame.Width-34f)+Frame.Left;
				case ACTrayOrientation.Right:
					return _closedPosition-Frame.Left;
				}

				//Default
				return 0;
			}
		}

		/// <summary>
		/// Gets or sets the location of the <c>dragTab></c> on this <see cref="ActionComponents.ACTray"/> 
		/// </summary>
		/// <value>The tab location.</value>
		/// <remarks>This property interacts with the <see cref="ActionComponents.ACTrayOrientation"/> property</remarks>
		[Export("tabLocation"), Browsable(true), DisplayName("Tray Tab Location")]
		public ACTrayTabLocation tabLocation{
			get{ return _tabLocation;}
			set{
				//Save location
				_tabLocation=value;

				//Force a redraw
				this.SetNeedsDisplay ();
			}
		}

		/// <summary>
		/// Gets or sets the type of the <c>dragTab</c> drawn on this <see cref="ActionComponents.ACTray"/> 
		/// </summary>
		/// <value>The type of the tab.</value>
		/// <remarks>The <see cref="ActionComponents.ACTrayAppearance"/> also controls the appearance of the <c>dragTab</c> </remarks>
		[Export("tabType"), Browsable(true), DisplayName("Tray Tab Type")]
		public ACTrayTabType tabType{
			get{ return _tabType;}
			set{
				//Save type
				_tabType=value;

				//Force a redraw
				this.SetNeedsDisplay ();
			}
		}

		/// <summary>
		/// Gets or sets the type of the frame drawn around the edge of this <see cref="ActionComponents.ACTray"/> 
		/// </summary>
		/// <value>The type of the frame.</value>
		/// <remarks>This property reacts with the <see cref="ActionComponents.ACTrayOrientation"/> property to
		/// control the appearance and the location of the frame</remarks>
		[Export("frameType"), Browsable(true), DisplayName("Tray Frame Type")]
		public ACTrayFrameType frameType{
			get{ return _frameType;}
			set{
				//Save value
				_frameType=value;

				//Force a redraw
				this.SetNeedsDisplay ();
			}
		}

		/// <summary>
		/// Gets or sets the tab offset for a <c>Custom</c> <see cref="ActionComponents.ACTrayTabLocation"/> 
		/// </summary>
		/// <value>The tab offset.</value>
		/// <remarks>Based on the <see cref="ActionComponents.ACTray"/>'s <see cref="ActionComponents.ACTrayOrientation"/>, the offset will
		/// either be from the tray's top or left side</remarks>
		[Export("tabOffset"), Browsable(true), DisplayName("Tray Tab Offset")]
		public nfloat tabOffset{
			get{ return _tabOffset;}
			set{
				//Save value
				_tabOffset=value;

				//Force a redraw
				this.SetNeedsDisplay ();
			}
		}

		/// <summary>
		/// Gets or sets the width of the <c>dragTab</c> for this <see cref="ActionComponents.ACTray"/> 
		/// </summary>
		/// <value>The width of the tab.</value>
		/// <remarks>The minimum width is 30 pixels.</remarks>
		[Export("tabWidth"), Browsable(true), DisplayName("Tray Tab Width")]
		public nfloat tabWidth{
			get{ return _tabWidth;}
			set{
				//Save value
				_tabWidth =value;

				//Validate
				if (_tabWidth<30f) _tabWidth=30f;

				//Force a redraw
				this.SetNeedsDisplay ();
			}
		}

		/// <summary>
		/// Gets or sets the icon displayed on the <c>dragTab</c> of this <see cref="ActionComponents.ACTray"/> 
		/// </summary>
		/// <value>The icon.</value>
		/// <remarks>The icon will be displayed based on the <c>dragTab</c>'s <see cref="ActionComponents.ACTrayTabType"/>  </remarks>
		[Export("icon"), Browsable(true), DisplayName("Tray Tab Icon")]
		public UIImage icon {
			get{ return _icon;}
			set{
				//Save value
				_icon=value;

				//Force a redraw
				this.SetNeedsDisplay ();
			}
		}

		/// <summary>
		/// Gets or sets the title for the <see cref="ActionComponents.ACTray"/>'s <c>dragTab</c> 
		/// </summary>
		/// <value>The title.</value>
		/// <remarks>This title will be displayed based on the <c>dragTab</c>'s <see cref="ActionComponents.ACTrayTabType"/>  </remarks>
		[Export("title"), Browsable(true), DisplayName("Tray Tab Title")]
		public string title{
			get{ return _title;}
			set{
				//Save value
				_title=value;

				//Force a redraw
				this.SetNeedsDisplay ();
			}
		}

		/// <summary>
		/// Controlls the general appearance of the control
		/// </summary>
		public ACTrayAppearance appearance { get; set;}

		/// <summary>
		/// Gets or sets the <see cref="ActionComponents.ACTrayType"/> of this <see cref="ActionComponents.ACTray"/> 
		/// </summary>
		/// <value>The type of the tray.</value>
		[Export("trayType"), Browsable(true), DisplayName("Tray Type")]
		public ACTrayType trayType { get; set; }
		#endregion 

		#region Appearance Properties
		/// <summary>
		/// Gets or sets the background color
		/// </summary>
		/// <value>The background color</value>
		[Export("background"), Browsable(true), DisplayName("Background Color")]
		public UIColor background {
			get{ return appearance.background;}
			set{
				appearance.background = value;
			}
		}

		/// <summary>
		/// Gets or sets the border color
		/// </summary>
		/// <value>The border color</value>
		[Export("border"), Browsable(true), DisplayName("Border Color")]
		public UIColor border{
			get { return appearance.border;}
			set {
				appearance.border=value;
			}
		}

		/// <summary>
		/// Gets or sets the shadow color
		/// </summary>
		/// <value>The shadow color</value>
		[Export("shadow"), Browsable(true), DisplayName("Shadow Color")]
		public UIColor shadow{
			get{ return appearance.shadow;}
			set{
				appearance.shadow=value;
			}
		}

		/// <summary>
		/// Gets or sets the frame color
		/// </summary>
		/// <value>The frame color</value>
		[Export("frameColor"), Browsable(true), DisplayName("Frame Color")]
		public UIColor frameColor{
			get{ return appearance.frame;}
			set{
				appearance.frame=value;
			}
		}

		/// <summary>
		/// Gets or sets the thumb background.
		/// </summary>
		/// <value>The thumb background.</value>
		[Export("thumbBackground"), Browsable(true), DisplayName("Thumb Background Color")]
		public UIColor thumbBackground {
			get{ return appearance.thumbBackground;}
			set{
				appearance.thumbBackground=value;
			}
		}

		/// <summary>
		/// Gets or sets the thumb border.
		/// </summary>
		/// <value>The thumb border.</value>
		[Export("thumbBorder"), Browsable(true), DisplayName("Thumb Border Color")]
		public UIColor thumbBorder{
			get{ return appearance.thumbBorder;}
			set{
				appearance.thumbBorder=value;
			}
		}

		/// <summary>
		/// Gets or sets the thumb aplha.
		/// </summary>
		/// <value>The thumb aplha.</value>
		[Export("tabAlpha"), Browsable(true), DisplayName("Tab Alpha Transparency")]
		public nfloat tabAlpha{
			get{return appearance.tabAlpha;}
			set{
				//Save value
				appearance.tabAlpha=value;
			}
		}

		/// <summary>
		/// Gets or sets the grip background.
		/// </summary>
		/// <value>The grip background.</value>
		[Export("gripBackground"), Browsable(true), DisplayName("Grip Background Color")]
		public UIColor gripBackground{
			get{ return appearance.gripBackground;}
			set{
				appearance.gripBackground=value;
			}
		}

		/// <summary>
		/// Gets or sets the grip border.
		/// </summary>
		/// <value>The grip border.</value>
		[Export("gripBorder"), Browsable(true), DisplayName("Grip Border Color")]
		public UIColor gripBorder{
			get{ return appearance.gripBorder;}
			set{
				appearance.gripBorder=value;
			}
		}

		/// <summary>
		/// Gets or sets the thumb blend mode.
		/// </summary>
		/// <value>The thumb blend mode.</value>
		[Export("thumbBlendMode"), Browsable(true), DisplayName("Thumb Blend Mode")]
		public CGBlendMode thumbBlendMode{
			get{ return appearance.thumbBlendMode;}
			set{
				appearance.thumbBlendMode=value;
			}
		}

		/// <summary>
		/// Gets or sets the color of the title.
		/// </summary>
		/// <value>The color of the title.</value>
		[Export("titleColor"), Browsable(true), DisplayName("Title Color")]
		public UIColor titleColor{
			get{ return appearance.titleColor;}
			set{
				appearance.titleColor=value;
			}
		}

		/// <summary>
		/// Gets or sets the size of the title.
		/// </summary>
		/// <value>The size of the title.</value>
		[Export("titleSize"), Browsable(true), DisplayName("Title Font Size")]
		public nfloat titleSize{
			get{ return appearance.titleSize;}
			set{
				appearance.titleSize=value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <see cref="ActionComponents.ACTrayAppearance"/> is flat.
		/// </summary>
		/// <value><c>true</c> if flat; otherwise, <c>false</c>.</value>
		/// <remarks>This value was added to support the iOS 7 degisn language</remarks>
		[Export("flat"), Browsable(true), DisplayName("iOS 7 Flat Appearance")]
		public bool flat{
			get{ return appearance.flat;}
			set{
				appearance.flat = value;
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACTray"/> class.
		/// </summary>
		public ACTray() : base() {
			//Add an appearance handler to this object
			this.appearance = new ACTrayAppearance ();
			
			//Complete initialization
			this.Initialize ();
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACTray"/> class.
		/// </summary>
		/// <param name="coder">Coder.</param>
		public ACTray(NSCoder coder): base(coder){
			//Add an appearance handler to this object
			this.appearance = new ACTrayAppearance ();
			
			//Complete initialization
			this.Initialize ();
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACTray"/> class.
		/// </summary>
		/// <param name="flag">Flag.</param>
		public ACTray(NSObjectFlag flag): base(flag){
			//Add an appearance handler to this object
			this.appearance = new ACTrayAppearance ();
			
			//Complete initialization
			this.Initialize ();
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACTray"/> class.
		/// </summary>
		/// <param name="bounds">Bounds.</param>
		public ACTray(CGRect bounds): base(bounds){
			//Add an appearance handler to this object
			this.appearance = new ACTrayAppearance ();
			
			//Complete initialization
			this.Initialize ();
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACTray"/> class.
		/// </summary>
		/// <param name="ptr">Ptr.</param>
		public ACTray(IntPtr ptr): base(ptr){
			//Add an appearance handler to this object
			this.appearance = new ACTrayAppearance ();
			
			//Complete initialization
			this.Initialize ();
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACTray"/> class
		/// with the user provided appearance
		/// </summary>
		/// <param name="appearance">The user specified appearance for the control</param>
		public ACTray(ACTrayAppearance appearance){
			
			//Save the user defined appearance
			this.appearance = appearance;
			
			//Complete initialization
			this.Initialize ();
		}
		
		/// <summary>
		/// Initialize this instance.
		/// </summary>
		private void Initialize(){
			
			//Clear background
			this.BackgroundColor = UIColor.Clear;

			//Automatically enable touch events
			this.UserInteractionEnabled=true;
			this.MultipleTouchEnabled=true;
			this.ExclusiveTouch = true;

			//Set defaults
			this._orientation = ACTrayOrientation.Left;
			this._tabType = ACTrayTabType.GripOnly;
			this._tabLocation = ACTrayTabLocation.Middle;
			this._frameType = ACTrayFrameType.None;
			this.trayType = ACTrayType.Draggable;
			this.ClipsToBounds = true;
			
			//Handle appearance changes
			this.appearance.AppearanceModified += delegate() {
				//Force a redraw
				this.SetNeedsDisplay ();
			};

			//Add an observer for when the splitter changes orientation
			_orientationObserver = NSNotificationCenter.DefaultCenter.AddObserver (UIApplication.DidChangeStatusBarOrientationNotification, notification => {
				//Ensure the tray is positioned correctly inside it's parent view
				RepositionInParentView();

				//Force a redraw on orientation change
				this.SetNeedsDisplay ();
			});

			//Add double tap recognizer
			_doubleTap = new UITapGestureRecognizer ();
			_doubleTap.NumberOfTapsRequired = 2;
			_doubleTap.AddTarget (this, new Selector ("OnDoubleTap:"));
			this.AddGestureRecognizer (_doubleTap);

		}

		/// <summary>
		/// Handles the <see cref="ActionComponents.ACTray"/> being double tapped
		/// </summary>
		/// <param name="sender">Sender.</param>
		[Export("OnDoubleTap:")]
		public void OnDoubleTap (UIGestureRecognizer sender) {

			//Take action based on the tray type
			switch (trayType) {
			case ACTrayType.Draggable:
				//Is this a valid touch?
				if (!_thumbHotspot.Contains(sender.LocationInView (this))) return;

				//Is the tray closed?
				if (isClosed) {
					//Yes, open tray
					OpenTray (true);
				} else {
					//No, close tray
					CloseTray (true);
				}

				//Inform caller that we have been touched and moved
				RaiseTouched();
				break;
			}

		}
		#endregion 

		#region Public Methods
		/// <summary>
		/// WARNING Experimental Feature! Adds a <c>UIVisualEffect</c> to the given tray with a couple of limitations. It can only be called after the tray
		/// has been displayed and is only effective for trays that don't change size. The thumb style will be switched to
		/// square to accomodate the blur effect and right now the whole thing looks a bit clunky. Use at own risk... mileage may vary.
		/// </summary>
		/// <param name="style">Style.</param>
		public void AddBlur(UIBlurEffectStyle style, bool blurThumb) {

			// Switch the tray background to clear
			appearance.background = UIColor.Clear;

			var blur = UIBlurEffect.FromStyle (style);
			var blurView = new UIVisualEffectView (blur) {
				Frame = _contentArea
			};
			Add (blurView);

			// Bluring thumb as well?
			if (blurThumb) {
				appearance.thumbBackground = UIColor.Clear;
				var blurThumbView = new UIVisualEffectView (blur) {
					Frame = _thumbHotspot
				};
				Add (blurThumbView);
			}
		}

		/// <summary>
		/// Opens the <see cref="ActionComponents.ACTray"/> 
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public void OpenTray(bool animated){
			CGRect location = new CGRect();

			//Recalculate open and closing to be safe
			CalculateOpenAndClosedPositions ();

			//Is the tray already open?
			if (isOpened)
				return;

			//Calculate location based on the tray's orientation
			switch (_orientation) {
			case ACTrayOrientation.Left:
			case ACTrayOrientation.Right:
				location=new CGRect(_openedPosition,Frame.Y,Frame.Width,Frame.Height);
				break;
			case ACTrayOrientation.Bottom:
			case ACTrayOrientation.Top:
				location=new CGRect(Frame.X,_openedPosition,Frame.Width,Frame.Height);
				break;
			}

			//Automatically bring view to front?
			if (bringToFrontOnTouch) this.Superview.BringSubviewToFront(this);

			//Animate opening the tray
			if (animated) {
				//Define Animation
				UIView.BeginAnimations("MoveTray");
				UIView.SetAnimationDuration(0.5f);

				//Disable interaction during animation
				_animating=true;

				//Set end of Animation handler
				UIView.SetAnimationDelegate(this);
				UIView.SetAnimationDidStopSelector(new Selector("AnimationCompleted"));

				Frame=location;
				
				//Execute Animation
				UIView.CommitAnimations();

				#if TRIAL 
				ACToast.MakeText("ACTray by Appracatappra, LLC.",ACToastLength.Medium).Show();
				#endif
			} else {
				Frame=location;

				//Inform caller
				RaiseMoved ();
				RaiseOpened ();
			}

			//Save state
			_isOpened = true;

		}

		/// <summary>
		/// Handles the finalization of an opened or closed animation
		/// </summary>
		[Export("AnimationCompleted")]
		public virtual void AnimationCompleted(){

			//Take action based on the state
			if (isOpened) {
				//Inform caller
				RaiseMoved ();
				RaiseOpened();
			} 

			//Re-enable touching
			_animating=false;
		}

		/// <summary>
		/// Closes the <see cref="ActionComponents.ACTray"/> 
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public void CloseTray(bool animated){
			CGRect location = new CGRect();

			//Recalculate open and closing to be safe
			CalculateOpenAndClosedPositions ();

			//Is the tray already closed?
			if (isClosed)
				return;

			//Calculate location based on the tray's orientation
			switch (_orientation) {
			case ACTrayOrientation.Left:
			case ACTrayOrientation.Right:
				location=new CGRect(_closedPosition,Frame.Y,Frame.Width,Frame.Height);
				break;
			case ACTrayOrientation.Bottom:
			case ACTrayOrientation.Top:
				location=new CGRect(Frame.X,_closedPosition,Frame.Width,Frame.Height);
				break;
			}

			//Animate opening the tray
			if (animated) {
				//Define Animation
				UIView.BeginAnimations("MoveTray");
				UIView.SetAnimationDuration(0.5f);

				//Disable interaction during animation
				_animating=true;

				//Set end of Animation handler
				UIView.SetAnimationDelegate(this);
				UIView.SetAnimationDidStopSelector(new Selector("AnimationCompleted"));
				
				Frame=location;
				
				//Execute Animation
				UIView.CommitAnimations();

				#if TRIAL 
				ACToast.MakeText("ACTray by Appracatappra, LLC.",ACToastLength.Medium).Show();
				#endif
			} else {
				Frame=location;
			}

			//Inform caller
			RaiseMoved ();
			RaiseClosed ();

			//Save state
			_isOpened = false;

		}

		/// <summary>
		/// Moves this <see cref="ActionComponents.ACTray"/> to the given location 
		/// </summary>
		/// <param name="point">Point.</param>
		public void MoveTo(CGPoint point){

			// Move entire tray to the given location
			Frame = new CGRect (point.X, point.Y, Frame.Width, Frame.Height);
		}
		#endregion 

		#region Private Methods
		[Export("RepositionInParentView")]
		private void RepositionInParentView(){
			bool wasOpen = isOpened;
			CGRect location = new CGRect();

			//Recalculate Open and Closed positions
			CalculateOpenAndClosedPositions ();

			//Ensure the tray remains stuck to the correct location after a rotation
			//has occurred
			if (wasOpen) {
				//Calculate location based on the tray's orientation
				switch (_orientation) {
				case ACTrayOrientation.Left:
				case ACTrayOrientation.Right:
					location=new CGRect(_openedPosition,Frame.Y,Frame.Width,Frame.Height);
					break;
				case ACTrayOrientation.Bottom:
				case ACTrayOrientation.Top:
					location=new CGRect(Frame.X,_openedPosition,Frame.Width,Frame.Height);
					break;
				}
			} else {
				//Calculate location based on the tray's orientation
				switch (_orientation) {
				case ACTrayOrientation.Left:
				case ACTrayOrientation.Right:
					location=new CGRect(_closedPosition,Frame.Y,Frame.Width,Frame.Height);
					break;
				case ACTrayOrientation.Bottom:
				case ACTrayOrientation.Top:
					location=new CGRect(Frame.X,_closedPosition,Frame.Width,Frame.Height);
					break;
				}
			}

			//Move to new location
			Frame=location;
		}

		/// <summary>
		/// Calculates the width based on the current device orientation
		/// </summary>
		/// <returns>The width.</returns>
		/// <param name="view">View.</param>
		private nfloat CalculateWidth(UIView view){
			nfloat width = 0f;

			//Is this on a superview?
			if (view == null) {
				//Yes, use master screen bounds
				width = UIScreen.MainScreen.Bounds.GetMaxX ();
			} else {
				//No, get the width of the parent view
				width = view.Bounds.GetMaxX ();
			}

			//Return result
			return width;
		}

		/// <summary>
		/// CCalculates the height based on the current device orientation
		/// </summary>
		/// <returns>The height.</returns>
		/// <param name="view">View.</param>
		private nfloat CalculateHeight(UIView view){
			nfloat height = 0f;

			//Is this on a superview?
			if (view == null) {
				//Yes, use master screen bounds
				height = UIScreen.MainScreen.Bounds.GetMaxY ();
			} else {
				height = view.Bounds.GetMaxY ();
			}

			//Return result
			return height;
		}

		/// <summary>
		/// Calculates the open and closed positions for the <see cref="ActionComponents.ACTray"/> 
		/// </summary>
		/// <remarks>This method is called when the <see cref="ActionComponents.ACTrayOrientation"/> changes and when
		/// the device is rotated.</remarks>
		private void CalculateOpenAndClosedPositions(){

			//Take action based on the current orientation
			switch(_orientation){
			case ACTrayOrientation.Left:
				_openedPosition=0f;
				_closedPosition=-(Frame.Width-34f);

				//Does the tray span the full height?
				_hideBodyShadow=(Frame.Height!=CalculateHeight (Superview));
				break;
			case ACTrayOrientation.Right:
				_openedPosition=CalculateWidth (Superview)-Frame.Width;
				_closedPosition=CalculateWidth (Superview)-34f;

				//Does the tray span the full height?
				_hideBodyShadow=(Frame.Height!=CalculateHeight (Superview));

				//Console.WriteLine(String.Format("Orientation: {0} Super: {1}",UIApplication.SharedApplication.StatusBarOrientation,Superview.Frame));
				break;
			case ACTrayOrientation.Top:
				_openedPosition=0f;
				_closedPosition=-(Frame.Height-34f);

				//Does the tray span the full width?
				_hideBodyShadow=(Frame.Width!=CalculateWidth (Superview));
				break;
			case ACTrayOrientation.Bottom:
				_openedPosition=CalculateHeight (Superview)-Frame.Height;
				_closedPosition=CalculateHeight (Superview)-34f;

				//Does the tray span the full width?
				_hideBodyShadow=(Frame.Width!=CalculateWidth (Superview));
				break;
			}
		}

		/// <summary>
		/// Moves the tray to the given location
		/// </summary>
		/// <param name="pt">Point.</param>
		private void MoveTray(CGPoint pt) {

			//Grab frame
			var frame = this.Frame;
			
			//Take action based on the orientation
			switch (_orientation) {
			case ACTrayOrientation.Left:
				//Adjust frame location
				frame.X += pt.X - _startLocation.X;

				//Validate
				if (frame.X<_closedPosition) frame.X=_closedPosition;
				if (frame.X>_openedPosition) frame.X=_openedPosition;
				break;
			case ACTrayOrientation.Right:
				//Adjust frame location
				frame.X += pt.X - _startLocation.X;
				
				//Validate
				if (frame.X<_openedPosition) frame.X=_openedPosition;
				if (frame.X>_closedPosition) frame.X=_closedPosition;
				break;
			case ACTrayOrientation.Top:
				//Adjust frame location
				frame.Y+=pt.Y-_startLocation.Y;

				//Validate
				if (frame.Y<_closedPosition) frame.Y=_closedPosition;
				if (frame.Y>_openedPosition) frame.Y=_openedPosition;
				break;
			case ACTrayOrientation.Bottom:
				//Adjust frame location
				frame.Y+=pt.Y-_startLocation.Y;

				//Validate
				if (frame.Y<_openedPosition) frame.Y=_openedPosition;
				if (frame.Y>_closedPosition) frame.Y=_closedPosition;
				break;
			}

			//Apply new location
			this.Frame = frame;

			//Inform caller
			RaiseMoved ();

		}

		/// <summary>
		/// Calculates the <c>dragTab</c> position.
		/// </summary>
		/// <returns>The tab position.</returns>
		/// <param name="rect">Rect.</param>
		private nfloat CalculateTabposition(CGRect rect){
			nfloat position = 0f;

			//Take action based on the user specified tab position
			switch (_tabLocation) {
			case ACTrayTabLocation.TopOrLeft:
				position=5f;
				break;
			case ACTrayTabLocation.Middle:
				//Take action based on orientation
				switch(_orientation){
				case ACTrayOrientation.Left:
				case ACTrayOrientation.Right:
					position=(rect.Height/2f)-(_tabWidth/2f);
					break;
				case ACTrayOrientation.Bottom:
				case ACTrayOrientation.Top:
					position=(rect.Width/2f)-(_tabWidth/2f);
					break;
				}
				break;
			case ACTrayTabLocation.BottomOrRight:
				//Take action based on orientation
				switch(_orientation){
				case ACTrayOrientation.Left:
				case ACTrayOrientation.Right:
					position=rect.Height-(_tabWidth+10f);
					break;
				case ACTrayOrientation.Bottom:
				case ACTrayOrientation.Top:
					position=rect.Width-(_tabWidth+10f);
					break;
				}
				break;
			case ACTrayTabLocation.Custom:
				position=_tabOffset;
				break;
			}

			//Return value
			return position;
		}

		/// <summary>
		/// Draws the marker.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		private void DrawMarker(float x, float y)
		{
			nfloat SZ = 20;
			
			CGContext c = UIGraphics.GetCurrentContext();
			
			c.BeginPath();
			c.AddLines( new [] { new CGPoint(x-SZ,y), new CGPoint(x+SZ,y) });
			c.AddLines( new [] { new CGPoint(x,y-SZ), new CGPoint(x,y+SZ) });
			c.StrokePath();
		}

		/// <summary>
		/// Degreeses to radians.
		/// </summary>
		/// <returns>The to radians.</returns>
		/// <param name="x">The x coordinate.</param>
		static private nfloat DegreesToRadians(nfloat x) 
		{       
			return (nfloat) (Math.PI * x / 180.0);
		}

		/// <summary>
		/// Draws the text rotated.
		/// </summary>
		/// <param name="text">Text.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="rotDegree">Rot degree.</param>
		private void DrawTextRotated(string text, nfloat x, nfloat y, nfloat rotDegree)
		{
			CGContext c = UIGraphics.GetCurrentContext();
			c.SaveState();

			//Used to help position the text during debug only
			//DrawMarker(x,y);
			
			// Proper rotation about a point
			var m = CGAffineTransform.MakeTranslation(-x,-y);
			m.Multiply( CGAffineTransform.MakeRotation(DegreesToRadians(rotDegree)));
			m.Multiply( CGAffineTransform.MakeTranslation(x,y));
			c.ConcatCTM( m );

			appearance.titleColor.SetFill ();
			c.SetTextDrawingMode(CGTextDrawingMode.Fill);
			c.SetShouldSmoothFonts(true);
			
			// Draws text UNDER the point
			// "This point represents the top-left corner of the string’s bounding box."
			//http://developer.apple.com/library/ios/#documentation/UIKit/Reference/NSString_UIKit_Additions/Reference/Reference.html
			NSString ns = new NSString(text);
			UIFont font = UIFont.SystemFontOfSize(appearance.titleSize);
			CGSize sz = ns.StringSize(font);
			CGRect rect = new CGRect(x,y,sz.Width,sz.Height);
			ns.DrawString( rect, font);
			
			c.RestoreState();
		}

		/// <summary>
		/// Draws the left tray.
		/// </summary>
		/// <param name="rect">Rect.</param>
		private void DrawLeftTray(CGRect rect){
			UIBezierPath rightEdgePath;
			UIBezierPath topEdgePath;
			UIBezierPath bottomEdgePath;
			UIBezierPath leftEdgePath;
			UIBezierPath thumbPath;
			UIBezierPath grip1Path;
			UIBezierPath grip2Path;
			UIBezierPath grip3Path;
			UIBezierPath tabIconPath;
			CGRect tabIconRect;
			
			nfloat y = 0f;
			
			//// General Declarations
			var context = UIGraphics.GetCurrentContext();
			
			//// Shadow Declarations
			var bodyShadow = appearance.shadow.CGColor;
			var bodyShadowOffset = new CGSize(3.1f, 0f);
			var bodyShadowBlurRadius = 5;
			
			//// Frames
			var tabAt = new CGRect(rect.Width-38f, CalculateTabposition (rect), 36, _tabWidth+10f);

			// Save thumb "hotspot" for testing later
			_thumbHotspot = tabAt;
			
			//// Body Drawing
			_contentArea = new CGRect (0, 0, rect.Width - 35f, rect.Height);
			var bodyPath = UIBezierPath.FromRect(_contentArea);
			context.SaveState();
			if (!appearance.flat) {
				if (_hideBodyShadow) {
					if (!isClosed) 
						context.SetShadow(bodyShadowOffset, bodyShadowBlurRadius, bodyShadow);
				} else {
					context.SetShadow(bodyShadowOffset, bodyShadowBlurRadius, bodyShadow);
				}
			}
			appearance.background.SetFill();
			bodyPath.Fill();
			context.RestoreState();
			
			appearance.border.SetStroke();
			bodyPath.LineWidth = 1;
			bodyPath.Stroke();

			//Mix up tab background
			UIColor tabColor;

			if (_frameType==ACTrayFrameType.None) {
				tabColor=appearance.background.ColorWithAlpha (appearance.tabAlpha);
			} else {
				tabColor=appearance.frame.ColorWithAlpha (appearance.tabAlpha);
			}

			//Is this Tab custom drawn?
			if (_tabType==ACTrayTabType.CustomDrawn) {
				//Yes, raise the custom drawn event
				RaiseCustomDrawDragTab(tabAt);
			} else {
				//// Tab Drawing
				UIBezierPath tabPath = new UIBezierPath();
				tabPath.MoveTo(new CGPoint(tabAt.GetMinX() + 1.5f, tabAt.GetMinY() + 0.95000f * tabAt.Height));
				tabPath.AddLineTo(new CGPoint(tabAt.GetMinX() + 31.5f, tabAt.GetMinY() + 0.90455f * tabAt.Height));
				tabPath.AddLineTo(new CGPoint(tabAt.GetMinX() + 31.5f, tabAt.GetMinY() + 0.08636f * tabAt.Height));
				tabPath.AddLineTo(new CGPoint(tabAt.GetMinX() + 1.5f, tabAt.GetMinY() + 0.04091f * tabAt.Height));
				tabPath.AddLineTo(new CGPoint(tabAt.GetMinX() + 1.5f, tabAt.GetMinY() + 0.95000f * tabAt.Height));
				tabPath.ClosePath();
				context.SaveState();
				if (!appearance.flat) {
					context.SetShadow (bodyShadowOffset, bodyShadowBlurRadius, bodyShadow);
				}
				tabColor.SetFill();
				tabPath.Fill();
				context.RestoreState();
			}
			
			//Take action based on the tab type
			switch (_tabType) {
			case ACTrayTabType.Plain:
				//Do nothing
				break;
			case ACTrayTabType.GripOnly:
				//// Drag Thumb
				{
					context.SaveState();
					if (!appearance.flat) context.SetBlendMode(CGBlendMode.Multiply);
					context.BeginTransparencyLayer();
					
					//Calculate position
					y=tabAt.GetMinY()+((_tabWidth/2f)-9f);
					
					//// Thumb Drawing
					thumbPath = UIBezierPath.FromRect(new CGRect(tabAt.GetMinX() + 8.5f, y, 17, 27));
					appearance.thumbBackground.SetFill();
					thumbPath.Fill();
					appearance.thumbBorder.SetStroke();
					thumbPath.LineWidth = 1;
					thumbPath.Stroke();
					
					
					//// Grip1 Drawing
					grip1Path = UIBezierPath.FromRect(new CGRect(tabAt.GetMinX() + 11.5f, y+4f, 2, 20));
					appearance.gripBackground.SetFill();
					grip1Path.Fill();
					appearance.gripBorder.SetStroke();
					grip1Path.LineWidth = 1;
					grip1Path.Stroke();
					
					
					//// Grip2 Drawing
					grip2Path = UIBezierPath.FromRect(new CGRect(tabAt.GetMinX() + 15.5f, y+4f, 2, 20));
					appearance.gripBackground.SetFill();
					grip2Path.Fill();
					appearance.gripBorder.SetStroke();
					grip2Path.LineWidth = 1;
					grip2Path.Stroke();
					
					
					//// Grip3 Drawing
					grip3Path = UIBezierPath.FromRect(new CGRect(tabAt.GetMinX() + 19.5f, y+4f, 2, 20));
					appearance.gripBackground.SetFill();
					grip3Path.Fill();
					appearance.gripBorder.SetStroke();
					grip3Path.LineWidth = 1;
					grip3Path.Stroke();
					
					
					context.EndTransparencyLayer();
					context.RestoreState();
				}
				break;
			case ACTrayTabType.GripAndTitle:
				//// Drag Thumb
				{
					context.SaveState();
					if (!appearance.flat) context.SetBlendMode(CGBlendMode.Multiply);
					context.BeginTransparencyLayer();
					
					//Calculate position
					y=tabAt.GetMinY()+12f+((_tabWidth/2f)*0.1f);
					
					//// Thumb Drawing
					thumbPath = UIBezierPath.FromRect(new CGRect(tabAt.GetMinX() + 8.5f, y, 17, 27));
					appearance.thumbBackground.SetFill();
					thumbPath.Fill();
					appearance.thumbBorder.SetStroke();
					thumbPath.LineWidth = 1;
					thumbPath.Stroke();
					
					
					//// Grip1 Drawing
					grip1Path = UIBezierPath.FromRect(new CGRect(tabAt.GetMinX() + 11.5f, y+4f, 2, 20));
					appearance.gripBackground.SetFill();
					grip1Path.Fill();
					appearance.gripBorder.SetStroke();
					grip1Path.LineWidth = 1;
					grip1Path.Stroke();
					
					
					//// Grip2 Drawing
					grip2Path = UIBezierPath.FromRect(new CGRect(tabAt.GetMinX() + 15.5f, y+4f, 2, 20));
					appearance.gripBackground.SetFill();
					grip2Path.Fill();
					appearance.gripBorder.SetStroke();
					grip2Path.LineWidth = 1;
					grip2Path.Stroke();
					
					
					//// Grip3 Drawing
					grip3Path = UIBezierPath.FromRect(new CGRect(tabAt.GetMinX() + 19.5f, y+4f, 2, 20));
					appearance.gripBackground.SetFill();
					grip3Path.Fill();
					appearance.gripBorder.SetStroke();
					grip3Path.LineWidth = 1;
					grip3Path.Stroke();
					
					context.EndTransparencyLayer();
					context.RestoreState();
				}
				
				//Add title
				DrawTextRotated (_title, tabAt.GetMinX() + 24f, y + 35f, 90);
				break;
			case ACTrayTabType.TitleOnly:
				//Calculate position
				y=tabAt.GetMinY()+12f+((_tabWidth/2f)*0.1f);

				//Add title
				DrawTextRotated (_title, tabAt.GetMinX() + 24f, y, 90);
				break;
			case ACTrayTabType.IconOnly:
				//Has an icon been specified?
				if (icon!=null) {
					//Calculate position
					y=tabAt.GetMinY()+((_tabWidth/2f)-9f);

					//// tabIcon Drawing
					tabIconRect = new CGRect(tabAt.GetMinX() + 1, y, 30, 30);
					tabIconPath = UIBezierPath.FromRect(tabIconRect);
					context.SaveState();
					tabIconPath.AddClip();
					icon.Draw(new CGRect((float)Math.Floor(tabIconRect.GetMinX() + 1f), (float)Math.Floor(y + 0.5f), icon.Size.Width, icon.Size.Height),CGBlendMode.Normal,appearance.tabAlpha);
					context.RestoreState();
				}
				break;
			case ACTrayTabType.IconAndTitle:
				//Calculate position
				y=tabAt.GetMinY()+12f+((_tabWidth/2f)*0.1f);

				//Has an icon been specified?
				if (icon!=null) {
					//// tabIcon Drawing
					tabIconRect = new CGRect(tabAt.GetMinX() + 1, y, 30, 30);
					tabIconPath = UIBezierPath.FromRect(tabIconRect);
					context.SaveState();
					tabIconPath.AddClip();
					icon.Draw(new CGRect((float)Math.Floor(tabIconRect.GetMinX() + 1f), (float)Math.Floor(y + 0.5f), icon.Size.Width, icon.Size.Height),CGBlendMode.Normal,appearance.tabAlpha);
					context.RestoreState();
				}

				//Add title
				DrawTextRotated (_title, tabAt.GetMinX() + 24f, y + 35f, 90);
				break;
			}
			
			//Take action based on the frame type
			switch(_frameType){
			case ACTrayFrameType.None:
				//// Fix rendering error
				rightEdgePath = UIBezierPath.FromRect(new CGRect(rect.Width-39f, 0f, 4, rect.Height));
				appearance.background.SetFill();
				rightEdgePath.Fill();
				break;
			case ACTrayFrameType.EdgeOnly:
				//// RightEdge Drawing
				rightEdgePath = UIBezierPath.FromRect(new CGRect(rect.Width-39f, 0f, 4, rect.Height));
				appearance.frame.SetFill();
				rightEdgePath.Fill();
				break;
			case ACTrayFrameType.EdgeAndSides:
				//// RightEdge Drawing
				rightEdgePath = UIBezierPath.FromRect(new CGRect(rect.Width-39f, 0f, 4, rect.Height));
				appearance.frame.SetFill();
				rightEdgePath.Fill();
				
				//// TopEdge Drawing
				topEdgePath = UIBezierPath.FromRect(new CGRect(0, 0, rect.Width-39f, 4));
				appearance.frame.SetFill();
				topEdgePath.Fill();
				
				//// BottomEdge Drawing
				bottomEdgePath = UIBezierPath.FromRect(new CGRect(0, rect.Height-4f, rect.Width-39f, 4));
				appearance.frame.SetFill();
				bottomEdgePath.Fill();
				break;
			case ACTrayFrameType.FullFrame:
				//// RightEdge Drawing
				rightEdgePath = UIBezierPath.FromRect(new CGRect(rect.Width-39f, 0f, 4, rect.Height));
				appearance.frame.SetFill();
				rightEdgePath.Fill();
				
				//// TopEdge Drawing
				topEdgePath = UIBezierPath.FromRect(new CGRect(0, 0, rect.Width-39f, 4));
				appearance.frame.SetFill();
				topEdgePath.Fill();
				
				//// BottomEdge Drawing
				bottomEdgePath = UIBezierPath.FromRect(new CGRect(0, rect.Height-4f, rect.Width-39f, 4));
				appearance.frame.SetFill();
				bottomEdgePath.Fill();
				
				//// LeftEdge Drawing
				leftEdgePath = UIBezierPath.FromRect(new CGRect(0, 0, 4, rect.Height));
				appearance.frame.SetFill();
				leftEdgePath.Fill();
				break;
			}
			
		}

		/// <summary>
		/// Draws the right tray.
		/// </summary>
		/// <param name="rect">Rect.</param>
		private void DrawRightTray(CGRect rect){
			UIBezierPath rightEdgePath;
			UIBezierPath topEdgePath;
			UIBezierPath bottomEdgePath;
			UIBezierPath leftEdgePath;
			UIBezierPath thumbPath;
			UIBezierPath grip1Path;
			UIBezierPath grip2Path;
			UIBezierPath grip3Path;
			UIBezierPath tabIconPath;
			CGRect tabIconRect;
			
			nfloat y = 0f;
			
			//// General Declarations
			var context = UIGraphics.GetCurrentContext();
			
			//// Shadow Declarations
			var bodyShadow = appearance.shadow.CGColor;
			var bodyShadowOffset = new CGSize(-3.1f, 0f);
			var bodyShadowBlurRadius = 5;
			
			//// Frames
			var tabAt = new CGRect(5f, CalculateTabposition (rect), 36, _tabWidth+10f);
			
			// Save thumb "hotspot" for testing later
			_thumbHotspot = tabAt;
			
			//// Body Drawing
			_contentArea = new CGRect (35f, 0, rect.Width - 35f, rect.Height);
			var bodyPath = UIBezierPath.FromRect(_contentArea);
			context.SaveState();
			if (!appearance.flat) {
				if (_hideBodyShadow) {
					if (!isClosed) 
						context.SetShadow(bodyShadowOffset, bodyShadowBlurRadius, bodyShadow);
				} else {
					context.SetShadow(bodyShadowOffset, bodyShadowBlurRadius, bodyShadow);
				}
			}
			appearance.background.SetFill();
			bodyPath.Fill();
			context.RestoreState();
			
			appearance.border.SetStroke();
			bodyPath.LineWidth = 1;
			bodyPath.Stroke();

			//Mix up tab background
			UIColor tabColor;
			
			if (_frameType==ACTrayFrameType.None) {
				tabColor=appearance.background.ColorWithAlpha (appearance.tabAlpha);
			} else {
				tabColor=appearance.frame.ColorWithAlpha (appearance.tabAlpha);
			}

			//Is this Tab custom drawn?
			if (_tabType==ACTrayTabType.CustomDrawn) {
				//Yes, raise the custom drawn event
				RaiseCustomDrawDragTab(tabAt);
			} else {
				//// Tab Drawing
				UIBezierPath tabPath = new UIBezierPath();
				tabPath.MoveTo(new CGPoint(tabAt.GetMinX() + 31.5f, tabAt.GetMinY() + 0.95000f * tabAt.Height));
				tabPath.AddLineTo(new CGPoint(tabAt.GetMinX() + 1.5f, tabAt.GetMinY() + 0.90455f * tabAt.Height));
				tabPath.AddLineTo(new CGPoint(tabAt.GetMinX() + 1.5f, tabAt.GetMinY() + 0.08636f * tabAt.Height));
				tabPath.AddLineTo(new CGPoint(tabAt.GetMinX() + 31.5f, tabAt.GetMinY() + 0.04091f * tabAt.Height));
				tabPath.AddLineTo(new CGPoint(tabAt.GetMinX() + 31.5f, tabAt.GetMinY() + 0.95000f * tabAt.Height));
				tabPath.ClosePath();
				context.SaveState();
				if (!appearance.flat) {
					context.SetShadow(bodyShadowOffset, bodyShadowBlurRadius, bodyShadow);
				}
				tabColor.SetFill();
				tabPath.Fill();
				context.RestoreState();
			}
			
			//Take action based on the tab type
			switch (_tabType) {
			case ACTrayTabType.Plain:
				//Do nothing
				break;
			case ACTrayTabType.GripOnly:
				//// Drag Thumb
			{
				context.SaveState();
				if (!appearance.flat) context.SetBlendMode(CGBlendMode.Multiply);
				context.BeginTransparencyLayer();
				
				//Calculate position
				y=tabAt.GetMinY()+((_tabWidth/2f)-9f);
				
				//// Thumb Drawing
				thumbPath = UIBezierPath.FromRect(new CGRect(tabAt.GetMinX() + 8.5f, y, 17, 27));
				appearance.thumbBackground.SetFill();
				thumbPath.Fill();
				appearance.thumbBorder.SetStroke();
				thumbPath.LineWidth = 1;
				thumbPath.Stroke();
				
				
				//// Grip1 Drawing
				grip1Path = UIBezierPath.FromRect(new CGRect(tabAt.GetMinX() + 11.5f, y+4f, 2, 20));
				appearance.gripBackground.SetFill();
				grip1Path.Fill();
				appearance.gripBorder.SetStroke();
				grip1Path.LineWidth = 1;
				grip1Path.Stroke();
				
				
				//// Grip2 Drawing
				grip2Path = UIBezierPath.FromRect(new CGRect(tabAt.GetMinX() + 15.5f, y+4f, 2, 20));
				appearance.gripBackground.SetFill();
				grip2Path.Fill();
				appearance.gripBorder.SetStroke();
				grip2Path.LineWidth = 1;
				grip2Path.Stroke();
				
				
				//// Grip3 Drawing
				grip3Path = UIBezierPath.FromRect(new CGRect(tabAt.GetMinX() + 19.5f, y+4f, 2, 20));
				appearance.gripBackground.SetFill();
				grip3Path.Fill();
				appearance.gripBorder.SetStroke();
				grip3Path.LineWidth = 1;
				grip3Path.Stroke();
				
				
				context.EndTransparencyLayer();
				context.RestoreState();
			}
				break;
			case ACTrayTabType.GripAndTitle:
				//// Drag Thumb
			{
				context.SaveState();
				if (!appearance.flat) context.SetBlendMode(CGBlendMode.Multiply);
				context.BeginTransparencyLayer();
				
				//Calculate position
				y=tabAt.GetMaxY()-(39f+((_tabWidth/2f)*0.1f));
				
				//// Thumb Drawing
				thumbPath = UIBezierPath.FromRect(new CGRect(tabAt.GetMinX() + 8.5f, y, 17, 27));
				appearance.thumbBackground.SetFill();
				thumbPath.Fill();
				appearance.thumbBorder.SetStroke();
				thumbPath.LineWidth = 1;
				thumbPath.Stroke();
				
				
				//// Grip1 Drawing
				grip1Path = UIBezierPath.FromRect(new CGRect(tabAt.GetMinX() + 11.5f, y+4f, 2, 20));
				appearance.gripBackground.SetFill();
				grip1Path.Fill();
				appearance.gripBorder.SetStroke();
				grip1Path.LineWidth = 1;
				grip1Path.Stroke();
				
				
				//// Grip2 Drawing
				grip2Path = UIBezierPath.FromRect(new CGRect(tabAt.GetMinX() + 15.5f, y+4f, 2, 20));
				appearance.gripBackground.SetFill();
				grip2Path.Fill();
				appearance.gripBorder.SetStroke();
				grip2Path.LineWidth = 1;
				grip2Path.Stroke();
				
				
				//// Grip3 Drawing
				grip3Path = UIBezierPath.FromRect(new CGRect(tabAt.GetMinX() + 19.5f, y+4f, 2, 20));
				appearance.gripBackground.SetFill();
				grip3Path.Fill();
				appearance.gripBorder.SetStroke();
				grip3Path.LineWidth = 1;
				grip3Path.Stroke();
				
				context.EndTransparencyLayer();
				context.RestoreState();
			}
				
				//Add title
				DrawTextRotated (_title, tabAt.GetMinX() + 10f, y - 10f, -90);
				break;
			case ACTrayTabType.TitleOnly:
				//Calculate position
				y=tabAt.GetMaxY()-(12f+((_tabWidth/2f)*0.1f));
				
				//Add title
				DrawTextRotated (_title, tabAt.GetMinX() + 10f, y, -90);
				break;
			case ACTrayTabType.IconOnly:
				//Has an icon been specified?
				if (icon!=null) {
					//Calculate position
					y=tabAt.GetMinY()+((_tabWidth/2f)-9f);
					
					//// tabIcon Drawing
					tabIconRect = new CGRect(tabAt.GetMinX() + 1, y, 30, 30);
					tabIconPath = UIBezierPath.FromRect(tabIconRect);
					context.SaveState();
					tabIconPath.AddClip();
					icon.Draw(new CGRect((float)Math.Floor(tabIconRect.GetMinX() + 1f), (float)Math.Floor(y + 0.5f), icon.Size.Width, icon.Size.Height),CGBlendMode.Normal,appearance.tabAlpha);
					context.RestoreState();
				}
				break;
			case ACTrayTabType.IconAndTitle:
				//Calculate position
				y=tabAt.GetMaxY()-(39f+((_tabWidth/2f)*0.1f));
				
				//Has an icon been specified?
				if (icon!=null) {
					//// tabIcon Drawing
					tabIconRect = new CGRect(tabAt.GetMinX() + 1, y, 30, 30);
					tabIconPath = UIBezierPath.FromRect(tabIconRect);
					context.SaveState();
					tabIconPath.AddClip();
					icon.Draw(new CGRect((float)Math.Floor(tabIconRect.GetMinX() + 1f), (float)Math.Floor(y + 0.5f), icon.Size.Width, icon.Size.Height),CGBlendMode.Normal,appearance.tabAlpha);
					context.RestoreState();
				}
				
				//Add title
				DrawTextRotated (_title, tabAt.GetMinX() + 10f, y - 10f, -90);
				break;
			}
			
			//Take action based on the frame type
			switch(_frameType){
			case ACTrayFrameType.None:
				//// Fix rendering error
				rightEdgePath = UIBezierPath.FromRect(new CGRect(35f, 0f, 4, rect.Height));
				appearance.background.SetFill();
				rightEdgePath.Fill();
				break;
			case ACTrayFrameType.EdgeOnly:
				//// RightEdge Drawing
				rightEdgePath = UIBezierPath.FromRect(new CGRect(35f, 0f, 4, rect.Height));
				appearance.frame.SetFill();
				rightEdgePath.Fill();
				break;
			case ACTrayFrameType.EdgeAndSides:
				//// RightEdge Drawing
				rightEdgePath = UIBezierPath.FromRect(new CGRect(35f, 0f, 4, rect.Height));
				appearance.frame.SetFill();
				rightEdgePath.Fill();
				
				//// TopEdge Drawing
				topEdgePath = UIBezierPath.FromRect(new CGRect(35f, 0, rect.Width-39f, 4));
				appearance.frame.SetFill();
				topEdgePath.Fill();
				
				//// BottomEdge Drawing
				bottomEdgePath = UIBezierPath.FromRect(new CGRect(35f, rect.Height-4f, rect.Width-39f, 4));
				appearance.frame.SetFill();
				bottomEdgePath.Fill();
				break;
			case ACTrayFrameType.FullFrame:
				//// RightEdge Drawing
				rightEdgePath = UIBezierPath.FromRect(new CGRect(35f, 0f, 4, rect.Height));
				appearance.frame.SetFill();
				rightEdgePath.Fill();
				
				//// TopEdge Drawing
				topEdgePath = UIBezierPath.FromRect(new CGRect(35f, 0, rect.Width-39f, 4));
				appearance.frame.SetFill();
				topEdgePath.Fill();
				
				//// BottomEdge Drawing
				bottomEdgePath = UIBezierPath.FromRect(new CGRect(35f, rect.Height-4f, rect.Width-39f, 4));
				appearance.frame.SetFill();
				bottomEdgePath.Fill();
				
				//// LeftEdge Drawing
				leftEdgePath = UIBezierPath.FromRect(new CGRect(rect.Width-4f, 0, 4, rect.Height));
				appearance.frame.SetFill();
				leftEdgePath.Fill();
				break;
			}
			
		}

		/// <summary>
		/// Draws the top tray.
		/// </summary>
		/// <param name="rect">Rect.</param>
		private void DrawTopTray(CGRect rect){
			UIBezierPath rightEdgePath;
			UIBezierPath topEdgePath;
			UIBezierPath bottomEdgePath;
			UIBezierPath leftEdgePath;
			UIBezierPath thumbPath;
			UIBezierPath grip1Path;
			UIBezierPath grip2Path;
			UIBezierPath grip3Path;
			UIBezierPath tabIconPath;
			CGRect tabIconRect;
			
			nfloat x = 0f;
			
			//// General Declarations
			var context = UIGraphics.GetCurrentContext();
			
			//// Shadow Declarations
			var bodyShadow = appearance.shadow.CGColor;
			var bodyShadowOffset = new CGSize(0f, 3.1f);
			var bodyShadowBlurRadius = 5;
			
			//// Frames
			var tabAt = new CGRect(CalculateTabposition (rect), rect.Height-38f, _tabWidth+10f, 36f);
			
			// Save thumb "hotspot" for testing later
			_thumbHotspot = tabAt;
			
			//// Body Drawing
			_contentArea = new CGRect (0, 0, rect.Width, rect.Height - 35f);
			var bodyPath = UIBezierPath.FromRect(_contentArea);
			context.SaveState();
			if (!appearance.flat) {
				if (_hideBodyShadow) {
					if (!isClosed) 
						context.SetShadow(bodyShadowOffset, bodyShadowBlurRadius, bodyShadow);
				} else {
					context.SetShadow(bodyShadowOffset, bodyShadowBlurRadius, bodyShadow);
				}
			}
			appearance.background.SetFill();
			bodyPath.Fill();
			context.RestoreState();
			
			appearance.border.SetStroke();
			bodyPath.LineWidth = 1;
			bodyPath.Stroke();

			//Mix up tab background
			UIColor tabColor;
			
			if (_frameType==ACTrayFrameType.None) {
				tabColor=appearance.background.ColorWithAlpha (appearance.tabAlpha);
			} else {
				tabColor=appearance.frame.ColorWithAlpha (appearance.tabAlpha);
			}

			//Is this Tab custom drawn?
			if (_tabType==ACTrayTabType.CustomDrawn) {
				//Yes, raise the custom drawn event
				RaiseCustomDrawDragTab(tabAt);
			} else {
				//// Tab Drawing
				UIBezierPath tabPath = new UIBezierPath();
				tabPath.MoveTo(new CGPoint(tabAt.GetMinX() + 0.08636f * tabAt.Width, tabAt.GetMinY() + 32.5f));
				tabPath.AddLineTo(new CGPoint(tabAt.GetMinX() + 0.90455f * tabAt.Width, tabAt.GetMinY() + 32.5f));
				tabPath.AddLineTo(new CGPoint(tabAt.GetMinX() + 0.95000f * tabAt.Width, tabAt.GetMinY() + 2.5f));
				tabPath.AddLineTo(new CGPoint(tabAt.GetMinX() + 0.04091f * tabAt.Width, tabAt.GetMinY() + 2.5f));
				tabPath.AddLineTo(new CGPoint(tabAt.GetMinX() + 0.08636f * tabAt.Width, tabAt.GetMinY() + 32.5f));
				tabPath.ClosePath();
				context.SaveState();
				if (!appearance.flat) {
					context.SetShadow(bodyShadowOffset, bodyShadowBlurRadius, bodyShadow);
				}
				tabColor.SetFill();
				tabPath.Fill();
				context.RestoreState();
			}
			
			//Take action based on the tab type
			switch (_tabType) {
			case ACTrayTabType.Plain:
				//Do nothing
				break;
			case ACTrayTabType.GripOnly:
				//// Drag Thumb
			{
				context.SaveState();
				if (!appearance.flat) context.SetBlendMode(CGBlendMode.Multiply);
				context.BeginTransparencyLayer();
				
				//Calculate position
				x=tabAt.GetMinX()+((_tabWidth/2f)-9f);
				
				//// Thumb Drawing
				thumbPath = UIBezierPath.FromRect(new CGRect(x, tabAt.GetMinY() + 9.5f, 26, 16));
				appearance.thumbBackground.SetFill();
				thumbPath.Fill();
				appearance.thumbBorder.SetStroke();
				thumbPath.LineWidth = 1;
				thumbPath.Stroke();

				
				//// Grip1 Drawing
				grip1Path = UIBezierPath.FromRect(new CGRect(x+3f, tabAt.GetMinY() + 12, 20, 2));
				appearance.gripBackground.SetFill();
				grip1Path.Fill();
				appearance.gripBorder.SetStroke();
				grip1Path.LineWidth = 1;
				grip1Path.Stroke();
				
				
				//// Grip2 Drawing
				grip2Path = UIBezierPath.FromRect(new CGRect(x+3f, tabAt.GetMinY() + 16.5f, 20, 2));
				appearance.gripBackground.SetFill();
				grip2Path.Fill();
				appearance.gripBorder.SetStroke();
				grip2Path.LineWidth = 1;
				grip2Path.Stroke();

				
				//// Grip3 Drawing
				grip3Path = UIBezierPath.FromRect(new CGRect(x+3f, tabAt.GetMinY() + 20.5f, 20, 2));
				appearance.gripBackground.SetFill();
				grip3Path.Fill();
				appearance.gripBorder.SetStroke();
				grip3Path.LineWidth = 1;
				grip3Path.Stroke();
				
				
				context.EndTransparencyLayer();
				context.RestoreState();
			}
				break;
			case ACTrayTabType.GripAndTitle:
				//// Drag Thumb
			{
				context.SaveState();
				if (!appearance.flat) context.SetBlendMode(CGBlendMode.Multiply);
				context.BeginTransparencyLayer();
				
				//Calculate position
				x=tabAt.GetMinX()+12f+((_tabWidth/2f)*0.1f);
				
				//// Thumb Drawing
				thumbPath = UIBezierPath.FromRect(new CGRect(x, tabAt.GetMinY() + 9.5f, 26, 16));
				appearance.thumbBackground.SetFill();
				thumbPath.Fill();
				appearance.thumbBorder.SetStroke();
				thumbPath.LineWidth = 1;
				thumbPath.Stroke();
				
				
				//// Grip1 Drawing
				grip1Path = UIBezierPath.FromRect(new CGRect(x+3f, tabAt.GetMinY() + 12, 20, 2));
				appearance.gripBackground.SetFill();
				grip1Path.Fill();
				appearance.gripBorder.SetStroke();
				grip1Path.LineWidth = 1;
				grip1Path.Stroke();
				
				
				//// Grip2 Drawing
				grip2Path = UIBezierPath.FromRect(new CGRect(x+3f, tabAt.GetMinY() + 16.5f, 20, 2));
				appearance.gripBackground.SetFill();
				grip2Path.Fill();
				appearance.gripBorder.SetStroke();
				grip2Path.LineWidth = 1;
				grip2Path.Stroke();
				
				
				//// Grip3 Drawing
				grip3Path = UIBezierPath.FromRect(new CGRect(x+3f, tabAt.GetMinY() + 20.5f, 20, 2));
				appearance.gripBackground.SetFill();
				grip3Path.Fill();
				appearance.gripBorder.SetStroke();
				grip3Path.LineWidth = 1;
				grip3Path.Stroke();
				
				context.EndTransparencyLayer();
				context.RestoreState();
			}

				//Add title
				DrawTextRotated (_title, x+35f, tabAt.GetMinY()+10f, 0);
				break;
			case ACTrayTabType.TitleOnly:
				//Calculate position
				x=tabAt.GetMinX()+12f+((_tabWidth/2f)*0.1f);
				
				//Add title
				DrawTextRotated (_title, x, tabAt.GetMinY()+10f, 0);
				break;
			case ACTrayTabType.IconOnly:
				//Has an icon been specified?
				if (icon!=null) {
					//Calculate position
					x=tabAt.GetMinX()+((_tabWidth/2f)-9f);
					
					//// tabIcon Drawing
					tabIconRect = new CGRect(x, tabAt.GetMinY()+6f, 30, 30);
					tabIconPath = UIBezierPath.FromRect(tabIconRect);
					context.SaveState();
					tabIconPath.AddClip();
					icon.Draw(new CGRect((float)Math.Floor(x + 1f), (float)Math.Floor(tabIconRect.GetMinY () + 0.5f), icon.Size.Width, icon.Size.Height),CGBlendMode.Normal,appearance.tabAlpha);
					context.RestoreState();
				}
				break;
			case ACTrayTabType.IconAndTitle:
				//Calculate position
				x=tabAt.GetMinX()+12f+((_tabWidth/2f)*0.1f);
				
				//Has an icon been specified?
				if (icon!=null) {
					//// tabIcon Drawing
					tabIconRect = new CGRect(x, tabAt.GetMinY()+6f, 30, 30);
					tabIconPath = UIBezierPath.FromRect(tabIconRect);
					context.SaveState();
					tabIconPath.AddClip();
					icon.Draw(new CGRect((float)Math.Floor(x + 1f), (float)Math.Floor(tabIconRect.GetMinY () + 0.5f), icon.Size.Width, icon.Size.Height),CGBlendMode.Normal,appearance.tabAlpha);
					context.RestoreState();
				}
				
				//Add title
				DrawTextRotated (_title, x+35f, tabAt.GetMinY()+10f, 0);
				break;
			}
			
			//Take action based on the frame type
			switch(_frameType){
			case ACTrayFrameType.None:
				//// Fix rendering error
				rightEdgePath = UIBezierPath.FromRect(new CGRect(0f, rect.Height-39f, rect.Width, 4));
				appearance.background.SetFill();
				rightEdgePath.Fill();
				break;
			case ACTrayFrameType.EdgeOnly:
				//// BottomEdge Drawing
				rightEdgePath = UIBezierPath.FromRect(new CGRect(0f, rect.Height-39f, rect.Width, 4));
				appearance.frame.SetFill();
				rightEdgePath.Fill();
				break;
			case ACTrayFrameType.EdgeAndSides:
				//// BottomEdge Drawing
				rightEdgePath = UIBezierPath.FromRect(new CGRect(0f, rect.Height-39f, rect.Width, 4));
				appearance.frame.SetFill();
				rightEdgePath.Fill();
				
				//// LeftEdge Drawing
				topEdgePath = UIBezierPath.FromRect(new CGRect(0, 0, 4, rect.Height-39f));
				appearance.frame.SetFill();
				topEdgePath.Fill();
				
				//// RightEdge Drawing
				bottomEdgePath = UIBezierPath.FromRect(new CGRect(rect.Width-4f, 0f, 4, rect.Height-39f));
				appearance.frame.SetFill();
				bottomEdgePath.Fill();
				break;
			case ACTrayFrameType.FullFrame:
				//// BottomEdge Drawing
				rightEdgePath = UIBezierPath.FromRect(new CGRect(0f, rect.Height-39f, rect.Width, 4));
				appearance.frame.SetFill();
				rightEdgePath.Fill();
				
				//// LeftEdge Drawing
				topEdgePath = UIBezierPath.FromRect(new CGRect(0, 0, 4, rect.Height-39f));
				appearance.frame.SetFill();
				topEdgePath.Fill();
				
				//// RightEdge Drawing
				bottomEdgePath = UIBezierPath.FromRect(new CGRect(rect.Width-4f, 0f, 4, rect.Height-39f));
				appearance.frame.SetFill();
				bottomEdgePath.Fill();
				
				//// LeftEdge Drawing
				leftEdgePath = UIBezierPath.FromRect(new CGRect(0, 0, rect.Width, 4));
				appearance.frame.SetFill();
				leftEdgePath.Fill();
				break;
			}
			
		}

		/// <summary>
		/// Draws the bottom tray.
		/// </summary>
		/// <param name="rect">Rect.</param>
		private void DrawBottomTray(CGRect rect){
			UIBezierPath rightEdgePath;
			UIBezierPath topEdgePath;
			UIBezierPath bottomEdgePath;
			UIBezierPath leftEdgePath;
			UIBezierPath thumbPath;
			UIBezierPath grip1Path;
			UIBezierPath grip2Path;
			UIBezierPath grip3Path;
			UIBezierPath tabIconPath;
			CGRect tabIconRect;
			
			nfloat x = 0f;
			
			//// General Declarations
			var context = UIGraphics.GetCurrentContext();
			
			//// Shadow Declarations
			var bodyShadow = appearance.shadow.CGColor;
			var bodyShadowOffset = new CGSize(0f, -3.1f);
			var bodyShadowBlurRadius = 5;
			
			//// Frames
			var tabAt = new CGRect(CalculateTabposition (rect), 2f, _tabWidth+10f, 36f);
			
			// Save thumb "hotspot" for testing later
			_thumbHotspot = tabAt;
			
			//// Body Drawing
			_contentArea = new CGRect (0, 35f, rect.Width, rect.Height - 35f);
			var bodyPath = UIBezierPath.FromRect(_contentArea);
			context.SaveState();
			if (!appearance.flat) {
				if (_hideBodyShadow) {
					if (!isClosed) 
						context.SetShadow(bodyShadowOffset, bodyShadowBlurRadius, bodyShadow);
				} else {
					context.SetShadow(bodyShadowOffset, bodyShadowBlurRadius, bodyShadow);
				}
			}
			appearance.background.SetFill();
			bodyPath.Fill();
			context.RestoreState();
			
			appearance.border.SetStroke();
			bodyPath.LineWidth = 1;
			bodyPath.Stroke();

			//Mix up tab background
			UIColor tabColor;
			
			if (_frameType==ACTrayFrameType.None) {
				tabColor=appearance.background.ColorWithAlpha (appearance.tabAlpha);
			} else {
				tabColor=appearance.frame.ColorWithAlpha (appearance.tabAlpha);
			}

			//Is this Tab custom drawn?
			if (_tabType==ACTrayTabType.CustomDrawn) {
				//Yes, raise the custom drawn event
				RaiseCustomDrawDragTab(tabAt);
			} else {
				//// Tab Drawing
				UIBezierPath tabPath = new UIBezierPath();
				tabPath.MoveTo(new CGPoint(tabAt.GetMinX() + 0.08636f * tabAt.Width, tabAt.GetMinY() + 2.5f));
				tabPath.AddLineTo(new CGPoint(tabAt.GetMinX() + 0.90455f * tabAt.Width, tabAt.GetMinY() + 2.5f));
				tabPath.AddLineTo(new CGPoint(tabAt.GetMinX() + 0.95000f * tabAt.Width, tabAt.GetMinY() + 32.5f));
				tabPath.AddLineTo(new CGPoint(tabAt.GetMinX() + 0.04091f * tabAt.Width, tabAt.GetMinY() + 32.5f));
				tabPath.AddLineTo(new CGPoint(tabAt.GetMinX() + 0.08636f * tabAt.Width, tabAt.GetMinY() + 2.5f));
				tabPath.ClosePath();
				context.SaveState();
				if (!appearance.flat) {
					context.SetShadow (bodyShadowOffset, bodyShadowBlurRadius, bodyShadow);
				}
				tabColor.SetFill();
				tabPath.Fill();
				context.RestoreState();
			}
			
			//Take action based on the tab type
			switch (_tabType) {
			case ACTrayTabType.Plain:
				//Do nothing
				break;
			case ACTrayTabType.GripOnly:
				//// Drag Thumb
			{
				context.SaveState();
				if (!appearance.flat) context.SetBlendMode(CGBlendMode.Multiply);
				context.BeginTransparencyLayer();
				
				//Calculate position
				x=tabAt.GetMinX()+((_tabWidth/2f)-9f);
				
				//// Thumb Drawing
				thumbPath = UIBezierPath.FromRect(new CGRect(x, tabAt.GetMinY() + 9.5f, 26, 16));
				appearance.thumbBackground.SetFill();
				thumbPath.Fill();
				appearance.thumbBorder.SetStroke();
				thumbPath.LineWidth = 1;
				thumbPath.Stroke();
				
				
				//// Grip1 Drawing
				grip1Path = UIBezierPath.FromRect(new CGRect(x+3f, tabAt.GetMinY() + 12, 20, 2));
				appearance.gripBackground.SetFill();
				grip1Path.Fill();
				appearance.gripBorder.SetStroke();
				grip1Path.LineWidth = 1;
				grip1Path.Stroke();
				
				
				//// Grip2 Drawing
				grip2Path = UIBezierPath.FromRect(new CGRect(x+3f, tabAt.GetMinY() + 16.5f, 20, 2));
				appearance.gripBackground.SetFill();
				grip2Path.Fill();
				appearance.gripBorder.SetStroke();
				grip2Path.LineWidth = 1;
				grip2Path.Stroke();
				
				
				//// Grip3 Drawing
				grip3Path = UIBezierPath.FromRect(new CGRect(x+3f, tabAt.GetMinY() + 20.5f, 20, 2));
				appearance.gripBackground.SetFill();
				grip3Path.Fill();
				appearance.gripBorder.SetStroke();
				grip3Path.LineWidth = 1;
				grip3Path.Stroke();
				
				
				context.EndTransparencyLayer();
				context.RestoreState();
			}
				break;
			case ACTrayTabType.GripAndTitle:
				//// Drag Thumb
			{
				context.SaveState();
				if (!appearance.flat) context.SetBlendMode(CGBlendMode.Multiply);
				context.BeginTransparencyLayer();
				
				//Calculate position
				x=tabAt.GetMinX()+12f+((_tabWidth/2f)*0.1f);
				
				//// Thumb Drawing
				thumbPath = UIBezierPath.FromRect(new CGRect(x, tabAt.GetMinY() + 9.5f, 26, 16));
				appearance.thumbBackground.SetFill();
				thumbPath.Fill();
				appearance.thumbBorder.SetStroke();
				thumbPath.LineWidth = 1;
				thumbPath.Stroke();
				
				
				//// Grip1 Drawing
				grip1Path = UIBezierPath.FromRect(new CGRect(x+3f, tabAt.GetMinY() + 12, 20, 2));
				appearance.gripBackground.SetFill();
				grip1Path.Fill();
				appearance.gripBorder.SetStroke();
				grip1Path.LineWidth = 1;
				grip1Path.Stroke();
				
				
				//// Grip2 Drawing
				grip2Path = UIBezierPath.FromRect(new CGRect(x+3f, tabAt.GetMinY() + 16.5f, 20, 2));
				appearance.gripBackground.SetFill();
				grip2Path.Fill();
				appearance.gripBorder.SetStroke();
				grip2Path.LineWidth = 1;
				grip2Path.Stroke();
				
				
				//// Grip3 Drawing
				grip3Path = UIBezierPath.FromRect(new CGRect(x+3f, tabAt.GetMinY() + 20.5f, 20, 2));
				appearance.gripBackground.SetFill();
				grip3Path.Fill();
				appearance.gripBorder.SetStroke();
				grip3Path.LineWidth = 1;
				grip3Path.Stroke();
				
				context.EndTransparencyLayer();
				context.RestoreState();
			}
				
				//Add title
				DrawTextRotated (_title, x+35f, tabAt.GetMinY()+10f, 0);
				break;
			case ACTrayTabType.TitleOnly:
				//Calculate position
				x=tabAt.GetMinX()+12f+((_tabWidth/2f)*0.1f);
				
				//Add title
				DrawTextRotated (_title, x, tabAt.GetMinY()+10f, 0);
				break;
			case ACTrayTabType.IconOnly:
				//Has an icon been specified?
				if (icon!=null) {
					//Calculate position
					x=tabAt.GetMinX()+((_tabWidth/2f)-9f);
					
					//// tabIcon Drawing
					tabIconRect = new CGRect(x, tabAt.GetMinY()+1f, 30, 30);
					tabIconPath = UIBezierPath.FromRect(tabIconRect);
					context.SaveState();
					tabIconPath.AddClip();
					icon.Draw(new CGRect((float)Math.Floor(x + 1f), (float)Math.Floor(tabIconRect.GetMinY () + 0.5f), icon.Size.Width, icon.Size.Height),CGBlendMode.Normal,appearance.tabAlpha);
					context.RestoreState();
				}
				break;
			case ACTrayTabType.IconAndTitle:
				//Calculate position
				x=tabAt.GetMinX()+12f+((_tabWidth/2f)*0.1f);
				
				//Has an icon been specified?
				if (icon!=null) {
					//// tabIcon Drawing
					tabIconRect = new CGRect(x, tabAt.GetMinY()+1f, 30, 30);
					tabIconPath = UIBezierPath.FromRect(tabIconRect);
					context.SaveState();
					tabIconPath.AddClip();
					icon.Draw(new CGRect((float)Math.Floor(x + 1f), (float)Math.Floor(tabIconRect.GetMinY () + 0.5f), icon.Size.Width, icon.Size.Height),CGBlendMode.Normal,appearance.tabAlpha);
					context.RestoreState();
				}
				
				//Add title
				DrawTextRotated (_title, x+35f, tabAt.GetMinY()+10f, 0);
				break;
			}
			
			//Take action based on the frame type
			switch(_frameType){
			case ACTrayFrameType.None:
				//// Fix rendering error
				rightEdgePath = UIBezierPath.FromRect(new CGRect(0f, 34f, rect.Width, 4));
				appearance.background.SetFill();
				rightEdgePath.Fill();
				break;
			case ACTrayFrameType.EdgeOnly:
				//// BottomEdge Drawing
				rightEdgePath = UIBezierPath.FromRect(new CGRect(0f, 34f, rect.Width, 4));
				appearance.frame.SetFill();
				rightEdgePath.Fill();
				break;
			case ACTrayFrameType.EdgeAndSides:
				//// BottomEdge Drawing
				rightEdgePath = UIBezierPath.FromRect(new CGRect(0f, 34f, rect.Width, 4));
				appearance.frame.SetFill();
				rightEdgePath.Fill();
				
				//// LeftEdge Drawing
				topEdgePath = UIBezierPath.FromRect(new CGRect(0, 35, 4, rect.Height-39f));
				appearance.frame.SetFill();
				topEdgePath.Fill();
				
				//// RightEdge Drawing
				bottomEdgePath = UIBezierPath.FromRect(new CGRect(rect.Width-4f, 35f, 4, rect.Height-39f));
				appearance.frame.SetFill();
				bottomEdgePath.Fill();
				break;
			case ACTrayFrameType.FullFrame:
				//// BottomEdge Drawing
				rightEdgePath = UIBezierPath.FromRect(new CGRect(0f, 34f, rect.Width, 4));
				appearance.frame.SetFill();
				rightEdgePath.Fill();
				
				//// LeftEdge Drawing
				topEdgePath = UIBezierPath.FromRect(new CGRect(0, 35, 4, rect.Height-39f));
				appearance.frame.SetFill();
				topEdgePath.Fill();
				
				//// RightEdge Drawing
				bottomEdgePath = UIBezierPath.FromRect(new CGRect(rect.Width-4f, 35f, 4, rect.Height-39f));
				appearance.frame.SetFill();
				bottomEdgePath.Fill();
				
				//// LeftEdge Drawing
				leftEdgePath = UIBezierPath.FromRect(new CGRect(0, rect.Height-4, rect.Width, 4));
				appearance.frame.SetFill();
				leftEdgePath.Fill();
				break;
			}
			
		}
		#endregion

		#region Overrides
		/// <summary>
		/// Draw the specified rect.
		/// </summary>
		/// <param name="rect">Rect.</param>
		public override void Draw (CGRect rect)
		{
			//Call Base
			base.Draw (rect);

			//Take action based on the orientation
			switch (_orientation) {
			case ACTrayOrientation.Left:
				DrawLeftTray(rect);
				break;
			case ACTrayOrientation.Right:
				DrawRightTray (rect);
				break;
			case ACTrayOrientation.Top:
				DrawTopTray(rect);
				break;
			case ACTrayOrientation.Bottom:
				DrawBottomTray(rect);
				break;
			}
		}

		/// <Docs>Lays out subviews.</Docs>
		/// <summary>
		/// Layouts the subviews.
		/// </summary>
		public override void LayoutSubviews ()
		{
			//Ensure the tray is stuck to the right location in the parent view
			RepositionInParentView ();

			//Layout the subviews for the object
			base.LayoutSubviews ();
		}

		/// <summary>
		/// Tests to see if the given point is inside this <see cref="ActionComponents.ACTray"/>. 
		/// </summary>
		/// <returns><c>true</c>, if inside was pointed, <c>false</c> otherwise.</returns>
		/// <param name="point">Point.</param>
		/// <param name="uievent">Uievent.</param>
		/// <remarks>A point is considered inside only if it is in the <see cref="ActionComponents.ACTray"/>'s <c>contentArea</c> or <c>dragTab</c>. It
		/// excludes the invisible strip on either side of the control.</remarks>
		public override bool PointInside (CGPoint point, UIEvent uievent)
		{
			//Only register points inside the content and thumb areas.
			//Exclude the invisible strip on either side of the tab.
			return (_contentArea.Contains (point) || _thumbHotspot.Contains (point));
		}

		/// <summary>
		/// Tests to see if this <see cref="ActionComponents.ACTray"/> or any of it's <c>subviews</c> were hit during an event
		/// </summary>
		/// <returns>The test.</returns>
		/// <param name="point">Point.</param>
		/// <param name="uievent">Uievent.</param>
		/// <remarks>If this is an auto closing type of <see cref="ActionComponents.ACTray"/> a touch inside it's
		/// content area will force to tray closed</remarks>
		public override UIView HitTest (CGPoint point, UIEvent uievent)
		{
			UIView view,wasHit;
			CGPoint pt;

			//If this is an auto closing tray and the point is inside it's
			//bounds, close the tray first
			if (PointInside (point, uievent) && trayType==ACTrayType.AutoClosingPopup)
				CloseTray (true);

			//Itterate through views backwards until you find the one
			//to send the event to
			for (int n=Subviews.Length-1; n>=0; --n) {
				view=Subviews[n];
				
				pt=this.ConvertPointToView (point,view);
				wasHit=view.HitTest (pt,uievent);
				if (wasHit!=null) {
					return wasHit;
				}
			}
			
			//Return to default behavior
			return base.HitTest (point, uievent);
		}

		/// <Docs>Set containing the touches.</Docs>
		/// <summary>
		/// Sent when one or more fingers touches the screen.
		/// </summary>
		/// <param name="touches">Touches.</param>
		/// <param name="evt">Evt.</param>
		public override void TouchesBegan (NSSet touches, UIEvent evt)
		{
			//Pass call to base object
			if (!this.ExclusiveTouch) base.TouchesBegan (touches, evt);

			//Touches not allowed while animating
			if (_animating) return;

			//Already dragging?
			if (_dragging) return;
			
			//Save starting location
			var pt = (touches.AnyObject as UITouch).LocationInView(this);
			_startLocation = pt;

			//Take action based on the tray type
			switch (trayType) {
			case ACTrayType.Draggable:
				//No, are we inside the thumb area?
				if (!_thumbHotspot.Contains (pt)) return;

				//Mark as dragging
				_dragging=true;
				break;
			case ACTrayType.Popup:
				//No, are we inside the thumb area?
				if (!_thumbHotspot.Contains (pt)) return;

				//Is the tray closed?
				if (isClosed) {
					//Yes, open the tray
					OpenTray (true);
				} else {
					//No, close the tray
					CloseTray(true);
				}
				break;
			case ACTrayType.AutoClosingPopup:
				//Is the tray closed?
				if (isClosed) {
					//No, are we inside the thumb area?
					if (!_thumbHotspot.Contains (pt)) return;

					//Yes, open the tray
					OpenTray (true);
				} else {
					//No, close the tray
					CloseTray(true);
				}
				break;
			}

			//Automatically bring view to front?
			if (bringToFrontOnTouch) this.Superview.BringSubviewToFront(this);

			//Inform caller of event
			RaiseTouched ();

		}
		
		/// <Docs>Set containing the touches.</Docs>
		/// <summary>
		/// To be added.
		/// </summary>
		/// <param name="touches">Touches.</param>
		/// <param name="evt">Evt.</param>
		public override void TouchesMoved (NSSet touches, UIEvent evt)
		{
			bool needsRedraw = false;
			
			//Pass call to base object
			if (!this.ExclusiveTouch) base.TouchesMoved(touches, evt);

			//Touches not allowed while animating
			if (_animating) return;

			// Move relative to the original touch point
			var pt = (touches.AnyObject as UITouch).LocationInView(this);

			//Take action based on the tray type
			switch (trayType) {
			case ACTrayType.Draggable:
				//No, are we inside the thumb area?
				if (!_dragging) return;

				//Are we hiding the shadow and the tray closed?
				needsRedraw=(_hideBodyShadow && isClosed);

				// Move view
				MoveTray(pt);

				//Do we need to redraw the component?
				if (needsRedraw) this.SetNeedsDisplay ();
				break;
			default:
				//Ignore movement
				return;
			}
		}
		
		/// <Docs>Set containing the touches.</Docs>
		/// <summary>
		/// Send when one or more fingers are lifted from the screen.
		/// </summary>
		/// <param name="touches">Touches.</param>
		/// <param name="evt">Evt.</param>
		public override void TouchesEnded (NSSet touches, UIEvent evt)
		{
			//Pass call to base object
			if (!this.ExclusiveTouch) base.TouchesEnded(touches, evt);

			//Touches not allowed while animating
			if (_animating) return;

			// Move relative to the original touch point 
			var pt = (touches.AnyObject as UITouch).LocationInView(this);
			
			switch (trayType) {
			case ACTrayType.Draggable:
				//Clear dragging flag
				_dragging=false;
				
				// Move view
				MoveTray(pt);
				break;
			}
			
			//Inform caller of event
			if (_thumbHotspot.Contains (pt)) RaiseReleased ();

		} 

		/// <Docs>Set containing the touches.</Docs>
		/// <summary>
		/// Sent when the touch processing has been cancelled.
		/// </summary>
		/// <para>This method is typically involved because the application
		///  was interrupted by an external source, like for example,
		///  an incoming phone call.</para>
		/// <param name="touches">Touches.</param>
		/// <param name="evt">Evt.</param>
		public override void TouchesCancelled (NSSet touches, UIEvent evt)
		{
			//Call base
			base.TouchesCancelled (touches, evt);

			//Terminate dragging
			_dragging=false;

		}

		/// <summary>
		/// Dispose the specified disposing.
		/// </summary>
		/// <param name="disposing">If set to <c>true</c> disposing.</param>
		protected override void Dispose (bool disposing)
		{
			//Remove the observer
			NSNotificationCenter.DefaultCenter.RemoveObserver (_orientationObserver);
			
			//Finish releasing the object
			base.Dispose (disposing);
		}
		#endregion 

		#region Events
		/// <summary>
		/// Occurs when this <see cref="ActionComponents.ACTray"/> is touched
		/// </summary>
		public delegate void ACTrayTouchDelegate(ACTray tray);
		public event ACTrayTouchDelegate Touched;
		
		/// <summary>
		/// Raises the touched event
		/// </summary>
		private void RaiseTouched(){
			//Inform caller
			if (this.Touched != null)
				this.Touched (this);
		}
		
		/// <summary>
		/// Occurs when this <see cref="ActionComponents.ACTray"/> is moved
		/// </summary>
		public delegate void ACTrayMovedDelegate(ACTray tray);
		public event ACTrayMovedDelegate Moved;
		
		/// <summary>
		/// Raises the moved event
		/// </summary>
		private void RaiseMoved(){
			//Inform caller
			if(this.Moved!=null) 
				this.Moved(this);
		}
		
		/// <summary>
		/// Occurs when this <see cref="ActionComponents.ACTray"/> was <c>Touched</c> and released 
		/// </summary>
		public delegate void ACTrayReleasedDelegate(ACTray tray);
		public event ACTrayReleasedDelegate Released;
		
		/// <summary>
		/// Raises the released event
		/// </summary>
		private void RaiseReleased(){
			//Inform caller
			if (this.Released != null)
				this.Released (this);
		}

		/// <summary>
		/// Occurs when this <see cref="ActionComponents.ACTray"/> is opened fully by the user
		/// </summary>
		public delegate void ACTrayOpenedDelegate(ACTray tray);
		public event ACTrayOpenedDelegate Opened;

		/// <summary>
		/// Raises the opened.
		/// </summary>
		private void RaiseOpened(){

			//Are we hiding the body shadow?
			if (_hideBodyShadow) {
				//Yes, we must redraw the component
				this.SetNeedsDisplay ();
			}

			//Inform caller
			if (this.Opened != null)
				this.Opened (this);
		}

		/// <summary>
		/// Occurs when this <see cref="ActionComponents.ACTray"/> is completely closed by the user
		/// </summary>
		public delegate void ACTrayClosedDelegate(ACTray tray);
		public event ACTrayClosedDelegate Closed;

		/// <summary>
		/// Raises the closed.
		/// </summary>
		private void RaiseClosed(){

			//Are we hiding the body shadow?
			if (_hideBodyShadow) {
				//Yes, we must redraw the component
				this.SetNeedsDisplay ();
			}
			
			//Inform caller
			if (this.Closed != null)
				this.Closed (this);
		}

		/// <summary>
		/// Occurs when the <see cref="ActionComponents.ACTrayTabType"/> is set to <c>CustomDrawn</c> and the <c>dragTab</c>
		/// needs to be drawn
		/// </summary>
		/// <remarks>The passed <c>rect</c> contains the boundary that the custom tab should be drawn against</remarks>
		public delegate void CustomDrawDragTabDelegate(ACTray tray, CGRect rect);
		public event CustomDrawDragTabDelegate CustomDrawDragTab;

		/// <summary>
		/// Raises the custom draw drag tab event
		/// </summary>
		/// <param name="rect">Rect.</param>
		private void RaiseCustomDrawDragTab(CGRect rect){
			if (this.CustomDrawDragTab!=null) this.CustomDrawDragTab(this,rect);
		}
		#endregion
	}
}

