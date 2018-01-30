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
	/// A left-side, icon based, customizable navigation strip and view controller
	/// </summary>
	/// <remarks>The <see cref="ActionComponents.ACNavBar"/> has three <see cref="ActionComponents.ACNavBarButtonCollection"/>s (top, middle and bottom) that
	/// <see cref="ActionComponents.ACNavBarButton"/>s can be added to. Several different <see cref="ActionComponents.ACNavBarButtonType"/>s can be created that provide
	/// automatic control of attached <c>View</c>s to simple touchable buttons to notification icons. The <see cref="ActionComponents.ACNavBar"/> appearance
	/// can be adjusted using <see cref="ActionComponents.ACNavBarAppearance"/> and <see cref="ActionComponents.ACNavBarButtonAppearance"/> properties. </remarks>
	public partial class ACNavBar : RelativeLayout 
	{
		#region Private Variables
		private Display _display;
		private ACNavBarPointer _pointer;
		private ACNavBarButtonCollection _top;
		private ACNavBarButtonCollection _middle;
		private ACNavBarButtonCollection _bottom;
		private ACNavBarButtonAppearance _buttonAppearanceEnabled;
		private ACNavBarButtonAppearance _buttonAppearanceDisabled;
		private ACNavBarButtonAppearance _buttonAppearanceSelected;
		private Bitmap _imageCache;
		private int _rehydrationId=0;
		private bool _hidden=false;
		private int _lastHeight = -1;
		#endregion 

		#region Public Properties
		/// <summary>
		/// [OPTIONAL] Tag to hold user information about this collection
		/// </summary>
		public object tag;
		#endregion 

		#region Computed Properties
		/// <summary>
		/// Gets or sets the rehydration identifier used to restore the selected <see cref="ActionComponents.ACNavBarButton"/> after a state change such as rotation. 
		/// </summary>
		/// <value>The rehydration identifier.</value>
		/// <remarks>Call the <see cref="ActionComponents.ACNavBar.SelectedButtonID"/> method in the <c>OnSaveInstanceState</c> method of your <c>Action</c>
		/// to get the value to set this property to.</remarks>
		public int rehydrationId{
			get{return _rehydrationId;}
			set{
				//Save value
				_rehydrationId=value;
			
				//Pass value to all collections
				_top.rehydrationId=value;
				_middle.rehydrationId=value;
				_bottom.rehydrationId=value;
			}
		}

		/// <summary>
		/// Returns the <c>SuperView</c> (parent view) of this <see cref="ActionComponents.ACNavBar"/> 
		/// </summary>
		/// <value>The containing <c>View</c> or <c>null</c> on error</value>
		public RelativeLayout SuperView{
			get{
				//Try to return the superview of this component
				try{
					//Return the view housing this control
					return (RelativeLayout)this.Parent;
				}
				catch {
					//Return null on failure
					return null;
				}
			}
		}

		/// <summary>
		/// Gets or sets the button appearance enabled values
		/// </summary>
		/// <value>The button appearance enabled values</value>
		/// <remarks>Any changes to this appearance will cascade to all <see cref="ActionComponents.ACNavBarButtonCollection"/>s and 
		/// all <see cref="ActionComponents.ACNavBarButton"/>s in those collections.</remarks>
		public ACNavBarButtonAppearance buttonAppearanceEnabled {
			get{ return _buttonAppearanceEnabled;}
			set{
				//Save value
				_buttonAppearanceEnabled=value;
				
				//Wireup modification handler
				_buttonAppearanceEnabled.AppearanceModified+=delegate() {
					//Cascade the appearance modification
					CascadeAppearance (_buttonAppearanceEnabled,_top,ACNavBarButtonState.Enabled);
					CascadeAppearance (_buttonAppearanceEnabled,_middle,ACNavBarButtonState.Enabled);
					CascadeAppearance (_buttonAppearanceEnabled,_bottom,ACNavBarButtonState.Enabled);
				};
				
				//Force appearance to cascade
				_buttonAppearanceEnabled.RaiseAppearanceModified();
			}
		}
		
		/// <summary>
		/// Gets or sets the button appearance disabled values
		/// </summary>
		/// <value>The button appearance disabled values</value>
		/// <remarks>Any changes to this appearance will cascade to all <see cref="ActionComponents.ACNavBarButtonCollection"/>s and 
		/// all <see cref="ActionComponents.ACNavBarButton"/>s in those collections.</remarks>
		public ACNavBarButtonAppearance buttonAppearanceDisabled {
			get{ return _buttonAppearanceDisabled;}
			set{
				//Save value
				_buttonAppearanceDisabled=value;
				
				//Wireup modification handler
				_buttonAppearanceDisabled.AppearanceModified+=delegate() {
					//Cascade the appearance modification
					CascadeAppearance (_buttonAppearanceDisabled,_top,ACNavBarButtonState.Disabled);
					CascadeAppearance (_buttonAppearanceDisabled,_middle,ACNavBarButtonState.Disabled);
					CascadeAppearance (_buttonAppearanceDisabled,_bottom,ACNavBarButtonState.Disabled);
				};
				
				//Force appearance to casecade
				_buttonAppearanceDisabled.RaiseAppearanceModified ();
			}
		}
		
		/// <summary>
		/// Gets or sets the button appearance selected values
		/// </summary>
		/// <value>The button appearance selected values</value>
		/// <remarks>Any changes to this appearance will cascade to all <see cref="ActionComponents.ACNavBarButtonCollection"/>s and 
		/// all <see cref="ActionComponents.ACNavBarButton"/>s in those collections.</remarks>
		public ACNavBarButtonAppearance buttonAppearanceSelected{
			get{ return _buttonAppearanceSelected;}
			set{
				//Save vlaue
				_buttonAppearanceSelected=value;
				
				//Wireup modification handler
				_buttonAppearanceSelected.AppearanceModified+=delegate() {
					//Cascade the appearance modification
					CascadeAppearance (_buttonAppearanceSelected,_top,ACNavBarButtonState.Selected);
					CascadeAppearance (_buttonAppearanceSelected,_middle,ACNavBarButtonState.Selected);
					CascadeAppearance (_buttonAppearanceSelected,_bottom,ACNavBarButtonState.Selected);
				};
				
				//Force appearance to casecade
				_buttonAppearanceSelected.RaiseAppearanceModified ();
			}
		}
		/// <summary>
		/// Controlls the general appearance of the control
		/// </summary>
		public ACNavBarAppearance appearance { get; set;}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="ActionComponents.ACNavBar"/> pointer is hidden.
		/// </summary>
		/// <value><c>true</c> if pointer hidden; otherwise, <c>false</c>.</value>
		public bool pointerHidden {
			get { return (_pointer.Visibility==ViewStates.Invisible);}
			set { _pointer.Visibility= value ? ViewStates.Invisible : ViewStates.Visible;}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="ActionComponents.ACNavBar"/> is hidden.
		/// </summary>
		/// <value><c>true</c> if hidden; otherwise, <c>false</c>.</value>
		/// <remarks>The <see cref="ActionComponents.ACNavBar"/> will slide on and off the left edge of the screen when shown or hidden. </remarks>
		public bool Hidden{
			get {return _hidden;}
			set {
				//Did the value change?
				if (value==_hidden) return;

				//Save value
				_hidden=value;

				//Take action based on requested state
				if (value) {
					var animator = ValueAnimator.OfFloat(0,-85);
					animator.SetDuration (500);
					animator.Update += (sender, e) => {
						RelativeLayout.LayoutParams lp=new RelativeLayout.LayoutParams(this.LayoutParameters);
						lp.LeftMargin=(int)e.Animation.AnimatedValue;
						this.LayoutParameters=lp;
					};
					animator.Start ();
				} else {
					var animator = ValueAnimator.OfFloat(-85,0);
					animator.SetDuration (500);
					animator.Update += (sender, e) => {
						RelativeLayout.LayoutParams lp=new RelativeLayout.LayoutParams(this.LayoutParameters);
						lp.LeftMargin=(int)e.Animation.AnimatedValue;
						this.LayoutParameters=lp;
					};
					animator.Start ();
				}
			}
		}

		/// <summary>
		/// Gets the top <see cref="ActionComponents.ACNavBarButtonCollection"/> of <see cref="ActionComponents.ACNavBarButton"/>s
		/// </summary>
		/// <value>The top collection</value>
		/// <remarks>This is the master <see cref="ActionComponents.ACNavBarButtonCollection"/>. The first button added to this collection will
		/// automatically be the selected button. Its <c>UIView</c> will be displayed and the <see cref="ActionComponents.ACNavBarPointer"/> will be
		/// moved into position beside this button. NOTE: You need to call the <c>DisplayDefaultView</c> method of the <see cref="ActionComponents.ACNavBar"/> to cause the 
		/// first view to be correctly displayed after the NavBar has been populated with buttons.</remarks>
		public ACNavBarButtonCollection top{
			get{ return _top;}
		}
		
		/// <summary>
		/// Gets the middle <see cref="ActionComponents.ACNavBarButtonCollection"/> of <see cref="ActionComponents.ACNavBarButton"/>s 
		/// </summary>
		/// <value>The middle collection</value>
		/// <remarks>The middle <see cref="ActionComponents.ACNavBarButtonCollection"/> is usually reserved for <c>Tool</c> <see cref="ActionComponents.ACNavBarButtonType"/>s of
		/// buttons that act on the currently selected <c>UIView</c> </remarks>
		public ACNavBarButtonCollection middle{
			get{ return _middle;}
		}
		
		/// <summary>
		/// Gets the bottom <see cref="ActionComponents.ACNavBarButtonCollection"/> of <see cref="ActionComponents.ACNavBarButton"/>s 
		/// </summary>
		/// <value>The bottom collection</value>
		/// <remarks>The bottom <see cref="ActionComponents.ACNavBarButtonCollection"/> is usually reserved for <c>Settings</c> and <c>Notification</c>
		/// <see cref="ActionComponents.ACNavBarButtonType"/>s of buttons.</remarks>
		public ACNavBarButtonCollection bottom{
			get{ return _bottom;}
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
			get{return ACView.GetViewLeftMargin (this);}
			set{ACView.SetViewLeftMargin (this, value);}
		}

		/// <summary>
		/// Gets or sets the top margin.
		/// </summary>
		/// <value>The top margin.</value>
		public int TopMargin{
			get{return ACView.GetViewTopMargin (this);}
			set{ACView.SetViewTopMargin (this, value);}
		}

		/// <summary>
		/// Gets or sets the right margin.
		/// </summary>
		/// <value>The right margin.</value>
		public int RightMargin{
			get{return ACView.GetViewRightMargin (this);}
			set{ACView.SetViewRightMargin (this, value);}
		}

		/// <summary>
		/// Gets or sets the bottom margin.
		/// </summary>
		/// <value>The bottom margin.</value>
		public int BottomMargin{
			get{return ACView.GetViewBottomMargin (this);}
			set{ACView.SetViewBottomMargin (this, value);}
		}

		/// <summary>
		/// Gets or sets the width of the layout.
		/// </summary>
		/// <value>The width of the layout.</value>
		public int LayoutWidth{
			get{ return ACView.GetViewWidth (this);}
			set{ACView.SetViewWidth (this, value);}
		}

		/// <summary>
		/// Gets or sets the height of the layout.
		/// </summary>
		/// <value>The height of the layout.</value>
		public int LayoutHeight{
			get{ return ACView.GetViewHeight (this);}
			set{ACView.SetViewHeight (this, value);}
		}
		#endregion 

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACNavBar"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		public ACNavBar(Context context)
			: base(context)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACNavBar"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="display">Display.</param>
		public ACNavBar(Context context, Display display)
			: base(context)
		{
			_display = display;
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACNavBar"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attrs">Attrs.</param>
		public ACNavBar(Context context, IAttributeSet attrs)
			: base(context, attrs)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACNavBar"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="appearance">Appearance.</param>
		public ACNavBar(Context context, ACNavBarAppearance appearance)
			: base(context)
		{
			Initialize ();

			this.appearance=appearance;
			appearance.AppearanceModified+= () => {
				//Force component to redraw
				Redraw ();
			};
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

			//Setup appearance
			appearance=new ACNavBarAppearance();
			appearance.AppearanceModified+= () => {
				//Force component to redraw
				Redraw ();
			};

			//Create a pointer and attach it
			_pointer=new ACNavBarPointer(this.Context,this.appearance);
			_pointer.Id=1;
			this.AddView(_pointer);

			//Create the default appearances
			this.buttonAppearanceEnabled = new ACNavBarButtonAppearance (clear, clear, 0, 1.0f);
			this.buttonAppearanceDisabled = new ACNavBarButtonAppearance (clear, clear, 0, 0.5f);
			this.buttonAppearanceSelected = new ACNavBarButtonAppearance (clear, clear, 0, 1.0f);

			//Create top button collection and wireup events
			_top = new ACNavBarButtonCollection (this, ACNavBarButtonCollectionLocation.Top, true, buttonAppearanceEnabled, buttonAppearanceDisabled, buttonAppearanceSelected);
			_top.NewPointerPosition+= delegate(float y, bool animated) {
				//Unselect from all other collections
				_middle.UnselectAllButtons();
				_bottom.UnselectAllButtons();
				
				//Move the pointer
				MovePointer (y, animated);
			};
			_top.CollectionModified+= delegate() {
				//TODO: Add handler for the top collection being modified
			};
			_top.Id=2;
			
			//Create middle button collection and wireup events
			_middle = new ACNavBarButtonCollection (this, ACNavBarButtonCollectionLocation.Middle, false, buttonAppearanceEnabled, buttonAppearanceDisabled, buttonAppearanceSelected);
			_middle.NewPointerPosition+= delegate(float y, bool animated) {
				//Unselect from all other collections
				_top.UnselectAllButtons();
				_bottom.UnselectAllButtons();
				
				//Move the pointer
				MovePointer (y, animated);
			};
			_middle.CollectionModified+= delegate() {
				//TODO: Add handler for the middle collection being modified
			};
			_middle.Id=3;
			
			//Create bottom button collection and wireup events
			_bottom = new ACNavBarButtonCollection (this, ACNavBarButtonCollectionLocation.Bottom, false, buttonAppearanceEnabled, buttonAppearanceDisabled, buttonAppearanceSelected);
			_bottom.NewPointerPosition+= delegate(float y, bool animated) {
				//Unselect from all other collections
				_top.UnselectAllButtons();
				_middle.UnselectAllButtons();
				
				//Move the pointer
				MovePointer (y, animated);
			};
			_bottom.CollectionModified+= delegate() {
				//TODO: Add handler for the bottom collection being modified
			};
			_bottom.Id=4;

		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Returns the currently selected button in this <see cref="ActionComponents.ACNavBar"/>
		/// </summary>
		/// <returns>Returns the <see cref="ActionComponents.ACNavBarButton"/> selected or <c>null</c> if no button is selected</returns>
		public ACNavBarButton SelectedButton(){
			ACNavBarButton button = null;
			
			//Is a button selected in the top group
			button = _top.SelectedButton ();
			if (button != null)
				return button;
			
			//Is a button selected in the middle group
			button = _middle.SelectedButton ();
			if (button != null)
				return button;
			
			//Is a button selected in the bottom group
			button = _bottom.SelectedButton ();
			
			//Return remaining button state
			return button;
		}

		/// <summary>
		/// Returns the Unique Identifier for the currently selected <see cref="ActionComponents.ACNavBarButton"/> or zero if no
		/// button is selected.
		/// </summary>
		/// <returns>The button identifier for the selected button or <c>0</c> if no button is selected </returns>
		/// <remarks>Use this routine to save the state of the NavBar before an activity like screen rotation</remarks>
		public int SelectedButtonId(){

			//Find the selected button
			ACNavBarButton selected=SelectedButton ();

			//Found?
			if (selected==null) return 0;

			//Found, return the button's ID
			return selected.Id;
		}

		/// <summary>
		/// Select the given <see cref="ActionComponents.ACNavBarButton"/> by the given <c>Id</c> 
		/// </summary>
		/// <param name="Id">Identifier.</param>
		/// <remarks>Use this routine to restore the NavBar's state after an activity like screen rotation</remarks>
		public void SelecteButtonByID(int Id){

			//Anything to process?
			if (Id==0) return;

			//Is the button in the top collection
			if (_top.SelectButtonById (Id)) return;

			//Is the button in the middle collection
			if (_middle.SelectButtonById (Id)) return;

			//Is the button in the bottom collection
			if (_bottom.SelectButtonById (Id)) return;
		}

		/// <summary>
		/// Causes the selected <see cref="ActionComponents.ACNavBarButton"/> in the any <see cref="ActionComponents.ACNavBarButtonCollection"/>
		/// to be displayed and raises the buttons <c>Touched</c> event
		/// </summary>
		/// <remarks>This routine <c>MUST</c> be called in the <c>OnStart</c> method of your <c>Action</c> to properly display <c>View</c> for the currently selected
		/// <see cref="ActionComponents.ACNavBarButton"/> </remarks>
		public void DisplayDefaultView(){
			
			//Find the currently selected button
			ACNavBarButton selected=SelectedButton ();

			//Found?
			if (selected==null) return;
		
			//Perform movement
			HydrateViewAndPointer(selected);
		}

		/// <summary>
		/// Redraw this instance.
		/// </summary>
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

		#region Private Methods
		/// <summary>
		/// Hydrates the view and moves the pointer to the selected <see cref="ActionComponents.ACNavBarButton"/> 
		/// </summary>
		/// <param name="selected">Selected.</param>
		private void HydrateViewAndPointer(ACNavBarButton selected){
			//Force the default view to be displayed
			selected.RaiseTouched();
			
			//Reposition the pointer so that it is beside the selected view
			if (_top.RepositionPointer()) return;
			if (_middle.RepositionPointer()) return;
			if (_bottom.RepositionPointer()) return;
		}

		/// <summary>
		/// Moves the pointer.
		/// </summary>
		/// <param name="y">The y coordinate.</param>
		/// <param name="animated">If <c>true</c> the move will be animated</param> 
		private void MovePointer(float y, bool animated){
			
			//Adjust location
			_pointer.MoveTo(y, animated);

			#if TRIAL
			Android.Widget.Toast.MakeText(this.Context, "AVNavBar by Appracatappra, LLC.", Android.Widget.ToastLength.Short).Show();
			#endif

		}

		/// <summary>
		/// Cascades changes to the given <see cref="ActionComponents.ACNavBarButtonAppearance"/> to the specified
		/// <see cref="ActionComponents.ACNavBarButtonCollection"/> for the given <see cref="ActionComponents.ACNavBarButtonState"/> 
		/// </summary>
		/// <param name="appearance">The modified appearance</param>
		/// <param name="collection">The collection that is receiving the modification</param>
		/// <param name="state">The button state of the appearance being modified</param>
		private void CascadeAppearance(ACNavBarButtonAppearance appearance, ACNavBarButtonCollection collection, ACNavBarButtonState state){
			
			//Does the collection exist yet?
			if (collection == null)
				return;
			
			//Take action based on the state being adjusted
			switch (state) {
			case ACNavBarButtonState.Enabled:
				collection.buttonAppearanceEnabled.Clone (appearance);
				break;
			case ACNavBarButtonState.Disabled:
				collection.buttonAppearanceDisabled.Clone (appearance);
				break;
			case ACNavBarButtonState.Selected :
				collection.buttonAppearanceSelected.Clone (appearance);
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
			
			//Draw background
			ShapeDrawable body= new ShapeDrawable(new RectShape());
			body.Paint.Color=appearance.background;
			body.SetBounds (0,0,64,this.Height);
			body.Draw (canvas);
			
			//Draw border
			ShapeDrawable bodyBorder= new ShapeDrawable(new RectShape());
			bodyBorder.Paint.Color=appearance.border;
			bodyBorder.SetBounds (60,0,64,this.Height);
			bodyBorder.Draw (canvas);
			
			//Draw shadow
			ShapeDrawable shadow= new ShapeDrawable(new RectShape());
			shadow.Paint.Color=appearance.shadow;
			shadow.SetBounds (65,0,69,this.Height);
			shadow.Draw (canvas);
			
			return controlBitmap;
		}
		#endregion 

		#region Override Methods
		/// <summary>
		/// Raises the draw event.
		/// </summary>
		/// <param name="canvas">Canvas.</param>
		protected override void OnDraw (Android.Graphics.Canvas canvas)
		{

			//Call base
			base.OnDraw (canvas);

			//Restoring image from cache?
			if (_imageCache==null) _imageCache=PopulateImageCache();
			
			//Draw cached image to canvas
			canvas.DrawBitmap (_imageCache,0,0,null);

			//Is the pointer in motion?
			if (!_pointer.moving) {
				//Cause all of the containers to redraw
				_top.Redraw ();
				_middle.Redraw ();
				_bottom.Redraw();
			}

		}

		/// <summary>
		/// Raises the layout event.
		/// </summary>
		/// <param name="changed">If set to <c>true</c> changed.</param>
		/// <param name="l">L.</param>
		/// <param name="t">T.</param>
		/// <param name="r">The red component.</param>
		/// <param name="b">The blue component.</param>
		protected override void OnLayout (bool changed, int l, int t, int r, int b)
		{

			//Has the height changed?
			if (_lastHeight != this.Height) {
				//Adjust all child regions to match new height
				top.Redraw ();
				middle.Redraw ();
				bottom.Redraw ();

				//Save last height
				_lastHeight = this.Height;
			}

			//Call base operation
			base.OnLayout (changed, l, t, r, b);

		}

		#endregion 
	}
}

