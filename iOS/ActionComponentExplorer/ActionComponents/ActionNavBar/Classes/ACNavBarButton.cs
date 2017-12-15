using System;
using Foundation;
using UIKit;
using CoreGraphics;
using ObjCRuntime;

namespace ActionComponents
{
	/// <summary>
	/// Defines a button that can be added to a <see cref="ActionComponents.ACNavBar"/> of a given <see cref="ActionComponents.ACNavBarButtonType"/> and
	/// <see cref="ActionComponents.ACNavBarButtonState"/>. Three <see cref="ActionComponents.ACNavBarButtonAppearance"/> properties controll the look and
	/// feel of the button when it is Enabled, Disabled or Selected. 
	/// </summary>
	/// <remarks><see cref="ActionComponents.ACNavBarButton"/>s cannot be created directly but are built by methods of the <see cref="ActionComponents.ACNavBarButtonCollection"/> 
	/// as <c>AddButton</c>, <c>AddAutoDisposingButton</c>, <c>AddTool</c> or <c>AddNotification</c> </remarks>
	[Register("ACNavBarButton")]
	public class ACNavBarButton : UIView
	{
		#region Private Variable Storage
		private ACNavBarButtonState _state = ACNavBarButtonState.Enabled;
		private ACNavBarButtonType _type = ACNavBarButtonType.View;
		private UIView _attachedView;
		private ACNavBarButtonAppearance _appearanceEnabled;
		private ACNavBarButtonAppearance _appearanceDisabled;
		private ACNavBarButtonAppearance _appearanceSelected;
		#endregion

		#region Computed Properties
		/// <summary>
		/// [OPTIONAL] object that can be attached to this <see cref="ActionComponents.ACNavBarButton"/> 
		/// </summary>
		public new object Tag { get; set; }

		/// <summary>
		/// Gets or sets the group this button belongs to.
		/// </summary>
		/// <value>The group.</value>
		public string Group { get; set; } = "";

		/// <summary>
		/// Gets or sets the name of the storyboard to load the view from.
		/// </summary>
		/// <value>The name of the storyboard.</value>
		public string StoryboardName { get; set; } = "Main";

		/// <summary>
		/// Gets or sets the name of the view to load from a storyboard.
		/// </summary>
		/// <value>The name of the view.</value>
		public string ViewName { get; set; } = "";

		/// <summary>
		/// Controls the appearance of the <see cref="ActionComponents.ACNavBarButton"/> when it is the <c>Enabled</c> state 
		/// </summary>
		public ACNavBarButtonAppearance AppearanceEnabled {
			get { return _appearanceEnabled; }
			set {
				// Save values
				_appearanceEnabled = value;

				// Wireup events
				_appearanceEnabled.AppearanceModified += SetNeedsLayout;
			}
		}

		/// <summary>
		/// Controls the appearance of the <see cref="ActionComponents.ACNavBarButton"/> when it is in the <c>Disabled</c> state 
		/// </summary>
		public ACNavBarButtonAppearance AppearanceDisabled
		{
			get { return _appearanceDisabled; }
			set
			{
				// Save values
				_appearanceDisabled = value;

				// Wireup events
				_appearanceDisabled.AppearanceModified += SetNeedsLayout;
			}
		}

		/// <summary>
		/// Controls the appearance of the <see cref="ActionComponents.ACNavBarButton"/> when it is in the <c>Selected</c> state
		/// </summary>
		public ACNavBarButtonAppearance AppearanceSelected
		{
			get { return _appearanceSelected; }
			set
			{
				// Save values
				_appearanceSelected = value;

				// Wireup events
				_appearanceSelected.AppearanceModified += SetNeedsLayout;
			}
		}

		/// <summary>
		/// Gets the current state of the button
		/// </summary>
		/// <value>The button's state</value>
		/// <remarks>You cannot set a button's state directly, it's set in response to events in the parent <see cref="ActionComponents.ACNavBar"/> </remarks>
		public ACNavBarButtonState State
		{
			get { return _state; }
			set
			{
				// Was this button previously selected?
				if (_state == ACNavBarButtonState.Selected && value != ACNavBarButtonState.Selected)
				{
					// Yes, handle deselection
					ButtonUnselected();
				}

				// Save new state
				_state = value;

				// Set interaction
				UserInteractionEnabled = (_state == ACNavBarButtonState.Enabled ||
										  _state == ACNavBarButtonState.Selected);
				MultipleTouchEnabled = UserInteractionEnabled;

				// Force redraw
				SetNeedsDisplay();

				// Inform caller of change
				StateChanged?.Invoke(this);
			}
		}

