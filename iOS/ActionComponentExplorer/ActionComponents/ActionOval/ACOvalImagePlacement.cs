using System;

namespace ActionComponents
{
	/// <summary>
	/// Defines the position of an image displayed as the background of a <see cref="ActionComponents.ACOval"/>
	/// </summary>
	public enum ACOvalImagePlacement
	{
		/// <summary>
		/// Pin the image to the top left corner of the the Oval
		/// </summary>
		TopLeft,

		/// <summary>
		/// Center the image in the oval
		/// </summary>
		Center,

		/// <summary>
		/// Scales the image to fit in the oval
		/// </summary>
		ScaleToFit,

		/// <summary>
		/// Allows the user to specify the X and Y position and the Height and Width of the image.
		/// </summary>
		FreeForm
	}
}

