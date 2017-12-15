using System;
using System.Threading;
using System.Collections;
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

namespace ActionComponents
{
	/// <summary>
	/// Holds all information about a <see cref="ActionComponents.ACAlertButton"/> attached to a <see cref="ActionComponents.ACAlert"/> 
	/// </summary>
	public class ACAlertButton : RelativeLayout
	{
		#region Private variables
		private Bitmap _imageCache;
		private ACAlertButtonAppearance _appearance;
		private ACAlertButtonAppearance _appearanceDisabled;
		private ACAlertButtonAppearance _appearanceTouched;
		private ACAlertButtonAppearance _appearanceHighlighted;
		private ACAlert _alert;
		private string _title;
		private bool _highlighted;
		private bool _bottomRight = false;
		private bool _bottomLeft = false;
		private bool _beingTouched = false;
		private bool _enabled = true;
		#endregion 

		#region Computed Properties
		/// <summary>
		/// Gets or sets the [OPTIONAL] tag that can be assosciated with this <see cref="ActionComponents.ACAlertButton"/> 
		/// </summary>
		/// <value>The tag.</value>
		public Object tag { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <see cref="ActionComponents.ACAlertButton"/> is enabled.
		/// </summary>
		/// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
		public bool Enabled {
			get { return _enabled; }
			set {
				_enabled = value;
				Redraw ();
			}
		}

		/// <summary>
		/// Gets or sets the title for this <see cref="ActionComponents.ACAlertButton"/> 
		/// </summary>
		/// <value>The title.</value>
		public string title{
			get { return _title;}
			set {
				//Save value and redraw
				_title = value;
				Redraw ();
			}
		}

		/// <summary>
		/// Gets or sets the appearance of this <see cref="ActionComponents.ACAlertButton"/> 
		/// </summary>
		/// <value>The appearance.</value>
		public ACAlertButtonAppearance appearance {
			get { return _appearance;}
			set {
				//Save and refresh
				_appearance = value;
				Redraw ();

				//Wire-up events
				_appearance.AppearanceModified += () => {
					Redraw();
				};
			}
		}

		/// <summary>
		/// Gets or sets the appearance of this <see cref="ActionComponents.ACAlertButton"/> when it is disabled
		/// </summary>
		/// <value>The appearance disabled.</value>
		public ACAlertButtonAppearance appearanceDisabled {
			get { return _appearanceDisabled;}
			set {
				//Save and refresh
				_appearanceDisabled = value;
				Redraw ();

				//Wire-up events
				_appearanceDisabled.AppearanceModified += () => {
					Redraw();
				};
			}
		}

		/// <summary>
		/// Gets or sets the touched appearance of this <see cref="ActionComponents.ACAlertButton"/> 
		/// </summary>
		/// <value>The appearance touched.</value>
		public ACAlertButtonAppearance appearanceTouched {
			get { return _appearanceTouched;}
			set {
				//Save and refresh
				_appearanceTouched = value;
				Redraw ();

				//Wire-up events
				_appearanceTouched.AppearanceModified += () => {
					Redraw();
				};
			}
		}

		/// <summary>
		/// Gets or sets the highlighted appearance of this <see cref="ActionComponents.ACAlertButton"/> 
		/// </summary>
		/// <value>The appearance highlighted.</value>
		public ACAlertButtonAppearance appearanceHighlighted {
			get { return _appearanceHighlighted;}
			set {
				//Save and refresh
				_appearanceHighlighted = value;
				Redraw ();

				//Wire-up events
				_appearanceHighlighted.AppearanceModified += () => {
					Redraw();
				};
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <see cref="ActionComponents.ACAlertButton"/> is highlighted.
		/// </summary>
		/// <value><c>true</c> if highlighted; otherwise, <c>false</c>.</value>
		public bool highlighted {
			get { return _highlighted;}
			set {
				//Save and refresh
				_highlighted = value;
				Redraw ();
			}
		}

		/// <summary>
		/// Gets or sets the left margin.
		/// </summary>
		/// <value>The left margin.</value>
		public int LeftMargin{
			get{return ACAlert.GetViewLeftMargin (this);}
			set{ACAlert.SetViewLeftMargin (this, value);}
		}

		/// <summary>
		/// Gets or sets the top margin.
		/// </summary>
		/// <value>The top margin.</value>
		public int TopMargin{
			get{return ACAlert.GetViewTopMargin (this);}
			set{ACAlert.SetViewTopMargin (this, value);}
		}

		/// <summary>
		/// Gets or sets the right margin.
		/// </summary>
		/// <value>The right margin.</value>
		public int RightMargin{
			get{return ACAlert.GetViewRightMargin (this);}
			set{ACAlert.SetViewRightMargin (this, value);}
		}

		/// <summary>
		/// Gets or sets the bottom margin.
		/// </summary>
		/// <value>The bottom margin.</value>
		public int BottomMargin{
			get{return ACAlert.GetViewBottomMargin (this);}
			set{ACAlert.SetViewBottomMargin (this, value);}
		}

		/// <summary>
		/// Gets or sets the width of the layout.
		/// </summary>
		/// <value>The width of the layout.</value>
		public int LayoutWidth{
			get{ return ACAlert.GetViewWidth (this);}
			set{ACAlert.SetViewWidth (this, value);}
		}

		/// <summary>
		/// Gets or sets the height of the layout.
		/// </summary>
		/// <value>The height of the layout.</value>
		public int LayoutHeight{
			get{ return ACAlert.GetViewHeight (this);}
			set{ACAlert.SetViewHeight (this, value);}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACAlertButton"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		public ACAlertButton(Context context)
			: base(context)
		{
		
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACAlertButton"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="alert">Alert.</param>
		/// <param name="appearance">Appearance.</param>
		/// <param name="appearanceHighlighted">Appearance highlighted.</param>
		/// <param name="title">Title.</param>
		/// <param name="highlighted">If set to <c>true</c> highlighted.</param>
		internal ACAlertButton (Context context, ACAlert alert, ACAlertButtonAppearance appearance, ACAlertButtonAppearance appearanceDisabled, ACAlertButtonAppearance appearanceTouched, ACAlertButtonAppearance appearanceHighlighted, string title, bool highlighted): base(context)
		{
			//Initialize
			this.SetBackgroundColor (Color.Argb (0, 0, 0, 0));
			this.Clickable=true;
			this._alert = alert;
			this._appearance = appearance;
			this._appearanceDisabled = appearanceDisabled;
			this._appearanceTouched = appearanceTouched;
			this._appearanceHighlighted = appearanceHighlighted;
			this._title = title;
			this._highlighted = highlighted;

			//Setup initial layout position and size 
			var layout = new RelativeLayout.LayoutParams (100,48);
			layout.LeftMargin = 0;
			layout.TopMargin = 0;
			this.LayoutParameters = layout;

			//Wire-up events
			this._appearance.AppearanceModified += () => {
				Redraw();
			};

			this._appearanceHighlighted.AppearanceModified += () => {
				Redraw();
			};

		}
		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACAlertButton"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="display">Display.</param>
		public ACAlertButton(Context context, Display display)
			: base(context)
		{


		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACAlertButtony"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attrs">Attrs.</param>
		public ACAlertButton(Context context, IAttributeSet attrs)
			: base(context, attrs)
		{


		}
		#endregion 

		#region Internal Methods
		/// <summary>
		/// Sets the button position.
		/// </summary>
		/// <param name="frame">Frame.</param>
		/// <param name="bottomLeft">If set to <c>true</c> bottom left.</param>
		/// <param name="bottomRight">If set to <c>true</c> bottom right.</param>
		internal void SetButtonPosition(int x, int y, int width, int height,  bool bottomLeft, bool bottomRight) {

			//Save new postion and corner information
			this.LeftMargin = x;
			this.TopMargin = y;
			this.LayoutWidth = width;
			this.LayoutHeight = height;
			_bottomLeft = bottomLeft;
			_bottomRight = bottomRight;

			//Force a redraw
			Redraw ();

		}
		#endregion 

		#region Public Methods
		/// <summary>
		/// Forces the <see cref="Appracatappra.ActionComponents.ActionTray.ACTray"/> to dump it's draw buffer and completely redraw the control 
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
		/// Removes this view from its parent view
		/// </summary>
		public void RemoveFromSuperview(){
			//Nab the parent, cast it into a ViewGroup and remove self
			((ViewGroup)this.Parent).RemoveView(this);
		}

		/// <summary>
		/// Test to see if the given x and y coordinates are inside this <see cref="ActionComponents.ACAlert"/> 
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
		/// Test to see if the given point is inside this <see cref="ActionComponents.ACAlert"/>
		/// </summary>
		/// <returns><c>true</c>, if inside was pointed, <c>false</c> otherwise.</returns>
		/// <param name="pt">Point.</param>
		public bool PointInside(Point pt){
			return PointInside (pt.X, pt.Y);
		}

		#endregion

		#region Private Drawing Methods
		/// <summary>
		/// Populates the image cache.
		/// </summary>
		/// <returns>The image cache.</returns>
		private Bitmap PopulateImageCache(){
			List<float> corners = new List<float> () { 0, 0, 0, 0, 0, 0, 0, 0 };
			ACAlertButtonAppearance lookFeel;

			//Set appearance
			if (!Enabled) {
				lookFeel = appearanceDisabled;
			} else if (_beingTouched) {
				lookFeel = appearanceTouched;
			} else if (highlighted) {
				lookFeel = appearanceHighlighted;
			} else {
				lookFeel = appearance;
			}

			//Create a temporary canvas
			var canvas=new Canvas();

			//Create bitmap storage and assign to canvas
			var controlBitmap=Bitmap.CreateBitmap (this.Width,this.Height,Bitmap.Config.Argb8888);
			canvas.SetBitmap (controlBitmap);

			// Take action based on the corners speficied
			if (_bottomLeft && _bottomRight) {
				//Round left and right bottom corners
				corners [4] = _alert.appearance.borderRadius;
				corners [5] = _alert.appearance.borderRadius;
				corners [6] = _alert.appearance.borderRadius;
				corners [7] = _alert.appearance.borderRadius;
			} else if (_bottomLeft) {
				corners [6] = _alert.appearance.borderRadius;
				corners [7] = _alert.appearance.borderRadius;
			} else if (_bottomRight) {
				corners [4] = _alert.appearance.borderRadius;
				corners [5] = _alert.appearance.borderRadius;
			} 

			//Convert list to array
			var cornerArray = corners.ToArray ();

			//Draw body of button
			ShapeDrawable body= new ShapeDrawable(new RoundRectShape(cornerArray,null,null));
			body.Paint.Color=lookFeel.background;
			body.Paint.SetStyle (Paint.Style.Fill);
			body.SetBounds (0,0,this.LayoutWidth,this.LayoutHeight);
			body.Draw (canvas);

			//Draw border of button
			ShapeDrawable bodyBorder= new ShapeDrawable(new RoundRectShape(cornerArray,null,null));
			bodyBorder.Paint.Color = lookFeel.border;
			bodyBorder.Paint.SetStyle (Paint.Style.Stroke);
			body.Paint.StrokeWidth = lookFeel.borderWidth;
			bodyBorder.SetBounds (0,0,this.LayoutWidth,this.LayoutHeight);
			bodyBorder.Draw (canvas);

			//Draw text of button
			Paint textPaint = new Paint ();
			textPaint.Color=lookFeel.titleColor;
			textPaint.SetStyle (Paint.Style.Fill);
			textPaint.TextSize = lookFeel.titleSize;
			textPaint.AntiAlias = true;

			//Draw text into canvas
			//canvas.DrawText (title, 0, 13, textPaint);
			ActionCanvasExtensions.DrawTextBlockInCanvas (canvas, title, 0, 13, this.LayoutWidth, 1, textPaint, TextBlockAlignment.Center);

			return controlBitmap;
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

			// Is this button enabled?
			if (!Enabled)
				return true;

			int x=(int)e.GetX ();
			int y=(int)e.GetY ();

			//Take action based on the event type
			switch(e.Action){
			case MotionEventActions.Down:
				//Update touched style
				_beingTouched = true;
				Redraw ();

				//Inform caller of event
				RaiseTouched ();

				//Inform system that we've handled this event 
				return true;
			case MotionEventActions.Move:

				break;
			case MotionEventActions.Up:
				//Update touched style
				_beingTouched = false;
				Redraw ();

				//Inform caller of event
				RaiseReleased ();

				//Inform system that we've handled this event 
				break;
			case MotionEventActions.Cancel:
				break;
			}

			// Not handled by this routine
			return base.OnTouchEvent(e);
		}
		#endregion

		#region Events
		/// <summary>
		/// Occurs when this <see cref="ActionComponents.ACAlert"/> is touched 
		/// </summary>
		public delegate void ACAlertButtonTouchedDelegate (ACAlertButton button);
		public event ACAlertButtonTouchedDelegate Touched;

		/// <summary>
		/// Raises the touched event
		/// </summary>
		private void RaiseTouched(){
			if (this.Touched != null)
				this.Touched (this);
		}

		/// <summary>
		/// Occurs when this <see cref="ActionComponents.ACAlert"/> is released 
		/// </summary>
		public delegate void ACAlertButtonReleasedDelegate (ACAlertButton button);
		public event ACAlertButtonReleasedDelegate Released;

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

