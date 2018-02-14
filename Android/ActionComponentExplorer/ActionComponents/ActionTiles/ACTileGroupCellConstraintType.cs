using System;

namespace ActionComponents
{
	/// <summary>
	/// Defines how the <c>rows</c> and <c>columns</c> inside a <c>ACTileGroup</c> will grow
	/// based on the number of the contained <c>ACTile</c>s and the size of the parent 
	/// <c>ACTileController</c> that holds the controller.  
	/// </summary>
	public enum ACTileGroupCellConstraintType
	{
		/// <summary>
		/// Calculated from the number of <c>rows</c> or <c>columns</c>
		/// </summary>
		Flexible,

		/// <summary>
		/// A fixed number of <c>rows</c> or <c>columns</c>
		/// </summary>
		Fixed,

		/// <summary>
		/// Fills the parent <c>ACTileController</c> based on the given indent padding
		/// values
		/// </summary>
		FitParent
	}
}

