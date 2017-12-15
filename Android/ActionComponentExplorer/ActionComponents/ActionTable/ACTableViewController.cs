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
	/// The <see cref="ActionComponents.ACTableViewController"/> is a type of custom <c>ListView</c> that has several helper routines to make working
	/// with simple lists easier and requiring fewer lines of code. The <c>ActionTable</c> has been designed to be highly code compatible with the iOS version so that a maximum of code
	/// can be shared between the two platforms. The <see cref="ActionComponents.ACTableViewController"/> contains a
	/// <see cref="ActionComponents.ACTableViewDataSource"/> with a collection of <see cref="ActionComponents.ACTableSection"/>s
	/// and <see cref="ActionComponents.ACTableItem"/>s that provide the data for the list. 
	/// </summary>
	/// <remarks>Available in ActionPack Business or Enterprise only.</remarks>
	public class ACTableViewController : ScrollView 
	{
		#region Private Variables
		private bool _allowsTouchHeader = false;
		private bool _allowsTouchFooter = false;
		private bool _allowsHighlight = true;
		private bool _allowsSelection = false;
		private ACTableViewDataSource _dataSource;
		private ACTableViewAppearance _appearance;
		private LinearLayout _linearLayout;
		private Random _rnd = new Random();
		#endregion 

		#region Internal Variables
		internal ACTableItem _selectedItem;
		internal int tableID;
		#endregion 

		#region Computed Properties
		/// <summary>
		/// Gets or sets the [OPTIONAL] tag that can be assosciated with this <see cref="ActionComponents.ACTableViewController"/> 
		/// </summary>
		/// <value>The tag.</value>
		public Object tag { get; set; }

		/// <summary>
		/// Gets the selected <see cref="ActionComponents.ACTableItem"/> or returns <c>null</c> if nothing is selected 
		/// </summary>
		/// <value>The selected item.</value>
		public ACTableItem selectedItem{
			get{ return _selectedItem;}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <see cref="ActionComponents.ACTableViewController"/> allows touching a row header.
		/// </summary>
		/// <value><c>true</c> if allows touch header; otherwise, <c>false</c>.</value>
		public bool allowsTouchHeader{
			get { return _allowsTouchHeader;}
			set{ _allowsTouchHeader = value;}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <see cref="ActionComponents.ACTableViewController"/> allows touching a row footer.
		/// </summary>
		/// <value><c>true</c> if allows touch footer; otherwise, <c>false</c>.</value>
		public bool allowsTouchFooter{
			get{ return _allowsTouchFooter;}
			set{ _allowsTouchFooter = value;}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <see cref="ActionComponents.ACTableViewController"/> allows highlighting of rows in the contained UITableView
		/// </summary>
		/// <value><c>true</c> if allows highlight; otherwise, <c>false</c>.</value>
		public bool allowsHighlight{
			get{ return _allowsHighlight;}
			set{ _allowsHighlight = value;}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <see cref="ActionComponents.ACTableViewController"/> allows selection of rows in the contained UITableView
		/// </summary>
		/// <value><c>true</c> if allows selection; otherwise, <c>false</c>.</value>
		public bool allowsSelection{
			get{ return _allowsSelection;}
			set{ _allowsSelection=value;}
		}

		/// <summary>
		/// Gets or sets the <see cref="ActionComponents.ACTableViewDataSource"/> for this
		/// <see cref="ActionComponents.ACTableViewController"/> 
		/// </summary>
		/// <value>The data source.</value>
		public ACTableViewDataSource dataSource{
			get{ return _dataSource;}
			set { _dataSource = value;}
		}

		/// <summary>
		/// Gets or sets the activity.
		/// </summary>
		/// <value>The activity.</value>
		public Activity activity{
			get{ return dataSource.activity;}
			set{ dataSource.activity = value;}
		}

		/// <summary>
		/// Gets or sets the <see cref="ActionComponents.ACTableViewAppearance"/> for the items in the <c>ListView</c>
		///  controlled by this <see cref="ActionComponents.ACTableViewController"/> 
		/// </summary>
		/// <value>The appearance.</value>
		public ACTableViewAppearance appearance{
			get { return _appearance;}
			set{ _appearance = value;}
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
		/// Initializes a new instance of the <see cref="Appracatappra.ActionComponents.ActionView.ACView"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		public ACTableViewController (Context context) : base(context)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Appracatappra.ActionComponents.ActionView.ACView"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attr">Attr.</param>
		public ACTableViewController (Context context, IAttributeSet attr) : base(context,attr)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Appracatappra.ActionComponents.ActionView.ACView"/> class.
		/// </summary>
		/// <param name="javaReference">Java reference.</param>
		/// <param name="transfer">Transfer.</param>
		public ACTableViewController (IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference,transfer)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Appracatappra.ActionComponents.ActionView.ACView"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attr">Attr.</param>
		/// <param name="defStyle">Def style.</param>
		public ACTableViewController (Context context, IAttributeSet attr, int defStyle) : base(context,attr,defStyle)
		{
			Initialize ();
		}

		/// <summary>
		/// Initialize this instance.
		/// </summary>
		private void Initialize(){

			//Setup default values
			dataSource = new ACTableViewDataSource(this);
			appearance = new ACTableViewAppearance ();
			tableID = _rnd.Next (100, 64000);

			//Wire-up appearance handler
			appearance.AppearanceModified += () => {
				//Request the table to be repainted
				dataSource.UpdateData();
			};

		}
		#endregion 

		#region Public Methods
		/// <summary>
		/// Causes this <see cref="ActionComponents.ACTableViewController"/> to populate it's TableView with
		/// data from the attached <see cref="ActionComponents.ACTableViewDataSource"/> 
		/// </summary>
		public void LoadData(){
			//Populate the datasource and reload the tableview
			dataSource.LoadData ();
		}

		/// <summary>
		/// Causes this <see cref="ActionComponents.ACTableViewController"/> to redisplay all of the <c>rows</c>
		/// it controls
		/// </summary>
		public void RefreshView(){

			//Has this table already been populated?
			if (_linearLayout == null) {
				// Create a vertical linear layout to add all the elements to.
				_linearLayout = new LinearLayout (activity);
				_linearLayout.Id = tableID + 10;
				_linearLayout.LayoutParameters = new ViewGroup.LayoutParams (ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
				_linearLayout.Orientation = Orientation.Vertical;
				AddView (_linearLayout);
			} else {
				//Empty any existing views
				_linearLayout.RemoveAllViews ();
			}

			//Prepare to populate
			int viewCount = dataSource.Count;
			View cell, divider;

			// Loop through the dialog adapter calling GetView on each item.
			// If a view is returned add it to the linear layout.
			for (int i = 0; i < viewCount; i++)
			{
				//Request a cell for the given position
				cell = dataSource.GetView(i, null, _linearLayout);

				//Was a cell returned?
				if (cell != null) {
					//Yes, insert it into the layout
					_linearLayout.AddView(cell);

					//Create a new divider and insert it into the list
					divider = new View (activity);
					divider.LayoutParameters = new LinearLayout.LayoutParams (LinearLayout.LayoutParams.FillParent, appearance.dividerHeight);
					divider.SetBackgroundColor (appearance.divider);
					_linearLayout.AddView (divider);
				}
			}
		}

		/// <summary>
		/// Clears the selected <see cref="ActionComponents.ACTableItem"/> from this 
		/// <see cref="ActionComponents.ACTableViewController"/> 
		/// </summary>
		public void ClearSelection(){
			//Release any previously selected cell
			if (selectedItem != null) {
				selectedItem.cell.SetBackgroundColor (appearance.cellBackground);
			}

			//Erase selection
			_selectedItem = null;
		}

		/// <summary>
		/// Selects the given <see cref="ActionComponents.ACTableItem"/> from the given
		/// <see cref="ActionComponents.ACTableSection"/> 
		/// </summary>
		/// <param name="sectionPosition">Section position.</param>
		/// <param name="itemPosition">Item position.</param>
		public void SelectItem(int sectionPosition, int itemPosition){

			//Can we make selections?
			if (!allowsSelection)
				return;

			//Release any previously selected cell
			if (selectedItem != null) {
				selectedItem.cell.SetBackgroundColor (appearance.cellBackground);
			}

			//Trap all errors
			try{
				var item = dataSource.sections[sectionPosition].items[itemPosition];

				//Set the selection to this item
				_selectedItem = item;
				item.cell.SetBackgroundColor(appearance.cellSelected);
			}
			catch{
				//Just ignore for the time being
			}
		}

		/// <summary>
		/// Selects the <see cref="ActionComponents.ACTableItem"/> with a <c>text</c> property that matches
		/// the given value
		/// </summary>
		/// <param name="text">Text.</param>
		/// <remarks>Nothing will be selected in the text cannot be found</remarks>
		public void SelectItem(string text){

			//Can we make selections?
			if (!allowsSelection)
				return;

			//Release any previously selected cell
			if (selectedItem != null) {
				selectedItem.cell.SetBackgroundColor (appearance.cellBackground);
			}

			//Scan dataset for a matching item
			foreach (ACTableSection section in dataSource.sections) {
				foreach(ACTableItem item in section.items){
					//Matching?
					if (item.text == text) {
						//Set the selection to this item
						_selectedItem = item;
						item.cell.SetBackgroundColor(appearance.cellSelected);

						//Stop searching
						return;
					}
				}
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
//			int x=(int)e.GetX ();
//			int y=(int)e.GetY ();

			//Take action based on the event type
			switch(e.Action){
			case MotionEventActions.Down:
				break;
			case MotionEventActions.Move:
				break;
			case MotionEventActions.Up:
				break;
			}

			// Pass event along to the parent object
			return base.OnTouchEvent(e);
		}
		#endregion 

		#region Events
		/// <summary>
		/// Occurs when this <see cref="ActionComponents.ACTableItem"/> has been selected by the user
		/// </summary>
		public delegate void ACTableItemSelectedDelegate (ACTableItem item);
		public event ACTableItemSelectedDelegate ItemsSelected;

		/// <summary>
		/// Raises the item selected event
		/// </summary>
		internal void RaiseItemSelected (ACTableItem item)
		{
			//If an event was attached, fire it off now
			if (item.canSelect && this.ItemsSelected != null)
				this.ItemsSelected (item);
		}

		/// <summary>
		/// Occurs when a <see cref="ActionComponents.ACTableItem"/>'s accessory button is tapped
		/// </summary>
		public delegate void AccessoryButtonTappedDelegate (ACTableItem item);
		public event AccessoryButtonTappedDelegate AccessoryButtonTapped;

		/// <summary>
		/// Raises the accessory button tapped event
		/// </summary>
		/// <param name="item">Item.</param>
		internal void RaiseAccessoryButtonTapped (ACTableItem item)
		{
			//If an event was attached, fire it off now
			if (this.AccessoryButtonTapped != null)
				this.AccessoryButtonTapped (item);
		}

		#endregion 
	}
}

