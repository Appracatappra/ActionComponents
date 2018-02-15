using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using Foundation;
using UIKit;
using CoreGraphics;
using ObjCRuntime;

namespace ActionComponents
{
	/// <summary>
	/// Handles the Navigation Bar that can be shown at the top of a <c>ACTileController</c>. The developer can include
	/// <c>Buttons</c> on the left and right hand sides of the bar.
	/// </summary>
	[Register("ACTileNavigationBar")]
	public class ACTileNavigationBar : UIView
	{
		#region Private Variables
		private ACTileController _controller;
		private ACTileNavigationBarAppearance _appearance;
		private string _title = "";
		private bool _hidden = true;
		private List<UIButton> _leftButtons = new List<UIButton>();
		private List<UIButton> _rightButtons = new List<UIButton>();
		#endregion

		#region Computed Properties
		/// <summary>
		/// Gets or sets the [OPTIONAL] tag that can be assosciated with this <c>ACTileNavigationBar</c> 
		/// </summary>
		/// <value>The tag.</value>
		public Object tag { get; set; }

		/// <summary>
		/// Gets the left navigation bar buttons.
		/// </summary>
		/// <value>The left buttons.</value>
		public List<UIButton> leftButtons {
			get { return _leftButtons; }
		}

		/// <summary>
		/// Gets the right navigation bar buttons.
		/// </summary>
		/// <value>The right buttons.</value>
		public List<UIButton> rightButtons {
			get { return _rightButtons; }
		}

		/// <summary>
		/// Gets or sets the <c>ACTileNavigationBarAppearance</c> for this 
		/// <c>ACTileNavigationBar</c>  
		/// </summary>
		/// <value>The appearance.</value>
		public ACTileNavigationBarAppearance appearance{
			get { return _appearance;}
			set {
				_appearance = value;

				//Wire-up events
				_appearance.AppearanceModified += () => {
					Redraw();
				};

				//Force an update
				Redraw ();
			}
		}

		/// <summary>
		/// Gets or sets the title for this <c>ACTileNavigationBar</c> 
		/// </summary>
		/// <value>The title.</value>
		public string title{
			get { return _title;}
			set {
				_title = value;
				Redraw ();
			}
		}

		/// <summary>
		/// Gets a value indicating whether this
		/// <c>ACTileNavigationBar</c> is hidden.
		/// </summary>
		/// <value><c>true</c> if hidden; otherwise, <c>false</c>.</value>
		public bool hidden {
			get { return _hidden;}
		}

