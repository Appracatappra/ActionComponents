using System;
using System.Drawing;

using Foundation;
using UIKit;
using CoreGraphics;

namespace ActionComponents
{
	/// <summary>
	/// Controls the appearance of a <see cref="ActionComponents.ACAlertButton"/> 
	/// </summary>
	public class ACAlertButtonAppearance
	{
		#region Private Properties
		private UIColor _background;
		private UIColor _border;
		private UIColor _titleColor;
		private float _titleSize;
		private float _borderWidth = 2f;
		private bool _flat=false;
		#endregion 

		#region Computed Properties
		/// <summary>
		/// Gets or sets the background color
		/// </summary>
		/// <value>The background color</value>
		public UIColor background {
			get{ return _background;}
			set{
				_background = value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets the border color
		/// </summary>
		/// <value>The border color</value>
		public UIColor border{
			get { return _border;}
			set {
				_border=value;
				RaiseAppearanceModified();
			}
		}

		/// <summary>
		/// Gets or sets the width of the border.
		/// </summary>
		/// <value>The width of the border.</value>
		public float borderWidth{
			get { return _borderWidth;}
			set {
				_borderWidth=value;
				RaiseAppearanceModified();
			}
		}

		/// <summary>
		/// Gets or sets the color of the title.
		/// </summary>
		/// <value>The color of the title.</value>
		public UIColor titleColor{
			get{ return _titleColor;}
			set{
				_titleColor=value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets the size of the title.
		/// </summary>
		/// <value>The size of the title.</value>
		public float titleSize{
			get{ return _titleSize;}
			set{
				_titleSize=value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <see cref="ActionComponents.ACAlertButtonAppearance"/> is flat.
		/// </summary>
		/// <value><c>true</c> if flat; otherwise, <c>false</c>.</value>
		/// <remarks>This value was added to support the iOS 7 degisn language</remarks>
		public bool flat{
			get{ return _flat;}
			set{
				_flat = value;
				RaiseAppearanceModified ();
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Appracatappra.ActionComponents.ActionTray.ACTrayAppearance"/> class and
		/// sets the default appearance for the control
		/// </summary>
		public ACAlertButtonAppearance ()
		{
			//Initialize default colors
			this._background = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 1.000f);
			this._border = UIColor.FromRGBA(0.328f, 0.328f, 0.328f, 1.000f);
			this._titleColor = UIColor.White;
			this._titleSize = 14f;

			//Are we running on an iOS 7 device?
			if (!iOSDevice.isIOS6) {
				//Yes, automatically flatten the appearance.
				Flatten ();
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Appracatappra.ActionComponents.ActionTray.ACTrayAppearance"/> class with
		/// the specified user defined appearance properties
		/// </summary>
		/// <param name="background">The control's background color</param>
		/// <param name="border">The control's border color</param>
		public ACAlertButtonAppearance(UIColor background, UIColor border){

			//Initialize
			this._background = background;
			this._border = border;
			this._titleColor = UIColor.White;
			this._titleSize = 14f;

			//Are we running on an iOS 7 device?
			if (!iOSDevice.isIOS6) {
				//Yes, automatically flatten the appearance.
				Flatten (background,border);
			}
		}

		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="ActionComponents.ACAlertButtonAppearance"/> class.
		/// </summary>
		/// <param name="background">Background.</param>
		/// <param name="border">Border.</param>
		/// <param name="textColor">Text color.</param>
		public ACAlertButtonAppearance(UIColor background, UIColor border, UIColor textColor){

			//Initialize
			this._background = background;
			this._border = border;
			this._titleColor = textColor;
			this._titleSize = 14f;

			//Are we running on an iOS 7 device?
			if (!iOSDevice.isIOS6) {
				//Yes, automatically flatten the appearance.
				Flatten (background,border,textColor);
			}
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Flattens the <see cref="Appracatappra.ActionComponents.ActionTray.ACTrayAppearance"/> to match the new iOS 7 design language
		/// </summary>
		public void Flatten(){

			//Set to the default flat style
			_flat = true;
			_background = UIColor.FromRGB(234,233,232);
			_border = UIColor.FromRGB(182,182,181);
			_borderWidth = 0.5f;
			this._titleColor = UIColor.FromRGB(0,122,255);

			//Inform caller of the appearance change
			RaiseAppearanceModified ();

		}

		/// <summary>
		/// Flattens the <see cref="Appracatappra.ActionComponents.ActionTray.ACTrayAppearance"/> to match the new iOS 7 design language
		/// </summary>
		/// <param name="backgroundColor">Background color.</param>
		public void Flatten(UIColor backgroundColor){

			//Set to the default flat style
			_flat = true;
			_background = backgroundColor;
			_border = UIColor.FromRGB(182,182,181);
			_borderWidth = 0.5f;
			this._titleColor = UIColor.FromRGB(0,122,255);

			//Inform caller of the appearance change
			RaiseAppearanceModified ();

		}

		/// <summary>
		/// Flattens the <see cref="Appracatappra.ActionComponents.ActionTray.ACTrayAppearance"/> to match the new iOS 7 design language
		/// </summary>
		/// <param name="backgroundColor">Background color.</param>
		/// <param name="borderColor">Border color.</param>
		public void Flatten(UIColor backgroundColor, UIColor borderColor){

			//Set to the default flat style
			_flat = true;
			_background = backgroundColor;
			_border = borderColor;
			_borderWidth = 0.5f;
			this._titleColor = UIColor.FromRGB(0,122,255);

			//Inform caller of the appearance change
			RaiseAppearanceModified ();

		}

		/// <summary>
		/// Flattens the <see cref="Appracatappra.ActionComponents.ActionTray.ACTrayAppearance"/> to match the new iOS 7 design language
		/// </summary>
		/// <param name="backgroundColor">Background color.</param>
		/// <param name="borderColor">Border color.</param>
		/// <param name="textColor">Text color.</param>
		public void Flatten(UIColor backgroundColor, UIColor borderColor, UIColor textColor){

			//Set to the default flat style
			_flat = true;
			_background = backgroundColor;
			_border = borderColor;
			_borderWidth = 0.5f;
			this._titleColor = textColor;

			//Inform caller of the appearance change
			RaiseAppearanceModified ();

		}
		#endregion

		#region Events
		/// <summary>
		/// Occurs when the appearance is modified.
		/// </summary>
		internal delegate void AppearanceModifiedDelegate();
		internal event AppearanceModifiedDelegate AppearanceModified;

		/// <summary>
		/// Raises the appearance modified event
		/// </summary>
		/// <remarks>Used to inform the attached component that an appearance 
		/// attribute has been modified</remarks>
		private void RaiseAppearanceModified(){
			//Inform caller that the appearance has changed
			if (this.AppearanceModified != null)
				this.AppearanceModified ();
		}
		#endregion
	}
}

