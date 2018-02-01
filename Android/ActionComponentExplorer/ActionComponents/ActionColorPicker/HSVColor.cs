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
	/// Defines a color as its Hue, Saturation and Value properties and contains utilities to move the color to and
	/// from other color spaces such as <c>UIColor</c>.
	/// </summary>
	public class HSVColor
	{
		#region Computed Properties
		/// <summary>
		/// Gets or sets the hue.
		/// </summary>
		/// <value>The hue.</value>
		public float Hue { get; set; }

		/// <summary>
		/// Gets or sets the saturation.
		/// </summary>
		/// <value>The saturation.</value>
		public float Saturation { get; set; }

		/// <summary>
		/// Gets or sets the value (brightness).
		/// </summary>
		/// <value>The value.</value>
		public float Value { get; set; }

		/// <summary>
		/// Gets or sets the RGB color for this HSV color
		/// </summary>
		/// <value>The RGB.</value>
		public RGBColor RGB {
			get { return ToRGB (); }
			set { FromRGB (value); }
		}

		/// <summary>
		/// Gets or sets the color.
		/// </summary>
		/// <value>The color.</value>
		public Color RawColor {
			get { return ToRawColor (); }
			set { FromRawColor (value); }
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.HSVColor"/> class.
		/// </summary>
		public HSVColor ()
		{
			// Initialize
			this.Hue = 0f;
			this.Saturation = 0f;
			this.Value = 0f;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.HSVColor"/> class.
		/// </summary>
		/// <param name="hue">Hue.</param>
		/// <param name="saturation">Saturation.</param>
		/// <param name="value">Value.</param>
		public HSVColor (float hue, float saturation, float value)
		{
			// Initialize
			this.Hue = hue;
			this.Saturation = saturation;
			this.Value = value;

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.HSVColor"/> class.
		/// </summary>
		/// <param name="color">Color.</param>
		public HSVColor (RGBColor color)
		{
			// Initialize
			this.FromRGB (color);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.HSVColor"/> class.
		/// </summary>
		/// <param name="color">Color.</param>
		public HSVColor (Color color)
		{
			// Initialize
			this.FromRawColor (color);
		}
		#endregion 

		#region Static Methods
		/// <summary>
		/// Converts a given hue value into its Red, Green, Blue component factors
		/// </summary>
		/// <returns>The to RG.</returns>
		/// <param name="hue">Hue.</param>
		public static RGBColor HueToRGB(float hue) {
			RGBColor result = new RGBColor ();

			// Compute factors
			float huePrime = hue / 60.0f;
			float x = 1.0f - (float)Math.Abs ((huePrime % 2.0f) - 1.0f);

			// Calculate base factors
			if (huePrime < 1.0f) {
				result.Red = 1;
				result.Green = x;
				result.Blue = 0;
			}
			else if (huePrime < 2.0f) {
				result.Red = x;
				result.Green = 1;
				result.Blue = 0;
			}
			else if (huePrime < 3.0f) {
				result.Red = 0;
				result.Green = 1;
				result.Blue = x;
			}
			else if (huePrime < 4.0f) {
				result.Red = 0;
				result.Green = x;
				result.Blue = 1;
			}
			else if (huePrime < 5.0f) {
				result.Red = x;
				result.Green = 0;
				result.Blue = 1;
			}
			else {
				result.Red = 1;
				result.Green = 0;
				result.Blue = x;
			}

			// Return results
			return result;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Converts the current HSV color to an RGB color.
		/// </summary>
		/// <returns>The RGB.</returns>
		public RGBColor ToRGB() {
			RGBColor result = HueToRGB (Hue);

			// Calculate factors
			float c = Value * Saturation;
			float m = Value - c;

			// Calculate RGB
			result.Red *= (c + m);
			result.Green *= (c + m);
			result.Blue *= (c + m);

			// Return Results
			return result;
		}

		/// <summary>
		/// Sets the value of this HSV color from the given RGB color
		/// </summary>
		/// <param name="color">Color.</param>
		public void FromRGB(RGBColor color) {
			HSVColor hsv = color.ToHSV (false);

			Hue = hsv.Hue;
			Saturation = hsv.Saturation;
			Value = hsv.Value;
		}

		/// <summary>
		/// Returns a <c>Color</c> for this HSV color
		/// </summary>
		/// <returns>The user interface color.</returns>
		public Color ToRawColor() {
			float[] hsv = new float[] {Hue, Saturation, Value };
			return Color.HSVToColor(255, hsv);
		}

		/// <summary>
		/// Sets the value of this HSV color from a <c>UIColor</c>
		/// </summary>
		/// <param name="color">Color.</param>
		public void FromRawColor(Color color) {

			// Save values
			Hue = color.GetHue();
			Saturation = color.GetSaturation();
			Value = color.GetBrightness();

		}
		#endregion
	}
}

