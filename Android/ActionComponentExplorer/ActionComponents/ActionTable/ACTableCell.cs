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
using Android.Views.InputMethods;


namespace ActionComponents
{
	/// <summary>
	/// The <see cref="ActionComponents.ACTableCell"/> is used by the <see cref="ActionComponents.ACTableViewController"/>
	/// to display <see cref="ActionComponents.ACTableSection"/>s and <see cref="ActionComponents.ACTableItem"/>s from the
	/// controller's <see cref="ActionComponents.ACTableViewDatasource"/> 
	/// </summary>
	/// <remarks>Use the <see cref="ActionComponents.ACTableViewController"/>'s <see cref="ActionComponents.ACTableViewAppearance"/>
	/// to control the look of the <see cref="ActionComponents.ACTableCell"/>. Available in ActionPack Business or Enterprise only. </remarks>
	public class ACTableCell : RelativeLayout
	{
		#region Private Variables
		private Button _accessoryButton;
		private ACLabel _title;
		private ACLabel _subtitle;
		private ACImageView _image;
		private CheckBox _checkbox;
		private ACLabel _disclosure;
		private ACTableViewController _controller;
		private ACTableSection _section;
		private ACTableItem _item;
		private ACTableCellSource _source;
		#endregion

		#region Computed Properties
		/// <summary>
		/// Gets or sets the [OPTIONAL] tag that can be assosciated with this <see cref="ActionComponents.ACTableCell"/> 
		/// </summary>
		/// <value>The tag.</value>
		public Object tag { get; set; }

		/// <summary>
		/// Gets the <see cref="ActionComponents.ACTableViewController"/> this <see cref="ActionComponents.ACTableCell"/> 
		/// is displayed in
		/// </summary>
		/// <value>The controller.</value>
		public ACTableViewController controller {
			get{ return _controller;}
		}

		/// <summary>
		/// Gets the source of this <see cref="ActionComponents.ACTableCell"/> as a
		/// <see cref="ActionComponents.ACTableSection"/> <c>Header</c> or <c>Footer</c> property or
		/// a <see cref="ActionComponents.ACTableItem"/>
		/// </summary>
		/// <value>The source.</value>
		public ACTableCellSource source {
			get { return _source;}
		}

		/// <summary>
		/// Gets the <see cref="ActionComponents.ACTableItem"/> used to populate this <see cref="ActionComponents.ACTableCell"/>
		/// </summary>
		/// <value>The item.</value>
		public ACTableItem item {
			get{ return _item;}
		}

		/// <summary>
		/// Gets the <see cref="ActionComponents.ACTableSection"/> used to populate this <see cref="ActionComponents.ACTableCell"/> 
		/// </summary>
		/// <value>The section.</value>
		public ACTableSection section {
			get{ return _section;}
		}

		/// <summary>
		/// Gets the accessory button.
		/// </summary>
		/// <value>The accessory button.</value>
		public Button accessoryButton{
			get{ return _accessoryButton;}
		}

		/// <summary>
		/// Gets the title.
		/// </summary>
		/// <value>The title.</value>
		public ACLabel title{
			get{ return _title;}
		}

		/// <summary>
		/// Gets the subtitle.
		/// </summary>
		/// <value>The subtitle.</value>
		public ACLabel subtitle{
			get{ return _subtitle;}
		}

		/// <summary>
		/// Gets the image.
		/// </summary>
		/// <value>The image.</value>
		public ACImageView image{
			get{ return _image;}
		}

		/// <summary>
		/// Gets the checkmark.
		/// </summary>
		/// <value>The checkmark.</value>
		public CheckBox checkmark{
			get{ return _checkbox;}
		}