		/// <summary>
		/// Returns the type of this <see cref="ActionComponents.ACNavBarButton"/>
		/// </summary>
		/// <value>The <see cref="ActionComponents.ACNavBarButtonType"/> type</value>
		/// <remarks>You cannot set a button's type directly, it's set based on which method of the <see cref="ActionComponents.ACNavBarButtonCollection"/>
		/// was used to create it: <c>AddButton</c>, <c>AddAutoDisposingButton</c>, <c>AddTool</c> or <c>AddNotification</c></remarks>
		public ACNavBarButtonType Type
		{
			get { return _type; }
			internal set
			{
				_type = value;
			}
		}

		/// <summary>
		/// Gets or sets the attached view controller.
		/// </summary>
		/// <value>The attached view controller.</value>
		public UIViewController AttachedViewController { get; set; }

		/// <summary>
		/// Gets or sets the <c>UIView</c>  being controlled by this <see cref="ActionComponents.ACNavBarButton"/>
		/// </summary>
		/// <value>The view.</value>
		/// <remarks>WARNING! This property should ONLY be set in response to a <c>RequestNewView</c> event on this <see cref="ActionComponents.ACNavBarButton"/>. 
		/// Setting the view outside of the event can cause undetermined behavior in the parent <see cref="ActionComponents.ACNavBar"/> and display issues!</remarks>
		public UIView AttachedView
		{
			get { return _attachedView; }
			set
			{
				// Save new view
				_attachedView = value;

				// Setting to null?
				if (_attachedView != null)
				{
					// Initially hide the view
					_attachedView.Alpha = 0.0f;

					// Insert the new view below the NavBar in the view stack
					Superview.Superview.Superview.InsertSubviewBelow(_attachedView, Superview.Superview);

					// Animate the view becoming visible
					// Define Animation
					UIView.BeginAnimations("ViewTransition");
					UIView.SetAnimationDuration(0.5f);

					// Fade view into existance
					_attachedView.Alpha = 1.0f;

					// Execute Animation
					UIView.CommitAnimations();
				}
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="ActionComponents.ACNavBarButton"/> is enabled.
		/// </summary>
		/// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
		/// <remarks>WARNING! You cannot disable the currently selected <see cref="ActionComponents.ACNavBarButton"/> </remarks>
		public bool IsEnabled
		{
			get { return (State == ACNavBarButtonState.Enabled || State == ACNavBarButtonState.Selected); }
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="ActionComponents.ACNavBarButton"/> is hidden.
		/// </summary>
		/// <value><c>true</c> if hidden; otherwise, <c>false</c>.</value>
		/// <remarks>WARNING! You cannot hide the currently selected <see cref="ActionComponents.ACNavBarButton"/> </remarks>
		public bool IsHidden
		{
			get { return State == ACNavBarButtonState.Hidden; }
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACNavBarButton"/> class.
		/// </summary>
		public ACNavBarButton()
		{
			// Initialize
			State = ACNavBarButtonState.Enabled;
			Initialize(null);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACNavBarButton"/> class.
		/// </summary>
		/// <param name="image">Image.</param>
		public ACNavBarButton(UIImage image)
		{
			// Initialize
			State = ACNavBarButtonState.Enabled;
			Initialize(image);

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACNavBarButton"/> class.
		/// </summary>
		/// <param name="imageName">Image name.</param>
		public ACNavBarButton(string imageName)
		{
			// Initialize
			State = ACNavBarButtonState.Enabled;
			Initialize(UIImage.FromBundle(imageName));

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACNavBarButton"/> class.
		/// </summary>
		/// <param name="type">Type.</param>
		/// <param name="image">Image.</param>
		public ACNavBarButton(ACNavBarButtonType type, UIImage image)
		{
			// Initialize
			_type = type;
			State = ACNavBarButtonState.Enabled;
			Initialize(image);

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACNavBarButton"/> class.
		/// </summary>
		/// <param name="type">Type.</param>
		/// <param name="imageName">Image name.</param>
		public ACNavBarButton(ACNavBarButtonType type, string imageName)
		{
			// Initialize
			_type = type;
			State = ACNavBarButtonState.Enabled;
			Initialize(UIImage.FromBundle(imageName));

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACNavBarButton"/> class.
		/// </summary>
		/// <param name="image">Image.</param>
		/// <param name="state">State.</param>
		public ACNavBarButton(UIImage image, ACNavBarButtonState state)
		{

			// Initialize
			State = state;
			Initialize(image);

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACNavBarButton"/> class.
		/// </summary>
		/// <param name="imageName">Image name.</param>
		/// <param name="state">State.</param>
		public ACNavBarButton(string imageName, ACNavBarButtonState state)
		{

			// Initialize
			State = state;
			Initialize(UIImage.FromFile(imageName));

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACNavBarButton"/> class.
		/// </summary>
		/// <param name="type">Type.</param>
		/// <param name="image">Image.</param>
		/// <param name="state">State.</param>
		public ACNavBarButton(ACNavBarButtonType type, UIImage image, ACNavBarButtonState state)
		{

			// Initialize
			_type = type;
			State = state;
			Initialize(image);

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACNavBarButton"/> class.
		/// </summary>
		/// <param name="type">Type.</param>
		/// <param name="imageName">Image name.</param>
		/// <param name="state">State.</param>
		public ACNavBarButton(ACNavBarButtonType type, string imageName, ACNavBarButtonState state)
		{

			// Initialize
			_type = type;
			State = state;
			Initialize(UIImage.FromFile(imageName));
		}

		/// <summary>
		/// Initialize the specified image.
		/// </summary>
		/// <returns>The initialize.</returns>
		/// <param name="image">Image.</param>
		internal void Initialize(UIImage image)
		{

			// Set initial bounds
			Frame = new CGRect(0, 0, 60, 40);

			//Make the button itself clear
			BackgroundColor = UIColor.Clear;

			//Set default appearances
			AppearanceEnabled = new ACNavBarButtonAppearance(image);

			AppearanceDisabled = new ACNavBarButtonAppearance(image)
			{
				Alpha = 0.5f
			};

			AppearanceSelected = new ACNavBarButtonAppearance(image);
		}
		#endregion

		#region Internal Methods
		/// <summary>
		/// Moves button view to the given location
		/// </summary>
		/// <param name="x">
		/// The new x position
		/// </param>
		/// <param name="y">
		/// The new y position
		/// </param>
		internal void MoveTo(nfloat x, nfloat y)
		{

			// Move to new location
			Frame = new CGRect(x, y, Frame.Width, Frame.Height);
		}

		/// <summary>
		/// Called when a button is unselected.
		/// </summary>
		internal void ButtonUnselected()
		{

			//Take action based on the type of button
			switch (Type)
			{
				case ACNavBarButtonType.View:
					// If a view is attached to this button hide it
					if (AttachedView != null)
					{
						// Disable user interaction
						AttachedView.UserInteractionEnabled = false;

						// Animate the view becoming invisible
						// Define Animation
						UIView.BeginAnimations("ViewTransition");
						UIView.SetAnimationDuration(0.5f);

						// Fade view out of existance
						AttachedView.Alpha = 0.0f;

						// Execute Animation
						UIView.CommitAnimations();
					}

					// Inform caller that the view has been hidden
					ViewHidden?.Invoke(this);
					break;
				case ACNavBarButtonType.AutoDisposingView:
					// Is a view attached to this button?
					if (AttachedView != null)
					{
						// Disable user interaction
						AttachedView.UserInteractionEnabled = false;

						// Define Animation
						UIView.BeginAnimations("ViewTransition");
						UIView.SetAnimationDuration(0.5f);

						// Set end of Animation handler
						UIView.SetAnimationDelegate(this);
						UIView.SetAnimationDidStopSelector(new Selector("AnimationCompleted"));

						// Fade view out of existance
						AttachedView.Alpha = 0.0f;

						// Execute Animation
						UIView.CommitAnimations();
					}

					// Inform caller that the view has been disposed of
					ViewDisposed?.Invoke(this);
					break;
			}
		}

		/// <summary>
		/// Handles the removal of the view from memory afer an automatic dispose happens
		/// </summary>
		[Export("AnimationCompleted")]
		internal virtual void AnimationCompleted()
		{

			// Remove the view from the superview
			AttachedView.RemoveFromSuperview();

			// Request view be removed from memory
			AttachedView.Dispose();
			AttachedView = null;

			// Release any attached view controllers
			AttachedViewController = null;

		}

		/// <summary>
		/// Performs the action for the button when tapped by the user
		/// </summary>
		internal void Invoke() {
			// Take action based on the type of NavBarButton this is
			switch (Type)
			{
				case ACNavBarButtonType.AutoDisposingView:
				case ACNavBarButtonType.View:
					// Does this button already have a view attached?
					if (AttachedView == null)
					{
						// Auto loading from a storyboard?
						if (ViewName != "")
						{
							// Load the named view from the named storyboard
							var storyboard = UIStoryboard.FromName(StoryboardName, null);
							AttachedViewController = storyboard.InstantiateViewController(ViewName);
							AttachedView = AttachedViewController.View;
						}
						else
						{
							// No, request that the caller builds a view for us
							AttachedView = RequestNewView?.Invoke(this);
						}
					}
					else
					{
						// Animate the view becoming visible
						// Define Animation
						UIView.BeginAnimations("ViewTransition");
						UIView.SetAnimationDuration(0.5f);

						// Fade view into existance
						AttachedView.Alpha = 1.0f;

						//Execute Animation
						UIView.CommitAnimations();
						AttachedView.UserInteractionEnabled = true;
					}

					// Automatically select this button
					State = ACNavBarButtonState.Selected;
					break;
				case ACNavBarButtonType.Tool:
					// No view adjustment required
					break;
				case ACNavBarButtonType.Selection:
					// Automatically select this button
					State = ACNavBarButtonState.GroupSelected;
					break;
				case ACNavBarButtonType.Notification:
					// Notifications show status information only and are not
					// allowed to be touchable
					return;
			}

			// If a deligate has been attached, inform caller of
			// touchdown event
			Touched?.Invoke(this);
		}
		#endregion

		#region Override Methods
		/// <summary>
		/// Draws the button's GUI within the given rect
		/// </summary>
		/// <param name="rect">Rect specifying the button's bounds</param>
		public override void Draw(CGRect rect)
		{
			// Call base
			base.Draw(rect);

			// Set appearance based on state
			ACNavBarButtonAppearance appearance = AppearanceEnabled;

			switch (State)
			{
				case ACNavBarButtonState.Hidden:
					// Make invisible and abort drawing
					Alpha = 0.0f;
					return;
				case ACNavBarButtonState.Enabled:
					appearance = AppearanceEnabled;
					break;
				case ACNavBarButtonState.Disabled:
					appearance = AppearanceDisabled;
					break;
				case ACNavBarButtonState.Selected:
				case ACNavBarButtonState.GroupSelected:
					appearance = AppearanceSelected;
					break;
			}

			// Start Drawing
			Alpha = appearance.Alpha;
			var context = UIGraphics.GetCurrentContext();

			// Body Drawing
			var bodyPath = UIBezierPath.FromRect(new CGRect(0, 0, rect.Width, rect.Height));
			appearance.Background.SetFill();
			bodyPath.Fill();
			appearance.Border.SetStroke();
			bodyPath.LineWidth = 1;
			bodyPath.Stroke();

			// Was an image defined?
			if (appearance.Image == null)
				return;

			// Icon Drawing
			nfloat x = (rect.Width / 2) - (appearance.Image.Size.Width / 2);
			nfloat y = (rect.Height / 2) - (appearance.Image.Size.Height / 2);
			var iconPath = UIBezierPath.FromRect(new CGRect(0, 0, rect.Width, rect.Height));
			context.SaveState();
			iconPath.AddClip();
			appearance.Image.Draw(new CGRect(x, y, appearance.Image.Size.Width, appearance.Image.Size.Height));
			context.RestoreState();

		}

		/// <Docs>Set containing the touches.</Docs>
		/// <summary>
		/// Sent when one or more fingers touches the screen.
		/// </summary>
		/// <param name="touches">Touches.</param>
		/// <param name="evt">Evt.</param>
		public override void TouchesBegan(NSSet touches, UIEvent evt)
		{
			// Can this button be touched?
			if (State != ACNavBarButtonState.Enabled)
				return;

			// Take action based on the type of NavBarButton this is
			Invoke();

			base.TouchesBegan(touches, evt);
		}
		#endregion

		#region Events
		/// <summary>
		/// Occurs when touched.
		/// </summary>
		public event ACNavBarButtonActionDelegate Touched;

		/// <summary>
		/// Occurs when request new view.
		/// </summary>
		public event ACNavBarViewDelegate RequestNewView;

		/// <summary>
		/// Occurs when view hidden.
		/// </summary>
		public event ACNavBarButtonActionDelegate ViewHidden;

		/// <summary>
		/// Occurs when view disposed.
		/// </summary>
		public event ACNavBarButtonActionDelegate ViewDisposed;

		/// <summary>
		/// Occurs when state changed.
		/// </summary>
		public event ACNavBarButtonActionDelegate StateChanged;
		#endregion
	}
}
