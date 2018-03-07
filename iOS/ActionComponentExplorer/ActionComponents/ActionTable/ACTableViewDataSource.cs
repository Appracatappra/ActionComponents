using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

using Foundation;
using UIKit;
using CoreGraphics;

namespace ActionComponents
{
	/// <summary>
	/// The <see cref="ActionComponents.ACTableViewDataSource"/> provides all data to it's parent 
	/// <see cref="ActionComponents.ACTableViewController"/> via the controller's <c>RequestData</c> event. It maintains a collection of
	/// <see cref="ActionComponents.ACTableSection"/>s each containing a collection of <see cref="ActionComponents.ACTableItem"/>.
	/// As a result, you may never need to create a custom version of this class to support your <c>UITableViews</c> maintained by the
	/// <see cref="ActionComponents.ACTableViewController"/>. 
	/// </summary>
	/// <remarks>Available in ActionPack Business or Enterprise only.</remarks>
	[Register("ACTableViewDataSource")]
	public class ACTableViewDataSource : UITableViewDataSource
	{
		#region Private Variables
		private ACTableViewController _controller;
		private List<ACTableSection> _sections=new List<ACTableSection>();
		#endregion 

		#region Computed Properties
		/// <summary>
		/// Gets the <see cref="ActionComponents.ACTableViewController"/> that this <see cref="ActionComponents.ACTableViewDataSource"/>
		/// is providing data for.
		/// </summary>
		/// <value>The controller.</value>
		public ACTableViewController controller{
			get{ return _controller;}
		}

		/// <summary>
		/// Gets the collection of <see cref="ActionComponents.ACTableSection"/> contained in this
		/// <see cref="ActionComponents.ACTableViewDataSource"/> 
		/// </summary>
		/// <value>The sections.</value>
		public List<ACTableSection> sections{
			get{ return _sections;}
		}
		#endregion 

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="ActionComponents.ACTableViewDataSource"/> class.
		/// </summary>
		public ACTableViewDataSource ()
		{
		}

		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="ActionComponents.ACTableViewDataSource"/> class.
		/// </summary>
		/// <param name="controller">Controller.</param>
		public ACTableViewDataSource (ACTableViewController controller)
		{
			//Initialize
			this._controller = controller;
		}
		#endregion 

		#region Public Methods
		/// <summary>
		/// Requests that this <see cref="ActionComponents.ACTableViewDataSource"/> reloads all of the data from it's source
		/// </summary>
		public virtual void LoadData(){

			//Clear any existing data
			_sections.Clear ();

			//Request that caller populates the datasource
			RaiseRequestData();
		}

		/// <summary>
		/// Removes the requested <see cref="ActionComponents.ACTableSection"/> from this <see cref="ActionComponents.ACTableViewDataSource"/>'s
		/// collection
		/// </summary>
		/// <param name="index">Index.</param>
		public void RemoveItemAt(int index){
			_sections.RemoveAt (index);
		}

		/// <summary>
		/// Empties this <see cref="ActionComponents.ACTableViewDataSource"/>'s collection of <see cref="ActionComponents.ACTableSection"/>  
		/// </summary>
		public void Clear(){
			_sections.Clear ();
		}

		/// <summary>
		/// Adds the given <see cref="ActionComponents.ACTableSection"/> to this <see cref="ActionComponents.ACTableViewDataSource"/> 
		/// </summary>
		/// <param name="section">Section.</param>
		public void AddSection(ACTableSection section){
			_sections.Add (section);
		}

		/// <summary>
		/// Creates a new <see cref="ActionComponents.ACTableSection"/> and adds it to this <see cref="ActionComponents.ACTableViewDataSource"/>  
		/// </summary>
		/// <returns>The section.</returns>
		/// <param name="header">Header.</param>
		public ACTableSection AddSection(string header){
			ACTableSection section = new ACTableSection (header);
			AddSection (section);
			return section;
		}

		/// <summary>
		/// Creates a new <see cref="ActionComponents.ACTableSection"/> and adds it to this <see cref="ActionComponents.ACTableViewDataSource"/>  
		/// </summary>
		/// <returns>The section.</returns>
		/// <param name="header">Header.</param>
		/// <param name="footer">Footer.</param>
		public ACTableSection AddSection(string header, string footer){
			ACTableSection section = new ACTableSection (header, footer);
			AddSection (section);
			return section;
		}

		/// <summary>
		/// Creates a new <see cref="ActionComponents.ACTableSection"/> and adds it to this <see cref="ActionComponents.ACTableViewDataSource"/>  
		/// </summary>
		/// <returns>The section.</returns>
		/// <param name="header">Header.</param>
		/// <param name="footer">Footer.</param>
		/// <param name="sectionCellID">Section cell I.</param>
		public ACTableSection AddSection(string header, string footer, string sectionCellID){
			ACTableSection section = new ACTableSection (header, footer, sectionCellID);
			AddSection (section);
			return section;
		}
		#endregion 

