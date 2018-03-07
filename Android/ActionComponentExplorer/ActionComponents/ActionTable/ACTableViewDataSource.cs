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
using Android.Animation;

namespace ActionComponents
{
	/// <summary>
	/// The <see cref="ActionComponents.ACTableViewDataSource"/> provides all data to it's parent 
	/// <see cref="ActionComponents.ACTableViewController"/> via the controller's <c>RequestData</c> event. It maintains a collection of
	/// <see cref="ActionComponents.ACTableSection"/>s each containing a collection of <see cref="ActionComponents.ACTableItems"/>.
	/// As a result, you may never need to create a custom version of this class to support your <c>ListView</c> maintained by the
	/// <see cref="ActionComponents.ACTableViewController"/>. 
	/// </summary>
	/// <remarks>Available in ActionPack Business or Enterprise only.</remarks>
	public class ACTableViewDataSource : BaseAdapter<string>
	{
		#region Private Variables
		private Activity _activity;
		private ACTableViewController _controller;
		private List<ACTableSection> _sections=new List<ACTableSection>();
		private List<ACTableIndex> _index = new List<ACTableIndex> ();
		#endregion 

		#region Computed Properties
		/// <summary>
		/// Gets or sets the [OPTIONAL] object tag that can be attached to this <see cref="ActionComponents.ACTableViewDataSource"/> 
		/// </summary>
		/// <value>The tag.</value>
		public object tag {get; set;}

		/// <summary>
		/// Gets or sets the activity that this <see cref="ActionComponents.ACTableViewDataSource"/> is attached to
		/// </summary>
		/// <value>The activity.</value>
		public Activity activity{
			get{ return _activity;}
			set{ 
				_activity = value;
			
				//Propogate to all sections
				foreach (ACTableSection section in sections) {
					section.activity = _activity;
				}
			}
		}

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

