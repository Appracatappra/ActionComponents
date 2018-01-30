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
	/// The <see cref="ActionComponents.ACTray"/> is a dockable, customizable, slide-out <c>View</c> controller
	/// with a <c>dragTab</c> that can be attached to the edge of any parent <c>View</c>. The <see cref="ActionComponents.ACTray"/> handles
	/// sliding the attached <c>View</c> based on the <see cref="ActionComponents.ACTrayType"/>.
	/// </summary>
	/// <description>The <see cref="ActionComponents.ACTray"/> can be styled by setting its <see cref="ActionComponents.ACTrayAppearance"/>
	/// and <see cref="ActionComponents.ACTrayTabType"/> properties. You can also control the position of the <c>dragTab</c> by setting the tray's
	/// <see cref="ActionComponents.ACTrayTabLocation"/>, <c>tabOffset</c> and <c>tabWidth</c> properties.</description>
	/// <remarks>WARNING! You MUST manually set the <see cref="ActionComponents.ACTray"/>'s <see cref="ActionComponents.ACTrayOrientation"/> 
	/// when the <c>View</c> first loads or the tray will not display or behave correctly.</remarks>
	public class ACTray : RelativeLayout
	{
		#region Private Variables
		private Bitmap _imageCache;
		private ACTrayOrientation _orientation;
		private ACTrayTabLocation _tabLocation;
		private ACTrayTabType _tabType;
		private ACTrayFrameType _frameType;
		private int _tabOffset=0;
		private int _tabWidth = 100;
		private int _icon;
		private string _title="";
		private int _openedPosition=100;
		private int _closedPosition = 0;
		private bool _opened=true;
		private bool _hideBodyShadow=false;
		private Rect _contentArea;
		private Rect _thumbHotspot;
		private bool _dragging;
		private int _previousX;
		private int _previousY;
		private ACTrayGestureListener _doubleTapListener;
		private GestureDetector _gestureDector;
		private bool _disableInteraction=false;
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
		public bool isClosed{
			get{return !_opened;}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="ActionComponents.ACTray"/> is opened.
		/// </summary>
		/// <value><c>true</c> if is opened; otherwise, <c>false</c>.</value>
		public bool isOpened{
			get{return _opened;}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="ActionComponents.ACTray"/> is dragging.
		/// </summary>
		/// <value><c>true</c> if dragging; otherwise, <c>false</c>.</value>
		public bool dragging{
			get{return _dragging;}
		}

		/// <summary>
		/// Gets the current amount of the <see cref="ActionComponents.ACTray"/>'s <c>ContentArea</c> that is 
		/// currently visible on screen
		/// </summary>
		/// <value>The amount of the <c>ContentArea</c> that is currently visible</value>
		/// <remarks>NOTE: This amount excludes the always visible <c>DragTab</c> </remarks>
		public int amountVisible{
			get {
				//Take action based on the orientation of the ActionTray
				switch(_orientation){
				case ACTrayOrientation.Top:
					return (this.layoutParams.Height-34)+TopMargin;
				case ACTrayOrientation.Bottom:
					return _closedPosition-TopMargin;
				case ACTrayOrientation.Left:
					return (this.layoutParams.Width-34)+LeftMargin;
				case ACTrayOrientation.Right:
					return _closedPosition-LeftMargin;
				}

				//Default
				return 0;
			}
		}

		/// <summary>
		/// Returns a string containing the save state of this <see cref="ActionComponents.ACTray"/>
		/// </summary>
		/// <value>The current state of the ActionTray</value>
		/// <remarks>Pass this value to the <c>RestoreState</c> property of the ActionTray after a rotation or restart to return the tray to it's previous state</remarks>
		public string SaveState {
			get{return String.Format("{0}|{1}|{2}|{3}|{4}",_opened,LeftMargin,TopMargin,RightMargin,BottomMargin);}
		}

		/// <summary>
		/// Gets the parent view for this <see cref="ActionComponents.ACTray"/> 
		/// </summary>
		/// <value>The super view.</value>
		public View SuperView{
			get{return (View)(this.Parent);}
		}

		/// <summary>
		/// Gets the layout parameters typecast to a <c>RelativeLayout.LayoutParams</c> format
		/// </summary>
		/// <value>The layout parameters.</value>
		public RelativeLayout.LayoutParams layoutParams{
			get{return (RelativeLayout.LayoutParams)this.LayoutParameters;}
		}

		/// <summary>
		/// Gets or sets the left margin.
		/// </summary>
		/// <value>The left margin.</value>
		public int LeftMargin{
			get{return layoutParams.LeftMargin;}
			set{
				//Adjust position and save back to parent object
				layoutParams.LeftMargin=value;
				this.LayoutParameters=layoutParams;
			}
		}

		/// <summary>
		/// Gets or sets the top margin.
		/// </summary>
		/// <value>The top margin.</value>
		public int TopMargin{
			get{return layoutParams.TopMargin;}
			set{
				//Adjust position and save back to parent object
				layoutParams.TopMargin=value;
				this.LayoutParameters=layoutParams;
			}
		}

		/// <summary>
		/// Gets or sets the right margin.
		/// </summary>
		/// <value>The right margin.</value>
		public int RightMargin{
			get{return layoutParams.RightMargin;}
			set{
				//Adjust position and save back to parent object
				layoutParams.RightMargin=value;
				this.LayoutParameters=layoutParams;
			}
		}

		/// <summary>
		/// Gets or sets the bottom margin.
		/// </summary>
		/// <value>The bottom margin.</value>
		public int BottomMargin{
			get{return layoutParams.BottomMargin;}
			set{
				//Adjust position and save back to parent object
				layoutParams.BottomMargin=value;
				this.LayoutParameters=layoutParams;
			}
		}

		/// <summary>
		/// Gets or sets the orientation of this <see cref="ActionComponents.ACTray"/> on the screen
		/// </summary>
		/// <value>The tray's orientation.</value>
		/// <remarks>This property controls how the <see cref="ActionComponents.ACTray"/> responds to user
		/// interaction and where the <c>dragTab</c> is located</remarks>
		public ACTrayOrientation orientation{
			get{ return _orientation;}
			set{
				//Save value
				_orientation=value;
				
				//Automatically set the open and closed positions based on my location
				CalculateOpenAndClosedPositions ();

				//Force a redraw
				this.Redraw ();
			}
		}

		/// <summary>
		/// Gets or sets the location of the <c>dragTab></c> on this <see cref="ActionComponents.ACTray"/> 
		/// </summary>
		/// <value>The tab location.</value>
		/// <remarks>This property interacts with the <see cref="ActionComponents.ACTrayOrientation"/> property</remarks>
		public ACTrayTabLocation tabLocation{
			get{ return _tabLocation;}
			set{
				//Save location
				_tabLocation=value;
				
				//Force a redraw
				this.Redraw ();
			}
		}

		/// <summary>
		/// Gets or sets the type of the <c>dragTab</c> drawn on this <see cref="ActionComponents.ACTray"/> 
		/// </summary>
		/// <value>The type of the tab.</value>
		/// <remarks>The <see cref="ActionComponents.ACTrayAppearance"/> also controls the appearance of the <c>dragTab</c> </remarks>
		public ACTrayTabType tabType{
			get{ return _tabType;}
			set{
				//Save type
				_tabType=value;
				
				//Force a redraw
				this.Redraw ();
			}
		}

		/// <summary>
		/// Gets or sets the type of the frame drawn around the edge of this <see cref="ActionComponents.ACTray"/> 
		/// </summary>
		/// <value>The type of the frame.</value>
		/// <remarks>This property reacts with the <see cref="ActionComponents.ACTrayOrientation"/> property to
		/// control the appearance and the location of the frame</remarks>
		public ACTrayFrameType frameType{
			get{ return _frameType;}
			set{
				//Save value
				_frameType=value;
				
				//Force a redraw
				this.Redraw ();
			}
		}
		
		/// <summary>
		/// Gets or sets the tab offset for a <c>Custom</c> <see cref="ActionComponents.ACTrayTabLocation"/> 
		/// </summary>
		/// <value>The tab offset.</value>
		/// <remarks>Based on the <see cref="ActionComponents.ACTray"/>'s <see cref="ActionComponents.ACTrayOrientation"/>, the offset will
		/// either be from the tray's top or left side</remarks>
		public int tabOffset{
			get{ return _tabOffset;}
			set{
				//Save value
				_tabOffset=value;
				
				//Force a redraw
				this.Redraw ();
			}
		}

		/// <summary>
		/// Gets or sets the width of the <c>dragTab</c> for this <see cref="ActionComponents.ACTray"/> 
		/// </summary>
		/// <value>The width of the tab.</value>
		/// <remarks>The minimum width is 30 pixels.</remarks>
		public int tabWidth{
			get{ return _tabWidth;}
			set{
				//Save value
				_tabWidth =value;
				
				//Validate
				if (_tabWidth<30) _tabWidth=30;
				
				//Force a redraw
				this.Redraw ();
			}
		}

		/// <summary>
		/// Gets or sets the icon displayed on the <c>dragTab</c> of this <see cref="ActionComponents.ACTray"/> 
		/// </summary>
		/// <value>The icon.</value>
		/// <remarks>The icon will be displayed based on the <c>dragTab</c>'s <see cref="ActionComponents.ACTrayTabType"/>  </remarks>
		public int icon {
			get{ return _icon;}
			set{
				//Save value
				_icon=value;
				
				//Force a redraw
				this.Redraw ();
			}
		}
		
		/// <summary>
		/// Gets or sets the title for the <see cref="ActionComponents.ACTray"/>'s <c>dragTab</c> 
		/// </summary>
		/// <value>The title.</value>
		/// <remarks>This title will be displayed based on the <c>dragTab</c>'s <see cref="ActionComponents.ACTrayTabType"/>  </remarks>
		public string title{
			get{ return _title;}
			set{
				//Save value
				_title=value;
				
				//Force a redraw
				this.Redraw ();
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
		public ACTrayType trayType { get; set; }
		#endregion 

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACTray"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		public ACTray(Context context)
			: base(context)
		{
			//Add an appearance handler to this object
			this.appearance = new ACTrayAppearance ();

			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACTray"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="display">Display.</param>
		public ACTray(Context context, Display display)
			: base(context)
		{
			//Add an appearance handler to this object
			this.appearance = new ACTrayAppearance ();

			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACTray"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attrs">Attrs.</param>
		public ACTray(Context context, IAttributeSet attrs)
			: base(context, attrs)
		{
			//Add an appearance handler to this object
			this.appearance = new ACTrayAppearance ();

			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACTray"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="appearance">Appearance.</param>
		public ACTray(Context context, ACTrayAppearance appearance)
			: base(context)
		{
			//Add an appearance handler to this object
			this.appearance = appearance;
			
			Initialize ();
		}

		/// <summary>
		/// Initialize this instance.
		/// </summary>
		private void Initialize() {
			Color clear=Color.Argb (0,0,0,0);
			
			//Make background clear
			this.SetBackgroundColor (clear);
			this.Clickable=true;
			this.FilterTouchesWhenObscured=false;

			//Set defaults
			this._orientation = ACTrayOrientation.Left;
			this._tabType = ACTrayTabType.GripOnly;
			this._tabLocation = ACTrayTabLocation.Middle;
			this._frameType = ACTrayFrameType.None;
			this.trayType = ACTrayType.Draggable;

			//Handle appearance changes
			this.appearance.AppearanceModified += delegate() {
				//Force a redraw
				this.Redraw ();
			};

			//Register a double tap listener
			_doubleTapListener=new ACTrayGestureListener(this);
			_gestureDector=new GestureDetector(_doubleTapListener);

		}
		#endregion 

		#region Public Methods
		/// <summary>
		/// Forces the <see cref="ActionComponents.ACTray"/> to dump it's draw buffer and completely redraw the control 
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

		/// <summary>
		/// Restores the state of this <see cref="ActionComponents.ACTray"/> after a rotation or restart
		/// </summary>
		/// <param name="value">Value.</param>
		/// <remarks>The value MUST have been generate by the SaveState property of the ActionTray or an error could result</remarks>
		public void RestoreState(string value){

			//Trap all errors
			try{
				//Break into segments
				char[] delimiterChars = {'|'};
				string[] segments = value.Split(delimiterChars);

				//Set the opened state of the tray
				var shouldOpen=bool.Parse (segments[0]);

				//Adjust visible state to match
				if (shouldOpen) {
					OpenTray(false);
				} else {
					CloseTray(false);
				}

				//Restore all of the margins too
				LeftMargin=int.Parse(segments[1]);
				TopMargin=int.Parse(segments[2]);
				RightMargin=int.Parse(segments[3]);
				Bottom=int.Parse(segments[4]);
			}
			catch{
				//For now, just ignore errors
			}
		}

		/// <summary>
		/// Opens the <see cref="ActionComponents.ACTray"/> 
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public void OpenTray(bool animated){

			//Is the tray already open?
			if (isOpened)
				return;
			
			//Recalculate open and closing to be safe
			CalculateOpenAndClosedPositions ();
			
			//Save state
			_opened=true;

			//Automatically bring view to front?
			if (bringToFrontOnTouch) this.BringToFront ();
			
			//Calculate location based on the tray's orientation
			switch (_orientation) {
			case ACTrayOrientation.Left:
				//Animate motion?
				if (animated) {
					_disableInteraction=true;
					var animator = ValueAnimator.OfInt (LeftMargin,_openedPosition);
					animator.SetDuration (500);
					animator.Update += (sender, e) => {
						LeftMargin=(int)e.Animation.AnimatedValue;
					};
					animator.AnimationEnd+= (sender, e) => {
						_disableInteraction=false;
					};
					animator.Start ();
				} else { 
					LeftMargin=_openedPosition;
				}
				break;
			case ACTrayOrientation.Right:
				//Animate motion?
				if (animated) {
					_disableInteraction=true;
					var animator = ValueAnimator.OfInt (RightMargin,_openedPosition);
					animator.SetDuration (500);
					animator.Update += (sender, e) => {
						RightMargin=(int)e.Animation.AnimatedValue;
					};
					animator.AnimationEnd+= (sender, e) => {
						_disableInteraction=false;
					};
					animator.Start ();
				} else { 
					RightMargin=_openedPosition;
				}
				break;
			case ACTrayOrientation.Bottom:
				//Animate motion?
				if (animated) {
					_disableInteraction=true;
					var animator = ValueAnimator.OfInt (BottomMargin,_openedPosition);
					animator.SetDuration (500);
					animator.Update += (sender, e) => {
						BottomMargin=(int)e.Animation.AnimatedValue;
					};
					animator.AnimationEnd+= (sender, e) => {
						_disableInteraction=false;
					};
					animator.Start ();
				} else { 
					BottomMargin=_openedPosition;
				}
				break;
			case ACTrayOrientation.Top:
				//Animate motion?
				if (animated) {
					_disableInteraction=true;
					var animator = ValueAnimator.OfInt (TopMargin,_openedPosition);
					animator.SetDuration (500);
					animator.Update += (sender, e) => {
						TopMargin=(int)e.Animation.AnimatedValue;
					};
					animator.AnimationEnd+= (sender, e) => {
						_disableInteraction=false;
					};
					animator.Start ();
				} else { 
					TopMargin=_openedPosition;
				}
				break;
			}
			
			//Inform caller
			RaiseMoved ();
			RaiseOpened ();

			#if TRIAL
			Android.Widget.Toast.MakeText(this.Context, "ACTray by Appracatappra, LLC.", Android.Widget.ToastLength.Short).Show();
			#endif
		}

		/// <summary>
		/// Closes the <see cref="ActionComponents.ACTray"/> 
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public void CloseTray(bool animated){

			//Is the tray already open?
			if (isClosed)
				return;
			
			//Recalculate open and closing to be safe
			CalculateOpenAndClosedPositions ();
			
			//Save state
			_opened=false;
			
			//Calculate location based on the tray's orientation
			switch (_orientation) {
			case ACTrayOrientation.Left:
				//Animate motion?
				if (animated) {
					_disableInteraction=true;
					var animator = ValueAnimator.OfInt (LeftMargin,_closedPosition);
					animator.SetDuration (500);
					animator.Update += (sender, e) => {
						LeftMargin=(int)e.Animation.AnimatedValue;
					};
					animator.AnimationEnd+= (sender, e) => {
						_disableInteraction=false;
					};
					animator.Start ();
				} else { 
					LeftMargin=_closedPosition;
				}
				break;
			case ACTrayOrientation.Right:
				//Animate motion?
				if (animated) {
					_disableInteraction=true;
					var animator = ValueAnimator.OfInt (RightMargin,_closedPosition);
					animator.SetDuration (500);
					animator.Update += (sender, e) => {
						RightMargin=(int)e.Animation.AnimatedValue;
					};
					animator.AnimationEnd+= (sender, e) => {
						_disableInteraction=false;
					};
					animator.Start ();
				} else { 
					RightMargin=_closedPosition;
				}
				break;
			case ACTrayOrientation.Bottom:
				//Animate motion?
				if (animated) {
					_disableInteraction=true;
					var animator = ValueAnimator.OfInt (BottomMargin,_closedPosition);
					animator.SetDuration (500);
					animator.Update += (sender, e) => {
						BottomMargin=(int)e.Animation.AnimatedValue;
					};
					animator.AnimationEnd+= (sender, e) => {
						_disableInteraction=false;
					};
					animator.Start ();
				} else { 
					BottomMargin=_closedPosition;
				}
				break;
			case ACTrayOrientation.Top:
				//Animate motion?
				if (animated) {
					_disableInteraction=true;
					var animator = ValueAnimator.OfInt (TopMargin,_closedPosition);
					animator.SetDuration (500);
					animator.Update += (sender, e) => {
						TopMargin=(int)e.Animation.AnimatedValue;
					};
					animator.AnimationEnd+= (sender, e) => {
						_disableInteraction=false;
					};
					animator.Start ();
				} else { 
					TopMargin=_closedPosition;
				}
				break;
			}

			//Inform caller
			RaiseMoved ();
			RaiseClosed ();

			#if TRIAL
			Android.Widget.Toast.MakeText(this.Context, "ACTray by Appracatappra, LLC.", Android.Widget.ToastLength.Short).Show();
			#endif
		}

		/// <summary>
		/// Moves the <see cref="ActionComponents.ACTray"/> to the given x,y coordinates 
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public void MoveTo(int x, int y) {

			//Move the tray to the given position
			layoutParams.LeftMargin=x;
			layoutParams.TopMargin=y;

		}
		#endregion 

		#region Private Methods
		/// <summary>
		/// Calculates the open and closed positions for the <see cref="ActionComponents.ACTray"/> 
		/// </summary>
		/// <remarks>This method is called when the <see cref="ActionComponents.ACTrayOrientation"/> changes and when
		/// the device is rotated.</remarks>
		private void CalculateOpenAndClosedPositions(){

			//Take action based on the current orientation
			switch(_orientation){
			case ACTrayOrientation.Left:
			case ACTrayOrientation.Right:
				_openedPosition=0;
				_closedPosition=-(layoutParams.Width-34);
				
				//Does the tray span the full height?
				_hideBodyShadow=(layoutParams.Height!=SuperView.Height);
				break;
			case ACTrayOrientation.Top:
			case ACTrayOrientation.Bottom:
				_openedPosition=0;
				_closedPosition=-(layoutParams.Height-34);
				
				//Does the tray span the full width?
				_hideBodyShadow=(layoutParams.Width!=SuperView.Width);
				break;
			}
		}

		/// <summary>
		/// Calculates the <c>dragTab</c> position.
		/// </summary>
		/// <returns>The tab position.</returns>
		private int CalculateTabposition(){
			int position = 0;
			
			//Take action based on the user specified tab position
			switch (_tabLocation) {
			case ACTrayTabLocation.TopOrLeft:
				position=5;
				break;
			case ACTrayTabLocation.Middle:
				//Take action based on orientation
				switch(_orientation){
				case ACTrayOrientation.Left:
				case ACTrayOrientation.Right:
					position=(this.Height/2)-(_tabWidth/2);
					break;
				case ACTrayOrientation.Bottom:
				case ACTrayOrientation.Top:
					position=(this.Width/2)-(_tabWidth/2);
					break;
				}
				break;
			case ACTrayTabLocation.BottomOrRight:
				//Take action based on orientation
				switch(_orientation){
				case ACTrayOrientation.Left:
				case ACTrayOrientation.Right:
					position=this.Height-(_tabWidth+10);
					break;
				case ACTrayOrientation.Bottom:
				case ACTrayOrientation.Top:
					position=this.Width-(_tabWidth+10);
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
		/// Moves the <see cref="ActionComponents.ACTray"/> to the given x,y coordinates
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <remarks>Movement will be constrained between the <c>openedPosition</c> and <c>closedPosition</c> of the given tray</remarks>
		private void MoveTray(int x, int y) {
			int newX=0;
			int newY=0;

			//Take action based on the orientation
			switch (_orientation) {
			case ACTrayOrientation.Left:
				//Get current values
				newX=LeftMargin;
				newY=TopMargin;

				//Adjust frame location
				newX += x - _previousX;
				
				//Validate
				if (newX<_closedPosition) newX=_closedPosition;
				if (newX>_openedPosition) newX=_openedPosition;

				//Apply new location
				LeftMargin=newX;
				TopMargin=newY;
				break;
			case ACTrayOrientation.Right:
				//Get current values
				newX=RightMargin;
				newY=TopMargin;

				//Adjust frame location
				newX += _previousX - x;
				
				//Validate
				if (newX<_closedPosition) newX=_closedPosition;
				if (newX>_openedPosition) newX=_openedPosition;

				//Apply new location
				RightMargin=newX;
				TopMargin=newY;
				break;
			case ACTrayOrientation.Top:
				//Get current values
				newX=LeftMargin;
				newY=TopMargin;

				//Adjust frame location
				newY += y - _previousY;
				
				//Validate
				if (newY<_closedPosition) newY=_closedPosition;
				if (newY>_openedPosition) newY=_openedPosition;

				//Apply new location
				LeftMargin=newX;
				TopMargin=newY;
				break;
			case ACTrayOrientation.Bottom:
				//Get current values
				newX=LeftMargin;
				newY=BottomMargin;

				//Adjust frame location
				newY += _previousY - y;
				
				//Validate
				if (newY<_closedPosition) newY=_closedPosition;
				if (newY>_openedPosition) newY=_openedPosition;

				//Apply new location
				LeftMargin=newX;
				BottomMargin=newY;
				break;
			}

			//Inform caller
			RaiseMoved ();
			
		}

		/// <summary>
		/// Draws the vertical thumb within the given <see cref="ActionComponents.ACTray"/> 
		/// </summary>
		/// <param name="canvas">Canvas.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		private void DrawVerticalThumb(Canvas canvas, int x, int y) {
			int gx=0, gy=y+4;

			//Draw background
			ShapeDrawable body= new ShapeDrawable(new RectShape());
			body.Paint.Color=appearance.thumbBackground;
			body.Paint.Alpha=appearance.thumbAlpha;
			body.SetBounds (x,y,x+17,y+27);
			body.Draw (canvas);

			//Draw border
			ShapeDrawable bodyBorder= new ShapeDrawable(new RectShape());
			bodyBorder.Paint.Color=appearance.thumbBorder;
			bodyBorder.Paint.SetStyle (Paint.Style.Stroke);
			bodyBorder.Paint.Alpha=appearance.thumbAlpha;
			body.Paint.StrokeWidth=1;
			bodyBorder.SetBounds (x,y,x+17,y+27);
			bodyBorder.Draw (canvas);

			//Draw first grip
			gx=x+4;
			ShapeDrawable grip1= new ShapeDrawable(new RectShape());
			grip1.Paint.Color=appearance.gripBackground;
			//grip1.Paint.Alpha=appearance.thumbAlpha;
			grip1.SetBounds (gx,gy,gx+2,gy+20);
			grip1.Draw (canvas);

			//Draw second grip
			gx=x+8;
			ShapeDrawable grip2= new ShapeDrawable(new RectShape());
			grip2.Paint.Color=appearance.gripBackground;
			//grip2.Paint.Alpha=appearance.thumbAlpha;
			grip2.SetBounds (gx,gy,gx+2,gy+20);
			grip2.Draw (canvas);

			//Draw third grip
			gx=x+12;
			ShapeDrawable grip3= new ShapeDrawable(new RectShape());
			grip3.Paint.Color=appearance.gripBackground;
			//grip3.Paint.Alpha=appearance.thumbAlpha;
			grip3.SetBounds (gx,gy,gx+2,gy+20);
			grip3.Draw (canvas);

		}

		/// <summary>
		/// Draws the horizontal thumb within the given <see cref="ActionComponents.ACTray"/> 
		/// </summary>
		/// <param name="canvas">Canvas.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		private void DrawHorizontalThumb(Canvas canvas, int x, int y) {
			int gx=x+4, gy=0;
			
			//Draw background
			ShapeDrawable body= new ShapeDrawable(new RectShape());
			body.Paint.Color=appearance.thumbBackground;
			body.Paint.Alpha=appearance.thumbAlpha;
			body.SetBounds (x,y,x+27,y+17);
			body.Draw (canvas);
			
			//Draw border
			ShapeDrawable bodyBorder= new ShapeDrawable(new RectShape());
			bodyBorder.Paint.Color=appearance.thumbBorder;
			bodyBorder.Paint.SetStyle (Paint.Style.Stroke);
			bodyBorder.Paint.Alpha=appearance.thumbAlpha;
			body.Paint.StrokeWidth=1;
			bodyBorder.SetBounds (x,y,x+27,y+17);
			bodyBorder.Draw (canvas);
			
			//Draw first grip
			gy=y+4;
			ShapeDrawable grip1= new ShapeDrawable(new RectShape());
			grip1.Paint.Color=appearance.gripBackground;
			//grip1.Paint.Alpha=appearance.thumbAlpha;
			grip1.SetBounds (gx,gy,gx+20,gy+2);
			grip1.Draw (canvas);
			
			//Draw second grip
			gy=y+8;
			ShapeDrawable grip2= new ShapeDrawable(new RectShape());
			grip2.Paint.Color=appearance.gripBackground;
			//grip2.Paint.Alpha=appearance.thumbAlpha;
			grip2.SetBounds (gx,gy,gx+20,gy+2);
			grip2.Draw (canvas);
			
			//Draw third grip
			gy=y+12;
			ShapeDrawable grip3= new ShapeDrawable(new RectShape());
			grip3.Paint.Color=appearance.gripBackground;
			//grip3.Paint.Alpha=appearance.thumbAlpha;
			grip3.SetBounds (gx,gy,gx+20,gy+2);
			grip3.Draw (canvas);
			
		}

		/// <summary>
		/// Draws the vertical title at the given coordinates
		/// </summary>
		/// <param name="canvas">Canvas.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		private void DrawVerticalTitle(Canvas canvas, int x, int y) {

			//Create paint object for title
			Paint pText=new Paint();
			pText.Color=appearance.titleColor;
			pText.TextSize=appearance.titleSize;

			//Calculate bounding area
			Rect r=new Rect();
			pText.GetTextBounds (_title,0,_title.Length,r);

			//Save canvas state
			canvas.Save();

			//Draw rotated text
			canvas.Rotate(-90,0,0);
			//canvas.Translate(-this.Height,0);
			canvas.DrawText (_title,-(y+r.Width()),x+r.Height (),pText);

			//Restore canvas state
			canvas.Restore();
		}

		/// <summary>
		/// Draws the vertical title.
		/// </summary>
		/// <param name="canvas">Canvas.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="title">Title.</param>
		private void DrawVerticalTitle(Canvas canvas, int x, int y, string title) {
			
			//Create paint object for title
			Paint pText=new Paint();
			pText.Color=appearance.titleColor;
			pText.TextSize=appearance.titleSize;
			
			//Calculate bounding area
			Rect r=new Rect();
			pText.GetTextBounds (title,0,title.Length,r);
			
			//Save canvas state
			canvas.Save();
			
			//Draw rotated text
			canvas.Rotate(-90,0,0);
			//canvas.Translate(-this.Height,0);
			canvas.DrawText (title,-(y+r.Width()),x+r.Height (),pText);
			
			//Restore canvas state
			canvas.Restore();
		}

		/// <summary>
		/// Draws the horizontal title at the given location
		/// </summary>
		/// <param name="canvas">Canvas.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		private void DrawHorizontalTitle(Canvas canvas, int x, int y){

			//Create paint object for title
			Paint pText=new Paint();
			pText.Color=appearance.titleColor;
			pText.TextSize=appearance.titleSize;

			//Draw title into canvas
			canvas.DrawText(_title,x,y,pText);
		}

		/// <summary>
		/// Draws the horizontal title.
		/// </summary>
		/// <param name="canvas">Canvas.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="title">Title.</param>
		private void DrawHorizontalTitle(Canvas canvas, int x, int y, string title){
			
			//Create paint object for title
			Paint pText=new Paint();
			pText.Color=appearance.titleColor;
			pText.TextSize=appearance.titleSize;
			
			//Draw title into canvas
			canvas.DrawText(title,x,y,pText);
		}

		/// <summary>
		/// Draws a left side <see cref="ActionComponents.ACTray"/> 
		/// </summary>
		/// <param name="canvas">Canvas.</param>
		private void DrawLeftTray(Canvas canvas){
			bool drawShadow=true;
			int x,y;
			Bitmap bitmap;

			//Draw background
			ShapeDrawable body= new ShapeDrawable(new RectShape());
			body.Paint.Color=appearance.background;
			body.SetBounds (0,0,this.Width-34,this.Height);
			body.Draw (canvas);
			
			//Draw border
			ShapeDrawable bodyBorder= new ShapeDrawable(new RectShape());
			bodyBorder.Paint.Color=appearance.border;
			bodyBorder.Paint.SetStyle (Paint.Style.Stroke);
			body.Paint.StrokeWidth=5;
			bodyBorder.SetBounds (0,0,this.Width-34,this.Height);
			bodyBorder.Draw (canvas);
			
			//Draw shadow
			if (drawShadow) {
				ShapeDrawable shadow= new ShapeDrawable(new RectShape());
				shadow.Paint.Color=appearance.shadow;
				shadow.SetBounds (this.Width-34,0,this.Width-30,this.Height);
				shadow.Draw (canvas);
			}

			//Draw tab body
			x=this.Width-34;
			y=CalculateTabposition();

			//Save content and tab hotspot locations
			_contentArea=new Rect(0,0,this.Width-34,this.Height);
			_thumbHotspot=new Rect(x,y,x+34,y+_tabWidth);

			//Define tab style
			Paint pPointer=new Paint();
			pPointer.SetStyle (Paint.Style.Fill);
			pPointer.AntiAlias=true;
			pPointer.StrokeWidth=1.0f;
			pPointer.Color=appearance.border;
			pPointer.Alpha=appearance.tabAlpha;

			//Custom drawing tab?
			if (_tabType==ACTrayTabType.CustomDrawn) {
				//Request to custom draw tab
				RaiseCustomDrawDragTab(canvas,_thumbHotspot);
			} else {
				//Draw tab body
				Path p=new Path();
				p.LineTo(x,y);
				p.LineTo(x+34,y+10);
				p.LineTo(x+34,y+(tabWidth-10));
				p.LineTo(x,y+tabWidth);
				p.LineTo(x,y);
				p.Close();
				canvas.DrawPath (p,pPointer);
			}

			//Define icon paint
			Paint iPaint=new Paint();
			iPaint.Alpha=appearance.tabAlpha;

			//Take action based on the tab type
			switch(_tabType){
			case ACTrayTabType.Plain:
				//Do nothing
				break;
			case ACTrayTabType.GripOnly:
				DrawVerticalThumb(canvas,x+8,y+((_tabWidth/2)-13));
				break;
			case ACTrayTabType.GripAndTitle:
				DrawVerticalThumb(canvas,x+8,y+15);
				DrawVerticalTitle(canvas,x+8,y+50);
				break;
			case ACTrayTabType.TitleOnly:
				DrawVerticalTitle(canvas,x+8,y+20);
				break;
			case ACTrayTabType.IconOnly:
				//Has an icon been defined?
				if (icon!=0) {
					//Load bitmap
					bitmap=BitmapFactory.DecodeResource(Resources,icon);

					//Draw image
					canvas.DrawBitmap (bitmap,x+(17-(bitmap.Width/2)),y+((_tabWidth/2)-(bitmap.Height/2)),iPaint);
				}
				break;
			case ACTrayTabType.IconAndTitle:
				//Has an icon been defined?
				if (icon!=0) {
					//Load bitmap
					bitmap=BitmapFactory.DecodeResource(Resources,icon);
					
					//Draw image
					canvas.DrawBitmap (bitmap,x+(17-(bitmap.Width/2)),y+15,iPaint);

					//Draw label
					DrawVerticalTitle(canvas,x+8,y+20+bitmap.Height);
				} else {
					DrawVerticalTitle(canvas,x+8,y+20);
				}
				break;
			}
		}

		/// <summary>
		/// Draws the right tray.
		/// </summary>
		/// <param name="canvas">Canvas.</param>
		private void DrawRightTray(Canvas canvas){
			bool drawShadow=true;
			int x,y;
			Bitmap bitmap;
			
			//Draw background
			ShapeDrawable body= new ShapeDrawable(new RectShape());
			body.Paint.Color=appearance.background;
			body.SetBounds (34,0,this.Width,this.Height);
			body.Draw (canvas);
			
			//Draw border
			ShapeDrawable bodyBorder= new ShapeDrawable(new RectShape());
			bodyBorder.Paint.Color=appearance.border;
			bodyBorder.Paint.SetStyle (Paint.Style.Stroke);
			body.Paint.StrokeWidth=5;
			bodyBorder.SetBounds (34,0,this.Width,this.Height);
			bodyBorder.Draw (canvas);
			
			//Draw shadow
			if (drawShadow) {
				ShapeDrawable shadow= new ShapeDrawable(new RectShape());
				shadow.Paint.Color=appearance.shadow;
				shadow.SetBounds (30,0,34,this.Height);
				shadow.Draw (canvas);
			}
			
			//Draw tab body
			x=0;
			y=CalculateTabposition();
			
			//Save content and tab hotspot locations
			_contentArea=new Rect(34,0,this.Width-34,this.Height);
			_thumbHotspot=new Rect(x,y,x+34,y+_tabWidth);

			//Define tab style
			Paint pPointer=new Paint();
			pPointer.SetStyle (Paint.Style.Fill);
			pPointer.AntiAlias=true;
			pPointer.StrokeWidth=1.0f;
			pPointer.Color=appearance.border;
			pPointer.Alpha=appearance.tabAlpha;

			//Custom drawing tab?
			if (_tabType==ACTrayTabType.CustomDrawn) {
				//Request to custom draw tab
				RaiseCustomDrawDragTab(canvas,_thumbHotspot);
			} else {
				//Draw tab body
				Path p=new Path();
				p.LineTo(x,y+10);
				p.LineTo(x+34,y);
				p.LineTo(x+34,y+tabWidth);
				p.LineTo(x,y+(tabWidth-10));
				p.LineTo(x,y+10);
				p.Close();
				canvas.DrawPath (p,pPointer);
			}

			//Define icon paint
			Paint iPaint=new Paint();
			iPaint.Alpha=appearance.tabAlpha;
			
			//Take action based on the tab type
			switch(_tabType){
			case ACTrayTabType.Plain:
				//Do nothing
				break;
			case ACTrayTabType.GripOnly:
				DrawVerticalThumb(canvas,x+8,y+((_tabWidth/2)-13));
				break;
			case ACTrayTabType.GripAndTitle:
				DrawVerticalThumb(canvas,x+8,y+15);
				DrawVerticalTitle(canvas,x+8,y+50);
				break;
			case ACTrayTabType.TitleOnly:
				DrawVerticalTitle(canvas,x+8,y+20);
				break;
			case ACTrayTabType.IconOnly:
				//Has an icon been defined?
				if (icon!=0) {
					//Load bitmap
					bitmap=BitmapFactory.DecodeResource(Resources,icon);
					
					//Draw image
					canvas.DrawBitmap (bitmap,x+(17-(bitmap.Width/2)),y+((_tabWidth/2)-(bitmap.Height/2)),iPaint);
				}
				break;
			case ACTrayTabType.IconAndTitle:
				//Has an icon been defined?
				if (icon!=0) {
					//Load bitmap
					bitmap=BitmapFactory.DecodeResource(Resources,icon);
					
					//Draw image
					canvas.DrawBitmap (bitmap,x+(17-(bitmap.Width/2)),y+15,iPaint);
					
					//Draw label
					DrawVerticalTitle(canvas,x+8,y+20+bitmap.Height);
				} else {
					DrawVerticalTitle(canvas,x+8,y+20);
				}
				break;
			}
		}

		/// <summary>
		/// Draws the top tray.
		/// </summary>
		/// <param name="canvas">Canvas.</param>
		private void DrawTopTray(Canvas canvas){
			bool drawShadow=true;
			int x,y;
			Bitmap bitmap;
			
			//Draw background
			ShapeDrawable body= new ShapeDrawable(new RectShape());
			body.Paint.Color=appearance.background;
			body.SetBounds (0,0,this.Width,this.Height-34);
			body.Draw (canvas);
			
			//Draw border
			ShapeDrawable bodyBorder= new ShapeDrawable(new RectShape());
			bodyBorder.Paint.Color=appearance.border;
			bodyBorder.Paint.SetStyle (Paint.Style.Stroke);
			body.Paint.StrokeWidth=5;
			bodyBorder.SetBounds (0,0,this.Width,this.Height-34);
			bodyBorder.Draw (canvas);
			
			//Draw shadow
			if (drawShadow) {
				ShapeDrawable shadow= new ShapeDrawable(new RectShape());
				shadow.Paint.Color=appearance.shadow;
				shadow.SetBounds (0,this.Height-34,this.Width,this.Height-30);
				shadow.Draw (canvas);
			}
			
			//Draw tab body
			x=CalculateTabposition();
			y=this.Height-34;
			
			//Save content and tab hotspot locations
			_contentArea=new Rect(0,0,this.Width,this.Height-34);
			_thumbHotspot=new Rect(x,y,x+_tabWidth,y+34);

			
			//Define tab style
			Paint pPointer=new Paint();
			pPointer.SetStyle (Paint.Style.Fill);
			pPointer.AntiAlias=true;
			pPointer.StrokeWidth=1.0f;
			pPointer.Color=appearance.border;
			pPointer.Alpha=appearance.tabAlpha;

			//Custom drawing tab?
			if (_tabType==ACTrayTabType.CustomDrawn) {
				//Request to custom draw tab
				RaiseCustomDrawDragTab(canvas,_thumbHotspot);
			} else {
				//Draw tab body
				Path p=new Path();
				p.LineTo(x,y);
				p.LineTo(x+_tabWidth,y);
				p.LineTo(x+(tabWidth-10),y+34);
				p.LineTo(x+10,y+34);
				p.LineTo(x,y);
				p.Close();
				canvas.DrawPath (p,pPointer);
			}

			//Define icon paint
			Paint iPaint=new Paint();
			iPaint.Alpha=appearance.tabAlpha;
			
			//Take action based on the tab type
			switch(_tabType){
			case ACTrayTabType.Plain:
				//Do nothing
				break;
			case ACTrayTabType.GripOnly:
				DrawHorizontalThumb(canvas,x+((_tabWidth/2)-13),y+8);
				break;
			case ACTrayTabType.GripAndTitle:
				DrawHorizontalThumb(canvas,x+15,y+8);
				DrawHorizontalTitle(canvas,x+50,y+18);
				break;
			case ACTrayTabType.TitleOnly:
				DrawHorizontalTitle(canvas,x+20,y+18);
				break;
			case ACTrayTabType.IconOnly:
				//Has an icon been defined?
				if (icon!=0) {
					//Load bitmap
					bitmap=BitmapFactory.DecodeResource(Resources,icon);
					
					//Draw image
					canvas.DrawBitmap (bitmap,x+((_tabWidth/2)-(bitmap.Width/2)),y+(17-(bitmap.Height/2)),iPaint);
				}
				break;
			case ACTrayTabType.IconAndTitle:
				//Has an icon been defined?
				if (icon!=0) {
					//Load bitmap
					bitmap=BitmapFactory.DecodeResource(Resources,icon);
					
					//Draw image
					canvas.DrawBitmap (bitmap,x+15,y+(17-(bitmap.Width/2)),iPaint);
					
					//Draw label
					DrawHorizontalTitle(canvas,x+20+bitmap.Height,y+18);
				} else {
					DrawHorizontalTitle(canvas,x+20,y+18);
				}
				break;
			}
		}

		/// <summary>
		/// Draws the bottom tray.
		/// </summary>
		/// <param name="canvas">Canvas.</param>
		private void DrawBottomTray(Canvas canvas){
			bool drawShadow=true;
			int x,y;
			Bitmap bitmap;
			
			//Draw background
			ShapeDrawable body= new ShapeDrawable(new RectShape());
			body.Paint.Color=appearance.background;
			body.SetBounds (0,34,this.Width,this.Height);
			body.Draw (canvas);
			
			//Draw border
			ShapeDrawable bodyBorder= new ShapeDrawable(new RectShape());
			bodyBorder.Paint.Color=appearance.border;
			bodyBorder.Paint.SetStyle (Paint.Style.Stroke);
			body.Paint.StrokeWidth=5;
			bodyBorder.SetBounds (0,34,this.Width,this.Height);
			bodyBorder.Draw (canvas);
			
			//Draw shadow
			if (drawShadow) {
				ShapeDrawable shadow= new ShapeDrawable(new RectShape());
				shadow.Paint.Color=appearance.shadow;
				shadow.SetBounds (0,30,this.Width,34);
				shadow.Draw (canvas);
			}
			
			//Draw tab body
			x=CalculateTabposition();
			y=0;
			
			//Save content and tab hotspot locations
			_contentArea=new Rect(0,0,this.Width,this.Height-34);
			_thumbHotspot=new Rect(x,y,x+_tabWidth,y+34);

			
			//Define tab style
			Paint pPointer=new Paint();
			pPointer.SetStyle (Paint.Style.Fill);
			pPointer.AntiAlias=true;
			pPointer.StrokeWidth=1.0f;
			pPointer.Color=appearance.border;
			pPointer.Alpha=appearance.tabAlpha;

			//Custom drawing tab?
			if (_tabType==ACTrayTabType.CustomDrawn) {
				//Request to custom draw tab
				RaiseCustomDrawDragTab(canvas,_thumbHotspot);
			} else {
				//Draw tab body
				Path p=new Path();
				p.LineTo(x+10,y);
				p.LineTo(x+(_tabWidth-10),y);
				p.LineTo(x+_tabWidth,y+34);
				p.LineTo(x,y+34);
				p.LineTo(x+10,y);
				p.Close();
				canvas.DrawPath (p,pPointer);
			}

			//Define icon paint
			Paint iPaint=new Paint();
			iPaint.Alpha=appearance.tabAlpha;
			
			//Take action based on the tab type
			switch(_tabType){
			case ACTrayTabType.Plain:
				//Do nothing
				break;
			case ACTrayTabType.GripOnly:
				DrawHorizontalThumb(canvas,x+((_tabWidth/2)-13),y+8);
				break;
			case ACTrayTabType.GripAndTitle:
				DrawHorizontalThumb(canvas,x+15,y+8);
				DrawHorizontalTitle(canvas,x+50,y+18);
				break;
			case ACTrayTabType.TitleOnly:
				DrawHorizontalTitle(canvas,x+20,y+18);
				break;
			case ACTrayTabType.IconOnly:
				//Has an icon been defined?
				if (icon!=0) {
					//Load bitmap
					bitmap=BitmapFactory.DecodeResource(Resources,icon);
					
					//Draw image
					canvas.DrawBitmap (bitmap,x+((_tabWidth/2)-(bitmap.Width/2)),y+(17-(bitmap.Height/2)),iPaint);
				}
				break;
			case ACTrayTabType.IconAndTitle:
				//Has an icon been defined?
				if (icon!=0) {
					//Load bitmap
					bitmap=BitmapFactory.DecodeResource(Resources,icon);
					
					//Draw image
					canvas.DrawBitmap (bitmap,x+15,y+(17-(bitmap.Width/2)),iPaint);
					
					//Draw label
					DrawHorizontalTitle(canvas,x+20+bitmap.Height,y+18);
				} else {
					DrawHorizontalTitle(canvas,x+20,y+18);
				}
				break;
			}
		}

		/// <summary>
		/// Populates the image cache.
		/// </summary>
		/// <returns>The image cache.</returns>
		private Bitmap PopulateImageCache(){
			
			//Create a temporary canvas
			var canvas=new Canvas();
			
			//Create bitmap storage and assign to canvas
			var controlBitmap=Bitmap.CreateBitmap (this.Width,this.Height,Bitmap.Config.Argb8888);
			canvas.SetBitmap (controlBitmap);

			//Take action based on the orientation
			switch (_orientation) {
			case ACTrayOrientation.Left:
				DrawLeftTray(canvas);
				break;
			case ACTrayOrientation.Right:
				DrawRightTray(canvas);
				break;
			case ACTrayOrientation.Top:
				DrawTopTray(canvas);
				break;
			case ACTrayOrientation.Bottom:
				DrawBottomTray(canvas);
				break;
			}
			
			return controlBitmap;
		}
		#endregion 

		#region Internal Methods
		internal void DoubleTapped(int x, int y){

			//Take action based on the tray type
			switch(trayType){
			case ACTrayType.Draggable:
				//Are we inside the thumb area?
				if (_thumbHotspot.Contains(x,y)){
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
					RaiseMoved ();
				}
				break;
			}
		}
		#endregion 

		#region Override Methods
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

		/// <summary>
		/// Raises the touch event event.
		/// </summary>
		/// <param name="e">E.</param>
		public override bool OnTouchEvent (MotionEvent e)
		{

			//Decode x/y position
			int x=(int)e.GetX ();
			int y=(int)e.GetY ();

			//Is interaction enabled?
			if (_disableInteraction) return false;

			//Is this a double tap event?
			if (_gestureDector.OnTouchEvent (e)) return true;

			//Take action based on the type of tab
			switch(trayType){
				case ACTrayType.Popup:
				//Take action based on the event type
				switch(e.Action){
					case MotionEventActions.Down:
					//Are we inside the thumb location?
					if (_thumbHotspot.Contains (x,y)) {
						//Take action based on the tray state
						if (isOpened) {
							CloseTray(true);
						} else {
							OpenTray(true);
						}
						return true;
					} else {
						return false;
					}
				}
				break;
				case ACTrayType.Draggable:
				//Take action based on the event type
				switch(e.Action){
					case MotionEventActions.Down:
					//Are we already dragging?
					if (_dragging) return true;

					//Are we inside the thumb area?
					if (_thumbHotspot.Contains (x,y)) {
						//Mark start of the drag event
						_dragging=true;

						//Save start of drag
						_previousX=x;
						_previousY=y;
						return true;
					} else {
						return false;
					}
					case MotionEventActions.Move:
					//Are we dragging?
					if (_dragging) {
						//Move tray
						MoveTray (x,y);
						return true;
					} else {
						return false;
					}
					case MotionEventActions.Up:
					//Clear any drag action
					_dragging=false;

					//Do we need to redraw the tab?
					if (_hideBodyShadow) {
						//Yes, force a redraw
						this.Invalidate();
					}
					break;
					case MotionEventActions.Cancel:
					//Clear any drag action
					_dragging=false;
					break;
				}

				//Take action based on the tray's orientation
				switch(_orientation){
					case ACTrayOrientation.Left:
					_opened=(LeftMargin!=_closedPosition);
					break;
					case ACTrayOrientation.Right:
					_opened=(RightMargin!=_closedPosition);
					break;
					case ACTrayOrientation.Top:
					_opened=(TopMargin!=_closedPosition);
					break;
					case ACTrayOrientation.Bottom:
					_opened=(BottomMargin!=_closedPosition);
					break;
				}
				break;
				case ACTrayType.AutoClosingPopup:
				//Disable all other interaction on this tray
				_disableInteraction=true;

				//Is the tray closed?
				if (isClosed) {
					//No, are we inside the thumb area?
					if (!_thumbHotspot.Contains (x,y)) return false;

					//Yes, open the tray
					OpenTray (true);
				} else {
					//No, close the tray
					CloseTray(true);
				}
				break;
			}

			//Automatically bring view to front?
			if (bringToFrontOnTouch) this.BringToFront();

			//Inform caller of event
			RaiseTouched ();

			//Inform system that we've handled this event 
			return base.OnTouchEvent (e);
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
				this.Redraw ();
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
				this.Redraw ();
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
		public delegate void CustomDrawDragTabDelegate(ACTray tray, Canvas canvas, Rect rect);
		public event CustomDrawDragTabDelegate CustomDrawDragTab;
		
		/// <summary>
		/// Raises the custom draw drag tab event
		/// </summary>
		/// <param name="canvas">Canvas.</param>
		/// <param name="rect">Rect.</param>
		private void RaiseCustomDrawDragTab(Canvas canvas, Rect rect){
			if (this.CustomDrawDragTab!=null) this.CustomDrawDragTab(this,canvas,rect);
		}
		#endregion
	}
}

