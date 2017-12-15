using System;
using System.Drawing;

using Foundation;
using UIKit;
using CoreGraphics;

namespace ActionComponents
{
	/// <summary>
	/// Holds all information about a <see cref="ActionControls.ACAlertButton"/> attached to a <see cref="ActionControls.ACAlert"/> 
	/// </summary>
	public class ACAlertButton : UIView
	{
		#region Private variables
		private ACAlertButtonAppearance _appearance;
		private ACAlertButtonAppearance _appearanceDisabled;
		private ACAlertButtonAppearance _appearanceTouched;
		private ACAlertButtonAppearance _appearanceHighlighted;
		private ACAlert _alert;
		private string _title;
		private bool _highlighted;
		private bool _bottomRight = false;
		private bool _bottomLeft = false;
		private bool _beingTouched = false;
		private bool _enabled = true;
		#endregion 

		#region Computed Properties
		/// <summary>
		/// Gets or sets the [OPTIONAL] tag that can be assosciated with this <see cref="ActionControls.ACAlertButton"/> 
		/// </summary>
		/// <value>The tag.</value>
		public Object tag { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <see cref="ActionControls.ACAlertButton"/> is enabled.
		/// </summary>
		/// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
		public bool Enabled {
			get { return _enabled; }
			set {
				_enabled = value;
				Redraw ();
			}
		}

		/// <summary>
		/// Gets or sets the title for this <see cref="ActionControls.ACAlertButton"/> 
		/// </summary>
		/// <value>The title.</value>
		public string title{
			get { return _title;}
			set {
				//Save value and redraw
				_title = value;
				Redraw ();
			}
		}

		/// <summary>
		/// Gets or sets the appearance of this <see cref="ActionControls.ACAlertButton"/> 
		/// </summary>
		/// <value>The appearance.</value>
		public ACAlertButtonAppearance appearance {
			get { return _appearance;}
			set {
				//Save and refresh
				_appearance = value;
				Redraw ();

				//Wire-up events
				_appearance.AppearanceModified += () => {
					Redraw();
				};
			}
		}

		/// <summary>
		/// Gets or sets the appearance of this <see cref="ActionControls.ACAlertButton"/> when it is disabled
		/// </summary>
		/// <value>The appearance disabled.</value>
		public ACAlertButtonAppearance appearanceDisabled {
			get { return _appearanceDisabled;}
			set {
				//Save and refresh
				_appearanceDisabled = value;
				Redraw ();

				//Wire-up events
				_appearanceDisabled.AppearanceModified += () => {
					Redraw();
				};
			}
		}

		/// <summary>
		/// Gets or sets the touched appearance of this <see cref="ActionControls.ACAlertButton"/> 
		/// </summary>
		/// <value>The appearance touched.</value>
		public ACAlertButtonAppearance appearanceTouched {
			get { return _appearanceTouched;}
			set {
				//Save and refresh
				_appearanceTouched = value;
				Redraw ();

				//Wire-up events
				_appearanceTouched.AppearanceModified += () => {
					Redraw();
				};
			}
		}

		/// <summary>
		/// Gets or sets the highlighted appearance of this <see cref="ActionControls.ACAlertButton"/> 
		/// </summary>
		/// <value>The appearance highlighted.</value>
		public ACAlertButtonAppearance appearanceHighlighted {
			get { return _appearanceHighlighted;}
			set {
				//Save and refresh
				_appearanceHighlighted = value;
				Redraw ();

				//Wire-up events
				_appearanceHighlighted.AppearanceModified += () => {
					Redraw();
				};
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <see cref="ActionControls.ACAlertButton"/> is highlighted.
		/// </summary>
		/// <value><c>true</c> if highlighted; otherwise, <c>false</c>.</value>
		public bool highlighted {
			get { return _highlighted;}
			set {
				//Save and refresh
				_highlighted = value;
				Redraw ();
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ActionControls.ACAlertButton"/> class.
		/// </summary>
		/// <param name="alert">Alert.</param>
		/// <param name="appearance">Appearance.</param>
		/// <param name="appearanceHighlighted">Appearance highlighted.</param>
		/// <param name="title">Title.</param>
		/// <param name="highlighted">If set to <c>true</c> highlighted.</param>
		internal ACAlertButton (ACAlert alert, ACAlertButtonAppearance appearance, ACAlertButtonAppearance appearanceDisabled, ACAlertButtonAppearance appearanceTouched, ACAlertButtonAppearance appearanceHighlighted, string title, bool highlighted)
		{
			//Initialize
			this.BackgroundColor = UIColor.Clear;
			this.UserInteractionEnabled=true;
			this._alert = alert;
			this._appearance = appearance;
			this._appearanceDisabled = appearanceDisabled;
			this._appearanceTouched = appearanceTouched;
			this._appearanceHighlighted = appearanceHighlighted;
			this._title = title;
			this._highlighted = highlighted;

			//Wire-up events
			this._appearance.AppearanceModified += () => {
				Redraw();
			};

			this._appearanceHighlighted.AppearanceModified += () => {
				Redraw();
			};

		}
		#endregion 

		#region Internal Methods
		/// <summary>
		/// Sets the button position.
		/// </summary>
		/// <param name="frame">Frame.</param>
		/// <param name="bottomLeft">If set to <c>true</c> bottom left.</param>
		/// <param name="bottomRight">If set to <c>true</c> bottom right.</param>
		internal void SetButtonPosition(CGRect frame, bool bottomLeft, bool bottomRight) {

			//Save new postion and corner information
			this.Frame = frame;
			_bottomLeft = bottomLeft;
			_bottomRight = bottomRight;

			//Force a redraw
			Redraw ();

		}
		#endregion 

		#region Public Methods
		/// <summary>
		/// Causes this <see cref="ActionControls.ACAlertButton"/> to redraw itself 
		/// </summary>
		public void Redraw(){

			//Force button to refresh
			SetNeedsDisplay ();
		}

		/// <summary>
		/// Tests to see if the given x and y coordinates are inside this <see cref="ActionControls.ACAlertButton"/> 
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
		/// Test to see if the given point is inside this <see cref="ActionControls.ACAlertButton"/> 
		/// </summary>
		/// <returns><c>true</c>, if point was inside, <c>false</c> otherwise.</returns>
		/// <param name="pt">Point.</param>
		public bool PointInside(CGPoint pt){
			return PointInside (pt.X, pt.Y);
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
			var context = UIGraphics.GetCurrentContext();
			UIBezierPath buttonBodyPath;
			ACAlertButtonAppearance lookFeel;

			//Set appearance
			if (!Enabled) {
				lookFeel = appearanceDisabled;
			} else if (_beingTouched) {
				lookFeel = appearanceTouched;
			} else if (highlighted) {
				lookFeel = appearanceHighlighted;
			} else {
				lookFeel = appearance;
			}

			//// Button
			{
				//// ButtonBody Drawing
				if (_bottomLeft && _bottomRight) {
					buttonBodyPath = UIBezierPath.FromRoundedRect (rect, UIRectCorner.BottomLeft | UIRectCorner.BottomRight, new CGSize (_alert.appearance.borderRadius, _alert.appearance.borderRadius));
				} else if (_bottomLeft) {
					buttonBodyPath = UIBezierPath.FromRoundedRect (rect, UIRectCorner.BottomLeft, new CGSize (_alert.appearance.borderRadius, _alert.appearance.borderRadius));
				} else if (_bottomRight) {
					buttonBodyPath = UIBezierPath.FromRoundedRect (rect, UIRectCorner.BottomRight, new CGSize (_alert.appearance.borderRadius, _alert.appearance.borderRadius));
				} else {
					buttonBodyPath = UIBezierPath.FromRect (rect);
				}
				buttonBodyPath.ClosePath();
				lookFeel.background.SetFill();
				buttonBodyPath.Fill();
				lookFeel.border.SetStroke();
				buttonBodyPath.LineWidth = appearance.borderWidth;
				buttonBodyPath.Stroke();

				//// LeftText Drawing
				var textRect = new CGRect(0, 13.5f, rect.Width, 20f);
				lookFeel.titleColor.SetFill ();
				if (lookFeel.flat && highlighted) {
					new NSString(title).DrawString(textRect, UIFont.BoldSystemFontOfSize(lookFeel.titleSize), UILineBreakMode.MiddleTruncation, UITextAlignment.Center);
				} else {
					new NSString(title).DrawString(textRect, UIFont.SystemFontOfSize(lookFeel.titleSize), UILineBreakMode.MiddleTruncation, UITextAlignment.Center);
				}
			}
		}

		/// <Docs>Set containing the touches.</Docs>
		/// <summary>
		/// Sent when one or more fingers touches the screen.
		/// </summary>
		/// <param name="touches">Touches.</param>
		/// <param name="evt">Evt.</param>
		public override void TouchesBegan (NSSet touches, UIEvent evt)
		{
			// Is this button enabled?
			if (!Enabled)
				return;

			//Update touched style
			_beingTouched = true;
			Redraw ();

			//Inform caller of event
			RaiseTouched ();

			//Pass call to base object
			base.TouchesBegan (touches, evt);
		}
	

		/// <Docs>Set containing the touches.</Docs>
		/// <summary>
		/// Send when one or more fingers are lifted from the screen.
		/// </summary>
		/// <param name="touches">Touches.</param>
		/// <param name="evt">Evt.</param>
		public override void TouchesEnded (NSSet touches, UIEvent evt)
		{
			// Is this button enabled?
			if (!Enabled)
				return;

			//Update touched style
			_beingTouched = false;
			Redraw ();

			//Inform caller of event
			RaiseReleased ();

			//Pass call to base object
			base.TouchesEnded(touches, evt);
		} 
		#endregion

		#region Events
		/// <summary>
		/// Occurs when this <see cref="ActionControls.ACAlert"/> is touched 
		/// </summary>
		public delegate void ACAlertButtonTouchedDelegate (ACAlertButton button);
		public event ACAlertButtonTouchedDelegate Touched;

		/// <summary>
		/// Raises the touched event
		/// </summary>
		private void RaiseTouched(){
			if (this.Touched != null)
				this.Touched (this);
		}

		/// <summary>
		/// Occurs when this <see cref="ActionControls.ACAlert"/> is released 
		/// </summary>
		public delegate void ACAlertButtonReleasedDelegate (ACAlertButton button);
		public event ACAlertButtonReleasedDelegate Released;

		/// <summary>
		/// Raises the released.
		/// </summary>
		private void RaiseReleased(){
			if (this.Released != null)
				this.Released (this);
		}
		#endregion 
	}
}