		/// <summary>
		/// Gets the index used to translate a <c>ListView</c> <c>position</c> into an <see cref="ActionComponents.ACTableViewController"/> 
		/// <see cref="ActionComponents.ACTableSection"/> and <see cref="ActionComponents.ACTableItem"/> index. 
		/// </summary>
		/// <value>The index.</value>
		public List<ACTableIndex> index{
			get{ return _index;}
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
		/// <param name="activity">Activity.</param>
		public ACTableViewDataSource (Activity activity)
		{
			//Initialize
			this._activity = activity;
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

		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="ActionComponents.ACTableViewDataSource"/> class.
		/// </summary>
		/// <param name="controller">Controller.</param>
		public ACTableViewDataSource (Activity activity, ACTableViewController controller)
		{
			//Initialize
			this._activity = activity;
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

			//Rebuild index
			RebuildIndex ();

			//Inform ListView that the table has changed
			_controller.RefreshView ();
		}

		/// <summary>
		/// Call this method to update the <c>ListView</c> after making changes to any <see cref="ActionComponents.ACTableSection"/>s or
		/// <see cref="ActionComponents.ACTableItem"/>s being controller by this <see cref="ActionComponents.ACTableViewDataSource"/> 
		/// </summary>
		public virtual void UpdateData(){

			//Rebuild index
			RebuildIndex ();

			//Inform ListView that the table has changed
			_controller.RefreshView ();
		}

		/// <summary>
		/// Returns the <see cref="ActionComponents.ACTableSection"/> being pointed to by the given
		/// <see cref="ActionComponents.ACTableIndex"/> 
		/// </summary>
		/// <returns>The at index.</returns>
		/// <param name="index">Index.</param>
		public ACTableSection SectionAtIndex(ACTableIndex index){

			//Return the section being pointed to by this index
			return _sections[index.sectionPosition];
		}

		/// <summary>
		/// Returns the <see cref="ActionComponents.ACTableSection"/> being pointed to by the given
		/// <c>position</c>
		/// </summary>
		/// <returns>The at index.</returns>
		/// <param name="position">Position.</param>
		public ACTableSection SectionAtIndex(int position){
			return SectionAtIndex (_index[position]);
		}

		/// <summary>
		/// Returns the <see cref="ActionComponents.ACTableItem"/> being pointed to by the given
		///  <see cref="ActionComponents.ACTableIndex"/> 
		/// </summary>
		/// <returns>The at index.</returns>
		/// <param name="index">Index.</param>
		public ACTableItem ItemAtIndex(ACTableIndex index){

			//Return the item being pointed to by this index
			return _sections [index.sectionPosition].items [index.itemPosition];
		}

		/// <summary>
		/// Returns the <see cref="ActionComponents.ACTableItem"/> being pointed to by the given
		/// <c>position</c>
		/// </summary>
		/// <returns>The at index.</returns>
		/// <param name="position">Position.</param>
		public ACTableItem ItemAtIndex(int position){
			return ItemAtIndex (_index[position]);
		}

		/// <summary>
		/// Returns the <c>string</c> value for the item pointed to by the given <see cref="ActionComponents.ACTableIndex"/> 
		/// </summary>
		/// <returns>The atindex.</returns>
		/// <param name="index">Index.</param>
		public string StringAtIndex(ACTableIndex index){

			//Return based on type
			if (index.isHeader) {
				return _sections [index.sectionPosition].header;
			} else if (index.isFooter) {
				return _sections [index.sectionPosition].footer;
			} else {
				return _sections [index.sectionPosition].items [index.itemPosition].text;
			}

		}

		/// <summary>
		/// Returns the <c>string</c> value for the item pointed to by the given <see cref="ActionComponents.ACTableIndex"/> 
		/// </summary>
		/// <returns>The at index.</returns>
		/// <param name="position">Position.</param>
		public string StringAtIndex(int position){
			return StringAtIndex (index[position]);
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
			_index.Clear ();
		}

		/// <summary>
		/// Adds the given <see cref="ActionComponents.ACTableSection"/> to this <see cref="ActionComponents.ACTableViewDataSource"/> 
		/// </summary>
		/// <param name="section">Section.</param>
		public void AddSection(ACTableSection section){
			_sections.Add (section);

			//Wire-up the activity
			section.activity = _activity;
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
		/// <param name="imageID">Image ID.</param>
		public ACTableSection AddSection(string header, int imageID){
			ACTableSection section = new ACTableSection (header,imageID);
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
		/// <param name="imageID">Image ID.</param>
		/// <param name="footer">Footer.</param>
		public ACTableSection AddSection(string header, int imageID, string footer){
			ACTableSection section = new ACTableSection (header, imageID, footer);
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

		#region Private Methods
		/// <summary>
		/// Requests that this <see cref="ActionComponents.ACTableViewDataSource"/> reconstruct the index used to translate a <c>ListView</c> 
		/// <c>position</c> into an <see cref="ActionComponents.ACTableViewController"/> 
		/// <see cref="ActionComponents.ACTableSection"/> and <see cref="ActionComponents.ACTableItem"/> index.   
		/// </summary>
		private void RebuildIndex(){
			int s = 0, i = 0;

			//Clear existing index
			_index.Clear ();

			//Process all sections
			foreach(ACTableSection section in _sections){
				//Does this section have a header?
				if (section.header != "")
					_index.Add (new ACTableIndex(s,-1));

				//Process all items for this section
				i = 0;
				foreach(ACTableItem item in section.items){
					//Add an index for this item and increment
					_index.Add (new ACTableIndex(s,i++));
				}

				//Does this section have a footer?
				if (section.footer != "")
					_index.Add (new ACTableIndex(s,-2));

				//Increment section index
				++s;
			}

		}

		/// <summary>
		/// Adjust the style of the given item from an iOS type to and Android specific type for display.
		/// </summary>
		/// <returns>The cell style.</returns>
		/// <param name="item">Item.</param>
		private UITableViewCellStyle AdjustCellStyle(ACTableItem item){
			UITableViewCellStyle style = item.style;

			//Switch any iOS styles into their Android counterparts
			switch(item.style){
			case UITableViewCellStyle.Default:
				//Does this item have an image?
				if (item.imageSource == ACTableItemImageSource.None) {
					//No, adjust based on the presense of detail text
					if (item.details == "") {
						style = UITableViewCellStyle.SimpleListItem1;
					} else {
						style = UITableViewCellStyle.SimpleListItem2;
					}
				} else {
					//Yes, adjust based on the presense of detail text
					if (item.details == "") {
						style = UITableViewCellStyle.ActivityListItem;
					} else {
						style = UITableViewCellStyle.ActionTableCell;
					}
				}
				break;
			case UITableViewCellStyle.Subtitle:
				style = UITableViewCellStyle.SimpleListItem2;
				break;
			case UITableViewCellStyle.Value1:
			case UITableViewCellStyle.Value2:
				style = UITableViewCellStyle.TwoLineListItem;
				break;
			}

			//Take action based on the accessory type
			switch (item.accessory) {
			case UITableViewCellAccessory.Checkmark:
			case UITableViewCellAccessory.DisclosureIndicator:
			case UITableViewCellAccessory.DetailedDisclosureButton:
				style = UITableViewCellStyle.ActionTableCell;
				break;
			}

			//Is there an accessory view attached?
			if (item.accessoryView != null) style = UITableViewCellStyle.ActionTableCell;

			//Return adjusted style
			return style;
		}
		#endregion 

		#region Override Methods
		/// <summary>
		/// Gets the unique ID of the item at the given position
		/// </summary>
		/// <returns>The item identifier.</returns>
		/// <param name="position">Position.</param>
		public override long GetItemId (int position)
		{
			return position;
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="ActionComponents.ACTableViewDataSource"/> contains any data
		/// </summary>
		/// <value><c>true</c> if this instance is empty; otherwise, <c>false</c>.</value>
		public override bool IsEmpty {
			get {
				return (_sections.Count == 0);
			}
		}

		/// <summary>
		/// Gets the <c>text</c> property of the <see cref="ActionComponents.ACTableItem"/> in the give <see cref="ActionComponents.ACTablesection"/> 
		/// being controlled by this <see cref="ActionComponents.ACTableViewDataSource"/> 
		/// </summary>
		/// <param name="position">Position.</param>
		/// <remarks>If the datasource is empty it returns an empty string ""</remarks>
		public override string this[int position] {
			get {
				//Anything to return?
				if (IsEmpty) {
					return "";
				} else {
					return StringAtIndex(position);
				}
			}
		}

		/// <summary>
		/// Returns the number of <see cref="ActionComponents.ACTableItem"/> in all <see cref="ActionComponents.ACTableSection"/>s 
		/// contained in this <see cref="ActionComponents.ACTableViewDataSource"/> 
		/// </summary>
		/// <value>The count.</value>
		public override int Count {
			get {
				//Anything to return?
				if (IsEmpty) {
					return 0;
				} else {
					//Return all lines in the resulting table
					return index.Count; 
				}
			}
		}

		/// <summary>
		/// Gets the view.
		/// </summary>
		/// <returns>The view.</returns>
		/// <param name="position">Position.</param>
		/// <param name="convertView">Convert view.</param>
		/// <param name="parent">Parent.</param>
		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			ACTableSection section;
			ACTableItem item;
			ACTableCell view;

			//Is this a header or footer?
			if (index[position].isHeader) {
				//Yes, grab the section
				section = SectionAtIndex (position);

				//Create a new cell to hold the header
				view = new ACTableCell (activity.BaseContext, UITableViewCellStyle.ActivityListItem);
				view.Id = (_controller.tableID+position)*100;

				//Attach the header
				view.AttachSectionHeader (section, _controller);

				//We're done populating
				return view;
			} else if (index[position].isFooter) {
				//Yes, grab the section
				section = SectionAtIndex (position);

				//Create a new cell to hold the header
				view = new ACTableCell (activity.BaseContext, UITableViewCellStyle.ActivityListItem);
				view.Id = (_controller.tableID+position)*100;

				//Attach the footer
				view.AttachSectionFooter (section, _controller);

				//Yes, then we're done populating
				return view;
			}

			//Access the item being pointed to
			item = ItemAtIndex (position);

			//Do we need to construct a new cell?
			if (item.cell == null) {
				//Yes, assemble view
				view = new ACTableCell (activity.BaseContext,AdjustCellStyle(item));

				//Create a unique ID for this cell
				view.Id = (_controller.tableID+position)*100;
				//Console.WriteLine ("Constructed cell {0} to hold item {1}", view.Id, item.text);

				//Populate view
				view.AttachItem (item,_controller);

				//Save new view
				item.cell = view;
			} else {
				view = (ACTableCell)item.cell;
			}

			//Return the populated view
			return view;
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
			Android.Widget.Toast.MakeText(_controller.Context, "ACTable by Appracatappra, LLC.", Android.Widget.ToastLength.Long).Show();
#else
			AppracatappraLicenseManager.ValidateLicense(_controller.Context);
#endif
		}
		#endregion 
	}
}

