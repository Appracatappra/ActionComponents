using System;
using UIKit;
using ActionComponents;
using Foundation;
using CoreGraphics;

namespace ActionComponentExplorer
{
	public partial class ViewController : UIViewController
	{
		public ACTrayManager TrayManager { get; set; } = new ACTrayManager();

		protected ViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			// Setup the tray
			TrayOne.trayType = ACTrayType.Popup;
			TrayOne.TraySizeChanged += (t, s) =>
			{
				ScrollView.Frame = TrayOne.ContentArea;
			};
			TrayOne.TraySize = new CGSize(400, 300);
			TrayManager.AddTray(TrayOne);

			TrayTwo.trayType = ACTrayType.Popup;
			TrayManager.AddTray(TrayTwo);

			TrayThree.trayType = ACTrayType.Popup;
			TrayManager.AddTray(TrayThree);

			// Set the tray manager to auto layout the trays
			TrayManager.TrayOrientation = ACTrayOrientation.Bottom;
			TrayManager.TabLocation = ACTrayTabLocation.BottomOrRight;
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}
