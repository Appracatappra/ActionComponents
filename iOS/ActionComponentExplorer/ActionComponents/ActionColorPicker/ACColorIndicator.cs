using System;
using UIKit;
using CoreImage;
using CoreGraphics;
using System.Runtime.InteropServices;
using CoreGraphics;
using Foundation;

namespace ActionComponents
{
	/// <summary>
	/// Represents a color selection indicator inside of an <c>ActionColorCube</c> or <c>ActionHueBar</c>. The uses can
	/// drag the indicator around to select a color.
	/// </summary>
	[Register("ACColorIndicator")]
	public class ACColorIndicator : UIView
	{
		#region Private Variables
		private UIColor _color;
		#endregion

		#region Computed Properties
		/// <summary>
		/// Gets or sets the color.
		/// </summary>
		/// <value>The color.</value>
		public UIColor Color {
			get { return _color; }
			set {
				_color = value;
				Redraw ();
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACColorIndicator"/> class.
		/// </summary>
		public ACColorIndicator () : base()
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACColorIndicator"/> class.
		/// </summary>
		/// <param name="coder">Coder.</param>
		public ACColorIndicator (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACColorIndicator"/> class.
		/// </summary>
		/// <param name="flag">Flag.</param>
		public ACColorIndicator (NSObjectFlag flag) : base(flag)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACColorIndicator"/> class.
		/// </summary>
		/// <param name="bounds">Bounds.</param>
		public ACColorIndicator (CGRect bounds) : base(bounds)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACColorIndicator"/> class.
		/// </summary>
		/// <param name="ptr">Ptr.</param>
		public ACColorIndicator (IntPtr ptr) : base(ptr)
		{
			Initialize ();
		}

		/// <summary>
		/// Initialize this instance.
		/// </summary>
		private void Initialize(){

			// Initialize
			this.BackgroundColor = UIColor.Clear;
			this._color = UIColor.White;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Redraw this instance.
		/// </summary>
		public void Redraw() {

			// Force a redraw
			SetNeedsDisplay ();
		}
		#endregion

		#region Override Methods
		/// <summary>
		/// Draw the specified rect.
		/// </summary>
		/// <returns>The draw.</returns>
		/// <param name="rect">Rect.</param>
		public override void Draw (CGRect rect)
		{
			base.Draw (rect);

			// Get current context
			CGContext context = UIGraphics.GetCurrentContext();

			// Calculate 
			CGPoint center= new CGPoint (Bounds.GetMidX (), Bounds.GetMidY ());
			nfloat radius = Bounds.GetMidX ();

			// Fill pointer
			context.SetFillColor (Color.CGColor);
			context.AddArc(center.X, center.Y, radius - 1.0f, 0.0f, 2.0f * (float) Math.PI,true);
			context.DrawPath (CGPathDrawingMode.Fill);

			// Outline in black
			context.SetStrokeColor (UIColor.Black.CGColor);
			context.AddArc(center.X, center.Y, radius - 1.0f, 0.0f, 2.0f * (float) Math.PI,true);
			context.SetLineWidth (2);
			context.DrawPath (CGPathDrawingMode.Stroke);

			// Outline in white
			context.SetStrokeColor (UIColor.White.CGColor);
			context.AddArc(center.X, center.Y, radius - 2.0f, 0.0f, 2.0f * (float) Math.PI,true);
			context.SetLineWidth (2);
			context.DrawPath (CGPathDrawingMode.Stroke);
		}
		#endregion
	}
}

