﻿using System;
using System.Drawing;

using Foundation;
using UIKit;
using CoreGraphics;


namespace ActionComponents
{
	/// <summary>
	/// The <see cref="ActionComponents.ACView"/> is a custom <c>UIView</c> that defines several helper properties and methods that
	/// make it a great basis for any custom user interface controls. It has built in <c>Dragable</c> support that can be limited in movement by
	/// a <see cref="ActionComponents.ACViewDragConstraint"/> applied to the <c>X</c> and/or <c>Y</c> axis. It defines helper events for
	/// being <c>Touched</c>, <c>Moved</c>, and/or <c>Released</c> and can be set to automatically become the front view when it is touched. And provides methods
	/// to make moving, rotating, and resizing the <see cref="ActionComponents.ACView"/> easier with less code.
	/// </summary>
	/// <remarks>The <see cref="ActionComponents.ACView"/> contains a special <c>Purge</c> command to force the release of any
	/// subview that are attached to it. This can be especially useful when dealing with tight memory situations or views that contain several large graphics.
	/// Available in ActionPack Business or Enterprise only.</remarks>
	[Register("ACView")]
	public class ACView : UIView
	{
		#region Private Variables
		private bool _isDraggable;
		private bool _dragging;
		private bool _bringToFrontOnTouched;
		private ACViewDragConstraint _xConstraint;
		private ACViewDragConstraint _yConstraint;
		private CGPoint _startLocation;
		#endregion 

