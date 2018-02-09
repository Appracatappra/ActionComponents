using System;
using System.Drawing;
using Foundation;
using UIKit;
using CoreGraphics;

namespace ActionComponents
{
	public class ACTileGroupAppearance
	{
		#region Private Properties
		private ACColor _background;
		private ACColor _border;
		private float _borderRadius;
		private ACColor _shadow;
		private bool _hasShadow = false;
		private ACColor _titleColor;
		private float _titleSize;
		private ACColor _footerColor;
		private float _footerSize;
		private bool _roundTopLeftCorner = true;
		private bool _roundTopRightCorner = true;
		private bool _roundBottomLeftCorner = true;
		private bool _roundBottomRightCorner = true;
		private float _borderWidth = 1f;
		private bool _hasBackground = false;
		#endregion 

		#region Computed Properties
		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <c>ACTileGroupAppearance</c> has background.
		/// </summary>
		/// <value><c>true</c> if has background; otherwise, <c>false</c>.</value>
		public bool hasBackground {
			get { return _hasBackground;}
			set {
				_hasBackground = value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <c>ACTileGroupAppearance</c> has shadow.
		/// </summary>
		/// <value><c>true</c> if has shadow; otherwise, <c>false</c>.</value>
		public bool hasShadow {
			get { return _hasShadow; }
			set {
				_hasShadow = value;
				RaiseAppearanceModified();
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
		/// <c>ACTileGroupAppearance</c> round top left corner.
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
		/// <c>ACTileGroupAppearance</c> round top right corner.
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
		/// <c>ACTileGroupAppearance</c> round bottom left corner.
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
		/// <c>ACTileGroupAppearance</c> round bottom rightt corner.
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
		/// <c>ACTileGroupAppearance</c> is a perfect round rect.
		/// </summary>
		/// <value><c>true</c> if is round rect; otherwise, <c>false</c>.</value>
		public bool isRoundRect {
			get {return (roundTopLeftCorner && roundTopRightCorner && roundBottomLeftCorner && roundBottomRightCorner);}
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
		/// Gets or sets a value indicating whether this
		/// <c>ACTileGroupAppearance</c> is a perfect rect.
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
		/// Gets or sets the color of the footer.
		/// </summary>
		/// <value>The color of the footer.</value>
		public ACColor footerColor{
			get{ return _footerColor;}
			set{
				_footerColor=value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets the size of the footer.
		/// </summary>
		/// <value>The size of the footer.</value>
		public float footerSize{
			get{ return _footerSize;}
			set{
				_footerSize=value;
				RaiseAppearanceModified ();
			}
		}

		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <c>ACTrayAppearance</c> class and
		/// sets the default appearance for the control
		/// </summary>
		public ACTileGroupAppearance ()
		{
			// Initilaize
			Initialize(ACColor.FromRGBA(250, 250, 250, 50), ACColor.LightGray);
		}

		/// <summary>
		/// Initializes a new instance of the <c>ACTrayAppearance</c> class with
		/// the specified user defined appearance properties
		/// </summary>
		/// <param name="background">The control's background color</param>
		/// <param name="border">The control's border color</param>
		/// <param name="shadow">The control's shadow color</param>
		public ACTileGroupAppearance(ACColor background, ACColor border){

			// Initilaize
			Initialize(background, border);

		}

		/// <summary>
		/// Initialize the specified background and border.
		/// </summary>
		/// <returns>The initialize.</returns>
		/// <param name="background">Background.</param>
		/// <param name="border">Border.</param>
		internal void Initialize(ACColor background, ACColor border) {

			//Initialize
			this._background = background;
			this._border = border;
			this._borderRadius = 10f;
			this._shadow = ACColor.FromRGBA(0.000f, 0.000f, 0.000f, 0.9f);
			this._titleColor = ACColor.White;
			this._titleSize = 18f;
			this._footerColor = ACColor.White;
			this._footerSize = 12f;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Clone this instance.
		/// </summary>
		public ACTileGroupAppearance Clone() {
			ACTileGroupAppearance appearance = new ACTileGroupAppearance ();

			//Copy over all values
			appearance.background = background;
			appearance.border = border;
			appearance.borderRadius = borderRadius;
			appearance.borderWidth = borderWidth;
			appearance.footerColor = footerColor;
			appearance.footerSize = footerSize;
			appearance.hasBackground = hasBackground;
			appearance.roundBottomLeftCorner = roundBottomLeftCorner;
			appearance.roundBottomRightCorner = roundBottomRightCorner;
			appearance.roundTopLeftCorner = roundTopLeftCorner;
			appearance.roundTopRightCorner = roundTopRightCorner;
			appearance.shadow = shadow;
			appearance.titleColor = titleColor;
			appearance.titleSize = titleSize;

			//Return new appearance
			return appearance;

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

