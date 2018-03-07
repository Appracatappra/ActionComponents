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
	/// The <see cref="ActionComponents.ACImageView"/> is a custom <c>ImageView</c> with built-in helper routines to automatically handle
	/// user interaction such as dragging (with optional constraints on the x and y axis), events such as <c>Touched</c>, <c>Moved</c> and <c>Released</c> and image loading,
	/// scaling and rotation. The <see cref="ActionComponents.ACImageView"/> includes a <c>DisposeImage</c> method to release the memory being
	/// held by an image <c>Bitmap</c>. Memory will automatically be purged before loading a new image into this <see cref="ActionComponents.ACImageView"/>. 
	/// </summary>
	/// <remarks>Notice!: The <see cref="ActionComponents.ACImageView"/> works best hosted inside of a <c>RelativeLayout</c>.
	/// Available in ActionPack Business or Enterprise only.</remarks>
	#if EMBED
	internal
	#else
	public
	#endif
	class ACImageView : ImageView
	{
		#region Private Properties
		private bool _isDraggable;
		private bool _dragging;
		private bool _bringToFrontOnTouched;
		private ACViewDragConstraint _xConstraint;
		private ACViewDragConstraint _yConstraint;
		private Point _startLocation;
		#endregion 

		#region Computed Variables
		/// <summary>
		/// Gets or sets the [OPTIONAL] tag that can be assosciated with this <see cref="ActionComponents.ACImageView"/> 
		/// </summary>
		/// <value>The tag.</value>
		public Object tag { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="ActionComponents.ACImageView"/>
		/// is draggable.
		/// </summary>
		/// <value><c>true</c> if is draggable; otherwise, <c>false</c>.</value>
		public bool draggable {
			get { return _isDraggable;}
			set { _isDraggable = value;}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="ActionComponents.ACImageView"/> is being dragged by the user.
		/// </summary>
		/// <value><c>true</c> if dragging; otherwise, <c>false</c>.</value>
		public bool dragging{
			get { return _dragging;}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="ActionComponents.ACImageView"/>
		/// is automatically brought to the front when touched.
		/// </summary>
		/// <value><c>true</c> if bring to front on touched; otherwise, <c>false</c>.</value>
		public bool bringToFrontOnTouched{
			get { return _bringToFrontOnTouched;}
			set { _bringToFrontOnTouched = value;}
		}

		/// <summary>
		/// Gets or sets the <see cref="ActionComponents.ACViewDragConstraint"/> applied to the <c>x axis</c> of this
		/// <see cref="ActionComponents.ACImageView"/> 
		/// </summary>
		/// <value>The x constraint.</value>
		public ACViewDragConstraint xConstraint{
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
		/// Gets or sets the <see cref="ActionComponents.ACViewDragConstraint"/> applied to the <c>y axis</c> of this
		/// <see cref="ActionComponents.ACImageView"/> 
		/// </summary>
		/// <value>The y constraint.</value>
		public ACViewDragConstraint yConstraint{
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
		/// Initializes a new instance of the <see cref="ActionComponents.ACImageView"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		public ACImageView (Context context) : base(context)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACImageView"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attr">Attr.</param>
		public ACImageView (Context context, IAttributeSet attr) : base(context,attr)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACImageView"/> class.
		/// </summary>
		/// <param name="javaReference">Java reference.</param>
		/// <param name="transfer">Transfer.</param>
		public ACImageView (IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference,transfer)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACImageView"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attr">Attr.</param>
		/// <param name="defStyle">Def style.</param>
		public ACImageView (Context context, IAttributeSet attr, int defStyle) : base(context,attr,defStyle)
		{
			Initialize ();
		}

		/// <summary>
		/// Initialize this instance.
		/// </summary>
		private void Initialize(){

			//Set defaults
			this._isDraggable = false;
			this._dragging = false;
			this._bringToFrontOnTouched = false;
			this._xConstraint = new ACViewDragConstraint ();
			this._yConstraint = new ACViewDragConstraint ();
			this._startLocation = new Point (0, 0);

			//Wireup change events
			this._xConstraint.ConstraintChanged+= () => {
				XConstraintModified();
			};
			this._yConstraint.ConstraintChanged+= () => {
				YConstraintModified();
			};
		}
		#endregion 

		#region Private Methods
		/// <summary>
		/// Adjust this view if the <c>xConstraint</c> has been modified
		/// </summary>
		private void XConstraintModified(){

			//Take action based on the constraint type
			switch (_xConstraint.constraintType) {
				case ACViewDragConstraintType.Constrained:
				//Make sure the x axis is inside the given range
				if (LeftMargin < _xConstraint.minimumValue || LeftMargin > _xConstraint.maximumValue) {
					//Pin to the minimum value
					LeftMargin = _xConstraint.minimumValue;
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
				case ACViewDragConstraintType.Constrained:
				//Make sure the y axis is inside the given range
				if (TopMargin < _yConstraint.minimumValue || TopMargin > _yConstraint.maximumValue) {
					//Pin to the minimum value
					TopMargin = _yConstraint.minimumValue; 
				}
				break;
			}
		}
		#endregion 

		#region Public Methods
		/// <summary>
		/// Loads the image displayed in this <see cref="ActionComponents.ACImageView"/> from the given
		/// <c>Bitmap</c>
		/// </summary>
		/// <param name="bitmap">Bitmap.</param>
		/// <remarks>If this <see cref="ActionComponents.ACImageView"/> already has an image loaded, it
		/// will be purged from memory before the new image is loaded</remarks>
		public void FromBitmap(Bitmap bitmap){

			//Release any memory being held by this ImageView
			DisposeImage ();

			//Load the image for this view
			SetImageBitmap (bitmap);
		}

		/// <summary>
		/// Loads the image displayed in this <see cref="ActionComponents.ACImageView"/> from the given
		/// filename
		/// </summary>
		/// <param name="filename">Filename.</param>
		/// <remarks>If this <see cref="ActionComponents.ACImageView"/> already has an image loaded, it
		/// will be purged from memory before the new image is loaded</remarks>
		public void FromFile(string filename){

			//Release any memory being held by this ImageView
			DisposeImage ();

			//Release bitmap memory when finished loading
			using (var bmp=ACImage.FromFile (filename)) {
				//Load the image for this view
				SetImageBitmap (bmp);
			}
		}

		/// <summary>
		/// Loads the image displayed in this <see cref="ActionComponents.ACImageView"/> from the given filename
		/// resampling the image to the given height/width
		/// </summary>
		/// <param name="filename">Filename.</param>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		/// <remarks>If this <see cref="ActionComponents.ACImageView"/> already has an image loaded, it
		/// will be purged from memory before the new image is loaded</remarks>
		public void FromFile(string filename, int width, int height){

			//Release any memory being held by this ImageView
			DisposeImage ();

			//Release bitmap memory when finished loading
			using (var bmp=ACImage.FromFile (filename,width,height)) {
				//Load the image for this view
				SetImageBitmap (bmp);
			}
		}

		/// <summary>
		/// Loads the image being displayed by this <see cref="ActionComponents.ACImageView"/> from the 
		/// given resources and resource ID
		/// </summary>
		/// <param name="resources">Resources.</param>
		/// <param name="resourceID">Resource ID.</param>
		/// <remarks>If this <see cref="ActionComponents.ACImageView"/> already has an image loaded, it
		/// will be purged from memory before the new image is loaded</remarks>
		public void FromResource(Android.Content.Res.Resources resources,int resourceID){

			//Release any memory being held by this ImageView
			DisposeImage ();

			//Release bitmap memory when finished loading
			using (var bmp=ACImage.FromResource(resources,resourceID)) {
				//Load the image for this view
				SetImageBitmap (bmp);
			}
		}

		/// <summary>
		/// Loads the image being displayed by this <see cref="ActionComponents.ACImageView"/> from the 
		/// given resources and resource ID resampling the image to the given height/width
		/// </summary>
		/// <param name="resources">Resources.</param>
		/// <param name="resourceID">Resource ID.</param>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		/// <remarks>If this <see cref="ActionComponents.ACImageView"/> already has an image loaded, it
		/// will be purged from memory before the new image is loaded</remarks>
		public void FromResource(Android.Content.Res.Resources resources,int resourceID, int width, int height){

			//Release any memory being held by this ImageView
			DisposeImage ();

			//Release bitmap memory when finished loading
			using (var bmp=ACImage.FromResource(resources,resourceID,width,height)) {
				//Load the image for this view
				SetImageBitmap (bmp);
			}
		}

		/// <summary>
		/// Forces this <see cref="ActionComponents.ACImageView"/> to release all of the memory being held
		/// by the <c>Bitmap</c> image it is displaying
		/// </summary>
		public void DisposeImage(){

			// Release the memory being held by this image view
			//((BitmapDrawable)this.getDrawable()).getBitmap().recycle();
			SetImageDrawable (null);
		}

		/// <summary>
		/// Moves this <see cref="ActionComponents.ACImageView"/> to the given point and honors any
		/// <see cref="ActionComponents.ACViewDragConstraint"/>s applied to the <c>X</c> or <c>Y</c> axis 
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public void MoveToPoint(int x, int y){

			//Ensure that we are moving as expected
			_startLocation = new Point(0, 0);

			//Create a new point and move to it
			MoveToPoint (new Point(x,y));
		}

		/// <summary>
		/// Moves this <see cref="ActionComponents.ACImageView"/> to the given point and honors any
		/// <see cref="ActionComponents.ACViewDragConstraint"/>s applied to the <c>X</c> or <c>Y</c> axis 
		/// </summary>
		/// <param name="pt">Point.</param>
		public void MoveToPoint(Point point){

			//Dragging?
			if (dragging) {

				//Process x coord constraint
				switch(xConstraint.constraintType) {
					case ACViewDragConstraintType.None:
					//Adjust frame location
					LeftMargin += point.X - _startLocation.X;
					break;
					case ACViewDragConstraintType.Locked:
					//Don't move x coord
					break;
					case ACViewDragConstraintType.Constrained:
					//Adjust frame location
					LeftMargin += point.X - _startLocation.X;

					//Outside constraints
					if (LeftMargin<xConstraint.minimumValue) {
						LeftMargin=xConstraint.minimumValue;
					} else if (LeftMargin>xConstraint.maximumValue) {
						LeftMargin=xConstraint.maximumValue;
					}
					break;
				}

				//Process y coord constraint
				switch(yConstraint.constraintType) {
					case ACViewDragConstraintType.None:
					//Adjust frame location
					TopMargin += point.Y - _startLocation.Y;
					break;
					case ACViewDragConstraintType.Locked:
					//Don't move x coord
					break;
					case ACViewDragConstraintType.Constrained:
					//Adjust frame location
					TopMargin += point.Y - _startLocation.Y;

					//Outside constraints
					if (TopMargin<yConstraint.minimumValue) {
						TopMargin=yConstraint.minimumValue;
					} else if (TopMargin>yConstraint.maximumValue) {
						TopMargin=yConstraint.maximumValue;
					}
					break;
				}
			} else {
				//Move to the given location
				LeftMargin = point.X;
				TopMargin = point.Y;
			}
		}

		/// <summary>
		/// Resize this <see cref="ActionComponents.ACImageView"/> to the specified width and height.
		/// </summary>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		public void Resize(int width, int height){
			//Resize this view
			LayoutWidth = width;
			LayoutHeight = height;
		}

		/// <summary>
		/// Rotates the image being controlled by this <see cref="ActionComponents.ACImageView"/> to the 
		/// given degrees
		/// </summary>
		/// <param name="degrees">Degrees.</param>
		public void RotateTo(float degrees) {

			//Set rotation type
			SetScaleType(ImageView.ScaleType.Matrix);

			//Create a matrix to handle the rotation
			Matrix mx=new Matrix();

			//Get the current size
			Rect rc = new Rect();
			GetDrawingRect(rc);

			//Apply rotation
			mx.SetRotate(degrees, rc.Width()/2, rc.Height()/2 );
			ImageMatrix = mx;

		}

		/// <summary>
		/// Test to see if the given x and y coordinates are inside this <see cref="ActionComponents.ACImageView"/> 
		/// </summary>
		/// <returns><c>true</c>, if inside was pointed, <c>false</c> otherwise.</returns>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public bool PointInside(int x, int y){
			//Is the give x and y inside
			if (x>=LeftMargin && x<=(LeftMargin+LayoutWidth)) {
				if (y>=TopMargin && y<=(TopMargin+LayoutHeight)) {
					//Inside
					return true;
				}
			}

			//Not inside
			return false;
		}

		/// <summary>
		/// Test to see if the given point is inside this <see cref="ActionComponents.ACImageView"/>
		/// </summary>
		/// <returns><c>true</c>, if inside was pointed, <c>false</c> otherwise.</returns>
		/// <param name="pt">Point.</param>
		public bool PointInside(Point pt){
			return PointInside (pt.X, pt.Y);
		}
		#endregion 

		#region Override Methods
		/// <summary>
		/// Raises the touch event event.
		/// </summary>
		/// <param name="e">E.</param>
		public override bool OnTouchEvent (MotionEvent e)
		{
			int x=(int)e.GetX ();
			int y=(int)e.GetY ();

			//Take action based on the event type
			switch(e.Action){
			case MotionEventActions.Down:
				//Are we already dragging?
				if (_dragging)
					return true;

				//Save the starting location
				_startLocation.X = x;
				_startLocation.Y = y;

				//Automatically bring view to front?
				if (bringToFrontOnTouched)
					this.BringToFront ();

				//Inform caller of event
				RaiseTouched ();

				//Inform system that we've handled this event 
				return true;
			case MotionEventActions.Move:
				//Are we dragging?
				if (draggable) {
					//Move view
					_dragging = true;
					MoveToPoint (x, y);

					//Inform caller of event
					RaiseMoved ();

					//Inform system that we've handled this event 
					return true;
				}
				break;
			case MotionEventActions.Up:
				//Clear any drag action
				_dragging=false;

				//Inform caller of event
				RaiseReleased ();

#if TRIAL
					Android.Widget.Toast.MakeText(this.Context, "ACImageView by Appracatappra, LLC.", Android.Widget.ToastLength.Short).Show();
#else
					AppracatappraLicenseManager.ValidateLicense(this.Context);
#endif

					//Inform system that we've handled this event 
					break;
			case MotionEventActions.Cancel:
				//Clear any drag action
				_dragging=false;
				break;
			}

			// Not handled by this routine
			return base.OnTouchEvent(e);
		}
		#endregion 

		#region Events
		/// <summary>
		/// Occurs when this <see cref="ActionComponents.ACImageView"/> is touched 
		/// </summary>
		public delegate void ACImageViewTouchedDelegate (ACImageView view);
		public event ACImageViewTouchedDelegate Touched;

		/// <summary>
		/// Raises the touched event
		/// </summary>
		private void RaiseTouched(){
			if (this.Touched != null)
				this.Touched (this);
		}

		/// <summary>
		/// Occurs when this <see cref="ActionComponents.ACImageView"/> is moved
		/// </summary>
		public delegate void ACImageViewMovedDelegate (ACImageView view);
		public event ACImageViewMovedDelegate Moved;

		/// <summary>
		/// Raises the moved event
		/// </summary>
		private void RaiseMoved(){
			if (this.Moved != null)
				this.Moved (this);
		}

		/// <summary>
		/// Occurs when this <see cref="ActionComponents.ACImageView"/> is released 
		/// </summary>
		public delegate void ACImageViewReleasedDelegate (ACImageView view);
		public event ACImageViewReleasedDelegate Released;

		/// <summary>
		/// Raises the released.
		/// </summary>
		private void RaiseReleased(){
			if (this.Released != null)
				this.Released (this);
		}
		#endregion 
	}
}

