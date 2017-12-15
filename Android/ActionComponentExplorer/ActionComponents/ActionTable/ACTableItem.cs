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
	/// The <see cref="ActionComponents.ACTableItem"/> works with the <see cref="ActionComponents.ACTableViewController"/> to provide
	/// a simple way to present tabular information without have to create a lot of repetitive code. The <see cref="ActionComponents.ACTableItem"/> will be contained
	/// in a <see cref="ActionComponents.ACTableSection"/> and the resulting information will be displayed in a 
	/// <see cref="ActionComponents.ACTableCell"/> 
	/// </summary>
	/// <remarks>Several methods are available to quickly build up an item and add accessories such as toggle buttons, edit fields, and sliders. An effort has been made to maintain code compatibility
	/// with the iOS version of ActionTable so that as much code as possibly can be reused across platforms. Available in ActionPack Business or Enterprise only.</remarks>
	public class ACTableItem
	{
		#region Private Variables
		private ACTableItemImageSource _imageSource = ACTableItemImageSource.None;
		private Activity _activity;
		private ACTableAccessorySwitchDelegate _accessorySwitchDelegate = null;
		private ACTableAccessoryStepperDelegate _accessoryStepperDelegate = null;
		private ACTableAccessorySliderDelegate _accessorySliderDelegate = null;
		private ACTableAccessoryButtonDelegate _accessoryButtonDelegate = null;
		private ACTableAccessoryTextDelegate _accessoryTextDelegate = null;
		#endregion 

		#region Internal Variables
		internal View cell;
		internal int sliderMinValue = 0;
		#endregion 

		#region Computed Properties
		/// <summary>
		/// Gets or sets the [OPTIONAL] object tag that can be attached to this <see cref="ActionComponents.ACTableItem"/> 
		/// </summary>
		/// <value>The tag.</value>
		public object tag {get; set;}

		/// <summary>
		/// Gets or sets the activity that this <see cref="ActionComponents.ACTableItem"/> is attached to
		/// </summary>
		/// <value>The activity.</value>
		public Activity activity{
			get{ return _activity;}
			set{ _activity = value;}
		}

		/// <summary>
		/// Gets or sets the detail text for this <see cref="ActionComponents.ACTableItem"/> 
		/// </summary>
		/// <value>The detail text.</value>
		public string details {get; set;}

		/// <summary>
		/// Gets or sets the text for this <see cref="ActionComponents.ACTableItem"/> 
		/// </summary>
		/// <value>The text.</value>
		public string text {get; set;}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="ActionComponents.ACTableItem"/> can be selected.
		/// </summary>
		/// <value><c>true</c> if this instance can select; otherwise, <c>false</c>.</value>
		public bool canSelect {get; set;}

		/// <summary>
		/// Gets or sets the table cell style for this <see cref="ActionComponents.ACTableItem"/> 
		/// </summary>
		/// <value>The style.</value>
		/// <remarks>The style was designed to maintain compatibility with the iOS version of ActionTable</remarks>
		public UITableViewCellStyle style {get; set;}

		/// <summary>
		/// Gets or sets the table accessory type for this
		/// </summary>
		/// <value>The accessory.</value>
		/// <remarks>The CellAccessory was designed to maintain compatibility with the iOS version of ActionTable</remarks>
		public UITableViewCellAccessory accessory {get; set;}

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
		/// Gets or sets the content view associated with this <see cref="ActionComponents.ACTableItem"/> 
		/// </summary>
		/// <value>The content view.</value>
		/// <remarks>This property is not currently used but here as a placeholder for future expansion</remarks>
		public View contentView {get; set;}

		/// <summary>
		/// Gets or sets the accessory view associated with this <see cref="ActionComponents.ACTableItem"/> 
		/// </summary>
		/// <value>The accessory view.</value>
		public View accessoryView {get; set;}

		/// <summary>
		/// Gets or sets the value being modified by the accessoryView attached to this <see cref="ActionComponents.ACTableItem"/> 
		/// </summary>
		/// <value>The accessory value.</value>
		public object accessoryValue {get; set;}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACTableItem"/> class.
		/// </summary>
		public ACTableItem (){
			//Initialize
			this.text = "";
			this.details = "";
			this.style = UITableViewCellStyle.Default;
			this.accessory = UITableViewCellAccessory.None;
			this.imageSource = ACTableItemImageSource.None;
			this.canSelect = false;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACTableItem"/> class.
		/// </summary>
		/// <param name="text">Text.</param>
		/// <param name="canSelect">If set to <c>true</c> can select.</param>
		public ACTableItem (string text, bool canSelect)
		{
			//Initialize
			this.text = text;
			this.details = "";
			this.style = UITableViewCellStyle.Default;
			this.accessory = UITableViewCellAccessory.None;
			this.imageSource = ACTableItemImageSource.None;
			this.canSelect = canSelect;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACTableItem"/> class.
		/// </summary>
		/// <param name="text">Text.</param>
		/// <param name="details">Details.</param>
		/// <param name="canSelect">If set to <c>true</c> can select.</param>
		public ACTableItem (string text, string details, bool canSelect)
		{
			//Initialize
			this.text = text;
			this.details = details;
			this.style = UITableViewCellStyle.Subtitle;
			this.accessory = UITableViewCellAccessory.None;
			this.imageSource = ACTableItemImageSource.None;
			this.canSelect = canSelect;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACTableItem"/> class.
		/// </summary>
		/// <param name="text">Text.</param>
		/// <param name="details">Details.</param>
		/// <param name="style">Style.</param>
		/// <param name="canSelect">If set to <c>true</c> can select.</param>
		public ACTableItem (string text, string details, UITableViewCellStyle style, bool canSelect)
		{
			//Initialize
			this.text = text;
			this.details = details;
			this.style = style;
			this.accessory = UITableViewCellAccessory.None;
			this.imageSource = ACTableItemImageSource.None;
			this.canSelect = canSelect;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACTableItem"/> class.
		/// </summary>
		/// <param name="imageFile">Image file.</param>
		/// <param name="text">Text.</param>
		/// <param name="details">Details.</param>
		/// <param name="canSelect">If set to <c>true</c> can select.</param>
		public ACTableItem (string imageFile, string text, string details, bool canSelect)
		{
			//Initialize
			this.text = text;
			this.details = details;
			this.style = UITableViewCellStyle.Subtitle;
			this.accessory = UITableViewCellAccessory.None;
			this.imageSource = ACTableItemImageSource.FromFile;
			this.imageFile = imageFile;
			this.canSelect = canSelect;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACTableItem"/> class.
		/// </summary>
		/// <param name="imageID">Image ID.</param>
		/// <param name="text">Text.</param>
		/// <param name="details">Details.</param>
		/// <param name="canSelect">If set to <c>true</c> can select.</param>
		public ACTableItem (int imageID, string text, string details, bool canSelect)
		{
			//Initialize
			this.text = text;
			this.details = details;
			this.style = UITableViewCellStyle.Subtitle;
			this.accessory = UITableViewCellAccessory.None;
			this.imageSource = ACTableItemImageSource.FromResource;
			this.imageID = imageID;
			this.canSelect = canSelect;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACTableItem"/> class.
		/// </summary>
		/// <param name="customImage">If set to <c>true</c> custom image.</param>
		/// <param name="text">Text.</param>
		/// <param name="details">Details.</param>
		/// <param name="canSelect">If set to <c>true</c> can select.</param>
		public ACTableItem (bool customImage, string text, string details, bool canSelect)
		{
			//Initialize
			this.text = text;
			this.details = details;
			this.style = UITableViewCellStyle.Subtitle;
			this.accessory = UITableViewCellAccessory.None;
			this.imageSource = (customImage) ? ACTableItemImageSource.CustomDrawn : ACTableItemImageSource.None;
			this.canSelect = canSelect;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACTableItem"/> class.
		/// </summary>
		/// <param name="imageFile">Image file.</param>
		/// <param name="text">Text.</param>
		/// <param name="details">Details.</param>
		/// <param name="style">Style.</param>
		/// <param name="canSelect">If set to <c>true</c> can select.</param>
		public ACTableItem (string imageFile, string text, string details, UITableViewCellStyle style, bool canSelect)
		{
			//Initialize
			this.text = text;
			this.details = details;
			this.style = style;
			this.accessory = UITableViewCellAccessory.None;
			this.imageSource = ACTableItemImageSource.FromFile;
			this.imageFile = imageFile;
			this.canSelect = canSelect;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACTableItem"/> class.
		/// </summary>
		/// <param name="imageID">Image I.</param>
		/// <param name="text">Text.</param>
		/// <param name="details">Details.</param>
		/// <param name="style">Style.</param>
		/// <param name="canSelect">If set to <c>true</c> can select.</param>
		public ACTableItem (int imageID, string text, string details, UITableViewCellStyle style, bool canSelect)
		{
			//Initialize
			this.text = text;
			this.details = details;
			this.style = style;
			this.accessory = UITableViewCellAccessory.None;
			this.imageSource = ACTableItemImageSource.FromResource;
			this.imageID = imageID;
			this.canSelect = canSelect;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACTableItem"/> class.
		/// </summary>
		/// <param name="imageFile">Image file.</param>
		/// <param name="text">Text.</param>
		/// <param name="details">Details.</param>
		/// <param name="style">Style.</param>
		/// <param name="accessory">Accessory.</param>
		/// <param name="canSelect">If set to <c>true</c> can select.</param>
		public ACTableItem (string imageFile, string text, string details, UITableViewCellStyle style, UITableViewCellAccessory accessory, bool canSelect)
		{
			//Initialize
			this.text = text;
			this.details = details;
			this.style = style;
			this.accessory = accessory;
			this.imageSource = ACTableItemImageSource.FromFile;
			this.imageFile = imageFile;
			this.canSelect = canSelect;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACTableItem"/> class.
		/// </summary>
		/// <param name="customImage">If set to <c>true</c> custom image.</param>
		/// <param name="text">Text.</param>
		/// <param name="details">Details.</param>
		/// <param name="style">Style.</param>
		/// <param name="accessory">Accessory.</param>
		/// <param name="canSelect">If set to <c>true</c> can select.</param>
		public ACTableItem (bool customImage, string text, string details, UITableViewCellStyle style, UITableViewCellAccessory accessory, bool canSelect)
		{
			//Initialize
			this.text = text;
			this.details = details;
			this.style = style;
			this.accessory = accessory;
			this.imageSource = (customImage) ? ACTableItemImageSource.CustomDrawn : ACTableItemImageSource.None;
			this.canSelect = canSelect;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACTableItem"/> class.
		/// </summary>
		/// <param name="imageSource">Image source.</param>
		/// <param name="imageFile">Image file.</param>
		/// <param name="text">Text.</param>
		/// <param name="details">Details.</param>
		/// <param name="style">Style.</param>
		/// <param name="accessory">Accessory.</param>
		/// <param name="canSelect">If set to <c>true</c> can select.</param>
		public ACTableItem (ACTableItemImageSource imageSource, string imageFile, string text, string details, UITableViewCellStyle style, UITableViewCellAccessory accessory, bool canSelect)
		{
			//Initialize
			this.text = text;
			this.details = details;
			this.style = style;
			this.accessory = accessory;
			this.imageSource = imageSource;
			this.imageFile = imageFile;
			this.canSelect = canSelect;
		}
		#endregion 

		#region Internal Methods

		#endregion 

		#region Public Methods
		/// <summary>
		/// Adds a switch accessory to this <see cref="ActionComponents.ACTableItem"/> 
		/// </summary>
		/// <returns>The accessory switch.</returns>
		/// <param name="value">The value that will be adjusted by the switch</param>
		public ToggleButton AddAccessorySwitch(bool value){
			ToggleButton sw = new ToggleButton(activity.BaseContext);

			//Define position
			var rl = new RelativeLayout.LayoutParams (80, 60);
			rl.TopMargin = 10;
			rl.RightMargin = 5;
			rl.AddRule (LayoutRules.AlignParentRight);
			sw.LayoutParameters = rl;

			//Configure
			sw.Checked = value;

			//Save current value
			accessoryValue = value;

			//Respond to the value being changed
			sw.CheckedChange += (sender, e) => {
				//Save new value
				accessoryValue = ((ToggleButton)sender).Checked;
				RaiseItemValueChanged();
			};

			//Attach to this item
			accessoryView = sw;

			//Return
			return sw;
		}

		/// <summary>
		/// Adds a switch accessory to this <see cref="ActionComponents.ACTableItem"/>
		/// </summary>
		/// <returns>The accessory switch.</returns>
		/// <param name="value">If set to <c>true</c> value.</param>
		/// <param name="switchDelegate">Switch delegate.</param>
		public ToggleButton AddAccessorySwitch(bool value, ACTableAccessorySwitchDelegate switchDelegate){
			ToggleButton sw = new ToggleButton(activity.BaseContext);

			//Define position
			var rl = new RelativeLayout.LayoutParams (80, 60);
			rl.TopMargin = 10;
			rl.RightMargin = 5;
			rl.AddRule (LayoutRules.AlignParentRight);
			sw.LayoutParameters = rl;

			//Configure
			sw.Checked = value;

			//Save current value
			accessoryValue = value;
			_accessorySwitchDelegate = switchDelegate;

			//Respond to the value being changed
			sw.CheckedChange += (sender, e) => {
				//Save new value
				accessoryValue = ((ToggleButton)sender).Checked;
				RaiseItemValueChanged();

				// Is a delegate attached?
				if (_accessorySwitchDelegate!=null) {
					_accessorySwitchDelegate(((ToggleButton)sender).Checked);
				}
			};

			//Attach to this item
			accessoryView = sw;

			//Return
			return sw;
		}

		/// <summary>
		/// Adds a stepper accessory to this <see cref="ActionComponents.ACTableItem"/>
		/// </summary>
		/// <returns>The accessory stepper.</returns>
		/// <param name="minimumValue">Minimum value.</param>
		/// <param name="maximumValue">Maximum value.</param>
		/// <param name="stepValue">Step value.</param>
		/// <param name="value">Value.</param>
		/// <remarks>Since Android doesn't have a compariable component to the iOS <c>Stepper</c>, this is currently mapped to a <c>SeekBar</c>.
		/// While still provided the <c>setpValue</c> is currently ignored. </remarks>
		public SeekBar AddAccessoryStepper(int minimumValue, int maximumValue, int stepValue, int value) {

			//For the time being, just map this to a slider accessory
			return AddAccessorySlider (minimumValue, maximumValue, value);
		}

		/// <summary>
		/// Adds a stepper accessory to this <see cref="ActionComponents.ACTableItem"/>
		/// </summary>
		/// <returns>The accessory stepper.</returns>
		/// <param name="minimumValue">Minimum value.</param>
		/// <param name="maximumValue">Maximum value.</param>
		/// <param name="stepValue">Step value.</param>
		/// <param name="value">Value.</param>
		/// <remarks>Since Android doesn't have a compariable component to the iOS <c>Stepper</c>, this is currently mapped to a <c>SeekBar</c>.
		/// While still provided the <c>setpValue</c> is currently ignored. </remarks>
		public SeekBar AddAccessoryStepper(int minimumValue, int maximumValue, int stepValue, int value, ACTableAccessorySliderDelegate sliderDelegate) {

			//For the time being, just map this to a slider accessory
			return AddAccessorySlider (minimumValue, maximumValue, value, sliderDelegate);
		}

		/// <summary>
		/// Adds a slider accessory to this <see cref="ActionComponents.ACTableItem"/> 
		/// </summary>
		/// <returns>The accessory slider.</returns>
		/// <param name="minimumValue">Minimum value.</param>
		/// <param name="maximumValue">Maximum value.</param>
		/// <param name="value">Value.</param>
		public SeekBar AddAccessorySlider(int minimumValue, int maximumValue, int value){
			SeekBar slider = new SeekBar (activity.BaseContext);

			//Define position
			var rl = new RelativeLayout.LayoutParams (200, 80);
			rl.TopMargin = 10;
			rl.RightMargin = 5;
			rl.AddRule (LayoutRules.AlignParentRight);
			slider.LayoutParameters = rl;

			//Configure
			sliderMinValue = minimumValue;
			slider.Max = maximumValue-minimumValue;
			slider.Progress = value;

			//Respond to the value being changed
			slider.ProgressChanged += (sender, e) => {
				//Save new value
				accessoryValue = ((SeekBar)sender).Progress + sliderMinValue;
				RaiseItemValueChanged();
			};

			//Save current value
			accessoryValue = value;

			//Attach to item
			accessoryView = slider;

			//Return slider
			return slider;
		}

		/// <summary>
		/// Adds a slider accessory to this <see cref="ActionComponents.ACTableItem"/>
		/// </summary>
		/// <returns>The accessory slider.</returns>
		/// <param name="minimumValue">Minimum value.</param>
		/// <param name="maximumValue">Maximum value.</param>
		/// <param name="value">Value.</param>
		/// <param name="sliderDelegate">Slider delegate.</param>
		public SeekBar AddAccessorySlider(int minimumValue, int maximumValue, int value, ACTableAccessorySliderDelegate sliderDelegate){
			SeekBar slider = new SeekBar (activity.BaseContext);

			//Define position
			var rl = new RelativeLayout.LayoutParams (200, 80);
			rl.TopMargin = 10;
			rl.RightMargin = 5;
			rl.AddRule (LayoutRules.AlignParentRight);
			slider.LayoutParameters = rl;

			//Configure
			sliderMinValue = minimumValue;
			slider.Max = maximumValue-minimumValue;
			slider.Progress = value;

			//Save current value
			accessoryValue = value;
			_accessorySliderDelegate = sliderDelegate;

			//Respond to the value being changed
			slider.ProgressChanged += (sender, e) => {
				//Save new value
				accessoryValue = ((SeekBar)sender).Progress + sliderMinValue;
				RaiseItemValueChanged();

				// Delegate attached?
				if (_accessorySliderDelegate!=null) {
					_accessorySliderDelegate(((SeekBar)sender).Progress + sliderMinValue);
				}
			};

			//Attach to item
			accessoryView = slider;

			//Return slider
			return slider;
		}

		/// <summary>
		/// Adds a <see cref="Appracatappra.ActionComponents.ActionView.ACImageView"/> accessory to this <see cref="ActionComponents.ACTableItem"/> 
		/// </summary>
		/// <returns>The accessory action image view.</returns>
		/// <param name="filename">Filename.</param>
		public ACImageView AddAccessoryActionImageView(string filename){
			return AddAccessoryActionImageView(ACImage.FromFile(filename));
		}

		/// <summary>
		/// Adds a <see cref="Appracatappra.ActionComponents.ActionView.ACImageView"/> accessory to this <see cref="ActionComponents.ACTableItem"/> 
		/// </summary>
		/// <returns>The accessory action image view.</returns>
		/// <param name="imageID">Image ID.</param>
		public ACImageView AddAccessoryActionImageView(int imageID){
			return AddAccessoryActionImageView(ACImage.FromResource (activity.Resources,imageID));
		}

		/// <summary>
		/// Adds a <see cref="Appracatappra.ActionComponents.ActionView.ACImageView"/> accessory to this <see cref="ActionComponents.ACTableItem"/> 
		/// </summary>
		/// <returns>The accessory action image view.</returns>
		/// <param name="image">Image.</param>
		public ACImageView AddAccessoryActionImageView(Bitmap image){
			ACImageView iv = new ACImageView (activity.BaseContext);

			//Define image position
			var rl = new RelativeLayout.LayoutParams (70,70);
			rl.TopMargin = 5;
			rl.RightMargin = 5;
			rl.AddRule (LayoutRules.AlignParentRight);
			iv.LayoutParameters = rl;

			//Attach image
			iv.SetImageBitmap (image);

			//Save current value
			accessoryValue = image;

			//Attach to item
			accessoryView = iv;

			//Return slider
			return iv;
		}

		/// <summary>
		/// Adds a button accessory to this <see cref="ActionComponents.ACTableItem"/>
		/// </summary>
		/// <returns>The accessory button.</returns>
		/// <param name="width">Width.</param>
		/// <param name="title">Title.</param>
		public Button AddAccessoryButton(int width, string title){
			Button button = new Button (activity.BaseContext);

			//Define image position
			var rl = new RelativeLayout.LayoutParams (width,70);
			rl.TopMargin = 5;
			rl.RightMargin = 5;
			rl.AddRule (LayoutRules.AlignParentRight);
			button.LayoutParameters = rl;

			//Configure button
			button.Text = title;

			//attach to item
			accessoryView = button;

			//Return button
			return button;
		}

		/// <summary>
		/// Adds a button accessory to this <see cref="ActionComponents.ACTableItem"/>
		/// </summary>
		/// <returns>The accessory button.</returns>
		/// <param name="width">Width.</param>
		/// <param name="title">Title.</param>
		/// <param name="buttonDelegate">Button delegate.</param>
		public Button AddAccessoryButton(int width, string title, ACTableAccessoryButtonDelegate buttonDelegate){
			Button button = new Button (activity.BaseContext);

			//Define image position
			var rl = new RelativeLayout.LayoutParams (width,70);
			rl.TopMargin = 5;
			rl.RightMargin = 5;
			rl.AddRule (LayoutRules.AlignParentRight);
			button.LayoutParameters = rl;

			//Configure button
			button.Text = title;

			//attach to item
			accessoryView = button;
			_accessoryButtonDelegate = buttonDelegate;

			// Wireup events
			button.Touch += (sender, e) => {
				// Is there a delegate?
				if (_accessoryButtonDelegate!=null) {
					_accessoryButtonDelegate();
				}
			};

			//Return button
			return button;
		}

		/// <summary>
		/// Adds an <c>EditText</c> accessory to this <see cref="ActionComponents.ACTableItem"/> 
		/// </summary>
		/// <returns>The accessory text field.</returns>
		/// <param name="width">Width.</param>
		/// <param name="placeholder">Placeholder.</param>
		/// <param name="text">Text.</param>
		public EditText AddAccessoryTextField(int width, string placeholder, string text){
			EditText et = new EditText (activity.BaseContext);

			//Define image position
			var rl = new RelativeLayout.LayoutParams (width,70);
			rl.TopMargin = 5;
			rl.RightMargin = 5;
			rl.AddRule (LayoutRules.AlignParentRight);
			et.LayoutParameters = rl;

			//Configure
			et.Hint = placeholder;
			et.Text = text;

			//Wire-up events
			et.TextChanged += (sender, e) => {
				//Save current value
				accessoryValue = ((EditText)sender).Text;
				RaiseItemValueChanged();
			};

			//Save current value
			accessoryValue = text;

			//Attach to item
			accessoryView = et;

			//Return item
			return et;
		}

		/// <summary>
		/// Adds an <c>EditText</c> accessory to this <see cref="ActionComponents.ACTableItem"/> 
		/// </summary>
		/// <returns>The accessory text field.</returns>
		/// <param name="width">Width.</param>
		/// <param name="placeholder">Placeholder.</param>
		/// <param name="text">Text.</param>
		/// <param name="textDelegate">Text delegate.</param>
		public EditText AddAccessoryTextField(int width, string placeholder, string text, ACTableAccessoryTextDelegate textDelegate){
			EditText et = new EditText (activity.BaseContext);

			//Define image position
			var rl = new RelativeLayout.LayoutParams (width,70);
			rl.TopMargin = 5;
			rl.RightMargin = 5;
			rl.AddRule (LayoutRules.AlignParentRight);
			et.LayoutParameters = rl;

			//Configure
			et.Hint = placeholder;
			et.Text = text;

			//Save current value
			accessoryValue = text;
			_accessoryTextDelegate = textDelegate;

			//Wire-up events
			et.TextChanged += (sender, e) => {
				//Save current value
				accessoryValue = ((EditText)sender).Text;
				RaiseItemValueChanged();

				// Delegate attached?
				if (_accessoryTextDelegate !=null) {
					_accessoryTextDelegate(((EditText)sender).Text);
				}
			};
				

			//Attach to item
			accessoryView = et;

			//Return item
			return et;
		}
		#endregion 

		#region Accessory Delegates
		/// <summary>
		/// Occurrs when this <see cref="ActionComponents.ACTableItem"/> has an attached <c>UISwitch</c> accessory
		/// and the value is changed.
		/// </summary>
		public delegate void ACTableAccessorySwitchDelegate (bool on);

		/// <summary>
		/// Occurrs when this <see cref="ActionComponents.ACTableItem"/> has an attached <c>UIStepper</c> accessory
		/// and the value is changed.
		/// </summary>
		public delegate void ACTableAccessoryStepperDelegate (int value);

		/// <summary>
		/// Occurrs when this <see cref="ActionComponents.ACTableItem"/> has an attached <c>UISlider</c> accessory
		/// and the value is changed.
		/// </summary>
		public delegate void ACTableAccessorySliderDelegate (int value);

		/// <summary>
		/// Occurrs when this <see cref="ActionComponents.ACTableItem"/> has an attached <c>UIButton</c> accessory
		/// and the button is tapped.
		/// </summary>
		public delegate void ACTableAccessoryButtonDelegate();

		/// <summary>
		/// Occurrs when this <see cref="ActionComponents.ACTableItem"/> has an attached <c>UITextField</c> accessory
		/// and the value is changed.
		/// </summary>
		public delegate void ACTableAccessoryTextDelegate(string text);
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
		internal void RaiseItemSelected ()
		{
			//If an event was attached, fire it off now
			if (canSelect && this.ItemsSelected != null)
				this.ItemsSelected (this);
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
		internal void RaiseItemValueChanged ()
		{
			if (this.ItemValueChanged != null) {
				this.ItemValueChanged (this);
			}
		}

		/// <summary>
		/// Occurs when this <see cref="ActionComponents.ACTableItem"/> needs the caller to draw a custom image
		/// </summary>
		/// <remarks>This event will be raised based on the state of the <c>imageSource</c> property</remarks>
		public delegate Bitmap ACTableItemRequestCustomImageDelegate (ACTableItem item);
		public event ACTableItemRequestCustomImageDelegate RequestCustomImage;

		/// <summary>
		/// Raises the request custom image event
		/// </summary>
		/// <returns>The request custom image.</returns>
		internal Bitmap RaiseRequestCustomImage() {
			if (this.RequestCustomImage == null) {
				return null;
			} else {
				return this.RequestCustomImage(this);
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
		internal void RaiseAccessoryButtonTapped ()
		{
			//If an event was attached, fire it off now
			if (this.AccessoryButtonTapped != null)
				this.AccessoryButtonTapped (this);
		}
		#endregion 
	}
}

