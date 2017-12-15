using System;
using System.Collections.Generic;
using System.Drawing;

using Foundation;
using UIKit;
using CoreGraphics;

namespace ActionComponents
{
	/// <summary>
	/// The <see cref="ActionComponents.ACTableItem"/> works with the <see cref="ActionComponents.ACTableViewController"/> to provide
	/// a simple way to present tabular information without have to create a lot of repetitive code.
	/// </summary>
	/// <remarks>Available in ActionPack Business or Enterprise only.</remarks>
	public class ACTableItem
	{
		#region Private Variables
		private ACTableItemImageSource _imageSource = ACTableItemImageSource.None;
		private ACTableAccessorySwitchDelegate _accessorySwitchDelegate = null;
		private ACTableAccessoryStepperDelegate _accessoryStepperDelegate = null;
		private ACTableAccessorySliderDelegate _accessorySliderDelegate = null;
		private ACTableAccessoryButtonDelegate _accessoryButtonDelegate = null;
		private ACTableAccessoryTextDelegate _accessoryTextDelegate = null;
		#endregion 

		#region Internal Variables
		internal UITableViewCell cell;
		#endregion 

		#region Computed Properties
		/// <summary>
		/// Gets or sets the [OPTIONAL] object tag that can be attached to this <see cref="ActionComponents.ACTableItem"/> 
		/// </summary>
		/// <value>The tag.</value>
		public object tag {get; set;}

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
		public UITableViewCellStyle style {get; set;}

