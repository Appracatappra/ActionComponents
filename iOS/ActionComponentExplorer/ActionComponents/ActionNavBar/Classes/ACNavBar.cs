using System;
using System.ComponentModel;
using Foundation;
using UIKit;
using CoreGraphics;

namespace ActionComponents
{
	/// <summary>
	/// A left-side, icon based, customizable navigation strip and view controller
	/// </summary>
	/// <remarks>The <see cref="ActionComponents.ACNavBar"/> has three <see cref="ActionComponents.ACNavBarButtonCollection"/>s (top, middle and bottom) that
	/// <see cref="ActionComponents.ACNavBarButton"/>s can be added to. Several different <see cref="ActionComponents.ACNavBarButtonType"/>s can be created that provide
	/// automatic control of attached <c>UIView</c>s to simple touchable buttons to notification icons. The <see cref="ActionComponents.ACNavBar"/> appearance
	/// can be adjusted using <see cref="ActionComponents.ACNavBarAppearance"/> and <see cref="ActionComponents.ACNavBarButtonAppearance"/> properties. </remarks>
	[Register("ACNavBar")]
	public class ACNavBar : UIView
	{
		#region Private Variables
		private ACNavBarAppearance _appearance = new ACNavBarAppearance();
		private bool _suspendUpdates = false;
		private ACNavBarButtonCollection _top;
		private ACNavBarButtonCollection _middle;
		private ACNavBarButtonCollection _bottom;
		private ACNavBarButtonAppearance _appearanceEnabled;
		private ACNavBarButtonAppearance _appearanceDisabled;
		private ACNavBarButtonAppearance _appearanceSelected;
		#endregion

		#region Public Properties
		/// <summary>
		/// [OPTIONAL] Tag to hold user information about this collection
		/// </summary>
		public object tag;
		#endregion

		#region Computed Properties
		public ACNavBarPointer Pointer { get; internal set; }

		/// <summary>
		/// Controlls the general appearance of the control
		/// </summary>
		public new ACNavBarAppearance Appearance
		{
			get { return _appearance; }
			set
			{
				// Save value
				_appearance = value;

				// Pass change to pointer
				Pointer.Appearance = _appearance;

				// Wireup events
				_appearance.AppearanceModified += SetNeedsDisplay;
			}
		}

		/// <summary>
		/// Gets or sets the button appearance enabled values
		/// </summary>
		/// <value>The button appearance enabled values</value>
		/// <remarks>Any changes to this appearance will cascade to all <see cref="ActionComponents.ACNavBarButtonCollection"/>s and 
		/// all <see cref="ActionComponents.ACNavBarButton"/>s in those collections.</remarks>
		public ACNavBarButtonAppearance ButtonAppearanceEnabled
		{
			get { return _appearanceEnabled; }
			set
			{
				// Save values
				_appearanceEnabled = value;

				// Wireup events
				_appearanceEnabled.AppearanceModified += () =>
				{
					CascadeAppearanceChanges(_appearanceEnabled, ACNavBarButtonState.Enabled);
				};

				// Apply change
				CascadeAppearanceChanges(_appearanceEnabled, ACNavBarButtonState.Enabled);
			}
		}

		/// <summary>
		/// Gets or sets the button appearance disabled values
		/// </summary>
		/// <value>The button appearance disabled values</value>
		/// <remarks>Any changes to this appearance will cascade to all <see cref="ActionComponents.ACNavBarButtonCollection"/>s and 
		/// all <see cref="ActionComponents.ACNavBarButton"/>s in those collections.</remarks>
		public ACNavBarButtonAppearance ButtonAppearanceDisabled
		{
			get { return _appearanceDisabled; }
			set
			{
				// Save values
				_appearanceDisabled = value;

				// Wireup events
				_appearanceDisabled.AppearanceModified += () =>
				{
					CascadeAppearanceChanges(_appearanceDisabled, ACNavBarButtonState.Disabled);
				};

				// Apply change
				CascadeAppearanceChanges(_appearanceDisabled, ACNavBarButtonState.Disabled);
			}
		}

		/// <summary>
		/// Gets or sets the button appearance selected values
		/// </summary>
		/// <value>The button appearance selected values</value>
		/// <remarks>Any changes to this appearance will cascade to all <see cref="ActionComponents.ACNavBarButtonCollection"/>s and 
		/// all <see cref="ActionComponents.ACNavBarButton"/>s in those collections.</remarks>
		public ACNavBarButtonAppearance ButtonAppearanceSelected
		{
			get { return _appearanceSelected; }
			set
			{
				// Save values
				_appearanceSelected = value;

				// Wireup events
				_appearanceSelected.AppearanceModified += () =>
				{
					CascadeAppearanceChanges(_appearanceSelected, ACNavBarButtonState.Selected);
				};

				// Apply change
				CascadeAppearanceChanges(_appearanceSelected, ACNavBarButtonState.Selected);
			}
		}

