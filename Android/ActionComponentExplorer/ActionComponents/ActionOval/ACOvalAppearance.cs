using System;
using Android.Graphics;

namespace ActionComponents
{
	public class ACOvalAppearance
	{
		#region Private Variables
		private bool _hasBorder = true;
		private Color _borderColor = Color.White;
		private int _borderWidth = 2;
		private Color _fillColor = Color.Rgb(215, 41, 126); //UIColor.FromRGBA(0.885f, 0.059f, 0.385f, 1.000f);
		private Bitmap _image = null;
		private ACOvalImagePlacement _imagePlacement = ACOvalImagePlacement.TopLeft;
		private int _imageX = 0;
		private int _imageY = 0;
		private int _imageHeight = 0;
		private int _imageWidth = 0;
		private string _fontName = "Helvetica";
		private int _fontSize = 24;
		private Color _fontColor = Color.White;
		#endregion

		#region Computed Properties
		/// <summary>
		/// Gets or sets the name of the font.
		/// </summary>
		/// <value>The name of the font.</value>
		public string FontName {
			get { return _fontName; }
			set {
				_fontName = value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets the size of the font.
		/// </summary>
		/// <value>The size of the font.</value>
		public int FontSize {
			get { return _fontSize; }
			set {
				_fontSize = value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets the color of the font.
		/// </summary>
		/// <value>The color of the font.</value>
		public Color FontColor {
			get { return _fontColor; }
			set {
				_fontColor = value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets the color of the fill.
		/// </summary>
		/// <value>The color of the fill.</value>
		public Color FillColor {
			get { return _fillColor; }
			set {
				_fillColor = value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets the image.
		/// </summary>
		/// <value>The image.</value>
		public Bitmap Image {
			get { return _image; }
			set {
				_image = value;

				// Save default image height and width
				_imageHeight = _image.Height;
				_imageWidth = _image.Width;

				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets the image placement.
		/// </summary>
		/// <value>The image placement.</value>
		public ACOvalImagePlacement ImagePlacement {
			get { return _imagePlacement; }
			set {
				_imagePlacement = value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets the image x.
		/// </summary>
		/// <value>The image x.</value>
		public int ImageX {
			get { return _imageX; }
			set {
				_imageX = value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets the image y.
		/// </summary>
		/// <value>The image y.</value>
		public int ImageY {
			get { return _imageY; }
			set {
				_imageY = value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets the height of the image.
		/// </summary>
		/// <value>The height of the image.</value>
		public int ImageHeight {
			get { return _imageHeight; }
			set {
				_imageHeight = value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets the width of the image.
		/// </summary>
		/// <value>The width of the image.</value>
		public int ImageWidth {
			get { return _imageWidth; }
			set {
				_imageWidth = value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance has border.
		/// </summary>
		/// <value><c>true</c> if this instance has border; otherwise, <c>false</c>.</value>
		public bool HasBorder {
			get { return _hasBorder; }
			set {
				_hasBorder = value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets the color of the border.
		/// </summary>
		/// <value>The color of the border.</value>
		public Color BorderColor {
			get { return _borderColor; }
			set {
				_borderColor = value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets the width of the border.
		/// </summary>
		/// <value>The width of the border.</value>
		public int BorderWidth {
			get { return _borderWidth; }
			set {
				_borderWidth = value;
				RaiseAppearanceModified ();
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACOvalAppearance"/> class.
		/// </summary>
		public ACOvalAppearance ()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACOvalAppearance"/> class.
		/// </summary>
		/// <param name="fillColor">Fill color.</param>
		/// <param name="borderColor">Border color.</param>
		public ACOvalAppearance (Color fillColor, Color borderColor)
		{

			// Initialize
			this._fillColor = fillColor;
			this._borderColor = borderColor;

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACOvalAppearance"/> class.
		/// </summary>
		/// <param name="fillColor">Fill color.</param>
		/// <param name="borderColor">Border color.</param>
		/// <param name="fontColor">Font color.</param>
		public ACOvalAppearance (Color fillColor, Color borderColor, Color fontColor)
		{

			// Initialize
			this._fillColor = fillColor;
			this._borderColor = borderColor;
			this._fontColor = fontColor;

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

