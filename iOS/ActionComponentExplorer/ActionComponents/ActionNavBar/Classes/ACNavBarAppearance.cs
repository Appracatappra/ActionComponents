using System;
using Foundation;
using UIKit;
using CoreGraphics;

namespace ActionComponents
{
	/// <summary>
	/// Controls the appearance of a <see cref="ActionComponents.ACNavBar"/> with properties such as 
	/// background color, border color, and shadow color
	/// </summary>
	/// <remarks>The <c>border</c> property also controls the color of the <see cref="ActionComponents.ACNavBarPointer"/>  </remarks>
	public class ACNavBarAppearance
	{
		#region Private Properties
		private UIColor _background;
		private UIColor _border;
		private UIColor _shadow;
		private bool _flat = false;
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
				_border = value;
				AppearanceModified?.Invoke();
			}
		}

		/// <summary>
		/// Gets or sets the shadow color
		/// </summary>
		/// <value>The shadow color</value>
		public UIColor Shadow
		{
			get { return _shadow; }
			set
			{
				_shadow = value;
				AppearanceModified?.Invoke();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <see cref="ActionComponents.ACNavBarAppearance"/> is flat.
		/// </summary>
		/// <value><c>true</c> if flat; otherwise, <c>false</c>.</value>
		/// <remarks>This value was added to support the iOS 7 design language</remarks>
		public bool Flat
		{
			get { return _flat; }
			set
			{
				_flat = value;
				AppearanceModified?.Invoke();
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACNavBarAppearance"/> class.
		/// </summary>
		public ACNavBarAppearance()
		{
			// Initialize
			_background = UIColor.FromRGB(200, 200, 200);
			_border = _background;
			_shadow = UIColor.Clear;
			_flat = true;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACNavBarAppearance"/> class.
		/// </summary>
		/// <param name="background">Background.</param>
		public ACNavBarAppearance(UIColor background)
		{
			// Initialize
			_background = background;
			_border = _background;
			_shadow = UIColor.Clear;
			_flat = true;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACNavBarAppearance"/> class.
		/// </summary>
		/// <param name="background">Background.</param>
		/// <param name="border">Border.</param>
		/// <param name="shadow">Shadow.</param>
		public ACNavBarAppearance(UIColor background, UIColor border, UIColor shadow)
		{
			// Initialize
			_background = background;
			_border = border;
			_shadow = shadow;
			_flat = false;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACNavBarAppearance"/> class.
		/// </summary>
		/// <param name="appearance">Appearance.</param>
		public ACNavBarAppearance(ACNavBarAppearance appearance){

			// Initialize
			_background = appearance.Background;
			_border = appearance.Border;
			_shadow = appearance.Shadow;
			_flat = appearance.Flat;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Clone this instance.
		/// </summary>
		/// <returns>The clone.</returns>
		public ACNavBarAppearance Clone() {

			// Create a new appearance based off of this one
			return new ACNavBarAppearance(this);
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
