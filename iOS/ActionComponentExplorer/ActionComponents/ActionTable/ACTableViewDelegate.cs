using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using Foundation;
using UIKit;
using CoreGraphics;
using ObjCRuntime;

namespace ActionComponents 
{
	/// <summary>
	/// The <see cref="ActionComponents.ACTableViewDelegate"/> handles all user interaction with the <c>UITableView</c> being controlled by it's parent
	/// <see cref="ActionComponents.ACTableViewController"/>. Events are processed and passed to the 
	/// <see cref="ActionComponents.ACTableSection"/>s and <see cref="ActionComponents.ACTableItem"/>s contained within the
	/// <see cref="ActionComponents.ACTableViewController"/>'s <see cref="ActionComponents.ACTableViewDataSource"/>. As a result
	/// you may never need to create a custom version of this class to support your <c>UITableViews</c>.
	/// </summary>
	/// <remarks>Available in ActionPack Business or Enterprise only.</remarks>
	[Register("ACTableViewDelegate")]
	public class ACTableViewDelegate : UITableViewDelegate
	{
		#region Private Variables
		private ACTableViewController _controller;
		#endregion 

		#region Computed Properties
		/// <summary>
		/// Gets the <see cref="ActionComponents.ACTableViewController"/> that this <see cref="ActionComponents.ACTableViewDelegate"/>
		/// is attached to.
		/// </summary>
		/// <value>The controller.</value>
		public ACTableViewController controller{
			get{ return _controller;}
		}
		#endregion 

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="ActionComponents.ACTableViewDelegate"/> class.
		/// </summary>
		public ACTableViewDelegate ()
		{
		}

		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="ActionComponents.ACTableViewDelegate"/> class.
		/// </summary>
		/// <param name="controller">Controller.</param>
		public ACTableViewDelegate (ACTableViewController controller)
		{
			//Initialize
			this._controller = controller;
		}
		#endregion 

		#region Override Methods
		/// <Docs>The table view containing the row/cell accessory that has been tapped.</Docs>
		/// <summary>
		/// Accessories the button tapped.
		/// </summary>
		/// <param name="tableView">Table view.</param>
		/// <param name="indexPath">Index path.</param>
		public override void AccessoryButtonTapped (UITableView tableView, NSIndexPath indexPath)
		{
			//Grab selected row
			ACTableItem item = controller.dataSource.sections[indexPath.Section].items[indexPath.Row];

			//Inform caller
			item.RaiseAccessoryButtonTapped ();
			controller.RaiseAccessoryButtonTapped (item);
		}

		/// <Docs>Table view containing the row.</Docs>
		/// <see cref="M:MonoTouch.UIKit.UIResponder.Copy(MonoTouch.Foundation.NSObject)"></see>
		/// <see cref="M:MonoTouch.UIKit.UIResponder.Paste(MonoTouch.Foundation.NSObject)"></see>
		/// <param name="indexPath">Location of the row.</param>
		/// <summary>
		/// Whether the editing menu should omit the Copy or Paste command for the specified row.
		/// </summary>
		/// <see langword="true"></see>
		/// <paramref name="action"></paramref>
		/// <see langword="false"></see>
		/// <see langword="false"></see>
		/// <returns><c>true</c> if this instance can perform action the specified tableView action indexPath sender; otherwise, <c>false</c>.</returns>
		/// <param name="tableView">Table view.</param>
		/// <param name="action">Action.</param>
		/// <param name="sender">Sender.</param>
		public override bool CanPerformAction (UITableView tableView, Selector action, NSIndexPath indexPath, NSObject sender)
		{
			//Grab selected row
			ACTableItem item = controller.dataSource.sections[indexPath.Section].items[indexPath.Row];

			//Query item
			return item.RaiseCanPerformAction ();
		}

		/// <Docs>Table view being edited.</Docs>
		/// <summary>
		/// Dids the end editing.
		/// </summary>
		/// <param name="tableView">Table view.</param>
		/// <param name="indexPath">Index path.</param>
		public override void DidEndEditing (UITableView tableView, NSIndexPath indexPath)
		{
			//Grab selected row
			ACTableItem item = controller.dataSource.sections[indexPath.Section].items[indexPath.Row];

			//Inform caller
			item.RaiseDidEndEditing ();
			controller.RaiseDidEndEditing (item);
		}

