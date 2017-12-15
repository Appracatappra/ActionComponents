using System;
using Foundation;
using UIKit;
using CoreGraphics;

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
		private UIColor _background;
		private UIColor _border;
		private UIImage _image;
		private float _alpha;
		#endregion

		#region Computed Properties
		/// <summary>
		/// Gets or sets the background color
		/// </summary>
		/// <value>The background color</value>
		public UIColor Background
		{
			get { return _background; }
			set
			{
				//Save and inform caller
				_background = value;
				AppearanceModified?.Invoke();
			}
		}

		/// <summary>
		/// Gets or sets the border color
		/// </summary>
		/// <value>The border color</value>
		public UIColor Border
		{
			get { return _border; }
			set
			{
				//Save and inform caller
				_border = value;
				AppearanceModified?.Invoke();
			}
		}

		/// <summary>
		/// Gets or sets the image displayed on the button
		/// </summary>
		/// <value>The image</value>
		public UIImage Image
		{
			get { return _image; }
			set
			{
				//Save and inform caller
				_image = value;
				AppearanceModified?.Invoke();
			}
		}

		/// <summary>
		/// Gets or sets the alpha that the image is displayed at
		/// </summary>
		/// <value>The alpha.</value>
		public float Alpha
		{
			get { return _alpha; }
			set
			{
				//Save and inform caller
				_alpha = value;
				AppearanceModified?.Invoke();
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACNavBarButtonAppearance"/> class.
		/// </summary>
		public ACNavBarButtonAppearance()
		{
			_background = UIColor.Clear;
			_border = UIColor.Clear;
			_alpha = 1.0f;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACNavBarButtonAppearance"/> class.
		/// </summary>
		/// <param name="image">Image.</param>
		public ACNavBarButtonAppearance(UIImage image)
		{
			_background = UIColor.Clear;
			_border = UIColor.Clear;
			_alpha = 1.0f;
			_image = image;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACNavBarButtonAppearance"/> class.
		/// </summary>
		/// <param name="imageName">Image name.</param>
		public ACNavBarButtonAppearance(string imageName)
		{
			_background = UIColor.Clear;
			_border = UIColor.Clear;
			_alpha = 1.0f;
			_image = UIImage.FromBundle(imageName);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACNavBarButtonAppearance"/> class.
		/// </summary>
		/// <param name="background">Background.</param>
		/// <param name="border">Border.</param>
		/// <param name="image">Image.</param>
		/// <param name="alpha">Alpha.</param>
		public ACNavBarButtonAppearance(UIColor background, UIColor border, UIImage image, float alpha)
		{
			//Initialize
			_background = background;
			_border = border;
			_image = image;
			_alpha = alpha;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACNavBarButtonAppearance"/> class.
		/// </summary>
		/// <param name="background">Background.</param>
		/// <param name="border">Border.</param>
		/// <param name="imageName">Image name.</param>
		/// <param name="alpha">Alpha.</param>
		public ACNavBarButtonAppearance(UIColor background, UIColor border, string imageName, float alpha)
		{
			//Initialize
			_background = background;
			_border = border;
			_image = UIImage.FromBundle(imageName);
			_alpha = alpha;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACNavBarButtonAppearance"/> class.
		/// </summary>
		/// <param name="appearance">Appearance.</param>
		public ACNavBarButtonAppearance(ACNavBarButtonAppearance appearance)
		{
			//Initialize
			_background = appearance.Background;
			_border = appearance.Border;
			_image = appearance.Image;
			_alpha = appearance.Alpha;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Clone this instance.
		/// </summary>
		/// <returns>The clone.</returns>
		public ACNavBarButtonAppearance Clone() {

			// Create new appearance based on this one
			return new ACNavBarButtonAppearance(this);
		}

		/// <summary>
		/// Copies the appearance into this appearance.
		/// </summary>
		/// <param name="appearance">Appearance.</param>
		/// <param name="triggerAppearanceModified">If set to <c>true</c> trigger appearance modified.</param>
		public void CopyAppearance(ACNavBarButtonAppearance appearance, bool triggerAppearanceModified) {

			// Copy the elements from the given appearance
			_background = appearance.Background;
			_border = appearance.Border;
			if (appearance.Image !=null) _image = appearance.Image;
			_alpha = appearance.Alpha;

			// Raise the AppearanceModified event?
			if (triggerAppearanceModified) AppearanceModified?.Invoke();

		}

		/// <summary>
		/// Sets the image.
		/// </summary>
		/// <param name="imageName">Image name.</param>
		public void SetImage(string imageName) {

			// Load image from bundle
			Image = UIImage.FromBundle(imageName);
		}
		#endregion

		#region Events
		/// <summary>
		/// Occurs when appearance modified.
		/// </summary>
		public event ACNavBarAppearanceModifiedDelegate AppearanceModified;
		#endregion
	}
}
