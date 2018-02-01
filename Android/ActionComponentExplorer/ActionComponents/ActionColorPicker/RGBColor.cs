using System;
using System.Threading;

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
using Android.Animation;

namespace ActionComponents
{
	/// <summary>
	/// Represents a color based on its Red, Green and Blue properties and contains the propereties and methods to
	/// convert the color to a different color space, such as <c>UIColor</c>.
	/// </summary>
	public class RGBColor
	{
		#region Computed Properties
		/// <summary>
		/// Gets or sets the red value.
		/// </summary>
		/// <value>The red value as a number between 0 and 1.</value>
		public float Red { get; set; }

		/// <summary>
		/// Gets or sets the blue value.
		/// </summary>
		/// <value>The blue value as a number between 0 and 1.</value>
		public float Blue { get; set; }

		/// <summary>
		/// Gets or sets the green value
		/// </summary>
		/// <value>The green value as a number between 0 and 1.</value>
		public float Green { get; set; }

		/// <summary>
		/// Gets or sets the HSV color for this RGB color
		/// </summary>
		/// <value>The HS.</value>
		public HSVColor HSV {
			get { return ToHSV (false); }
			set { FromHSV (value); }
		}

		/// <summary>
		/// Gets or sets the RGBColor as a raw Android <c>Color</c>.
		/// </summary>
		/// <value>The color as a raw Android <c>Color</c>.</value>
		public Color Raw {
			get { return ToRawColor (); }
			set { FromRawColor (value); }
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ActionHSBColorPicker.RGBColor"/> class.
		/// </summary>
		public RGBColor ()
		{
			// Initialize
			this.Red = 0f;
			this.Blue = 0f;
			this.Green = 0f;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionHSBColorPicker.RGBColor"/> class.
		/// </summary>
		/// <param name="red">Red (0 to 1).</param>
		/// <param name="blue">Blue (0 to 1).</param>
		/// <param name="green">Green (0 to 1).</param>
		public RGBColor (float red, float blue, float green)
		{
			// Intialize
			this.Red = red;
			this.Blue = blue;
			this.Green = green;
		} 

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionHSBColorPicker.RGBColor"/> class.
		/// </summary>
		/// <param name="color">Color.</param>
		public RGBColor (HSVColor color)
		{
			// Initialize
			this.FromHSV (color);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionHSBColorPicker.RGBColor"/> class.
		/// </summary>
		/// <param name="color">Color.</param>
		public RGBColor (Color color)
		{
			// Initialize
			this.FromRawColor (color);
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Tos the HS.
		/// </summary>
		/// <returns>The HS.</returns>
		/// <param name="preserveHS">If set to <c>true</c> preserves the current Hue and Saturation.</param>
		public HSVColor ToHSV(bool preserveHS) {
			HSVColor result = new HSVColor ();

			// Get the Maximum value
			float max = Red;

			if (max < Green)
				max = Green;

			if (max < Blue)
				max = Blue;

			// Get the minimum value
			float min = Red;

			if (min > Green)
				min = Green;

			if (min > Blue)
				min = Blue;

			// Save brightness (value)
			result.Value = max;

			// Calculate saturation
			float sat;

			if (max != 0.0f) {
				sat = (max - min) / max;
				result.Saturation = sat;
			} else {
				sat = 0.0f;

				// Black, so sat is undefined, use 0
				if (!preserveHS)
					result.Saturation = 0.0f;                                      
			}

			// Calculate hue
			float delta;

			if (sat == 0.0f) {
				// No color, so hue is undefined, use 0
				if (!preserveHS)
					result.Hue = 0.0f;                                      
			} else {
				delta = max - min;

				float hue;

				if (Red == max)
					hue = (Green - Blue) / delta;
				else if (Green == max)
					hue = 2 + (Blue - Red) / delta;
				else
					hue = 4 + (Red - Green) / delta;

				hue /= 6.0f;

				if (hue < 0.0f)
					hue += 1.0f;

				// 0.0 and 1.0 hues are actually both the same (red)
				if (!preserveHS || Math.Abs(hue - result.Hue) != 1.0f)
					result.Hue = hue;                               
			}

			// Return results
			return result;
		}

		/// <summary>
		/// Sets the value of this RGB color from the give HSV color
		/// </summary>
		/// <param name="color">Color.</param>
		public void FromHSV(HSVColor color) {
			RGBColor rgb = color.ToRGB ();

			// Save values
			Red = rgb.Red;
			Blue = rgb.Blue;
			Green = rgb.Green;
		}

		/// <summary>
		/// Returns a <c>Color</c> for this RGB color
		/// </summary>
		/// <returns>The user interface color.</returns>
		public Color ToRawColor() {

			return Color.Rgb((int)(Red * 255), (int)(Green * 255), (int)(Blue * 255));
		}

		/// <summary>
		/// Sets the value of this RGB color from a <c>UIColor</c>
		/// </summary>
		/// <param name="color">Color.</param>
		public void FromRawColor(Color color) {

			// Save values
			Red = ((float)color.R) / 255f;
			Green = ((float)color.G) / 255f;
			Blue = ((float)color.B) / 255f;

		}
		#endregion
	}
}