		/// <summary>
		/// Gets the top <see cref="ActionComponents.ACNavBarButtonCollection"/> of <see cref="ActionComponents.ACNavBarButton"/>s
		/// </summary>
		/// <value>The top collection</value>
		/// <remarks>This is the master <see cref="ActionComponents.ACNavBarButtonCollection"/>. The first button added to this collection will
		/// automatically be the selected button. Its <c>UIView</c> will be displayed and the <see cref="ActionComponents.ACNavBarPointer"/> will be
		/// moved into position beside this button. NOTE: You need to call the <c>DisplayDefaultView</c> method of the <see cref="ActionComponents.ACNavBar"/> to cause the 
		/// first view to be correctly displayed after the NavBar has been populated with buttons.</remarks>
		public ACNavBarButtonCollection Top
		{
			get { return _top; }
			set
			{
				// Save value
				_top = value;

				// Set location
				_top.MoveTo(0, 20f);

				// Wireup events
				_top.PointerPositionChanged += MovePointer;
				_top.CollectionModified += (collection) => {
					SetNeedsLayout();
				};
			}
		}

		/// <summary>
		/// Gets the middle <see cref="ActionComponents.ACNavBarButtonCollection"/> of <see cref="ActionComponents.ACNavBarButton"/>s 
		/// </summary>
		/// <value>The middle collection</value>
		/// <remarks>The middle <see cref="ActionComponents.ACNavBarButtonCollection"/> is usually reserved for <c>Tool</c> <see cref="ActionComponents.ACNavBarButtonType"/>s of
		/// buttons that act on the currently selected <c>UIView</c> </remarks>
		public ACNavBarButtonCollection Middle
		{
			get { return _middle; }
			set
			{
				// Save value
				_middle = value;

				// Wireup events
				_middle.PointerPositionChanged += MovePointer;
				_middle.CollectionModified += (collection) =>
				{
					SetNeedsLayout();
				};
			}
		}