		#region Override Methods
		/// <Docs>Table view containing the section.</Docs>
		/// <summary>
		/// Called to populate the header for the specified section.
		/// </summary>
		/// <see langword="null"></see>
		/// <returns>The for header.</returns>
		/// <param name="tableView">Table view.</param>
		/// <param name="section">Section.</param>
		public override string TitleForHeader (UITableView tableView, nint section)
		{
			//Return the name of the current section
			return _sections[(int)section].header;
		}

		/// <Docs>Table view containing the section.</Docs>
		/// <summary>
		/// Called to populate the footer for the specified section.
		/// </summary>
		/// <see langword="null"></see>
		/// <returns>The for footer.</returns>
		/// <param name="tableView">Table view.</param>
		/// <param name="section">Section.</param>
		public override string TitleForFooter (UITableView tableView, nint section)
		{
			//Return the name for the footer
			if (_sections[(int)section].footer=="") {
				return null;
			} else {
				return _sections[(int)section].footer;
			}
		}

		/// <Docs>To be added.</Docs>
		/// <summary>
		/// Rowses the in section.
		/// </summary>
		/// <returns>The in section.</returns>
		/// <param name="tableview">Tableview.</param>
		/// <param name="section">Section.</param>
		public override System.nint RowsInSection (UITableView tableView, nint section)
		{
			//Return the number of items defined for the current section
			return _sections[(int)section].items.Count;
		}

		/// <Docs>Table view displaying the sections.</Docs>
		/// <returns>Number of sections required to display the data. The default is 1 (a table must have at least one section).</returns>
		/// <summary>
		/// Numbers the of sections.
		/// </summary>
		/// <param name="tableView">Table view.</param>
		public override nint NumberOfSections (UITableView tableView)
		{
			//Return the number of sections
			return _sections.Count;
		}

		/// <Docs>Table view requesting the cell.</Docs>
		/// <summary>
		/// Gets the cell.
		/// </summary>
		/// <returns>The cell.</returns>
		/// <param name="tableView">Table view.</param>
		/// <param name="indexPath">Index path.</param>
		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			//Trap all errors
			try {
				//Grab item used to define cell information and create a unique cell ID
				ACTableItem item = _sections[indexPath.Section].items[indexPath.Row];
				string cellId = String.Format("{0}{1}",_sections[indexPath.Section].sectionCellID,item.style);

				//Decant reusable cell
				item.cell = tableView.DequeueReusableCell(cellId); 
				if (item.cell == null )
				{
					//Not found, assemble new cell
					item.cell = new UITableViewCell(item.style,cellId);
				}

				//Populate cell with the style information
				if (item.accessoryView is UIStepper || item.accessoryView is UISlider) {
					//Display the value for the stepper
					item.cell.TextLabel.Text = String.Format(item.text,item.accessoryValue);
				} else {
					item.cell.TextLabel.Text = item.text;
				}
				item.cell.Accessory = item.accessory;
				item.cell.SelectionStyle=controller.cellSelectionStyle;

				if (!string.IsNullOrEmpty(item.details)) {
					if (item.cell.DetailTextLabel!=null)
						item.cell.DetailTextLabel.Text = item.details;
				}

				var image = item.image;
				if (image != null)
				{
					item.cell.ImageView.HighlightedImage = image;
					item.cell.ImageView.Image = image;
				}

				if (item.contentView != null)
				{
					item.cell.ContentView.Add(item.contentView);
				}

				// Alternative method is to use AccessoryForRow in the UITableViewDelegate
				item.cell.AccessoryView = item.accessoryView;
				return item.cell;
			}
			catch (Exception e)
			{
				return new UITableViewCell(UITableViewCellStyle.Default,e.Message);
			}


		}
		#endregion 

		#region Events
		/// <summary>
		/// Occurres when this <see cref="ActionComponents.ACTableViewDataSource"/> needs to be populated with data
		/// </summary>
		public delegate void DataSourceRequestDataDelegate(ACTableViewDataSource dataSource);
		public event DataSourceRequestDataDelegate RequestData;

		/// <summary>
		/// Raises the request data event
		/// </summary>
		internal void RaiseRequestData()
		{
			if (this.RequestData != null)
				this.RequestData(this);

#if TRIAL
			ACToast.MakeText("ACTable by Appracatappra, LLC", ACToastLength.Long).Show();
#else
			AppracatappraLicenseManager.ValidateLicense();
#endif

		}
		#endregion 
	}
}

