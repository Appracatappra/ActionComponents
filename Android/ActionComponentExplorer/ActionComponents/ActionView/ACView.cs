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
	/// The <see cref="ActionComponents.ACView"/> is a custom <c>RelativeLayout</c> with built-in helper routines to automatically handle
	/// user interaction such as dragging (with optional constraints on the x and y axis), events such as <c>Touched</c>, <c>Moved</c> and <c>Released</c> and moving,
	/// and resizing. The <see cref="ActionComponents.ACView"/> includes a <c>Purge</c> method to release the memory being
	/// held by it's subviews and layouts. 
	/// </summary>
	/// <remarks>NOTICE: The <see cref="ActionComponents.ACView"/> works best if hosted inside of a <c>RelativeLayout</c>.
	/// Available in ActionPack Business or Enterprise only.</remarks>
	public class ACView : RelativeLayout
	{
		#region Public Static Methods
		/// <summary>
		/// Decodes the <c>LayoutParameters</c> for the given <c>View</c> and returns the <c>Height</c> property
		/// </summary>
		/// <returns>The view height.</returns>
		/// <param name="view">View.</param>
		public static int GetViewHeight(View view){
			int height = 0;

			//Determine the layout type
			if (view.LayoutParameters is FrameLayout.LayoutParams) {
				//Cast into parent type
				FrameLayout.LayoutParams frameLayout = (FrameLayout.LayoutParams)view.LayoutParameters;

				//Read property
				height = frameLayout.Height;
			} else if (view.LayoutParameters is GridLayout.LayoutParams) {
				//Cast into parent type
				GridLayout.LayoutParams gridLayout = (GridLayout.LayoutParams)view.LayoutParameters;

				//Read property
				height = gridLayout.Height;
			} else if (view.LayoutParameters is LinearLayout.LayoutParams) {
				//Cast into parent type
				LinearLayout.LayoutParams linearLayout = (LinearLayout.LayoutParams)view.LayoutParameters;

				//Read property
				height = linearLayout.Height;
			} else if (view.LayoutParameters is RelativeLayout.LayoutParams) {
				//Cast into parent type
				RelativeLayout.LayoutParams relativeLayout = (RelativeLayout.LayoutParams)view.LayoutParameters;

				//Read property
				height = relativeLayout.Height;
			} else if (view.LayoutParameters is TableLayout.LayoutParams) {
				//Cast into parent type
				TableLayout.LayoutParams tableLayout = (TableLayout.LayoutParams)view.LayoutParameters;

				//Read property
				height = tableLayout.Height;
			} else if (view.LayoutParameters is TableRow.LayoutParams) {
				//Cast into parent type
				TableRow.LayoutParams tableRow = (TableRow.LayoutParams)view.LayoutParameters;

				//Read property
				height = tableRow.Height;
			}

			//Return height
			return height;
		}

		/// <summary>
		/// Decodes the <c>LayoutParameters</c> for the given <c>View</c> and sets the <c>Height</c> property
		/// </summary>
		/// <param name="view">View.</param>
		/// <param name="height">Height.</param>
		public static void SetViewHeight(View view, int height){

			//Determine the layout type
			if (view.LayoutParameters is FrameLayout.LayoutParams) {
				//Cast into parent type
				FrameLayout.LayoutParams frameLayout = (FrameLayout.LayoutParams)view.LayoutParameters;

				//Write property
				frameLayout.Height= height;

				//Update view
				view.LayoutParameters = frameLayout;
			} else if (view.LayoutParameters is GridLayout.LayoutParams) {
				//Cast into parent type
				GridLayout.LayoutParams gridLayout = (GridLayout.LayoutParams)view.LayoutParameters;

				//Write property
				gridLayout.Height=height;

				//Update view
				view.LayoutParameters = gridLayout;
			} else if (view.LayoutParameters is LinearLayout.LayoutParams) {
				//Cast into parent type
				LinearLayout.LayoutParams linearLayout = (LinearLayout.LayoutParams)view.LayoutParameters;

				//Write property
				linearLayout.Height=height;

				//Update view
				view.LayoutParameters = linearLayout;
			} else if (view.LayoutParameters is RelativeLayout.LayoutParams) {
				//Cast into parent type
				RelativeLayout.LayoutParams relativeLayout = (RelativeLayout.LayoutParams)view.LayoutParameters;

				//Write property
				relativeLayout.Height=height;

				//Update view
				view.LayoutParameters = relativeLayout;
			} else if (view.LayoutParameters is TableLayout.LayoutParams) {
				//Cast into parent type
				TableLayout.LayoutParams tableLayout = (TableLayout.LayoutParams)view.LayoutParameters;

				//Write property
				tableLayout.Height=height;

				//Update view
				view.LayoutParameters = tableLayout;
			} else if (view.LayoutParameters is TableRow.LayoutParams) {
				//Cast into parent type
				TableRow.LayoutParams tableRow = (TableRow.LayoutParams)view.LayoutParameters;

				//Write property
				tableRow.Height=height;

				//Update view
				view.LayoutParameters = tableRow;
			}
		}

		/// <summary>
		/// Decodes the <c>LayoutParameters</c> for the given <c>View</c> and returns the <c>Width</c> property
		/// </summary>
		/// <returns>The view Width.</returns>
		/// <param name="view">View.</param>
		public static int GetViewWidth(View view){
			int Width = 0;

			//Determine the layout type
			if (view.LayoutParameters is FrameLayout.LayoutParams) {
				//Cast into parent type
				FrameLayout.LayoutParams frameLayout = (FrameLayout.LayoutParams)view.LayoutParameters;

				//Read property
				Width = frameLayout.Width;
			} else if (view.LayoutParameters is GridLayout.LayoutParams) {
				//Cast into parent type
				GridLayout.LayoutParams gridLayout = (GridLayout.LayoutParams)view.LayoutParameters;

				//Read property
				Width = gridLayout.Width;
			} else if (view.LayoutParameters is LinearLayout.LayoutParams) {
				//Cast into parent type
				LinearLayout.LayoutParams linearLayout = (LinearLayout.LayoutParams)view.LayoutParameters;

				//Read property
				Width = linearLayout.Width;
			} else if (view.LayoutParameters is RelativeLayout.LayoutParams) {
				//Cast into parent type
				RelativeLayout.LayoutParams relativeLayout = (RelativeLayout.LayoutParams)view.LayoutParameters;

				//Read property
				Width = relativeLayout.Width;
			} else if (view.LayoutParameters is TableLayout.LayoutParams) {
				//Cast into parent type
				TableLayout.LayoutParams tableLayout = (TableLayout.LayoutParams)view.LayoutParameters;

				//Read property
				Width = tableLayout.Width;
			} else if (view.LayoutParameters is TableRow.LayoutParams) {
				//Cast into parent type
				TableRow.LayoutParams tableRow = (TableRow.LayoutParams)view.LayoutParameters;

				//Read property
				Width = tableRow.Width;
			}

			//Return value
			return Width;
		}

		/// <summary>
		/// Decodes the <c>LayoutParameters</c> for the given <c>View</c> and sets the <c>Width</c> property
		/// </summary>
		/// <param name="view">View.</param>
		/// <param name="height">Width.</param>
		public static void SetViewWidth(View view, int Width){

			//Determine the layout type
			if (view.LayoutParameters is FrameLayout.LayoutParams) {
				//Cast into parent type
				FrameLayout.LayoutParams frameLayout = (FrameLayout.LayoutParams)view.LayoutParameters;

				//Write property
				frameLayout.Width= Width;

				//Update view
				view.LayoutParameters = frameLayout;
			} else if (view.LayoutParameters is GridLayout.LayoutParams) {
				//Cast into parent type
				GridLayout.LayoutParams gridLayout = (GridLayout.LayoutParams)view.LayoutParameters;

				//Write property
				gridLayout.Width=Width;

				//Update view
				view.LayoutParameters = gridLayout;
			} else if (view.LayoutParameters is LinearLayout.LayoutParams) {
				//Cast into parent type
				LinearLayout.LayoutParams linearLayout = (LinearLayout.LayoutParams)view.LayoutParameters;

				//Write property
				linearLayout.Width=Width;

				//Update view
				view.LayoutParameters = linearLayout;
			} else if (view.LayoutParameters is RelativeLayout.LayoutParams) {
				//Cast into parent type
				RelativeLayout.LayoutParams relativeLayout = (RelativeLayout.LayoutParams)view.LayoutParameters;

				//Write property
				relativeLayout.Width=Width;

				//Update view
				view.LayoutParameters = relativeLayout;
			} else if (view.LayoutParameters is TableLayout.LayoutParams) {
				//Cast into parent type
				TableLayout.LayoutParams tableLayout = (TableLayout.LayoutParams)view.LayoutParameters;

				//Write property
				tableLayout.Width=Width;

				//Update view
				view.LayoutParameters = tableLayout;
			} else if (view.LayoutParameters is TableRow.LayoutParams) {
				//Cast into parent type
				TableRow.LayoutParams tableRow = (TableRow.LayoutParams)view.LayoutParameters;

				//Write property
				tableRow.Width=Width;

				//Update view
				view.LayoutParameters = tableRow;
			}
		}

		/// <summary>
		/// Decodes the <c>LayoutParameters</c> for the given <c>View</c> and returns the <c>LeftMargin</c> property
		/// </summary>
		/// <returns>The view LeftMargin.</returns>
		/// <param name="view">View.</param>
		public static int GetViewLeftMargin(View view){
			int LeftMargin = 0;

			//Determine the layout type
			if (view.LayoutParameters is FrameLayout.LayoutParams) {
				//Cast into parent type
				FrameLayout.LayoutParams frameLayout = (FrameLayout.LayoutParams)view.LayoutParameters;

				//Read property
				LeftMargin = frameLayout.LeftMargin;
			} else if (view.LayoutParameters is GridLayout.LayoutParams) {
				//Cast into parent type
				GridLayout.LayoutParams gridLayout = (GridLayout.LayoutParams)view.LayoutParameters;

				//Read property
				LeftMargin = gridLayout.LeftMargin;
			} else if (view.LayoutParameters is LinearLayout.LayoutParams) {
				//Cast into parent type
				LinearLayout.LayoutParams linearLayout = (LinearLayout.LayoutParams)view.LayoutParameters;

				//Read property
				LeftMargin = linearLayout.LeftMargin;
			} else if (view.LayoutParameters is RelativeLayout.LayoutParams) {
				//Cast into parent type
				RelativeLayout.LayoutParams relativeLayout = (RelativeLayout.LayoutParams)view.LayoutParameters;

				//Read property
				LeftMargin = relativeLayout.LeftMargin;
			} else if (view.LayoutParameters is TableLayout.LayoutParams) {
				//Cast into parent type
				TableLayout.LayoutParams tableLayout = (TableLayout.LayoutParams)view.LayoutParameters;

				//Read property
				LeftMargin = tableLayout.LeftMargin;
			} else if (view.LayoutParameters is TableRow.LayoutParams) {
				//Cast into parent type
				TableRow.LayoutParams tableRow = (TableRow.LayoutParams)view.LayoutParameters;

				//Read property
				LeftMargin = tableRow.LeftMargin;
			}

			//Return value
			return LeftMargin;
		}

		/// <summary>
		/// Decodes the <c>LayoutParameters</c> for the given <c>View</c> and sets the <c>LeftMargin</c> property
		/// </summary>
		/// <param name="view">View.</param>
		/// <param name="height">LeftMargin.</param>
		public static void SetViewLeftMargin(View view, int LeftMargin){

			//Determine the layout type
			if (view.LayoutParameters is FrameLayout.LayoutParams) {
				//Cast into parent type
				FrameLayout.LayoutParams frameLayout = (FrameLayout.LayoutParams)view.LayoutParameters;

				//Write property
				frameLayout.LeftMargin= LeftMargin;

				//Update view
				view.LayoutParameters = frameLayout;
			} else if (view.LayoutParameters is GridLayout.LayoutParams) {
				//Cast into parent type
				GridLayout.LayoutParams gridLayout = (GridLayout.LayoutParams)view.LayoutParameters;

				//Write property
				gridLayout.LeftMargin=LeftMargin;

				//Update view
				view.LayoutParameters = gridLayout;
			} else if (view.LayoutParameters is LinearLayout.LayoutParams) {
				//Cast into parent type
				LinearLayout.LayoutParams linearLayout = (LinearLayout.LayoutParams)view.LayoutParameters;

				//Write property
				linearLayout.LeftMargin=LeftMargin;

				//Update view
				view.LayoutParameters = linearLayout;
			} else if (view.LayoutParameters is RelativeLayout.LayoutParams) {
				//Cast into parent type
				RelativeLayout.LayoutParams relativeLayout = (RelativeLayout.LayoutParams)view.LayoutParameters;

				//Write property
				relativeLayout.LeftMargin=LeftMargin;

				//Update view
				view.LayoutParameters = relativeLayout;
			} else if (view.LayoutParameters is TableLayout.LayoutParams) {
				//Cast into parent type
				TableLayout.LayoutParams tableLayout = (TableLayout.LayoutParams)view.LayoutParameters;

				//Write property
				tableLayout.LeftMargin=LeftMargin;

				//Update view
				view.LayoutParameters = tableLayout;
			} else if (view.LayoutParameters is TableRow.LayoutParams) {
				//Cast into parent type
				TableRow.LayoutParams tableRow = (TableRow.LayoutParams)view.LayoutParameters;

				//Write property
				tableRow.LeftMargin=LeftMargin;

				//Update view
				view.LayoutParameters = tableRow;
			}
		}

		/// <summary>
		/// Decodes the <c>LayoutParameters</c> for the given <c>View</c> and returns the <c>RightMargin</c> property
		/// </summary>
		/// <returns>The view RightMargin.</returns>
		/// <param name="view">View.</param>
		public static int GetViewRightMargin(View view){
			int RightMargin = 0;

			//Determine the layout type
			if (view.LayoutParameters is FrameLayout.LayoutParams) {
				//Cast into parent type
				FrameLayout.LayoutParams frameLayout = (FrameLayout.LayoutParams)view.LayoutParameters;

				//Read property
				RightMargin = frameLayout.RightMargin;
			} else if (view.LayoutParameters is GridLayout.LayoutParams) {
				//Cast into parent type
				GridLayout.LayoutParams gridLayout = (GridLayout.LayoutParams)view.LayoutParameters;

				//Read property
				RightMargin = gridLayout.RightMargin;
			} else if (view.LayoutParameters is LinearLayout.LayoutParams) {
				//Cast into parent type
				LinearLayout.LayoutParams linearLayout = (LinearLayout.LayoutParams)view.LayoutParameters;

				//Read property
				RightMargin = linearLayout.RightMargin;
			} else if (view.LayoutParameters is RelativeLayout.LayoutParams) {
				//Cast into parent type
				RelativeLayout.LayoutParams relativeLayout = (RelativeLayout.LayoutParams)view.LayoutParameters;

				//Read property
				RightMargin = relativeLayout.RightMargin;
			} else if (view.LayoutParameters is TableLayout.LayoutParams) {
				//Cast into parent type
				TableLayout.LayoutParams tableLayout = (TableLayout.LayoutParams)view.LayoutParameters;

				//Read property
				RightMargin = tableLayout.RightMargin;
			} else if (view.LayoutParameters is TableRow.LayoutParams) {
				//Cast into parent type
				TableRow.LayoutParams tableRow = (TableRow.LayoutParams)view.LayoutParameters;

				//Read property
				RightMargin = tableRow.RightMargin;
			}

			//Return value
			return RightMargin;
		}

		/// <summary>
		/// Decodes the <c>LayoutParameters</c> for the given <c>View</c> and sets the <c>RightMargin</c> property
		/// </summary>
		/// <param name="view">View.</param>
		/// <param name="height">RightMargin.</param>
		public static void SetViewRightMargin(View view, int RightMargin){

			//Determine the layout type
			if (view.LayoutParameters is FrameLayout.LayoutParams) {
				//Cast into parent type
				FrameLayout.LayoutParams frameLayout = (FrameLayout.LayoutParams)view.LayoutParameters;

				//Write property
				frameLayout.RightMargin= RightMargin;

				//Update view
				view.LayoutParameters = frameLayout;
			} else if (view.LayoutParameters is GridLayout.LayoutParams) {
				//Cast into parent type
				GridLayout.LayoutParams gridLayout = (GridLayout.LayoutParams)view.LayoutParameters;

				//Write property
				gridLayout.RightMargin=RightMargin;

				//Update view
				view.LayoutParameters = gridLayout;
			} else if (view.LayoutParameters is LinearLayout.LayoutParams) {
				//Cast into parent type
				LinearLayout.LayoutParams linearLayout = (LinearLayout.LayoutParams)view.LayoutParameters;

				//Write property
				linearLayout.RightMargin=RightMargin;

				//Update view
				view.LayoutParameters = linearLayout;
			} else if (view.LayoutParameters is RelativeLayout.LayoutParams) {
				//Cast into parent type
				RelativeLayout.LayoutParams relativeLayout = (RelativeLayout.LayoutParams)view.LayoutParameters;

				//Write property
				relativeLayout.RightMargin=RightMargin;

				//Update view
				view.LayoutParameters = relativeLayout;
			} else if (view.LayoutParameters is TableLayout.LayoutParams) {
				//Cast into parent type
				TableLayout.LayoutParams tableLayout = (TableLayout.LayoutParams)view.LayoutParameters;

				//Write property
				tableLayout.RightMargin=RightMargin;

				//Update view
				view.LayoutParameters = tableLayout;
			} else if (view.LayoutParameters is TableRow.LayoutParams) {
				//Cast into parent type
				TableRow.LayoutParams tableRow = (TableRow.LayoutParams)view.LayoutParameters;

				//Write property
				tableRow.RightMargin=RightMargin;

				//Update view
				view.LayoutParameters = tableRow;
			}
		}

		/// <summary>
		/// Decodes the <c>LayoutParameters</c> for the given <c>View</c> and returns the <c>TopMargin</c> property
		/// </summary>
		/// <returns>The view TopMargin.</returns>
		/// <param name="view">View.</param>
		public static int GetViewTopMargin(View view){
			int TopMargin = 0;

			//Determine the layout type
			if (view.LayoutParameters is FrameLayout.LayoutParams) {
				//Cast into parent type
				FrameLayout.LayoutParams frameLayout = (FrameLayout.LayoutParams)view.LayoutParameters;

				//Read property
				TopMargin = frameLayout.TopMargin;
			} else if (view.LayoutParameters is GridLayout.LayoutParams) {
				//Cast into parent type
				GridLayout.LayoutParams gridLayout = (GridLayout.LayoutParams)view.LayoutParameters;

				//Read property
				TopMargin = gridLayout.TopMargin;
			} else if (view.LayoutParameters is LinearLayout.LayoutParams) {
				//Cast into parent type
				LinearLayout.LayoutParams linearLayout = (LinearLayout.LayoutParams)view.LayoutParameters;

				//Read property
				TopMargin = linearLayout.TopMargin;
			} else if (view.LayoutParameters is RelativeLayout.LayoutParams) {
				//Cast into parent type
				RelativeLayout.LayoutParams relativeLayout = (RelativeLayout.LayoutParams)view.LayoutParameters;

				//Read property
				TopMargin = relativeLayout.TopMargin;
			} else if (view.LayoutParameters is TableLayout.LayoutParams) {
				//Cast into parent type
				TableLayout.LayoutParams tableLayout = (TableLayout.LayoutParams)view.LayoutParameters;

				//Read property
				TopMargin = tableLayout.TopMargin;
			} else if (view.LayoutParameters is TableRow.LayoutParams) {
				//Cast into parent type
				TableRow.LayoutParams tableRow = (TableRow.LayoutParams)view.LayoutParameters;

				//Read property
				TopMargin = tableRow.TopMargin;
			}

			//Return value
			return TopMargin;
		}

		/// <summary>
		/// Decodes the <c>LayoutParameters</c> for the given <c>View</c> and sets the <c>TopMargin</c> property
		/// </summary>
		/// <param name="view">View.</param>
		/// <param name="height">TopMargin.</param>
		public static void SetViewTopMargin(View view, int TopMargin){

			//Determine the layout type
			if (view.LayoutParameters is FrameLayout.LayoutParams) {
				//Cast into parent type
				FrameLayout.LayoutParams frameLayout = (FrameLayout.LayoutParams)view.LayoutParameters;

				//Write property
				frameLayout.TopMargin= TopMargin;

				//Update view
				view.LayoutParameters = frameLayout;
			} else if (view.LayoutParameters is GridLayout.LayoutParams) {
				//Cast into parent type
				GridLayout.LayoutParams gridLayout = (GridLayout.LayoutParams)view.LayoutParameters;

				//Write property
				gridLayout.TopMargin=TopMargin;

				//Update view
				view.LayoutParameters = gridLayout;
			} else if (view.LayoutParameters is LinearLayout.LayoutParams) {
				//Cast into parent type
				LinearLayout.LayoutParams linearLayout = (LinearLayout.LayoutParams)view.LayoutParameters;

				//Write property
				linearLayout.TopMargin=TopMargin;

				//Update view
				view.LayoutParameters = linearLayout;
			} else if (view.LayoutParameters is RelativeLayout.LayoutParams) {
				//Cast into parent type
				RelativeLayout.LayoutParams relativeLayout = (RelativeLayout.LayoutParams)view.LayoutParameters;

				//Write property
				relativeLayout.TopMargin=TopMargin;

				//Update view
				view.LayoutParameters = relativeLayout;
			} else if (view.LayoutParameters is TableLayout.LayoutParams) {
				//Cast into parent type
				TableLayout.LayoutParams tableLayout = (TableLayout.LayoutParams)view.LayoutParameters;

				//Write property
				tableLayout.TopMargin=TopMargin;

				//Update view
				view.LayoutParameters = tableLayout;
			} else if (view.LayoutParameters is TableRow.LayoutParams) {
				//Cast into parent type
				TableRow.LayoutParams tableRow = (TableRow.LayoutParams)view.LayoutParameters;

				//Write property
				tableRow.TopMargin=TopMargin;

				//Update view
				view.LayoutParameters = tableRow;
			}
		}

		/// <summary>
		/// Decodes the <c>LayoutParameters</c> for the given <c>View</c> and returns the <c>BottomMargin</c> property
		/// </summary>
		/// <returns>The view BottomMargin.</returns>
		/// <param name="view">View.</param>
		public static int GetViewBottomMargin(View view){
			int BottomMargin = 0;

			//Determine the layout type
			if (view.LayoutParameters is FrameLayout.LayoutParams) {
				//Cast into parent type
				FrameLayout.LayoutParams frameLayout = (FrameLayout.LayoutParams)view.LayoutParameters;

				//Read property
				BottomMargin = frameLayout.BottomMargin;
			} else if (view.LayoutParameters is GridLayout.LayoutParams) {
				//Cast into parent type
				GridLayout.LayoutParams gridLayout = (GridLayout.LayoutParams)view.LayoutParameters;

				//Read property
				BottomMargin = gridLayout.BottomMargin;
			} else if (view.LayoutParameters is LinearLayout.LayoutParams) {
				//Cast into parent type
				LinearLayout.LayoutParams linearLayout = (LinearLayout.LayoutParams)view.LayoutParameters;

				//Read property
				BottomMargin = linearLayout.BottomMargin;
			} else if (view.LayoutParameters is RelativeLayout.LayoutParams) {
				//Cast into parent type
				RelativeLayout.LayoutParams relativeLayout = (RelativeLayout.LayoutParams)view.LayoutParameters;

				//Read property
				BottomMargin = relativeLayout.BottomMargin;
			} else if (view.LayoutParameters is TableLayout.LayoutParams) {
				//Cast into parent type
				TableLayout.LayoutParams tableLayout = (TableLayout.LayoutParams)view.LayoutParameters;

				//Read property
				BottomMargin = tableLayout.BottomMargin;
			} else if (view.LayoutParameters is TableRow.LayoutParams) {
				//Cast into parent type
				TableRow.LayoutParams tableRow = (TableRow.LayoutParams)view.LayoutParameters;

				//Read property
				BottomMargin = tableRow.BottomMargin;
			}

			//Return value
			return BottomMargin;
		}

		/// <summary>
		/// Decodes the <c>LayoutParameters</c> for the given <c>View</c> and sets the <c>BottomMargin</c> property
		/// </summary>
		/// <param name="view">View.</param>
		/// <param name="height">BottomMargin.</param>
		public static void SetViewBottomMargin(View view, int BottomMargin){

			//Determine the layout type
			if (view.LayoutParameters is FrameLayout.LayoutParams) {
				//Cast into parent type
				FrameLayout.LayoutParams frameLayout = (FrameLayout.LayoutParams)view.LayoutParameters;

				//Write property
				frameLayout.BottomMargin= BottomMargin;

				//Update view
				view.LayoutParameters = frameLayout;
			} else if (view.LayoutParameters is GridLayout.LayoutParams) {
				//Cast into parent type
				GridLayout.LayoutParams gridLayout = (GridLayout.LayoutParams)view.LayoutParameters;

				//Write property
				gridLayout.BottomMargin=BottomMargin;

				//Update view
				view.LayoutParameters = gridLayout;
			} else if (view.LayoutParameters is LinearLayout.LayoutParams) {
				//Cast into parent type
				LinearLayout.LayoutParams linearLayout = (LinearLayout.LayoutParams)view.LayoutParameters;

				//Write property
				linearLayout.BottomMargin=BottomMargin;

				//Update view
				view.LayoutParameters = linearLayout;
			} else if (view.LayoutParameters is RelativeLayout.LayoutParams) {
				//Cast into parent type
				RelativeLayout.LayoutParams relativeLayout = (RelativeLayout.LayoutParams)view.LayoutParameters;

				//Write property
				relativeLayout.BottomMargin=BottomMargin;

				//Update view
				view.LayoutParameters = relativeLayout;
			} else if (view.LayoutParameters is TableLayout.LayoutParams) {
				//Cast into parent type
				TableLayout.LayoutParams tableLayout = (TableLayout.LayoutParams)view.LayoutParameters;

				//Write property
				tableLayout.BottomMargin=BottomMargin;

				//Update view
				view.LayoutParameters = tableLayout;
			} else if (view.LayoutParameters is TableRow.LayoutParams) {
				//Cast into parent type
				TableRow.LayoutParams tableRow = (TableRow.LayoutParams)view.LayoutParameters;

				//Write property
				tableRow.BottomMargin=BottomMargin;

				//Update view
				view.LayoutParameters = tableRow;
			}
		}
		#endregion 

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
		/// Initializes a new instance of the <see cref="ActionComponents.ACView"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		public ACView (Context context) : base(context)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACView"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attr">Attr.</param>
		public ACView (Context context, IAttributeSet attr) : base(context,attr)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACView"/> class.
		/// </summary>
		/// <param name="javaReference">Java reference.</param>
		/// <param name="transfer">Transfer.</param>
		public ACView (IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference,transfer)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACView"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attr">Attr.</param>
		/// <param name="defStyle">Def style.</param>
		public ACView (Context context, IAttributeSet attr, int defStyle) : base(context,attr,defStyle)
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
		/// Moves this <see cref="ActionComponents.ACView"/> to the given point and honors any
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
		/// Moves this <see cref="ActionComponents.ACView"/> to the given point and honors any
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
		/// Resize this <see cref="ActionComponents.ACView"/> to the specified width and height.
		/// </summary>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		public void Resize(int width, int height){
			//Resize this view
			LayoutWidth = width;
			LayoutHeight = height;
		}

		/// <summary>
		/// Test to see if the given x and y coordinates are inside this <see cref="ActionComponents.ACView"/> 
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
		/// Test to see if the given point is inside this <see cref="ActionComponents.ACView"/>
		/// </summary>
		/// <returns><c>true</c>, if inside was pointed, <c>false</c> otherwise.</returns>
		/// <param name="pt">Point.</param>
		public bool PointInside(Point pt){
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
		/// calling <c>Dispose()</c> doesn't always release the child bitmaps in the <c>ImageView</c>'s <c>Image</c> property.</remarks>
		public void Purge(bool forceGarbageCollection){
			object subview;

			//Try to release any subviews held by this view
			for(int i=ChildCount;i>=0;i--){
				//Grab the child view
				subview = GetChildAt (i);

				//Trap any errors
				try{
					//Do special processing on any other Action Component in this view
					if (subview is ACImageView) {
						//Release any memory contained in this ImageView's image
						((ACImageView)subview).DisposeImage();
					} else if (subview is ACView) {
						//Call child's purge routine
						((ACView)subview).Purge (false);
					}

					//Attempt to remove the view
					RemoveViewAt (i);
				}
				catch {
					#if DEBUG
					//Report disposal issue
					Console.WriteLine ("Unable to purge {0}", subview);
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

					return true;
				}
				break;
			case MotionEventActions.Up:
				//Clear any drag action
				_dragging=false;

				//Inform caller of event
				RaiseReleased ();

				#if TRIAL
					Android.Widget.Toast.MakeText(this.Context, "ACView by Appracatappra, LLC.", Android.Widget.ToastLength.Short).Show();
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

