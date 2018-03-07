using System;

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
using System.Threading;

namespace ActionComponents
{
	/// <summary>
	/// Defines a button that can be added to a <see cref="ActionComponents.ACNavBar"/> of a given <see cref="ActionComponents.ACNavBarButtonType"/> and
	/// <see cref="ActionComponents.ACNavBarButtonState"/>. Three <see cref="ActionComponents.ACNavBarButtonAppearance"/> properties controll the look and
	/// feel of the button when it is Enabled, Disabled or Selected. 
	/// </summary>
	/// <remarks><see cref="ActionComponents.ACNavBarButton"/>s cannot be created directly but are built by methods of the <see cref="ActionComponents.ACNavBarButtonCollection"/> 
	/// as <c>AddButton</c>, <c>AddAutoDisposingButton</c>, <c>AddTool</c> or <c>AddNotification</c> </remarks>
	public class ACNavBarButton : View
	{
		#region Private Variable Storage
		private Display _display;
		private ACNavBarButtonState _state;
		private ACNavBarButtonType _type=ACNavBarButtonType.View;
		private View _attachedView;
		private bool _enabled=true;
		private Bitmap _imageCache;
		private bool _debounce=false;
		#endregion

		#region Public Properties
		/// <summary>
		/// [OPTIONAL] tag object that can be assigned to this <see cref="ActionComponents.ACNavBarButton"/> for reference 
		/// </summary>
		public object tag;

		/// <summary>
		/// Controls the appearance of the <see cref="ActionComponents.ACNavBarButton"/> when it is in the <c>Enabled</c> state 
		/// </summary>
		public ACNavBarButtonAppearance appearanceEnabled;

		/// <summary>
		/// Controls the appearance of the <see cref="ActionComponents.ACNavBarButton"/> when it is in the <c>Disabled</c> state 
		/// </summary>
		public ACNavBarButtonAppearance appearanceDisabled;

		/// <summary>
		/// Controls the appearance of the <see cref="ActionComponents.ACNavBarButton"/> when it is in the <c>Selected</c> state 
		/// </summary>
		public ACNavBarButtonAppearance appearanceSelected;
		#endregion

		#region Computed Properties
		/// <summary>
		/// Gets the current state of the button
		/// </summary>
		/// <value>The button's state</value>
		/// <remarks>You cannot set a button's state directly, it's set in response to events in the parent <see cref="ActionComponents.ACNavBar"/> </remarks>
		public ACNavBarButtonState state {
			get { return _state;}
		}
		
		/// <summary>
		/// Returns the type of this <see cref="ActionComponents.ACNavBarButton"/>
		/// </summary>
		/// <value>The <see cref="ActionComponents.ACNavBarButtonType"/> type</value>
		/// <remarks>You cannot set a button's type directly, it's set based on which method of the <see cref="ActionComponents.ACNavBarButtonCollection"/>
		/// was used to create it: <c>AddButton</c>, <c>AddAutoDisposingButton</c>, <c>AddTool</c> or <c>AddNotification</c></remarks>
		public ACNavBarButtonType type {
			get { return _type;}
		}

