using System;
using Foundation;
using UIKit;
using CoreGraphics;

namespace ActionComponents
{
	/// <summary>
	/// The arrow pointer that is optionally displayed by the selected <see cref="ActionComponents.ACNavBarButton"/> of a
	/// <see cref="ActionComponents.ACNavBar"/> 
	/// </summary>
	/// <remarks>The <see cref="ActionComponents.ACNavBarPointer"/> is controlled by its parent <see cref="ActionComponents.ACNavBar"/> and
	/// should not be modified direction</remarks>
	[Register("ACNavBarPointer")]
	public class ACNavBarPointer : UIView
	{
		#region Private Variables
		/// <summary>
		/// Controlls the general appearance of the control
		/// </summary>
		private ACNavBarAppearance _appearance = new ACNavBarAppearance();
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets or sets the appearance of the pointer.
		/// </summary>
		/// <value>The appearance.</value>
		public new ACNavBarAppearance Appearance {
			get { return _appearance; }
			internal set {
				// Save value 
				_appearance = value;

				// Wireup events
				_appearance.AppearanceModified += SetNeedsDisplay;
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACNavBarPointer"/> class.
		/// </summary>
		public ACNavBarPointer()
		{
			// Initialize
			Initialize();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACNavBarPointer"/> class.
		/// </summary>
		/// <param name="coder">Coder.</param>
		public ACNavBarPointer(NSCoder coder) : base(coder)
		{
			// Initialize
			Initialize();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACNavBarPointer"/> class.
		/// </summary>
		/// <param name="flag">Flag.</param>
		public ACNavBarPointer(NSObjectFlag flag) : base(flag)
		{
			// Initialize
			Initialize();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACNavBarPointer"/> class.
		/// </summary>
		/// <param name="bounds">Bounds.</param>
		public ACNavBarPointer(CGRect bounds) : base(bounds)
		{
			// Initialize
			Initialize();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACNavBarPointer"/> class.
		/// </summary>
		/// <param name="ptr">Ptr.</param>
		public ACNavBarPointer(IntPtr ptr) : base(ptr)
		{
			// Initialize
			Initialize();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACNavBarPointer"/> class.
		/// </summary>
		/// <param name="appearance">Appearance.</param>
		public ACNavBarPointer(ACNavBarAppearance appearance)
		{
			// Initialize
			Appearance = appearance;
			Initialize();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACNavBarPointer"/> class.
		/// </summary>
		/// <param name="appearance">Appearance.</param>
		/// <param name="frame">Frame.</param>
		public ACNavBarPointer(ACNavBarAppearance appearance, CGRect frame)
		{
			// Initialize
			Appearance = appearance;
			Frame = frame;
			Initialize();
		}

		/// <summary>
		/// Initialize this instance.
		/// </summary>
		private void Initialize()
		{

			// Clear background
			BackgroundColor = UIColor.Clear;

			// Handle appearance changes
			Appearance.AppearanceModified += SetNeedsDisplay;
		}
		#endregion

		#region Internal Methods
		/// <summary>
		/// Moves the pointer to the given location.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		internal void MoveTo(nfloat x, nfloat y)
		{
			// Set new location
			Frame = new CGRect(x, y, Frame.Width, Frame.Height);
		}
		#endregion

		#region Override Methods
		/// <summary>
		/// Draws the pointer onscreen.
		/// </summary>
		/// <returns>The draw.</returns>
		/// <param name="rect">Rect.</param>
		public override void Draw(CGRect rect)
		{
			// Call base
			base.Draw(rect);

			// Initialize Drawing
			var context = UIGraphics.GetCurrentContext();

			// Shadow Declarations
			var barShadow = Appearance.Shadow.CGColor;
			var barShadowOffset = new CGSize(2.1f, -0.1f);
			var barShadowBlurRadius = 5.5f;

			// Pointer Drawing
			UIBezierPath pointerPath = new UIBezierPath();
			pointerPath.MoveTo(new CGPoint(0, -0));
			pointerPath.AddLineTo(new CGPoint(0, 24));
			pointerPath.AddLineTo(new CGPoint(13, 10.87f));
			pointerPath.AddLineTo(new CGPoint(0, -0));
			pointerPath.ClosePath();
			context.SaveState();
			if (!Appearance.Flat)
			{
				context.SetShadow(barShadowOffset, barShadowBlurRadius, barShadow);
			}
			Appearance.Border.SetFill();
			pointerPath.Fill();
			context.RestoreState();
		}
  		#endregion
	}
}