		/// <Docs>Table view that is going to be editable.</Docs>
		/// <summary>
		/// Called for each row being displayed by the table view, to determine what editing style to use for that row.
		/// </summary>
		/// <paramref name="indexPath"></paramref>
		/// <remarks>When the table view enters editing mode, this method allows the editing style to be set for each row.</remarks>
		/// <see cref="T:MonoTouch.UIKit.UITableViewCell"></see>
		/// <see cref="P:MonoTouch.UIKit.UITableViewCell.Editing"></see>
		/// <see langword="true"></see>
		/// <see cref="F:MonoTouch.UIKit.UITableViewCellEditingStyle.Delete"></see>
		/// <returns>The style for row.</returns>
		/// <param name="tableView">Table view.</param>
		/// <param name="indexPath">Index path.</param>
		public override UITableViewCellEditingStyle EditingStyleForRow (UITableView tableView, NSIndexPath indexPath)
		{
			//Grab selected row
			ACTableItem item = controller.dataSource.sections[indexPath.Section].items[indexPath.Row];

			//Query item
			return item.RaiseEditingStyleForItem ();
		}

		/// <Docs>Table view.</Docs>
		/// <summary>
		/// Gets the height for footer.
		/// </summary>
		/// <returns>The height for footer.</returns>
		/// <param name="tableView">Table view.</param>
		/// <param name="section">Section.</param>
		public override nfloat GetHeightForFooter (UITableView tableView, nint section)
		{
			//Grab selected section
			ACTableSection tableSection = controller.dataSource.sections[(int)section];

			//Cascade height selection
			if (tableSection.footer == null) {
				return 0f;
			} if (tableSection.DefinesFooterHeight()) {
				return tableSection.GetFooterHeight();
			} else {
				//Use default value from table view controller
				return controller.footerHeight;
			}
		}

		/// <Docs>Table view.</Docs>
		/// <summary>
		/// Gets the height for header.
		/// </summary>
		/// <returns>The height for header.</returns>
		/// <param name="tableView">Table view.</param>
		/// <param name="section">Section.</param>
		public override nfloat GetHeightForHeader (UITableView tableView, nint section)
		{
			//Grab selected section
			ACTableSection tableSection = controller.dataSource.sections[(int)section];

			//Cascade height selection
			if (tableSection.header == null) {
				return 0f;
			} else if (tableSection.DefinesHeaderHeight()) {
				return tableSection.GetHeaderHeight();
			} else {
				//Use default value from table view controller
				return controller.headerHeight;
			}
		}

		/// <Docs>Table view.</Docs>
		/// <summary>
		/// Gets the height for row.
		/// </summary>
		/// <returns>The height for row.</returns>
		/// <param name="tableView">Table view.</param>
		/// <param name="indexPath">Index path.</param>
		public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			//Grab selected section
			ACTableSection tableSection = controller.dataSource.sections[indexPath.Section];
			ACTableItem item = controller.dataSource.sections[indexPath.Section].items[indexPath.Row];

			//Cascade height selection
			if (item.DefinesItemHeight()) {
				return item.GetItemHeight();
			} else if (tableSection.DefinesItemHeight()) {
				return tableSection.GetItemHeight(item);
			} else {
				//Use default value from table view controller
				return controller.rowHeight;
			}
		}

		/// <Docs>Table view containing the row.</Docs>
		/// <summary>
		/// Rows the selected.
		/// </summary>
		/// <param name="tableView">Table view.</param>
		/// <param name="indexPath">Index path.</param>
		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			//Grab selected row
			ACTableItem item = controller.dataSource.sections[indexPath.Section].items[indexPath.Row];

			//Inform caller
			item.RaiseItemSelected ();
			if (item.canSelect) controller.RaiseItemSelected (item);
		}

		/// <Docs>To be added.</Docs>
		/// <summary>
		/// To be added.
		/// </summary>
		/// <remarks>To be added.</remarks>
		/// <returns><c>true</c>, if highlight row was shoulded, <c>false</c> otherwise.</returns>
		/// <param name="tableView">Table view.</param>
		/// <param name="rowIndexPath">Row index path.</param>
		public override bool ShouldHighlightRow (UITableView tableView, NSIndexPath rowIndexPath)
		{
			//Grab selected row
			ACTableItem item = controller.dataSource.sections[rowIndexPath.Section].items[rowIndexPath.Row];

			//Is highlighting allowed?
			if (_controller.allowsHighlight) {
				//Query item
				return item.RaiseShouldHighlightItem ();
			} else {
				//No highlighting allowed
				return false;
			}
		}

		/// <Docs>Table view containing the row.</Docs>
		/// <summary>
		/// Shoulds the show menu.
		/// </summary>
		/// <returns><c>true</c>, if show menu was shoulded, <c>false</c> otherwise.</returns>
		/// <param name="tableView">Table view.</param>
		/// <param name="rowAtindexPath">Row atindex path.</param>
		public override bool ShouldShowMenu (UITableView tableView, NSIndexPath rowAtindexPath)
		{
			//Grab selected row
			ACTableItem item = controller.dataSource.sections[rowAtindexPath.Section].items[rowAtindexPath.Row];

			//Query item
			return item.RaiseShouldShowMenu ();
		}
		#endregion 
	}
}

