// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ActionComponentExplorer
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		UIKit.UIScrollView ScrollView { get; set; }

		[Outlet]
		ActionComponents.ACTileController tileController { get; set; }

		[Outlet]
		ActionComponents.ACTray TrayOne { get; set; }

		[Outlet]
		ActionComponents.ACTray TrayThree { get; set; }

		[Outlet]
		ActionComponents.ACTray TrayTwo { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (tileController != null) {
				tileController.Dispose ();
				tileController = null;
			}

			if (TrayOne != null) {
				TrayOne.Dispose ();
				TrayOne = null;
			}

			if (ScrollView != null) {
				ScrollView.Dispose ();
				ScrollView = null;
			}

			if (TrayTwo != null) {
				TrayTwo.Dispose ();
				TrayTwo = null;
			}

			if (TrayThree != null) {
				TrayThree.Dispose ();
				TrayThree = null;
			}
		}
	}
}
