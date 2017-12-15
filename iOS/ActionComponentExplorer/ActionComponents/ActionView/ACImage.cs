using System;
using System.Drawing;
using System.IO;

using Foundation;
using UIKit;
using CoreGraphics;

namespace ActionComponents
{
	/// <summary>
	/// <see cref="ActionComponents.ACImage"/> adds several helpful routines for working with <c>UIImages</c> including the ability to
	/// dectect iPhone 5 (or greater) images in the form name<c>-568h@2x</c>.ext and automatically load them in place of name.ext files. It includes methods to help
	/// with the rotation of images as well.
	/// </summary>
	/// <remarks>Available in ActionPack Business or Enterprise only.</remarks>
	[Register("ACImage")]
	public static class ACImage 
	{
		#region Public Static Methods
		/// <summary>
		/// Returns a <c>UIImage</c> from the given filename or <c>null</c> if the image cannot be loaded
		/// </summary>
		/// <returns>The <c>UIImage</c> for the specified filename or <c>null</c> if not found or error</returns>
		/// <param name="filename">The full name and path of the image to load</param>
		/// <remarks>If running on an iPhone 5 (or greater) and asking for an image file in the form name.ext and a file in same directory with a name in the format name<c>-568h@2x</c>.ext exists, it
		/// will automatically be loaded instead. The same holds true for devices with <c>Retina Displays</c>, if ask for a file name.ext and a file name<c>@2x</c>.ext exists,
		/// it will automatically be loaded instead.</remarks>
		public static UIImage FromFile(string filename) {
			UIImage image;
			string srcFilename = filename;

			//Trap all errors
			try {
				//Decompose image path into parts
				string imagePath = Path.GetDirectoryName(filename);
				string imageFile = Path.GetFileNameWithoutExtension(filename);
				string imageExt = Path.GetExtension(filename);

				//We we running on an iPhone 5?
				if (iOSDevice.is568h) {
					//Yes, recompose to load taller image
					filename=String.Format("{0}-568h@2x{1}",imageFile,imageExt);
					filename=Path.Combine(imagePath,filename);
				} else if (iOSDevice.isRetina) {
					//Yes, recompose for 2x image
					filename=String.Format("{0}@2x{1}",imageFile,imageExt);
					filename=Path.Combine(imagePath,filename);
				} else if (iOSDevice.iSHD) {
					//Yes, recompose for 2x image
					filename=String.Format("{0}@3x{1}",imageFile,imageExt);
					filename=Path.Combine(imagePath,filename);
				}

				//Load requested image
				if (File.Exists(filename)) {
					image=UIImage.FromFile(filename);
				} else {
					image=UIImage.FromFile(srcFilename);
				}
			}
			catch {
				//Set the image to null on error
				image = null;
			}

			//Return adjusted image
			return image;
		}

		/// <summary>
		/// Given a value in degrees, return the value in radians
		/// </summary>
		/// <param name="degrees">Degrees to convert</param>
		/// <returns>The value in radians</returns>
		public static nfloat DegreesToRadians(nfloat degrees){
			//Degrees specified in 360?
			if (degrees>180) degrees-=360;

			//Convert to radians and return
			return degrees*((float)Math.PI/180f);
		}

		/// <summary>
		/// Given a source <c>UIImage</c>, return a new <c>UIImage</c> containing the source image rotated
		/// to the given radian value
		/// </summary>
		/// <param name="source">
		/// The source image to rotate
		/// </param>
		/// <param name="degrees">
		/// The degrees to rotate the image to
		/// </param>
		/// <returns>
		/// A new <c>UIImage</c> containing the image rotated to the given degrees
		/// </returns>
		public static UIImage RotateImage(this UIImage source, float degrees){
			UIView rotatedViewBox=new UIView(new CGRect(0,0,source.Size.Width,source.Size.Height));
			CGAffineTransform t=CGAffineTransform.MakeRotation(ACImage.DegreesToRadians(degrees));
			rotatedViewBox.Transform=t;
			//CGSize rotatedSize=rotatedViewBox.Frame.Size;
			CGSize rotatedSize=new CGSize(rotatedViewBox.Frame.Size.Width+10,rotatedViewBox.Frame.Size.Height+10);
			rotatedViewBox.Dispose();
			rotatedViewBox=null;

			UIGraphics.BeginImageContext(rotatedSize);
			CGContext bitmap=UIGraphics.GetCurrentContext();

			bitmap.TranslateCTM(rotatedSize.Width/2,rotatedSize.Height/2);
			bitmap.RotateCTM(ACImage.DegreesToRadians(degrees));

			bitmap.ScaleCTM(1.1f,-1.1f);
			bitmap.DrawImage(new CGRect(-source.Size.Width/2,-source.Size.Height/2,source.Size.Width,source.Size.Height),source.CGImage);

			UIImage image=UIGraphics.GetImageFromCurrentImageContext();
			UIGraphics.EndImageContext();
			bitmap.Dispose();
			bitmap=null;

			return image;
		}
		#endregion 
	}
}

