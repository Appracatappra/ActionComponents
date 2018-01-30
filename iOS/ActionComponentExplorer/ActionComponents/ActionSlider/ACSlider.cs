using System;
using System.Drawing;

using Foundation;
using UIKit;
using CoreGraphics;


namespace ActionComponents
{
	/// <summary>
	/// <see cref="ActionComponents.ACSlider"/> is a custom slider control designed to operate like the brightness and 
	/// contrast sliders built into the iPhone Control Center. The <c>FillPercent</c> property gets or sets the percentage
	/// that the slider is filled (from 0% to 100%). If the user taps of drags in the control (from top to bottom) the 
	/// <c>FillPercent</c> will be adjusted accordingly and the <c>ValueChanged</c>,<c>Touched</c>, <c>Moved</c>, and/or 
	/// <c>Released</c> events will be raised.
	/// </summary>
	/// <remarks>The <see cref="ActionComponents.ACSlider"/> is designed to be drawn vertically and the minimum width 
	/// should not be less than 50 pixels.</remarks>
	[Register("ACSlider")]
	public class ACSlider : UIView
	{
		#region Private Variables
		private bool dragging = false;
		private CGPoint startLocation = new CGPoint(0, 0);
		private float fillPercent = 50f;
		private UIColor borderColor = UIColor.FromRGBA(104, 104, 104, 255);
		private UIColor bodyColor = UIColor.FromRGBA(157, 157, 157, 255);
		private UIColor fillColor = UIColor.White;
		private UIImage icon = null;
		#endregion

		#region Computed Properties
		/// <summary>
		/// Gets or sets the [OPTIONAL] tag that can be assosciated with this <see cref="ActionComponents.ACSlider"/>. 
		/// </summary>
		/// <value>The tag.</value>
		public Object tag { get; set; }

		/// <summary>
		/// Gets or sets the fill percent from 0% to 100%.
		/// </summary>
		/// <value>The fill percent.</value>
		public float FillPercent
		{
			get { return fillPercent; }
			set
			{
				if (value < 0f)
				{
					fillPercent = 0f;
				}
				else if (value > 100f)
				{
					fillPercent = 100f;
				}
				else
				{
					fillPercent = value;
				}
				SetNeedsDisplay();
			}
		}

		/// <summary>
		/// Gets or sets the color of the border.
		/// </summary>
		/// <value>The color of the border.</value>
		public UIColor BorderColor
		{
			get { return borderColor; }
			set
			{
				borderColor = value;
				SetNeedsDisplay();
			}
		}

		/// <summary>
		/// Gets or sets the color of the body.
		/// </summary>
		/// <value>The color of the body.</value>
		public UIColor BodyColor
		{
			get { return bodyColor; }
			set
			{
				bodyColor = value;
				SetNeedsDisplay();
			}
		}

		/// <summary>
		/// Gets or sets the color of the fill.
		/// </summary>
		/// <value>The color of the fill.</value>
		public UIColor FillColor
		{
			get { return fillColor; }
			set
			{
				fillColor = value;
				SetNeedsDisplay();
			}
		}

