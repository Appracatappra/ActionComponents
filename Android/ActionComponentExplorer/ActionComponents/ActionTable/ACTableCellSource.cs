using System;

namespace ActionComponents
{
	/// <summary>
	/// Defines the source of a given <see cref="ActionComponents.ACTableCell"/> as a
	/// <see cref="ActionComponents.ACTableSection"/> <c>Header</c> or <c>Footer</c> property or
	/// a <see cref="ActionComponents.ACTableItem"/> 
	/// </summary>
	/// <remarks>Available in ActionPack Business or Enterprise only.</remarks>
	public enum ACTableCellSource
	{
		/// <summary>
		/// The source is a <see cref="ActionComponents.ACTableSection"/> <c>Header</c> 
		/// </summary>
		Header,
		/// <summary>
		/// The source is a <see cref="ActionComponents.ACTableSection"/> <c>Footer</c> 
		/// </summary>
		Footer,
		/// <summary>
		/// The source is a <see cref="ActionComponents.ACTableItem"/> 
		/// </summary>
		Item
	}
}

