using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.ComponentModel;
using Foundation;
using UIKit;
using CoreGraphics;

namespace ActionComponents 
{
	/// <summary>
	/// The <see cref="ActionComponents.ACTableViewController"/> is a customized <c>UITableViewController</c> that support several new
	/// features, methods and events that make working with <c>UITableViews</c> easier and with far less code. By implementing the <c>RequestData</c> event and passing
	/// a collection of <see cref="ActionComponents.ACTableSection"/>s each containing a collection of
	/// <see cref="ActionComponents.ACTableItem"/>s you'll almost never need to create a custom <c>TableViewDataSource</c> or a 
	/// <c>TableViewDelegate</c>. 
	/// </summary>
	/// <remarks>Available in ActionPack Buesines or Enterprise only.</remarks>
	[Register("ACTableViewController")]
	public class ACTableViewController  : UITableViewController
	{
		#region Private Variables
		private UITableViewCellSelectionStyle _cellSelectionStyle = UITableViewCellSelectionStyle.Blue;
		private float _footerHeight = 44f;
		private float _headerHeight = 44f;
		private float _rowHeight = 44f;
		private bool _allowsHighlight = true;
		#endregion 

		#region Computed Properties
		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <see cref="ActionComponents.ACTableViewController"/> allows highlighting of rows in the contained UITableView
		/// </summary>
		/// <value><c>true</c> if allows highlight; otherwise, <c>false</c>.</value>
		[Export("allowsHighlight"), Browsable(true)]
		public bool allowsHighlight{
			get{ return _allowsHighlight;}
			set{ _allowsHighlight = value;}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <see cref="ActionComponents.ACTableViewController"/> allows selection of rows in the contained UITableView
		/// </summary>
		/// <value><c>true</c> if allows selection; otherwise, <c>false</c>.</value>
		[Export("allowsSelection"), Browsable(true)]
		public bool allowsSelection{
			get{ return TableView.AllowsSelection;}
			set{ TableView.AllowsSelection=value;}
		}

		/// <summary>
		/// Gets or sets the height of the footer in the tableview controlled by this <see cref="ActionComponents.ACTableViewController"/> 
		/// </summary>
		/// <value>The height of the footer.</value>
		[Export("footerHeight"), Browsable(true)]
		public float footerHeight{
			get{ return _footerHeight;}
			set{ _footerHeight = value;}
		}

		/// <summary>
		/// Gets or sets the height of the header in the tableview controlled by this <see cref="ActionComponents.ACTableViewController"/> 
		/// </summary>
		/// <value>The height of the header.</value>
		[Export("headerHeight"), Browsable(true)]
		public float headerHeight{
			get{ return _headerHeight;}
			set{ _headerHeight = value;}
		}

		/// <summary>
		/// Gets or sets the height of the row in the tableview controlled by this <see cref="ActionComponents.ACTableViewController"/> 
		/// </summary>
		/// <value>The height of the row.</value>
		[Export("rowHeight"), Browsable(true)]
		public float rowHeight{
			get{ return _rowHeight;}
			set{ _rowHeight = value;}
		}

		/// <summary>
		/// Gets or sets the cell selection style for items in this <see cref="ActionComponents.ACTableViewController"/>'s TableView
		/// </summary>
		/// <value>The cell selection style.</value>
		[Export("cellSelectionStyle"), Browsable(true)]
		public UITableViewCellSelectionStyle cellSelectionStyle{
			get{ return _cellSelectionStyle;}
			set{ _cellSelectionStyle = value;}
		}

		/// <summary>
		/// Gets or sets the <see cref="ActionComponents.ACTableViewDataSource"/> for this
		/// <see cref="ActionComponents.ACTableViewController"/> 
		/// </summary>
		/// <value>The data source.</value>
		public ACTableViewDataSource dataSource{
			get{ return (ACTableViewDataSource)TableView.DataSource;}
			set { TableView.DataSource = value;}
		}

		/// <summary>
		/// Gets or sets the <see cref="ActionComponents.ACTableViewDelegate"/> for this
		/// <see cref="ActionComponents.ACTableViewController"/> 
		/// </summary>
		/// <value>The table delegate.</value>
		public ACTableViewDelegate tableDelegate{
			get{ return (ACTableViewDelegate)TableView.Delegate;}
			set{ TableView.Delegate = value;}
		}
		#endregion 

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="ActionComponents.ACTableViewController"/> class.
		/// </summary>
		public ACTableViewController () : base()
		{
			//Initialize
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="ActionComponents.ACTableViewController"/> class.
		/// </summary>
		/// <param name="coder">Coder.</param>
		public ACTableViewController(NSCoder coder) : base(coder)
		{
			//Initialize
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="ActionComponents.ACTableViewController"/> class.
		/// </summary>
		/// <param name="t">T.</param>
		public ACTableViewController(NSObjectFlag t) : base(t)
		{
			//Initialize
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="ActionComponents.ACTableViewController"/> class.
		/// </summary>
		/// <param name="handle">Handle.</param>
		public ACTableViewController(IntPtr handle) : base(handle)
		{
			//Initialize
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="ActionComponents.ACTableViewController"/> class.
		/// </summary>
		/// <param name="nibName">Nib name.</param>
		/// <param name="bundle">Bundle.</param>
		public ACTableViewController(string nibName, NSBundle bundle) : base(nibName,bundle)
		{
			//Initialize
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="ActionComponents.ACTableViewController"/> class.
		/// </summary>
		/// <param name="withStyle">With style.</param>
		public ACTableViewController(UITableViewStyle withStyle) : base(withStyle)
		{
			//Initialize
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="ActionComponents.ACTableViewController"/> class.
		/// </summary>
		/// <param name="frame">Frame.</param>
		public ACTableViewController (CGRect frame) : base()
		{
			//Initialize
			TableView.Frame = frame;
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="ActionComponents.ACTableViewController"/> class.
		/// </summary>
		/// <param name="withStyle">With style.</param>
		/// <param name="frame">Frame.</param>
		public ACTableViewController(UITableViewStyle withStyle, CGRect frame) : base(withStyle)
		{
			//Initialize
			TableView.Frame = frame;
			Initialize ();
		}

		/// <summary>
		/// Initialize this instance.
		/// </summary>
		private void Initialize(){

			//Setup default values
			TableView.DataSource = new ACTableViewDataSource(this);
			TableView.Delegate = new ACTableViewDelegate(this);
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
			TableView.ReloadData ();
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
		public new event AccessoryButtonTappedDelegate AccessoryButtonTapped;

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

		/// <summary>
		/// Occurs when editing has ended for the <see cref="ActionComponents.ACTableItem"/> 
		/// </summary>
		public delegate void DidEndEditingDelegate (ACTableItem item);
		public new event DidEndEditingDelegate DidEndEditing;

		/// <summary>
		/// Raises the accessory button tapped event
		/// </summary>
		internal void RaiseDidEndEditing (ACTableItem item)
		{
			//If an event was attached, fire it off now
			if (this.DidEndEditing != null)
				this.DidEndEditing (item);
		}
		#endregion 
	}
}

