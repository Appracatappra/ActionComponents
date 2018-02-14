using System;
using Android.Graphics;

namespace ActionComponents
{
	public class ACTileNavigationBarAppearance
	{
		#region Private Properties
		private ACColor _background;
		private ACColor _darkBackground;
		private ACColor _titleColor;
		private int _titleSize;
		private int _barHeight = 50;
		private int _topPadding = 20;
		private bool _autoDarkenBackground = true;
		internal bool _darkened = false;
		#endregion 

		#region Computed Properties
		/// <summary>
		/// Gets or sets the background color
		/// </summary>
		/// <value>The background color</value>
		public ACColor background {
			get{ return _darkened ? _darkBackground : _background;}
			set{
				_background = value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets the dark background.
		/// </summary>
		/// <value>The dark background.</value>
		public ACColor darkBackground {
			get { return _darkBackground; }
			set {
				_darkBackground = value;
				RaiseAppearanceModified();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the <c>ACTileNavigationBar</c> will automatically
		/// darken background when the tiles scroll under it.
		/// </summary>
		/// <value><c>true</c> if auto darken background; otherwise, <c>false</c>.</value>
		public bool autoDarkenBackground {
			get { return _autoDarkenBackground; }
			set { _autoDarkenBackground = value; }
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
		public int titleSize{
			get{ return _titleSize;}
			set{
				_titleSize=value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets the height of the bar.
		/// </summary>
		/// <value>The height of the bar.</value>
		public int barHeight {
			get { return _barHeight; }
			set {
				if (value < 32)  {
					_barHeight = 32;
				} else {
					_barHeight = value;
				}
				RaiseAppearanceModified();
			}
		}

		/// <summary>
		/// Gets or sets the top padding that is applied to the navigation bar.
		/// </summary>
		/// <value>The top padding.</value>
		public int topPadding {
			get { return _topPadding; }
			set {
				_topPadding = value;
				RaiseAppearanceModified();
			}
		}

		/// <summary>
		/// Gets the total height of the navigation bar.
		/// </summary>
		/// <value>The total height as the <c>topPadding</c> plus the <c>barHeight</c>.</value>
		public int totalHeight {
			get { return topPadding + barHeight; }
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACTileNavigationBarAppearance"/> class.
		/// </summary>
		public ACTileNavigationBarAppearance ()
		{
			// Initialize default colors
			Initialize(ACColor.FromRGBA(213, 213, 213, 100), ACColor.FromRGBA(50, 50, 50, 240), ACColor.White);

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACTileNavigationBarAppearance"/> class.
		/// </summary>
		/// <param name="background">Background.</param>
		/// <param name="darkBackground">the dark background color.</param>
		public ACTileNavigationBarAppearance(ACColor background, ACColor darkBackground){

			// Initialize default colors
			Initialize(background, darkBackground, ACColor.White);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACTileNavigationBarAppearance"/> class.
		/// </summary>
		/// <param name="background">Background.</param>
		/// <param name="darkBackground">the dark background color.</param>
		/// <param name="titleColor">Title color.</param>
		public ACTileNavigationBarAppearance(ACColor background, ACColor darkBackground, ACColor titleColor)
		{

			// Initialize default colors
			Initialize(background, darkBackground, titleColor);
		}

		/// <summary>
		/// Initialize the specified background, border and titleColor.
		/// </summary>
		/// <returns>The initialize.</returns>
		/// <param name="background">Background.</param>
		/// <param name="darkBackground">the dark background color.</param>
		/// <param name="border">Border.</param>
		/// <param name="titleColor">Title color.</param>
		internal void Initialize(ACColor background, ACColor darkBackground, ACColor titleColor) {

			//Initialize
			this._background = background;
			this._darkBackground = darkBackground;
			this._titleColor = titleColor;
			this._titleSize = 18;
			this._topPadding = 10;

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
		internal void RaiseAppearanceModified(){
			//Inform caller that the appearance has changed
			if (this.AppearanceModified != null)
				this.AppearanceModified ();
		}
		#endregion
	}
}

