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
using Android.Animation;

namespace ActionComponents
{
	/// <summary>
	/// The <see cref="ActionComponents.ACImage"/> is a static class that adds several helpful utilities for
	/// dealing with images as <c>Bitmaps</c>.
	/// </summary>
	/// <remarks>The <see cref="ActionComponents.ACImage"/> attempts to maintain the same calling structure
	/// and functionality with the iOS version of the <c>ActionPack</c> to assist in building cross platform mobile applications. 
	/// Available in ActionPack Business or Enterprise only.</remarks>
	public static class ACImage
	{
		#region Public Static Methods
		/// <summary>
		/// Loads the image from the given filename and path
		/// </summary>
		/// <returns>The image as a <c>Bitmap</c> or <c>null</c> on error </returns>
		/// <param name="filename">The full path and filename of the image to load</param>
		/// <remarks>The resulting bitmap will allocate its pixels such that they can be purged if the system needs to reclaim memory.</remarks>
		public static Bitmap FromFile(string filename){
			//Trap all errors
			try {
				BitmapFactory.Options opts = new BitmapFactory.Options() {InPurgeable = true};
				return BitmapFactory.DecodeFile (filename,opts);
			}
			catch {
				//Return null on error
				return null;
			}
		}

		/// <summary>
		/// Queries the given image file and returns information about the file in a <see cref="ActionComponents.ACImageInfo"/> 
		/// </summary>
		/// <returns>The info from file.</returns>
		/// <param name="filename">Filename.</param>
		/// <remarks>This method queries the image file without actually loading it into memory</remarks>
		public static ACImageInfo ImageInfoFromFile(string filename){
			//Trap all errors
			try{
				//Query image file
				BitmapFactory.Options opts = new BitmapFactory.Options() {InJustDecodeBounds = true};
				BitmapFactory.DecodeFile (filename,opts);

				//Return information about the image file
				return new ACImageInfo(opts.OutHeight, opts.OutWidth, opts.OutMimeType);
			}
			catch {
				//Return null on error
				return null;
			}
		}

		/// <summary>
		/// Loads the image from the given filename and path resampling it to the requested height/width
		/// </summary>
		/// <returns>The image as a <c>Bitmap</c> or <c>null</c> on error </returns>
		/// <param name="filename">The full path and filename of the image to load</param>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		/// <remarks>The resulting bitmap will allocate its pixels such that they can be purged if the system needs to reclaim memory.</remarks>
		public static Bitmap FromFile(string filename, int width, int height){
			//Trap all errors
			try {
				BitmapFactory.Options opts = new BitmapFactory.Options() {InPurgeable = true};

				//Calculate in sample size
				opts.InSampleSize= CalculateInSampleSize(ImageInfoFromFile(filename),width,height);

				return BitmapFactory.DecodeFile (filename,opts);
			}
			catch {
				//Return null on error
				return null;
			}
		}

		/// <summary>
		/// Loads the image from the given filename and path with the given options
		/// </summary>
		/// <returns>The image as a <c>Bitmap</c> or <c>null</c> on error</returns>
		/// <param name="filename">The full path and filename of the image to load</param>
		/// <param name="options">Options.</param>
		public static Bitmap FromFile(string filename, BitmapFactory.Options options){
			//Trap all errors
			try {
				return BitmapFactory.DecodeFile (filename, options);
			}
			catch {
				//Return null on error
				return null;
			}
		}

		/// <summary>
		/// Loads the image from the given resources and resource ID
		/// </summary>
		/// <returns>The image as a <c>Bitmap</c> or <c>null</c> on error</returns>
		/// <param name="resources">Resources.</param>
		/// <param name="resourceID">Resource ID.</param>
		/// <remarks>The resulting bitmap will allocate its pixels such that they can be purged if the system needs to reclaim memory.</remarks>
		public static Bitmap FromResource(Android.Content.Res.Resources resources, int resourceID){
			//Trap all errors
			try {
				BitmapFactory.Options opts = new BitmapFactory.Options() {InPurgeable = true};
				return BitmapFactory.DecodeResource (resources, resourceID, opts);
			}
			catch {
				//Return null on error
				return null;
			}
		}

