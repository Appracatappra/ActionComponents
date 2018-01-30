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
	/// The <see cref="ActionComponents.ACLabel"/> has helper methods and events such as <c>Touched</c> and <c>Released</c> that turn it
	/// into an interactive element.
	/// </summary>
	/// <remarks>Available in ActionPack Business or Enterprise only.</remarks>
	public class ACLabel : TextView
	{
		#region Private Properties
		private bool _bringToFrontOnTouched;
		#endregion 

		#region Computed Properties
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="ActionComponents.ACLabel"/>
		/// is automatically brought to the front when touched.
		/// </summary>
		/// <value><c>true</c> if bring to front on touched; otherwise, <c>false</c>.</value>
		public bool bringToFrontOnTouched{
			get { return _bringToFrontOnTouched;}
			set { _bringToFrontOnTouched = value;}
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
		/// Initializes a new instance of the <see cref="ActionComponents.ACLabel"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		public ACLabel (Context context) : base(context)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACLabel"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attr">Attr.</param>
		public ACLabel (Context context, IAttributeSet attr) : base(context,attr)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACLabel"/> class.
		/// </summary>
		/// <param name="javaReference">Java reference.</param>
		/// <param name="transfer">Transfer.</param>
		public ACLabel (IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference,transfer)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACLabel"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attr">Attr.</param>
		/// <param name="defStyle">Def style.</param>
		public ACLabel (Context context, IAttributeSet attr, int defStyle) : base(context,attr,defStyle)
		{
			Initialize ();
		}

		/// <summary>
		/// Initialize this instance.
		/// </summary>
		private void Initialize(){

			//Set defaults

		}
		#endregion 

		#region Public Methods
		/// <summary>
		/// Moves this <see cref="ActionComponents.ACLabel"/> to the given point  
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public void MoveToPoint(int x, int y){

			//Create a new point and move to it
			MoveToPoint (new Point(x,y));
		}

		/// <summary>
		/// Moves this <see cref="ActionComponents.ACLabel"/> to the given point  
		/// </summary>
		/// <param name="pt">Point.</param>
		public void MoveToPoint(Point point){

			//Move to the given location
			LeftMargin = point.X;
			TopMargin = point.Y;
		}

		/// <summary>
		/// Resize this <see cref="ActionComponents.ACLabel"/> to the specified width and height.
		/// </summary>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		public void Resize(int width, int height){
			//Resize this view
			LayoutWidth = width;
			LayoutHeight = height;
		}

		/// <summary>
		/// Test to see if the given x and y coordinates are inside this <see cref="ActionComponents.ACLabel"/> 
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
		/// Test to see if the given point is inside this <see cref="ActionComponents.ACLabel"/>
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
				//Automatically bring view to front?
				if (bringToFrontOnTouched) this.BringToFront();

				//Inform caller of event
				RaiseTouched ();

				//Inform system that we've handled this event 
				break;
			case MotionEventActions.Up:

				//Inform caller of event
				RaiseReleased ();

				#if TRIAL
					Android.Widget.Toast.MakeText(this.Context, "ACLabel by Appracatappra, LLC.", Android.Widget.ToastLength.Short).Show();
				#endif

				//Inform system that we've handled this event 
				break;
			case MotionEventActions.Cancel:
				//Ignore for now
				break;
			}

			// Not handled by this routine
			return base.OnTouchEvent(e);
		}
		#endregion 

		#region Events
		/// <summary>
		/// Occurs when this <see cref="ActionComponents.ACLabel"/> is touched 
		/// </summary>
		public delegate void ACLabelTouchedDelegate (ACLabel view);
		public event ACLabelTouchedDelegate Touched;

		/// <summary>
		/// Raises the touched event
		/// </summary>
		private void RaiseTouched(){
			if (this.Touched != null)
				this.Touched (this);
		}

		/// <summary>
		/// Occurs when this <see cref="ActionComponents.ACLabel"/> is moved
		/// </summary>
		public delegate void ACLabelMovedDelegate (ACLabel view);
		public event ACLabelMovedDelegate Moved;

		/// <summary>
		/// Raises the moved event
		/// </summary>
		private void RaiseMoved(){
			if (this.Moved != null)
				this.Moved (this);
		}

		/// <summary>
		/// Occurs when this <see cref="ActionComponents.ACLabel"/> is released 
		/// </summary>
		public delegate void ACLabelReleasedDelegate (ACLabel view);
		public event ACLabelReleasedDelegate Released;

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

