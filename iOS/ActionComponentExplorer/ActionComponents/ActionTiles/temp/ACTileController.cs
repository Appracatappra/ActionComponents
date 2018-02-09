using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using Foundation;
using UIKit;
using CoreGraphics;

namespace ActionComponents
{
	[Register("ACTileController")]
	public class ACTileController : UIView
	{
		#region Private Variables
		private bool _isDraggable;
		private bool _dragging;
		private bool _bringToFrontOnTouched;
		private ACTileDragConstraint _xConstraint;
		private ACTileDragConstraint _yConstraint;
		private CGPoint _startLocation;
		private List<ACTileGroup> _groups = new List<ACTileGroup> ();
		private UIScrollView _scrollView;
		private ACTileControllerAppearance _appearance;
		private ACTileNavigationBar _navigationBar;
		private ACTileControllerScrollDirection _scrollDirection;
		private bool _suspendUpdates = false;
		private ACTileGroupAppearance _groupAppearance;
		private ACTileAppearance _tileAppearance;
		private bool _liveUpdate = false;
		private double _liveUpdateFrequency = 5;
		private NSTimer _updateTimer;
		private ACTileLiveUpdate _liveUpdateAction;
		private bool _liveUpdateRunning = false;
		#endregion 

		#region Computed Properties
		/// <summary>
		/// Gets or sets the [OPTIONAL] tag that can be assosciated with this ACTileController. 
		/// </summary>
		/// <value>The tag.</value>
		public Object tag { get; set; }

		/// <summary>
		/// Gets or sets the .ACTileLiveUpdate action that will be performed via an automatic update
		/// kicked off by the <c>liveUpdateTimer</c> in the parent ACTileController 
		/// </summary>
		/// <value>The live update action.</value>
		public ACTileLiveUpdate liveUpdateAction{
			get { return _liveUpdateAction;}
			set{ _liveUpdateAction = value;}
		}

