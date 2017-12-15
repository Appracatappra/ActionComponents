using System;
using System.Drawing;

using Foundation;
using UIKit;
using CoreGraphics;

namespace ActionComponents
{
	public class ACToastAppearance
	{
		#region Private Variables
		private float _textSize = 16;
		private float _cornerRadius = 5;
		private UIColor _textColor = UIColor.White;
		private UIColor _textShadow = UIColor.DarkGray;
		private UIColor _background = UIColor.FromRGBA (0, 0, 0, 0.7f);
		private bool _flat = false;
		#endregion 

		#region Computed Properties
		/// <summary>
		/// Gets or sets the size of the text.
		/// </summary>
		/// <value>The size of the font.</value>
		public float textSize {
			get { return _textSize;}
			set {
				_textSize = value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets the corner radius.
		/// </summary>
		/// <value>The corner radius.</value>
		public float cornerRadius {
			get { return _cornerRadius;}
			set {
				_cornerRadius = value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets the color of the text.
		/// </summary>
		/// <value>The color of the text.</value>
		public UIColor textColor{
			get { return _textColor;}
			set {
				_textColor = value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets the text shadow.
		/// </summary>
		/// <value>The text shadow.</value>
		public UIColor textShadow{
			get { return _textShadow;}
			set {
				_textShadow = value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets the background color
		/// </summary>
		/// <value>The background.</value>
		public UIColor background{
			get { return _background;}
			set {
				_background = value;
				RaiseAppearanceModified ();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <see cref="ActionComponents.ACToastAppearance"/> is flat.
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
		/// Initializes a new instance of the <see cref="ActionComponents.ACToastAppearance"/> class.
		/// </summary>
		public ACToastAppearance ()
		{
			// Automatically flatten appearance
			Flatten();
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Flattens the <see cref="ActionComponents.ACToastAppearance"/> to match the new iOS 7 design language
		/// </summary>
		public void Flatten(){

			//Set to the default flat style
			_flat = true;
			_background = UIColor.FromRGBA (0, 0, 0, 0.7f);
			_textColor = UIColor.White;


			//Inform caller of the appearance change
			RaiseAppearanceModified ();

		}

		/// <summary>
		/// Flattens the <see cref="ActionComponents.ACToastAppearance"/> to match the new iOS 7 design language
		/// </summary>
		/// <param name="backgroundColor">Background color.</param>
		public void Flatten(UIColor backgroundColor){

			//Set to the default flat style
			_flat = true;
			_background = backgroundColor;
			_textColor = UIColor.White;

			//Inform caller of the appearance change
			RaiseAppearanceModified ();

		}

		/// <summary>
		/// Flattens the <see cref="ActionComponents.ACToastAppearance"/> to match the new iOS 7 design language
		/// </summary>
		/// <param name="backgroundColor">Background color.</param>
		/// <param name="foregroundColor">Foreground color.</param>
		public void Flatten(UIColor backgroundColor, UIColor foregroundColor){

			//Set to the default flat style
			_flat = true;
			_background = backgroundColor;
			_textColor = foregroundColor;

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

