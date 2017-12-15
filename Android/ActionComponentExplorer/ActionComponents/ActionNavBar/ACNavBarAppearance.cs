using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;

namespace ActionComponents
{
	/// <summary>
	/// Controls the appearance of a <see cref="ActionComponents.ACNavBar"/> with properties such as 
	/// background color, border color, and shadow color
	/// </summary>
	/// <remarks>The <c>border</c> property also controls the color of the <see cref="ActionComponents.ACNavBarPointer"/>  </remarks>
	public class ACNavBarAppearance
	{
		#region Private Variables
		private Color _background;
		private Color _border;
		private Color _shadow;
		private bool _flat = false;
		#endregion 

		#region Computed Properies
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
		#endregion 

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACNavBarAppearance"/> class and
		/// sets the default appearance for the control
		/// </summary>
		public ACNavBarAppearance ()
		{
			//Initialize default colors
			this._background=Color.Black;
			this._border=Color.Black;
			this.shadow=Color.Argb (87,0,0,0);
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACNavBarAppearance"/> class with
		/// the specified user defined appearance properties
		/// </summary>
		/// <param name="background">The control's background color</param>
		/// <param name="border">The control's border color</param>
		/// <param name="shadow">The control's shadow color</param>
		public ACNavBarAppearance(Color background, Color border, Color shadow){
			
			//Initialize
			this._background = background;
			this._border = border;
			this._shadow = shadow;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <see cref="ActionComponents.ACNavBarAppearance"/> is flat.
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

		#region Public Methods
		/// <summary>
		/// Flattens the <see cref="ActionComponents.ACNavBarAppearance"/> to match the new iOS 7 design language
		/// </summary>
		public void Flatten(){

			//Set to the default flat style
			_flat = true;
			_background = Color.Rgb(200, 200, 200);
			_border = _background;
			_shadow=Color.Argb (0,0,0,0);

			//Inform caller of the appearance change
			RaiseAppearanceModified ();

		}

		/// <summary>
		/// Flattens the <see cref="ActionComponents.ACNavBarAppearance"/> to match the new iOS 7 design language
		/// </summary>
		public void Flatten(Color backgroundColor){

			//Set to the default flat style
			_flat = true;
			_background = backgroundColor;
			_border = backgroundColor;
			_shadow=Color.Argb (0,0,0,0);

			//Inform caller of the appearance change
			RaiseAppearanceModified ();

		}
		#endregion
	}
	
}