		/// <summary>
		/// Gets or sets the table accessory type for this
		/// </summary>
		/// <value>The accessory.</value>
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
		/// Gets the image for this <see cref="ActionComponents.ACTableItem"/> 
		/// </summary>
		/// <value>The image.</value>
		/// <remarks>This property will be used based on the value of the <c>imageSource</c> property</remarks>
		public UIImage image {
			get{
				// Get image based on the imageSource flag
				switch (imageSource) {
				case ACTableItemImageSource.None:
					return null;
				case ACTableItemImageSource.FromFile:
					return ACImage.FromFile (imageFile);
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
		public UIView contentView {get; set;}

		/// <summary>
		/// Gets or sets the accessory view associated with this <see cref="ActionComponents.ACTableItem"/> 
		/// </summary>
		/// <value>The accessory view.</value>
		public UIView accessoryView {get; set;}

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
		/// <summary>
		/// Does this <see cref="ActionComponents.ACTableItem"/> define an item height?
		/// </summary>
		/// <returns><c>true</c>, if item height was defines, <c>false</c> otherwise.</returns>
		internal bool DefinesItemHeight(){
			return (this.RequestItemHeight != null);
		}

		/// <summary>
		/// Gets the height of the item.
		/// </summary>
		/// <returns>The item height.</returns>
		internal float GetItemHeight(){
			return this.RequestItemHeight (this);
		}
		#endregion 

		#region Public Methods
		/// <summary>
		/// Adds a switch accessory to this <see cref="ActionComponents.ACTableItem"/> 
		/// </summary>
		/// <returns>The accessory switch.</returns>
		/// <param name="value">The value that will be adjusted by the switch</param>
		public UISwitch AddAccessorySwitch(bool value){
			UISwitch sw = new UISwitch ();

			//Configure
			sw.On = value;

			//Save current value
			accessoryValue = value;

			//Respond to the value being changed
			sw.ValueChanged += (sender, e) => {
				//Save new value
				accessoryValue = ((UISwitch)sender).On;
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
		/// <param name="value">Sets the initial value for the switch</param>
		/// <param name="switchDelegate">This delegate will be called when the switch's value changes.</param>
		public UISwitch AddAccessorySwitch(bool value, ACTableAccessorySwitchDelegate switchDelegate){
			UISwitch sw = new UISwitch ();

			//Configure
			sw.On = value;

			//Save current value
			accessoryValue = value;
			_accessorySwitchDelegate = switchDelegate;

			//Respond to the value being changed
			sw.ValueChanged += (sender, e) => {
				//Save new value
				accessoryValue = ((UISwitch)sender).On;

				// Has a delegate been attached?
				if (_accessorySwitchDelegate!=null) {
					// Yes, call method
					_accessorySwitchDelegate(((UISwitch)sender).On);
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
		/// <param name="value">The value that will be adjusted by this stepper</param>
		public UIStepper AddAccessoryStepper(double minimumValue, double maximumValue, double stepValue, double value){
			UIStepper stepper = new UIStepper ();

			//Configure
			stepper.MinimumValue = minimumValue;
			stepper.MaximumValue = maximumValue;
			stepper.StepValue = stepValue;
			stepper.Value = value;

			//Save current value
			accessoryValue = value;

			//Respond to the value being changed
			stepper.ValueChanged += (sender, e) => {
				//Save new value
				accessoryValue = ((UIStepper)sender).Value;

				//Update display
				if (cell !=null) {
					cell.TextLabel.Text = String.Format(text,accessoryValue);
				}
			};

			//Attach to this item
			accessoryView = stepper;

			//Return stepper
			return stepper;
		}

		/// <summary>
		/// Adds a stepper accessory to this <see cref="ActionComponents.ACTableItem"/> 
		/// </summary>
		/// <returns>The accessory stepper.</returns>
		/// <param name="minimumValue">Minimum value.</param>
		/// <param name="maximumValue">Maximum value.</param>
		/// <param name="stepValue">Step value.</param>
		/// <param name="value">Value.</param>
		/// <param name="stepperDelegate">This delegate will be called when the stepper value changes.</param>
		public UIStepper AddAccessoryStepper(double minimumValue, double maximumValue, double stepValue, double value, ACTableAccessoryStepperDelegate stepperDelegate){
			UIStepper stepper = new UIStepper ();

			//Configure
			stepper.MinimumValue = minimumValue;
			stepper.MaximumValue = maximumValue;
			stepper.StepValue = stepValue;
			stepper.Value = value;

			//Save current value
			accessoryValue = value;
			_accessoryStepperDelegate = stepperDelegate;

			//Respond to the value being changed
			stepper.ValueChanged += (sender, e) => {
				//Save new value
				accessoryValue = ((UIStepper)sender).Value;

				//Update display
				if (cell !=null) {
					cell.TextLabel.Text = String.Format(text,accessoryValue);
				}

				// Is there a delegate attached?
				if (_accessoryStepperDelegate!=null) {
					_accessoryStepperDelegate(((UIStepper)sender).Value);
				}
			};

			//Attach to this item
			accessoryView = stepper;

			//Return stepper
			return stepper;
		}

		/// <summary>
		/// Adds a slider accessory to this <see cref="ActionComponents.ACTableItem"/> 
		/// </summary>
		/// <returns>The accessory slider.</returns>
		/// <param name="minimumValue">Minimum value.</param>
		/// <param name="maximumValue">Maximum value.</param>
		/// <param name="value">Value.</param>
		public UISlider AddAccessorySlider(float minimumValue, float maximumValue, float value){
			UISlider slider = new UISlider ();

			//Configure
			slider.MinValue = minimumValue;
			slider.MaxValue = maximumValue;
			slider.Value = value;

			//Save current value
			accessoryValue = value;

			//Respond to the value being changed
			slider.ValueChanged += (sender, e) => {
				//Save new value
				accessoryValue = ((UISlider)sender).Value;

				//Update display
				if (cell !=null) {
					cell.TextLabel.Text = String.Format(text,accessoryValue);
				}
			};

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
		/// <param name="sliderDelegate">This delegate is called when the slider's value changes.</param>
		public UISlider AddAccessorySlider(float minimumValue, float maximumValue, float value, ACTableAccessorySliderDelegate sliderDelegate){
			UISlider slider = new UISlider ();

			//Configure
			slider.MinValue = minimumValue;
			slider.MaxValue = maximumValue;
			slider.Value = value;

			//Save current value
			accessoryValue = value;
			_accessorySliderDelegate = sliderDelegate;

			//Respond to the value being changed
			slider.ValueChanged += (sender, e) => {
				//Save new value
				accessoryValue = ((UISlider)sender).Value;

				//Update display
				if (cell !=null) {
					cell.TextLabel.Text = String.Format(text,accessoryValue);
				}

				// Has a delegate been attached?
				if (_accessorySliderDelegate!=null) {
					// Yes, call it
					_accessorySliderDelegate(((UISlider)sender).Value);
				}
			};

			//Attach to item
			accessoryView = slider;

			//Return slider
			return slider;
		}

		/// <summary>
		/// Adds an image accesspry to this <see cref="ActionComponents.ACTableItem"/> 
		/// </summary>
		/// <returns>The accessory action image view.</returns>
		/// <param name="filename">Filename.</param>
		public ACImageView AddAccessoryActionImageView(string filename){
			return AddAccessoryActionImageView (ACImage.FromFile(filename));
		}

		/// <summary>
		/// Adds an image accessory to this <see cref="ActionComponents.ACTableItem"/> 
		/// </summary>
		/// <returns>The accessory action image view.</returns>
		/// <param name="image">Image.</param>
		public ACImageView AddAccessoryActionImageView(UIImage image){
			ACImageView imageView = new ACImageView (image);

			//Attach to item
			accessoryView = imageView;

			//Return image view
			return imageView; 
		}

		/// <summary>
		/// Adds a button accessory to this <see cref="ActionComponents.ACTableItem"/> 
		/// </summary>
		/// <returns>The accessory button.</returns>
		/// <param name="type">Type.</param>
		/// <param name="width">the button's width</param>
		/// <param name="title">Title.</param>
		public UIButton AddAccessoryButton(UIButtonType type, float width, string title){
			UIButton button = new UIButton (type);

			//Configure button
			button.Frame = new CGRect (0, 0, width, 32);
			button.SetTitle (title, UIControlState.Normal);

			//attach to item
			accessoryView = button;

			//Return button
			return button;
		}

		/// <summary>
		/// Adds a button accessory to this <see cref="ActionComponents.ACTableItem"/> 
		/// </summary>
		/// <returns>The accessory button.</returns>
		/// <param name="type">Type.</param>
		/// <param name="width">Width.</param>
		/// <param name="title">Title.</param>
		/// <param name="buttonDelegate">The delegate that is called when the button is pressed.</param>
		public UIButton AddAccessoryButton(UIButtonType type, float width, string title, ACTableAccessoryButtonDelegate buttonDelegate){
			UIButton button = new UIButton (type);

			//Configure button
			button.Frame = new CGRect (0, 0, width, 32);
			button.SetTitle (title, UIControlState.Normal);

			//attach to item
			accessoryView = button;
			_accessoryButtonDelegate = buttonDelegate;

			// Attach an event
			button.TouchUpInside += (sender, e) => {
				// Is there a delegate attached?
				if (_accessoryButtonDelegate!=null) {
					// Yes, call it
					_accessoryButtonDelegate();
				}
			};

			//Return button
			return button;
		}

		/// <summary>
		/// Add a text field accessory to this <see cref="ActionComponents.ACTableItem"/> 
		/// </summary>
		/// <returns>The accessory text field.</returns>
		/// <param name="width">Width.</param>
		/// <param name="placeholder">Placeholder.</param>
		/// <param name="text">Text.</param>
		public UITextField AddAccessoryTextField(float width, string placeholder, string text){
			return AddAccessoryTextField (UITextBorderStyle.RoundedRect, UIColor.Black, UIColor.White, UIFont.SystemFontOfSize (17f), UITextAutocorrectionType.Default, 
			                              UIKeyboardType.Default, UIReturnKeyType.Done, UITextFieldViewMode.WhileEditing, width, placeholder, text, null);
		}

		/// <summary>
		/// Add a text field accessory to this <see cref="ActionComponents.ACTableItem"/> 
		/// </summary>
		/// <returns>The accessory text field.</returns>
		/// <param name="width">Width.</param>
		/// <param name="placeholder">Placeholder.</param>
		/// <param name="text">Text.</param>
		/// <param name="textDelegate">The delegate that is called when the text value changes.</param>
		public UITextField AddAccessoryTextField(float width, string placeholder, string text, ACTableAccessoryTextDelegate textDelegate){
			return AddAccessoryTextField (UITextBorderStyle.RoundedRect, UIColor.Black, UIColor.White, UIFont.SystemFontOfSize (17f), UITextAutocorrectionType.Default, 
				UIKeyboardType.Default, UIReturnKeyType.Done, UITextFieldViewMode.WhileEditing, width, placeholder, text, textDelegate);
		}

		/// <summary>
		/// Add a text field accessory to this <see cref="ActionComponents.ACTableItem"/> 
		/// </summary>
		/// <returns>The accessory text field.</returns>
		/// <param name="keyboardType">Keyboard type.</param>
		/// <param name="width">Width.</param>
		/// <param name="placeholder">Placeholder.</param>
		/// <param name="text">Text.</param>
		public UITextField AddAccessoryTextField(UIKeyboardType keyboardType, float width, string placeholder, string text){
			return AddAccessoryTextField (UITextBorderStyle.RoundedRect, UIColor.Black, UIColor.White, UIFont.SystemFontOfSize (17f), UITextAutocorrectionType.Default, 
			                              keyboardType, UIReturnKeyType.Done, UITextFieldViewMode.WhileEditing, width, placeholder, text, null);
		}

		/// <summary>
		/// Add a text field accessory to this <see cref="ActionComponents.ACTableItem"/> 
		/// </summary>
		/// <returns>The accessory text field.</returns>
		/// <param name="keyboardType">Keyboard type.</param>
		/// <param name="width">Width.</param>
		/// <param name="placeholder">Placeholder.</param>
		/// <param name="text">Text.</param>
		/// <param name="textDelegate">The delegate that is called when the text value changes.</param>
		public UITextField AddAccessoryTextField(UIKeyboardType keyboardType, float width, string placeholder, string text, ACTableAccessoryTextDelegate textDelegate){
			return AddAccessoryTextField (UITextBorderStyle.RoundedRect, UIColor.Black, UIColor.White, UIFont.SystemFontOfSize (17f), UITextAutocorrectionType.Default, 
				keyboardType, UIReturnKeyType.Done, UITextFieldViewMode.WhileEditing, width, placeholder, text, textDelegate);
		}

		/// <summary>
		/// Add a text field accessory to this <see cref="ActionComponents.ACTableItem"/> 
		/// </summary>
		/// <returns>The accessory text field.</returns>
		/// <param name="font">Font.</param>
		/// <param name="width">Width.</param>
		/// <param name="placeholder">Placeholder.</param>
		/// <param name="text">Text.</param>
		public UITextField AddAccessoryTextField(UIFont font, float width, string placeholder, string text){
			return AddAccessoryTextField (UITextBorderStyle.RoundedRect, UIColor.Black, UIColor.White, font, UITextAutocorrectionType.Default, 
			                              UIKeyboardType.Default, UIReturnKeyType.Done, UITextFieldViewMode.WhileEditing, width, placeholder, text, null);
		}

		/// <summary>
		/// Add a text field accessory to this <see cref="ActionComponents.ACTableItem"/> 
		/// </summary>
		/// <returns>The accessory text field.</returns>
		/// <param name="font">Font.</param>
		/// <param name="width">Width.</param>
		/// <param name="placeholder">Placeholder.</param>
		/// <param name="text">Text.</param>
		/// <param name="textDelegate">The delegate that is called when the text value changes.</param>
		public UITextField AddAccessoryTextField(UIFont font, float width, string placeholder, string text, ACTableAccessoryTextDelegate textDelegate){
			return AddAccessoryTextField (UITextBorderStyle.RoundedRect, UIColor.Black, UIColor.White, font, UITextAutocorrectionType.Default, 
				UIKeyboardType.Default, UIReturnKeyType.Done, UITextFieldViewMode.WhileEditing, width, placeholder, text, textDelegate);
		}

		/// <summary>
		/// Add a text field accessory to this <see cref="ActionComponents.ACTableItem"/> 
		/// </summary>
		/// <returns>The accessory text field.</returns>
		/// <param name="keyboardType">Keyboard type.</param>
		/// <param name="font">Font.</param>
		/// <param name="width">Width.</param>
		/// <param name="placeholder">Placeholder.</param>
		/// <param name="text">Text.</param>
		public UITextField AddAccessoryTextField(UIKeyboardType keyboardType, UIFont font, float width, string placeholder, string text){
			return AddAccessoryTextField (UITextBorderStyle.RoundedRect, UIColor.Black, UIColor.White, font, UITextAutocorrectionType.Default, 
			                              keyboardType, UIReturnKeyType.Done, UITextFieldViewMode.WhileEditing, width, placeholder, text, null);
		}

		/// <summary>
		/// Add a text field accessory to this <see cref="ActionComponents.ACTableItem"/> 
		/// </summary>
		/// <returns>The accessory text field.</returns>
		/// <param name="keyboardType">Keyboard type.</param>
		/// <param name="font">Font.</param>
		/// <param name="width">Width.</param>
		/// <param name="placeholder">Placeholder.</param>
		/// <param name="text">Text.</param>
		/// <param name="textDelegate">The delegate that is called when the text value changes.</param>
		public UITextField AddAccessoryTextField(UIKeyboardType keyboardType, UIFont font, float width, string placeholder, string text, ACTableAccessoryTextDelegate textDelegate){
			return AddAccessoryTextField (UITextBorderStyle.RoundedRect, UIColor.Black, UIColor.White, font, UITextAutocorrectionType.Default, 
				keyboardType, UIReturnKeyType.Done, UITextFieldViewMode.WhileEditing, width, placeholder, text, textDelegate);
		}

		/// <summary>
		/// Add a text field accessory to this <see cref="ActionComponents.ACTableItem"/> 
		/// </summary>
		/// <returns>The accessory text field.</returns>
		/// <param name="textColor">Text color.</param>
		/// <param name="backgroundColor">Background color.</param>
		/// <param name="font">Font.</param>
		/// <param name="width">Width.</param>
		/// <param name="placeholder">Placeholder.</param>
		/// <param name="text">Text.</param>
		public UITextField AddAccessoryTextField(UIColor textColor, UIColor backgroundColor, UIFont font, float width, string placeholder, string text){
			return AddAccessoryTextField (UITextBorderStyle.RoundedRect, textColor, backgroundColor, font, UITextAutocorrectionType.Default, 
				UIKeyboardType.Default, UIReturnKeyType.Done, UITextFieldViewMode.WhileEditing, width, placeholder, text, null);
		}

		/// <summary>
		/// Add a text field accessory to this <see cref="ActionComponents.ACTableItem"/> 
		/// </summary>
		/// <returns>The accessory text field.</returns>
		/// <param name="textColor">Text color.</param>
		/// <param name="backgroundColor">Background color.</param>
		/// <param name="font">Font.</param>
		/// <param name="width">Width.</param>
		/// <param name="placeholder">Placeholder.</param>
		/// <param name="text">Text.</param>
		/// <param name="textDelegate">The delegate that is called when the text value changes.</param>
		public UITextField AddAccessoryTextField(UIColor textColor, UIColor backgroundColor, UIFont font, float width, string placeholder, string text, ACTableAccessoryTextDelegate textDelegate){
			return AddAccessoryTextField (UITextBorderStyle.RoundedRect, textColor, backgroundColor, font, UITextAutocorrectionType.Default, 
				UIKeyboardType.Default, UIReturnKeyType.Done, UITextFieldViewMode.WhileEditing, width, placeholder, text, textDelegate);
		}

		/// <summary>
		/// Add a text field accessory to this <see cref="ActionComponents.ACTableItem"/> 
		/// </summary>
		/// <returns>The accessory text field.</returns>
		/// <param name="keyboardType">Keyboard type.</param>
		/// <param name="textColor">Text color.</param>
		/// <param name="backgroundColor">Background color.</param>
		/// <param name="font">Font.</param>
		/// <param name="width">Width.</param>
		/// <param name="placeholder">Placeholder.</param>
		/// <param name="text">Text.</param>
		public UITextField AddAccessoryTextField(UIKeyboardType keyboardType, UIColor textColor, UIColor backgroundColor, UIFont font, float width, string placeholder, string text){
			return AddAccessoryTextField (UITextBorderStyle.RoundedRect, textColor, backgroundColor, font, UITextAutocorrectionType.Default, 
			                              keyboardType, UIReturnKeyType.Done, UITextFieldViewMode.WhileEditing, width, placeholder, text, null);
		}

		/// <summary>
		/// Add a text field accessory to this <see cref="ActionComponents.ACTableItem"/>
		/// </summary>
		/// <returns>The accessory text field.</returns>
		/// <param name="keyboardType">Keyboard type.</param>
		/// <param name="textColor">Text color.</param>
		/// <param name="backgroundColor">Background color.</param>
		/// <param name="font">Font.</param>
		/// <param name="width">Width.</param>
		/// <param name="placeholder">Placeholder.</param>
		/// <param name="text">Text.</param>
		/// <param name="textDelegate">The delegate that is called when the text value changes.</param>
		public UITextField AddAccessoryTextField(UIKeyboardType keyboardType, UIColor textColor, UIColor backgroundColor, UIFont font, float width, string placeholder, string text, ACTableAccessoryTextDelegate textDelegate){
			return AddAccessoryTextField (UITextBorderStyle.RoundedRect, textColor, backgroundColor, font, UITextAutocorrectionType.Default, 
				keyboardType, UIReturnKeyType.Done, UITextFieldViewMode.WhileEditing, width, placeholder, text, textDelegate);
		}

		/// <summary>
		/// Add a text field accessory to this <see cref="ActionComponents.ACTableItem"/> 
		/// </summary>
		/// <returns>The accessory text field.</returns>
		/// <param name="borderStyle">Border style.</param>
		/// <param name="textColor">Text color.</param>
		/// <param name="backgroundColor">Background color.</param>
		/// <param name="font">Font.</param>
		/// <param name="autoCorrectionType">Auto correction type.</param>
		/// <param name="keyboardType">Keyboard type.</param>
		/// <param name="returnKeyType">Return key type.</param>
		/// <param name="clearButtonMode">Clear button mode.</param>
		/// <param name="width">Width.</param>
		/// <param name="placeholder">Placeholder.</param>
		/// <param name="text">Text.</param>
		/// <param name="textDelegate">The delegate that is called when the text value changes.</param>
		public UITextField AddAccessoryTextField (UITextBorderStyle borderStyle, UIColor textColor, UIColor backgroundColor, UIFont font, UITextAutocorrectionType autoCorrectionType, UIKeyboardType keyboardType, UIReturnKeyType returnKeyType, UITextFieldViewMode clearButtonMode, float width, string placeholder, string text, ACTableAccessoryTextDelegate textDelegate){
			UITextField textField = new UITextField (new CGRect (0f, 0f, width, 32f)){
				BorderStyle = borderStyle,
				TextColor = textColor,
				Font = font,
				Placeholder = placeholder,
				Text = text,
				BackgroundColor = backgroundColor,
				AutocorrectionType = autoCorrectionType,
				KeyboardType = keyboardType,
				ReturnKeyType = returnKeyType,
				ClearButtonMode = clearButtonMode
			};

			//Save value
			accessoryValue = text;
			_accessoryTextDelegate = textDelegate;

			//Wire-up events
			textField.ValueChanged += (sender, e) => {
				//Save value
				accessoryValue = ((UITextField)sender).Text;
			};

			textField.ShouldReturn = delegate (UITextField field){
				field.ResignFirstResponder ();
				accessoryValue=field.Text;

				// Is a delegate attached?
				if (_accessoryTextDelegate!=null) {
					// Yes, call it
					_accessoryTextDelegate(field.Text);
				}

				return true;
			};

			textField.ShouldEndEditing = delegate (UITextField field){
				accessoryValue=field.Text;

				// Is a delegate attached?
				if (_accessoryTextDelegate!=null) {
					// Yes, call it
					_accessoryTextDelegate(field.Text);
				}

				return true;
			};

			textField.Ended += (sender, e) => {
				((UITextField)sender).ResignFirstResponder();
			};

			//Attach to item
			accessoryView = textField;

			//Return field
			return textField;
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
		public delegate void ACTableAccessoryStepperDelegate (double value);

		/// <summary>
		/// Occurrs when this <see cref="ActionComponents.ACTableItem"/> has an attached <c>UISlider</c> accessory
		/// and the value is changed.
		/// </summary>
		public delegate void ACTableAccessorySliderDelegate (float value);

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
		public delegate UIImage ACTableItemRequestCustomImageDelegate (ACTableItem item);
		public event ACTableItemRequestCustomImageDelegate RequestCustomImage;

		/// <summary>
		/// Raises the request custom image event
		/// </summary>
		/// <returns>The request custom image.</returns>
		internal UIImage RaiseRequestCustomImage() {
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

		/// <summary>
		/// Occurs when the <see cref="ActionComponents.ACTableItem"/> needs to see if it should omit the copy or
		/// paste command for this row in the TableView
		/// </summary>
		public delegate bool CanPerformActionDelegate (ACTableItem item);
		public event CanPerformActionDelegate CanPerformAction;

		/// <summary>
		/// Raises the can perform action.
		/// </summary>
		/// <returns><c>true</c>, if can perform action was raised, <c>false</c> otherwise.</returns>
		/// <remarks>If the event isn't handled by the caller, the default is <c>true</c> </remarks>
		internal bool RaiseCanPerformAction ()
		{
			//If an event was attached, fire it off now
			if (this.CanPerformAction != null) {
				return this.CanPerformAction (this);
			} else {
				//Defaults to true if the event has not been specified
				return true;
			}
		}

		/// <summary>
		/// Occurs when editing has ended for this <see cref="ActionComponents.ACTableItem"/> 
		/// </summary>
		public delegate void DidEndEditingDelegate (ACTableItem item);
		public event DidEndEditingDelegate DidEndEditing;

		/// <summary>
		/// Raises the did end editing event
		/// </summary>
		internal void RaiseDidEndEditing ()
		{
			//If an event was attached, fire it off now
			if (this.DidEndEditing != null)
				this.DidEndEditing (this);
		}

		/// <summary>
		/// Occurs when the table needs to know the editing style for this <see cref="ActionComponents.ACTableItem"/> 
		/// </summary>
		public delegate UITableViewCellEditingStyle EditingStyleForItemDelegate (ACTableItem item);
		public event EditingStyleForItemDelegate EditingStyleForItem;

		/// <summary>
		/// Raises the editing style for item.
		/// </summary>
		/// <returns>The editing style for item.</returns>
		/// <remarks>If the event isn't handled by the caller, the default is <c>None</c> </remarks>
		internal UITableViewCellEditingStyle RaiseEditingStyleForItem ()
		{
			//If an event was attached, fire it off now
			if (this.EditingStyleForItem != null) {
				return this.EditingStyleForItem (this);
			} else {
				//Defaults to none
				return UITableViewCellEditingStyle.None;
			}
		}

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

		/// <summary>
		/// Raises the should highlight item event
		/// </summary>
		/// <returns><c>true</c>, if should highlight item was raised, <c>false</c> otherwise.</returns>
		/// <remarks>If the event isn't handled by the caller, the default is <c>true</c> </remarks>
		internal bool RaiseShouldHighlightItem ()
		{
			//If an event was attached, fire it off now
			if (this.ShouldHighlightItem != null) {
				return this.ShouldHighlightItem (this);
			} else {
				return true;
			}
		}

		/// <summary>
		/// Occurs when this <see cref="ActionComponents.ACTableItem"/> needs to be show the edit menu
		/// </summary>
		public delegate bool ShouldShowMenuDelegate (ACTableItem item);
		public event ShouldShowMenuDelegate ShouldShowMenu;

		/// <summary>
		/// Raises the should show edit menu event
		/// </summary>
		/// <returns><c>true</c>, if should highlight item was raised, <c>false</c> otherwise.</returns>
		/// <remarks>If the event isn't handled by the caller, the default is <c>true</c> </remarks>
		internal bool RaiseShouldShowMenu ()
		{
			//If an event was attached, fire it off now
			if (this.ShouldShowMenu != null) {
				return this.ShouldShowMenu (this);
			} else {
				return true;
			}
		}
		#endregion 
	}
}