		/// <summary>
		/// Gets the disclosure indicator
		/// </summary>
		/// <value>The disclosure.</value>
		public ACLabel disclosure{
			get { return _disclosure;}
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
		/// Initializes a new instance of the <see cref="Appracatappra.ActionComponents.ActionView.ACTableCell"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		public ACTableCell (Context context) : base(context)
		{
			Initialize (UITableViewCellStyle.Default);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACTableCell"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="style">Style.</param>
		public ACTableCell (Context context, UITableViewCellStyle style) : base(context)
		{
			Initialize (style);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Appracatappra.ActionComponents.ActionView.ACTableCell"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attr">Attr.</param>
		public ACTableCell (Context context, IAttributeSet attr) : base(context,attr)
		{
			Initialize (UITableViewCellStyle.Default);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Appracatappra.ActionComponents.ActionView.ACTableCell"/> class.
		/// </summary>
		/// <param name="javaReference">Java reference.</param>
		/// <param name="transfer">Transfer.</param>
		public ACTableCell (IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference,transfer)
		{
			Initialize (UITableViewCellStyle.Default);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Appracatappra.ActionComponents.ActionView.ACTableCell"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attr">Attr.</param>
		/// <param name="defStyle">Def style.</param>
		public ACTableCell (Context context, IAttributeSet attr, int defStyle) : base(context,attr,defStyle)
		{
			Initialize (UITableViewCellStyle.Default);
		}

		/// <summary>
		/// Initialize this instance.
		/// </summary>
		private void Initialize(UITableViewCellStyle style){
			int cellHeight = 80;
			int titleSize = 11;
			int titleHeight = 40;
			int titleOffset = 10;
			int subtitleHeight = 30;
			int subtitleOffset = 0;

			//Adjust cell height base on the given style
			switch (style) {
			case UITableViewCellStyle.SimpleListItem1:
				cellHeight = 80;
				titleSize = 11;
				titleHeight = 40;
				titleOffset = 20;
				subtitleHeight = 30;
				subtitleOffset = 0;
				break;
			case UITableViewCellStyle.ActionTableCell:
			case UITableViewCellStyle.SimpleListItem2:
				cellHeight = 80;
				titleSize = 11;
				titleHeight = 40;
				titleOffset = 10;
				subtitleHeight = 30;
				subtitleOffset = 0;
				break;
			case UITableViewCellStyle.TwoLineListItem:
				cellHeight = 60;
				titleSize = 7;
				titleHeight = 30;
				titleOffset = 5;
				subtitleHeight = 25;
				subtitleOffset = 5;
				break;
			case UITableViewCellStyle.ActivityListItem:
				cellHeight = 40;
				titleSize = 7;
				titleHeight = 30;
				titleOffset = 5;
				subtitleHeight = 10;
				subtitleOffset = 10;
				break;
			}

			//Set default layout
			this.LayoutParameters = new LinearLayout.LayoutParams (LinearLayout.LayoutParams.FillParent,cellHeight);

			//Define image position
			var irl = new RelativeLayout.LayoutParams (cellHeight-10,cellHeight-10);
			irl.TopMargin = 5;
			irl.LeftMargin = 5;
			irl.AddRule (LayoutRules.AlignParentLeft);

			//Build Image
			_image = new ACImageView (this.Context);
			_image.LayoutParameters =irl;
			this.AddView (_image);

			//Define title position
			var trl = new RelativeLayout.LayoutParams (RelativeLayout.LayoutParams.FillParent, titleHeight);
			trl.TopMargin = titleOffset;
			trl.LeftMargin = 10;
			trl.AddRule (LayoutRules.AlignParentTop);

			//Build title
			_title = new ACLabel (this.Context);
			_title.Text = "";
			_title.SetTextColor (Color.White);
			_title.SetTextSize (ComplexUnitType.Px, titleHeight-10); //ComplexUnitType.Pt, titleSize
			_title.LayoutParameters = trl;
			this.AddView (_title);

			//Define title position
			var strl = new RelativeLayout.LayoutParams (RelativeLayout.LayoutParams.FillParent, subtitleHeight);
			strl.BottomMargin = subtitleOffset;
			strl.LeftMargin = 10;
			strl.AddRule (LayoutRules.AlignParentBottom);

			//Build subtitle
			_subtitle = new ACLabel (this.Context);
			_subtitle.Text = "";
			_subtitle.SetTextColor (Color.DarkGray);
			_subtitle.SetTextSize (ComplexUnitType.Px, subtitleHeight-10); //Pt, 7
			_subtitle.LayoutParameters = strl;
			this.AddView (_subtitle);

			//Define button position
			var brl = new RelativeLayout.LayoutParams (cellHeight-20, cellHeight-20);
			brl.TopMargin = 10;
			brl.RightMargin = 5;
			brl.AddRule (LayoutRules.AlignParentRight);

			//Build accessory button
			_accessoryButton = new Button (this.Context);
			_accessoryButton.Text = ">";
			_accessoryButton.LayoutParameters = brl;
			this.AddView (_accessoryButton);

			//Build checkbox
			_checkbox = new CheckBox (this.Context);
			_checkbox.Checked = true;
			_checkbox.Enabled = false;
			_checkbox.LayoutParameters = brl;
			this.AddView (_checkbox);

			//Build subtitle
			_disclosure = new ACLabel (this.Context);
			_disclosure.Text = ">";
			_disclosure.SetTextColor (Color.DarkGray);
			_disclosure.SetTextSize (ComplexUnitType.Pt, 14);
			_disclosure.LayoutParameters = brl;
			this.AddView (_disclosure);

		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Attaches the given <see cref="Appracatappra.ActionComponents.ActionView.ACTableSection"/> to this <see cref="Appracatappra.ActionComponents.ActionView.ACTableCell"/>
		/// and displays its <c>Header</c> property
		/// </summary>
		/// <param name="section">Section.</param>
		/// <param name="controller">Controller.</param>
		public void AttachSectionHeader(ACTableSection section, ACTableViewController controller){

			//Assign parent item
			_controller = controller;
			_source = ACTableCellSource.Header;
			_item = null;
			_section = section;

			//Assign unique ID's to all of the child view based on the parent's view
			_image.Id = Id + 1;
			_title.Id = Id + 2;
			_subtitle.Id = Id + 3;
			_accessoryButton.Id = Id + 4;
			_disclosure.Id = Id + 5;

			//Set title
			title.Text = section.header;

			//Display image?
			if (section.imageSource == ACTableItemImageSource.None) {
				//No, hide image
				_image.Visibility = ViewStates.Gone;
				_title.LeftMargin = 10;
				_subtitle.LeftMargin = 10;
			} else {
				//Yes, show image
				_image.Visibility = ViewStates.Visible;
				_title.LeftMargin = ACView.GetViewWidth(_image)+20;
				_subtitle.LeftMargin = ACView.GetViewWidth(_image)+20;

				//Set image source
				_image.SetImageBitmap (section.image);
			}

			//Adjust GUI
			_subtitle.Visibility = ViewStates.Gone;
			_accessoryButton.Visibility = ViewStates.Gone;
			_checkbox.Visibility = ViewStates.Gone;
			_disclosure.Visibility = ViewStates.Gone;

			//Style cell
			SetBackgroundColor (controller.appearance.headerBackground);
			title.SetTextColor (controller.appearance.headerTextColor);
		}

		/// <summary>
		/// Attaches the given <see cref="Appracatappra.ActionComponents.ActionView.ACTableSection"/> to this <see cref="Appracatappra.ActionComponents.ActionView.ACTableCell"/>
		/// and displays its <c>Footer</c> property
		/// </summary>
		/// <param name="section">Section.</param>
		/// <param name="controller">Controller.</param>
		public void AttachSectionFooter(ACTableSection section, ACTableViewController controller){

			//Assign parent item
			_controller = controller;
			_source = ACTableCellSource.Footer;
			_item = null;
			_section = section;

			//Assign unique ID's to all of the child view based on the parent's view
			_image.Id = Id + 1;
			_title.Id = Id + 2;
			_subtitle.Id = Id + 3;
			_accessoryButton.Id = Id + 4;
			_disclosure.Id = Id + 5;

			//Set title
			title.Text = section.footer;

			//Hide image
			_image.Visibility = ViewStates.Gone;
			_title.LeftMargin = 10;
			_subtitle.LeftMargin = 10;

			//Adjust GUI
			_subtitle.Visibility = ViewStates.Gone;
			_accessoryButton.Visibility = ViewStates.Gone;
			_checkbox.Visibility = ViewStates.Gone;
			_disclosure.Visibility = ViewStates.Gone;

			//Style cell
			SetBackgroundColor (controller.appearance.footerBackground);
			title.SetTextColor (controller.appearance.footerTextColor);
		}

		/// <summary>
		/// Attaches the given <see cref="Appracatappra.ActionComponents.ActionView.ACTableItem"/> to this <see cref="Appracatappra.ActionComponents.ActionView.ACTableCell"/>
		/// and displays it's contents
		/// </summary>
		/// <param name="item">Item.</param>
		public void AttachItem(ACTableItem parentItem, ACTableViewController controller){

			//Attach to the parent item
			//Assign parent item
			_controller = controller;
			_source = ACTableCellSource.Item;
			_item = parentItem;
			_section = null;

			//Assign unique ID's to all of the child view based on the parent's view
			_image.Id = Id + 1;
			_title.Id = Id + 2;
			_subtitle.Id = Id + 3;
			_accessoryButton.Id = Id + 4;
			_disclosure.Id = Id + 5;

			//Set title
			title.Text = item.text;

			//Set subtitle
			subtitle.Text = item.details;

			//Display image?
			if (item.imageSource == ACTableItemImageSource.None) {
				//No, hide image
				_image.Visibility = ViewStates.Gone;
				_title.LeftMargin = 10;
				_subtitle.LeftMargin = 10;
			} else {
				//Yes, show image
				_image.Visibility = ViewStates.Visible;
				_title.LeftMargin = ACView.GetViewWidth(_image)+20;
				_subtitle.LeftMargin = ACView.GetViewWidth(_image)+20;

				//Set image source
				_image.SetImageBitmap (item.image);
			}

			//Display detailed disclosure?
			if (item.accessory == UITableViewCellAccessory.DetailedDisclosureButton) {
				//Show disclosure
				_accessoryButton.Visibility = ViewStates.Visible;

				//Wire-up touched
				_accessoryButton.Click += (object sender, EventArgs e) => {
					//Inform item and controller of button press
					item.RaiseAccessoryButtonTapped();
					controller.RaiseAccessoryButtonTapped(item);
				};

				//Is there an accessory view?
				if (item.accessoryView != null) {
					//Yes, we need to move it to the left
					ACView.SetViewRightMargin (item.accessoryView,100);
				}
			} else {
				//Hide button
				_accessoryButton.Visibility = ViewStates.Gone;
			}

			//Displaying checkbox?
			if (item.accessory == UITableViewCellAccessory.Checkmark) {
				//Show checkmark
				_checkbox.Visibility = ViewStates.Visible;
			} else {
				//Hide checkmark
				_checkbox.Visibility = ViewStates.Gone;
			}

			//Display disclosure
			if (item.accessory == UITableViewCellAccessory.DisclosureIndicator) {
				//Show disclosure indicator
				_disclosure.Visibility = ViewStates.Visible;
			} else {
				//Hide disclosure indicator
				_disclosure.Visibility = ViewStates.Gone;
			}

			//Is there an accessory view?
			if (item.accessoryView != null) {
				//Configure accessory
				item.accessoryView.Id = Id + 10;
				item.accessoryView.FocusableInTouchMode = true;

				//Wire-up custom handlers
				if (item.accessoryView is SeekBar) {
					SeekBar sb = ((SeekBar)item.accessoryView);

					//Save current accessory value
					item.accessoryValue = sb.Progress + item.sliderMinValue;

					//Update title display
					title.Text = String.Format (item.text, item.accessoryValue);

					//Respond to the value being changed
					sb.ProgressChanged += (sender, e) => {
						//Update display
						title.Text = String.Format (item.text, item.accessoryValue);
					};
				} else if (item.accessoryView is EditText) {
					EditText et = (EditText)item.accessoryView;

					et.FocusChange += (object sender, FocusChangeEventArgs e) => {
						if (e.HasFocus) {
							//Console.WriteLine("EditText {0} has focus",((EditText)sender).Id);

						} else {
							//Console.WriteLine("EditText {0} released focus",((EditText)sender).Id);

						}
					};
				}

				//Attach accessory to parent view
				AddView (item.accessoryView);
			}

			//Style cell
			_title.SetTextColor (controller.appearance.titleColor);
			_subtitle.SetTextColor (controller.appearance.subtitleColor);
			SetBackgroundColor (controller.appearance.cellBackground);
		}
		#endregion 

		#region Private Methods

		#endregion 

		#region Override Methods
		/// <summary>
		/// Raises the touch event event.
		/// </summary>
		/// <param name="e">E.</param>
		public override bool OnTouchEvent (MotionEvent e)
		{
			//int x=(int)e.GetX ();
			//int y=(int)e.GetY ();

			//Take action based on the event type
			switch(e.Action){
			case MotionEventActions.Down:
				//Take action based on the cells source
				switch (source) {
				case ACTableCellSource.Header:
					//Can the user touch the header?
					if (controller.allowsTouchHeader) {
						//Does this table allow for cell highlighting?
						if (controller.allowsHighlight) {
							//Yes, change the cell background color
							SetBackgroundColor (controller.appearance.cellHighlight);
							return true;
						}
					}
					break;
				case ACTableCellSource.Footer:
					//Can the user touch the footer?
					if (controller.allowsTouchFooter) {
						//Does this table allow for cell highlighting?
						if (controller.allowsHighlight) {
							//Yes, change the cell background color
							SetBackgroundColor (controller.appearance.cellHighlight);
							return true;
						}
					}
					break;
				case ACTableCellSource.Item:
					//Does this table allow for cell highlighting?
					if (controller.allowsHighlight) {
						//Yes, change the cell background color
						SetBackgroundColor (controller.appearance.cellHighlight);
						return true;
					}
					break;
				}
				break;
			case MotionEventActions.Move:
				//Take action based on the cells source
				switch (source) {
				case ACTableCellSource.Header:
					//Can the user touch the header?
					if (controller.allowsTouchHeader) {
						//Does this table allow for cell highlighting?
						if (controller.allowsHighlight) {
							//Yes, reset the cell background color
							SetBackgroundColor (controller.appearance.headerBackground);
						}
					}
					break;
				case ACTableCellSource.Footer:
					//Can the user touch the footer?
					if (controller.allowsTouchFooter) {
						//Does this table allow for cell highlighting?
						if (controller.allowsHighlight) {
							//Yes, reset the cell background color
							SetBackgroundColor (controller.appearance.footerBackground);
						}
					}
					break;
				case ACTableCellSource.Item:
					//Do we need to adjust the appearance
					if (controller.allowsSelection) {
						if (item == controller.selectedItem) {
							SetBackgroundColor (controller.appearance.cellSelected);
						} else {
							SetBackgroundColor (controller.appearance.cellBackground);
						}
					} else if (controller.allowsHighlight) {
						//Yes, restore the cell background color
						SetBackgroundColor (controller.appearance.cellBackground);
					}
					break;
				}
				break;
			case MotionEventActions.Up:
				//Take action based on the cells source
				switch (source) {
				case ACTableCellSource.Header:
					//Can the user touch the header?
					if (controller.allowsTouchHeader) {
						//Inform header that it has been touched
						section.RaiseSectionHeaderTouched ();

						//Does this table allow for cell highlighting?
						if (controller.allowsHighlight) {
							//Yes, restore the cell background color
							SetBackgroundColor (controller.appearance.headerBackground);
						}
					}
					break;
				case ACTableCellSource.Footer:
					//Can the user touch the footer?
					if (controller.allowsTouchFooter) {
						//Inform footer that it has been touched
						section.RaiseSectionFooterTouched ();

						//Does this table allow for cell highlighting?
						if (controller.allowsHighlight) {
							//Yes, restore the cell background color
							SetBackgroundColor (controller.appearance.footerBackground);
						}
					}
					break;
				case ACTableCellSource.Item:
					//Inform item and controller that it has been touched
					item.RaiseItemSelected ();
					controller.RaiseItemSelected (item);

					//Do we need to adjust the appearance
					if (controller.allowsSelection && item.canSelect) {
						//Was another cell selected?
						if (controller.selectedItem != null) {
							//Yes, clear current selection
							controller.selectedItem.cell.SetBackgroundColor (controller.appearance.cellBackground);
						}

						//Select this cell
						controller._selectedItem = item;
						SetBackgroundColor (controller.appearance.cellSelected);
					} else if (controller.allowsHighlight) {
						//Yes, restore the cell background color
						SetBackgroundColor (controller.appearance.cellBackground);
					}
					break;
				}
				break;
			}

			// Not handled by this routine
			return base.OnTouchEvent(e);
		}
		#endregion
	}
}

