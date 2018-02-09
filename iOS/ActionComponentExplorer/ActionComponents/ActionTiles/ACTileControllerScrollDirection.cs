using System;

namespace ActionComponents
{
	/// <summary>
	/// Controls the direction that <c>UIActionTileGroup</c>s will be scrolled
	/// inside their parent <c>UIActionTileController</c>  
	/// </summary>
	public enum ACTileControllerScrollDirection
	{
		/// <summary>
		/// The <c>UIActionTileGroup</c>s will scroll left to right 
		/// </summary>
		Horizontal,
		/// <summary>
		/// The <c>UIActionTileGroup</c>s will scroll top to bottom 
		/// </summary>
		Vertical
	}
}

