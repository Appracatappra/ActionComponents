using System;

namespace ActionComponents
{
	/// <summary>
	/// Defines the source of an image displayed in an <see cref="ActionComponents.ACTableItem"/> 
	/// </summary>
	/// <remarks>Available in ActionPack Business or Enterprise only.</remarks>
	public enum ACTableItemImageSource
	{
		/// <summary>
		/// No image
		/// </summary>
		None,
		/// <summary>
		/// Loaded from the file specified in the <c>imageFile</c> property of the <see cref="ActionComponents.ACTableItem"/> 
		/// </summary>
		FromFile,
		/// <summary>
		/// The <see cref="ActionComponents.ACTableItem"/> will request the caller custom draw the image by raising the <c>RequestCustomImage</c> 
		/// event
		/// </summary>
		CustomDrawn
	}
}