		#endregion 

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <c>ACTileNavigationBar</c> class.
		/// </summary>
		public ACTileNavigationBar () : base()
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the
		/// <c>ACTileNavigationBar</c> class.
		/// </summary>
		/// <param name="controller">Controller.</param>
		public ACTileNavigationBar (ACTileController controller) : base()
		{
			//Save defaults
			this._controller = controller;

			// Initialize
			Initialize ();

			// Yes, adjust height based on the device orientation.
			switch (iOSDevice.currentDeviceOrientation)
			{
				case UIInterfaceOrientation.LandscapeLeft:
				case UIInterfaceOrientation.LandscapeRight:
					Frame = new CGRect(0, hidden ? -appearance.barHeight : 0f, _controller.Frame.Width, appearance.barHeight);
					break;
				default:
					Frame = new CGRect(0, hidden ? -appearance.totalHeight : 0f, _controller.Frame.Width, appearance.totalHeight);
					break;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <c>ACTileNavigationBar</c> class.
		/// </summary>
		/// <param name="coder">Coder.</param>
		public ACTileNavigationBar (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <c>ACTileNavigationBar</c> class.
		/// </summary>
		/// <param name="flag">Flag.</param>
		public ACTileNavigationBar (NSObjectFlag flag) : base(flag)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <c>ACTileNavigationBar</c> class.
		/// </summary>
		/// <param name="bounds">Bounds.</param>
		public ACTileNavigationBar (RectangleF bounds) : base(bounds)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <c>ACTileNavigationBar</c> class.
		/// </summary>
		/// <param name="ptr">Ptr.</param>
		public ACTileNavigationBar (IntPtr ptr) : base(ptr)
		{
			Initialize ();
		}

		/// <summary>
		/// Initialize this instance.
		/// </summary>
		private void Initialize(){

			//Set defaults
			this.BackgroundColor = ACColor.Clear;
			this.appearance = new ACTileNavigationBarAppearance ();
			this.UserInteractionEnabled = true;
			this.MultipleTouchEnabled = true;

			//Wire-up appearance events
			_appearance.AppearanceModified += () => {
				Redraw();
			};
		}
		#endregion 

		#region Private Methods
		/// <summary>
		/// Moves the <c>ACTileNavigationBar</c> to the given y location
		/// </summary>
		/// <param name="y">The y coordinate.</param>
		private void MoveBarTo(float y) {

			//Adjust the bar's position
			this.Frame = new CGRect (this.Frame.Left, y, this.Frame.Width, this.Frame.Height);
		}

		/// <summary>
		/// Bars the shown completed.
		/// </summary>
		[Export("BarShownCompleted")]
		public virtual void BarShownCompleted(){

			//Inform caller
			RaiseBarShown ();

		}

		/// <summary>
		/// Bars the hidden completed.
		/// </summary>
		[Export("BarHiddenCompleted")]
		public virtual void BarHiddenCompleted(){

			//Inform caller
			RaiseBarHidden ();

		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Forces this <c>ACTileNavigationBar</c> to fully redraw itself
		/// </summary>
		public void Redraw() {

			// Update buttons
			foreach(UIButton button in leftButtons) {
				button.TintColor = appearance.titleColor;
			}

			foreach (UIButton button in rightButtons)
			{
				button.TintColor = appearance.titleColor;
			}

			//Force component to update view
			this.SetNeedsDisplay ();
		}

		/// <summary>
		/// Resize to the specified width.
		/// </summary>
		/// <returns>The resize.</returns>
		/// <param name="width">Width.</param>
		public void Resize(nfloat width) {

			// Yes, adjust height based on the device orientation.
			switch (iOSDevice.currentDeviceOrientation)
			{
				case UIInterfaceOrientation.LandscapeLeft:
				case UIInterfaceOrientation.LandscapeRight:
					Frame = new CGRect(0, hidden ? -appearance.barHeight : 0f, width, appearance.barHeight);
					break;
				default:
					Frame = new CGRect(0, hidden ? -appearance.totalHeight : 0f, width, appearance.totalHeight);
					break;
			}

			// Redraw bar to take the size change into account.
			Redraw();
		}

		/// <summary>
		/// Shows the <c>ACTileNavigationBar</c> 
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public void ShowNavigationBar(bool animated) {

			//Anything to do?
			if (_hidden == false)
				return;

			//Should the move be animated?
			if (animated) {
				//Define Animation
				UIView.BeginAnimations("ShowBar");
				UIView.SetAnimationDuration(1f);

				//Set end of Animation handler
				UIView.SetAnimationDelegate(this);
				UIView.SetAnimationDidStopSelector(new Selector("BarShownCompleted"));

				//Move bar
				MoveBarTo (0);

				//Execute Animation
				UIView.CommitAnimations();
			} else {
				//Move bar
				MoveBarTo (0);
			}

			//Mark bar as visible
			_hidden = false;
		}

		/// <summary>
		/// Hides the <c>ACTileNavigationBar</c> 
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public void HideNavigationBar(bool animated) {

			//Anything to do?
			if (_hidden == true)
				return;

			//Should the move be animated?
			if (animated) {
				//Define Animation
				UIView.BeginAnimations("HideBar");
				UIView.SetAnimationDuration(1f);

				//Set end of Animation handler
				UIView.SetAnimationDelegate(this);
				UIView.SetAnimationDidStopSelector(new Selector("BarHiddenCompleted"));

				//Move bar
				MoveBarTo (-appearance.totalHeight);

				//Execute Animation
				UIView.CommitAnimations();
			} else {
				//Move bar
				MoveBarTo (-appearance.totalHeight);
			}

			//Mark bar as hidden
			_hidden = true;
		}

		/// <summary>
		/// Adds a new button to the left side of the navigation bar.
		/// </summary>
		/// <returns>The left button.</returns>
		/// <param name="title">Title.</param>
		/// <param name="icon">Icon.</param>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		/// <param name="action">Action.</param>
		public UIButton AddLeftButton(string title, UIImage icon, nfloat width, nfloat height, EventHandler action){

			// Create button
			var button = UIButton.FromType(UIButtonType.RoundedRect);

			// Initialize
			button.SetTitle(title, UIControlState.Normal);
			button.SetImage(icon, UIControlState.Normal);
			button.Frame = new CGRect(0, 0, width, height);
			button.TintColor = appearance.titleColor;
			button.TouchUpInside += action;
			button.Enabled = true;

			// Add button to self
			leftButtons.Add(button);
			AddSubview(button);
			SetNeedsLayout();

			// Return new button
			return button;
		}

		/// <summary>
		/// Adds the given button to the left side of the control.
		/// </summary>
		/// <param name="button">Button.</param>
		public void AddLeftButton(UIButton button) {

			// Configure button
			button.TintColor = appearance.titleColor;

			// Add button to self
			leftButtons.Add(button);
			AddSubview(button);
			SetNeedsLayout();
		}

		/// <summary>
		/// Removes the requested button from the left side of the navigation bar.
		/// </summary>
		/// <param name="n">The location of the button to remove</param>
		public void RemoveLeftButton(int n){

			// Remove button
			leftButtons[n].RemoveFromSuperview();
			leftButtons.RemoveAt(n);
			SetNeedsLayout();
		}

		/// <summary>
		/// Adds a button to the right side of the navigation bar.
		/// </summary>
		/// <returns>The right button.</returns>
		/// <param name="title">Title.</param>
		/// <param name="icon">Icon.</param>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		/// <param name="action">Action.</param>
		public UIButton AddRightButton(string title, UIImage icon, nfloat width, nfloat height, EventHandler action)
		{

			// Create button
			var button = UIButton.FromType(UIButtonType.RoundedRect);

			// Initialize
			button.SetTitle(title, UIControlState.Normal);
			button.SetImage(icon, UIControlState.Normal);
			button.Frame = new CGRect(0, 0, width, height);
			button.TintColor = appearance.titleColor;
			button.TouchUpInside += action;
			button.Enabled = true;

			// Add button to self
			rightButtons.Add(button);
			AddSubview(button);
			SetNeedsLayout();

			// Return new button
			return button;
		}

		/// <summary>
		/// Adds the given button to the right side of the navigation bar.
		/// </summary>
		/// <param name="button">Button.</param>
		public void AddRightButton(UIButton button)
		{

			// Configure button
			button.TintColor = appearance.titleColor;

			// Add button to self
			rightButtons.Add(button);
			AddSubview(button);
			SetNeedsLayout();
		}

		/// <summary>
		/// Removes a button from the right side of the navigation bar.
		/// </summary>
		/// <param name="n">The location of the button to remove.</param>
		public void RemoveRightButton(int n)
		{

			// Remove button
			rightButtons[n].RemoveFromSuperview();
			rightButtons.RemoveAt(n);
			SetNeedsLayout();
		}
		#endregion

		#region Override Methods
		/// <summary>
		/// Draw the specified rect.
		/// </summary>
		/// <param name="rect">Rect.</param>
		public override void Draw (CGRect rect)
		{
			base.Draw (rect);

			//// General Declarations
			var context = UIGraphics.GetCurrentContext();

			//// Color Declarations
			var background = appearance.background;
			var titleColor = appearance.titleColor;

			//// Rectangle Drawing
			var rectanglePath = UIBezierPath.FromRect(rect);
			background.SetFill();
			rectanglePath.Fill();

			//// Text Drawing
			var textRect = rect;

			// Yes, adjust height based on the device orientation.
			switch (iOSDevice.currentDeviceOrientation)
			{
				case UIInterfaceOrientation.LandscapeLeft:
				case UIInterfaceOrientation.LandscapeRight:
					textRect = new CGRect(0, rect.Y, rect.Width, appearance.barHeight);
					break;
				default:
					textRect = new CGRect(0, rect.Y + appearance.topPadding, rect.Width, appearance.barHeight);
					break;
			}

			// Draw title
			{
				var textContent = title;
				titleColor.SetFill();
				var textStyle = new NSMutableParagraphStyle();
				textStyle.Alignment = UITextAlignment.Center;
				var textFontAttributes = new UIStringAttributes { Font = UIFont.SystemFontOfSize(appearance.titleSize), ForegroundColor = titleColor, ParagraphStyle = textStyle };
				var textTextHeight = new NSString(textContent).GetBoundingRect(new CGSize(textRect.Width, nfloat.MaxValue), NSStringDrawingOptions.UsesLineFragmentOrigin, textFontAttributes, null).Height;
				context.SaveState();
				context.ClipToRect(textRect);
				new NSString(textContent).DrawString(new CGRect(textRect.GetMinX(), textRect.GetMinY() + (textRect.Height - textTextHeight) / 2.0f, textRect.Width, textTextHeight), textFontAttributes);
				context.RestoreState();
			}

		}

		/// <summary>
		/// Layouts the subviews.
		/// </summary>
		public override void LayoutSubviews()
		{
			base.LayoutSubviews();

			// Set initial states
			nfloat margin = (iOSDevice.DeviceType == AppleHardwareType.iPhoneX) ? 15f : 5f;
			nfloat x = margin;
			nfloat y = appearance.topPadding + (appearance.barHeight / 2);

			// Yes, adjust height based on the device orientation.
			switch (iOSDevice.currentDeviceOrientation)
			{
				case UIInterfaceOrientation.LandscapeLeft:
				case UIInterfaceOrientation.LandscapeRight:
					y = (appearance.barHeight / 2);
					break;
				default:
					y = appearance.topPadding + (appearance.barHeight / 2);
					break;
			}

			// Layout left side nav bar buttons
			foreach(UIButton button in leftButtons) {
				button.Frame = new CGRect(x, y - (button.Frame.Height / 2f), button.Frame.Width, button.Frame.Height);
				if (!button.Hidden)
				{
					x += 5f + button.Frame.Width;
				}
			}

			// Layout right side nav bar buttons
			x = Frame.Width - margin;
			for (int n = rightButtons.Count - 1; n >= 0; --n){
				var button = rightButtons[n];
				button.Frame = new CGRect(x - button.Frame.Width, y - (button.Frame.Height / 2f), button.Frame.Width, button.Frame.Height);
				if (!button.Hidden)
				{
					x -= 5f + button.Frame.Width;
				}
			}
		}
		#endregion 

		#region Events
		/// <summary>
		/// Occurs when this <c>ACTileNavigationBar</c> is shown 
		/// </summary>
		public delegate void ACTileNavigationBarShownDelegate (ACTileNavigationBar navigationBar);
		public event ACTileNavigationBarShownDelegate BarShown;

		/// <summary>
		/// Raises the BarShown event
		/// </summary>
		private void RaiseBarShown(){
			if (this.BarShown != null)
				this.BarShown (this);
		}

		/// <summary>
		/// Occurs when the <c>ACTileNavigationBar</c>  hidden.
		/// </summary>
		public delegate void ACTileNavigationBarHiddenDelegate (ACTileNavigationBar navigationBar);
		public event ACTileNavigationBarHiddenDelegate BarHidden;

		/// <summary>
		/// Raises the BarHidden event
		/// </summary>
		private void RaiseBarHidden(){
			if (this.BarHidden != null)
				this.BarHidden (this);
		}
		#endregion 
	}
}

