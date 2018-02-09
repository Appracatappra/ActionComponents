using System;
using UIKit;
using ActionComponents;
using Foundation;

namespace ActionComponentExplorer
{
	public partial class ViewController : UIViewController
	{
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

			// Setup tile controller
			tileController.title = "Home Controller";
			tileController.scrollDirection = ACTileControllerScrollDirection.Vertical;
			tileController.appearance.backgroundImage = UIImage.FromBundle("Agreement.jpg");
			tileController.navigationBar.ShowNavigationBar(true);
			tileController.navigationBar.AddLeftButton("Home", null, 50, 32, (sender, e) => {
				Console.WriteLine("Home button pressed");
			});
			tileController.navigationBar.AddRightButton("Edit", null, 50, 32, (sender, e) => {
				Console.WriteLine("Edit button pressed");
			});
			tileController.navigationBar.AddRightButton("", UIImage.FromBundle("Add"), 16, 16,(sender, e) => {
				Console.WriteLine("Add button pressed");
			});

			// Suspend updates
			tileController.suspendUpdates = true;

			var sceneSize = 4;

			// Adjust based on device
			if (iOSDevice.isPhone) {
				tileController.appearance.cellSize = 45f;
				sceneSize = 3;
			} else {
				tileController.appearance.cellSize = 50f;
			}

			// Style tiles
			tileController.tileAppearance.titleSize = 14f;
			tileController.tileAppearance.subtitleColor = ACColor.FromRGB(100, 100, 100);
			tileController.tileAppearance.subtitleSize = 12f;

			//// Add new group
			//var favorites = tileController.AddGroup(ACTileGroupType.ExpandingGroup, "Sample Tiles", "This is a sample footer.");
			//favorites.appearance.hasBackground = false;
			//favorites.autoFitTiles = true;

			//favorites.AddTile(ACTileStyle.Default, ACTileSize.Single, "Today", "", "", "CalendarDay");
			//favorites.AddTile(ACTileStyle.Default, ACTileSize.DoubleVertical, "Crop", "", "", "Crop");
			//favorites.AddTile(ACTileStyle.BigPicture, ACTileSize.DoubleHorizontal, "Last Image", "", "", "Arrow.jpg").appearance.titleColor = ACColor.White;
			//favorites.AddTile(ACTileStyle.DescriptionBlock, ACTileSize.Quad, "Message", "Welcome!", "We hope that you are enjoying your ActionTile View. Look around and see what all I can do!", "OpenMail");
			//favorites.AddTile(ACTileStyle.Default, ACTileSize.Single, "Map", "", "", "Marker");
			//favorites.AddTile(ACTileStyle.TopTitle, ACTileSize.Single, "Locked", "", "", "Lock");
			//favorites.AddTile(ACTileStyle.CornerIcon, ACTileSize.Single, "Images", "32", "", "FilmRoll");
			//favorites.AddTile(ACTileStyle.Default, ACTileSize.Single, "Pasteboard", "", "", "PasteBoard");


			//// Randomily assign a purple color to the tiles in this group with the
			//// given brightness range
			//favorites.ChromaKeyTiles(ACColor.Purple, 50, 250);

			//// Assign a live update action to this group
			//favorites.liveUpdateAction = new ACTileLiveUpdateGroupChromaKey(favorites, ACColor.Purple, 50, 250);

			// Add new group
			var scenes = tileController.AddGroup(ACTileGroupType.ExpandingGroup, "Favorite Scenes", "");

			scenes.AddCustomSizedTile(1, sceneSize, ACTileStyle.Scene, "Evening", "", "", "Home");
			scenes.AddCustomSizedTile(1, sceneSize, ACTileStyle.Scene, "Go to Bed", "", "", "Home");
			scenes.AddCustomSizedTile(1, sceneSize, ACTileStyle.Scene, "Night", "", "", "Home");
			scenes.AddCustomSizedTile(1, sceneSize, ACTileStyle.Scene, "Start Work", "", "", "Home");
			scenes.AddCustomSizedTile(1, sceneSize, ACTileStyle.Scene, "Sunset", "", "", "Home");
			scenes.AddCustomSizedTile(1, sceneSize, ACTileStyle.Scene, "Wake Up", "", "", "Home");

			// Add new group
			var accessories = tileController.AddGroup(ACTileGroupType.ExpandingGroup, "Favorite Accessories", "");

			accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Kitchen Eve Door", "No Response", "", "Home");
			accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Patio Eve Humidity", "No Response", "", "Home");
			accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Patio Eve Temp", "No Response", "", "Home");
			accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Bedroom", "Off", "", "Light");
			accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Dining Room Left", "Off", "", "Light");
			accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Dining Room Right", "Off", "", "Light");
			accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Living Bloom", "Off", "", "Light");
			accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Living Iris", "Off", "", "Light");
			accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Living Lightstrip", "Off", "", "Light");
			accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Living Table Lamp", "Off", "", "Light");
			accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Living Wicker Bottom", "Off", "", "Light");
			accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Living Wicker Top", "Off", "", "Light");
			accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Office Desk", "Off", "", "Light");
			accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Office Side Table", "Off", "", "Light");

			// Wire-up accessories events
			accessories.TileTouched += (group, tile) => {
				switch(tile.subtitle) {
					case "Off":
						tile.subtitle = "On";
						if (tile.title == "Bedroom"){
							// Simulate a light color
							tile.ChromaKeyTile(ACColor.Purple, 50, 250);
						} else {
							// Just set to white
							tile.appearance.background = ACColor.White;
						}
						tile.icon = UIImage.FromBundle("Brightness");
						break;
					case "On":
						tile.subtitle = "Off";
						tile.appearance.background = ACColor.FromRGBA(213, 213, 213, 240);
						tile.icon = UIImage.FromBundle("Light");
						break;
				}
			};

			// Restore updates
			tileController.suspendUpdates = false;

			// Tell the controller to automatically update itself
			tileController.liveUpdate = true;

			// Display touched tile
			tileController.TileTouched += (group, tile) => {
				Console.WriteLine("Touched tile {0} in group {1}", tile.title, group.title);
			};
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}
