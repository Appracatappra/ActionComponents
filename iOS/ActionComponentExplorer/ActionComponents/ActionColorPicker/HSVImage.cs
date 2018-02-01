using System;
using UIKit;
using CoreImage;
using CoreGraphics;
using System.Runtime.InteropServices;
using CoreGraphics;

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
		public HSVImage ()
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
		public static nfloat Pin(nfloat minValue, nfloat value, nfloat maxValue)
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
			return (Byte) ((int) value * percentIn255 / 255);
		}

		/// <summary>
		/// Creates the background rx image context.
		/// </summary>
		/// <returns>The background rx image context.</returns>
		/// <param name="w">The width.</param>
		/// <param name="h">The height.</param>
		/// <param name="data">Data.</param>
		public static CGBitmapContext CreateBGRxImageContext(int w, int h, Byte[] data)
		{
			// Create context
			CGColorSpace colorSpace = CGColorSpace.CreateDeviceRGB ();
			CGBitmapFlags kBGRxBitmapInfo = CGBitmapFlags.ByteOrder32Little | CGBitmapFlags.NoneSkipFirst; 
			CGBitmapContext context = new CGBitmapContext (data, w, h, 8, w * 4, colorSpace, kBGRxBitmapInfo); 

			// Free memory
			colorSpace.Dispose ();

			// Return new context
			return context;
		}

		/// <summary>
		/// Saturations the brightness square image.
		/// </summary>
		/// <returns>The brightness square image.</returns>
		/// <param name="hue">Hue.</param>
		public static UIImage SaturationBrightnessSquareImage(nfloat hue) {

			// Calculate metrics
			int w = 256, h = 256;
			int bytesPerRow = w * 4;
			int bitmapByteCount = bytesPerRow * h;
			int bytesPerPixel = bytesPerRow / w;
			int bitsPerComponent = 8;

			// Allocate memory for the image's bitmap
			Byte[] BitmapData = new Byte[bitmapByteCount];

			// Create graphics context
			CGBitmapContext context = CreateBGRxImageContext (w, h, BitmapData);

			// Precompute RGB values
			HSVColor hsv = new HSVColor (hue * 360.0f, 1f, 1f);
			RGBColor hueRGB = hsv.RGB;

			Byte r_s = (Byte) ((1.0f - hueRGB.Red) * 255);
			Byte g_s = (Byte) ((1.0f - hueRGB.Green) * 255);
			Byte b_s = (Byte) ((1.0f - hueRGB.Blue) * 255);

			// Create storage for color manipulation
			int byteIndex = 0;
			Byte r_hs = 0, g_hs = 0, b_hs = 0;
			Byte max = 255, x = 0, y = 0;

			// Poke color cube into graphics space
			for (int s = 255; s >= 0; --s) {
				// Create row color
				r_hs = (Byte)(max - Blend((Byte)s, r_s));
				g_hs = (Byte)(max - Blend((Byte)s, g_s));
				b_hs = (Byte)(max - Blend((Byte)s, b_s));

				// Process each column on this row
				for (int v = 255; v >= 0; --v) {
					// Get byte index
					byteIndex = (bytesPerRow * v) + (s * bytesPerPixel);

					// Insert color at current x,y location
					y = (Byte)(max - v);
					BitmapData[byteIndex] = Blend(y, b_hs);
					BitmapData[byteIndex+1] = Blend(y, g_hs);
					BitmapData[byteIndex+2] = Blend(y, r_hs);
				}
			}

			// Frame color box
			context.SetStrokeColor (UIColor.White.CGColor);
			context.SetLineWidth (2);
			context.AddRect (new CGRect (0, 0, w, h));
			context.DrawPath (CGPathDrawingMode.Stroke);

			// Convert to image
			CGImage img = context.ToImage ();
			UIImage image = new UIImage (img);

			// Free memory
			context.Dispose ();
			BitmapData = null;
			img.Dispose ();

			// Return resulting image
			return image;
		}

		/// <summary>
		/// Hues the bar image.
		/// </summary>
		/// <returns>The bar image.</returns>
		/// <param name="index">Index.</param>
		/// <param name="hsv">Hsv.</param>
		public static UIImage HueBarImage(ACHueBarComponentIndex index, HSVColor hsv) {

			// Calculate metrics
			int w = 256, h = 1;
			int bytesPerRow = w * 4;
			int bitmapByteCount = bytesPerRow * h;
			int bytesPerPixel = bytesPerRow / w;
			int bitsPerComponent = 8;

			// Allocate memory for the image's bitmap
			Byte[] BitmapData = new Byte[bitmapByteCount];

			// Create graphics context
			CGBitmapContext context = CreateBGRxImageContext (w, h, BitmapData);

			// Create storage for color manipulation
			int byteIndex = 0;
			RGBColor rgb;
			HSVColor hsvAdjusted;

			// Draw color bar
			for (int x = 0; x < 256; ++x) {
				// Calculate byte index
				byteIndex = (x * bytesPerPixel);

				// Calculate new color space
				switch (index) {
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
				hsvAdjusted = new HSVColor (hsv.Hue * 360.0f, hsv.Saturation, hsv.Value);
				rgb = hsvAdjusted.RGB;

				// Insert color
				BitmapData[byteIndex] = (Byte)(rgb.Blue * 255f);
				BitmapData[byteIndex+1] = (Byte)(rgb.Green * 255f);
				BitmapData[byteIndex+2] = (Byte)(rgb.Red * 255f);
			}

			// Convert to image
			CGImage img = context.ToImage ();
			UIImage image = new UIImage (img);

			// Free memory
			context.Dispose ();
			BitmapData = null;
			img.Dispose ();

			// Return resulting image
			return image;
		}
		#endregion
	}
}

