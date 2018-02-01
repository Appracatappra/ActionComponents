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
using Java.Nio;
using System.IO;

namespace ActionComponents
{
	/// <summary>
	/// The <c>HSVImage</c> class creates the HSV images to present in the <c>ACColorCube</c> and <c>ACHueBar</c> based
	/// on the current Hue, Saturation and Value properties.
	/// </summary>
	public class HSVImage
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.HSVImage"/> class.
		/// </summary>
		public HSVImage()
		{
		}
		#endregion

		#region Static Methods
		/// <summary>
		/// Pin the specified minValue, value and maxValue.
		/// </summary>
		/// <param name="minValue">Minimum value.</param>
		/// <param name="value">Value.</param>
		/// <param name="maxValue">Max value.</param>
		public static float Pin(float minValue, float value, float maxValue)
		{
			if (minValue > value)
				return minValue;
			else if (maxValue < value)
				return maxValue;
			else
				return value;
		}

		/// <summary>
		/// Blend the specified value and percent.
		/// </summary>
		/// <param name="value">Value.</param>
		/// <param name="percentIn255">Percent in 255.</param>
		public static Byte Blend(Byte value, Byte percentIn255)
		{
			return (Byte)((int)value * percentIn255 / 255);
		}

		/// <summary>
		/// Saturations the brightness square image.
		/// </summary>
		/// <returns>The brightness square image.</returns>
		/// <param name="hue">Hue.</param>
		public static Bitmap SaturationBrightnessSquareImage(float hue)
		{
			// Calculate metrics
			int w = 256, h = 256;
			int bytesPerRow = w * 4;
			int bitmapByteCount = bytesPerRow * h;
			int bytesPerPixel = bytesPerRow / w;

			// Create pixel buffer
			var RGBImage = new int[w * h];

			// Precompute RGB values
			HSVColor hsv = new HSVColor(hue * 360.0f, 1f, 1f);
			RGBColor hueRGB = hsv.RGB;

			Byte r_s = (Byte)((1.0f - hueRGB.Red) * 255);
			Byte g_s = (Byte)((1.0f - hueRGB.Green) * 255);
			Byte b_s = (Byte)((1.0f - hueRGB.Blue) * 255);

			// Create storage for color manipulation
			Byte r_hs = 0, g_hs = 0, b_hs = 0;
			Byte max = 255, x = 0, y = 0;

			// Poke color cube into graphics space
			for (int s = 0; s <= 255; ++s)
			{
				// Create row color
				r_hs = (Byte)(max - Blend((Byte)s, r_s));
				g_hs = (Byte)(max - Blend((Byte)s, g_s));
				b_hs = (Byte)(max - Blend((Byte)s, b_s));

				// Process each column on this row
				for (int v = 0; v <=255; ++v)
				{
					// Get byte index
					var index = (v * 256) + s;

					// Insert color at current x,y location
					y = (Byte)(max - v);
					var pixel = new Color(Blend(y, r_hs), Blend(y, g_hs), Blend(y, b_hs));
					RGBImage[index] = (int)pixel;
				}
			}

			// Return results
			Bitmap createdBitmap = Bitmap.CreateBitmap(RGBImage, w, h, Bitmap.Config.Argb8888);
			return createdBitmap;
		}

		/// <summary>
		/// Hues the bar image.
		/// </summary>
		/// <returns>The bar image.</returns>
		/// <param name="index">Index.</param>
		/// <param name="hsv">Hsv.</param>
		public static Bitmap HueBarImage(ACHueBarComponentIndex index, HSVColor hsv)
		{
			// Calculate metrics
			int w = 256, h = 50;
			int bytesPerRow = w * 4;
			int bitmapByteCount = bytesPerRow * h;
			int bytesPerPixel = bytesPerRow / w;

			// Create pixel buffer
			var RGBImage = new int[w * h];

			// Create storage for color manipulation
			int byteIndex = 0;
			RGBColor rgb;
			HSVColor hsvAdjusted;

			// Draw color bar
			for (int x = 0; x < 256; ++x)
			{
				// Calculate byte index
				byteIndex = (x * bytesPerPixel);

				// Calculate new color space
				switch (index)
				{
					case ACHueBarComponentIndex.ComponentIndexHue:
						hsv.Hue = (float)x / 255f;
						break;
					case ACHueBarComponentIndex.ComponentIndexSaturation:
						hsv.Saturation = (float)x / 255f;
						break;
					case ACHueBarComponentIndex.ComponentIndexBrightness:
						hsv.Value = (float)x / 255f;
						break;
				}

				// Adjust color space
				hsvAdjusted = new HSVColor(hsv.Hue * 360.0f, hsv.Saturation, hsv.Value);
				rgb = hsvAdjusted.RGB;

				// Make color
				var pixel = new Color((Byte)(rgb.Red * 255f), (Byte)(rgb.Green * 255f), (Byte)(rgb.Blue * 255f));

				// Insert color
				for (int y = 0; y < 50; ++y) {
					// Build index
					var n = (y * 256) + x;
					RGBImage[n] = (int)pixel;
				}
			}

			// Return results
			Bitmap createdBitmap = Bitmap.CreateBitmap(RGBImage, w, h, Bitmap.Config.Argb8888);
			return createdBitmap;
		}
		#endregion
	}
}
