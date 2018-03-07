# Getting Started with ActionAlerts

To use an **Action Alert** in your mobile application include the `ActionComponents.dll` library and reference the following using statement in your C# code:

```csharp
using ActionComponents;
```

## A Special Note About Working with Action Alerts in Android

When using an **Action Alert** on the Android OS you must specify the context as the first parameter of the **Action Alert‘s** call. This is usually the `Activity` that the alert is being displayed in.

**Note**: You are also responsible for redisplaying an **Action Alert** in response to the device being rotated or the `Activity` being restarted.

## Displaying a Simple Action Alert

**Action Alerts** were designed to be displayed with a minimum of code and the quickest and easiest way to create and display one is by calling one of the many preset Static methods of the `ACAlert` class such as:

### iOS Example

```csharp
// Default Alert with icon, title, description and OK button
ACAlert.ShowAlertOK (UIImage.FromFile ("ActionAlert_57.png"), "ActionAlert", "A cross platform Alert, Dialog and Notification system for iOS and Android.");
```

### Android Example

```csharp
// Default Alert with icon, title, description and OK button
ACAlert.ShowAlertOK (this, Resource.Drawable.ActionAlert_57, "ActionAlert", "A cross platform Alert, Dialog and Notification system for iOS and Android.");
```

For more complete control over the **Action Alert** create a new instance by calling one of its constructors and adjust its properties and settings manually via code.

## Adding Buttons to an Action Alert

All of the **Action Alert** base types can be made interactive by adding touchable buttons that will be incorporated into the bottom edge of the alert. Several Static methods exist for creating standard buttons such as *OK* or *Cancel*:

### iOS Example

```csharp
_alert = ACAlert.ShowAlertOKCancel ("ActionAlert", "A cross platform Alert, Dialog and Notification system for iOS and Android.");
```

### Android Example

```csharp
_alert = ACAlert.ShowAlertOKCancel (this, "ActionAlert", "A cross platform Alert, Dialog and Notification system for iOS and Android.");
```

Or custom buttons can be created using:

### iOS Example

```csharp
_alert = ACAlert.ShowAlert ("ActionAlert", "A cross platform Alert, Dialog and Notification system for iOS and Android.","Cancel,Maybe,OK");
```

### Android Example

```csharp
_alert = ACAlert.ShowAlert (this, "ActionAlert", "A cross platform Alert, Dialog and Notification system for iOS and Android.","Cancel,Maybe,OK");
```

Where “*Cancel,Maybe,OK*” represents a comma separated list of button titles that will be created and added to the alert.

## Responding to User Interaction

**Action Alert** defines several events that can be responded to such as `Touched`, `Moved`, `Released`, `ButtonTouched`, `ButtonReleased` or `OverlayTouched`. The following is an example of handling a button being pressed on an **Action Alert**:

```csharp
//Respond to any button being tapped
_alert.ButtonReleased += (button) => {
    _alert.Hide();
    ACAlert.ShowAlert(ACAlertLocation.Top, String.Format ("You tapped the {0} button.", button.title),"");
};
```

## Using Dragable

**Action Alert** feature built-in drag handling with optional constraints on the X and/or Y axis. In the following example we will make an alert draggable, lock it’s Y coordinate in place and allow the X coordinate to be drug within a given range:

```csharp
// Set alert to be draggable and apply limits to it's movement
_alert.draggable = true;
_alert.xConstraint.constraintType = ACViewDragConstraintType.Constrained;
_alert.xConstraint.minimumValue = 140f;
_alert.xConstraint.maximumValue = 920f;
_alert.yConstraint.constraintType = ACViewDragConstraintType.Locked;
```

## Adjust an ActionAlert’s Appearance

**Action Alerts** were designed to be totally customizable. Here is an example of adjusting an alerts color and squaring off one of its corners:

### iOS Example

```csharp
// Create an alert and customize it's appearance
_alert = ACAlert.ShowAlert (UIImage.FromFile ("ActionAlert_57.png"), "ActionAlert is Customizable", "You can 'square off' one or more of the corners and adjust all of the colors and styles by using properties of the alert.", "No,Yes");
_alert.appearance.background = UIColor.Orange;
_alert.buttonAppearance.background = UIColor.Orange;
_alert.buttonAppearanceHighlighted.titleColor = UIColor.Orange;
_alert.appearance.roundBottomLeftCorner = false;

// Respond to any button being tapped
_alert.ButtonReleased += (button) => {
    _alert.Hide();
};
```

### Android Example

```csharp
// Create an alert and customize it's appearance
_alert = ACAlert.ShowAlert (this, Resource.Drawable.ActionAlert_57, "ActionAlert is Customizable", "You can 'square off' one or more of the corners and adjust all of the colors and styles by using properties of the alert.", "No,Yes");
_alert.appearance.background = Color.Orange;
_alert.buttonAppearance.background = Color.Orange;
_alert.buttonAppearanceHighlighted.titleColor = Color.Orange;
_alert.appearance.roundBottomLeftCorner = false;

// Respond to any button being tapped
_alert.ButtonReleased += (button) => {
    _alert.Hide();
};
```

# Trial Version

The Trial version of **Action Alert** is fully functional but includes a `Toast` style popup. The fully licensed version removes this popup.