		/// <summary>
		/// Returns the <see cref="ActionComponents.ACNavBar"/> containing this <see cref="ActionComponents.ACNavBar.ACNavBarButton"/> 
		/// </summary>
		/// <value>The nav bar.</value>
		public ACNavBar NavBar{
			get{
				//Try to return the superview of this component
				try{
					//Return the view housing this control
					return (ACNavBar)this.Parent;
				}
				catch {
					//Return null on failure
					return null;
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the <c>UIView</c>  being controlled by this <see cref="ActionComponents.ACNavBarButton"/>
		/// </summary>
		/// <value>The view.</value>
		/// <remarks>WARNING! This property should ONLY be set in response to a <c>RequestNewView</c> event on this <see cref="ActionComponents.ACNavBarButton"/>. 
		/// Setting the view outside of the event can cause undetermined behavior in the parent <see cref="ActionComponents.ACNavBar"/> and display issues!</remarks>
		public View attachedView{
			get{ return _attachedView;}
			set{ 
				//Save new view
				_attachedView = value;
				
				//Setting to null?
				if (_attachedView!=null) {
					//Take action based on the button type
					switch(_type){
					case ACNavBarButtonType.AutoDisposingView:
						//Add this view to the visible views
						NavBar.SuperView.AddView (_attachedView);
						NavBar.BringToFront ();
						break;
					default:
						//Is this button selected?
						if (_state==ActionComponents.ACNavBarButtonState.Selected) {
							//Make this view visible
							_attachedView.Visibility=ViewStates.Visible;
						} else {
							//Hide this view for now
							_attachedView.Visibility=ViewStates.Gone;
						}
						break;
					}

				}
			}
		}
		
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="ActionComponents.ACNavBarButton"/> is enabled.
		/// </summary>
		/// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
		/// <remarks>WARNING! You cannot disable the currently selected <see cref="ActionComponents.ACNavBarButton"/> </remarks>
		public override bool Enabled {
			get { return _enabled;}
			set {
				//You cannot disable the selected button
				if (!value && _state==ACNavBarButtonState.Selected) {
					Console.WriteLine("WARNING! You cannot disable the currently selected NavBar Button.");
					return;
				}
				
				//Set interaction
				_enabled=value;
				base.Enabled=value;
				
				//Update state and force redisplay
				_state=value ? ACNavBarButtonState.Enabled : ACNavBarButtonState.Disabled;
				this.Invalidate ();
				
				//Inform caller of the state change
				RaiseStateChanged();
			}
		}
		
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="ActionComponents.ACNavBarButton"/> is hidden.
		/// </summary>
		/// <value><c>true</c> if hidden; otherwise, <c>false</c>.</value>
		/// <remarks>WARNING! You cannot hide the currently selected <see cref="ActionComponents.ACNavBarButton"/> </remarks>
		public bool Hidden {
			get { return _state == ACNavBarButtonState.Hidden;}
			set {
				//Take action based on new state
				if (value) {
					//You cannot hide the selected button
					if (_state==ACNavBarButtonState.Selected){
						Console.WriteLine("WARNING! You cannot hide the currently selected NavBar Button.");
						return;
					}
					
					//Hiding
					_state=ACNavBarButtonState.Hidden;
					this.Visibility=ViewStates.Invisible;
				} else {
					//Unhiding
					_state=this._enabled ? ACNavBarButtonState.Enabled : ACNavBarButtonState.Disabled;
					this.Visibility=ViewStates.Visible;
				}
				
				//Force redraw and inform caller of change
				this.Invalidate ();
				RaiseStateChanged();
			}
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
		/// Gets or sets the width of the layout.
		/// </summary>
		/// <value>The width of the layout.</value>
		public int LayoutWidth{
			get{ return layoutParams.Width;}
			set{
				//Adjust width and save back to parent object
				layoutParams.Width = value;
				this.LayoutParameters = layoutParams;
			}
		}

		/// <summary>
		/// Gets or sets the height of the layout.
		/// </summary>
		/// <value>The height of the layout.</value>
		public int LayoutHeight{
			get{ return layoutParams.Height;}
			set{
				//Adjust height and save back to parent object
				layoutParams.Height = value;
				this.LayoutParameters = layoutParams;
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACNavBarButton"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		public ACNavBarButton(Context context)
			: base(context)
		{
			Initialize (true);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACNavBarButton"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="display">Display.</param>
		public ACNavBarButton(Context context, Display display)
			: base(context)
		{
			_display = display;
			Initialize (true);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACNavBarButton"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attrs">Attrs.</param>
		public ACNavBarButton(Context context, IAttributeSet attrs)
			: base(context, attrs)
		{
			Initialize (true);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACNavBarButton"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="image">Image.</param>
		/// <param name="enabled">If set to <c>true</c> enabled.</param>
		/// <param name="hidden">If set to <c>true</c> hidden.</param>
		public ACNavBarButton(Context context, int image, bool enabled, bool hidden)
			: base(context)
		{
			this.Hidden = hidden;

			Initialize (enabled);

			//Set initial image
			this.appearanceEnabled.image = image;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACNavBarButton"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="image">Image.</param>
		/// <param name="enabled">If set to <c>true</c> enabled.</param>
		/// <param name="hidden">If set to <c>true</c> hidden.</param>
		/// <param name="tag">Tag.</param>
		public ACNavBarButton(Context context, int image, bool enabled, bool hidden, object tag)
			: base(context)
		{
			this.Hidden = hidden;
			this.tag = tag;

			Initialize (enabled);

			//Set initial image
			this.appearanceEnabled.image = image;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACNavBarButton"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="appearanceEnabled">Appearance enabled.</param>
		/// <param name="appearanceDisabled">Appearance disabled.</param>
		/// <param name="appearanceSelected">Appearance selected.</param>
		public ACNavBarButton(Context context, ACNavBarButtonAppearance appearanceEnabled, ACNavBarButtonAppearance appearanceDisabled, ACNavBarButtonAppearance appearanceSelected)
			: base(context)
		{
			Initialize (true);

			this.appearanceEnabled = appearanceEnabled;
			this.appearanceEnabled.AppearanceModified+= delegate() {
				Redraw ();
			};
			this.appearanceDisabled = appearanceDisabled;
			this.appearanceDisabled.AppearanceModified+= delegate() {
				Redraw ();
			};
			this.appearanceSelected = appearanceSelected;
			this.appearanceSelected.AppearanceModified+= delegate() {
				Redraw ();
			};
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACNavBarButton"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="type">Type.</param>
		/// <param name="appearanceEnabled">Appearance enabled.</param>
		/// <param name="appearanceDisabled">Appearance disabled.</param>
		/// <param name="appearanceSelected">Appearance selected.</param>
		/// <param name="enabled">If set to <c>true</c> enabled.</param>
		/// <param name="hidden">If set to <c>true</c> hidden.</param>
		/// <param name="tag">Tag.</param>
		public ACNavBarButton(Context context, ACNavBarButtonType type, ACNavBarButtonAppearance appearanceEnabled, ACNavBarButtonAppearance appearanceDisabled, ACNavBarButtonAppearance appearanceSelected, bool enabled, bool hidden, object tag)
			: base(context)
		{
			this._type = type;
			this.Hidden = hidden;
			this.tag = tag;

			Initialize (enabled);
			
			this.appearanceEnabled = appearanceEnabled;
			this.appearanceEnabled.AppearanceModified+= delegate() {
				Redraw ();
			};
			this.appearanceDisabled = appearanceDisabled;
			this.appearanceDisabled.AppearanceModified+= delegate() {
				Redraw ();
			};
			this.appearanceSelected = appearanceSelected;
			this.appearanceSelected.AppearanceModified+= delegate() {
				Redraw ();
			};
		}

		/// <summary>
		/// Initialize the object and sets its enabled state
		/// </summary>
		/// <param name="isEnabled">If set to <c>true</c> is enabled.</param>
		private void Initialize(bool isEnabled) {

			//Define clear color
			Color clear=Color.Argb (0,0,0,0);

			//Make the button itself clear
			this.SetBackgroundColor (clear);
			this.LayoutParameters = new RelativeLayout.LayoutParams (58, 40);
			this.SetMinimumHeight(40);
			this.SetMinimumWidth (58);
			this.Clickable=true;
			this.FilterTouchesWhenObscured=false;
			this.SoundEffectsEnabled=true;

			//Enable component
			_enabled=isEnabled;
			_state=_enabled ? ACNavBarButtonState.Enabled : ACNavBarButtonState.Disabled;
			
			//Set default appearances and wire-ups
			appearanceEnabled = new ACNavBarButtonAppearance (clear, clear, 0, 1.0f);
			appearanceEnabled.AppearanceModified+= delegate() {
				//Force a redraw
				Redraw ();
			};
			
			appearanceDisabled = new ACNavBarButtonAppearance (clear, clear, 0, 0.5f);
			appearanceDisabled.AppearanceModified+= delegate() {
				//Force a redraw
				Redraw ();
			};
			
			appearanceSelected = new ACNavBarButtonAppearance (clear, clear, 0, 1.0f);
			appearanceSelected.AppearanceModified+= delegate() {
				//Force a redraw
				Redraw ();
			};

			//Wireup touch handler
			this.Touch+= (sender, e) => {

				//Is the button enabled?
				if (!Enabled) return;

				//Is debounce in effect?
				if (_debounce) return;

				//Start debouncing routine
				_debounce=true;
				ThreadPool.QueueUserWorkItem((callback) =>{
					//Wait before clearing the debounce cycle
					Thread.Sleep(500);
					_debounce=false;
				});

				//Inform caller that we've been touched
				RaiseTouched ();
			};

		}
		#endregion 

		#region Public Methods
		/// <summary>
		/// Causes the <see cref="ActionComponents.ACNavBarButton"/> to redraw it's contents totally by dumping it's image cache
		/// and doing a total refresh
		/// </summary>
		/// <remarks>WARNING! This routine should be called sparingly as it has a performance hit</remarks>
		public void Redraw(){

			//Clear any existing image cache
			if (_imageCache!=null) {
				_imageCache.Dispose ();
				_imageCache=null;
			}

			//Force a redraw
			this.Invalidate ();

		}
		#endregion 

		#region Internal Methods
		/// <summary>
		/// Moves button view to the given location
		/// </summary>
		/// <param name="x">
		/// The new x position
		/// </param>
		/// <param name="y">
		/// The new y position
		/// </param>
		internal void MoveTo(float x, float y){
			//Move into the new position
			LeftMargin = (int)x;
			TopMargin = (int)y;
		}

		/// <summary>
		/// Sets the state of the button.
		/// </summary>
		/// <param name="state">The new state for the button</param>
		internal void SetButtonState(ACNavBarButtonState state){

			//Save value and force an update of the control's appearance
			_state=state;
			this.Invalidate ();

			if (_attachedView!=null) {
				//Is this button selected?
				if (_state==ActionComponents.ACNavBarButtonState.Selected) {
					//Make this view visible
					_attachedView.Visibility=ViewStates.Visible;
				} else {
					//Hide this view for now
					_attachedView.Visibility=ViewStates.Gone;
				}
			}

			//Inform caller that the state has changed
			RaiseStateChanged ();
		}

		/// <summary>
		/// Buttons the unselected.
		/// </summary>
		internal void ButtonUnselected(){
			
			//Take action based on the type of button
			switch (_type) {
			case ACNavBarButtonType.View:
				//If a view is attached to this button hide it
				if (_attachedView != null) {
					//Hide this view for now
					_attachedView.Visibility=ViewStates.Gone;
				}
				
				//Inform caller that the view has been hidden
				RaiseViewHidden();
				break;
			case ACNavBarButtonType.AutoDisposingView:
				//Is a view attached to this button?
				if (_attachedView!=null) {
					//Hide this view for now
					//_attachedView.Visibility=ViewStates.Gone;

					//Remove the view from the parent
					NavBar.SuperView.RemoveView (_attachedView);

					//Release view from memory
					_attachedView.Dispose();

					//Release reference to view
					_attachedView=null;

					//Inform caller that the view was released
					RaiseViewDisposed();
				}
				
				//Inform caller that the view has been disposed of
				RaiseViewDisposed ();
				break;
			}
		}
		#endregion 

		#region Private Methods
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
			
			//Grab the default states
			Color background = appearanceEnabled.background;
			Color border = appearanceEnabled.border;
			int image = appearanceEnabled.image;
			
			//Take action based on the button's state
			switch (_state) {
			case ACNavBarButtonState.Hidden:
				//Nothing to do abort drawing
				return controlBitmap;
			case ACNavBarButtonState.Disabled:
				//Switch properties
				if (appearanceDisabled.background.A!=0) background=appearanceDisabled.background;
				if (appearanceDisabled.border.A!=0) border=appearanceDisabled.border;
				if (appearanceDisabled.image!=0) image=appearanceDisabled.image;
				break;
			case ACNavBarButtonState.Selected:
				//Switch properties
				if (appearanceSelected.background.A!=0) background=appearanceSelected.background;
				if (appearanceSelected.border.A!=0) border=appearanceSelected.border;
				if (appearanceSelected.image!=0) image=appearanceSelected.image;
				break;
			}
			
			//Draw background
			ShapeDrawable body= new ShapeDrawable(new RectShape());
			body.Paint.Color=background;
			body.SetBounds (0,0,this.Width,this.Height);
			body.Draw (canvas);
			
			//Draw border
			ShapeDrawable bodyBorder= new ShapeDrawable(new RectShape());
			bodyBorder.Paint.Color=border;
			bodyBorder.Paint.SetStyle (Paint.Style.Stroke);
			bodyBorder.SetBounds (0,0,this.Width,this.Height);
			bodyBorder.Draw (canvas);
			
			//Draw Image
			if (image!=0) {
				//Load image bitmap from resources
				Bitmap bitmap=BitmapFactory.DecodeResource(Resources,image);
				
				var h=bitmap.Height;
				if (h>40) h=40;
				
				var w=bitmap.Width;
				if (w>40) w=40;
				
				var l=((this.Width/2)-(w/2));
				
				var t=((this.Height/2)-(h/2));
				
				//Draw bitmap into canvas
				canvas.DrawBitmap (bitmap,null,new Rect(l,t,l+w,t+h),null);
			}
			
			return controlBitmap;
		}
		#endregion 

		#region Overrides
		/// <summary>
		/// Raises the measure event.
		/// </summary>
		/// <param name="widthMeasureSpec">Width measure spec.</param>
		/// <param name="heightMeasureSpec">Height measure spec.</param>
		protected override void OnMeasure (int widthMeasureSpec, int heightMeasureSpec)
		{
			//Return the fixed height and width of this view
			SetMeasuredDimension (58,40);

		}

		/// <summary>
		/// Raises the draw event.
		/// </summary>
		/// <param name="canvas">Canvas.</param>
		protected override void OnDraw (Canvas canvas)
		{
			base.OnDraw (canvas);

			//Adjust alpha based on the button's state
			switch (_state) {
			case ACNavBarButtonState.Hidden:
				//Nothing to process
				return;
			case ACNavBarButtonState.Enabled:
				this.Alpha = appearanceEnabled.alpha;
				break;
			case ACNavBarButtonState.Disabled:
				this.Alpha=appearanceDisabled.alpha;
				break;
			case ACNavBarButtonState.Selected:
				this.Alpha=appearanceSelected.alpha;
				break;
			}

			//Restoring image from cache?
			if (_imageCache==null) _imageCache=PopulateImageCache();
			
			//Draw cached image to canvas
			canvas.DrawBitmap (_imageCache,0,0,null);

		}
		#endregion 

		#region Events
		/// <summary>
		/// Occurs when touched.
		/// </summary>
		public delegate void TouchedDelegate(ACNavBarButton responder);
		/// <summary>
		/// Occurs when touched.
		/// </summary>
		public event TouchedDelegate Touched;

		/// <summary>
		/// Raises the touched event
		/// </summary>
		internal void RaiseTouched(){
			
			//Take action based on the type of NavBarButton this is
			switch (_type) {
			case ACNavBarButtonType.AutoDisposingView:
			case ACNavBarButtonType.View:
				//Does this button already have a view attached?
				if (_attachedView==null){
					//No, request that the caller builds a view for us
					RaiseRequestNewView();
				} else {
					//Make the current view visible
					_attachedView.Visibility=ViewStates.Visible;
				}

				#if TRIAL
				Android.Widget.Toast.MakeText(this.Context, "NavBar by Appracatappra", Android.Widget.ToastLength.Short).Show();
#else
					AppracatappraLicenseManager.ValidateLicense(this.Context);
#endif
					break;
			case ACNavBarButtonType.Tool:
				//No view adjustment required
				break;
			case ACNavBarButtonType.Notification:
				//Notifications show status information only and are not
				//allowed to be touchable
				return;
			}
			
			//Inform caller of touch
			if (this.Touched !=null) this.Touched(this);
		}

		/// <summary>
		/// Occurs when the <see cref="ActionComponents.ACNavBarButton"/> is of <see cref="ActionComponents.ACNavBarButtonType"/>
		/// View or AutoDisposingView, the button has been selected and no view is attached.
		/// </summary>
		/// <remarks>When responding to this request create a new <c>UIView</c> and attach it to the button's view property</remarks>
		public delegate void RequestNewViewDelegate(ACNavBarButton responder);
		/// <summary>
		/// Occurs when the <see cref="ActionComponents.ACNavBarButton"/> is of <see cref="ActionComponents.ACNavBarButtonType"/>
		/// View or AutoDisposingView, the button has been selected and no view is attached.
		/// </summary>
		/// <remarks>When responding to this request create a new <c>UIView</c> and attach it to the button's view property</remarks>
		public event RequestNewViewDelegate RequestNewView;
		
		/// <summary>
		/// Raises the request new view event
		/// </summary>
		internal void RaiseRequestNewView(){
			//Inform caller that we need them to create a view for us
			if (this.RequestNewView != null)
				this.RequestNewView (this);
		}
		
		/// <summary>
		/// Occurs when this <see cref="ActionComponents.ACNavBarButton"/> is of <see cref="ActionComponents.ACNavBarButtonType"/> View
		/// and the button has been unselected and the view under its control has been hidden
		/// </summary>
		public delegate void ViewHiddenDelegate (ACNavBarButton responder);
		/// <summary>
		/// Occurs when this <see cref="ActionComponents.ACNavBarButton"/> is of <see cref="ActionComponents.ACNavBarButtonType"/> View
		/// and the button has been unselected and the view under its control has been hidden
		/// </summary>
		public event ViewHiddenDelegate ViewHidden;
		
		/// <summary>
		/// Raises the view hidden event
		/// </summary>
		internal void RaiseViewHidden(){
			//Inform caller that the view attached to this button has been hidden
			if (this.ViewHidden != null)
				this.ViewHidden (this);
		}
		
		/// <summary>
		/// Occurs when this <see cref="ActionComponents.ACNavBarButton"/> is of <see cref="ActionComponents.ACNavBarButtonType"/> AutoDisposingView
		/// and the button has been unselected and the view under its control has been removed from memory
		/// </summary>
		public delegate void ViewDisposedDelegate (ACNavBarButton responder);
		/// <summary>
		/// Occurs when this <see cref="ActionComponents.ACNavBarButton"/> is of <see cref="ActionComponents.ACNavBarButtonType"/> AutoDisposingView
		/// and the button has been unselected and the view under its control has been removed from memory
		/// </summary>
		public event ViewDisposedDelegate ViewDisposed;
		
		/// <summary>
		/// Raises the view disposed event
		/// </summary>
		internal void RaiseViewDisposed(){
			//Inform caller that the view attached to this button has been
			//removed from memory
			if (this.ViewDisposed != null)
				this.ViewDisposed (this);
		}
		
		/// <summary>
		/// Occurs when state changed.
		/// </summary>
		internal delegate void StateChangedDelegate(ACNavBarButton responder);
		/// <summary>
		/// Occurs when state changed.
		/// </summary>
		internal event StateChangedDelegate StateChanged;
		
		/// <summary>
		/// Raises the state changed event
		/// </summary>
		private void RaiseStateChanged(){
			//Inform caller of the state change
			if (this.StateChanged != null)
				this.StateChanged (this);
		}
		#endregion 
	}
}

