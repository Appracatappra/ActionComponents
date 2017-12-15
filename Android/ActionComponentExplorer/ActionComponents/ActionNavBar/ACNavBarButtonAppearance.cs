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
	/// Defines the appearance of a <see cref="ActionComponents.ACNavBarButton"/> 
	/// </summary>
	/// <remarks>Changes to the default <see cref="ActionComponents.ACNavBarButtonAppearance"/>s in the parent <see cref="ActionComponents.ACNavBar"/> and
	/// and <see cref="ActionComponents.ACNavBarButtonCollection"/>s will cascade down to lower levels.</remarks>
	public class ACNavBarButtonAppearance
	{
		#region Private Variable Storage
		private Color _background;
		private Color _border;
		private int _image;
		private float _alpha;
		#endregion
		
		#region Computed Properties
		/// <summary>
		/// Gets or sets the background color
		/// </summary>
		/// <value>The background color</value>
		public Color background {
			get { return _background;}
			set {
				//Save and inform caller
				_background=value;
				RaiseAppearanceModified();
			}
		}
		
		/// <summary>
		/// Gets or sets the border color
		/// </summary>
		/// <value>The border color</value>
		public Color border{
			get { return _border;}
			set {
				//Save and inform caller
				_border=value;
				RaiseAppearanceModified();
			}
		}
		
		/// <summary>
		/// Gets or sets the image displayed on the button
		/// </summary>
		/// <value>The image</value>
		public int image {
			get { return _image;}
			set {
				//Save and inform caller
				_image = value;
				RaiseAppearanceModified ();
			}
		}
		
		/// <summary>
		/// Gets or sets the alpha that the image is displayed at
		/// </summary>
		/// <value>The alpha.</value>
		public float alpha{
			get { return _alpha;}
			set {
				//Save and inform caller
				_alpha = value;
				RaiseAppearanceModified ();
			}
		}
		#endregion
		
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACNavBarButtonAppearance"/> class.
		/// </summary>
		public ACNavBarButtonAppearance ()
		{
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACNavBarButtonAppearance"/> class.
		/// </summary>
		/// <param name="background">The background color</param>
		/// <param name="border">The border color</param>
		/// <param name="image">An image for the apperance state</param>
		/// <param name="alpha">The alpha setting for the state</param>
		public ACNavBarButtonAppearance(Color background, Color border, int image, float alpha){
			//Initialize
			this._background = background;
			this._border = border;
			this._image = image;
			this._alpha = alpha;
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACNavBarButtonAppearance"/> class.
		/// </summary>
		/// <param name="appearance">The source appearance to clone this appearance from</param>
		public ACNavBarButtonAppearance(ACNavBarButtonAppearance appearance){
			//Initialize
			this._background = appearance.background;
			this._border = appearance.border;
			this._image = appearance.image;
			this._alpha = appearance.alpha;
		}
		#endregion 
		
		#region Public Methods
		/// <summary>
		/// Clones the values from the passed <see cref="ActionComponents.ACNavBarButtonAppearance"/> into this one 
		/// </summary>
		/// <param name="appearance">The Appearance to clone</param>
		public void Clone(ACNavBarButtonAppearance appearance){
			//Clone the passed appearance into this one
			this._background = appearance.background;
			this._border = appearance.border;
			this._image = appearance.image;
			this._alpha = appearance.alpha;
			
			//Inform caller that the appearance has changed
			RaiseAppearanceModified ();
		}
		#endregion 
		
		#region Events
		/// <summary>
		/// Occurs when the appearance is modified.
		/// </summary>
		internal delegate void AppearanceModifiedDelegate();
		/// <summary>
		/// Occurs when the appearance is modified.
		/// </summary>
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

