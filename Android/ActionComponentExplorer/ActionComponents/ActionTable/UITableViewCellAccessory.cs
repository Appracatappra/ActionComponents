using System;

namespace ActionComponents
{
	/// <summary>
	/// Defines the standard Android accessories that can be added to a <see cref="ActionComponents.ACTableItem"/> and a set of pseudo-types that match
	/// the standard iOS <c>UITableViewCellAccessory</c> types to assist in cross-platform development
	/// </summary>
	/// <remarks>Where possible, the iOS types will be automatically mapped to the Android types by the <see cref="ActionComponents.ACTableViewDataSource"/>.
	/// Available in ActionPack Business or Enterprise only. </remarks>
	public enum UITableViewCellAccessory
	{
		/// <summary>
		/// No accessory
		/// </summary>
		/// <remarks>This is the default</remarks>
		None,
		/// <summary>
		/// A checkmark accessory
		/// </summary>
		/// <remarks>Displays a <c>checkmark</c> by this item in the list </remarks>
		Checkmark,
		/// <summary>
		/// A square button containing a chevron (right-pointing arrow) is displayed on the right side of the cell. This accessory tracks touches separately from the rest of the cell.
		/// </summary>
		/// <remarks>Simulates the iOS <c>DetailedDisclosureButton</c> on Android to assist in cross-platform development</remarks>
		DetailedDisclosureButton,
		/// <summary>
		/// A chevron (right-pointing arrow) is displayed on the right side of the cell. This accessory does not track touches. 
		/// </summary>
		/// <remarks>Simulates the iOS <c>DetailedDisclosureButton</c> on Android to assist in cross-platform development</remarks>
		DisclosureIndicator
	}
}

