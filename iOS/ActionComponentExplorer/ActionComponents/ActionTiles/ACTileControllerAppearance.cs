using System;
using System.Drawing;
using Foundation;
using UIKit;
using CoreGraphics;

namespace ActionComponents
{
	/// <summary>
	/// Defines the customizable properties of an <c>ACTileController</c> that control it's look and feel.
	/// </summary>
	public class ACTileControllerAppearance
	{
		#region Private Properties
		private ACColor _background;
		private UIImage _backgroundImage;
		private ACColor _border;
		private float _indentTop = 0f;
		private float _indentBottom = 0f;
		private float _indentLeft = 0f;
		private float _indentRight = 0f;
		private float _cellSize = 68f;
		private float _cellGap = 10f;
		private float _groupGap = 20f;
		#endregion 

		#region Computed Properties
		/// <summary>
		/// Gets or sets the size of the cell.
		/// </summary>
		/// <value>The size of the cell.</value>
		public float cellSize {
			get { return _cellSize;}
			set {
				_cellSize = value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets the cell gap.
		/// </summary>
		/// <value>The cell gap.</value>
		public float cellGap {
			get { return _cellGap;}
			set {
				_cellGap = value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets the group gap.
		/// </summary>
		/// <value>The group gap.</value>
		public float groupGap {
			get { return _groupGap;}
			set {
				_groupGap = value;
				RaiseAppearanceModified ();
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
		/// Gets or sets the background image.
		/// </summary>
		/// <value>The background image.</value>
		public UIImage backgroundImage {
			get{ return _backgroundImage;}
			set{
				_backgroundImage = value;
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
		/// Gets or sets the indent top.
		/// </summary>
		/// <value>The indent top.</value>
		public float indentTop{
			get { return _indentTop;}
			set {
				_indentTop = value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets the indent bottom.
		/// </summary>
		/// <value>The indent bottom.</value>
		public float indentBottom{
			get { return _indentBottom;}
			set {
				_indentBottom = value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets the indent left.
		/// </summary>
		/// <value>The indent left.</value>
		public float indentLeft{
			get { return _indentLeft;}
			set {
				_indentLeft = value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets the indent right.
		/// </summary>
		/// <value>The indent right.</value>
		public float indentRight{
			get { return _indentRight;}
			set {
				_indentRight = value;
				RaiseAppearanceModified ();
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <c>ACTrayAppearance</c> class and
		/// sets the default appearance for the control
		/// </summary>
		public ACTileControllerAppearance ()
		{
			//Initialize default colors
			this._background = ACColor.LightGray;
			this._border = ACColor.Clear;

		}

		/// <summary>
		/// Initializes a new instance of the <c>ACTrayAppearance</c> class with
		/// the specified user defined appearance properties
		/// </summary>
		/// <param name="background">The control's background color</param>
		/// <param name="border">The control's border color</param>
		/// <param name="shadow">The control's shadow color</param>
		public ACTileControllerAppearance(ACColor background, ACColor border){

			//Initialize
			this._background = background;
			this._border = border;
		
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