		/// <summary>
		/// Gets a value indicating whether this
		/// <c>ACTileController</c> is running a live update
		/// </summary>
		/// <value><c>true</c> if live update running; otherwise, <c>false</c>.</value>
		public bool liveUpdateRunning {
			get { return _liveUpdateRunning;}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <c>ACTileController</c> suspends updates while <c>ACTileGroup</c>s and/or
		/// <c>ACTile</c>s are being added, edited or removed.  
		/// </summary>
		/// <value><c>true</c> if suspend updates; otherwise, <c>false</c>.</value>
		/// <remarks>You should set this property to <c>true</c> when adding or removing large sums of <c>ACTileGroup</c>s
		/// or <c>ACTile</c>s.</remarks>
		public bool suspendUpdates {
			get { return _suspendUpdates;}
			set {
				_suspendUpdates = value;

				//If false assume we need to do a full refresh
				if (!_suspendUpdates) {
					ResizeElements ();
					Redraw ();
				}
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <c>ACTileController</c> does automatic "live updating" of the <c>ACTile</c>s
		/// inside it's <c>ACTileGroup</c>s  
		/// </summary>
		/// <value><c>true</c> if live update; otherwise, <c>false</c>.</value>
		public bool liveUpdate{
			get { return _liveUpdate;}
			set {
				_liveUpdate = value;

				//Starting or stopping updates?
				if (_liveUpdate) {
					//Is there currently a timer?
					if (_updateTimer == null) {
						//No, start a new timer
						_updateTimer = NSTimer.CreateRepeatingScheduledTimer(TimeSpan.FromSeconds(_liveUpdateFrequency),AnimationTimeLoop);
					}
				} else {
					//Is there currently a timer?
					if (_updateTimer != null) {
						//Yes, stop the time and release it
						_updateTimer.Invalidate ();
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
		public double liveUpdateFrequency{
			get { return _liveUpdateFrequency;}
			set {
				_liveUpdateFrequency = value;

				//Starting or stopping updates?
				if (_liveUpdate) {
					//Is there currently a timer?
					if (_updateTimer != null) {
						//Yes, stop the time and release it
						_updateTimer.Invalidate ();
						_updateTimer = null;
					}

					//Start a new timer
					_updateTimer = NSTimer.CreateRepeatingScheduledTimer(TimeSpan.FromSeconds(_liveUpdateFrequency), AnimationTimeLoop);
				} 
			}
		}

		/// <summary>
		/// Gets or sets the default <c>ACTileGroupAppearance</c> for this 
		/// <c>ACTileController</c> 
		/// </summary>
		/// <value>The group appearance.</value>
		public ACTileGroupAppearance groupAppearance {
			get { return _groupAppearance;}
			set {
				_groupAppearance = value;
				CascadeAppearanceChange ();
			}
		}

		/// <summary>
		/// Gets or sets the default <c>ACTileAppearance</c> for this
		/// <c>ACTileController</c> 
		/// </summary>
		/// <value>The tile appearance.</value>
		public ACTileAppearance tileAppearance{
			get { return _tileAppearance;}
			set {
				_tileAppearance = value;
				CascadeTileAppearanceChange ();
			}
		}

		/// <summary>
		/// Gets or sets the <c>ACTileControllerScrollDirection</c> for this
		/// <c>ACTileController</c>  
		/// </summary>
		/// <value>The scroll direction.</value>
		/// <remarks>The default scroll direction will match the device's orientation</remarks>
		public ACTileControllerScrollDirection scrollDirection {
			get { return _scrollDirection;}
			set {
				_scrollDirection = value;
				ResizeElements ();
				Redraw ();
			}
		}

		/// <summary>
		/// Gets or sets the <c>ACTileControllerAppearance</c> fro this <c>ACTileController</c>  
		/// </summary>
		/// <value>The appearance.</value>
		public ACTileControllerAppearance appearance{
			get { return _appearance;}
			set {
				_appearance = value;

				//Wire-up events
				_appearance.AppearanceModified += () => {
					ResizeElements ();
					Redraw();
				};

				//Update control
				ResizeElements ();
				Redraw ();
			}
		}

		/// <summary>
		/// Gets the <c>ACTileNavigationBar</c> attached to this <c>ACTileController</c>  
		/// </summary>
		/// <value>The navigation bar.</value>
		public ACTileNavigationBar navigationBar{
			get { return _navigationBar;}
		}

		/// <summary>
		/// Gets the <c>UIScrollView</c> inside this <c>ACTileController</c> 
		/// </summary>
		/// <value>The scroll view.</value>
		public UIScrollView scrollView {
			get { return _scrollView;}
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
		public int Count {
			get { return _groups.Count;}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <c>ACTile</c>
		/// is draggable.
		/// </summary>
		/// <value><c>true</c> if is draggable; otherwise, <c>false</c>.</value>
		public bool draggable {
			get { return _isDraggable;}
			set { _isDraggable = value;}
		}

		/// <summary>
		/// Gets a value indicating whether this <c>ACTile</c> is being dragged by the user.
		/// </summary>
		/// <value><c>true</c> if dragging; otherwise, <c>false</c>.</value>
		public bool dragging{
			get { return _dragging;}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <c>ACTile</c>
		/// is automatically brought to the front when touched.
		/// </summary>
		/// <value><c>true</c> if bring to front on touched; otherwise, <c>false</c>.</value>
		public bool bringToFrontOnTouched{
			get { return _bringToFrontOnTouched;}
			set { _bringToFrontOnTouched = value;}
		}

		/// <summary>
		/// Gets or sets the <c>ACTileDragConstraint</c> applied to the <c>x axis</c> of this
		/// <c>ACTile</c> 
		/// </summary>
		/// <value>The x constraint.</value>
		public ACTileDragConstraint xConstraint{
			get { return _xConstraint;}
			set {
				_xConstraint = value;

				//Wireup changed event
				_xConstraint.ConstraintChanged += () => {
					XConstraintModified();
				};

				//Fire event
				XConstraintModified ();
			}
		}

		/// <summary>
		/// Gets or sets the <c>ACTileDragConstraint</c> applied to the <c>y axis</c> of this
		/// <c>ACTile</c> 
		/// </summary>
		/// <value>The y constraint.</value>
		public ACTileDragConstraint yConstraint{
			get { return _yConstraint;}
			set {
				_yConstraint = value;

				//Wireup changed event
				_yConstraint.ConstraintChanged += () => {
					YConstraintModified();
				};

				//Fire event
				YConstraintModified ();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <c>ACTile</c>
		/// is enabled.
		/// </summary>
		/// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
		/// <remarks>Enabling/disabling a <c>ACTile</c> automatically changes the value of it's
		/// <c>UserInteractionEnabled</c> flag</remarks>
		public bool Enabled{
			get { return UserInteractionEnabled;}
			set { UserInteractionEnabled = value;}
		}
		#endregion 

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <c>ACTileController</c> class.
		/// </summary>
		public ACTileController () : base()
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <c>ACTileController</c> class.
		/// </summary>
		/// <param name="coder">Coder.</param>
		public ACTileController (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <c>ACTileController</c> class.
		/// </summary>
		/// <param name="flag">Flag.</param>
		public ACTileController (NSObjectFlag flag) : base(flag)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <c>ACTileController</c> class.
		/// </summary>
		/// <param name="bounds">Bounds.</param>
		public ACTileController (CGRect bounds) : base(bounds)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <c>ACTileController</c> class.
		/// </summary>
		/// <param name="ptr">Ptr.</param>
		public ACTileController (IntPtr ptr) : base(ptr)
		{
			Initialize ();
		}

		/// <summary>
		/// Initialize this instance.
		/// </summary>
		private void Initialize(){

			//Set defaults
			this.BackgroundColor = ACColor.Clear;
			this._isDraggable = false;
			this._dragging = false;
			this._bringToFrontOnTouched = false;
			this._xConstraint = new ACTileDragConstraint ();
			this._yConstraint = new ACTileDragConstraint ();
			this._startLocation = new CGPoint (0, 0);
			this._appearance = new ACTileControllerAppearance ();
			this.groupAppearance = new ACTileGroupAppearance ();
			this.UserInteractionEnabled = true;
			this.MultipleTouchEnabled = true;

			//Set the default scroll direction
			switch (iOSDevice.currentDeviceOrientation) {
			case UIInterfaceOrientation.LandscapeLeft:
			case UIInterfaceOrientation.LandscapeRight:
				_scrollDirection = ACTileControllerScrollDirection.Horizontal;
				break;
			case UIInterfaceOrientation.Portrait:
			case UIInterfaceOrientation.PortraitUpsideDown:
				_scrollDirection = ACTileControllerScrollDirection.Vertical;
				break;
			}

			//Add a navigation bar
			this._navigationBar=new ACTileNavigationBar(this,new CGRect(0,-50,this.Frame.Width,50f));
			AddSubview (this._navigationBar);

			//Wire-up Navigation Bar events
			this._navigationBar.BarShown += (navigationBar) => {
				ResizeElements();
				Redraw();
			};

			this._navigationBar.BarHidden += (navigationBar) => {
				ResizeElements();
				Redraw();
			};

			//Insert an embedded scroll view controller
			this._scrollView = new UIScrollView (this.Frame);
			this._scrollView.BackgroundColor = ACColor.Clear;
			this._scrollView.ContentSize = this.Frame.Size;
			AddSubview (this._scrollView);

			//Wire-up events
			this._appearance.AppearanceModified += () => {
				ResizeElements ();
				Redraw();
			};

			//Wireup change events
			this._xConstraint.ConstraintChanged+= () => {
				XConstraintModified();
			};
			this._yConstraint.ConstraintChanged+= () => {
				YConstraintModified();
			};
		}
		#endregion

		#region Internal Methods
		/// <summary>
		/// Resizes all of the elements contained within this <c>ACTileController</c> 
		/// </summary>
		internal void ResizeElements(){

			//Adjust the size and position of the contained scroll view controller
			_scrollView.Frame = new CGRect (0, (navigationBar.hidden ? 0 : 50) + appearance.indentTop, Frame.Width, Frame.Height - ((navigationBar.hidden ? 0 : 50) + appearance.indentTop + appearance.indentBottom));

			//Recalculate the size of all groups in this controller
			foreach (ACTileGroup group in _groups) {
				group.RecalculateSize ();
			}

		}
		#endregion 

		#region Private Methods
		/// <summary>
		/// Animations the time loop.
		/// </summary>
		[Export("AnimationTimeLoop:")]
		private void AnimationTimeLoop(NSTimer obj) {

			//Inform caller that we're started a live update
			RaiseLiveUpdating ();

			//Perform any updates on the groups
			foreach (ACTileGroup group in _groups) {
				//Does this group contain a live update action?
				if (group.liveUpdateAction != null) {
					//Yes, call it into action
					group.liveUpdateAction.PerformUpdate ();
				}

				//Check all tiles in the group for a live update
				group.LiveUpdateTiles ();

				//Tell the group that it is live updating
				group.RaiseLiveUpdating ();
			}

			//Kickoff the live update action for the controller itself
			if (liveUpdateAction != null) {
				liveUpdateAction.PerformUpdate ();
			}

		}

		/// <summary>
		/// Cascades the appearance change to every <c>ACTileGroup</c> in this controller
		/// </summary>
		private void CascadeAppearanceChange(){

			//Pass change down to each group
			foreach (ACTileGroup group in _groups) {
				group.appearance = groupAppearance;
			}

		}

		/// <summary>
		/// Cascades the tile appearance change to every <c>ACTileGroup</c> in this controller
		/// </summary>
		private void CascadeTileAppearanceChange(){

			//Pass change down to each group
			foreach (ACTileGroup group in _groups) {
				group.defaultTileAppearance = tileAppearance;
			}

		}

		/// <summary>
		/// Adjust this view if the <c>xConstraint</c> has been modified
		/// </summary>
		private void XConstraintModified(){

			//Take action based on the constraint type
			switch (_xConstraint.constraintType) {
				case ACTileDragConstraintType.Constrained:
				//Make sure the x axis is inside the given range
				if (Frame.Left < _xConstraint.minimumValue || Frame.Left > _xConstraint.maximumValue) {
					//Pin to the minimum value
					Frame = new CGRect (_xConstraint.minimumValue, Frame.Top, Frame.Width, Frame.Height);
				}
				break;
			}

		}

		/// <summary>
		/// Adjust this view if the <c>yConstraint</c> has been modified
		/// </summary>
		private void YConstraintModified(){

			//Take action based on the constraint type
			switch (_yConstraint.constraintType) {
				case ACTileDragConstraintType.Constrained:
				//Make sure the y axis is inside the given range
				if (Frame.Top < _yConstraint.minimumValue || Frame.Top > _yConstraint.maximumValue) {
					//Pin to the minimum value
					Frame = new CGRect (Frame.Left, _yConstraint.minimumValue, Frame.Width, Frame.Height); 
				}
				break;
			}
		}
		#endregion 

		#region Public Methods
		/// <summary>
		/// For <c>Horizontal</c> <c>ACTileControllerScrollDirection</c> the <c>ACTileGroup</c>
		/// will fill the parent <c>ACTileController</c> vertically and grow in width based on the number of
		/// <c>ACTile</c>s it contains. For <c>Vertical</c> this is inverted, it will fill the parent
		/// <c>ACTileController</c> horizontally and grow vertically.
		/// </summary>
		/// <returns>The expanding group.</returns>
		public ACTileGroup AddExpandingGroup() {
			return AddExpandingGroup ("", "");
		}

		/// <summary>
		/// For <c>Horizontal</c> <c>ACTileControllerScrollDirection</c> the <c>ACTileGroup</c>
		/// will fill the parent <c>ACTileController</c> vertically and grow in width based on the number of
		/// <c>ACTile</c>s it contains. For <c>Vertical</c> this is inverted, it will fill the parent
		/// <c>ACTileController</c> horizontally and grow vertically.
		/// </summary>
		/// <returns>The expanding group.</returns>
		/// <param name="title">Title.</param>
		public ACTileGroup AddExpandingGroup(string title) {
			return AddExpandingGroup (title, "");
		}

		/// <summary>
		/// For <c>Horizontal</c> <c>ACTileControllerScrollDirection</c> the <c>ACTileGroup</c>
		/// will fill the parent <c>ACTileController</c> vertically and grow in width based on the number of
		/// <c>ACTile</c>s it contains. For <c>Vertical</c> this is inverted, it will fill the parent
		/// <c>ACTileController</c> horizontally and grow vertically.
		/// </summary>
		/// <returns>The expanding group.</returns>
		/// <param name="title">Title.</param>
		/// <param name="footer">Footer.</param>
		public ACTileGroup AddExpandingGroup(string title, string footer) {
			ACTileGroup group = new ACTileGroup();

			//Create group based on the scroll direction
			switch (scrollDirection) {
			case ACTileControllerScrollDirection.Horizontal:
				group = new ACTileGroup (this, ACTileGroupType.ExpandingGroup, title, footer, new ACTileGroupCellConstraint (ACTileGroupCellConstraintType.Flexible, 0), 
				                               new ACTileGroupCellConstraint (ACTileGroupCellConstraintType.FitParent, 0));
				break;
			case ACTileControllerScrollDirection.Vertical:
				group = new ACTileGroup (this, ACTileGroupType.ExpandingGroup, title, footer, new ACTileGroupCellConstraint (ACTileGroupCellConstraintType.FitParent, 0), 
				                               new ACTileGroupCellConstraint (ACTileGroupCellConstraintType.Flexible, 0));
				break;
			}

			//Save group and add to scrollview
			_groups.Add (group);
			_scrollView.AddSubview (group);

			//Update display
			Redraw ();

			//Return new group
			return group;
		}

		/// <summary>
		/// The <c>ACTileGroup</c> will be one "page" wide and high, filling the parent
		/// <c>ACTileController</c> both horizontally and vertically.  
		/// </summary>
		/// <returns>The page group.</returns>
		public ACTileGroup AddPageGroup() {
			return AddPageGroup ("", "");
		}

		/// <summary>
		/// The <c>ACTileGroup</c> will be one "page" wide and high, filling the parent
		/// <c>ACTileController</c> both horizontally and vertically.  
		/// </summary>
		/// <returns>The page group.</returns>
		/// <param name="title">Title.</param>
		public ACTileGroup AddPageGroup(string title) {
			return AddPageGroup (title, "");
		}

		/// <summary>
		/// The <c>ACTileGroup</c> will be one "page" wide and high, filling the parent
		/// <c>ACTileController</c> both horizontally and vertically.  
		/// </summary>
		/// <returns>The page group.</returns>
		/// <param name="title">Title.</param>
		/// <param name="footer">Footer.</param>
		public ACTileGroup AddPageGroup(string title, string footer) {
			ACTileGroup group = new ACTileGroup();

			//Create group based on the scroll direction
			switch (scrollDirection) {
			case ACTileControllerScrollDirection.Horizontal:
				group = new ACTileGroup (this, ACTileGroupType.PageGroup, title, footer, new ACTileGroupCellConstraint (ACTileGroupCellConstraintType.FitParent, 0), 
				                               new ACTileGroupCellConstraint (ACTileGroupCellConstraintType.FitParent, 0));
				break;
			case ACTileControllerScrollDirection.Vertical:
				group = new ACTileGroup (this, ACTileGroupType.PageGroup, title, footer, new ACTileGroupCellConstraint (ACTileGroupCellConstraintType.FitParent, 0), 
				                               new ACTileGroupCellConstraint (ACTileGroupCellConstraintType.FitParent, 0));
				break;
			}

			//Save group and add to scrollview
			_groups.Add (group);
			_scrollView.AddSubview (group);

			//Update display
			Redraw ();

			//Return new group
			return group;
		}

		/// <summary>
		/// For <c>Horizontal</c> <c>ACTileControllerScrollDirection</c> the <c>ACTileGroup</c>
		/// will fill the parent <c>ACTileController</c> horizontally and have a fixed height based on the given number of rows. 
		/// For <c>Vertical</c> this is inverted, it will fill the parent <c>ACTileController</c> vertically and have a fixed width based on the
		/// given number of columns. 
		/// </summary>
		/// <returns>The fixed size page group.</returns>
		/// <param name="cells">Cells.</param>
		public ACTileGroup AddFixedSizePageGroup(int cells){
			return AddFixedSizePageGroup ("", "", cells);
		}

		/// <summary>
		/// For <c>Horizontal</c> <c>ACTileControllerScrollDirection</c> the <c>ACTileGroup</c>
		/// will fill the parent <c>ACTileController</c> horizontally and have a fixed height based on the given number of rows. 
		/// For <c>Vertical</c> this is inverted, it will fill the parent <c>ACTileController</c> vertically and have a fixed width based on the
		/// given number of columns. 
		/// </summary>
		/// <returns>The fixed size page group.</returns>
		/// <param name="title">Title.</param>
		/// <param name="cells">Cells.</param>
		public ACTileGroup AddFixedSizePageGroup(string title,int cells){
			return AddFixedSizePageGroup (title, "", cells);
		}

		/// <summary>
		/// For <c>Horizontal</c> <c>ACTileControllerScrollDirection</c> the <c>ACTileGroup</c>
		/// will fill the parent <c>ACTileController</c> horizontally and have a fixed height based on the given number of rows. 
		/// For <c>Vertical</c> this is inverted, it will fill the parent <c>ACTileController</c> vertically and have a fixed width based on the
		/// given number of columns. 
		/// </summary>
		/// <returns>The fixed size page group.</returns>
		/// <param name="title">Title.</param>
		/// <param name="footer">Footer.</param>
		/// <param name="cells">Cells.</param>
		public ACTileGroup AddFixedSizePageGroup(string title, string footer, int cells) {
			ACTileGroup group = new ACTileGroup();

			//Create group based on the scroll direction
			switch (scrollDirection) {
			case ACTileControllerScrollDirection.Horizontal:
				group = new ACTileGroup (this, ACTileGroupType.FixedSizePageGroup, title, footer, new ACTileGroupCellConstraint (ACTileGroupCellConstraintType.FitParent, 0), 
				                               new ACTileGroupCellConstraint (ACTileGroupCellConstraintType.Fixed, cells));
				break;
			case ACTileControllerScrollDirection.Vertical:
				group = new ACTileGroup (this, ACTileGroupType.FixedSizePageGroup, title, footer, new ACTileGroupCellConstraint (ACTileGroupCellConstraintType.Fixed, cells), 
				                               new ACTileGroupCellConstraint (ACTileGroupCellConstraintType.FitParent, 0));
				break;
			}

			//Save group and add to scrollview
			_groups.Add (group);
			_scrollView.AddSubview (group);

			//Update display
			Redraw ();

			//Return new group
			return group;
		}

		/// <summary>
		/// The <c>ACTileGroup</c> will have a fixed height and width based on the given number of rows and columns.
		/// </summary>
		/// <returns>The fixed size group.</returns>
		/// <param name="rows">Rows.</param>
		/// <param name="columns">Columns.</param>
		public ACTileGroup AddFixedSizeGroup(int rows, int columns){
			return AddFixedSizeGroup ("", "", rows, columns);
		}

		/// <summary>
		/// The <c>ACTileGroup</c> will have a fixed height and width based on the given number of rows and columns.
		/// </summary>
		/// <returns>The fixed size group.</returns>
		/// <param name="title">Title.</param>
		/// <param name="rows">Rows.</param>
		/// <param name="columns">Columns.</param>
		public ACTileGroup AddFixedSizeGroup(string title, int rows, int columns){
			return AddFixedSizeGroup (title, "", rows, columns);
		}

		/// <summary>
		/// The <c>ACTileGroup</c> will have a fixed height and width based on the given number of rows and columns.
		/// </summary>
		/// <returns>The fixed size group.</returns>
		/// <param name="title">Title.</param>
		/// <param name="footer">Footer.</param>
		/// <param name="rows">Rows.</param>
		/// <param name="columns">Columns.</param>
		public ACTileGroup AddFixedSizeGroup(string title, string footer, int rows, int columns) {
			ACTileGroup group = new ACTileGroup();

			//Create group based on the scroll direction
			switch (scrollDirection) {
			case ACTileControllerScrollDirection.Horizontal:
				group = new ACTileGroup (this, ACTileGroupType.FixedSizeGroup, title, footer, new ACTileGroupCellConstraint (ACTileGroupCellConstraintType.Fixed, columns), 
				                               new ACTileGroupCellConstraint (ACTileGroupCellConstraintType.Fixed, rows));
				break;
			case ACTileControllerScrollDirection.Vertical:
				group = new ACTileGroup (this, ACTileGroupType.FixedSizeGroup, title, footer, new ACTileGroupCellConstraint (ACTileGroupCellConstraintType.Fixed, columns), 
				                               new ACTileGroupCellConstraint (ACTileGroupCellConstraintType.Fixed, rows));
				break;
			}

			//Save group and add to scrollview
			_groups.Add (group);
			_scrollView.AddSubview (group);

			//Update display
			Redraw ();

			//Return new group
			return group;
		}

		/// <summary>
		/// Removes the <c>ACTilegGroup</c> at index.
		/// </summary>
		/// <param name="index">Index.</param>
		public void RemoveGroupAt(int index){
			_groups.RemoveAt (index);
			ResizeElements ();
			Redraw ();
		}

		/// <summary>
		/// Removes all <c>ACTileGroup</c>s 
		/// </summary>
		public void ClearGroups() {
			_groups.Clear ();
			ResizeElements ();
			Redraw ();
		}

		/// <summary>
		/// Forces this <c>ACTileController</c> to fully redraw itself
		/// </summary>
		public void Redraw() {
			nfloat x = appearance.indentLeft;
			nfloat y = 0;

			//Is updating suspended?
			if (suspendUpdates)
				return;

			//Force component to update view
			this.SetNeedsDisplay ();

			//Reflow all groups in this controller
			foreach (ACTileGroup group in _groups) {
				//Take action based on the scroll direction of this controller
				switch (scrollDirection) {
				case ACTileControllerScrollDirection.Horizontal:
					group.MoveToPoint (x, y);
					x += group.Frame.Width + appearance.groupGap;
					break;
				case ACTileControllerScrollDirection.Vertical:
					group.MoveToPoint (x, y);
					y += group.Frame.Height + appearance.groupGap;
					break;
				}
			}

			//Adjust scroll size based on scroll direction
			switch (scrollDirection) {
			case ACTileControllerScrollDirection.Horizontal:
				scrollView.ContentSize = new CGSize(x + appearance.indentRight, scrollView.Frame.Height);
				break;
			case ACTileControllerScrollDirection.Vertical:
				scrollView.ContentSize = new CGSize (scrollView.Frame.Width, y + appearance.indentBottom);
				break;
			}
		}

		/// <summary>
		/// Moves this <c>ACTile</c> to the given point and honors any
		/// <c>ACTileDragConstraint</c>s applied to the <c>X</c> or <c>Y</c> axis 
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public void MoveToPoint(float x, float y) {

			//Ensure that we are moving as expected
			_startLocation = new CGPoint (0, 0);

			//Create a new point and move to it
			MoveToPoint (new CGPoint(x,y));
		}

		/// <summary>
		/// Moves this <c>ACTile</c> to the given point and honors any
		/// <c>ACTileDragConstraint</c>s applied to the <c>X</c> or <c>Y</c> axis 
		/// </summary>
		/// <param name="pt">Point.</param>
		public void MoveToPoint(CGPoint pt) {

			//Dragging?
			if (dragging) {

				//Grab frame
				var frame = this.Frame;

				//Process x coord constraint
				switch(xConstraint.constraintType) {
					case ACTileDragConstraintType.None:
					//Adjust frame location
					frame.X += pt.X - _startLocation.X;
					break;
					case ACTileDragConstraintType.Locked:
					//Don't move x coord
					break;
					case ACTileDragConstraintType.Constrained:
					//Adjust frame location
					frame.X += pt.X - _startLocation.X;

					//Outside constraints
					if (frame.X<xConstraint.minimumValue) {
						frame.X=xConstraint.minimumValue;
					} else if (frame.X>xConstraint.maximumValue) {
						frame.X=xConstraint.maximumValue;
					}
					break;
				}

				//Process y coord constraint
				switch(yConstraint.constraintType) {
					case ACTileDragConstraintType.None:
					//Adjust frame location
					frame.Y += pt.Y - _startLocation.Y;
					break;
					case ACTileDragConstraintType.Locked:
					//Don't move x coord
					break;
					case ACTileDragConstraintType.Constrained:
					//Adjust frame location
					frame.Y += pt.Y - _startLocation.Y;

					//Outside constraints
					if (frame.Y<yConstraint.minimumValue) {
						frame.Y=yConstraint.minimumValue;
					} else if (frame.Y>yConstraint.maximumValue) {
						frame.Y=yConstraint.maximumValue;
					}
					break;
				}

				//Apply new location
				this.Frame = frame;
			} else {
				//Move to the given location
				Frame = new CGRect (pt.X,pt.Y, Frame.Width, Frame.Height);
			}
		}

		/// <summary>
		/// Resize this <c>ACTile</c> to the specified width and height.
		/// </summary>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		public void Resize(float width, float height){
			//Resize this view
			Frame = new CGRect (Frame.Left, Frame.Top, width, height);
		}

		/// <summary>
		/// Rotates this <c>ACTile</c> to the given degrees
		/// </summary>
		/// <param name="degrees">Degrees.</param>
		public void RotateTo(float degrees) {
			this.Transform=CGAffineTransform.MakeRotation(ACImage.DegreesToRadians(degrees));	
		}

		/// <summary>
		/// Tests to see if the given x and y coordinates are inside this <c>ACTile</c> 
		/// </summary>
		/// <returns><c>true</c>, if the point was inside, <c>false</c> otherwise.</returns>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public bool PointInside(nfloat x, nfloat y){
			//Is the give x inside
			if (x>=Frame.X && x<=(Frame.X+Frame.Width)) {
				if (y>=Frame.Y && y<=(Frame.Y+Frame.Height)) {
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
		public bool PointInside(CGPoint pt){
			return PointInside (pt.X, pt.Y);
		}

		/// <summary>
		/// The <c>Purge</c> command causes this <c>ACTile</c> to force the removal of any
		/// subview from the screen and dispose of the memory that they contain. If <c>forceGarbageCollection</c> is <c>true</c>, garbage collection
		/// will be forced at the end of the purge cycle. The <c>Purge</c> command will cascade to any <c>ACTile</c>
		/// or <c>ACImageView</c> subviews attached to this <c>ACTile</c> 
		/// </summary>
		/// <param name="forceGarbageCollection">If set to <c>true</c> force garbage collection.</param>
		/// <remarks>Special handling is taken on <c>UIImageViews</c> to ensure that they fully release any image memory that they contain. Simply
		/// calling <c>Dispose()</c> doesn't always release the child bitmaps in the <c>UIImageView</c>'s <c>Image</c> property.</remarks>
		public void Purge(bool forceGarbageCollection){

			//Release any subviews that are attached to this view
			foreach(UIView view in Subviews){

				//Remove the view from it's superview
				view.RemoveFromSuperview ();

				//Trap any errors
				try {
					//Look for any speciality views and take extra cleaning action
					if (view is ACTile) {
						//Call child's purge routine
						((ACTile)view).Purge (false);
					} else if (view is UIImageView) {
						//Force the image view to release the memory being held by it's
						//image, if one exists
						if (((UIImageView)view).Image!=null) {
							((UIImageView)view).Image.Dispose ();
							((UIImageView)view).Image = null;
						}
						view.Dispose ();
					} else {
						//Force disposal of this subview
						view.Dispose ();
					}

				}
				catch {
					#if DEBUG
					//Report disposal issue
					Console.WriteLine ("Unable to purge {0}", view);
					#endif
				}
			}

			//Are we forcing garbage collection?
			if (forceGarbageCollection) {
				//Yes, tell garbage collector to kick-off
				System.GC.Collect();

				#if DEBUG
				Console.WriteLine("GC Memory usage {0}", System.GC.GetTotalMemory(true)); 
				Console.WriteLine("GC GEN:{0} Count:{1}",GC.GetGeneration(this), GC.CollectionCount(GC.GetGeneration(this)));
				#endif
			}

		}
		#endregion

		#region Override Methods
		/// <summary>
		/// Draw the specified rect.
		/// </summary>
		/// <param name="rect">Rect.</param>
		public override void Draw (CGRect rect)
		{
			base.Draw (rect);

			//Is this a custom drawn tile?
			if (appearance.customDrawn) {
				//Yes, request that the user draws the tile and return
				RaiseRequestCustomDraw (rect);
				return;
			}

			//// General Declarations
			var context = UIGraphics.GetCurrentContext();

			//// Backdrop Drawing
			var backdropPath = UIBezierPath.FromRect(rect);

			//Fill with color
			appearance.background.SetFill ();
			backdropPath.Fill ();

			//Is there an image?
			if (appearance.backgroundImage != null){
				//Draw background image
				context.SaveState ();
				backdropPath.AddClip ();
				appearance.backgroundImage.Draw (new CGRect (0, 0, rect.Size.Width, rect.Size.Height));
				context.RestoreState ();
			}
			appearance.border.SetStroke();
			backdropPath.LineWidth = 1;
			backdropPath.Stroke();

		}

		/// <Docs>Set containing the touches.</Docs>
		/// <summary>
		/// Sent when one or more fingers touches the screen.
		/// </summary>
		/// <param name="touches">Touches.</param>
		/// <param name="evt">Evt.</param>
		public override void TouchesBegan (NSSet touches, UIEvent evt)
		{
			//Already dragging?
			if (_dragging) return;

			//Save starting location
			var pt = (touches.AnyObject as UITouch).LocationInView(this);
			_startLocation = pt;

			//Automatically bring view to front?
			if (_bringToFrontOnTouched && this.Superview!=null) this.Superview.BringSubviewToFront(this);

			//Inform caller of event
			RaiseTouched ();

			//Pass call to base object
			base.TouchesBegan (touches, evt);
		}

		/// <Docs>Set containing the touches.</Docs>
		/// <summary>
		/// Sent when the <c>ACTile</c> is being dragged
		/// </summary>
		/// <param name="touches">Touches.</param>
		/// <param name="evt">Evt.</param>
		public override void TouchesMoved (NSSet touches, UIEvent evt)
		{
			//Is this view draggable?
			if (draggable) {
				// Move relative to the original touch point
				_dragging=true;
				var pt = (touches.AnyObject as UITouch).LocationInView(this);
				MoveToPoint(pt);

				//Inform caller of event
				RaiseMoved ();
			}

			//Pass call to base object
			base.TouchesMoved(touches, evt);
		}

		/// <Docs>Set containing the touches.</Docs>
		/// <summary>
		/// Send when one or more fingers are lifted from the screen.
		/// </summary>
		/// <param name="touches">Touches.</param>
		/// <param name="evt">Evt.</param>
		public override void TouchesEnded (NSSet touches, UIEvent evt)
		{
			//Is this view draggable?
			if (draggable) {
				// Move relative to the original touch point 
				var pt = (touches.AnyObject as UITouch).LocationInView(this);
				MoveToPoint(pt);
				_dragging=false;
			}

			//Clear starting point
			_startLocation = new CGPoint (0, 0);

			//Inform caller of event
			RaiseReleased ();

			//Pass call to base object
			base.TouchesEnded(touches, evt);
		} 
		#endregion 

		#region Events
		/// <summary>
		/// Occurs when this <c>ACTileController</c> is touched 
		/// </summary>
		public delegate void ACTileControllerTouchedDelegate (ACTileController view);
		public event ACTileControllerTouchedDelegate Touched;

		/// <summary>
		/// Raises the touched event
		/// </summary>
		private void RaiseTouched(){
			if (this.Touched != null)
				this.Touched (this);
		}

		/// <summary>
		/// Occurs when this <c>ACTileController</c> is moved
		/// </summary>
		public delegate void ACTileControllerMovedDelegate (ACTileController view);
		public event ACTileControllerMovedDelegate Moved;

		/// <summary>
		/// Raises the moved event
		/// </summary>
		private void RaiseMoved(){
			if (this.Moved != null)
				this.Moved (this);
		}

		/// <summary>
		/// Occurs when this <c>ACTileController</c> is released 
		/// </summary>
		public delegate void ACTileControllerReleasedDelegate (ACTileController view);
		public event ACTileControllerReleasedDelegate Released;

		/// <summary>
		/// Raises the released.
		/// </summary>
		private void RaiseReleased(){
			if (this.Released != null)
				this.Released (this);
		}

		/// <summary>
		/// Occurs when live updating has been kicked off by the <c>ACTileController</c> 
		/// </summary>
		public delegate void ACTileControllerLiveUpdatingDelegate (ACTileController controller);
		public event ACTileControllerLiveUpdatingDelegate LiveUpdating;

		/// <summary>
		/// Raises the live updating.
		/// </summary>
		internal void RaiseLiveUpdating(){
			if (this.LiveUpdating != null)
				this.LiveUpdating (this);
		}

		/// <summary>
		/// Occurs when the <c>ACTile</c> <c>Style</c> is set to <c>CustomDrawn</c>
		/// and the <c>ACTile</c> needs to be updated
		/// </summary>
		public delegate void ACTileControllerRequestCustomDrawDelegate (ACTileController controller, CGRect rect);
		public event ACTileControllerRequestCustomDrawDelegate RequestCustomDraw;

		/// <summary>
		/// Raises the RequestCustomDraw.
		/// </summary>
		private void RaiseRequestCustomDraw(CGRect rect){
			if (this.RequestCustomDraw != null)
				this.RequestCustomDraw (this, rect);
		}
		#endregion 
	}
}

