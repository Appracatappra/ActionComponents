using System;
using System.Drawing;

using Foundation;
using UIKit;
using CoreGraphics;
using ObjCRuntime;
using System.Runtime.InteropServices;

namespace ActionComponents
{
	/// <summary>
	/// Helper class that returns information about the iOS device that the Xamarin.iOS app is running on
	/// </summary>
	public static class iOSDevice
	{
		#region Internal Properties
		[DllImport(Constants.SystemLibrary)]
		internal static extern int sysctlbyname(
			[MarshalAs(UnmanagedType.LPStr)] string property,
			IntPtr output,
			IntPtr oldLen,
			IntPtr newp,
			uint newlen);
		#endregion

		#region Computed Properties
		/// <summary>
		/// Gets a value indicating whether this <see cref="ActionComponents.iOSDevice"/> is phone.
		/// </summary>
		/// <value><c>true</c> if is phone; otherwise, <c>false</c>.</value>
		public static bool isPhone
		{
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="Appracatappra.ActionComponents.iOSDevice"/> is pad.
		/// </summary>
		/// <value><c>true</c> if is pad; otherwise, <c>false</c>.</value>
		public static bool isPad
		{
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad; }
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="Appracatappra.ActionComponents.iOSDevice"/> is a is568h device such as the iPhone 5.
		/// </summary>
		/// <value><c>true</c> if is568h; otherwise, <c>false</c>.</value>
		public static bool is568h
		{
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone && UIScreen.MainScreen.Bounds.Height * UIScreen.MainScreen.Scale >= 1136; }
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="Appracatappra.ActionComponents.iOSDevice"/> has a retina display.
		/// </summary>
		/// <value><c>true</c> if is retina; otherwise, <c>false</c>.</value>
		public static bool isRetina
		{
			get { return (UIScreen.MainScreen.Scale == 2.0f); }
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="Appracatappra.ActionComponents.iOSDevice"/> is running on a High Definition (HD) screen such as an iPhone 6 device.
		/// </summary>
		/// <value><c>true</c> the app is running on an HD device; otherwise, <c>false</c>.</value>
		public static bool iSHD
		{
			get { return (UIScreen.MainScreen.Scale == 3.0f); }
		}

		/// <summary>
		/// Gets a value indicating whether the app is running on an iOS 6 device.
		/// </summary>
		/// <value><c>true</c> if the app is running on iOS 6; otherwise, <c>false</c>.</value>
		public static bool isIOS6
		{
			get { return (UIDevice.CurrentDevice.SystemVersion[0] == '6'); }
		}

		/// <summary>
		/// Gets a value indicating whether the app is running on an iOS 7 device.
		/// </summary>
		/// <value><c>true</c> if the app is running on iOS 7; otherwise, <c>false</c>.</value>
		public static bool isIOS7
		{
			get { return (UIDevice.CurrentDevice.SystemVersion[0] == '7'); }
		}

		/// <summary>
		/// Gets a value indicating whether the app is running on an iOS 8 device.
		/// </summary>
		/// <value><c>true</c> if the app is running on iOS 8; otherwise, <c>false</c>.</value>
		public static bool isIOS8
		{
			get { return (UIDevice.CurrentDevice.SystemVersion[0] == '8'); }
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
		public static UIInterfaceOrientation currentDeviceOrientation
		{
			get
			{
				// Takes the current device orientation and converts it to an
				// interface orientation
				switch (UIDevice.CurrentDevice.Orientation)
				{
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
						if (UIScreen.MainScreen.Bounds.Width < UIScreen.MainScreen.Bounds.Height)
						{
							return UIInterfaceOrientation.Portrait;
						}
						else
						{
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
		public static CGRect RotatedScreenBounds
		{
			get
			{
				return iOSDevice.AvailableScreenBounds;
			}
		}

		/// <summary>
		/// Returns the bounds for the device's <c>MainScreen</c> adjusted to fit the current <c>UIInterfaceOrientation</c> taking into
		/// account if the system status bar is being displayed
		/// </summary>
		/// <value>The rotated screen bounds optionally minus the system status bar area</value>
		public static CGRect AvailableScreenBounds
		{
			get
			{
				CGRect bounds = new CGRect();

				//Adjust view to match current interface orientation
				switch (currentDeviceOrientation)
				{
					case UIInterfaceOrientation.LandscapeLeft:
					case UIInterfaceOrientation.LandscapeRight:
						// Use the bigger value as the width
						if (UIScreen.MainScreen.Bounds.Width > UIScreen.MainScreen.Bounds.Height)
						{
							bounds = new CGRect(0, 0, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height);
						}
						else
						{
							bounds = new CGRect(0, 0, UIScreen.MainScreen.Bounds.Height, UIScreen.MainScreen.Bounds.Width);
						}
						break;
					case UIInterfaceOrientation.Portrait:
					case UIInterfaceOrientation.PortraitUpsideDown:
						// Use the smaller value as the width
						if (UIScreen.MainScreen.Bounds.Width < UIScreen.MainScreen.Bounds.Height)
						{
							bounds = new CGRect(0, 0, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height);
						}
						else
						{
							bounds = new CGRect(0, 0, UIScreen.MainScreen.Bounds.Height, UIScreen.MainScreen.Bounds.Width);
						}
						break;
					default:
						// Use the smaller value as the width
						if (UIScreen.MainScreen.Bounds.Width < UIScreen.MainScreen.Bounds.Height)
						{
							bounds = new CGRect(0, 0, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height);
						}
						else
						{
							bounds = new CGRect(0, 0, UIScreen.MainScreen.Bounds.Height, UIScreen.MainScreen.Bounds.Width);
						}
						break;
				}

				//Return rotated and adjusted height
				return bounds;
			}
		}

		/// <summary>
		/// Gets the model name of the iOS Device that the app is running on. For example, `iPhone10,3` or `iPhone10,6` 
		/// for the `iPhone X`. When running in an iOS Simulator, the correct model name is returned instead of
		/// 'x86_64`.
		/// </summary>
		/// <value>The name of the model.</value>
		public static string ModelName {
			get {
				var model = GetSystemProperty("hw.machine");

				// Running on simulator
				if (model == "x86_64") {
					// Running on simulator, get model from there
					model = ((NSString)NSProcessInfo.ProcessInfo.Environment["SIMULATOR_MODEL_IDENTIFIER"]).ToString();
				}

				return model;
			}
		}

		/// <summary>
		/// Gets the type of the device. For example, "iPhone10,3" will return an <c>AppleHardwareType</c> enum of the
		///  human readable counterpart "iPhoneX".
		/// </summary>
		/// <value>The type of the device or <c>unknown</c> if the type cannot be decided.</value>
		public static AppleHardwareType DeviceType {
			get {
				switch(ModelName) {
					case "Watch2,6":
					case "Watch2,7":
						return AppleHardwareType.appleWatchSeries1;

					case "Watch2,3":
					case "Watch2,4":
						return AppleHardwareType.appleWatchSeries2;

					case "Watch3,1":
					case "Watch3,2": 
					case "Watch3,3":
					case "Watch3,4":
						return AppleHardwareType.appleWatchSeries3;

					case "Watch1,1":
						return AppleHardwareType.appleWatch;

					case "iPhone1,1":
						return AppleHardwareType.iPhone;

					case "iPhone1,2":
						return AppleHardwareType.iPhone3G;

					case "iPhone2,1":
						return AppleHardwareType.iPhone3GS;

					case "iPhone3,1":
					case "iPhone3,3":
						return AppleHardwareType.iPhone4;

					case "iPhone4,1":
						return AppleHardwareType.iPhone4S;

					case "iPhone5,1":
					case "iPhone5,2":
						return AppleHardwareType.iPhone5;

					case "iPhone5,3":
					case "iPhone5,4":
						return AppleHardwareType.iPhone5C;

					case "iPhone6,1":
					case "iPhone6,2":
						return AppleHardwareType.iPhone5S;

					case "iPhone7,1":
						return AppleHardwareType.iPhone6Plus;

					case "iPhone7,2":
						return AppleHardwareType.iPhone6;

					case "iPhone8,1":
						return AppleHardwareType.iPhone6S;

					case "iPhone8,2":
						return AppleHardwareType.iPhone6SPlus;

					case "iPhone8,4":
						return AppleHardwareType.iPhoneSE;

					case "iPhone9,1":
					case "iPhone9,3":
						return AppleHardwareType.iPhone7;

					case "iPhone9,2":
					case "iPhone9,4":
						return AppleHardwareType.iPhone7Plus;

					case "iPhone10,1":
					case "iPhone10,4":
						return AppleHardwareType.iPhone8;

					case "iPhone10,2":
					case "iPhone10,5":
						return AppleHardwareType.iPhone8Plus;

					case "iPhone10,3":
					case "iPhone10,6":
						return AppleHardwareType.iPhoneX;

					case "iPad1,1":
						return AppleHardwareType.iPad;

					case "iPad2,1":
					case "iPad2,2":
					case "iPad2,3":
					case "iPad2,4":
						return AppleHardwareType.iPad2;

					case "iPad2,5":
					case "iPad2,6":
					case "iPad2,7":
						return AppleHardwareType.iPadMini;

					case "iPad3,1":
					case "iPad3,2":
					case "iPad3,3":
						return AppleHardwareType.iPad3;

					case "iPad3,4":
					case "iPad3,5":
					case "iPad3,6":
						return AppleHardwareType.iPad4;

					case "iPad4,1":
					case "iPad4,2":
					case "iPad4,3":
						return AppleHardwareType.iPadAir;

					case "iPad4,4":
					case "iPad4,5":
					case "iPad4,6":
						return AppleHardwareType.iPadMini2;

					case "iPad4,7":
					case "iPad4,8":
					case "iPad4,9":
						return AppleHardwareType.iPadMini3;

					case "iPad5,1":
					case "iPad5,2":
						return AppleHardwareType.iPadMini4;

					case "iPad5,3":
					case "iPad5,4":
						return AppleHardwareType.iPadAir2;

					case "iPad6,3":
					case "iPad6,4":
						return AppleHardwareType.iPadPro12In;

					case "iPad6,7":
					case "iPad6,8":
						return AppleHardwareType.iPadPro9In;

					case "iPad6,11":
					case "iPad6,12":
						return AppleHardwareType.iPad5thGen;

					case "iPad7,1":
					case "iPad7,2":
						return AppleHardwareType.iPadPro12In2ndGen;

					case "iPad7,3":
					case "iPad7,4":
						return AppleHardwareType.iPadPro10In;

					case "iPod1,1":
						return AppleHardwareType.iPodTouch;

					case "iPod2,1":
						return AppleHardwareType.iPodTouch2ndGen;

					case "iPod3,1":
						return AppleHardwareType.iPodTouch3rdGen;

					case "iPod4,1":
						return AppleHardwareType.iPodTouch4thGen;

					case "iPod5,1":
						return AppleHardwareType.iPodTouch5thGen;

					case "iPod7,1":
						return AppleHardwareType.iPodTouch6thGen;

					case "AppleTV2,1":
						return AppleHardwareType.appleTV2ndGen;

					case "AppleTV3,1":
					case "AppleTV3,2":
						return AppleHardwareType.appleTV3rdGen;

					case "AppleTV5,3":
						return AppleHardwareType.appleTV4thGen;

					case "AppleTV6,2":
						return AppleHardwareType.appleTV4K;

					default:
						return AppleHardwareType.unknown;
				}
			}
		}
		#endregion

		#region Public Static Methods
		/// <summary>
		/// Gets the value of the given system property. For example, "hw.machine" will return the model number of the
		/// iOS Device that the app is running on.
		/// </summary>
		/// <returns>The system property value.</returns>
		/// <param name="property">Property to get the value for.</param>
		public static string GetSystemProperty(string property)
		{
			var pLen = Marshal.AllocHGlobal(sizeof(int));
			sysctlbyname(property, IntPtr.Zero, pLen, IntPtr.Zero, 0);
			var length = Marshal.ReadInt32(pLen);
			var pStr = Marshal.AllocHGlobal(length);
			sysctlbyname(property, pStr, pLen, IntPtr.Zero, 0);
			return Marshal.PtrToStringAnsi(pStr);
		}
		#endregion

	}
		
}

