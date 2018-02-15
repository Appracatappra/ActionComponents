using System;
namespace ActionComponents
{
	/// <summary>
	/// Used to convert an Apple device model name (in the form "iPhone10,3") to a human readable form (such as 
	/// "iPhoneX"). This enum works with the `iOSDevice` class to get the type of device the app is running 
	/// on.
	/// </summary>
	public enum AppleHardwareType
	{
		iPhone,
		iPhone3G,
		iPhone3GS,
		iPhone4,
		iPhone4S,
		iPhone5,
		iPhone5C,
		iPhone5S,
		iPhone6,
		iPhone6S,
		iPhone6Plus,
		iPhone6SPlus,
		iPhoneSE,
		iPhone7,
		iPhone7Plus,
		iPhone8,
		iPhone8Plus,
		iPhoneX,

		iPad,
		iPad2,
		iPadMini,
		iPad3,
		iPad4,
		iPadAir,
		iPadMini2,
		iPadMini3,
		iPadMini4,
		iPadAir2,
		iPadPro12In,
		iPadPro9In,
		iPad5thGen,
		iPadPro12In2ndGen,
		iPadPro10In,

		iPodTouch,
		iPodTouch2ndGen,
		iPodTouch3rdGen,
		iPodTouch4thGen,
		iPodTouch5thGen,
		iPodTouch6thGen,

		appleTV2ndGen,
		appleTV3rdGen,
		appleTV4thGen,
		appleTV4K,

		appleWatch,
		appleWatchSeries1,
		appleWatchSeries2,
		appleWatchSeries3,

		unknown
	}
}
