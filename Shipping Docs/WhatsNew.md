###Version 04.04###

The following features and bug fixes have been added to `ActionComponents` in version 04.04:

* **Xamarin Version** - The component suite was updated for the latest Xamarin version.
* **License Activation** - Fixed issue where the required license activation was not being accepted correctly.

###Version 04.03###

The following features and bug fixes have been added to `ActionComponents` in version 04.03:

* **Action Tiles** - Added the Action Tiles control for both iOS and Android.
* **Action Color** - Added the `ACColor` class to both iOS and Android to support cross-platform color manipulation. `ACColor` can be implicitly converted to and from an iOS `UIColor` or Android `Color.

iOS specific changes:

* **AppleHardwareType** - Added the `AppleHardwareType` enum Used to convert an Apple device model name (in the form "iPhone10,3") to a human readable form (such as "iPhoneX"). This enum works with the `iOSDevice` class to get the type of device the app is running on.
* **iOSDevice** - Added features to get the current device `ModelName` and the `DeviceType` of the iOS device an app is running on.

Android specific changes:

* **ACView** - `ACView` was enhanced with new features to correctly calculate the height and width of `View` and `Layout` instances. New features were also added to get the current default display and the size of several UI features such as Status and Navigation bar height.
* **CoreGraphics** - Ported parts of the following elements to support ease of porting UI code from iOS to Android: `CGContext`, `CGPoint`, `CGRect`, `CGSize`.
* **Foundation** - Ported parts of the following elements to support ease of porting UI code from iOS to Android: `NSMutableParagraphStyle`, `NSSet`, `NSString`, `NSStringDrawingOptions`, `NSWritingDirection`.
* **UIKit** - Ported parts of the following elements to support ease of porting UI code from iOS to Android: `UIBezierPath`, `UIEvent`, `UIFont`, `UIGraphics`, `UIImage`, `UILineBreakMode`, `UIScrollView`, `UIStringAttributes`, `UITouch`, `UIView`.


###Version 04.02###

The following features and bug fixes have been added to `ActionComponents` in version 04.02:

* **Action Color Picker** - Added the Action Color Picker control.

###Version 04.01###

The following features and bug fixes have been added to `ActionComponents` in version 04.01:

* **Action Slider** - Added the Action Slider control.

###Version 04.00###

The following features and bug fixes have been added to `ActionComponents` in version 04.00:

* **Latest OS Versions** - Added support for the latest version of Android and iOS.
* **Renamed Product** - Changed product name from `ActionPack` to `ActionComponents`.
* **Changed Namespace** - Changed the namespaces so that all elements are directly under the `ActionComponents` namespace for ease of use.
* **Renamed Components** - All components have gone from a `UIAction` prefix to `AC` (example: `UIActionToast` to `ACToast`) to prevent naming issues on iOS and to shorten the name of each component.
* **Added ActionOval** - Added `ACOval` to add several different types of Oval elements to your app's user interface.

###Version 03.02###

The following features and bug fixes have been added to `ActionPack` in version 03.01:

* **iOS 9** - Added support for iOS 9 apps.
* **Download Manager** - Fixed crash on cancel for iOS 9.

###Version 03.01###

The following features and bug fixes have been added to `ActionPack` in version 03.01:

* **Unified APIs** - Updated support for the latest Unified APIs and Xamarin.iOS v8.6.

###Version 03.00###

The following features and bug fixes have been added to `ActionPack` in version 03.00:

* **Unified APIs** - Support has been added for the Unified APIs.
* **ActionOval Preview** - Includes a preview version of the new ActionOval control (currently iOS only).
* **Minor Bug Fixes** - Several minor bug fixes.

###Version 02.00###

The following features and bug fixes have been added to `ActionPack` in version 02.00:

* **iOS 8 Support** - Adds support for iOS 8.
* **Orientation Errors** - Fixes issue where the components were not reading the device orientation correctly.
* **Size Errors** - Fixes issue where the components were not reading the device screen size correctly.
* **Crashing** - Fixes issue that could cause the sample app to crash on some machines.
* **ActionTable** - Added delegates to the `UIActionTableItem` accessories to make them easier to work with (see new sample apps for usage).
* **ActionToast** - Added new `ShowText` static method to create and display a Toast type popup easily.
* **UINavBar** - Moves the top collection out from under the status bar on iOS 7 and greater.

###Version 01.05###

The following features and bug fixes have been added to `ActionPack` in version 01.05:

* **Export Selectors** - Fixes an issue with export selectors in the latest version of Xamarin.
* **iOS Designer** - Initial support for the iOS Designer.

###Version 01.04###

The following features and bug fixes have been added to `ActionPack` in version 01.04:

* **DisplayDefaultView** - Fixes a bug where _DisplayDefaultView_ only worked for the top collection of buttons in iOS.
* **ActionTable Android** - Exposes **titleColor** and **subtitleColor** to work with the color of the title and subtiles of ActionTable cells on Android.

###Version 01.03###

The following features and bug fixes have been added to `ActionPack` in version 01.03:

* **ActionTable Text Size** - This version fixes an issue that can occur with `ActionTable` and some Android devices where the text is cut off half-way in the middle.


###Version 01.02###

The following features and bug fixes have been added to `ActionPack` in version 01.02:

* **Custom Subview** - Added the ability to attach a custom subview to the `ActionAlert` in place of the description text. The subview, can in turn, contain other subviews.

###Version 01.01###

The following features and bug fixes have been added to `ActionPack` in version 01.01:

* **Static Linking** - Fixed an error with static linking in Xamarin.iOS.
* **Auto Select iOS 7 Styling** - Automatically selects iOS 7 appearance if running on an iOS 7 device.
* **Color Adjustments** - Adjusted several colors to better fit with iOS 7 design language
* **Screen Size Issue** - Fixes an issues where the background overlay is incorrectly sized if running on iOS 7
* **Enhancements** - Various other minor enhancement and performance tweaks.
