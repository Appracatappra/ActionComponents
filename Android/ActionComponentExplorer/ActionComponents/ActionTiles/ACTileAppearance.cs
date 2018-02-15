using System;
using Android.Graphics;

namespace ActionComponents
{
	/// <summary>
	/// Defines the customizable properties of an <c>ACTile</c> that control it's look and feel.
	/// </summary>
	public class ACTileAppearance
	{
		#region Private Properties
		private ACColor _background;
		private ACColor _border;
		private float _borderRadius;
		private ACColor _shadow;
		private bool _hasShadow = false;
		private ACColor _titleColor;
		private float _titleSize;
		private ACColor _subtitleColor;
		private float _subtitleSize;
		private ACColor _titleBackground;
		private ACColor _descriptionColor;
		private float _descriptionSize;
		private bool _roundTopLeftCorner = true;
		private bool _roundTopRightCorner = true;
		private bool _roundBottomLeftCorner = true;
		private bool _roundBottomRightCorner = true;
		private float _borderWidth = 2f;
		private bool _autoSetTextColor = false;
		#endregion 

		#region Computed Properties
		/// <summary>
		/// Gets or sets a value indicating whether this appearance auto sets text color based on the background color.
		/// </summary>
		/// <value><c>true</c> if auto set text color; otherwise, <c>false</c>.</value>
		public bool autoSetTextColor {
			get { return _autoSetTextColor; }
			set {
				_autoSetTextColor = value;
			}
		}

