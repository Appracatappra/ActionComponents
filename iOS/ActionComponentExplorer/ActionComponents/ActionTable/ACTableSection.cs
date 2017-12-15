using System;
using System.Collections.Generic;
using System.Drawing;

using Foundation;
using UIKit;
using CoreGraphics;

namespace ActionComponents
{
	/// <summary>
	/// The <see cref="ActionComponents.ACTableSection"/> works with the <see cref="ActionComponents.ACTableViewController"/> to provide
	/// a simple way to present tabular information without have to create a lot of repetitive code. It contains a collection of <see cref="ActionComponents.ACTableItem"/>s
	/// used to build a standard UITableView from.
	/// </summary>
	/// <remarks>Available in ActionPack Business or Enterperise only.</remarks>
	public class ACTableSection
	{
		#region Private Variables
		private List<ACTableItem> _items = new List<ACTableItem> ();
		#endregion 

		#region Computed Properties
		/// <summary>
		/// Gets or sets the [OPTIONAL] object tag for this <see cref="ActionComponents.ACTableSection"/> 
		/// </summary>
		/// <value>The tag.</value>
		public object tag {get; set;}

		/// <summary>
		/// Gets or sets the header for this <see cref="ActionComponents.ACTableSection"/> 
		/// </summary>
		/// <value>The header.</value>
		public string header {get; set;}

		/// <summary>
		/// Gets or sets the ID used to uniquely idetify the cell created from the <see cref="ActionComponents.ACTableItem"/>s wihtin this 
		/// <see cref="ActionComponents.ACTableSection"/> 
		/// </summary>
		/// <value>The section cell ID</value>
		/// <remarks>If a value isn't specified for this value the section header will be used with any spaces removed</remarks>
		public string sectionCellID {get; set;}

		/// <summary>
		/// Gets or sets the footer for this <see cref="ActionComponents.ACTableSection"/> 
		/// </summary>
		/// <value>The footer.</value>
		public string footer {get; set;}

		/// <summary>
		/// Gets the list of <see cref="ActionComponents.ACTableItem"/>s for this <see cref="ActionComponents.ACTableSection"/>  
		/// </summary>
		/// <value>The items.</value>
		public List<ACTableItem> items{
			get{ return _items;}
		}

