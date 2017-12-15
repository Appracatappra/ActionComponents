using System;

namespace ActionComponents
{
	/// <summary>
	/// Defines the built-in <c>ListView</c> item cell types that can be used on a <see cref="ActionComponents.ACTableItem"/> plus a set
	/// of pseudo-types that match the iOS <c>UITableViewCellStyle</c> types for cross platform development
	/// </summary>
	/// <remarks>Aside from having the direct named properties as the standard Android <c>ListView</c> cell types, a set of properties named to match the
	/// standard iOS <c>UITableViewCellStyle</c> have been created to assist in building cross-platform mobile apps. The iOS style properties will be
	/// automatically matched to their Android counterparts by the <see cref="ActionComponents.ACTableViewDataSource"/>.
	/// Available in ActionPack Business or Enterprise only. </remarks>
	public enum UITableViewCellStyle
	{
		/// <summary>
		/// A single line of text with an optional image on the left side
		/// </summary>
		/// <remarks>If an image is attached to the parent <see cref="ActionComponents.ACTableItem"/> then the Android <c>ActivityListItem</c> 
		/// type will be used, else the <c>SimpleListItem1</c> type will be used</remarks>
		Default,
		/// <summary>
		/// A large line of header text with a smaller line of subtext
		/// </summary>
		/// <remarks>This will map to the Android <c>SimpleListItem2</c> cell type </remarks>
		Subtitle,
		/// <summary>
		/// Two lines of text
		/// </summary>
		/// <remarks>This will map to the Android <c>TwoLineListItem</c> cell type</remarks>
		Value1,
		/// <summary>
		/// Two lines of text
		/// </summary>
		/// <remarks>This will map to the Android <c>TwoLineListItem</c> cell type</remarks>
		Value2,
		/// <summary>
		/// The standard Android <c>SimpleListItem1</c> cell type with one line of text
		/// </summary>
		SimpleListItem1,
		/// <summary>
		/// The standard Android <c>SimpleListItem2</c> cell type with one large header text and a smaller sub text item
		/// </summary>
		SimpleListItem2,
		/// <summary>
		/// The standard Android <c>TwoLineListItem</c> cell type with two lines of text
		/// </summary>
		TwoLineListItem,
		/// <summary>
		/// The standard Android <c>ActivityListItem</c> cell type with an image on the left side and a single line of text
		/// </summary>
		ActivityListItem,
		/// <summary>
		/// Forces the <see cref="ActionComponents.ACTableItem"/> to be rendered in a
		/// <see cref="ActionComponents.ACTableCell"/> used to provide compatibility with the default iOS table row type
		/// </summary>
		ActionTableCell
	}
}