		/// <summary>
		/// Gets or sets the optional icon displayed at the bottom of the control.
		/// </summary>
		/// <value>The icon.</value>
		public UIImage Icon
		{
			get { return icon; }
			set
			{
				icon = value;
				SetNeedsDisplay();
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACSlider"/> class.
		/// </summary>
		public ACSlider()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACSlider"/> class.
		/// </summary>
		/// <param name="coder">Coder.</param>
		public ACSlider(NSCoder coder) : base(coder)
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACSlider"/> class.
		/// </summary>
		/// <param name="flag">Flag.</param>
		public ACSlider(NSObjectFlag flag) : base(flag)
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACSlider"/> class.
		/// </summary>
		/// <param name="bounds">Bounds.</param>
		public ACSlider(CGRect bounds) : base(bounds)
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACSlider"/> class.
		/// </summary>
		/// <param name="ptr">Ptr.</param>
		public ACSlider(IntPtr ptr) : base(ptr)
		{

		}
		#endregion

		#region Overrides
		/// <summary>
		/// Draws the slider in its current configuration.
		/// </summary>
		/// <param name="rect">The bounds of the control.</param>
		public override void Draw(CGRect rect)
		{
			base.Draw(rect);

			//// General Declarations
			var context = UIGraphics.GetCurrentContext();

			//// Variable Declarations
			var boundary = rect;
			var thumbVisible = fillPercent > 7.0f;
			var fillRadius = 10.0f;
			var fillVisible = fillPercent > 0.0f;
			var sliderHeight = rect.Height;
			var changeRatio = (sliderHeight - 4.0f) / 100.0f;
			var fillOffset = 2.0f + (100.0f - fillPercent) * changeRatio;
			var thumbOffset = fillOffset + 7.0f;
			var fillHeight = sliderHeight - 3.0f - fillOffset;
			var fillX = 2.0f;
			var fillWidth = boundary.Width - 3.0f;

			//// MainBody Drawing
			var mainBodyPath = UIBezierPath.FromRoundedRect(new CGRect(boundary.GetMinX() + NMath.Floor(boundary.Width * 0.01500f) + 0.5f, boundary.GetMinY() + NMath.Floor(boundary.Height * 0.00250f) + 0.5f, NMath.Floor(boundary.Width * 0.99500f) - NMath.Floor(boundary.Width * 0.01500f), NMath.Floor(boundary.Height * 0.99250f) - NMath.Floor(boundary.Height * 0.00250f)), 10.0f);
			bodyColor.SetFill();
			mainBodyPath.Fill();
			borderColor.SetStroke();
			mainBodyPath.LineWidth = 1.0f;
			mainBodyPath.Stroke();


			// Adjust fill parameters based on fill percent
			if (FillPercent < 3.0f)
			{
				fillRadius = 2.0f;
				fillX = 10.0f;
				fillWidth = boundary.Width - 16.0f;
			}
			else if (FillPercent < 7.0f)
			{
				fillRadius = 10.0f;
				fillX = 4.0f;
				fillWidth = boundary.Width - 7.0f;
			}

			if (fillVisible)
			{
				//// FilledArea Drawing
				var filledAreaPath = UIBezierPath.FromRoundedRect(new CGRect(fillX, fillOffset, fillWidth, fillHeight), fillRadius);
				fillColor.SetFill();
				filledAreaPath.Fill();
			}


			if (thumbVisible)
			{
				//// DragThum Drawing
				var dragThumPath = UIBezierPath.FromRoundedRect(new CGRect((boundary.Width / 2f) - 11f, thumbOffset, 22.0f, 3.5f), 1.75f);
				bodyColor.SetFill();
				dragThumPath.Fill();
				borderColor.SetStroke();
				dragThumPath.LineWidth = 1.0f;
				dragThumPath.Stroke();
			}


			//// IconFrame Drawing
			if (icon != null)
			{
				var iconFrameRect = new CGRect(boundary.GetMinX() + NMath.Floor((boundary.Width - 22.0f) * 0.51282f + 0.5f), boundary.GetMinY() + boundary.Height - 34.0f, 22.0f, 22.0f);
				var iconFramePath = UIBezierPath.FromRect(iconFrameRect);
				context.SaveState();
				iconFramePath.AddClip();
				context.TranslateCTM(NMath.Floor(iconFrameRect.GetMinX() + 0.5f), NMath.Floor(iconFrameRect.GetMinY() + 0.5f));
				context.ScaleCTM(1.0f, -1.0f);
				context.TranslateCTM(0.0f, -icon.Size.Height);
				context.DrawImage(new CGRect(0.0f, 0.0f, icon.Size.Width, icon.Size.Height), icon.CGImage);
				context.RestoreState();
			}
		}

		/// <Docs>Set containing the touches.</Docs>
		/// <summary>
		/// Sent when one or more fingers touches the screen.
		/// </summary>
		/// <param name="touches">Touches.</param>
		/// <param name="evt">Evt.</param>
		public override void TouchesBegan(NSSet touches, UIEvent evt)
		{
			//Already dragging?
			if (dragging) return;

			//Save starting location
			var pt = (touches.AnyObject as UITouch).LocationInView(this);
			startLocation = pt;

			//Inform caller of event
			RaiseTouched();

			//Pass call to base object
			if (!ExclusiveTouch)
				base.TouchesBegan(touches, evt);
		}

		/// <Docs>Set containing the touches.</Docs>
		/// <summary>
		/// Sent when the <see cref="ActionComponents.ACView"/> is being dragged
		/// </summary>
		/// <param name="touches">Touches.</param>
		/// <param name="evt">Evt.</param>
		public override void TouchesMoved(NSSet touches, UIEvent evt)
		{
			// Move relative to the original touch point
			dragging = true;
			var pt = (touches.AnyObject as UITouch).LocationInView(this);

			// Calculate new percentage
			var changeRatio = (Frame.Height - 4.0f) / 100.0f;
			FillPercent = (float)((Frame.Height - 4.0f - pt.Y) / changeRatio);

			//Inform caller of event
			RaiseValueChanged();
			RaiseMoved();

			//Pass call to base object
			if (!ExclusiveTouch)
				base.TouchesMoved(touches, evt);
		}

		/// <Docs>Set containing the touches.</Docs>
		/// <summary>
		/// Send when one or more fingers are lifted from the screen.
		/// </summary>
		/// <param name="touches">Touches.</param>
		/// <param name="evt">Evt.</param>
		public override void TouchesEnded(NSSet touches, UIEvent evt)
		{
			// Simple touch operation?
			if (!dragging)
			{
				// Calculate new percentage
				var changeRatio = (Frame.Height - 4.0f) / 100.0f;
				FillPercent = (float)((Frame.Height - 4.0f - startLocation.Y) / changeRatio);
				RaiseValueChanged();
			}

			//Clear starting point
			startLocation = new CGPoint(0, 0);
			dragging = false;

			//Inform caller of event
			RaiseReleased();

#if TRIAL
			ACToast.MakeText("ACSlider by Appracatappra, LLC.",2f).Show();
#endif

			//Pass call to base object
			if (!ExclusiveTouch)
				base.TouchesEnded(touches, evt);
		}
		#endregion

		#region Events
		/// <summary>
		/// Occurs when this <see cref="ActionComponents.ACSlider"/> is touched. 
		/// </summary>
		public delegate void ACSliderTouchedDelegate(ACSlider view);
		public event ACSliderTouchedDelegate Touched;

		/// <summary>
		/// Raises the touched event
		/// </summary>
		private void RaiseTouched()
		{
			if (this.Touched != null)
				this.Touched(this);
		}

		/// <summary>
		/// Occurs when this <see cref="ActionComponents.ACSlider"/> is moved.
		/// </summary>
		public delegate void ACSliderMovedDelegate(ACSlider view);
		public event ACSliderMovedDelegate Moved;

		/// <summary>
		/// Raises the moved event
		/// </summary>
		private void RaiseMoved()
		{
			if (this.Moved != null)
				this.Moved(this);
		}

		/// <summary>
		/// Occurs when this <see cref="ActionComponents.ACSlider"/> is released. 
		/// </summary>
		public delegate void ACSliderReleasedDelegate(ACSlider view);
		public event ACSliderReleasedDelegate Released;

		/// <summary>
		/// Raises the released.
		/// </summary>
		private void RaiseReleased()
		{
			if (this.Released != null)
				this.Released(this);
		}

		/// <summary>
		/// Occurs when this <see cref="ActionComponents.ACSlider"/> value changes.
		/// </summary>
		public delegate void ACSliderValueChanged(int fillPercent);
		public event ACSliderValueChanged ValueChanged;

		/// <summary>
		/// Raises the value changed event.
		/// </summary>
		private void RaiseValueChanged()
		{
			if (this.ValueChanged != null)
			{
				this.ValueChanged((int)FillPercent);
			}
		}
		#endregion
	}
}