		/// <summary>
		/// Gets the bottom <see cref="ActionComponents.ACNavBarButtonCollection"/> of <see cref="ActionComponents.ACNavBarButton"/>s 
		/// </summary>
		/// <value>The bottom collection</value>
		/// <remarks>The bottom <see cref="ActionComponents.ACNavBarButtonCollection"/> is usually reserved for <c>Settings</c> and <c>Notification</c>
		/// <see cref="ActionComponents.ACNavBarButtonType"/>s of buttons.</remarks>
		public ACNavBarButtonCollection Bottom
		{
			get { return _bottom; }
			set
			{
				// Save value
				_bottom = value;

				// Wireup events
				_bottom.PointerPositionChanged += MovePointer;
				_bottom.CollectionModified += (collection) =>
				{
					SetNeedsLayout();
				};
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="ActionComponents.ACNavBar"/> pointer is hidden.
		/// </summary>
		/// <value><c>true</c> if pointer hidden; otherwise, <c>false</c>.</value>
		[Export("PointerHiden"), Browsable(true)]
		public bool PointerHidden
		{
			get { return Pointer.Hidden; }
			set { Pointer.Hidden = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="T:ActionComponents.ACNavBar"/> has suspend updates.
		/// </summary>
		/// <value><c>true</c> if suspend updates; otherwise, <c>false</c>.</value>
		public bool SuspendUpdates {
			get { return _suspendUpdates; }
			set {
				_suspendUpdates = value;

				// Cascade change to all button collections
				Top.SuspendUpdates = _suspendUpdates;
				Middle.SuspendUpdates = _suspendUpdates;
				Bottom.SuspendUpdates = _suspendUpdates;
			}
		}

		/// <summary>
		/// Gets or sets the selected button.
		/// </summary>
		/// <value>The selected button.</value>
		public ACNavBarButton SelectedButton
		{
			get;
			internal set;
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACNavBar"/> class.
		/// </summary>
		public ACNavBar()
		{
			// Initialise
			Initialize();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACNavBar"/> class.
		/// </summary>
		/// <param name="coder">Coder.</param>
		public ACNavBar(NSCoder coder) : base(coder)
		{
			// Initialise
			Initialize();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACNavBar"/> class.
		/// </summary>
		/// <param name="flag">Flag.</param>
		public ACNavBar(NSObjectFlag flag) : base(flag)
		{
			// Initialise
			Initialize();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACNavBar"/> class.
		/// </summary>
		/// <param name="bounds">Bounds.</param>
		public ACNavBar(CGRect bounds) : base (bounds)
		{
			// Initialise
			Initialize();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACNavBar"/> class.
		/// </summary>
		/// <param name="ptr">Ptr.</param>
		public ACNavBar(IntPtr ptr) : base(ptr)
		{
			// Initialise
			Initialize();
		}

		/// <summary>
		/// Initialize this instance.
		/// </summary>
		internal void Initialize()
		{
			//Clear background
			this.BackgroundColor = UIColor.Clear;

			// Create a pointer and display at the default location
			Pointer = new ACNavBarPointer(Appearance, new CGRect(64, -30, 18, 24));
			AddSubview(Pointer);

			// Automatically enable touch events
			UserInteractionEnabled = true;
			MultipleTouchEnabled = true;

			// Create button collection and add to view
			Top = new ACNavBarButtonCollection(this, 20f);
			AddSubview(Top);

			Middle = new ACNavBarButtonCollection(this, (Frame.Height / 2f) - 42f);
			AddSubview(Middle);

			Bottom = new ACNavBarButtonCollection(this, (Frame.Height - 2f) - 42f);
			AddSubview(Bottom);

			//Set default appearances
			ButtonAppearanceEnabled = new ACNavBarButtonAppearance ();

			ButtonAppearanceDisabled = new ACNavBarButtonAppearance ()
			{
				Alpha = 0.5f
						};

			ButtonAppearanceSelected = new ACNavBarButtonAppearance();

		}
		#endregion

		#region Internal Methods
		/// <summary>
		/// Cascades the given appearance changes to button in all groups (Top, Middle, Bottom) for the given state.
		/// </summary>
		/// <param name="appearance">The new apparance.</param>
		/// <param name="state">The state that's being changed.</param>
		internal void CascadeAppearanceChanges(ACNavBarButtonAppearance appearance, ACNavBarButtonState state)
		{

			// Take action based on the element being copied
			switch (state)
			{
				case ACNavBarButtonState.Enabled:
					Top.ButtonAppearanceEnabled.CopyAppearance(appearance, true);
					Middle.ButtonAppearanceEnabled.CopyAppearance(appearance, true);
					Bottom.ButtonAppearanceEnabled.CopyAppearance(appearance, true);
					break;
				case ACNavBarButtonState.Disabled:
					Top.ButtonAppearanceDisabled.CopyAppearance(appearance, true);
					Middle.ButtonAppearanceDisabled.CopyAppearance(appearance, true);
					Bottom.ButtonAppearanceDisabled.CopyAppearance(appearance, true);
					break;
				case ACNavBarButtonState.Selected:
					Top.ButtonAppearanceSelected.CopyAppearance(appearance, true);
					Middle.ButtonAppearanceSelected.CopyAppearance(appearance, true);
					Bottom.ButtonAppearanceSelected.CopyAppearance(appearance, true);
					break;
			}

			// Cause this collection to redraw
			SetNeedsDisplay();
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Moves the pointer.
		/// </summary>
		/// <param name="y">The y coordinate.</param>
		private void MovePointer(nfloat y)
		{

			//Define Animation
			UIView.BeginAnimations("MovePointer");
			UIView.SetAnimationDuration(0.5f);

			//Adjust location
			Pointer.MoveTo(64, y);

			//Execute Animation
			UIView.CommitAnimations();

#if TRIAL
			ACToast.MakeText("ACNavBar by Appracatappra, LLC.", ACToastLength.Medium).Show();
#else
			AppracatappraLicenseManager.ValidateLicense();
#endif
		}
		#endregion

		#region Override Methods
		/// <summary>
		/// Draw the specified rect.
		/// </summary>
		/// <param name="rect">Rect.</param>
		public override void Draw(CGRect rect)
		{
			// Call base
			base.Draw(rect);

			// General Declarations
			var context = UIGraphics.GetCurrentContext();

			// Shadow Declarations
			var barShadow = Appearance.Shadow.CGColor;
			var barShadowOffset = new CGSize(2.1f, -0.1f);
			var barShadowBlurRadius = 5.5f;

			var toolstripPath = UIBezierPath.FromRect(new CGRect(0, 0, 64, rect.Height));
			context.SaveState();
			if (!Appearance.Flat)
			{
				context.SetShadow(barShadowOffset, barShadowBlurRadius, barShadow);
			}
			Appearance.Background.SetFill();
			toolstripPath.Fill();
			context.RestoreState();

			// ToolBorder Drawing
			var toolBorderPath = UIBezierPath.FromRect(new CGRect(62, 0, 2, rect.Height));
			Appearance.Border.SetFill();
			toolBorderPath.Fill();
		}

		/// <summary>
		/// Layouts the subviews.
		/// </summary>
		public override void LayoutSubviews()
		{
			base.LayoutSubviews();

			// Adjust the button collections to view correctly
			UIView.BeginAnimations("MoveCollections");
			UIView.SetAnimationDuration(0.5f);

			//Move the middle collection into position
			Middle.MoveTo(0, (Frame.Height / 2f) - (Middle.Frame.Height / 2f));
			Bottom.MoveTo(0, Frame.Height - 2f - Bottom.Frame.Height);

			//Execute Animation
			UIView.CommitAnimations();
		}
  		#endregion
	}
}