		/// <summary>
		/// Queries the image resource and returns information about it in a <see cref="ActionComponents.ACImageInfo"/> 
		/// </summary>
		/// <returns>The info from resource.</returns>
		/// <param name="resources">Resources.</param>
		/// <param name="resourceID">Resource ID.</param>
		/// <remarks>This method queries the image resource without actually loading it into memory</remarks>
		public static ACImageInfo ImageInfoFromResource(Android.Content.Res.Resources resources, int resourceID){
			//Trap all errors
			try{
				//Query image file
				BitmapFactory.Options opts = new BitmapFactory.Options() {InJustDecodeBounds = true};
				BitmapFactory.DecodeResource (resources, resourceID, opts);

				//Return information about the image file
				return new ACImageInfo(opts.OutHeight, opts.OutWidth, opts.OutMimeType);
			}
			catch {
				//Return null on error
				return null;
			}
		}

		/// <summary>
		/// Loads the image from the given resources and resource ID resampling it to the requested height/width
		/// </summary>
		/// <returns>The image as a <c>Bitmap</c> or <c>null</c> on error</returns>
		/// <param name="resources">Resources.</param>
		/// <param name="resourceID">Resource ID.</param>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		/// <remarks>The resulting bitmap will allocate its pixels such that they can be purged if the system needs to reclaim memory.</remarks>
		public static Bitmap FromResource(Android.Content.Res.Resources resources, int resourceID, int width, int height){
			//Trap all errors
			try {
				BitmapFactory.Options opts = new BitmapFactory.Options() {InPurgeable = true};

				//Calculate in sample size
				opts.InSampleSize = CalculateInSampleSize(ImageInfoFromResource(resources, resourceID), width, height);

				return BitmapFactory.DecodeResource (resources, resourceID, opts);
			}
			catch {
				//Return null on error
				return null;
			}
		}

		/// <summary>
		/// Loads the image from the given resources and resource ID with the given options
		/// </summary>
		/// <returns>The image as a <c>Bitmap</c> or <c>null</c> on error</returns>
		/// <param name="resources">Resources.</param>
		/// <param name="resourceID">Resource ID.</param>
		/// <param name="options">Options.</param>
		public static Bitmap FromResource(Android.Content.Res.Resources resources, int resourceID, BitmapFactory.Options options){
			//Trap all errors
			try {
				return BitmapFactory.DecodeResource (resources, resourceID, options);
			}
			catch {
				//Return null on error
				return null;
			}
		}

		/// <summary>
		/// Calculates the sampling factor for an image being loaded into memory and down-sampled at the same time
		/// </summary>
		/// <returns>The in sample size.</returns>
		/// <param name="options">Options.</param>
		/// <param name="reqWidth">Req width.</param>
		/// <param name="reqHeight">Req height.</param>
		public static int CalculateInSampleSize(BitmapFactory.Options options, int reqWidth, int reqHeight)
		{
			// Raw height and width of image
			var height = (float)options.OutHeight;
			var width = (float)options.OutWidth;
			var inSampleSize = 1D;

			// Calculate the sampling factor
			if (height > reqHeight || width > reqWidth)
			{
				inSampleSize = width > height
					? height/reqHeight
						: width/reqWidth;
			}

			return (int) inSampleSize;
		}

		/// <summary>
		/// Calculates the sampling factor for an image being loaded into memory and down-sampled at the same time
		/// </summary>
		/// <returns>The in sample size.</returns>
		/// <param name="info">Info.</param>
		/// <param name="reqWidth">Req width.</param>
		/// <param name="reqHeight">Req height.</param>
		public static int CalculateInSampleSize(ACImageInfo info, int reqWidth, int reqHeight)
		{
			// Raw height and width of image
			var height = (float)info.Height;
			var width = (float)info.Width;
			var inSampleSize = 1D;

			// Calculate the sampling factor
			if (height > reqHeight || width > reqWidth)
			{
				inSampleSize = width > height
					? height/reqHeight
						: width/reqWidth;
			}

			return (int) inSampleSize;
		}

		/// <summary>
		/// Rotates the given image <c>Bitmap</c> about it center axis to the given degrees
		/// </summary>
		/// <returns>The image.</returns>
		/// <param name="bitmap">Bitmap.</param>
		/// <param name="degrees">Degrees.</param>
		/// <remarks>Negative degrees rotate counter-clockwise</remarks>
		public static Bitmap RotateImage(Bitmap bitmap, int degrees){

			// Create a matrix to rotate the image
			Matrix matrix = new Matrix();
			matrix.PostRotate(degrees);

			// Create the new image rotated using the provided matrix
			Bitmap rotatedBmp = Bitmap.CreateBitmap(bitmap, 0, 0, bitmap.Width, bitmap.Height, matrix, true);    

			return rotatedBmp;
		}
		#endregion 
	}
}

