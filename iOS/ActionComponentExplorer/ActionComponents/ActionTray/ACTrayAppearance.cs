using System;
using System.Drawing;

using Foundation;
using UIKit;
using CoreGraphics;
using ObjCRuntime;

namespace ActionComponents
{
	/// <summary>
	/// Controls the appearance of a <see cref="ActionComponents.ACTray"/> with properties such as 
	/// background color, border color, and shadow color.
	/// </summary>
	/// <remarks>The <see cref="ActionComponents.ACTrayAppearance"/> also controls the appearance of the
	/// <see cref="ActionComponents.ACTray"/>'s <c>dragTab</c>.</remarks>
	public class ACTrayAppearance
	{
		#region Private Properties
		private UIColor _background;
		private UIColor _border;
		private UIColor _shadow;
		private UIColor _frame;
		private UIColor _thumbBackground;
		private UIColor _thumbBorder;
		private UIColor _gripBackground;
		private UIColor _gripBorder;
		private CGBlendMode _thumbBlendMode;
		private nfloat _tabAplha;
		private UIColor _titleColor;
		private nfloat _titleSize;
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
		/// Gets or sets the shadow color
		/// </summary>
		/// <value>The shadow color</value>
		public UIColor shadow{
			get{ return _shadow;}
			set{
				_shadow=value;
				RaiseAppearanceModified();
			}
		}

