using System;

namespace ActionComponents
{
	/// <summary>
	/// The <see cref="ActionComponents.ACImageInfo"/> holds information about an image file that has been decoded by the
	/// <see cref="ActionComponents.ACImage"/> static class. 
	/// </summary>
	/// <remarks>Available in ActionPack Business or Enterprise only.</remarks>
	 public class ACImageInfo
	{

		#region Computed Properties
		/// <summary>
		/// Gets or sets the height of the image
		/// </summary>
		/// <value>The height.</value>
		public int Height { get; set; }

		/// <summary>
		/// Gets or sets the width of the image
		/// </summary>
		/// <value>The width.</value>
		public int Width { get; set; }

		/// <summary>
		/// Gets or sets the mime type of the image
		/// </summary>
		/// <value>The type of the MIME.</value>
		public string MimeType { get; set; }
		#endregion 

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACImageInfo"/> class.
		/// </summary>
		public ACImageInfo ()
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACImageInfo"/> class.
		/// </summary>
		/// <param name="height">Height.</param>
		/// <param name="width">Width.</param>
		/// <param name="mimeType">MIME type.</param>
		public ACImageInfo(int height, int width, string mimeType){

			// Initialize
			this.Height = height;
			this.Width = width;
			this.MimeType = mimeType;

		}
		#endregion 
	}
}

