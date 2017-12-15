using System;
using System.Drawing;

using Foundation;
using UIKit;
using CoreGraphics;

namespace ActionComponents
{
	public class ACAlertAppearance
	{
		#region Private Properties
		private UIColor _background;
		private UIColor _border;
		private float _borderRadius;
		private UIColor _shadow;
		private UIColor _titleColor;
		private float _titleSize;
		private UIColor _descriptionColor;
		private float _descriptionSize;
		private bool _roundTopLeftCorner = true;
		private bool _roundTopRightCorner = true;
		private bool _roundBottomLeftCorner = true;
		private bool _roundBottomRightCorner = true;
		private UIColor _overlay;
		private float _overlayAlpha;
		private float _borderWidth = 2f;
		private UIColor _activityIndicatorColor = UIColor.White;
		private bool _flat=false;
		#endregion 

		#region Computed Properties
		/// <summary>
		/// Gets or sets the color of the activity indicator when running under iOS 7 or greater
		/// </summary>
		/// <value>The color of the activity indicator.</value>
		public UIColor activityIndicatorColor{
			get { return _activityIndicatorColor;}
			set {
				_activityIndicatorColor=value;
				RaiseAppearanceModified();
			}
		}

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
		/// Gets or sets the border radius.
		/// </summary>
		/// <value>The border radius.</value>
		public float borderRadius{
			get { return _borderRadius;}
			set {
				_borderRadius = value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <see cref="ActionComponents.ACAlertAppearance"/> round top left corner.
		/// </summary>
		/// <value><c>true</c> if round top left corner; otherwise, <c>false</c>.</value>
		public bool roundTopLeftCorner {
			get { return _roundTopLeftCorner;}
			set {
				_roundTopLeftCorner = value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <see cref="ActionComponents.ACAlertAppearance"/> round top right corner.
		/// </summary>
		/// <value><c>true</c> if round top right corner; otherwise, <c>false</c>.</value>
		public bool roundTopRightCorner {
			get { return _roundTopRightCorner;}
			set {
				_roundTopRightCorner = value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <see cref="ActionComponents.ACAlertAppearance"/> round bottom left corner.
		/// </summary>
		/// <value><c>true</c> if round bottom left corner; otherwise, <c>false</c>.</value>
		public bool roundBottomLeftCorner {
			get { return _roundBottomLeftCorner;}
			set {
				_roundBottomLeftCorner = value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <see cref="ActionComponents.ACAlertAppearance"/> round bottom rightt corner.
		/// </summary>
		/// <value><c>true</c> if round bottom rightt corner; otherwise, <c>false</c>.</value>
		public bool roundBottomRightCorner {
			get { return _roundBottomRightCorner;}
			set {
				_roundBottomRightCorner = value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets a value indicating whether this
		/// <see cref="ActionComponents.ACAlertAppearance"/> is a perfect round rect.
		/// </summary>
		/// <value><c>true</c> if is round rect; otherwise, <c>false</c>.</value>
		public bool isRoundRect {
			get {return (roundTopLeftCorner && roundTopRightCorner && roundBottomLeftCorner && roundBottomRightCorner);}
		}

		/// <summary>
		/// Gets a value indicating whether this
		/// <see cref="ActionComponents.ACAlertAppearance"/> is a perfect rect.
		/// </summary>
		/// <value><c>true</c> if is rect; otherwise, <c>false</c>.</value>
		public bool isRect {
			get {return (!roundTopLeftCorner && !roundTopRightCorner && !roundBottomLeftCorner && !roundBottomRightCorner);}
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
		/// Gets or sets the color of the description.
		/// </summary>
		/// <value>The color of the description.</value>
		public UIColor descriptionColor{
			get{ return _descriptionColor;}
			set{
				_descriptionColor=value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets the size of the description.
		/// </summary>
		/// <value>The size of the description.</value>
		public float descriptionSize{
			get{ return _descriptionSize;}
			set{
				_descriptionSize=value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets the overlay color
		/// </summary>
		/// <value>The overlay.</value>
		public UIColor overlay{
			get { return _overlay;}
			set {
				_overlay = value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets the overlay alpha.
		/// </summary>
		/// <value>The overlay alpha.</value>
		public float overlayAlpha{
			get { return _overlayAlpha;}
			set {
				_overlayAlpha = value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <see cref="Appracatappra.ActionComponents.ActionTray.ACTrayAppearance"/> is flat.
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
		public ACAlertAppearance ()
		{
			//Initialize default colors
			this._background = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 1.000f);
			this._border = UIColor.FromRGBA(0.328f, 0.328f, 0.328f, 1.000f);
			this._borderRadius = 17f;
			this._shadow = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 0.9f);
			this._titleColor = UIColor.White;
			this._titleSize = 14f;
			this._descriptionColor = UIColor.White;
			this._descriptionSize = 12f;
			this._overlay = UIColor.Black;
			this._overlayAlpha = 0.5f;

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
		/// <param name="shadow">The control's shadow color</param>
		public ACAlertAppearance(UIColor background, UIColor border){

			//Initialize
			this._background = background;
			this._border = border;
			this._borderRadius = 17f;
			this._shadow = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 0.9f);
			this._titleColor = UIColor.White;
			this._titleSize = 14f;
			this._descriptionColor = UIColor.White;
			this._descriptionSize = 12f;
			this._overlay = UIColor.Black;
			this._overlayAlpha = 0.5f;

			//Are we running on an iOS 7 device?
			if (!iOSDevice.isIOS6) {
				//Yes, automatically flatten the appearance.
				Flatten (background,border);
			}
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Makes the <see cref="ActionComponents.ACAlert"/> rectangular
		/// </summary>
		public void MakeRectangular() {

			//Flatten all corners
			_roundTopLeftCorner = false;
			_roundTopRightCorner = false;
			_roundBottomLeftCorner = false;
			_roundBottomRightCorner = false;

			//Inform caller of the appearance change
			RaiseAppearanceModified ();
		}

		/// <summary>
		/// Makes the <see cref="ActionComponents.ACAlert"/> round rectangle.
		/// </summary>
		public void MakeRoundRectangle() {

			//Flatten all corners
			_roundTopLeftCorner = true;
			_roundTopRightCorner = true;
			_roundBottomLeftCorner = true;
			_roundBottomRightCorner = true;

			//Inform caller of the appearance change
			RaiseAppearanceModified ();
		}

		/// <summary>
		/// Flattens the <see cref="ActionComponents.ACAlert"/> to match the new iOS 7 design language
		/// </summary>
		public void Flatten(){

			//Set to the default flat style
			_flat = true;
			_background = UIColor.FromRGB(234,233,232);
			_border = UIColor.FromRGB(232,231,230); //UIColor.FromRGB(182,128,181);
			_shadow = UIColor.Clear;
			_borderWidth = 0.5f;
			_borderRadius = 8f;
			_titleColor = UIColor.Black;
			_descriptionColor = _titleColor;
			_activityIndicatorColor = _descriptionColor;

			//Inform caller of the appearance change
			RaiseAppearanceModified ();

		}

		/// <summary>
		/// Flattens the <see cref="ActionComponents.ACAlert"/> to match the new iOS 7 design language
		/// </summary>
		/// <param name="backgroundColor">Background color.</param>
		public void Flatten(UIColor backgroundColor){

			//Set to the default flat style
			_flat = true;
			_background = backgroundColor;
			_border = UIColor.FromRGB(232,231,230);
			_shadow = UIColor.Clear;
			_borderWidth = 0.5f;
			_borderRadius = 8f;
			_titleColor = UIColor.Black;
			_descriptionColor = _titleColor;
			_activityIndicatorColor = _descriptionColor;

			//Inform caller of the appearance change
			RaiseAppearanceModified ();

		}

		/// <summary>
		/// Flattens the <see cref="ActionComponents.ACAlert"/> to match the new iOS 7 design language
		/// </summary>
		/// <param name="backgroundColor">Background color.</param>
		/// <param name="foregroundColor">Foreground color.</param>
		public void Flatten(UIColor backgroundColor, UIColor foregroundColor){

			//Set to the default flat style
			_flat = true;
			_background = backgroundColor;
			_border = UIColor.FromRGB(232,231,230);
			_shadow = UIColor.Clear;
			_borderWidth = 0.5f;
			_borderRadius = 8f;
			_titleColor = foregroundColor;
			_descriptionColor = _titleColor;
			_activityIndicatorColor = _descriptionColor;

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

