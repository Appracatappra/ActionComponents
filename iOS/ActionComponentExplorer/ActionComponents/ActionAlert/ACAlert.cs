using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

using Foundation;
using UIKit;
using CoreGraphics;

namespace ActionComponents
{
	[Register("ACAlert")]
	public class ACAlert : UIView
	{
		#region Static Methods
		/// <summary>
		/// Creates a new <see cref="ActionComponents.ACAlert"/> and displays it. If the user taps the alert,
		/// it will automatically close.
		/// </summary>
		/// <returns>The alert.</returns>
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		public static ACAlert ShowAlert(string title, string description) {
			//Build new 
			ACAlert alert = new ACAlert (ACAlertType.Default, ACAlertLocation.Middle, title, description);

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
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		/// <param name="buttonTitles">Button titles.</param>
		public static ACAlert ShowAlert(string title, string description, string buttonTitles) {
			//Build new 
			ACAlert alert = new ACAlert (ACAlertType.Default, ACAlertLocation.Middle, title, description, buttonTitles);

			//Show alert
			alert.Show ();

			//Return new alert
			return alert;
		}

		/// <summary>
		/// Creates a new <see cref="ActionComponents.ACAlert"/> with the given buttons and displays it.
		/// </summary>
		/// <returns>The alert.</returns>
		/// <param name="icon">Icon.</param>
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		/// <param name="buttonTitles">Button titles.</param>
		public static ACAlert ShowAlert(UIImage icon, string title, string description, string buttonTitles) {
			//Build new 
			ACAlert alert = new ACAlert (ACAlertType.Default, ACAlertLocation.Middle, icon, title, description, buttonTitles);

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
		/// <param name="location">Location.</param>
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		public static ACAlert ShowAlert(ACAlertLocation location, string title, string description) {
			//Build new 
			ACAlert alert = new ACAlert (ACAlertType.Default, location, title, description);

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
		/// <param name="icon">Icon.</param>
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		public static ACAlert ShowAlert(UIImage icon, string title, string description) {
			//Build new 
			ACAlert alert = new ACAlert (ACAlertType.Default, ACAlertLocation.Middle, icon, title, description);

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
		/// <param name="location">Location.</param>
		/// <param name="icon">Icon.</param>
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		public static ACAlert ShowAlert(ACAlertLocation location, UIImage icon, string title, string description) {
			//Build new 
			ACAlert alert = new ACAlert (ACAlertType.Default, location, icon, title, description);

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
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		public static ACAlert ShowAlertOK(string title, string description) {
			//Build new 
			ACAlert alert = new ACAlert (ACAlertType.Default, ACAlertLocation.Middle, title, description, new string []{"OK"});

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
		/// <param name="icon">Icon.</param>
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		public static ACAlert ShowAlertOK(UIImage icon, string title, string description) {
			//Build new 
			ACAlert alert = new ACAlert (ACAlertType.Default, ACAlertLocation.Middle, icon, title, description, new string []{"OK"});

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
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		public static ACAlert ShowAlertOKCancel(string title, string description) {
			//Build new 
			ACAlert alert = new ACAlert (ACAlertType.Default, ACAlertLocation.Middle, title, description, new string []{"Cancel","OK"});

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
		/// <param name="icon">Icon.</param>
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		public static ACAlert ShowAlertOKCancel(UIImage icon, string title, string description) {
			//Build new 
			ACAlert alert = new ACAlert (ACAlertType.Default, ACAlertLocation.Middle, icon, title, description, new string []{"Cancel","OK"});

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
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		/// <param name="modal">If set to <c>true</c> modal.</param>
		public static ACAlert ShowActivityAlert(string title, string description, bool modal) {
			//Build new 
			ACAlert alert = new ACAlert (ACAlertType.ActivityAlert, ACAlertLocation.Middle, title, description);
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
		/// <param name="location">Location.</param>
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		/// <param name="modal">If set to <c>true</c> modal.</param>
		public static ACAlert ShowActivityAlert(ACAlertLocation location, string title, string description, bool modal) {
			//Build new 
			ACAlert alert = new ACAlert (ACAlertType.ActivityAlert, location, title, description);
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
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		/// <param name="modal">If set to <c>true</c> modal.</param>
		public static ACAlert ShowActivityAlertCancel(string title, string description, bool modal) {
			//Build new 
			ACAlert alert = new ACAlert (ACAlertType.ActivityAlert, ACAlertLocation.Middle, title, description, new string []{"Cancel"});
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
		/// <param name="location">Location.</param>
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		/// <param name="modal">If set to <c>true</c> modal.</param>
		public static ACAlert ShowActivityAlertCancel(ACAlertLocation location, string title, string description, bool modal) {
			//Build new 
			ACAlert alert = new ACAlert (ACAlertType.ActivityAlert, location, title, description, new string []{"Cancel"});
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
		/// <param name="title">Title.</param>
		/// <param name="modal">If set to <c>true</c> modal.</param>
		public static ACAlert ShowActivityAlertMedium(string title, bool modal) {
			//Build new 
			ACAlert alert = new ACAlert (ACAlertType.ActivityAlertMedium, ACAlertLocation.Middle, title, "");
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
		/// <param name="title">Title.</param>
		/// <param name="modal">If set to <c>true</c> modal.</param>
		public static ACAlert ShowActivityAlertMediumCancel(string title, bool modal) {
			//Build new 
			ACAlert alert = new ACAlert (ACAlertType.ActivityAlertMedium, ACAlertLocation.Middle, title, "", new string []{"Cancel"});
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
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		/// <param name="modal">If set to <c>true</c> modal.</param>
		public static ACAlert ShowProgressAlert(string title, string description, bool modal) {
			//Build new 
			ACAlert alert = new ACAlert (ACAlertType.ProgressAlert, ACAlertLocation.Middle, title, description);
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
		/// <param name="icon">Icon.</param>
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		/// <param name="modal">If set to <c>true</c> modal.</param>
		public static ACAlert ShowProgressAlert(UIImage icon, string title, string description, bool modal) {
			//Build new 
			ACAlert alert = new ACAlert (ACAlertType.ProgressAlert, ACAlertLocation.Middle, icon, title, description);
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
		/// <param name="location">Location.</param>
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		/// <param name="modal">If set to <c>true</c> modal.</param>
		public static ACAlert ShowProgressAlert(ACAlertLocation location, string title, string description, bool modal) {
			//Build new 
			ACAlert alert = new ACAlert (ACAlertType.ProgressAlert, ACAlertLocation.Middle, title, description);
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
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		/// <param name="buttonTitles">A comma seperated list of Button titles.</param>
		/// <param name="modal">If set to <c>true</c> modal.</param>
		public static ACAlert ShowProgressAlert(string title, string description, string buttonTitles, bool modal) {
			//Build new 
			ACAlert alert = new ACAlert (ACAlertType.ProgressAlert, ACAlertLocation.Middle, title, description, buttonTitles);
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
		/// <param name="icon">Icon.</param>
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		/// <param name="buttonTitles">A comma seperated list of Button titles.</param>
		/// <param name="modal">If set to <c>true</c> modal.</param>
		public static ACAlert ShowProgressAlert(UIImage icon, string title, string description, string buttonTitles, bool modal) {
			//Build new 
			ACAlert alert = new ACAlert (ACAlertType.ProgressAlert, ACAlertLocation.Middle, icon, title, description, buttonTitles);
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
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		/// <param name="modal">If set to <c>true</c> modal.</param>
		public static ACAlert ShowProgressAlertCancel(string title, string description, bool modal) {
			//Build new 
			ACAlert alert = new ACAlert (ACAlertType.ProgressAlert, ACAlertLocation.Middle, title, description, new string []{"Cancel"});
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
		/// <param name="icon">Icon.</param>
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		/// <param name="modal">If set to <c>true</c> modal.</param>
		public static ACAlert ShowProgressAlertCancel(UIImage icon, string title, string description, bool modal) {
			//Build new 
			ACAlert alert = new ACAlert (ACAlertType.ProgressAlert, ACAlertLocation.Middle, icon, title, description, new string []{"Cancel"});
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

		#region Private Variables
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
		private CGPoint _startLocation;
		private string _title="";
		private string _description="";
		private bool _modal = true;
		private ACAlertOverlay _overlay;
		private UIImage _icon;
		private bool _hidden=true;
		private UIActivityIndicatorView _activityIndicator;
		private UIProgressView _progressView;
		private UIView _subview;
		#endregion 

		#region Computed Properties
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
		/// Gets the type of this <see cref="ActionComponents.ACAlert"/>
		/// </summary>
		/// <value>The type.</value>
		[Export("type"), Browsable(true), DisplayName("Alert Type")]
		public ACAlertType type{
			get { return _type;}
		}

		/// <summary>
		/// Gets the subview attached to this <see cref="ActionComponents.ACAlert"/>
		/// </summary>
		/// <value>The subview.</value>
		public UIView subview {
			get { return _subview; }
		}

		/// <summary>
		/// Returns the embedded <c>Activity Indicator</c> for <c>ActivityAlert</c> types of <see cref="ActionComponents.ACAlert"/>s 
		/// </summary>
		/// <value>The activity indicator.</value>
		public UIActivityIndicatorView activityIndicator {
			get { return _activityIndicator;}
		}

		/// <summary>
		/// Returns the embedded <c>Progress Indicator</c> for <c>ProgressAlert</c> types of <see cref="ActionComponents.ACAlert"/>s
		/// </summary>
		/// <value>The progress view.</value>
		public UIProgressView progressView {
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
		public UIImage icon{
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
					//Remove overlay
					_overlay.RemoveFromSuperview ();
					_overlay = null;
				}
			}
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
		/// Gets the location of this <see cref="ActionComponents.ACAlert"/> 
		/// </summary>
		/// <value>The location.</value>
		[Export("location"), Browsable(true)]
		public ACAlertLocation location{
			get { return _location;}
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
		[Export("title"), Browsable(true)]
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
		[Export("description"), Browsable(true)]
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
		[Export("draggable"), Browsable(true)]
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
		[Export("bringToFrontOnTouched"), Browsable(true), DisplayName("Bring to front on touched")]
		public bool bringToFrontOnTouched{
			get { return _bringToFrontOnTouched;}
			set { _bringToFrontOnTouched = value;}
		}

		/// <summary>
		/// Gets or sets the <see cref="Appracatappra.ActionComponents.ActionView.ACAlertDragConstraint"/> applied to the <c>x axis</c> of this
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
		/// Gets or sets the <see cref="Appracatappra.ActionComponents.ActionView.ACAlertDragConstraint"/> applied to the <c>y axis</c> of this
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
		/// Gets or sets a value indicating whether this <see cref="ActionComponents.ACAlert"/>
		/// is enabled.
		/// </summary>
		/// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
		/// <remarks>Enabling/disabling a <see cref="ActionComponents.ACAlert"/> automatically changes the value of it's
		/// <c>UserInteractionEnabled</c> flag</remarks>
		public bool Enabled{
			get { return UserInteractionEnabled;}
			set { UserInteractionEnabled = value;}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACAlert"/> class.
		/// </summary>
		/// <param name="type">Type.</param>
		/// <param name="location">Location.</param>
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		public ACAlert (ACAlertType type, ACAlertLocation location, string title, string description) : base()
		{
			//Save defaults
			this._type = type;
			this._location = location;
			this._title = title;
			this._description = description;

			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACAlert"/> class.
		/// </summary>
		/// <param name="type">Type.</param>
		/// <param name="location">Location.</param>
		/// <param name="title">Title.</param>
		/// <param name="subview">the subview attached to this</param>
		public ACAlert (ACAlertType type, ACAlertLocation location, string title, UIView subview) : base()
		{
			//Save defaults
			this._type = type;
			this._location = location;
			this._title = title;
			this._subview = subview;

			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACAlert"/> class.
		/// </summary>
		/// <param name="type">Type.</param>
		/// <param name="location">Location.</param>
		/// <param name="icon">Icon.</param>
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		public ACAlert (ACAlertType type, ACAlertLocation location, UIImage icon, string title, string description) : base()
		{
			//Save defaults
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
		/// <param name="type">Type.</param>
		/// <param name="location">Location.</param>
		/// <param name="icon">Icon.</param>
		/// <param name="title">Title.</param>
		/// <param name="subview">Subview.</param>
		public ACAlert (ACAlertType type, ACAlertLocation location, UIImage icon, string title, UIView subview) : base()
		{
			//Save defaults
			this._type = type;
			this._location = location;
			this._icon = icon;
			this._title = title;
			this._subview=subview;

			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACAlert"/> class.
		/// </summary>
		/// <param name="type">Type.</param>
		/// <param name="location">Location.</param>
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		/// <param name="buttonTitles">Button titles.</param>
		public ACAlert (ACAlertType type, ACAlertLocation location, string title, string description, string[] buttonTitles) : base()
		{
			//Save defaults
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
		/// <param name="type">Type.</param>
		/// <param name="location">Location.</param>
		/// <param name="title">Title.</param>
		/// <param name="subview">Subview.</param>
		/// <param name="buttonTitles">Button titles.</param>
		public ACAlert (ACAlertType type, ACAlertLocation location, string title, UIView subview, string[] buttonTitles) : base()
		{
			//Save defaults
			this._type = type;
			this._location = location;
			this._title = title;
			this._subview=subview;

			Initialize ();

			//Add all buttons to the alert
			AddButtons (buttonTitles);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACAlert"/> class.
		/// </summary>
		/// <param name="type">Type.</param>
		/// <param name="location">Location.</param>
		/// <param name="icon">Icon.</param>
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		/// <param name="buttonTitles">Button titles.</param>
		public ACAlert (ACAlertType type, ACAlertLocation location, UIImage icon, string title, string description, string[] buttonTitles) : base()
		{
			//Save defaults
			this._type = type;
			this._location = location;
			this._icon = icon;
			this._title = title;
			this._description = description;

			Initialize ();

			//Add all buttons to the alert
			AddButtons (buttonTitles);
		}

		public ACAlert (ACAlertType type, ACAlertLocation location, UIImage icon, string title, UIView subview, string[] buttonTitles) : base()
		{
			//Save defaults
			this._type = type;
			this._location = location;
			this._icon = icon;
			this._title = title;
			this._subview=subview;

			Initialize ();

			//Add all buttons to the alert
			AddButtons (buttonTitles);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACAlert"/> class.
		/// </summary>
		/// <param name="type">Type.</param>
		/// <param name="location">Location.</param>
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		/// <param name="buttonTitles">A comma seperated Button titles.</param>
		public ACAlert (ACAlertType type, ACAlertLocation location, string title, string description, string buttonTitles) : base()
		{
			//Save defaults
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
		/// <param name="type">Type.</param>
		/// <param name="location">Location.</param>
		/// <param name="title">Title.</param>
		/// <param name="subview">Subview.</param>
		/// <param name="buttonTitles">Button titles.</param>
		public ACAlert (ACAlertType type, ACAlertLocation location, string title, UIView subview, string buttonTitles) : base()
		{
			//Save defaults
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
		/// <param name="type">Type.</param>
		/// <param name="location">Location.</param>
		/// <param name="icon">Icon.</param>
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		/// <param name="buttonTitles">Button titles.</param>
		public ACAlert (ACAlertType type, ACAlertLocation location, UIImage icon, string title, string description, string buttonTitles) : base()
		{
			//Save defaults
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

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACAlert"/> class.
		/// </summary>
		/// <param name="type">Type.</param>
		/// <param name="location">Location.</param>
		/// <param name="icon">Icon.</param>
		/// <param name="title">Title.</param>
		/// <param name="subview">Subview.</param>
		/// <param name="buttonTitles">Button titles.</param>
		public ACAlert (ACAlertType type, ACAlertLocation location, UIImage icon, string title, UIView subview, string buttonTitles) : base()
		{
			//Save defaults
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
		private void Initialize()
		{

			//Set defaults
			this.BackgroundColor = UIColor.Clear;
			this._isDraggable = false;
			this._dragging = false;
			this._bringToFrontOnTouched = false;
			this._xConstraint = new ACAlertDragConstraint();
			this._yConstraint = new ACAlertDragConstraint();
			this._startLocation = new CGPoint(0, 0);
			this.UserInteractionEnabled = true;

			//Initially lock the y constraint if we are on an iPhone
			if (iOSDevice.isPhone)
			{
				this._xConstraint.constraintType = ACAlertDragConstraintType.Locked;
			}

			//Wireup change events
			this._xConstraint.ConstraintChanged += () =>
			{
				XConstraintModified();
			};
			this._yConstraint.ConstraintChanged += () =>
			{
				YConstraintModified();
			};

			//Set default appearance
			this._appearance = new ACAlertAppearance();
			this._appearance.AppearanceModified += () =>
			{
				AdjustAlertPosition();
				RepositionButtons();
			};

			//Create default button appearances
			this._buttonAppearance = new ACAlertButtonAppearance();
			this._buttonAppearanceDisabled = new ACAlertButtonAppearance();

			//Are we running on iOS 7 or greater?
			if (!iOSDevice.isIOS6)
			{
				this._buttonAppearanceTouched = new ACAlertButtonAppearance(UIColor.FromRGB(0, 122, 255), this._appearance.border, UIColor.White);
				this._buttonAppearanceHighlighted = new ACAlertButtonAppearance();
			}
			else
			{
				this._buttonAppearanceTouched = new ACAlertButtonAppearance(UIColor.FromRGB(240, 240, 240), this._appearance.border, UIColor.Black);
				this._buttonAppearanceHighlighted = new ACAlertButtonAppearance(UIColor.FromRGBA(0.272f, 0.272f, 0.272f, 1.000f), this._appearance.border);
			}

			//Create any suplimental controls based on the alert's type
			switch (type)
			{
				case ACAlertType.ActivityAlertMedium:
				case ACAlertType.ActivityAlert:
					//Create a new activity indicator and insert it into the control
					_activityIndicator = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.WhiteLarge);
					_activityIndicator.Frame = new CGRect(23, 19, 37, 37);
					_activityIndicator.HidesWhenStopped = true;

					//Are we running on iOS 7 or greater?
					if (!iOSDevice.isIOS6)
					{
						//Yes, set the activity indicator color
						_activityIndicator.Color = appearance.activityIndicatorColor;
					}
					this.AddSubview(_activityIndicator);
					break;
				case ACAlertType.ProgressAlert:
					//Create new progress indicator and insert it into the control
					_progressView = new UIProgressView(new CGRect(20, 20, 100, 9));
					this.AddSubview(_progressView);
					break;
			}

			//Adjust the alert location
			AdjustAlertPosition();

			// Set the AutoresizingMask to anchor the view to the top left but
			// allow height and width to grow
			this.AutoresizingMask = (UIViewAutoresizing.FlexibleMargins);

#if TRIAL
			ACToast.MakeText("ActionAlert by Appracatappra", ACToastLength.Short).Show();
#else
			AppracatappraLicenseManager.ValidateLicense();
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

			CGRect rect;
			var buttonWidth = (Frame.Width - 10f) / _buttons.Count;
			nfloat x = 5f;
			var i = 0;

			//Process all buttons
			foreach (ACAlertButton button in _buttons) {
				//Calculate button position
				rect = new CGRect (x, Frame.Height - 55f, buttonWidth, 47f);

				//Reset the buttons size and location based on position
				if (i == 0) {
					button.SetButtonPosition (rect, appearance.roundBottomLeftCorner, (_buttons.Count == 1) ? appearance.roundBottomRightCorner : false);
				} else if (i == (_buttons.Count - 1)) {
					button.SetButtonPosition (rect, false, appearance.roundBottomRightCorner);
				} else {
					button.SetButtonPosition (rect, false, false);
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
		private nfloat CalculateAlertHeight(){
			nfloat height = 0f;
			CGSize descSize;

			//Does the alert have a title?
			if (title != "") {
				height = 32f;
			}

			//Take action based on the alert's type
			switch (type) {
			case ACAlertType.ActivityAlert:
				if (description !="") {
					//Calculate the height of the description area
					#if __UNIFIED__
					descSize = UIStringDrawing.StringSize (description, UIFont.SystemFontOfSize (appearance.descriptionSize), new CGSize (215f, float.MaxValue), UILineBreakMode.WordWrap);
					#else
					descSize = StringSize (description, UIFont.SystemFontOfSize (appearance.descriptionSize), new CGSize (215f, float.MaxValue), UILineBreakMode.WordWrap);
					#endif
					height += 9f + ((descSize.Height < 22f) ? 22f : descSize.Height);
				}
				break;
			case ACAlertType.Subview:
			case ACAlertType.ProgressAlert:
			case ACAlertType.Default:
				if (description != "") {
					//Calculate the height of the description area
					#if __UNIFIED__
					descSize = UIStringDrawing.StringSize (description, UIFont.SystemFontOfSize(appearance.descriptionSize), new CGSize((_icon !=null) ? 215f : 277f, float.MaxValue), UILineBreakMode.WordWrap);
					#else
					descSize = StringSize (description, UIFont.SystemFontOfSize(appearance.descriptionSize), new CGSize((_icon !=null) ? 215f : 277f, float.MaxValue), UILineBreakMode.WordWrap);
					#endif
					height += 9f+ ((_icon != null && descSize.Height < 22f) ? 22f : descSize.Height);

					//Is there a title?
					if (title == "" && _icon != null) {
						//No, add an adjust in
						height += 32f;
					}
				} else if (_icon != null) {
					//Add the rest of the icon to the offset
					height += 32f;
				}

				//Add the height of our subview to the alert
				if (subview != null) {
					height += subview.Frame.Height;
				}
				break;
			case ACAlertType.ActivityAlertMedium:
				//Add size of indicator
				height += 60f;
				break;
			}

			//Does the alert have buttons?
			if (_buttons.Count > 0) {
				height += 60f;
			} else {
				height += 13f;
			}

			//Add room for shadow
			height += 10f;

			//Make any adjustments based on type
			switch (type) {
			case ACAlertType.ActivityAlert:
				if (description=="") {
					//Adjust activity indication to fit
					_activityIndicator.ActivityIndicatorViewStyle = UIActivityIndicatorViewStyle.White;
					_activityIndicator.Frame = new CGRect (20, 15, 20, 20);

					//Are we running on iOS 7?
					if (iOSDevice.isIOS7) {
						//Yes, set the activity indicator color
						_activityIndicator.Color = appearance.activityIndicatorColor;
					}
				}
				break;
			case ACAlertType.ActivityAlertMedium:
				_activityIndicator.Frame = new CGRect (43,44,37,37);
				break;
			case ACAlertType.ProgressAlert:
				height += 30f;
				break;
			}

			//Return new height
			return height;
		}

		/// <summary>
		/// Calculates the width of the alert based on its type and elements inside it
		/// </summary>
		/// <returns>The alert width.</returns>
		private float CalculateAlertWidth(){
			float width = 0f;

			//Take action based on the alert's type
			switch (type) {
			case ACAlertType.ActivityAlert:
			case ACAlertType.Subview:
			case ACAlertType.ProgressAlert:
			case ACAlertType.Default:
				width = 312f;
				break;
			case ACAlertType.ActivityAlertMedium:
				width = 123f;
				break;
			}

			//Return new width
			return width;
		}

		/// <summary>
		/// Adjusts the alert position based on its content, style, and location
		/// </summary>
		private void AdjustAlertPosition(){
			nfloat x = 0, y = 0;
			nfloat width = CalculateAlertWidth ();
			nfloat height = CalculateAlertHeight ();

			//Calculate x position
			x = (iOSDevice.AvailableScreenBounds.Width / 2f) - (width / 2f);

			//Calculate y position based on the requested location
			switch (location) {
			case ACAlertLocation.Top:
				y = 20f;
				break;
			case ACAlertLocation.Middle:
				y = (iOSDevice.AvailableScreenBounds.Height / 2f) - (height / 2f);
				break;
			case ACAlertLocation.Bottom:
				y = iOSDevice.AvailableScreenBounds.Height - 20f - height;
				break;
			}

			//Move alert into position
			Frame = new CGRect (x, y, width, height);

			//Force alert to redraw itself
			Redraw ();
		}

		/// <summary>
		/// Adjusts the alert position.
		/// </summary>
		/// <param name="view">View.</param>
		private void AdjustAlertPosition(UIView view){
			nfloat x = 0, y = 0;
			nfloat width = CalculateAlertWidth ();
			nfloat height = CalculateAlertHeight ();

			//Calculate x position
			x = (view.Frame.Width / 2f) - (width / 2f);

			//Calculate y position based on the requested location
			switch (location) {
			case ACAlertLocation.Top:
				y = 0f;
				break;
			case ACAlertLocation.Middle:
				y = (view.Frame.Height / 2f) - (height / 2f);
				break;
			case ACAlertLocation.Bottom:
				y = view.Frame.Height -  height;
				break;
			}

			//Move alert into position
			Frame = new CGRect (x, y, width, height);

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
				if (Frame.Left < _xConstraint.minimumValue || Frame.Left > _xConstraint.maximumValue) {
					//Pin to the minimum value
					Frame = new CGRect (_xConstraint.minimumValue, Frame.Top, Frame.Width, Frame.Height);
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
				if (Frame.Top < _yConstraint.minimumValue || Frame.Top > _yConstraint.maximumValue) {
					//Pin to the minimum value
					Frame = new CGRect (Frame.Left, _yConstraint.minimumValue, Frame.Width, Frame.Height); 
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

			//Already shown?
			if (!hidden) return;

			//Gain access to master window
			UIWindow window = UIApplication.SharedApplication.Windows [0];

			//Is this a modal dialog box?
			if (modal) {
				//Create an overlay and display it
				_overlay = new ACAlertOverlay (this);
				window.RootViewController.View.AddSubview (_overlay);

				//Wire-up event
				_overlay.Touched += () => {
					RaiseOverlayTouched();
				};
			}

			//Displays the alert within the parent view
			window.RootViewController.View.AddSubview (this);

			//Activate any suplmental controls based on type
			switch (type) {
			case ACAlertType.ActivityAlertMedium:
			case ACAlertType.ActivityAlert:
				_activityIndicator.StartAnimating ();
				break;
			}

			//Save state
			_hidden = false;
		}

		/// <summary>
		/// Show the <see cref="ActionComponents.ACAlert"/> in specified rootView and reposition if needed.
		/// </summary>
		/// <param name="rootView">Root view.</param>
		/// <param name="reposition">If set to <c>true</c> reposition.</param>
		public void Show(UIView rootView, bool reposition){

			//Already shown?
			if (!hidden) return;

			//Is this a modal dialog box?
			if (modal) {
				//Create an overlay and display it
				_overlay = new ACAlertOverlay (this);
				rootView.AddSubview (_overlay);

				//Wire-up event
				_overlay.Touched += () => {
					RaiseOverlayTouched();
				};
			}

			//Displays the alert within the parent view
			rootView.AddSubview (this);

			// Reposition to match parent view?
			if (reposition) {
				// Yes, attempt to match the position and size of the root view
				_overlay.Frame = rootView.Frame;
				AdjustAlertPosition (rootView);
			}

			//Activate any suplmental controls based on type
			switch (type) {
				case ACAlertType.ActivityAlertMedium:
				case ACAlertType.ActivityAlert:
				_activityIndicator.StartAnimating ();
				break;
			}

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

			//Is there an overlay?
			if (_overlay != null) {
				//Remove it
				_overlay.RemoveFromSuperview ();
				_overlay = null;
			}

			//Remove from parent
			this.RemoveFromSuperview ();

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
			buttonAppearanceTouched.Flatten (UIColor.FromRGB (0,122,255), UIColor.FromRGB (234, 233, 232));
		}

		/// <summary>
		/// Flattens the <see cref="ActionComponents.ACAlert"/> and its <see cref="ActionComponents.ACAlertButton"/>s
		/// to match the new iOS 7 design language
		/// </summary>
		/// <param name="backgroundColor">Background color.</param>
		public void Flatten(UIColor backgroundColor){

			//Adjust all appearances for iOS 7
			appearance.Flatten (backgroundColor);
			buttonAppearance.Flatten (backgroundColor);
			buttonAppearanceHighlighted.Flatten (backgroundColor);
			buttonAppearanceTouched.Flatten (UIColor.FromRGB (0,122,255), backgroundColor);
		}

		/// <summary>
		/// Flattens the <see cref="ActionComponents.ACAlert"/> and its <see cref="ActionComponents.ACAlertButton"/>s
		/// to match the new iOS 7 design language
		/// </summary>
		/// <param name="backgroundColor">Background color.</param>
		/// <param name="foregroundColor">Foreground color.</param>
		public void Flatten(UIColor backgroundColor, UIColor foregroundColor){

			//Adjust all appearances for iOS 7
			appearance.Flatten (backgroundColor,foregroundColor);
			buttonAppearance.Flatten (backgroundColor);
			buttonAppearanceHighlighted.Flatten (backgroundColor);
			buttonAppearanceTouched.Flatten (UIColor.FromRGB (0,122,255), backgroundColor);
		}

		/// <summary>
		/// Forces the <see cref="ActionComponents.ACAlert"/> to redraw itself
		/// </summary>
		public void Redraw(){
			//Force the component to redraw
			SetNeedsDisplay ();
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
				button= new ACAlertButton (this, buttonAppearance, buttonAppearanceDisabled, buttonAppearanceTouched, buttonAppearanceHighlighted, title, (i==(titles.Length-1)));

				//Add button to collection
				_buttons.Add (button);

				//Add button to view
				AddSubview (button);

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
			ACAlertButton button= new ACAlertButton (this, appearance, appearanceDisabled, appearanceTouched, appearanceHighlighted, title, highlighted);

			//Add button to collection
			_buttons.Add (button);

			//Adjust alert height
			AdjustAlertPosition ();

			//Adjust button positions
			RepositionButtons ();

			//Add button to view
			AddSubview (button);

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
		/// Moves this <see cref="ActionComponents.ACAlert"/> to the given point and honors any
		/// <see cref="Appracatappra.ActionComponents.ActionView.ACAlertDragConstraint"/>s applied to the <c>X</c> or <c>Y</c> axis 
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public void MoveToPoint(float x, float y) {

			//Ensure that we are moving as expected
			_startLocation = new CGPoint (0, 0);

			//Create a new point and move to it
			MoveToPoint (new CGPoint(x,y));
		}

		/// <summary>
		/// Moves this <see cref="ActionComponents.ACAlert"/> to the given point and honors any
		/// <see cref="Appracatappra.ActionComponents.ActionView.ACAlertDragConstraint"/>s applied to the <c>X</c> or <c>Y</c> axis 
		/// </summary>
		/// <param name="pt">Point.</param>
		public void MoveToPoint(CGPoint pt) {

			//Grab frame
			var frame = this.Frame;

			//Process x coord constraint
			switch(xConstraint.constraintType) {
				case ACAlertDragConstraintType.None:
				//Adjust frame location
				frame.X += pt.X - _startLocation.X;
				break;
				case ACAlertDragConstraintType.Locked:
				//Don't move x coord
				break;
				case ACAlertDragConstraintType.Constrained:
				//Adjust frame location
				frame.X += pt.X - _startLocation.X;

				//Outside constraints
				if (frame.X<xConstraint.minimumValue) {
					frame.X=xConstraint.minimumValue;
				} else if (frame.X>xConstraint.maximumValue) {
					frame.X=xConstraint.maximumValue;
				}
				break;
			}

			//Process y coord constraint
			switch(yConstraint.constraintType) {
				case ACAlertDragConstraintType.None:
				//Adjust frame location
				frame.Y += pt.Y - _startLocation.Y;
				break;
				case ACAlertDragConstraintType.Locked:
				//Don't move x coord
				break;
				case ACAlertDragConstraintType.Constrained:
				//Adjust frame location
				frame.Y += pt.Y - _startLocation.Y;

				//Outside constraints
				if (frame.Y<yConstraint.minimumValue) {
					frame.Y=yConstraint.minimumValue;
				} else if (frame.Y>yConstraint.maximumValue) {
					frame.Y=yConstraint.maximumValue;
				}
				break;
			}

			//Apply new location
			this.Frame = frame;
		}

		/// <summary>
		/// Resize this <see cref="ActionComponents.ACAlert"/> to the specified width and height.
		/// </summary>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		public void Resize(float width, float height){
			//Resize this view
			Frame = new CGRect (Frame.Left, Frame.Top, width, height);
		}

		/// <summary>
		/// Rotates this <see cref="ActionComponents.ACAlert"/> to the given degrees
		/// </summary>
		/// <param name="degrees">Degrees.</param>
		public void RotateTo(float degrees) {
			this.Transform=CGAffineTransform.MakeRotation(ACImage.DegreesToRadians(degrees));	
		}

		/// <summary>
		/// Tests to see if the given x and y coordinates are inside this <see cref="ActionComponents.ACAlert"/> 
		/// </summary>
		/// <returns><c>true</c>, if the point was inside, <c>false</c> otherwise.</returns>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public bool PointInside(nfloat x, nfloat y){
			//Is the give x inside
			if (x>=Frame.X && x<=(Frame.X+Frame.Width)) {
				if (y>=Frame.Y && y<=(Frame.Y+Frame.Height)) {
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
		/// <returns><c>true</c>, if point was inside, <c>false</c> otherwise.</returns>
		/// <param name="pt">Point.</param>
		public bool PointInside(CGPoint pt){
			return PointInside (pt.X, pt.Y);
		}

		#endregion 

		#region Private Drawing Methods
		/// <summary>
		/// Sets the corner.
		/// </summary>
		/// <returns>The corner.</returns>
		/// <param name="corners">Corners.</param>
		/// <param name="addCorner">Add corner.</param>
		private UIRectCorner SetCorner(UIRectCorner corners, UIRectCorner addCorner){

			//Has a corner been set yet?
			if (corners==UIRectCorner.AllCorners) {
				corners = addCorner;
			} else {
				corners |= addCorner;
			}

			//Return adjusted corners
			return corners;
		}

		/// <summary>
		/// Draws the default alert.
		/// </summary>
		/// <param name="rect">Rect.</param>
		private void DrawDefaultAlert (CGRect rect){
			//// General Declarations
			var context = UIGraphics.GetCurrentContext();
			#if __UNIFIED__
			var descSize = UIStringDrawing.StringSize(description, UIFont.SystemFontOfSize(appearance.descriptionSize), new CGSize((_icon !=null) ? 215f : 277f, float.MaxValue), UILineBreakMode.WordWrap);
			#else
			var descSize = UIStringDrawing.StringSize(description, UIFont.SystemFontOfSize(appearance.descriptionSize), new CGSize((_icon !=null) ? 215f : 277f, float.MaxValue), UILineBreakMode.WordWrap);
			#endif
			CGRect titleRect, descriptionRect, progressRect;

			//Does this alert have an icon?
			if (_icon == null) {
				//No
				titleRect = new CGRect(13, 15, 278, 17);
				descriptionRect = new CGRect(13, (title=="") ? 15 : 41, 278, descSize.Height);
				progressRect = new CGRect (13, descriptionRect.Top + descSize.Height + 10f, 278, 9);

				//Do we have a subview?
				if (subview != null) {
					_subview.Frame = new CGRect (13, (title == "") ? 15 : 41, 278, subview.Frame.Height);
				}
			} else {
				//Yes
				titleRect = new CGRect(76, 15, 215, 17);
				descriptionRect = new CGRect(76, (title=="") ? 15 : 41, 215, descSize.Height);
				progressRect = new CGRect (76, descriptionRect.Top + descSize.Height + 10f, 215, 9);

				//Do we have a subview?
				if (subview != null) {
					_subview.Frame = new CGRect (76, (title == "") ? 15 : 41, 215, subview.Frame.Height);
				}

				//// alertIcon Drawing
				var alertIconPath = UIBezierPath.FromRect(new CGRect(12.5f, 14.5f, 54.5F, 54.5f));
				context.SaveState();
				alertIconPath.AddClip();
				icon.Draw(new CGRect(13, 15, 54, 54));
				context.RestoreState();
			}

			//Does the alert have a title?
			if (title != "") {
				//// Title Drawing
				appearance.titleColor.SetFill();
				new NSString(title).DrawString(titleRect, UIFont.BoldSystemFontOfSize(appearance.titleSize), UILineBreakMode.Clip, UITextAlignment.Left);
			}

			//Does the alert have a description?
			if (description!="") {
				//// Description Drawing
				appearance.descriptionColor.SetFill();
				new NSString(description).DrawString(descriptionRect, UIFont.SystemFontOfSize(appearance.descriptionSize), UILineBreakMode.WordWrap, UITextAlignment.Left);
			}

			//Does the alert have a subview?
			if (subview != null) {
				AddSubview (subview);
			}

			//Is this a progress style alert?
			if (type == ACAlertType.ProgressAlert) {
				_progressView.Frame = progressRect;
			}
		}

		/// <summary>
		/// Draws the activity alert.
		/// </summary>
		/// <param name="rect">Rect.</param>
		private void DrawActivityAlert(CGRect rect){
			//// General Declarations
			var context = UIGraphics.GetCurrentContext();
			#if __UNIFIED__
			var descSize = UIStringDrawing.StringSize(description, UIFont.SystemFontOfSize(appearance.descriptionSize), new CGSize( 215f, float.MaxValue), UILineBreakMode.WordWrap);
			#else
			var descSize = StringSize(description, UIFont.SystemFontOfSize(appearance.descriptionSize), new CGSize( 215f, float.MaxValue), UILineBreakMode.WordWrap);
			#endif
			CGRect titleRect, descriptionRect;

			//Calculate positioning
			titleRect = new CGRect((description=="") ? 50 : 76, 15, 215, 17);
			descriptionRect = new CGRect(76, (title=="") ? 15 : 41, 215, descSize.Height);

			//Does the alert have a title?
			if (title != "") {
				//// Title Drawing
				appearance.titleColor.SetFill();
				new NSString(title).DrawString(titleRect, UIFont.BoldSystemFontOfSize(appearance.titleSize), UILineBreakMode.Clip, UITextAlignment.Left);
			}

			//Does the alert have a description?
			if (description!="") {
				//// Description Drawing
				appearance.descriptionColor.SetFill();
				new NSString(description).DrawString(descriptionRect, UIFont.SystemFontOfSize(appearance.descriptionSize), UILineBreakMode.WordWrap, UITextAlignment.Left);
			}
		}

		private void DrawMediumActivityAlert(CGRect rect){
			//// General Declarations
			var context = UIGraphics.GetCurrentContext();
			CGRect titleRect;

			//Calculate positioning
			titleRect = new CGRect(10,15,103,17);

			//Does the alert have a title?
			if (title != "") {
				//// Title Drawing
				appearance.titleColor.SetFill();
				new NSString(title).DrawString(titleRect, UIFont.BoldSystemFontOfSize(appearance.titleSize), UILineBreakMode.Clip, UITextAlignment.Center);
			}


		}
		#endregion

		#region Override Methods
		/// <summary>
		/// Draw the specified rect.
		/// </summary>
		/// <param name="rect">Rect.</param>
		#if __UNIFIED__
		public override void Draw (CoreGraphics.CGRect rect)
		#else
		public override void Draw (CGRect rect)
		#endif
		{
			//Create adjusted box for shadow
			CGRect alertRect = new CGRect (rect.Left + 5f, rect.Top + 2f, rect.Width - 10f, rect.Height - 10f);

			//// General Declarations
			var context = UIGraphics.GetCurrentContext();
			UIBezierPath alertBodyPath;
			UIRectCorner corners=UIRectCorner.AllCorners;

			//// Shadow Declarations
			var alertShadow = appearance.shadow.CGColor;
			var alertShadowOffset = new CGSize(0.1f, 3.1f);
			var alertShadowBlurRadius = 5;

			//// AlertBox
			{

				//// AlertBody Drawing
				if (appearance.isRect) {
					//It is a perfect rectangle
					alertBodyPath = UIBezierPath.FromRect (alertRect);
				} else if (appearance.isRoundRect) {
					//It is a perfect round rectangle
					alertBodyPath = UIBezierPath.FromRoundedRect(alertRect, appearance.borderRadius);
				} else {
					//It is a round rectangle with one or more square corners
					//Calculate corners
					if (appearance.roundTopLeftCorner) corners = SetCorner (corners, UIRectCorner.TopLeft);
					if (appearance.roundTopRightCorner) corners = SetCorner (corners, UIRectCorner.TopRight);
					if (appearance.roundBottomLeftCorner) corners = SetCorner (corners, UIRectCorner.BottomLeft);
					if (appearance.roundBottomRightCorner) corners = SetCorner (corners, UIRectCorner.BottomRight);

					//Make path
					alertBodyPath = UIBezierPath.FromRoundedRect(alertRect, corners, new CGSize(appearance.borderRadius, appearance.borderRadius));
				}
				context.SaveState();
				#if __UNIFIED__
				context.SetShadow(alertShadowOffset, alertShadowBlurRadius, alertShadow);
				#else
				context.SetShadowWithColor(alertShadowOffset, alertShadowBlurRadius, alertShadow);
				#endif
				appearance.background.SetFill();
				alertBodyPath.Fill();
				context.RestoreState();

				appearance.border.SetStroke();
				alertBodyPath.LineWidth = appearance.borderWidth;
				alertBodyPath.Stroke();
	
			}

			//Take action based on the alert's type
			switch (type) {
			case ACAlertType.Subview:
			case ACAlertType.ProgressAlert:
			case ACAlertType.Default:
				DrawDefaultAlert (alertRect);
				break;
			case ACAlertType.ActivityAlert:
				DrawActivityAlert (alertRect);
				break;
			case ACAlertType.ActivityAlertMedium:
				DrawMediumActivityAlert (alertRect);
				break;
			}

			base.Draw (rect);

		}

		/// <Docs>Set containing the touches.</Docs>
		/// <summary>
		/// Sent when one or more fingers touches the screen.
		/// </summary>
		/// <param name="touches">Touches.</param>
		/// <param name="evt">Evt.</param>
		public override void TouchesBegan (NSSet touches, UIEvent evt)
		{
			//Already dragging?
			if (_dragging) return;

			//Save starting location
			var pt = (touches.AnyObject as UITouch).LocationInView(this);
			_startLocation = pt;

			//Automatically bring view to front?
			if (_bringToFrontOnTouched && this.Superview!=null) this.Superview.BringSubviewToFront(this);

			//Inform caller of event
			RaiseTouched ();

			//Pass call to base object
			base.TouchesBegan (touches, evt);
		}

		/// <Docs>Set containing the touches.</Docs>
		/// <summary>
		/// Sent when the <see cref="ActionComponents.ACAlert"/> is being dragged
		/// </summary>
		/// <param name="touches">Touches.</param>
		/// <param name="evt">Evt.</param>
		public override void TouchesMoved (NSSet touches, UIEvent evt)
		{
			//Is this view draggable?
			if (draggable) {
				// Move relative to the original touch point
				_dragging=true;
				var pt = (touches.AnyObject as UITouch).LocationInView(this);
				MoveToPoint(pt);

				//Inform caller of event
				RaiseMoved ();
			}

			//Pass call to base object
			base.TouchesMoved(touches, evt);
		}

		/// <Docs>Set containing the touches.</Docs>
		/// <summary>
		/// Send when one or more fingers are lifted from the screen.
		/// </summary>
		/// <param name="touches">Touches.</param>
		/// <param name="evt">Evt.</param>
		public override void TouchesEnded (NSSet touches, UIEvent evt)
		{
			//Is this view draggable?
			if (draggable) {
				// Move relative to the original touch point 
				var pt = (touches.AnyObject as UITouch).LocationInView(this);
				MoveToPoint(pt);
				_dragging=false;
			}

			//Clear starting point
			_startLocation = new CGPoint (0, 0);

			//Inform caller of event
			RaiseReleased ();

			#if TRIAL
			ACToast.MakeText("ACAlert by Appracatappra", ACToastLength.Short).Show();
#else
			AppracatappraLicenseManager.ValidateLicense();
#endif

			//Pass call to base object
			base.TouchesEnded(touches, evt);
		} 
		#endregion 

		#region Events
		/// <summary>
		/// Occurs when this <see cref="ActionComponents.ACAlert"/> is touched 
		/// </summary>
		public delegate void ACAlertTouchedDelegate (ACAlert alert);
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
		public delegate void ACAlertMovedDelegate (ACAlert alert);
		public event ACAlertMovedDelegate Moved;

		/// <summary>
		/// Raises the moved event
		/// </summary>
		private void RaiseMoved ()
		{
			if (this.Moved != null)
				this.Moved (this);
		}

		/// <summary>
		/// Occurs when this <see cref="ActionComponents.ACAlert"/> is released 
		/// </summary>
		public delegate void ACAlertReleasedDelegate (ACAlert alert);
		public event ACAlertReleasedDelegate Released;

		/// <summary>
		/// Raises the released.
		/// </summary>
		private void RaiseReleased ()
		{
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

