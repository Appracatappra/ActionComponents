using System;
using System.Threading;
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
using Foundation;
using CoreGraphics;
using ActionComponents;
using UIKit;

// Mappings "Unified" iOS types to Android types
using nfloat = global::System.Single;
using nint = global::System.Int32;
using nuint = global::System.UInt32;

namespace ActionComponents
{
	/// <summary>
	/// Handles the Navigation Bar that can be shown at the top of a <c>ACTileController</c>. The developer can include
	/// <c>Buttons</c> on the left and right hand sides of the bar.
	/// </summary>
	public class ACTileNavigationBar : UIView
	{
		#region Private Variables
		private ACTileController _controller;
		private ACTileNavigationBarAppearance _appearance;
		private string _title = "";
		private bool _hidden = true;
		private List<Button> _leftButtons = new List<Button>();
		private List<Button> _rightButtons = new List<Button>();
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
		public List<Button> leftButtons {
			get { return _leftButtons; }
		}

		/// <summary>
		/// Gets the right navigation bar buttons.
		/// </summary>
		/// <value>The right buttons.</value>
		public List<Button> rightButtons {
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
		public ACTileNavigationBar (Context context) : base(context)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the
		/// <c>ACTileNavigationBar</c> class.
		/// </summary>
		/// <param name="controller">Controller.</param>
		public ACTileNavigationBar (Context context, ACTileController controller) : base(context)
		{
			//Save defaults
			this._controller = controller;

			// Initialize
			Initialize ();

			// Set the initial size and location
			Frame = new CGRect(0, hidden ? -appearance.totalHeight : 0f, _controller.Frame.Width, appearance.totalHeight);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:UIKit.UIView"/> class.
		/// </summary>
		public ACTileNavigationBar() : base(Android.App.Application.Context) {
			// Init
			Initialize();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:UIKit.UIView"/> class.
		/// </summary>
		/// <param name="rect">Rect.</param>
		public ACTileNavigationBar(CGRect rect) : base(Android.App.Application.Context)
		{
			// Init
			Initialize();

			// Set initial size
			this.Frame = rect;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:UIKit.UIView"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attr">Attr.</param>
		public ACTileNavigationBar(Context context, IAttributeSet attr) : base(context,attr)
		{
			Initialize();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:UIKit.UIView"/> class.
		/// </summary>
		/// <param name="javaReference">Java reference.</param>
		/// <param name="transfer">Transfer.</param>
		public ACTileNavigationBar(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference,transfer)
		{
			Initialize();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:UIKit.UIView"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attr">Attr.</param>
		/// <param name="defStyle">Def style.</param>
		public ACTileNavigationBar(Context context, IAttributeSet attr, int defStyle) : base(context,attr,defStyle)
		{
			Initialize();
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
		public virtual void BarShownCompleted(){

			//Inform caller
			RaiseBarShown ();

		}

		/// <summary>
		/// Bars the hidden completed.
		/// </summary>
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
			foreach (Button button in leftButtons)
			{
				button.SetTextColor(appearance.titleColor);
			}

			foreach (Button button in rightButtons)
			{
				button.SetTextColor(appearance.titleColor);
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

			// Adjust height based on the device orientation.
			Frame = new CGRect(0, hidden ? -appearance.totalHeight : 0f, width, appearance.totalHeight);

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
				//UIView.BeginAnimations("ShowBar");
				//UIView.SetAnimationDuration(1f);

				//Set end of Animation handler
				//UIView.SetAnimationDelegate(this);
				//UIView.SetAnimationDidStopSelector(new Selector("BarShownCompleted"));

				//Move bar
				MoveBarTo (0);

				//Execute Animation
				//UIView.CommitAnimations();
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
				//UIView.BeginAnimations("HideBar");
				//UIView.SetAnimationDuration(1f);

				//Set end of Animation handler
				//UIView.SetAnimationDelegate(this);
				//UIView.SetAnimationDidStopSelector(new Selector("BarHiddenCompleted"));

				//Move bar
				MoveBarTo (-appearance.totalHeight);

				//Execute Animation
				//UIView.CommitAnimations();
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
		public Button AddLeftButton(string title, Bitmap icon, int width, int height, EventHandler<TouchEventArgs> action)
		{

			// Create button
			var button = new Button(this.Context);

			// Initialize
			button.Text = title;
			button.SetTextColor(appearance.titleColor);
			button.Enabled = true;
			button.Touch += action;

			//Setup initial layout position and size 
			var layout = new RelativeLayout.LayoutParams(width, height);
			layout.TopMargin = 0;
			layout.RightMargin = 0;
			button.LayoutParameters = layout;

			// Add button to self
			leftButtons.Add(button);
			AddView(button);
			SetNeedsLayout();

			// Return new button
			return button;
		}

		/// <summary>
		/// Adds the given button to the left side of the control.
		/// </summary>
		/// <param name="button">Button.</param>
		public void AddLeftButton(Button button)
		{

			// Configure button
			button.SetTextColor(appearance.titleColor);

			// Add button to self
			leftButtons.Add(button);
			AddView(button);
			SetNeedsLayout();
		}

		/// <summary>
		/// Removes the requested button from the left side of the navigation bar.
		/// </summary>
		/// <param name="n">The location of the button to remove</param>
		public void RemoveLeftButton(int n)
		{

			// Remove button
			RemoveView(leftButtons[n]);
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
		public Button AddRightButton(string title, Bitmap icon, int width, int height, EventHandler<TouchEventArgs> action)
		{

			// Create button
			var button = new Button(this.Context);

			// Initialize
			button.Text = title;
			button.SetTextColor(appearance.titleColor);
			button.Enabled = true;
			button.Touch += action;

			//Setup initial layout position and size 
			var layout = new RelativeLayout.LayoutParams(width, height);
			layout.TopMargin = 0;
			layout.RightMargin = 0;
			button.LayoutParameters = layout;

			// Add button to self
			rightButtons.Add(button);
			AddView(button);
			SetNeedsLayout();

			// Return new button
			return button;
		}

		/// <summary>
		/// Adds the given button to the right side of the navigation bar.
		/// </summary>
		/// <param name="button">Button.</param>
		public void AddRightButton(Button button)
		{

			// Configure button
			button.SetTextColor(appearance.titleColor);

			// Add button to self
			rightButtons.Add(button);
			AddView(button);
			SetNeedsLayout();
		}

		/// <summary>
		/// Removes a button from the right side of the navigation bar.
		/// </summary>
		/// <param name="n">The location of the button to remove.</param>
		public void RemoveRightButton(int n)
		{

			// Remove button
			RemoveView(rightButtons[n]);
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
			var context = UIGraphics.GetCurrentContext(this);

			//// Color Declarations
			var background = appearance.background;
			var titleColor = appearance.titleColor;

			//// Rectangle Drawing
			var rectanglePath = UIBezierPath.FromRect(rect);
			background.SetFill();
			rectanglePath.Fill();

			//// Text Drawing
			var textRect = new CGRect(0, rect.Y + appearance.topPadding, rect.Width, appearance.barHeight);;

			// Draw title
			{
				var textContent = title;
				titleColor.SetFill();
				var textStyle = new NSMutableParagraphStyle(){
					Alignment = UITextAlignment.Center,
					VerticalAlignment = TextBlockAlignment.Center
				};
				var textFontAttributes = new UIStringAttributes { Font = UIFont.SystemFontOfSize(appearance.titleSize), ForegroundColor = titleColor, ParagraphStyle = textStyle };
				context.SaveState();
				context.ClipToRect(textRect);
				new NSString(textContent).DrawString(rect, textFontAttributes);
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
			int margin = 5;
			int x = margin;
			int y = appearance.topPadding + (appearance.barHeight / 2);

			// Layout left side nav bar buttons
			foreach (Button button in leftButtons)
			{
				button.Left = x;
				button.Top = y - (button.Height / 2);
				if (button.Visibility == ViewStates.Visible)
				{
					x += 5 + button.Width;
				}
			}

			// Layout right side nav bar buttons
			x = Width - margin;
			for (int n = rightButtons.Count - 1; n >= 0; --n)
			{
				var button = rightButtons[n];
				button.Left = x - button.Width;
				button.Top = y - (button.Height / 2);
				if (button.Visibility == ViewStates.Visible)
				{
					x -= 5 + button.Width;
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

