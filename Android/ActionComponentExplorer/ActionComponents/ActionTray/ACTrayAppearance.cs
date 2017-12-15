using System;
using Android.Graphics;

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
		private Color _background;
		private Color _border;
		private Color _shadow;
		private Color _frame;
		private Color _thumbBackground;
		private Color _thumbBorder;
		private Color _gripBackground;
		private Color _gripBorder;
		private int _thumbAlpha;
		private Color _titleColor;
		private float _titleSize;
		private int _tabAlpha;
		private bool _flat=false;
		#endregion 
		
		#region Computed Properties
		/// <summary>
		/// Gets or sets the background color
		/// </summary>
		/// <value>The background color</value>
		public Color background {
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
		public Color border{
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
		public Color shadow{
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
		public Color frame{
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
		public Color thumbBackground {
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
		public Color thumbBorder{
			get{ return _thumbBorder;}
			set{
				_thumbBorder=value;
				RaiseAppearanceModified ();
			}
		}
		
		/// <summary>
		/// Gets or sets the grip background.
		/// </summary>
		/// <value>The grip background.</value>
		public Color gripBackground{
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
		public Color gripBorder{
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
		public int thumbAlpha{
			get{ return _thumbAlpha;}
			set{
				_thumbAlpha=value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets the tab alpha.
		/// </summary>
		/// <value>The tab alpha.</value>
		public int tabAlpha{
			get{return _tabAlpha;}
			set{
				//Save value
				_tabAlpha=value;

				//Adjust other colors too
				_thumbAlpha=value;
				_titleColor.A=(byte)value;

				RaiseAppearanceModified();
			}
		}
		
		/// <summary>
		/// Gets or sets the color of the title.
		/// </summary>
		/// <value>The color of the title.</value>
		public Color titleColor{
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
		/// <see cref="ActionComponents.ACTrayAppearance"/> is flat.
		/// </summary>
		/// <value><c>true</c> if flat; otherwise, <c>false</c>.</value>
		/// <remarks>This value was added to support the iOS 7 design language</remarks>
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
			this._background=Color.Argb(255,62,62,62);
			this._border=Color.Argb(255,45,45,45);
			this._shadow=Color.Argb(50,0,0,0);
			this._frame=Color.Argb(255,83,83,83);
			this._thumbBackground=Color.Argb(255,85,85,85);
			this._thumbBorder=Color.Argb (255,72,72,72);
			this._thumbAlpha=128;
			this._gripBackground=Color.Argb (100,72,72,72);
			this._gripBorder=Color.Argb(100,59,59,59);
			this._titleColor=Color.White;
			this._titleSize=12f;
			this._tabAlpha=255;
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACTrayAppearance"/> class with
		/// the specified user defined appearance properties
		/// </summary>
		/// <param name="background">The control's background color</param>
		/// <param name="border">The control's border color</param>
		/// <param name="shadow">The control's shadow color</param>
		public ACTrayAppearance(Color background, Color border, Color shadow){
			
			//Initialize
			this._background = background;
			this._border = border;
			this._shadow = shadow;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Flattens the <see cref="ActionComponents.ACTrayAppearance"/> to match the new iOS 7 design language
		/// </summary>
		public void Flatten(){

			//Set to the default flat style
			_flat = true;
			_background = Color.Rgb(200, 200, 200);
			_shadow=Color.Argb(0,0,0,0);
			_border = _background;
			_frame = _background;
			_thumbBackground = _background;
			_thumbBorder = _background;
			_gripBackground = Color.White;
			_gripBorder = _gripBackground;
			this._titleColor = Color.White;
			this._titleSize = 12f;

			//Inform caller of the appearance change
			RaiseAppearanceModified ();

		}

		/// <summary>
		/// Flattens the <see cref="ActionComponents.ACTrayAppearance"/> to match the new iOS 7 design language
		/// </summary>
		/// <param name="backgroundColor">Background color.</param>
		public void Flatten(Color backgroundColor){

			//Set to the default flat style
			_flat = true;
			_background = backgroundColor;
			_shadow=Color.Argb(0,0,0,0);
			_border = backgroundColor;
			_frame = _background;
			_thumbBackground = _background;
			_thumbBorder = _background;
			_gripBackground = Color.White;
			_gripBorder = _gripBackground;
			this._titleColor = Color.White;
			this._titleSize = 12f;

			//Inform caller of the appearance change
			RaiseAppearanceModified ();

		}

		/// <summary>
		/// Flattens the <see cref="ActionComponents.ACTrayAppearance"/> to match the new iOS 7 design language
		/// </summary>
		/// <param name="backgroundColor">Background color.</param>
		/// <param name="foregroundColor">Foreground color.</param>
		public void Flatten(Color backgroundColor, Color foregroundColor){

			//Set to the default flat style
			_flat = true;
			_background = backgroundColor;
			_shadow=Color.Argb(0,0,0,0);
			_border = backgroundColor;
			_frame = _background;
			_thumbBackground = _background;
			_thumbBorder = _background;
			_gripBackground = foregroundColor;
			_gripBorder = _gripBackground;
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

