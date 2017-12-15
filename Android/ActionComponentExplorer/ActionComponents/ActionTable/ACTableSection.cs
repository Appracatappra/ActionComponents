using System;
using System.Collections.Generic;
using System.Drawing;

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
	/// The <see cref="ActionComponents.ACTableSection"/> works with the <see cref="ActionComponents.ACTableViewController"/> to provide
	/// a simple way to present tabular information without have to create a lot of repetitive code. It contains a collection of <see cref="ActionComponents.ACTableItem"/>s
	/// used to build a standard UITableView from.
	/// </summary>
	/// <remarks>Available in ActionPack Business or Enterprise only.</remarks>
	public class ACTableSection
	{
		#region Private Variables
		private ACTableItemImageSource _imageSource = ACTableItemImageSource.None;
		private List<ACTableItem> _items = new List<ACTableItem> ();
		private Activity _activity;
		#endregion 

		#region Computed Properties
		/// <summary>
		/// Gets or sets the [OPTIONAL] object tag for this <see cref="ActionComponents.ACTableSection"/> 
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

				//Propogate activity
				foreach (ACTableItem item in items) {
					item.activity = _activity;
				}
			}
		}

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
		/// Gets or sets the source of an image displayed for this <see cref="ActionComponents.ACTableItem"/> 
		/// </summary>
		/// <value>The image source.</value>
		public ACTableItemImageSource imageSource {
			get{ return _imageSource;}
			set{ _imageSource = value;}
		}

		/// <summary>
		/// Gets or sets the source of an image displayed for this <see cref="ActionComponents.ACTableItem"/> 
		/// </summary>
		/// <value>The image file.</value>
		/// <remarks>This property will be used based on the value of the <c>imageSource</c> property</remarks>
		public string imageFile {get; set;}

		/// <summary>
		/// Gets or sets the ID of the image resource displayed for this <see cref="ActionComponents.ACTableItem"/> 
		/// </summary>
		/// <value>The image ID</value>
		public int imageID {get; set;}

		/// <summary>
		/// Gets the image for this <see cref="ActionComponents.ACTableItem"/> 
		/// </summary>
		/// <value>The image.</value>
		/// <remarks>This property will be used based on the value of the <c>imageSource</c> property</remarks>
		public Bitmap image {
			get{
				// Get image based on the imageSource flag
				switch (imageSource) {
					case ACTableItemImageSource.None:
					return null;
					case ACTableItemImageSource.FromFile:
					return ACImage.FromFile (imageFile);
					case ACTableItemImageSource.FromResource:
					return ACImage.FromResource (_activity.Resources , imageID);
					case ACTableItemImageSource.CustomDrawn:
					return RaiseRequestCustomImage ();
					default:
					return null;
				}
			}
		}

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
			this.footer = "";
			this.sectionCellID = header.Replace(" ","");
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACTableSection"/> class.
		/// </summary>
		/// <param name="header">Header.</param>
		/// <param name="imageID">Image ID.</param>
		public ACTableSection (string header, int imageID)
		{
			//Initialize
			this.header = header;
			this.footer = "";
			this.imageSource = ACTableItemImageSource.FromResource;
			this.imageID = imageID;
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
		/// <param name="imageID">Image I.</param>
		/// <param name="footer">Footer.</param>
		public ACTableSection (string header, int imageID, string footer)
		{
			//Initialize
			this.header = header;
			this.footer = footer;
			this.imageSource = ACTableItemImageSource.FromResource;
			this.imageID = imageID;
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

			//Attach activity
			tableItem.activity = _activity;

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
		/// <param name="imageID">Image ID.</param>
		/// <param name="text">Text.</param>
		/// <param name="details">Details.</param>
		/// <param name="canSelect">If set to <c>true</c> can select.</param>
		public ACTableItem AddItem(int imageID, string text, string details, bool canSelect){
			ACTableItem item = new ACTableItem (imageID, text, details, canSelect);
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
		/// <param name="imageID">Image ID.</param>
		/// <param name="text">Text.</param>
		/// <param name="details">Details.</param>
		/// <param name="style">Style.</param>
		/// <param name="canSelect">If set to <c>true</c> can select.</param>
		public ACTableItem AddItem(int imageID, string text, string details, UITableViewCellStyle style, bool canSelect){
			ACTableItem item = new ACTableItem (imageID, text, details, style, canSelect);
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
		/// Occurs when this <see cref="ActionComponents.ACTableItem"/> needs the caller to draw a custom image
		/// </summary>
		/// <remarks>This event will be raised based on the state of the <c>imageSource</c> property</remarks>
		public delegate Bitmap ACTableSectionRequestCustomImageDelegate (ACTableSection section);
		public event ACTableSectionRequestCustomImageDelegate RequestCustomImage;

		/// <summary>
		/// Raises the request custom image event
		/// </summary>
		/// <returns>The request custom image.</returns>
		internal Bitmap RaiseRequestCustomImage ()
		{
			if (this.RequestCustomImage == null) {
				return null;
			} else {
				return this.RequestCustomImage (this);
			}
		}

		/// <summary>
		/// Occurs when a <see cref="ActionComponents.ACTableSection"/> <c>Header</c> row is touched 
		/// </summary>
		public delegate void SectionHeaderTouchedDelegate (ACTableSection section);
		public event SectionHeaderTouchedDelegate SectionHeaderTouched;

		/// <summary>
		/// Raises the section header touched event
		/// </summary>
		internal void RaiseSectionHeaderTouched(){
			if (this.SectionHeaderTouched != null)
				this.SectionHeaderTouched (this);
		}

		/// <summary>
		/// Occurs when a <see cref="ActionComponents.ACTableSection"/> <c>Header</c> row is touched 
		/// </summary>
		public delegate void SectionFooterTouchedDelegate (ACTableSection section);
		public event SectionFooterTouchedDelegate SectionFooterTouched;

		/// <summary>
		/// Raises the section header touched event
		/// </summary>
		internal void RaiseSectionFooterTouched(){
			if (this.SectionFooterTouched != null)
				this.SectionFooterTouched (this);
		}
		#endregion 
	}
}