		/// <summary>
		/// Gets or sets the frame color
		/// </summary>
		/// <value>The frame color</value>
		public UIColor frame{
			get{ return _frame;}
			set{
				_frame=value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets the thumb background.
		/// </summary>
		/// <value>The thumb background.</value>
		public UIColor thumbBackground {
			get{ return _thumbBackground;}
			set{
				_thumbBackground=value;
				RaiseAppearanceModified();
			}
		}
		
		/// <summary>
		/// Gets or sets the thumb border.
		/// </summary>
		/// <value>The thumb border.</value>
		public UIColor thumbBorder{
			get{ return _thumbBorder;}
			set{
				_thumbBorder=value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets the thumb aplha.
		/// </summary>
		/// <value>The thumb aplha.</value>
		public nfloat tabAlpha{
			get{return _tabAplha;}
			set{
				//Save value
				_tabAplha=value;

				//Adjust other colors to match
				_thumbBackground=_thumbBackground.ColorWithAlpha(value);
				_thumbBorder=_thumbBorder.ColorWithAlpha(value);
				_gripBackground=_gripBackground.ColorWithAlpha(value);
				_gripBorder=_gripBorder.ColorWithAlpha(value);
				_titleColor=_titleColor.ColorWithAlpha (value);

				RaiseAppearanceModified();
			}
		}
		
		/// <summary>
		/// Gets or sets the grip background.
		/// </summary>
		/// <value>The grip background.</value>
		public UIColor gripBackground{
			get{ return _gripBackground;}
			set{
				_gripBackground=value;
				RaiseAppearanceModified ();
			}
		}
		
		/// <summary>
		/// Gets or sets the grip border.
		/// </summary>
		/// <value>The grip border.</value>
		public UIColor gripBorder{
			get{ return _gripBorder;}
			set{
				_gripBorder=value;
				RaiseAppearanceModified ();
			}
		}
		
		/// <summary>
		/// Gets or sets the thumb blend mode.
		/// </summary>
		/// <value>The thumb blend mode.</value>
		public CGBlendMode thumbBlendMode{
			get{ return _thumbBlendMode;}
			set{
				_thumbBlendMode=value;
				RaiseAppearanceModified ();
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
		public nfloat titleSize{
			get{ return _titleSize;}
			set{
				_titleSize=value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <see cref="ActionComponents.ACTrayAppearance"/> is flat.
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
		/// Initializes a new instance of the <see cref="ActionComponents.ACTrayAppearance"/> class and
		/// sets the default appearance for the control
		/// </summary>
		public ACTrayAppearance ()
		{
			//Initialize default colors
			this._background = UIColor.FromRGBA(0.629f, 0.629f, 0.629f, 1.000f);
			this._border = UIColor.FromRGBA(0.456f, 0.456f, 0.456f, 1.000f);
			this._shadow = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 1.000f);
			this._frame = UIColor.FromRGBA(0.831f, 0.831f, 0.831f, 1.000f);
			this._thumbBackground = UIColor.FromRGBA(0.858f, 0.858f, 0.858f, 1.000f);
			this._thumbBorder=UIColor.FromRGBA(0.726f, 0.726f, 0.726f, 1.000f);
			this._gripBackground = UIColor.FromRGBA(0.721f, 0.721f, 0.721f, 1.000f);
			this._gripBorder = UIColor.FromRGBA(0.597f, 0.597f, 0.597f, 1.000f);
			this._thumbBlendMode = CGBlendMode.Multiply;
			this._tabAplha=1.0f;
			this._titleColor = UIColor.White;
			this._titleSize = 12f;

			//Is this running on iOS 7 or greater?
			if (!iOSDevice.isIOS6) {
				//Yes, switch to iOS 7 styling
				Flatten ();
			}
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACTrayAppearance"/> class with
		/// the specified user defined appearance properties
		/// </summary>
		/// <param name="background">The control's background color</param>
		/// <param name="border">The control's border color</param>
		/// <param name="shadow">The control's shadow color</param>
		public ACTrayAppearance(UIColor background, UIColor border, UIColor shadow){
			
			//Initialize
			this._background = background;
			this._border = border;
			this._shadow = shadow;

			//Is this running on iOS 7 or greater?
			if (!iOSDevice.isIOS6) {
				//Yes, switch to iOS 7 styling
				Flatten (background);
			}
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Flattens the <see cref="ActionComponents.ACTrayAppearance"/> to match the new iOS 7 design language
		/// </summary>
		public void Flatten(){

			//Set to the default flat style
			_flat = true;
			_background = UIColor.FromRGB(200, 200, 200);
			_border = _background;
			_frame = _background;
			_thumbBackground = _background;
			_thumbBorder = _background;
			_gripBackground = UIColor.White;
			_gripBorder = _gripBackground;
			this._thumbBlendMode = CGBlendMode.Normal;
			this._titleColor = UIColor.White;
			this._titleSize = 12f;

			//Inform caller of the appearance change
			RaiseAppearanceModified ();

		}

		/// <summary>
		/// Flattens the <see cref="ActionComponents.ACTrayAppearance"/> to match the new iOS 7 design language
		/// </summary>
		/// <param name="backgroundColor">Background color.</param>
		public void Flatten(UIColor backgroundColor){

			//Set to the default flat style
			_flat = true;
			_background = backgroundColor;
			_border = backgroundColor;
			_frame = _background;
			_thumbBackground = _background;
			_thumbBorder = _background;
			_gripBackground = UIColor.White;
			_gripBorder = _gripBackground;
			this._thumbBlendMode = CGBlendMode.Normal;
			this._titleColor = UIColor.White;
			this._titleSize = 12f;

			//Inform caller of the appearance change
			RaiseAppearanceModified ();

		}

		/// <summary>
		/// Flattens the <see cref="ActionComponents.ACTrayAppearance"/> to match the new iOS 7 design language
		/// </summary>
		/// <param name="backgroundColor">Background color.</param>
		/// <param name="foregroundColor">Foreground color.</param>
		public void Flatten(UIColor backgroundColor, UIColor foregroundColor){

			//Set to the default flat style
			_flat = true;
			_background = backgroundColor;
			_border = backgroundColor;
			_frame = _background;
			_thumbBackground = _background;
			_thumbBorder = _background;
			_gripBackground = foregroundColor;
			_gripBorder = _gripBackground;
			this._thumbBlendMode = CGBlendMode.Normal;
			this._titleColor = foregroundColor;
			this._titleSize = 12f;

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

