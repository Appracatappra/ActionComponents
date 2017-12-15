using System;
using Android.Graphics;

namespace ActionComponents
{
	/// <summary>
	/// Controls the appearance of a <see cref="ActionComponents.ACTableViewController"/> with properties such as 
	/// background color, border color, and shadow color.
	/// </summary>
	/// <remarks>Available in ActionPack Business or Enterprise only.</remarks>
	public class ACTableViewAppearance
	{
		#region Private Properties
		private Color _headerBackground;
		private Color _headerTextColor;
		private Color _footerBackground;
		private Color _footerTextColor;
		private Color _divider;
		private int _dividerHeight = 2;
		private Color _cellBackground;
		private Color _cellHighlight;
		private Color _cellSelected;
		private Color _titleColor;
		private Color _subtitleColor;
		#endregion 

		#region Computed Properties
		/// <summary>
		/// Gets or sets the cell background.
		/// </summary>
		/// <value>The cell background.</value>
		public Color cellBackground{
			get{ return _cellBackground;}
			set{
				_cellBackground = value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets the color of the title text.
		/// </summary>
		/// <value>The color of the title.</value>
		public Color titleColor{
			get{ return _titleColor;}
			set{
				_titleColor = value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets the color of the subtitle text.
		/// </summary>
		/// <value>The color of the subtitle.</value>
		public Color subtitleColor{
			get{ return _subtitleColor;}
			set{
				_subtitleColor = value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets the cell highlight.
		/// </summary>
		/// <value>The cell highlight.</value>
		public Color cellHighlight{
			get{ return _cellHighlight;}
			set{
				_cellHighlight = value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets the cell selected.
		/// </summary>
		/// <value>The cell selected.</value>
		public Color cellSelected{
			get{ return _cellSelected;}
			set{
				_cellSelected = value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets the divider color
		/// </summary>
		/// <value>The divider.</value>
		public Color divider{
			get{ return _divider;}
			set{
				_divider = value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets the height of the divider.
		/// </summary>
		/// <value>The height of the divider.</value>
		public int dividerHeight{
			get { return _dividerHeight;}
			set {
				_dividerHeight = value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets the header background.
		/// </summary>
		/// <value>The header background.</value>
		public Color headerBackground {
			get{ return _headerBackground;}
			set{
				_headerBackground = value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets the color of the header text.
		/// </summary>
		/// <value>The color of the header text.</value>
		public Color headerTextColor{
			get{ return _headerTextColor;}
			set{
				_headerTextColor=value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets the footer background.
		/// </summary>
		/// <value>The footer background.</value>
		public Color footerBackground{
			get{ return _footerBackground;}
			set{
				_footerBackground = value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets the color of the footer text.
		/// </summary>
		/// <value>The color of the footer text.</value>
		public Color footerTextColor{
			get{ return _footerTextColor;}
			set{
				_footerTextColor = value;
				RaiseAppearanceModified ();
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="ActionComponents.ACTableViewAppearance"/> class.
		/// </summary>
		public ACTableViewAppearance ()
		{
			//Initialize default colors "Color.Argb(255,62,62,62);"
			this._headerBackground = Color.Rgb(62,62,62);
			this._headerTextColor = Color.White;
			this.footerBackground = Color.Rgb(62,62,62);
			this.footerTextColor = Color.White;
			this.divider = Color.Rgb(62,62,62);
			this.cellBackground = Color.Argb (0, 255, 255, 255);
			this._cellHighlight = Color.Orange;
			this.cellSelected = Color.CornflowerBlue;
			this.titleColor = Color.White;
			this.subtitleColor = Color.White;
		}

		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="ActionComponents.ACTableViewAppearance"/> class.
		/// </summary>
		/// <param name="headerBackground">Header background.</param>
		/// <param name="headerText">Header text.</param>
		/// <param name="footerBackground">Footer background.</param>
		/// <param name="footerTextColor">Footer text color.</param>
		public ACTableViewAppearance(Color headerBackground, Color headerText, Color footerBackground, Color footerTextColor){

			//Initialize
			this._headerBackground = headerBackground;
			this._headerTextColor = headerText;
			this._footerBackground = footerBackground;
			this._footerTextColor = footerTextColor;
			this.titleColor = Color.White;
			this.subtitleColor = Color.White;
		}

		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="ActionComponents.ACTableViewAppearance"/> class.
		/// </summary>
		/// <param name="headerBackground">Header background.</param>
		/// <param name="headerText">Header text.</param>
		/// <param name="footerBackground">Footer background.</param>
		/// <param name="footerTextColor">Footer text color.</param>
		/// <param name="titleColor">Title color.</param>
		/// <param name="subtitleColor">Subtitle color.</param>
		public ACTableViewAppearance(Color headerBackground, Color headerText, Color footerBackground, Color footerTextColor, Color titleColor, Color subtitleColor){

			//Initialize
			this._headerBackground = headerBackground;
			this._headerTextColor = headerText;
			this._footerBackground = footerBackground;
			this._footerTextColor = footerTextColor;
			this.titleColor = titleColor;
			this.subtitleColor = subtitleColor;
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