		#region Computed Properties
		/// <summary>
		/// Gets or sets the [OPTIONAL] tag that can be assosciated with this <see cref="ActionComponents.ACView"/> 
		/// </summary>
		/// <value>The tag.</value>
		public Object tag { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="ActionComponents.ACView"/>
		/// is draggable.
		/// </summary>
		/// <value><c>true</c> if is draggable; otherwise, <c>false</c>.</value>
		public bool draggable {
			get { return _isDraggable;}
			set { _isDraggable = value;}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="ActionComponents.ACView"/> is being dragged by the user.
		/// </summary>
		/// <value><c>true</c> if dragging; otherwise, <c>false</c>.</value>
		public bool dragging{
			get { return _dragging;}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="ActionComponents.ACView"/>
		/// is automatically brought to the front when touched.
		/// </summary>
		/// <value><c>true</c> if bring to front on touched; otherwise, <c>false</c>.</value>
		public bool bringToFrontOnTouched{
			get { return _bringToFrontOnTouched;}
			set { _bringToFrontOnTouched = value;}
		}

		/// <summary>
		/// Gets or sets the <see cref="ActionComponents.ACViewDragConstraint"/> applied to the <c>x axis</c> of this
		/// <see cref="ActionComponents.ACView"/> 
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
		/// <see cref="ActionComponents.ACView"/> 
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
		/// Gets or sets a value indicating whether this <see cref="ActionComponents.ACView"/>
		/// is enabled.
		/// </summary>
		/// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
		/// <remarks>Enabling/disabling a <see cref="ActionComponents.ACView"/> automatically changes the value of it's
		/// <c>UserInteractionEnabled</c> flag</remarks>
		public bool Enabled{
			get { return UserInteractionEnabled;}
			set { UserInteractionEnabled = value;}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACView"/> class.
		/// </summary>
		public ACView () : base()
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACView"/> class.
		/// </summary>
		/// <param name="coder">Coder.</param>
		public ACView (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACView"/> class.
		/// </summary>
		/// <param name="flag">Flag.</param>
		public ACView (NSObjectFlag flag) : base(flag)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACView"/> class.
		/// </summary>
		/// <param name="bounds">Bounds.</param>
		public ACView (CGRect bounds) : base(bounds)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACView"/> class.
		/// </summary>
		/// <param name="ptr">Ptr.</param>
		public ACView (IntPtr ptr) : base(ptr)
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
			this._startLocation = new CGPoint (0, 0);
			this.UserInteractionEnabled=true;

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
			case ACViewDragConstraintType.Constrained:
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
		/// Moves this <see cref="ActionComponents.ACView"/> to the given point and honors any
		/// <see cref="ActionComponents.ACViewDragConstraint"/>s applied to the <c>X</c> or <c>Y</c> axis 
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public void MoveToPoint(nfloat x, nfloat y) {

			//Ensure that we are moving as expected
			_startLocation = new CGPoint (0, 0);

			//Create a new point and move to it
			MoveToPoint (new CGPoint(x,y));
		}

		/// <summary>
		/// Moves this <see cref="ActionComponents.ACView"/> to the given point and honors any
		/// <see cref="ActionComponents.ACViewDragConstraint"/>s applied to the <c>X</c> or <c>Y</c> axis 
		/// </summary>
		/// <param name="pt">Point.</param>
		public void MoveToPoint(CGPoint pt) {

			//Dragging?
			if (dragging) {

				//Grab frame
				var frame = this.Frame;

				//Process x coord constraint
				switch(xConstraint.constraintType) {
					case ACViewDragConstraintType.None:
					//Adjust frame location
					frame.X += pt.X - _startLocation.X;
					break;
				case ACViewDragConstraintType.Locked:
					//Don't move x coord
					break;
				case ACViewDragConstraintType.Constrained:
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
				case ACViewDragConstraintType.None:
					//Adjust frame location
					frame.Y += pt.Y - _startLocation.Y;
					break;
				case ACViewDragConstraintType.Locked:
					//Don't move x coord
					break;
				case ACViewDragConstraintType.Constrained:
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
		/// Resize this <see cref="ActionComponents.ACView"/> to the specified width and height.
		/// </summary>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		public void Resize(float width, float height){
			//Resize this view
			Frame = new CGRect (Frame.Left, Frame.Top, width, height);
		}

		/// <summary>
		/// Rotates this <see cref="ActionComponents.ACView"/> to the given degrees
		/// </summary>
		/// <param name="degrees">Degrees.</param>
		public void RotateTo(float degrees) {
			this.Transform=CGAffineTransform.MakeRotation(ACImage.DegreesToRadians(degrees));	
		}

		/// <summary>
		/// Tests to see if the given x and y coordinates are inside this <see cref="ActionComponents.ACView"/> 
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
		/// Test to see if the given point is inside this <see cref="ActionComponents.ACView"/> 
		/// </summary>
		/// <returns><c>true</c>, if point was inside, <c>false</c> otherwise.</returns>
		/// <param name="pt">Point.</param>
		public bool PointInside(CGPoint pt){
			return PointInside (pt.X, pt.Y);
		}

		/// <summary>
		/// The <c>Purge</c> command causes this <see cref="ActionComponents.ACView"/> to force the removal of any
		/// subview from the screen and dispose of the memory that they contain. If <c>forceGarbageCollection</c> is <c>true</c>, garbage collection
		/// will be forced at the end of the purge cycle. The <c>Purge</c> command will cascade to any <see cref="ActionComponents.ACView"/>
		/// or <see cref="ActionComponents.ACImageView"/> subviews attached to this <see cref="ActionComponents.ACView"/> 
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
					if (view is ACView) {
						//Call child's purge routine
						((ACView)view).Purge (false);
					} else if (view is ACImageView) {
						//Call child's purge routine
						((ACImageView)view).Purge (false);
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
			if (! ExclusiveTouch) 
				base.TouchesBegan (touches, evt);
		}

		/// <Docs>Set containing the touches.</Docs>
		/// <summary>
		/// Sent when the <see cref="ActionComponents.ACView"/> is being dragged
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
			if (! ExclusiveTouch) 
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

			#if TRIAL
			ACToast.MakeText("ACView by Appracatappra, LLC.",2f).Show();
			#endif

			//Pass call to base object
			if (! ExclusiveTouch) 
				base.TouchesEnded(touches, evt);
		} 
		#endregion 

		#region Events
		/// <summary>
		/// Occurs when this <see cref="ActionComponents.ACView"/> is touched 
		/// </summary>
		public delegate void ACViewTouchedDelegate (ACView view);
		public event ACViewTouchedDelegate Touched;

		/// <summary>
		/// Raises the touched event
		/// </summary>
		private void RaiseTouched(){
			if (this.Touched != null)
				this.Touched (this);
		}

		/// <summary>
		/// Occurs when this <see cref="ActionComponents.ACView"/> is moved
		/// </summary>
		public delegate void ACViewMovedDelegate (ACView view);
		public event ACViewMovedDelegate Moved;

		/// <summary>
		/// Raises the moved event
		/// </summary>
		private void RaiseMoved(){
			if (this.Moved != null)
				this.Moved (this);
		}

		/// <summary>
		/// Occurs when this <see cref="ActionComponents.ACView"/> is released 
		/// </summary>
		public delegate void ACViewReleasedDelegate (ACView view);
		public event ACViewReleasedDelegate Released;

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

