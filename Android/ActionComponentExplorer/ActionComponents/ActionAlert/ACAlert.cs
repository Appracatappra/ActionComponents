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
	/// The <see cref="ActionComponents.ACAlert"/> is a custom <c>RelativeLayout</c> with built-in helper routines to automatically handle
	/// user interaction such as dragging (with optional constraints on the x and y axis), events such as <c>Touched</c>, <c>Moved</c> and <c>Released</c> and moving,
	/// and resizing. The <see cref="ActionComponents.ACAlert"/> includes a <c>Purge</c> method to release the memory being
	/// held by it's subviews and layouts. 
	/// </summary>
	/// <remarks>NOTICE: The <see cref="ActionComponents.ACAlert"/> works best if hosted inside of a <c>RelativeLayout</c>.
	/// Available in ActionPack Business or Enterprise only.</remarks>
	public class ACAlert : RelativeLayout
	{
		#region Embedded ACView Static Methods
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

		#region Static Methods
		/// <summary>
		/// Creates a new <see cref="ActionComponents.ACAlert"/> and displays it. If the user taps the alert,
		/// it will automatically close.
		/// </summary>
		/// <returns>The alert.</returns>
		/// <param name="context">Context.</param>
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		public static ACAlert ShowAlert(Context context, string title, string description) {
			//Build new 
			ACAlert alert = new ACAlert (context, ACAlertType.Default, ACAlertLocation.Middle, title, description);

			//Wire-up auto close handler
			alert.Touched += (a) => {
				a.Hide();
			};

			//Show alert
			alert.Show ();

			//Return new alert
			return alert;
		}

		/// <summary>
		/// Creates a new <see cref="ActionComponents.ACAlert"/> with the given buttons and displays it.
		/// </summary>
		/// <returns>The alert.</returns>
		/// <param name="context">Context.</param>
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		/// <param name="buttonTitles">Button titles.</param>
		public static ACAlert ShowAlert(Context context, string title, string description, string buttonTitles) {
			//Build new 
			ACAlert alert = new ACAlert (context, ACAlertType.Default, ACAlertLocation.Middle, title, description, buttonTitles);

			//Show alert
			alert.Show ();

			//Return new alert
			return alert;
		}

		/// <summary>
		/// Creates a new <see cref="ActionComponents.ACAlert"/> with the given buttons and displays it.
		/// </summary>
		/// <returns>The alert.</returns>
		/// <param name="context">Context.</param>
		/// <param name="icon">Icon.</param>
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		/// <param name="buttonTitles">Button titles.</param>
		public static ACAlert ShowAlert(Context context, int icon, string title, string description, string buttonTitles) {
			//Build new 
			ACAlert alert = new ACAlert (context, ACAlertType.Default, ACAlertLocation.Middle, icon, title, description, buttonTitles);

			//Show alert
			alert.Show ();

			//Return new alert
			return alert;
		}

		/// <summary>
		/// Creates a new <see cref="ActionComponents.ACAlert"/> and displays it at the given location. If the user taps the alert,
		/// it will automatically close.
		/// </summary>
		/// <returns>The alert.</returns>
		/// <param name="context">Context.</param>
		/// <param name="location">Location.</param>
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		public static ACAlert ShowAlert(Context context, ACAlertLocation location, string title, string description) {
			//Build new 
			ACAlert alert = new ACAlert (context, ACAlertType.Default, location, title, description);

			//Wire-up auto close handler
			alert.Touched += (a) => {
				a.Hide();
			};

			//Show alert
			alert.Show ();

			//Return new alert
			return alert;
		}

		/// <summary>
		/// Creates a new <see cref="ActionComponents.ACAlert"/> and displays it. If the user taps the alert,
		/// it will automatically close.
		/// </summary>
		/// <returns>The alert.</returns>
		/// <param name="context">Context.</param>
		/// <param name="icon">Icon.</param>
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		public static ACAlert ShowAlert(Context context, int icon, string title, string description) {
			//Build new 
			ACAlert alert = new ACAlert (context, ACAlertType.Default, ACAlertLocation.Middle, icon, title, description);

			//Wire-up auto close handler
			alert.Touched += (a) => {
				a.Hide();
			};

			//Show alert
			alert.Show ();

			//Return new alert
			return alert;
		}

		/// <summary>
		/// Creates a new <see cref="ActionComponents.ACAlert"/> and displays it at the given location. If the user taps the alert,
		/// it will automatically close.
		/// </summary>
		/// <returns>The alert.</returns>
		/// <param name="context">Context.</param>
		/// <param name="location">Location.</param>
		/// <param name="icon">Icon.</param>
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		public static ACAlert ShowAlert(Context context, ACAlertLocation location, int icon, string title, string description) {
			//Build new 
			ACAlert alert = new ACAlert (context, ACAlertType.Default, location, icon, title, description);

			//Wire-up auto close handler
			alert.Touched += (a) => {
				a.Hide();
			};

			//Show alert
			alert.Show ();

			//Return new alert
			return alert;
		}

		/// <summary>
		/// Creates a new <see cref="ActionComponents.ACAlert"/> with an OK button and displays it. If the user taps the OK button,
		/// the alert will automatically close.
		/// </summary>
		/// <returns>The alert</returns>
		/// <param name="context">Context.</param>
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		public static ACAlert ShowAlertOK(Context context, string title, string description) {
			//Build new 
			ACAlert alert = new ACAlert (context, ACAlertType.Default, ACAlertLocation.Middle, title, description, new string []{"OK"});

			//Wire-up auto close handler
			alert.ButtonReleased += (button) => {
				//Close when a button is pressed
				alert.Hide();
			};

			//Show alert
			alert.Show ();

			//Return new alert
			return alert;
		}

		/// <summary>
		/// Creates a new <see cref="ActionComponents.ACAlert"/> with an OK button and displays it. If the user taps the OK button,
		/// the alert will automatically close.
		/// </summary>
		/// <returns>The alert</returns>
		/// <param name="context">Context.</param>
		/// <param name="icon">Icon.</param>
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		public static ACAlert ShowAlertOK(Context context, int icon, string title, string description) {
			//Build new 
			ACAlert alert = new ACAlert (context, ACAlertType.Default, ACAlertLocation.Middle, icon, title, description, new string []{"OK"});

			//Wire-up auto close handler
			alert.ButtonReleased += (button) => {
				//Close when a button is pressed
				alert.Hide();
			};

			//Show alert
			alert.Show ();

			//Return new alert
			return alert;
		}

		/// <summary>
		/// Creates a new <see cref="ActionComponents.ACAlert"/> with an OK/Cancel buttons and displays it. If the user taps the OK button,
		/// the alert will automatically close.
		/// </summary>
		/// <returns>The alert OK cancel.</returns>
		/// <param name="context">Context.</param>
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		public static ACAlert ShowAlertOKCancel(Context context, string title, string description) {
			//Build new 
			ACAlert alert = new ACAlert (context, ACAlertType.Default, ACAlertLocation.Middle, title, description, new string []{"Cancel","OK"});

			//Wire-up auto close handler
			alert.ButtonReleased += (button) => {
				//Close when a button is pressed
				alert.Hide();
			};

			//Show alert
			alert.Show ();

			//Return new alert
			return alert;
		}

		/// <summary>
		/// Creates a new <see cref="ActionComponents.ACAlert"/> with an OK/Cancel buttons and displays it. If the user taps the OK button,
		/// the alert will automatically close.
		/// </summary>
		/// <returns>The alert OK cancel.</returns>
		/// <param name="context">Context.</param>
		/// <param name="icon">Icon.</param>
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		public static ACAlert ShowAlertOKCancel(Context context, int icon, string title, string description) {
			//Build new 
			ACAlert alert = new ACAlert (context, ACAlertType.Default, ACAlertLocation.Middle, icon, title, description, new string []{"Cancel","OK"});

			//Wire-up auto close handler
			alert.ButtonReleased += (button) => {
				//Close when a button is pressed
				alert.Hide();
			};

			//Show alert
			alert.Show ();

			//Return new alert
			return alert;
		}

		/// <summary>
		/// Creates a new <see cref="ActionComponents.ACAlert"/> with an embedded <c>Activity Indicator</c> and displays it. 
		/// </summary>
		/// <returns>The activity alert.</returns>
		/// <param name="context">Context.</param>
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		/// <param name="modal">If set to <c>true</c> modal.</param>
		public static ACAlert ShowActivityAlert(Context context, string title, string description, bool modal) {
			//Build new 
			ACAlert alert = new ACAlert (context, ACAlertType.ActivityAlert, ACAlertLocation.Middle, title, description);
			alert.modal = modal;

			//Show alert
			alert.Show ();

			//Return new alert
			return alert;
		}

		/// <summary>
		/// Creates a new <see cref="ActionComponents.ACAlert"/> with an embedded <c>Activity Indicator</c> and displays it.
		/// </summary>
		/// <returns>The activity alert.</returns>
		/// <param name="context">Context.</param>
		/// <param name="location">Location.</param>
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		/// <param name="modal">If set to <c>true</c> modal.</param>
		public static ACAlert ShowActivityAlert(Context context, ACAlertLocation location, string title, string description, bool modal) {
			//Build new 
			ACAlert alert = new ACAlert (context, ACAlertType.ActivityAlert, location, title, description);
			alert.modal = modal;

			//Show alert
			alert.Show ();

			//Return new alert
			return alert;
		}

		/// <summary>
		/// Creates a new <see cref="ActionComponents.ACAlert"/> with an embedded <c>Activity Indicator</c>, a <c>Cancel</c> button and displays it.
		/// The alert will automatically close if the cancel button is tapped.
		/// </summary>
		/// <returns>The activity alert cancel.</returns>
		/// <param name="context">Context.</param>
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		/// <param name="modal">If set to <c>true</c> modal.</param>
		public static ACAlert ShowActivityAlertCancel(Context context, string title, string description, bool modal) {
			//Build new 
			ACAlert alert = new ACAlert (context, ACAlertType.ActivityAlert, ACAlertLocation.Middle, title, description, new string []{"Cancel"});
			alert.modal = modal;

			//Wire-up auto close handler
			alert.ButtonReleased += (button) => {
				//Close when a button is pressed
				alert.Hide();
			};

			//Show alert
			alert.Show ();

			//Return new alert
			return alert;
		}

		/// <summary>
		/// Creates a new <see cref="ActionComponents.ACAlert"/> with an embedded <c>Activity Indicator</c>, a <c>Cancel</c> button and displays it.
		/// The alert will automatically close if the cancel button is tapped.
		/// </summary>
		/// <returns>The activity alert cancel.</returns>
		/// <param name="context">Context.</param>
		/// <param name="location">Location.</param>
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		/// <param name="modal">If set to <c>true</c> modal.</param>
		public static ACAlert ShowActivityAlertCancel(Context context, ACAlertLocation location, string title, string description, bool modal) {
			//Build new 
			ACAlert alert = new ACAlert (context, ACAlertType.ActivityAlert, location, title, description, new string []{"Cancel"});
			alert.modal = modal;

			//Wire-up auto close handler
			alert.ButtonReleased += (button) => {
				//Close when a button is pressed
				alert.Hide();
			};

			//Show alert
			alert.Show ();

			//Return new alert
			return alert;
		}

		/// <summary>
		/// Creates a new medium sized <see cref="ActionComponents.ACAlert"/> with an embedded <c>Activity Indicator</c> and displays it.
		/// </summary>
		/// <returns>The activity alert medium.</returns>
		/// <param name="context">Context.</param>
		/// <param name="title">Title.</param>
		/// <param name="modal">If set to <c>true</c> modal.</param>
		public static ACAlert ShowActivityAlertMedium(Context context, string title, bool modal) {
			//Build new 
			ACAlert alert = new ACAlert (context, ACAlertType.ActivityAlertMedium, ACAlertLocation.Middle, title, "");
			alert.modal = modal;

			//Show alert
			alert.Show ();

			//Return new alert
			return alert;
		}

		/// <summary>
		/// Creates a new <see cref="ActionComponents.ACAlert"/> with an embedded <c>Activity Indicator</c>, a <c>Cancel</c> button and displays it.
		/// The alert will automatically close if the cancel button is tapped.
		/// </summary>
		/// <returns>The activity alert medium cancel.</returns>
		/// <param name="context">Context.</param>
		/// <param name="title">Title.</param>
		/// <param name="modal">If set to <c>true</c> modal.</param>
		public static ACAlert ShowActivityAlertMediumCancel(Context context, string title, bool modal) {
			//Build new 
			ACAlert alert = new ACAlert (context, ACAlertType.ActivityAlertMedium, ACAlertLocation.Middle, title, "", new string []{"Cancel"});
			alert.modal = modal;

			//Wire-up auto close handler
			alert.ButtonReleased += (button) => {
				//Close when a button is pressed
				alert.Hide();
			};

			//Show alert
			alert.Show ();

			//Return new alert
			return alert;
		}

		/// <summary>
		/// Creates a new <see cref="ActionComponents.ACAlert"/> with an embedded <c>Progress Indicator</c> and displays it.
		/// </summary>
		/// <returns>The progress alert.</returns>
		/// <param name="context">Context.</param>
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		/// <param name="modal">If set to <c>true</c> modal.</param>
		public static ACAlert ShowProgressAlert(Context context, string title, string description, bool modal) {
			//Build new 
			ACAlert alert = new ACAlert (context, ACAlertType.ProgressAlert, ACAlertLocation.Middle, title, description);
			alert.modal = modal;

			//Show alert
			alert.Show ();

			//Return new alert
			return alert;
		}

		/// <summary>
		/// Creates a new <see cref="ActionComponents.ACAlert"/> with an embedded <c>Progress Indicator</c> and displays it.
		/// </summary>
		/// <returns>The progress alert.</returns>
		/// <param name="context">Context.</param>
		/// <param name="icon">Icon.</param>
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		/// <param name="modal">If set to <c>true</c> modal.</param>
		public static ACAlert ShowProgressAlert(Context context, int icon, string title, string description, bool modal) {
			//Build new 
			ACAlert alert = new ACAlert (context, ACAlertType.ProgressAlert, ACAlertLocation.Middle, icon, title, description);
			alert.modal = modal;

			//Show alert
			alert.Show ();

			//Return new alert
			return alert;
		}

		/// <summary>
		/// Creates a new <see cref="ActionComponents.ACAlert"/> with an embedded <c>Progress Indicator</c> and displays it.
		/// </summary>
		/// <returns>The progress alert.</returns>
		/// <param name="context">Context.</param>
		/// <param name="location">Location.</param>
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		/// <param name="modal">If set to <c>true</c> modal.</param>
		public static ACAlert ShowProgressAlert(Context context, ACAlertLocation location, string title, string description, bool modal) {
			//Build new 
			ACAlert alert = new ACAlert (context, ACAlertType.ProgressAlert, ACAlertLocation.Middle, title, description);
			alert.modal = modal;

			//Show alert
			alert.Show ();

			//Return new alert
			return alert;
		}

		/// <summary>
		/// Creates a new <see cref="ActionComponents.ACAlert"/> with an embedded <c>Progress Indicator</c> and displays it along with
		/// custom <see cref="ActionComponents.ACAlertButton"/>s for each button title provided
		/// </summary>
		/// <returns>The progress alert.</returns>
		/// <param name="context">Context.</param>
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		/// <param name="buttonTitles">A comma seperated list of Button titles.</param>
		/// <param name="modal">If set to <c>true</c> modal.</param>
		public static ACAlert ShowProgressAlert(Context context, string title, string description, string buttonTitles, bool modal) {
			//Build new 
			ACAlert alert = new ACAlert (context, ACAlertType.ProgressAlert, ACAlertLocation.Middle, title, description, buttonTitles);
			alert.modal = modal;

			//Show alert
			alert.Show ();

			//Return new alert
			return alert;
		}

		/// <summary>
		/// Creates a new <see cref="ActionComponents.ACAlert"/> with an embedded <c>Progress Indicator</c> and displays it along with
		/// custom <see cref="ActionComponents.ACAlertButton"/>s for each button title provided
		/// </summary>
		/// <returns>The progress alert.</returns>
		/// <param name="context">Context.</param>
		/// <param name="icon">Icon.</param>
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		/// <param name="buttonTitles">A comma seperated list of Button titles.</param>
		/// <param name="modal">If set to <c>true</c> modal.</param>
		public static ACAlert ShowProgressAlert(Context context, int icon, string title, string description, string buttonTitles, bool modal) {
			//Build new 
			ACAlert alert = new ACAlert (context, ACAlertType.ProgressAlert, ACAlertLocation.Middle, icon, title, description, buttonTitles);
			alert.modal = modal;

			//Show alert
			alert.Show ();

			//Return new alert
			return alert;
		}

		/// <summary>
		/// Creates a new <see cref="ActionComponents.ACAlert"/> with an embedded <c>Progress Indicator</c>, <c>Cancel</c> button and displays it.
		/// If the user presses the cancel button the alert will automatically close.
		/// </summary>
		/// <returns>The progress alert cancel.</returns>
		/// <param name="context">Context.</param>
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		/// <param name="modal">If set to <c>true</c> modal.</param>
		public static ACAlert ShowProgressAlertCancel(Context context, string title, string description, bool modal) {
			//Build new 
			ACAlert alert = new ACAlert (context, ACAlertType.ProgressAlert, ACAlertLocation.Middle, title, description, new string []{"Cancel"});
			alert.modal = modal;

			//Wire-up auto close handler
			alert.ButtonReleased += (button) => {
				//Close when a button is pressed
				alert.Hide();
			};

			//Show alert
			alert.Show ();

			//Return new alert
			return alert;
		}

		/// <summary>
		/// Creates a new <see cref="ActionComponents.ACAlert"/> with an embedded <c>Progress Indicator</c>, <c>Cancel</c> button and displays it.
		/// If the user presses the cancel button the alert will automatically close.
		/// </summary>
		/// <returns>The progress alert cancel.</returns>
		/// <param name="context">Context.</param>
		/// <param name="icon">Icon.</param>
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		/// <param name="modal">If set to <c>true</c> modal.</param>
		public static ACAlert ShowProgressAlertCancel(Context context, int icon, string title, string description, bool modal) {
			//Build new 
			ACAlert alert = new ACAlert (context, ACAlertType.ProgressAlert, ACAlertLocation.Middle, icon, title, description, new string []{"Cancel"});
			alert.modal = modal;

			//Wire-up auto close handler
			alert.ButtonReleased += (button) => {
				//Close when a button is pressed
				alert.Hide();
			};

			//Show alert
			alert.Show ();

			//Return new alert
			return alert;
		}
		#endregion 

		#region Private Properties
		private Context _context;
		private Bitmap _imageCache;
		private ACAlertType _type;
		private ACAlertAppearance _appearance;
		private ACAlertLocation _location;
		private ACAlertButtonAppearance _buttonAppearance;
		private ACAlertButtonAppearance _buttonAppearanceDisabled;
		private ACAlertButtonAppearance _buttonAppearanceTouched;
		private ACAlertButtonAppearance _buttonAppearanceHighlighted;
		private List<ACAlertButton> _buttons = new List<ACAlertButton> ();
		private bool _isDraggable;
		private bool _dragging;
		private bool _bringToFrontOnTouched;
		private ACAlertDragConstraint _xConstraint;
		private ACAlertDragConstraint _yConstraint;
		private Point _startLocation;
		private string _title="";
		private string _description="";
		private bool _modal = true;
		private ACAlertOverlay _overlay;
		private int _icon = -1;
		private bool _hidden=true;
		private ProgressBar _activityIndicator;
		private ProgressBar _progressView;
		private RelativeLayout _subview;
		#endregion

		#region Computed Variables
		/// <summary>
		/// Gets or sets the [OPTIONAL] tag that can be assosciated with this <see cref="ActionComponents.ACAlert"/> 
		/// </summary>
		/// <value>The tag.</value>
		public Object tag { get; set; }

		/// <summary>
		/// Gets the overlay used to black out the screen for a modal <see cref="ActionComponents.ACAlert"/> 
		/// </summary>
		/// <value>The overlay.</value>
		public ACAlertOverlay Overlay {
			get { return _overlay; }
		}

		/// <summary>
		/// Gets the subview attached to this <see cref="ActionComponents.ACAlert"/>
		/// </summary>
		/// <value>The subview.</value>
		public RelativeLayout subview {
			get { return _subview; }
		}

		/// <summary>
		/// Gets the type of this <see cref="ActionComponents.ACAlert"/>
		/// </summary>
		/// <value>The type.</value>
		public ACAlertType type{
			get { return _type;}
		}

		/// <summary>
		/// Returns the embedded <c>Activity Indicator</c> for <c>ActivityAlert</c> types of <see cref="ActionComponents.ACAlert"/>s 
		/// </summary>
		/// <value>The activity indicator.</value>
		public ProgressBar activityIndicator {
			get { return _activityIndicator;}
		}

		/// <summary>
		/// Returns the embedded <c>Progress Indicator</c> for <c>ProgressAlert</c> types of <see cref="ActionComponents.ACAlert"/>s
		/// </summary>
		/// <value>The progress view.</value>
		public ProgressBar progressView {
			get { return _progressView;}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="ActionComponents.ACAlert"/> is hidden.
		/// </summary>
		/// <value><c>true</c> if hidden; otherwise, <c>false</c>.</value>
		public bool hidden{
			get { return _hidden;}
		}

		/// <summary>
		/// Gets or sets the icon displayed on this <see cref="ActionComponents.ACAlert"/> 
		/// </summary>
		/// <value>The icon.</value>
		/// <remarks>A value of negative one (-1) is used to indicate no icon present.</remarks>
		public int icon{
			get { return _icon;}
			set {
				_icon = value;
				AdjustAlertPosition ();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <see cref="ActionComponents.ACAlert"/> is modal.
		/// </summary>
		/// <value><c>true</c> if modal; otherwise, <c>false</c>.</value>
		public bool modal{
			get { return _modal;}
			set {
				_modal = value;

				//Not modal and an overlay exists?
				if (!_modal && _overlay != null) {
					//Adjust the overlay's modality
					_overlay.AdjustModality (_modal);
				}
			}
		}

		/// <summary>
		/// Gets the location of this <see cref="ActionComponents.ACAlert"/> 
		/// </summary>
		/// <value>The location.</value>
		public ACAlertLocation location{
			get { return _location;}
		}

		/// <summary>
		/// Gets or sets the appearance of this <see cref="ActionComponents.ACAlert"/> 
		/// </summary>
		/// <value>The appearance.</value>
		public ACAlertAppearance appearance{
			get { return _appearance;}
			set {
				_appearance = value;
				AdjustAlertPosition ();
				RepositionButtons ();

				//Wire-up events
				appearance.AppearanceModified += () => {
					AdjustAlertPosition ();
					RepositionButtons ();
				};

				//Adjust overlay if it exists
				if (_overlay != null)
					_overlay.AdjustAppearance ();
			}
		}

		/// <summary>
		/// Gets or sets the default <see cref="ActionComponents.ACAlertButtonAppearance"/> 
		/// </summary>
		/// <value>The button appearance.</value>
		public ACAlertButtonAppearance buttonAppearance{
			get { return _buttonAppearance;}
			set {
				_buttonAppearance = value;
				CascadeButtonAppearances (ACAlertButtonApperanceType.Normal);
			}
		}

		/// <summary>
		/// Gets or sets the default disabled <see cref="ActionComponents.ACAlertButtonAppearance"/> 
		/// </summary>
		/// <value>The button appearance disabled.</value>
		public ACAlertButtonAppearance buttonAppearanceDisabled{
			get { return _buttonAppearanceDisabled;}
			set {
				_buttonAppearanceDisabled = value;
				CascadeButtonAppearances (ACAlertButtonApperanceType.Disabled);
			}
		}

		/// <summary>
		/// Gets or sets the touched button <see cref="ActionComponents.ACAlertButtonAppearance"/>
		/// </summary>
		/// <value>The button appearance touched.</value>
		public ACAlertButtonAppearance buttonAppearanceTouched{
			get { return _buttonAppearanceTouched;}
			set {
				_buttonAppearanceTouched = value;
				CascadeButtonAppearances (ACAlertButtonApperanceType.Highlighted);
			}
		}

		/// <summary>
		/// Gets or sets the default highlighted <see cref="ActionComponents.ACAlertButtonAppearance"/> 
		/// </summary>
		/// <value>The button appearance highlighted.</value>
		public ACAlertButtonAppearance buttonAppearanceHighlighted{
			get { return _buttonAppearanceHighlighted;}
			set {
				_buttonAppearanceHighlighted = value;
				CascadeButtonAppearances (ACAlertButtonApperanceType.Touched);
			}
		}

		/// <summary>
		/// Gets or sets the title of this <see cref="ActionComponents.ACAlert"/> 
		/// </summary>
		/// <value>The title.</value>
		public string title{
			get { return _title;}
			set {
				_title = value;
				AdjustAlertPosition ();
			}
		}

		/// <summary>
		/// Gets or sets the description of this <see cref="ActionComponents.ACAlert"/> 
		/// </summary>
		/// <value>The description.</value>
		public string description{
			get { return _description;}
			set {
				_description = value;
				AdjustAlertPosition ();
			}
		}

		/// <summary>
		/// Gets the number of <see cref="ActionComponents.ACAlertButton"/>s in this <see cref="ActionComponents.ACAlert"/>  
		/// </summary>
		/// <value>The count.</value>
		public int Count{
			get { return _buttons.Count;}
		}

		/// <summary>
		/// Gets or sets the <see cref="ActionComponents.ACAlertButton"/> at the specified index.
		/// </summary>
		/// <param name="index">Index.</param>
		public ACAlertButton this[int index]
		{
			get
			{
				return _buttons[index];
			}

			set
			{
				_buttons[index] = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="ActionComponents.ACAlert"/>
		/// is draggable.
		/// </summary>
		/// <value><c>true</c> if is draggable; otherwise, <c>false</c>.</value>
		public bool draggable {
			get { return _isDraggable;}
			set { _isDraggable = value;}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="ActionComponents.ACAlert"/> is being dragged by the user.
		/// </summary>
		/// <value><c>true</c> if dragging; otherwise, <c>false</c>.</value>
		public bool dragging{
			get { return _dragging;}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="ActionComponents.ACAlert"/>
		/// is automatically brought to the front when touched.
		/// </summary>
		/// <value><c>true</c> if bring to front on touched; otherwise, <c>false</c>.</value>
		public bool bringToFrontOnTouched{
			get { return _bringToFrontOnTouched;}
			set { _bringToFrontOnTouched = value;}
		}

		/// <summary>
		/// Gets or sets the <see cref="ActionComponents.ACAlertDragConstraint"/> applied to the <c>x axis</c> of this
		/// <see cref="ActionComponents.ACAlert"/> 
		/// </summary>
		/// <value>The x constraint.</value>
		public ACAlertDragConstraint xConstraint{
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
		/// Gets or sets the <see cref="ActionComponents.ACAlertDragConstraint"/> applied to the <c>y axis</c> of this
		/// <see cref="ActionComponents.ACAlert"/> 
		/// </summary>
		/// <value>The y constraint.</value>
		public ACAlertDragConstraint yConstraint{
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
		/// Initializes a new instance of the <see cref="ActionComponents.ACAlert"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		public ACAlert (Context context) : base(context)
		{
			//Save context
			this._context = context;

			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACAlert"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attr">Attr.</param>
		public ACAlert (Context context, IAttributeSet attr) : base(context,attr)
		{
			//Save context
			this._context = context;

			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACAlert"/> class.
		/// </summary>
		/// <param name="javaReference">Java reference.</param>
		/// <param name="transfer">Transfer.</param>
		public ACAlert (IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference,transfer)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACAlert"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attr">Attr.</param>
		/// <param name="defStyle">Def style.</param>
		public ACAlert (Context context, IAttributeSet attr, int defStyle) : base(context,attr,defStyle)
		{
			//Save context
			this._context = context;

			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACAlert"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="type">Type.</param>
		/// <param name="location">Location.</param>
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		public ACAlert (Context context, ACAlertType type, ACAlertLocation location, string title, string description) : base(context)
		{
			//Save defaults
			this._context = context;
			this._type = type;
			this._location = location;
			this._title = title;
			this._description = description;

			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACAlert"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="type">Type.</param>
		/// <param name="location">Location.</param>
		/// <param name="title">Title.</param>
		/// <param name="subview">Subview.</param>
		public ACAlert (Context context, ACAlertType type, ACAlertLocation location, string title, RelativeLayout subview) : base(context)
		{
			//Save defaults
			this._context = context;
			this._type = type;
			this._location = location;
			this._title = title;
			this._subview = subview;

			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACAlert"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="type">Type.</param>
		/// <param name="location">Location.</param>
		/// <param name="icon">Icon.</param>
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		public ACAlert (Context context, ACAlertType type, ACAlertLocation location, int icon, string title, string description) : base(context)
		{
			//Save defaults
			this._context = context;
			this._type = type;
			this._location = location;
			this._icon = icon;
			this._title = title;
			this._description = description;

			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACAlert"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="type">Type.</param>
		/// <param name="location">Location.</param>
		/// <param name="icon">Icon.</param>
		/// <param name="title">Title.</param>
		/// <param name="subview">Subview.</param>
		public ACAlert (Context context, ACAlertType type, ACAlertLocation location, int icon, string title, RelativeLayout subview) : base(context)
		{
			//Save defaults
			this._context = context;
			this._type = type;
			this._location = location;
			this._icon = icon;
			this._title = title;
			this._subview = subview;

			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACAlert"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="type">Type.</param>
		/// <param name="location">Location.</param>
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		/// <param name="buttonTitles">Button titles.</param>
		public ACAlert (Context context, ACAlertType type, ACAlertLocation location, string title, string description, string[] buttonTitles) : base(context)
		{
			//Save defaults
			this._context = context;
			this._type = type;
			this._location = location;
			this._title = title;
			this._description = description;

			Initialize ();

			//Add all buttons to the alert
			AddButtons (buttonTitles);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACAlert"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="type">Type.</param>
		/// <param name="location">Location.</param>
		/// <param name="title">Title.</param>
		/// <param name="subview">Subview.</param>
		/// <param name="buttonTitles">Button titles.</param>
		public ACAlert (Context context, ACAlertType type, ACAlertLocation location, string title, RelativeLayout subview, string[] buttonTitles) : base(context)
		{
			//Save defaults
			this._context = context;
			this._type = type;
			this._location = location;
			this._title = title;
			this._subview = subview;

			Initialize ();

			//Add all buttons to the alert
			AddButtons (buttonTitles);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACAlert"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="type">Type.</param>
		/// <param name="location">Location.</param>
		/// <param name="icon">Icon.</param>
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		/// <param name="buttonTitles">Button titles.</param>
		public ACAlert (Context context, ACAlertType type, ACAlertLocation location, int icon, string title, string description, string[] buttonTitles) : base(context)
		{
			//Save defaults
			this._context = context;
			this._type = type;
			this._location = location;
			this._icon = icon;
			this._title = title;
			this._description = description;

			Initialize ();

			//Add all buttons to the alert
			AddButtons (buttonTitles);
		}

		public ACAlert (Context context, ACAlertType type, ACAlertLocation location, int icon, string title, RelativeLayout subview, string[] buttonTitles) : base(context)
		{
			//Save defaults
			this._context = context;
			this._type = type;
			this._location = location;
			this._icon = icon;
			this._title = title;
			this._subview = subview;

			Initialize ();

			//Add all buttons to the alert
			AddButtons (buttonTitles);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACAlert"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="type">Type.</param>
		/// <param name="location">Location.</param>
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		/// <param name="buttonTitles">A comma seperated Button titles.</param>
		public ACAlert (Context context, ACAlertType type, ACAlertLocation location, string title, string description, string buttonTitles) : base(context)
		{
			//Save defaults
			this._context = context;
			this._type = type;
			this._location = location;
			this._title = title;
			this._description = description;

			Initialize ();

			//Parse titles and create buttons
			char[] delimiterChars = {','};
			string[] titles = buttonTitles.Split(delimiterChars);
			AddButtons(titles);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACAlert"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="type">Type.</param>
		/// <param name="location">Location.</param>
		/// <param name="title">Title.</param>
		/// <param name="subview">Subview.</param>
		/// <param name="buttonTitles">Button titles.</param>
		public ACAlert (Context context, ACAlertType type, ACAlertLocation location, string title, RelativeLayout subview, string buttonTitles) : base(context)
		{
			//Save defaults
			this._context = context;
			this._type = type;
			this._location = location;
			this._title = title;
			this._subview = subview;

			Initialize ();

			//Parse titles and create buttons
			char[] delimiterChars = {','};
			string[] titles = buttonTitles.Split(delimiterChars);
			AddButtons(titles);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACAlert"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="type">Type.</param>
		/// <param name="location">Location.</param>
		/// <param name="icon">Icon.</param>
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		/// <param name="buttonTitles">Button titles.</param>
		public ACAlert (Context context, ACAlertType type, ACAlertLocation location, int icon, string title, string description, string buttonTitles) : base(context)
		{
			//Save defaults
			this._context = context;
			this._type = type;
			this._location = location;
			this._icon = icon;
			this._title = title;
			this._description = description;

			Initialize ();

			//Parse titles and create buttons
			char[] delimiterChars = {','};
			string[] titles = buttonTitles.Split(delimiterChars);
			AddButtons(titles);
		}

		public ACAlert (Context context, ACAlertType type, ACAlertLocation location, int icon, string title, RelativeLayout subview, string buttonTitles) : base(context)
		{
			//Save defaults
			this._context = context;
			this._type = type;
			this._location = location;
			this._icon = icon;
			this._title = title;
			this._subview = subview;

			Initialize ();

			//Parse titles and create buttons
			char[] delimiterChars = {','};
			string[] titles = buttonTitles.Split(delimiterChars);
			AddButtons(titles);
		}

		/// <summary>
		/// Initialize this instance.
		/// </summary>
		private void Initialize(){

			//Set defaults
			this.SetBackgroundColor (Color.Argb (0, 0, 0, 0));
			this._isDraggable = false;
			this._dragging = false;
			this._bringToFrontOnTouched = false;
			this._xConstraint = new ACAlertDragConstraint ();
			this._yConstraint = new ACAlertDragConstraint ();
			this._startLocation = new Point (0, 0);

			//Setup initial layout position and size 
			var layout = new RelativeLayout.LayoutParams (302,174);

			//Set position based on location requested
			switch (_location) {
			case ACAlertLocation.Top:
				layout.AddRule (LayoutRules.AlignParentTop);
				layout.AddRule (LayoutRules.CenterHorizontal);
				layout.TopMargin = 20;
				break;
			case ACAlertLocation.Middle:
				layout.AddRule (LayoutRules.CenterInParent);
				break;
			case ACAlertLocation.Bottom:
				layout.AddRule (LayoutRules.AlignParentBottom);
				layout.AddRule (LayoutRules.CenterHorizontal);
				layout.BottomMargin = 20;
				break;
			case ACAlertLocation.Custom:
				layout.TopMargin = 20;
				layout.RightMargin = 20;
				break;
			}

			this.LayoutParameters = layout;

			//Wireup change events
			this._xConstraint.ConstraintChanged+= () => {
				XConstraintModified();
			};
			this._yConstraint.ConstraintChanged+= () => {
				YConstraintModified();
			};

			//Set default appearance
			this._appearance = new ACAlertAppearance ();
			this._appearance.AppearanceModified += () => {
				AdjustAlertPosition ();
				RepositionButtons ();
			};

			//Create default button appearances
			this._buttonAppearance = new ACAlertButtonAppearance ();
			this._buttonAppearanceDisabled = new ACAlertButtonAppearance ();
			this._buttonAppearanceTouched = new ACAlertButtonAppearance (Color.Rgb(240,240,240), this._appearance.border, Color.Black);
			this._buttonAppearanceHighlighted = new ACAlertButtonAppearance (Color.Rgb (69,69,69), this._appearance.border);

			//Create any suplimental controls based on the alert's type
			switch(type){
			case ACAlertType.ActivityAlertMedium:
			case ACAlertType.ActivityAlert:
				//Create layout for activity indicator
				var activityLayout = new RelativeLayout.LayoutParams (37, 37);
				activityLayout.LeftMargin = 23;
				activityLayout.TopMargin = 19;

				//Create a new activity indicator and insert it into the control
				_activityIndicator = new ProgressBar (this.Context, null, Android.Resource.Attribute.ProgressBarStyleLarge);
				_activityIndicator.LayoutParameters = activityLayout;
				_activityIndicator.Indeterminate = true;
				this.AddView (_activityIndicator);
				break;
			case ACAlertType.ProgressAlert:
				//Create layout for activity indicator
				var progressLayout = new RelativeLayout.LayoutParams (100, 9);
				progressLayout.LeftMargin = 20;
				progressLayout.TopMargin = 20;

				//Create new progress indicator and insert it into the control
				_progressView = new ProgressBar (this.Context, null, Android.Resource.Attribute.ProgressBarStyleHorizontal);
				_progressView.LayoutParameters = progressLayout;
				_progressView.Max = 100;
				this.AddView (_progressView);
				break;
			}

			//Adjust the alert location
			AdjustAlertPosition ();

			#if TRIAL
			Android.Widget.Toast.MakeText(this.Context, "ActionAlert by Appracatappra", Android.Widget.ToastLength.Short).Show();
			#endif
		}
		#endregion 

		#region Private Methods
		/// <summary>
		/// Cascades changes to the <see cref="ActionComponents.ACAlertButtonAppearance"/> to every
		/// <see cref="ActionComponents.ACAlertButton"/> in the alerts collection 
		/// </summary>
		private void CascadeButtonAppearances(ACAlertButtonApperanceType appearanceType){

			//Process all buttons
			foreach (ACAlertButton button in _buttons) {
				switch (appearanceType) {
				case ACAlertButtonApperanceType.Normal:
					button.appearance = _buttonAppearance;
					break;
				case ACAlertButtonApperanceType.Highlighted:
					button.appearanceHighlighted = _buttonAppearanceHighlighted;
					break;
				case ACAlertButtonApperanceType.Touched:
					button.appearanceTouched = _buttonAppearanceTouched;
					break;
				case ACAlertButtonApperanceType.Disabled:
					button.appearanceDisabled = _buttonAppearanceDisabled;
					break;
				}
			}

		}

		/// <summary>
		/// Repositions all of the <see cref="ActionComponents.ACAlertButton"/>s being controlled by this
		/// <see cref="ActionComponents.ACAlert"/>  
		/// </summary>
		/// <remarks>Called when a new button is added or an old one removed</remarks>
		private void RepositionButtons(){

			//KKM: For some odd reason I'm having to add an extra 5 pixels to the calculation for Android here
			var buttonWidth = (this.LayoutWidth - 15) / _buttons.Count;
			var x = 5;
			var y = this.LayoutHeight - 55;
			var i = 0;

			//Process all buttons
			foreach (ACAlertButton button in _buttons) {
				//Reset the buttons size and location based on position
				if (i == 0) {
					button.SetButtonPosition (x, y, buttonWidth, 47, appearance.roundBottomLeftCorner, (_buttons.Count == 1) ? appearance.roundBottomRightCorner : false);
				} else if (i == (_buttons.Count - 1)) {
					button.SetButtonPosition (x, y, buttonWidth, 47, false, appearance.roundBottomRightCorner);
				} else {
					button.SetButtonPosition (x, y, buttonWidth, 47, false, false);
				}

				//Increment
				x += buttonWidth;
				++i;
			}

		}

		/// <summary>
		/// Calculates the height of the alert base on its type and the elements inside it
		/// </summary>
		/// <returns>The alert height.</returns>
		private int CalculateAlertHeight(){
			int height = 0;
			int descSize = 0;
			RelativeLayout.LayoutParams activityLayout;

			//Does the alert have a title?
			if (title != "") {
				height = 32;
			}

			//Create a paint object to calculate the text size
			Paint textPaint = new Paint ();
			textPaint.AntiAlias = true;
			textPaint.TextSize = appearance.descriptionSize;

			//Take action based on the alert's type
			switch (type) {
			case ACAlertType.ActivityAlert:
				if (description !="") {
					//Calculate the height of the description area
					descSize = ActionCanvasExtensions.MeasureTextBlock (description, 215, 1000, textPaint);
					height += 15 + ((descSize < 22) ? 22 : descSize);
				}
				break;
			case ACAlertType.Subview:
			case ACAlertType.ProgressAlert:
			case ACAlertType.Default:
				if (description != "") {
					//Calculate the height of the description area
					descSize = ActionCanvasExtensions.MeasureTextBlock (description, (_icon != -1) ? 215 : 277, 1000, textPaint);
					height += 15 + ((_icon != -1 && descSize < 22) ? 22 : descSize);

					//Is there a title?
					if (title == "") {
						//No, add an adjust in
						height += (_icon != -1) ? 32 : 8;
					}
				} else if (_icon != -1) {
					//Add the rest of the icon to the offset
					height += 32;
				}

				//Is there a subview?
				if (subview != null) {
					//Nab the layout parameters and grab the 
					RelativeLayout.LayoutParams subLP = (RelativeLayout.LayoutParams)subview.LayoutParameters;
					height += subLP.Height;
				}
				break;
			case ACAlertType.ActivityAlertMedium:
				//Add size of indicator
				height += 60;
				break;
			}

			//Does the alert have buttons?
			if (_buttons.Count > 0) {
				height += 60;
			} else {
				height += 13;
			}

			//Add room for shadow
			height += 10;

			//Make any adjustments based on type
			switch (type) {
			case ACAlertType.ActivityAlert:
				if (description=="") {
					//Create new layout for alert
					activityLayout = new RelativeLayout.LayoutParams (20, 20);
					activityLayout.LeftMargin = 20;
					activityLayout.TopMargin = 15;

					//Adjust activity indication to fit
					_activityIndicator.LayoutParameters = activityLayout;
				}
				break;
			case ACAlertType.ActivityAlertMedium:
				//Create new layout for alert
				activityLayout = new RelativeLayout.LayoutParams (37,37);
				activityLayout.LeftMargin = 43;
				activityLayout.TopMargin = 44;

				//Adjust activity indication to fit
				_activityIndicator.LayoutParameters = activityLayout;
				break;
			case ACAlertType.ProgressAlert:
				height += (description == "") ? 28 : 18;
				break;
			}

			//Return new height
			return height;
		}

		/// <summary>
		/// Calculates the width of the alert based on its type and elements inside it
		/// </summary>
		/// <returns>The alert width.</returns>
		private int CalculateAlertWidth(){
			int width = 0;

			//Take action based on the alert's type
			switch (type) {
			case ACAlertType.Subview:
			case ACAlertType.ActivityAlert:
			case ACAlertType.ProgressAlert:
			case ACAlertType.Default:
				width = 312;
				break;
			case ACAlertType.ActivityAlertMedium:
				width = 123;
				break;
			}

			//Return new width
			return width;
		}

		/// <summary>
		/// Adjusts the alert position based on its content, style, and location
		/// </summary>
		private void AdjustAlertPosition(){

			//Adjust height and width
			this.LayoutWidth = CalculateAlertWidth ();
			this.LayoutHeight = CalculateAlertHeight ();

			//Force alert to redraw itself
			Redraw ();
		}

		/// <summary>
		/// Adjust this view if the <c>xConstraint</c> has been modified
		/// </summary>
		private void XConstraintModified(){

			//Take action based on the constraint type
			switch (_xConstraint.constraintType) {
				case ACAlertDragConstraintType.Constrained:
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
				case ACAlertDragConstraintType.Constrained:
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
		/// Show this <see cref="ActionComponents.ACAlert"/> 
		/// </summary>
		public void Show(){
			Activity parentActivity = (Activity)_context;

			//Already shown?
			if (!hidden) return;

			//Create an overlay to house this alert and display it in the parent view
			_overlay = new ACAlertOverlay (_context, this);
			parentActivity.AddContentView(_overlay, new ViewGroup.LayoutParams(ViewGroup.LayoutParams.FillParent,ViewGroup.LayoutParams.FillParent));

			//Wire-up event
			_overlay.Touched += () => {
				RaiseOverlayTouched();
			};

			//Insert alert into overlay frame
			_overlay.AddView (this);

			//Save state
			_hidden = false;
		}

		/// <summary>
		/// Removes this <see cref="ActionComponents.ACAlert"/> from the screen
		/// </summary>
		public void Hide() {

			//Already hidden?
			if (hidden)
				return;

			//Remove from parent
			this.RemoveFromSuperview ();

			//Remove the overlay from the parent and dispose of it
			_overlay.RemoveFromSuperview ();
			_overlay = null;

			//Save state
			_hidden = true;

		}

		/// <summary>
		/// Flattens the <see cref="ActionComponents.ACAlert"/> and its <see cref="ActionComponents.ACAlertButton"/>s
		/// to match the new iOS 7 design language
		/// </summary>
		public void Flatten(){

			//Adjust all appearances for iOS 7
			appearance.Flatten ();
			buttonAppearance.Flatten ();
			buttonAppearanceHighlighted.Flatten ();
			buttonAppearanceTouched.Flatten (Color.Rgb (0,122,255), Color.Rgb (234, 233, 232));
		}

		/// <summary>
		/// Flattens the <see cref="ActionComponents.ACAlert"/> and its <see cref="ActionComponents.ACAlertButton"/>s
		/// to match the new iOS 7 design language
		/// </summary>
		/// <param name="backgroundColor">Background color.</param>
		public void Flatten(Color backgroundColor){

			//Adjust all appearances for iOS 7
			appearance.Flatten (backgroundColor);
			buttonAppearance.Flatten (backgroundColor);
			buttonAppearanceHighlighted.Flatten (backgroundColor);
			buttonAppearanceTouched.Flatten (Color.Rgb (0,122,255), backgroundColor);
		}

		/// <summary>
		/// Flattens the <see cref="ActionComponents.ACAlert"/> and its <see cref="ActionComponents.ACAlertButton"/>s
		/// to match the new iOS 7 design language
		/// </summary>
		/// <param name="backgroundColor">Background color.</param>
		/// <param name="foregroundColor">Foreground color.</param>
		public void Flatten(Color backgroundColor, Color foregroundColor){

			//Adjust all appearances for iOS 7
			appearance.Flatten (backgroundColor,foregroundColor);
			buttonAppearance.Flatten (backgroundColor);
			buttonAppearanceHighlighted.Flatten (backgroundColor);
			buttonAppearanceTouched.Flatten (Color.Rgb (0,122,255), backgroundColor);
		}

		/// <summary>
		/// Takes a comma seperated string of button titles, creates a new <see cref="ActionComponents.ACAlertButton"/> for
		/// each title and adds them to this <see cref="ActionComponents.ACAlert"/> 
		/// </summary>
		/// <param name="buttonTitles">Button titles.</param>
		/// <remarks>The last button added will automatically be highlighted</remarks>
		public void AddButtons(string buttonTitles) {

			//Parse titles and create buttons
			char[] delimiterChars = {','};
			string[] titles = buttonTitles.Split(delimiterChars);
			AddButtons(titles);
		}

		/// <summary>
		/// Takes an array of titles and adds a new <see cref="ActionComponents.ACAlertButton"/> to this
		/// <see cref="ActionComponents.ACAlert"/> for each title in the array 
		/// </summary>
		/// <param name="titles">Titles.</param>
		/// <remarks>The last button added will automatically be highlighted</remarks>
		public void AddButtons(string[] titles){
			int i = 0;
			ACAlertButton button;

			//Process all passed buttons
			foreach(string title in titles){
				button= new ACAlertButton (_context, this, buttonAppearance, buttonAppearanceDisabled, buttonAppearanceTouched, buttonAppearanceHighlighted, title, (i==(titles.Length-1)));

				//Add button to collection
				_buttons.Add (button);

				//Add button to view
				AddView (button);

				//Wire-up events
				button.Touched += (b) => {
					RaiseButtonTouched(b);
				};

				button.Released += (b) => {
					RaiseButtonReleased(b);
				};

				//Increment
				++i;
			}

			//Adjust alert height
			AdjustAlertPosition ();

			//Adjust button positions
			RepositionButtons ();

		}

		/// <summary>
		/// Adds a new <see cref="ActionComponents.ACAlertButton"/> to this <see cref="ActionComponents.ACAlert"/> 
		/// </summary>
		/// <returns>The new <see cref="ActionComponents.ACAlertButton"/> </returns>
		/// <param name="title">Title.</param>
		/// <param name="highlighted">If set to <c>true</c> highlighted.</param>
		public ACAlertButton AddButton(string title, bool highlighted) {

			//Return new button
			return AddButton (buttonAppearance, buttonAppearanceDisabled, buttonAppearanceTouched, buttonAppearanceHighlighted, title, highlighted);
		}

		/// <summary>
		/// Adds a new <see cref="ActionComponents.ACAlertButton"/> to this <see cref="ActionComponents.ACAlert"/>  
		/// </summary>
		/// <returns>The new <see cref="ActionComponents.ACAlert"/> </returns>
		/// <param name="appearance">Appearance.</param>
		/// <param name="appearanceTouched">Appearance touched.</param>
		/// <param name="appearanceHighlighted">Appearance highlighted.</param>
		/// <param name="title">Title.</param>
		/// <param name="highlighted">If set to <c>true</c> highlighted.</param>
		public ACAlertButton AddButton(ACAlertButtonAppearance appearance, ACAlertButtonAppearance appearanceDisabled, ACAlertButtonAppearance appearanceTouched, ACAlertButtonAppearance appearanceHighlighted, string title, bool highlighted) {

			//Create new button
			ACAlertButton button= new ACAlertButton (_context, this, appearance, appearanceDisabled, appearanceTouched, appearanceHighlighted, title, highlighted);

			//Add button to collection
			_buttons.Add (button);

			//Adjust alert height
			AdjustAlertPosition ();

			//Adjust button positions
			RepositionButtons ();

			//Add button to view
			AddView (button);

			//Wire-up events
			button.Touched += (b) => {
				RaiseButtonTouched(b);
			};

			button.Released += (b) => {
				RaiseButtonReleased(b);
			};

			//Return new button
			return button;
		}

		/// <summary>
		/// Removes the <see cref="ActionComponents.ACAlertButton"/> at the given index from this <see cref="ActionComponents.ACAlert"/> 
		/// </summary>
		/// <param name="index">Index.</param>
		public void RemoveButtonAt(int index){

			//Remove button from view
			_buttons [index].RemoveFromSuperview ();

			//Remove the given button
			_buttons.RemoveAt (index);

			//Any buttons left?
			if (_buttons.Count==0) {
				//No adjust alert size
				AdjustAlertPosition ();
			} else {
				//Adjust button positions
				RepositionButtons ();
			}
		}

		/// <summary>
		/// Removes all <see cref="ActionComponents.ACAlertButton"/>s from this <see cref="ActionComponents.ACAlert"/>  
		/// </summary>
		public void Clear(){

			//Remove each button from the view
			foreach(ACAlertButton button in _buttons){
				button.RemoveFromSuperview ();
			}

			//Remove from collection
			_buttons.Clear ();

			//Resize alert
			AdjustAlertPosition ();
		}

		/// <summary>
		/// Removes this view from its parent view
		/// </summary>
		public void RemoveFromSuperview(){
			//Nab the parent, cast it into a ViewGroup and remove self
			((ViewGroup)this.Parent).RemoveView(this);
		}

		/// <summary>
		/// Moves this <see cref="ActionComponents.ACAlert"/> to the given point and honors any
		/// <see cref="ActionComponents.ACAlertDragConstraint"/>s applied to the <c>X</c> or <c>Y</c> axis 
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
		/// Moves this <see cref="ActionComponents.ACAlert"/> to the given point and honors any
		/// <see cref="ActionComponents.ACAlertDragConstraint"/>s applied to the <c>X</c> or <c>Y</c> axis 
		/// </summary>
		/// <param name="pt">Point.</param>
		public void MoveToPoint(Point point){

			//Dragging?
			if (dragging) {

				//Process x coord constraint
				switch(xConstraint.constraintType) {
					case ACAlertDragConstraintType.None:
					//Adjust frame location
					LeftMargin += point.X - _startLocation.X;
					break;
					case ACAlertDragConstraintType.Locked:
					//Don't move x coord
					break;
					case ACAlertDragConstraintType.Constrained:
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
					case ACAlertDragConstraintType.None:
					//Adjust frame location
					TopMargin += point.Y - _startLocation.Y;
					break;
					case ACAlertDragConstraintType.Locked:
					//Don't move x coord
					break;
					case ACAlertDragConstraintType.Constrained:
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
		/// Resize this <see cref="ActionComponents.ACAlert"/> to the specified width and height.
		/// </summary>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		public void Resize(int width, int height){
			//Resize this view
			LayoutWidth = width;
			LayoutHeight = height;
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
		/// Draws the default alert.
		/// </summary>
		/// <param name="canvas">Canvas.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		private void DrawDefaultAlert(Canvas canvas, int x, int y, int width, int height) {
			int textLeft, textWidth, descSize=0;
			Bitmap bitmap;
			RelativeLayout.LayoutParams progressLayout;

			//Create title paint
			Paint titlePaint = new Paint ();
			titlePaint.Color=appearance.titleColor;
			titlePaint.SetStyle (Paint.Style.Fill);
			titlePaint.AntiAlias = true;
			titlePaint.TextSize = appearance.titleSize;
			titlePaint.FakeBoldText = true;

			//Create description paint
			Paint descPaint = new Paint ();
			descPaint.Color=appearance.descriptionColor;
			descPaint.SetStyle (Paint.Style.Fill);
			descPaint.AntiAlias = true;
			descPaint.TextSize = appearance.descriptionSize;

			//Calculate description size
			if (description != "") {
				descSize = ActionCanvasExtensions.MeasureTextBlock (description, (_icon !=-1) ? 215 : 277, 1000, descPaint);
			}

			//Does this alert have an icon?
			if (icon == -1) {
				//No
				textLeft = 13;
				textWidth = 278;

				//Build Progress layout
				progressLayout = new RelativeLayout.LayoutParams (278, 9);
				progressLayout.LeftMargin = 13;
				progressLayout.TopMargin = ((title=="") ? 15 : 41) + descSize + 10;
			} else {
				//Yes, slide text over and decrease wrap length
				textLeft = 76;
				textWidth = 215;

				//Define icon paint
				Paint iPaint=new Paint();

				//Load bitmap
				bitmap=BitmapFactory.DecodeResource(Resources,icon);

				//Draw image
				//canvas.DrawBitmap (bitmap,x+15,y+(17-(bitmap.Width/2)),iPaint);
				canvas.DrawBitmap (bitmap, null, new Rect (13, 15, 67, 69), iPaint);

				//Build Progress layout
				progressLayout = new RelativeLayout.LayoutParams (215, 9);
				progressLayout.LeftMargin = 76;
				progressLayout.TopMargin = ((title=="") ? 15 : 41) + descSize + 10;
			}

			//Does this alert have a title?
			if (title !="") {
				//Draw out title
				ActionCanvasExtensions.DrawTextBlockInCanvas(canvas,title,textLeft,15,textWidth,1,titlePaint);
			}

			//Does this alert have a description?
			if (description != "") {
				//Draw out description
				ActionCanvasExtensions.DrawTextBlockInCanvas(canvas,description,textLeft,(title=="") ? 15 : 41, textWidth, 1000, descPaint);
			}

			//Does this alert have a subview?
			if (subview != null) {
				// Grab current parameters
				RelativeLayout.LayoutParams subLP = (RelativeLayout.LayoutParams)subview.LayoutParameters;

				//Adjust parameters
				subLP.LeftMargin = textLeft;
				subLP.TopMargin = (title == "") ? 15 : 41;
				subLP.Width = textWidth;

				//Apply changes and attach to view
				_subview.LayoutParameters = subLP;
				AddView (subview);
			}

			//Is this a progress style alert?
			if (type == ACAlertType.ProgressAlert) {
				_progressView.LayoutParameters = progressLayout;
			}

		}

		/// <summary>
		/// Draws the activity alert.
		/// </summary>
		/// <param name="canvas">Canvas.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		private void DrawActivityAlert(Canvas canvas, int x, int y, int width, int height) {
			Bitmap bitmap;

			//Create title paint
			Paint titlePaint = new Paint ();
			titlePaint.Color=appearance.titleColor;
			titlePaint.SetStyle (Paint.Style.Fill);
			titlePaint.AntiAlias = true;
			titlePaint.TextSize = appearance.titleSize;
			titlePaint.FakeBoldText = true;

			//Create description paint
			Paint descPaint = new Paint ();
			descPaint.Color=appearance.descriptionColor;
			descPaint.SetStyle (Paint.Style.Fill);
			descPaint.AntiAlias = true;
			descPaint.TextSize = appearance.descriptionSize;

			//Does this alert have a title?
			if (title !="") {
				//Draw out title
				ActionCanvasExtensions.DrawTextBlockInCanvas(canvas,title,(description=="") ? 50 : 76,15,215,1,titlePaint);
			}

			//Does this alert have a description?
			if (description != "") {
				//Draw out description
				ActionCanvasExtensions.DrawTextBlockInCanvas(canvas,description,76,(title=="") ? 15 : 41, 215, 1000, descPaint);
			}
		}

		/// <summary>
		/// Draws the medium activity alert.
		/// </summary>
		/// <param name="canvas">Canvas.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		private void DrawMediumActivityAlert(Canvas canvas, int x, int y, int width, int height){

			//Create title paint
			Paint titlePaint = new Paint ();
			titlePaint.Color=appearance.titleColor;
			titlePaint.SetStyle (Paint.Style.Fill);
			titlePaint.AntiAlias = true;
			titlePaint.TextSize = appearance.titleSize;
			titlePaint.FakeBoldText = true;

			//Does this alert have a title?
			if (title !="") {
				//Draw out title
				ActionCanvasExtensions.DrawTextBlockInCanvas(canvas,title,10,15,103,1,titlePaint,TextBlockAlignment.Center);
			}

		}

		/// <summary>
		/// Populates the image cache.
		/// </summary>
		/// <returns>The image cache.</returns>
		private Bitmap PopulateImageCache(){
			List<float> corners = new List<float> () { 0, 0, 0, 0, 0, 0, 0, 0 };
			int x = 5;
			int y = 5;
			int width = this.Width - 10;
			int height = this.Height - 10;

			//Create a temporary canvas
			var canvas=new Canvas();

			//Create bitmap storage and assign to canvas
			var controlBitmap=Bitmap.CreateBitmap (this.Width,this.Height,Bitmap.Config.Argb8888);
			canvas.SetBitmap (controlBitmap);

			// Take action based on the corners speficied
			if (appearance.roundTopLeftCorner) {
				corners [0] = appearance.borderRadius;
				corners [1] = appearance.borderRadius;
			}
			if (appearance.roundTopRightCorner) {
				corners [2] = appearance.borderRadius;
				corners [3] = appearance.borderRadius;
			}
			if (appearance.roundBottomRightCorner) {
				corners [4] = appearance.borderRadius;
				corners [5] = appearance.borderRadius;
			}
			if (appearance.roundBottomLeftCorner) {
				corners [6] = appearance.borderRadius;
				corners [7] = appearance.borderRadius;
			}

			//Convert list to array
			var cornerArray = corners.ToArray ();

			//Draw shadow
			if (!appearance.flat) {
				ShapeDrawable shadow= new ShapeDrawable(new RoundRectShape(cornerArray,null,null));
				shadow.Paint.Color=appearance.shadow;
				shadow.SetBounds (3,5,this.Width-6,this.Height-5);
				shadow.Draw (canvas);
			}

			//Draw body of button
			ShapeDrawable body= new ShapeDrawable(new RoundRectShape(cornerArray,null,null));
			body.Paint.Color=appearance.background;
			body.SetBounds (x,y,width,height);
			body.Draw (canvas);

			//Draw border of button
			ShapeDrawable bodyBorder= new ShapeDrawable(new RoundRectShape(cornerArray,null,null));
			bodyBorder.Paint.Color=appearance.border;
			bodyBorder.Paint.SetStyle (Paint.Style.Stroke);
			body.Paint.StrokeWidth = appearance.borderWidth;
			bodyBorder.SetBounds (x,y,width,height);
			bodyBorder.Draw (canvas);

			//Take action based on the alert's type
			switch (type) {
			case ACAlertType.Subview:
			case ACAlertType.ProgressAlert:
			case ACAlertType.Default:
				DrawDefaultAlert (canvas, x, y, width, height);
				break;
			case ACAlertType.ActivityAlert:
				DrawActivityAlert (canvas, x, y, width, height);
				break;
			case ACAlertType.ActivityAlertMedium:
				DrawMediumActivityAlert (canvas, x, y, width, height);
				break;
			}

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
		/// Occurs when this <see cref="ActionComponents.ACAlert"/> is touched 
		/// </summary>
		public delegate void ACAlertTouchedDelegate (ACAlert view);
		public event ACAlertTouchedDelegate Touched;

		/// <summary>
		/// Raises the touched event
		/// </summary>
		private void RaiseTouched(){
			if (this.Touched != null)
				this.Touched (this);
		}

		/// <summary>
		/// Occurs when this <see cref="ActionComponents.ACAlert"/> is moved
		/// </summary>
		public delegate void ACAlertMovedDelegate (ACAlert view);
		public event ACAlertMovedDelegate Moved;

		/// <summary>
		/// Raises the moved event
		/// </summary>
		private void RaiseMoved(){
			if (this.Moved != null)
				this.Moved (this);
		}

		/// <summary>
		/// Occurs when this <see cref="ActionComponents.ACAlert"/> is released 
		/// </summary>
		public delegate void ACAlertReleasedDelegate (ACAlert view);
		public event ACAlertReleasedDelegate Released;

		/// <summary>
		/// Raises the released.
		/// </summary>
		private void RaiseReleased(){
			if (this.Released != null)
				this.Released (this);
		}

		public delegate void ACAlertButtonTouched (ACAlertButton button);
		public event ACAlertButtonTouched ButtonTouched;

		/// <summary>
		/// Raises the ButtonTouched event.
		/// </summary>
		private void RaiseButtonTouched (ACAlertButton button)
		{
			if (this.ButtonTouched != null)
				this.ButtonTouched (button);
		}

		public delegate void ACAlertButtonReleased (ACAlertButton button);
		public event ACAlertButtonReleased ButtonReleased;

		/// <summary>
		/// Raises the ButtonTouched event.
		/// </summary>
		private void RaiseButtonReleased (ACAlertButton button)
		{
			if (this.ButtonReleased != null)
				this.ButtonReleased (button);
		}

		public delegate void ACAlertOverlayTouched ();
		public event ACAlertOverlayTouched OverlayTouched;

		/// <summary>
		/// Raises the ButtonTouched event.
		/// </summary>
		private void RaiseOverlayTouched ()
		{
			if (this.OverlayTouched != null)
				this.OverlayTouched ();
		}
		#endregion 
	}
}