		/// <summary>
		/// Returns the number of <see cref="ActionComponents.ACTableItem"/> in this <see cref="ActionComponents.ACTableSection"/>'s
		/// collection of items
		/// </summary>
		/// <value>The count.</value>
		public int count{
			get{ return _items.Count;}
		}
		#endregion 

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACTableSection"/> class.
		/// </summary>
		/// <param name="header">Header.</param>
		public ACTableSection (string header)
		{
			//Initialize
			this.header = header;
			this.sectionCellID = header.Replace(" ","");
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACTableSection"/> class.
		/// </summary>
		/// <param name="header">Header.</param>
		/// <param name="footer">Footer.</param>
		public ACTableSection (string header, string footer)
		{
			//Initialize
			this.header = header;
			this.footer = footer;
			this.sectionCellID = header.Replace(" ","");
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACTableSection"/> class.
		/// </summary>
		/// <param name="header">Header.</param>
		/// <param name="footer">Footer.</param>
		/// <param name="sectionCellID">Section cell ID.</param>
		public ACTableSection (string header, string footer, string sectionCellID)
		{
			//Initialize
			this.header = header;
			this.footer = footer;
			this.sectionCellID = sectionCellID;
		}
		#endregion

		#region Public Properties

		/// <summary>
		/// Removes the <see cref="ActionComponents.ACTableItem"/> at index from this section's collection.
		/// </summary>
		/// <param name="index">Index.</param>
		public void RemoveItemAt(int index){
			_items.RemoveAt (index);
		}

		/// <summary>
		/// Removes all <see cref="ActionComponents.ACTableItem"/> from this section's collection. 
		/// </summary>
		public void Clear(){
			_items.Clear ();
		}

		/// <summary>
		/// Adds the <see cref="ActionComponents.ACTableItem"/> to this section's collection
		/// </summary>
		/// <param name="tableItem">Item.</param>
		public void AddItem(ACTableItem tableItem){
			_items.Add (tableItem);

			//Wire-up events
			tableItem.ItemsSelected += (item) => {
				RaiseItemSelected(item);
			};

			tableItem.ItemValueChanged += (item) => {
				RaiseItemValueChanged(item);
			};

			tableItem.AccessoryButtonTapped += (item) => {
				RaiseAccessoryButtonTapped(item);
			};

			tableItem.DidEndEditing += (item) => {
				RaiseDidEndEditing(item);
			};

		}

		/// <summary>
		/// Creates a new <see cref="ActionComponents.ACTableItem"/> and adds it to this section's collection
		/// </summary>
		/// <returns>The item.</returns>
		/// <param name="text">Text.</param>
		/// <param name="canSelect">If set to <c>true</c> can select.</param>
		public ACTableItem AddItem(string text, bool canSelect){
			ACTableItem item = new ACTableItem (text, canSelect);
			AddItem (item);
			return item;
		}

		/// <summary>
		/// Creates a new <see cref="ActionComponents.ACTableItem"/> and adds it to this section's collection
		/// </summary>
		/// <returns>The item.</returns>
		/// <param name="text">Text.</param>
		/// <param name="details">Details.</param>
		/// <param name="canSelect">If set to <c>true</c> can select.</param>
		public ACTableItem AddItem(string text, string details, bool canSelect){
			ACTableItem item = new ACTableItem (text, details, canSelect);
			AddItem (item);
			return item;
		}

		/// <summary>
		/// Creates a new <see cref="ActionComponents.ACTableItem"/> and adds it to this section's collection
		/// </summary>
		/// <returns>The item.</returns>
		/// <param name="text">Text.</param>
		/// <param name="details">Details.</param>
		/// <param name="style">Style.</param>
		/// <param name="canSelect">If set to <c>true</c> can select.</param>
		public ACTableItem AddItem(string text, string details, UITableViewCellStyle style, bool canSelect){
			ACTableItem item = new ACTableItem (text, details, style, canSelect);
			AddItem (item);
			return item;
		}

		/// <summary>
		/// Creates a new <see cref="ActionComponents.ACTableItem"/> and adds it to this section's collection
		/// </summary>
		/// <returns>The item.</returns>
		/// <param name="imageFile">Image file.</param>
		/// <param name="text">Text.</param>
		/// <param name="details">Details.</param>
		/// <param name="canSelect">If set to <c>true</c> can select.</param>
		public ACTableItem AddItem(string imageFile, string text, string details, bool canSelect){
			ACTableItem item = new ACTableItem (imageFile, text, details, canSelect);
			AddItem (item);
			return item;
		}

		/// <summary>
		/// Creates a new <see cref="ActionComponents.ACTableItem"/> and adds it to this section's collection
		/// </summary>
		/// <returns>The item.</returns>
		/// <param name="customImage">If set to <c>true</c> custom image.</param>
		/// <param name="text">Text.</param>
		/// <param name="details">Details.</param>
		/// <param name="canSelect">If set to <c>true</c> can select.</param>
		public ACTableItem AddItem(bool customImage, string text, string details, bool canSelect){
			ACTableItem item = new ACTableItem (customImage, text, details, canSelect);
			AddItem (item);
			return item;
		}

		/// <summary>
		/// Creates a new <see cref="ActionComponents.ACTableItem"/> and adds it to this section's collection
		/// </summary>
		/// <returns>The item.</returns>
		/// <param name="imageFile">Image file.</param>
		/// <param name="text">Text.</param>
		/// <param name="details">Details.</param>
		/// <param name="style">Style.</param>
		/// <param name="canSelect">If set to <c>true</c> can select.</param>
		public ACTableItem AddItem(string imageFile, string text, string details, UITableViewCellStyle style, bool canSelect){
			ACTableItem item = new ACTableItem (imageFile, text, details, style, canSelect);
			AddItem (item);
			return item;
		}

		/// <summary>
		/// Creates a new <see cref="ActionComponents.ACTableItem"/> and adds it to this section's collection
		/// </summary>
		/// <returns>The item.</returns>
		/// <param name="imageFile">Image file.</param>
		/// <param name="text">Text.</param>
		/// <param name="details">Details.</param>
		/// <param name="style">Style.</param>
		/// <param name="accessory">Accessory.</param>
		/// <param name="canSelect">If set to <c>true</c> can select.</param>
		public ACTableItem AddItem(string imageFile, string text, string details, UITableViewCellStyle style, UITableViewCellAccessory accessory, bool canSelect){
			ACTableItem item = new ACTableItem (imageFile, text, details, style, accessory, canSelect);
			AddItem (item);
			return item;
		}

		/// <summary>
		/// Creates a new <see cref="ActionComponents.ACTableItem"/> and adds it to this section's collection
		/// </summary>
		/// <returns>The item.</returns>
		/// <param name="customImage">If set to <c>true</c> custom image.</param>
		/// <param name="text">Text.</param>
		/// <param name="details">Details.</param>
		/// <param name="style">Style.</param>
		/// <param name="accessory">Accessory.</param>
		/// <param name="canSelect">If set to <c>true</c> can select.</param>
		public ACTableItem AddItem(bool customImage, string text, string details, UITableViewCellStyle style, UITableViewCellAccessory accessory, bool canSelect){
			ACTableItem item = new ACTableItem (customImage, text, details, style, accessory, canSelect);
			AddItem (item);
			return item;
		}

		/// <summary>
		/// Creates a new <see cref="ActionComponents.ACTableItem"/> and adds it to this section's collection
		/// </summary>
		/// <returns>The item.</returns>
		/// <param name="imageSource">Image source.</param>
		/// <param name="imageFile">Image file.</param>
		/// <param name="text">Text.</param>
		/// <param name="details">Details.</param>
		/// <param name="style">Style.</param>
		/// <param name="accessory">Accessory.</param>
		/// <param name="canSelect">If set to <c>true</c> can select.</param>
		public ACTableItem AddItem(ACTableItemImageSource imageSource, string imageFile, string text, string details, UITableViewCellStyle style, UITableViewCellAccessory accessory, bool canSelect){
			ACTableItem item = new ACTableItem (imageSource, imageFile, text, details, style, accessory, canSelect);
			AddItem (item);
			return item;
		}
		#endregion 

		#region Internal Methods
		/// <summary>
		/// Does this <see cref="ActionComponents.ACTableSection"/> define a footer height
		/// </summary>
		/// <returns><c>true</c>, if footer height was defined, <c>false</c> otherwise.</returns>
		internal bool DefinesFooterHeight(){
			return (this.RequestFooterHeight != null);
		}

		/// <summary>
		/// Gets the height of the footer.
		/// </summary>
		/// <returns>The footer height.</returns>
		internal float GetFooterHeight(){
			return this.RequestFooterHeight (this);
		}

		/// <summary>
		/// Does this <see cref="ActionComponents.ACTableSection"/> define a header height
		/// </summary>
		/// <returns><c>true</c>, if footer height was defined, <c>false</c> otherwise.</returns>
		internal bool DefinesHeaderHeight(){
			return (this.RequestHeaderHeight != null);
		}

		/// <summary>
		/// Gets the height of the header.
		/// </summary>
		/// <returns>The header height.</returns>
		internal float GetHeaderHeight(){
			return this.RequestHeaderHeight (this);
		}

		/// <summary>
		/// Does this <see cref="ActionComponents.ACTableSection"/> define an item height?
		/// </summary>
		/// <returns><c>true</c>, if item height was defined, <c>false</c> otherwise.</returns>
		internal bool DefinesItemHeight(){
			return (this.RequestItemHeight != null);
		}

		/// <summary>
		/// Gets the height of the item.
		/// </summary>
		/// <returns>The item height.</returns>
		internal float GetItemHeight(ACTableItem item){
			return this.RequestItemHeight (item);
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
		/// <param name="item">Item.</param>
		internal void RaiseItemSelected (ACTableItem item)
		{
			//If an event was attached, fire it off now
			if (this.ItemsSelected != null)
				this.ItemsSelected (item);
		}

		/// <summary>
		/// Occurs when the value for this <see cref="ActionComponents.ACTableItem"/> have been changed 
		/// </summary>
		public delegate void ACTableItemValueChangedDelegate (ACTableItem item);
		public event ACTableItemValueChangedDelegate ItemValueChanged;

		/// <summary>
		/// Raises the item value changed event
		/// </summary>
		/// <param name="item">Item.</param>
		internal void RaiseItemValueChanged (ACTableItem item)
		{
			if (this.ItemValueChanged != null) {
				this.ItemValueChanged (item);
			}
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

		/// <summary>
		/// Occurs when editing has ended for the <see cref="ActionComponents.ACTableItem"/> 
		/// </summary>
		public delegate void DidEndEditingDelegate (ACTableItem item);
		public event DidEndEditingDelegate DidEndEditing;

		/// <summary>
		/// Raises the accessory button tapped event
		/// </summary>
		internal void RaiseDidEndEditing (ACTableItem item)
		{
			//If an event was attached, fire it off now
			if (this.DidEndEditing != null)
				this.DidEndEditing (item);
		}

		/// <summary>
		/// Occurs when the table needs the height of the header for this <see cref="ActionComponents.ACTableSection"/> 
		/// </summary>
		public delegate float RequestHeaderHeightDelegate (ACTableSection section);
		public event RequestHeaderHeightDelegate RequestHeaderHeight;

		/// <summary>
		/// Occurs when the table needs the height of the footer for this <see cref="ActionComponents.ACTableSection"/> 
		/// </summary>
		public delegate float RequestFooterHeightDelegate (ACTableSection section);
		public event RequestFooterHeightDelegate RequestFooterHeight;

		/// <summary>
		/// Occurs when the table needs the height of the footer for the <see cref="ActionComponents.ACTableItem"/> 
		/// </summary>
		public delegate float RequestItemHeightDelegate (ACTableItem item);
		public event RequestItemHeightDelegate RequestItemHeight;

		/// <summary>
		/// Occurs when this <see cref="ActionComponents.ACTableItem"/> needs to be highlighted 
		/// </summary>
		public delegate bool ShouldHighlightItemDelegate (ACTableItem item);
		public event ShouldHighlightItemDelegate ShouldHighlightItem;
		#endregion 
	}
}

