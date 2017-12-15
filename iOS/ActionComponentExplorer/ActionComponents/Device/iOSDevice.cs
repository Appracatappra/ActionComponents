using System;
using System.Drawing;

using Foundation;
using UIKit;
using CoreGraphics;


namespace ActionComponents
{
	/// <summary>
	/// Helper class that returns information about the iOS device that the Xamarin.iOS app is running on
	/// </summary>
	public static class iOSDevice
	{
		#region Computed Properties
		/// <summary>
		/// Gets a value indicating whether this <see cref="ActionComponents.iOSDevice"/> is phone.
		/// </summary>
		/// <value><c>true</c> if is phone; otherwise, <c>false</c>.</value>
		public static bool isPhone{
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="Appracatappra.ActionComponents.iOSDevice"/> is pad.
		/// </summary>
		/// <value><c>true</c> if is pad; otherwise, <c>false</c>.</value>
		public static bool isPad{
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad; }
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="Appracatappra.ActionComponents.iOSDevice"/> is a is568h device such as the iPhone 5.
		/// </summary>
		/// <value><c>true</c> if is568h; otherwise, <c>false</c>.</value>
		public static bool is568h{
			get {return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone && UIScreen.MainScreen.Bounds.Height * UIScreen.MainScreen.Scale >= 1136;}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="Appracatappra.ActionComponents.iOSDevice"/> has a retina display.
		/// </summary>
		/// <value><c>true</c> if is retina; otherwise, <c>false</c>.</value>
		public static bool isRetina{
			get { return (UIScreen.MainScreen.Scale == 2.0f);}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="Appracatappra.ActionComponents.iOSDevice"/> is running on a High Definition (HD) screen such as an iPhone 6 device.
		/// </summary>
		/// <value><c>true</c> the app is running on an HD device; otherwise, <c>false</c>.</value>
		public static bool iSHD{
			get { return (UIScreen.MainScreen.Scale == 3.0f); }
		}

		/// <summary>
		/// Gets a value indicating whether the app is running on an iOS 6 device.
		/// </summary>
		/// <value><c>true</c> if the app is running on iOS 6; otherwise, <c>false</c>.</value>
		public static bool isIOS6{
			get { return (UIDevice.CurrentDevice.SystemVersion [0] == '6');}
		}

		/// <summary>
		/// Gets a value indicating whether the app is running on an iOS 7 device.
		/// </summary>
		/// <value><c>true</c> if the app is running on iOS 7; otherwise, <c>false</c>.</value>
		public static bool isIOS7{
			get { return (UIDevice.CurrentDevice.SystemVersion [0] == '7');}
		}

		/// <summary>
		/// Gets a value indicating whether the app is running on an iOS 8 device.
		/// </summary>
		/// <value><c>true</c> if the app is running on iOS 8; otherwise, <c>false</c>.</value>
		public static bool isIOS8{
			get { return (UIDevice.CurrentDevice.SystemVersion [0] == '8');}
		}

		/// <summary>
		/// Gets a value indicating whether the app is running on an iOS 9 device.
		/// </summary>
		/// <value><c>true</c> if the app is running on iOS 9; otherwise, <c>false</c>.</value>
		public static bool isIOS9
		{
			get { return (UIDevice.CurrentDevice.SystemVersion[0] == '9'); }
		}

		/// <summary>
		/// Gets the current device orientation.
		/// </summary>
		/// <value>The current device orientation.</value>
		public static UIInterfaceOrientation currentDeviceOrientation {
			get {
				// Takes the current device orientation and converts it to an
				// interface orientation
				switch (UIDevice.CurrentDevice.Orientation) {
				case UIDeviceOrientation.LandscapeLeft:
					return UIInterfaceOrientation.LandscapeLeft;
				case UIDeviceOrientation.LandscapeRight:
					return UIInterfaceOrientation.LandscapeRight;
				case UIDeviceOrientation.Portrait:
					return UIInterfaceOrientation.Portrait;
				case UIDeviceOrientation.PortraitUpsideDown:
					return UIInterfaceOrientation.PortraitUpsideDown;
				case UIDeviceOrientation.Unknown:
					// iOS was unable to determin the device orientation
					// Attempt to determine the device orientation based on the screen bounds
					if (UIScreen.MainScreen.Bounds.Width < UIScreen.MainScreen.Bounds.Height) {
						return UIInterfaceOrientation.Portrait;
					} else {
						return UIInterfaceOrientation.LandscapeLeft;
					}
				default:
					return UIInterfaceOrientation.Unknown;
				}
			}
		}

		/// <summary>
		/// Returns the bounds for the device's <c>MainScreen</c> adjusted to fit the current <c>UIInterfaceOrientation</c>
		/// </summary>
		/// <value>The rotated screen bounds.</value>
		public static CGRect RotatedScreenBounds{
			get {
				return iOSDevice.AvailableScreenBounds;
			}
		}

		/// <summary>
		/// Returns the bounds for the device's <c>MainScreen</c> adjusted to fit the current <c>UIInterfaceOrientation</c> taking into
		/// account if the system status bar is being displayed
		/// </summary>
		/// <value>The rotated screen bounds optionally minus the system status bar area</value>
		public static CGRect AvailableScreenBounds{
			get {
				CGRect bounds = new CGRect();

				//Adjust view to match current interface orientation
				switch (currentDeviceOrientation) {
				case UIInterfaceOrientation.LandscapeLeft:
				case UIInterfaceOrientation.LandscapeRight:
					// Use the bigger value as the width
					if (UIScreen.MainScreen.Bounds.Width > UIScreen.MainScreen.Bounds.Height) {
						bounds = new CGRect (0, 0, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height);
					} else {
						bounds = new CGRect (0, 0, UIScreen.MainScreen.Bounds.Height, UIScreen.MainScreen.Bounds.Width);
					}
					break;
				case UIInterfaceOrientation.Portrait:
				case UIInterfaceOrientation.PortraitUpsideDown:
					// Use the smaller value as the width
					if (UIScreen.MainScreen.Bounds.Width < UIScreen.MainScreen.Bounds.Height) {
						bounds = new CGRect (0, 0, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height);
					} else {
						bounds = new CGRect (0, 0, UIScreen.MainScreen.Bounds.Height, UIScreen.MainScreen.Bounds.Width);
					}
					break;
				default:
					// Use the smaller value as the width
					if (UIScreen.MainScreen.Bounds.Width < UIScreen.MainScreen.Bounds.Height) {
						bounds = new CGRect (0, 0, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height);
					} else {
						bounds = new CGRect (0, 0, UIScreen.MainScreen.Bounds.Height, UIScreen.MainScreen.Bounds.Width);
					}
					break;
				}

				//Return rotated and adjusted height
				return bounds;
			}
		}
		#endregion 

	}
		
}

