using System;

namespace ActionComponents
{
	/// <summary>
	/// Defines the types of edits that can be done on a <see cref="ActionComponents.ACTableItem"/> as defined by iOS to assist in cross-platform
	/// development.
	/// </summary>
	/// <remarks>Cell editing is currently not supported for Android. These types were added for compatibitily with iOS ONLY! Available in ActionPack Business or
	/// Enterprise only.</remarks>
	public enum UITableViewCellEditingStyle
	{
		/// <summary>
		/// No editing control is displayed in the cell (this is the default).
		/// </summary>
		None,
		/// <summary>
		/// A red circle with a white minus sign is displayed, to indicate the cell can be deleted.
		/// </summary>
		Delete,
		/// <summary>
		/// A green circle with a white plus sign is displayed, indicating a new row can be inserted.
		/// </summary>
		Insert,
	}
}

