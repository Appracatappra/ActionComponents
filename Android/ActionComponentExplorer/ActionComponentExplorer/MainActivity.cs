using Android.App;
using Android.Widget;
using Android.OS;
using ActionComponents;
using Android.Graphics;
using UIKit;
using System;

namespace ActionComponentExplorer
{
	[Activity(Label = "ActionComponentExplorer", Theme = "@android:style/Theme.NoTitleBar", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set license information BEFORE any components are called to suppress the
			// "Unlicensed Appracatappra Product" Toast popup.
			AppracatappraLicenseManager.FirstName = "Kevin";
			AppracatappraLicenseManager.LastName = "Mullins";
			AppracatappraLicenseManager.Email = "kevin_mullins@mac.com";
			AppracatappraLicenseManager.LicenseKey = "786a00012252366fdb67634a1be774c8";
			AppracatappraLicenseManager.ActivationKey = "394-826-6678-7718";

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			//Button button = FindViewById<Button>(Resource.Id.myButton);

			//button.Click += delegate { button.Text = $"{count++} clicks!"; };
			// RunOnUiThread(() => colorTest.SetImageBitmap(image));

			// Testing
			var tileController = FindViewById<ACTileController>(Resource.Id.tileController);
			tileController.title = "Home Controller";
			tileController.navigationBar.ShowNavigationBar(true);
			tileController.scrollDirection = ACTileControllerScrollDirection.Vertical;
			tileController.appearance.backgroundImage = UIImage.FromResources(Resources, Resource.Drawable.Agreement);

			// Stop the tile controller from updating while we populate it
			tileController.suspendUpdates = true;

			// Add and configure the first group
			var group1 = tileController.AddGroup(ACTileGroupType.ExpandingGroup, "Group One", "This is a sample group.");
			group1.appearance.hasBackground = true;
			group1.autoFitTiles = true;

			group1.AddTile(ACTileStyle.Default, ACTileSize.Single, "Today", "", "", Resources, Resource.Drawable.CalendarDay);
			group1.AddTile(ACTileStyle.Default, ACTileSize.DoubleVertical, "Crop", "", "", Resources, Resource.Drawable.Crop);
			group1.AddTile(ACTileStyle.BigPicture, ACTileSize.DoubleHorizontal, "Last Image", "", "", Resources, Resource.Drawable.Arrow).appearance.titleColor = ACColor.White;
			group1.AddTile(ACTileStyle.DescriptionBlock, ACTileSize.Quad, "Message", "Welcome!", "We hope that you are enjoying your ActionTile View. Look around and see what all I can do!", Resources, Resource.Drawable.OpenMail);
			group1.AddTile(ACTileStyle.Default, ACTileSize.Single, "Map", "", "", Resources, Resource.Drawable.Marker);
			group1.AddTile(ACTileStyle.TopTitle, ACTileSize.Single, "Locked", "", "", Resources, Resource.Drawable.Lock);
			group1.AddTile(ACTileStyle.CornerIcon, ACTileSize.Single, "Images", "32", "", Resources, Resource.Drawable.FilmRoll);
			group1.AddTile(ACTileStyle.Default, ACTileSize.Single, "Pasteboard", "", "", Resources, Resource.Drawable.Paste);

			// Randomily assign a purple color to the tiles in this group with the
			// given brightness range
			group1.ChromaKeyTiles(ACColor.Purple, 50, 250);

			group1.liveUpdateAction = new ACTileLiveUpdateGroupColorRandom(group1, new ACColor[] {
				ACColor.ActionBrightOrange,
				ACColor.ActionRedOrange,
				ACColor.Red,
				ACColor.ActionBrickRed
			});

			//// Assign a live update action to this group
			//group1.liveUpdateAction = new ACTileLiveUpdateGroupChromaKey(group1, ACColor.Purple, 50, 250);

			//var sceneSize = 4;

			////// Adjust based on device
			//tileController.appearance.cellSize = 50f;

			//// Style tiles
			//tileController.tileAppearance.titleSize = 14f;
			//tileController.tileAppearance.subtitleColor = ACColor.FromRGB(100, 100, 100);
			//tileController.tileAppearance.subtitleSize = 12f;

			//// Add new group
			//var scenes = tileController.AddGroup(ACTileGroupType.ExpandingGroup, "Favorite Scenes", "");

			//scenes.AddCustomSizedTile(1, sceneSize, ACTileStyle.Scene, "Evening", "", "", Resources, Resource.Drawable.House);
			//scenes.AddCustomSizedTile(1, sceneSize, ACTileStyle.Scene, "Go to Bed", "", "", Resources, Resource.Drawable.House);
			//scenes.AddCustomSizedTile(1, sceneSize, ACTileStyle.Scene, "Night", "", "", Resources, Resource.Drawable.House);
			//scenes.AddCustomSizedTile(1, sceneSize, ACTileStyle.Scene, "Start Work", "", "", Resources, Resource.Drawable.House);
			//scenes.AddCustomSizedTile(1, sceneSize, ACTileStyle.Scene, "Sunset", "", "", Resources, Resource.Drawable.House);
			//scenes.AddCustomSizedTile(1, sceneSize, ACTileStyle.Scene, "Wake Up", "", "", Resources, Resource.Drawable.House);

			//// Add new group
			//var accessories = tileController.AddGroup(ACTileGroupType.ExpandingGroup, "Favorite Accessories", "");

			//accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Kitchen Eve Door", "No Response", "", Resources, Resource.Drawable.House);
			//accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Patio Eve Humidity", "No Response", "", Resources, Resource.Drawable.House);
			//accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Patio Eve Temp", "No Response", "", Resources, Resource.Drawable.House);
			//accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Bedroom", "Off", "", Resources, Resource.Drawable.LightOff);
			//accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Dining Room Left", "Off", "", Resources, Resource.Drawable.LightOff);
			//accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Dining Room Right", "Off", "", Resources, Resource.Drawable.LightOff);
			//accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Living Bloom", "Off", "", Resources, Resource.Drawable.LightOff);
			//accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Living Iris", "Off", "", Resources, Resource.Drawable.LightOff);
			//accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Living Lightstrip", "Off", "", Resources, Resource.Drawable.LightOff);
			//accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Living Table Lamp", "Off", "", Resources, Resource.Drawable.LightOff);
			//accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Living Wicker Bottom", "Off", "", Resources, Resource.Drawable.LightOff);
			//accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Living Wicker Top", "Off", "", Resources, Resource.Drawable.LightOff);
			//accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Office Desk", "Off", "", Resources, Resource.Drawable.LightOff);
			//accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Office Side Table", "Off", "", Resources, Resource.Drawable.LightOff);

			//// Wire-up accessories events
			//accessories.TileTouched += (group, tile) => {
			//	switch (tile.subtitle)
			//	{
			//		case "Off":
			//			tile.subtitle = "On";
			//			if (tile.title == "Bedroom")
			//			{
			//				// Simulate a light color
			//				tile.ChromaKeyTile(ACColor.Purple, 50, 250);
			//			}
			//			else
			//			{
			//				// Just set to white
			//				tile.appearance.background = ACColor.White;
			//			}
			//			tile.icon = UIImage.FromResources(Resources, Resource.Drawable.LightOn);
			//			break;
			//		case "On":
			//			tile.subtitle = "Off";
			//			tile.appearance.background = ACColor.FromRGBA(213, 213, 213, 240);
			//			tile.icon = UIImage.FromResources(Resources, Resource.Drawable.LightOff);
			//			break;
			//	}
			//};


			// Allow update and automatically refresh the screen
			tileController.suspendUpdates = false;

			// Tell the controller to automatically update itself
			tileController.liveUpdate = true;

			// Display touched tile
			tileController.TileTouched += (group, tile) => {
				Console.WriteLine("Touched tile {0} in group {1}", tile.title, group.title);
			};
		}
	}
}

