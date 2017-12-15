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

namespace ActionComponents
{
	/// <summary>
	/// The arrow pointer that is optionally displayed by the selected <see cref="ActionComponents.ACNavBarButton"/> of a
	/// <see cref="ActionComponents.ACNavBar"/> 
	/// </summary>
	/// <remarks>The <see cref="ActionComponents.ACNavBarPointer"/> is controlled by its parent <see cref="ActionComponents.ACNavBar"/> and
	/// should not be modified direction</remarks>
	public partial class ACNavBarPointer : View
	{
		#region Private Variables
		private Display _display;
		private ACNavBarAppearance _appearance;
		private Bitmap _imageCache;
		private bool _moving=false;
		#endregion 
		
		#region Computed Properties
		/// <summary>
		/// Gets a value indicating whether this <see cref="ActionComponents.ACNavBarPointer"/> is moving.
		/// </summary>
		/// <value><c>true</c> if moving; otherwise, <c>false</c>.</value>
		public bool moving {
			get{return _moving;}
		}

		/// <summary>
		/// Controlls the general appearance of the control
		/// </summary>
		public ACNavBarAppearance appearance {
			get{return _appearance;}
			set{
				_appearance=value;

				//Wireup change handler
				_appearance.AppearanceModified+= () => {
					//Force a redraw
					this.Invalidate();
				};
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
		/// Initializes a new instance of the <see cref="ActionComponents.ACNavBarPointer"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		public ACNavBarPointer(Context context)
			: base(context)
		{
			appearance=new ACNavBarAppearance();
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACNavBarPointer"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="display">Display.</param>
		public ACNavBarPointer(Context context, Display display)
			: base(context)
		{
			_display = display;
			appearance=new ACNavBarAppearance();
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACNavBarPointer"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attrs">Attrs.</param>
		public ACNavBarPointer(Context context, IAttributeSet attrs)
			: base(context, attrs)
		{
			appearance=new ACNavBarAppearance();
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACNavBarPointer"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="appearance">Appearance.</param>
		public ACNavBarPointer(Context context, ACNavBarAppearance appearance)
			: base(context)
		{
			this.appearance=appearance;
			Initialize ();
		}

		/// <summary>
		/// Initialize this instance.
		/// </summary>
		private void Initialize() {
			
			//Make background clear
			this.SetBackgroundColor (Color.Argb (0,0,0,0));
			this.LayoutParameters = new RelativeLayout.LayoutParams (18, 24);
			this.LeftMargin = 64;
			this.TopMargin = 8;

			//Wireup changes to appearance
			this.appearance.AppearanceModified += () => {
				//Clear cache and redraw
				if (_imageCache!=null) {
					_imageCache.Dispose();
					_imageCache=null;
				}
				this.Invalidate();
			};
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

			//Define pointer style
			Paint pPointer=new Paint();
			pPointer.SetStyle (Paint.Style.Fill);
			pPointer.AntiAlias=true;
			pPointer.StrokeWidth=1.0f;
			pPointer.Color=appearance.border;
			
			//Draw pointer
			Path p=new Path();
			p.LineTo(this.Width,this.Height/2f);
			p.LineTo(0,this.Height);
			p.LineTo(0,0);
			p.Close();
			canvas.DrawPath (p,pPointer);

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
			SetMeasuredDimension (18,24);
		}

		/// <summary>
		/// Raises the draw event.
		/// </summary>
		/// <param name="canvas">Canvas.</param>
		protected override void OnDraw (Canvas canvas)
		{
			//Restoring image from cache?
			if (_imageCache==null) _imageCache=PopulateImageCache();

			//Draw cached image to canvas
			canvas.DrawBitmap (_imageCache,0,0,null);
		}
		#endregion 

		#region Public Methods
		/// <summary>
		/// Moves the <see cref="ActionComponents.ACNavBarPointer"/> to the location specified and optionally animates the move 
		/// </summary>
		/// <param name="y">The y coordinate.</param>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		/// <remarks>NOTE: Pointer animation is currently NOT supported on the Android OS because of performance issue.</remarks>
		public void MoveTo(float y, bool animated){

			//Move to new location
			if (this.GetY ()!=y) {
				//Are we animating this move?
				if (animated) {
					//Yes, inform caller that we are in motion
					_moving=true;

					var animator = ValueAnimator.OfFloat(this.Top,y);
					animator.SetDuration (500);
					animator.Update += (sender, e) => {
						TopMargin=(int)e.Animation.AnimatedValue;
					};
					animator.AnimationEnd+= (sender, e) => {
						//Clear moving flag
						_moving=false;
					};
					animator.Start ();
				} else {
					//Clear any existing moving flag
					_moving=false;

					//No, jump directly to the new position
					TopMargin=(int)y;
				}
			}
		}
		#endregion 
	}
}