		/// <summary>
		/// Gets or sets the background color
		/// </summary>
		/// <value>The background color</value>
		public ACColor background {
			get{ return _background;}
			set{
				_background = value;
				if (autoSetTextColor) CalculateTextColorsForBackground();
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets the border color
		/// </summary>
		/// <value>The border color</value>
		public ACColor border{
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
		/// <c>ACTileAppearance</c> round top left corner.
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
		/// <c>ACTileAppearance</c> round top right corner.
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
		/// <c>ACTileAppearance</c> round bottom left corner.
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
		/// <c>ACTileAppearance</c> round bottom rightt corner.
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
		/// Gets or sets a value indicating whether this
		/// <c>ACTileAppearance</c> is a perfect round rect.
		/// </summary>
		/// <value><c>true</c> if is round rect; otherwise, <c>false</c>.</value>
		public bool isRoundRect {
			get {return (roundTopLeftCorner && roundTopRightCorner && roundBottomLeftCorner && roundBottomRightCorner);}
			set {
				_roundTopLeftCorner = value;
				_roundTopRightCorner = value;
				_roundBottomLeftCorner = value;
				_roundBottomRightCorner = value;
				RaiseAppearanceModified();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <c>ACTileAppearance</c> is a perfect rect.
		/// </summary>
		/// <value><c>true</c> if is rect; otherwise, <c>false</c>.</value>
		public bool isRect {
			get {return (!roundTopLeftCorner && !roundTopRightCorner && !roundBottomLeftCorner && !roundBottomRightCorner);}
			set
			{
				_roundTopLeftCorner = value;
				_roundTopRightCorner = value;
				_roundBottomLeftCorner = value;
				_roundBottomRightCorner = value;
				RaiseAppearanceModified();
			}
		}

		/// <summary>
		/// Gets or sets the shadow color
		/// </summary>
		/// <value>The shadow color</value>
		public ACColor shadow{
			get{ return _shadow;}
			set{
				_shadow=value;
				RaiseAppearanceModified();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <c>ACTileAppearance</c> has shadow.
		/// </summary>
		/// <value><c>true</c> if has shadow; otherwise, <c>false</c>.</value>
		public bool hasShadow{
			get { return _hasShadow;}
			set {
				_hasShadow = value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets the color of the title.
		/// </summary>
		/// <value>The color of the title.</value>
		public ACColor titleColor{
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
		/// Gets or sets the color of the subtitle.
		/// </summary>
		/// <value>The color of the subtitle.</value>
		public ACColor subtitleColor{
			get{ return _subtitleColor;}
			set{
				_subtitleColor=value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets the size of the subtitle.
		/// </summary>
		/// <value>The size of the subtitle.</value>
		public float subtitleSize{
			get{ return _subtitleSize;}
			set{
				_subtitleSize=value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets the title background.
		/// </summary>
		/// <value>The title background.</value>
		public ACColor titleBackground {
			get { return _titleBackground;}
			set {
				_titleBackground = value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets the color of the description.
		/// </summary>
		/// <value>The color of the description.</value>
		public ACColor descriptionColor{
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

		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Appracatappra.ActionComponents.ActionTray.ACTrayAppearance"/> class and
		/// sets the default appearance for the control
		/// </summary>
		public ACTileAppearance ()
		{
			//Initialize default colors
			Initialize(ACColor.FromRGBA(213, 213, 213, 240), ACColor.Clear);

		}

		/// <summary>
		/// Initializes a new instance of the <c>ACTileAppearance</c> class class and
		/// sets the default appearance for the control
		/// </summary>
		/// <param name="background">Background.</param>
		public ACTileAppearance (ACColor background)
		{
			//Initialize default colors
			Initialize(background, ACColor.Clear);

		}

		/// <summary>
		/// Initializes a new instance of the ACTrayAppearance class with
		/// the specified user defined appearance properties
		/// </summary>
		/// <param name="background">The control's background color</param>
		/// <param name="border">The control's border color</param>
		/// <param name="shadow">The control's shadow color</param>
		public ACTileAppearance(ACColor background, ACColor border){

			//Initialize
			Initialize(background, border);
		}

		/// <summary>
		/// Initialize the specified background and border.
		/// </summary>
		/// <returns>The initialize.</returns>
		/// <param name="background">Background.</param>
		/// <param name="border">Border.</param>
		internal void Initialize(ACColor background, ACColor border) {

			this._background = background;
			this._border = border;
			this._borderRadius = 10f;
			this._shadow = ACColor.FromRGBA(0.000f, 0.000f, 0.000f, 0.9f);
			this._titleColor = ACColor.FromRGB(50, 50, 50);
			this._titleSize = 10f;
			this._subtitleColor = ACColor.White;
			this._subtitleSize = 18f;
			this._titleBackground = ACColor.Clear;
			this._descriptionColor = ACColor.FromRGB(100, 100, 100);
			this._descriptionSize = 12f;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Clone this instance.
		/// </summary>
		public ACTileAppearance Clone(){
			ACTileAppearance appearance = new ACTileAppearance ();

			//Copy over all properties
			appearance.background = background;
			appearance.border = border;
			appearance.borderRadius = borderRadius;
			appearance.borderWidth = borderWidth;
			appearance.descriptionColor = descriptionColor;
			appearance.descriptionSize = descriptionSize;
			appearance.hasShadow = hasShadow;
			appearance.roundBottomLeftCorner = roundBottomLeftCorner;
			appearance.roundBottomRightCorner = roundBottomRightCorner;
			appearance.roundTopLeftCorner = roundTopLeftCorner;
			appearance.roundTopRightCorner = roundTopRightCorner;
			appearance.shadow = shadow;
			appearance.subtitleColor = subtitleColor;
			appearance.subtitleSize = subtitleSize;
			appearance.titleBackground = titleBackground;
			appearance.titleColor = titleColor;
			appearance.titleSize = titleSize;

			//Return new appearance
			return appearance;
		}

		/// <summary>
		/// Calculates the text colors for background.
		/// </summary>
		public void CalculateTextColorsForBackground() {
			float hue = 0, saturation = 0, brightness = 0, alpha = 0;

			// Breakdown input color
			background.GetHSBA(ref hue, ref saturation, ref brightness, ref alpha);

			// Pick colors based on brightness
			if (brightness < 0.5f) {
				_titleColor = ACColor.White;
				_subtitleColor = ACColor.White;
				_descriptionColor = ACColor.White;
			} else {
				_titleColor = ACColor.Black;
				_subtitleColor = ACColor.Black;
				_descriptionColor = ACColor.Black;
			}
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

