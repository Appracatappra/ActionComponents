using System;

namespace ActionComponents
{
	/// <summary>
	/// Defines the style of a <c>ACTile</c> and controls what information it will
	/// display and how that information will be rendered.
	/// </summary>
	public enum ACTileStyle
	{
		/// <summary>
		/// A <c>ACTile</c> with a large central icon and optional title on the bottom.
		/// </summary>
		Default,

		/// <summary>
		/// A <c>ACTile</c> with a central block of text, and optional title and 
		/// an optional title image.
		/// </summary>
		DescriptionBlock,

		/// <summary>
		/// A <c>ACTile</c> with an icon 32 x 32 pixel icon in the lower left hand corner
		/// and an optional title.
		/// </summary>
		CornerIcon,

		/// <summary>
		/// A <c>ACTile</c> with a large central icon and optional title on the top line.
		/// </summary>
		TopTitle,

		/// <summary>
		/// A <c>ACTile</c> with a large image and optional title on the bottom.
		/// </summary>
		BigPicture,

		/// <summary>
		/// A <c>ACTile</c> with a large image and optional title. This style ONLY work with a <c>ACTileSize</c> of
		/// <c>DoubleHorizontal</c>.
		/// </summary>
		Scene,

		/// <summary>
		/// A <c>ACTile</c> with a large image in the upper left hand corner and optional title and subtitle at the 
		/// bottom. This style ONLY work with a <c>ACTileSize</c> of <c>Quad</c>.
		/// </summary>
		Accessory,

		/// <summary>
		/// Allows the user to custom draw the <c>ACTile</c>.
		/// </summary>
		CustomDrawn
	}
}

