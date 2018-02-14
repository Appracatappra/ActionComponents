using System;

namespace ActionComponents
{
	/// <summary>
	/// Defines the type of a <c>ACTileGroup</c> based on the 
	/// <c>ACTileControllerScrollDirection</c> and the size of the 
	/// <c>ACTileController</c> that is hosting the group. 
	/// </summary>
	/// <remarks>This property will control how the group behaves.</remarks>
	public enum ACTileGroupType
	{
		/// <summary>
		/// For <c>Horizontal</c> <c>ACTileControllerScrollDirection</c> the <c>ACTileGroup</c>
		/// will fill the parent <c>ACTileController</c> vertically and grow in width based on the number of
		/// <c>ACTile</c>s it contains. For <c>Vertical</c> this is inverted, it will fill the parent
		/// <c>ACTileController</c> horizontally and grow vertically.     
		/// </summary>
		ExpandingGroup,

		/// <summary>
		/// The <c>ACTileGroup</c> will be one "page" wide and high, filling the parent
		/// <c>ACTileController</c> both horizontally and vertically.  
		/// </summary>
		PageGroup,

		/// <summary>
		/// For <c>Horizontal</c> <c>ACTileControllerScrollDirection</c> the <c>ACTileGroup</c>
		/// will fill the parent <c>ACTileController</c> horizontally and have a fixed height based on the given number of rows. 
		/// For <c>Vertical</c> this is inverted, it will fill the parent <c>ACTileController</c> vertically and have a fixed width based on the
		/// given number of columns.     
		/// </summary>
		FixedSizePageGroup,

		/// <summary>
		/// The <c>ACTileGroup</c> will have a fixed height and width based on the given number of rows and columns. 
		/// </summary>
		FixedSizeGroup
	}
}

