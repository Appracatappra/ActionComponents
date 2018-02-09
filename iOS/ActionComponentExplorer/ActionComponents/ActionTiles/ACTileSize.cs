using System;

namespace ActionComponents
{
	/// <summary>
	/// Defines the number of <c>cells</c> required to display a given <c>ACTile</c> within it's
	/// parent <c>ACTileGroup</c>  
	/// </summary>
	/// <remarks>A <c>cell</c> is the unit of measure with the "virtual layout grid" that <c>ACTile</c>
	/// are laid out in.</remarks>
	public enum ACTileSize
	{
		/// <summary>
		/// A single <c>cell</c> is required to display the given <c>ACTile</c>. 
		/// </summary>
		Single,

		/// <summary>
		/// Two horizontal <c>cells</c> are required to display the given <c>ACTile</c>. 
		/// </summary>
		DoubleHorizontal,

		/// <summary>
		/// Four horizontal <c>cells</c> are required to display the given <c>ACTile</c>. 
		/// </summary>
		QuadHorizontal,

		/// <summary>
		/// Two vertical <c>cells</c> are required to display the given <c>ACTile</c>. 
		/// </summary>
		DoubleVertical,

		/// <summary>
		/// Four <c>cells</c>, two horizontal and two vertical, are required to display the given <c>ACTile</c>. 
		/// </summary>
		Quad,

		/// <summary>
		/// Allows the developer to customize the tile size in rows and columns used.
		/// </summary>
		Custom
	}
}

