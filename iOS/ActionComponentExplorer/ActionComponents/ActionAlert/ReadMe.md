# About Action Alert

**Action Alert** is a highly customizable, cross platform alert and notification system for iOS and Android. Present both modal and non-modal alerts, dialog boxes and notifications with a minimal of code to quickly inform the user of the state of a process or alert them of a situation that needs their attention in your mobile app.

**Action Alert** makes it easy include images, activity indicators, progress bars, and interactive buttons to a notification, while providing for a maximum of code reuse across platforms. In many cases the same code used to display an ActionAlert on one platform can be used virtually unchanged on another which not only saves time, but improves code maintainability.

# Alert and Notification Types

**Action Alert** provides for four main alert and notification types which can be expanded on by optionally adding titles, descriptions, icons, buttons (such as *OK* or *Cancel*) or any combination of the above. The base types are:

* **Default** – A standard alert type that can have an icon, title and/or description. The Default type has a fixed width of 312 pixels and a flexible height based on its content.
* **ActivityAlert** – Displays a “spinning gear” *ActivityIndicator* on iOS or a “spinning circle” ProgressBar on Android along with a title and/or description. The **ActivityAlert** type has a fixed width of 312 pixels and a flexible height based on its content.
* **ActivityAlertMedium** – The same as the **ActivityAlert** type but only allows for a title and has a fixed width of 123 pixels.
* **ProgressAlert** – Like the **Default** type it can have an icon, title and/or description however it includes a progress bar at the bottom of the notification. Also like the **Default** type, it has a fixed width of 312 pixels and a flexible height based on its content.

## Default Locations and User Draggable

**Action Alert** supports three default vertical locations: **Top**, **Middle** and **Bottom** with the alert centered horizontally. Set the default location to **Custom** and you can place the **Action Alert** anywhere on screen via code.

## Events

**Action Alert** defines the following events that you can monitor and respond to:

* `Touched`
* `Moved`
* `Released`
* `ButtonTouched`
* `ButtonReleased`
* `OverlayTouched` – For modal alerts, the user has tapped outside of the **Action Alert‘s** bounds.

In addition individual `ActionAlertButtons` attached to an **Action Alert** have `Touched` and `Released` events that can be responded to directly.

## Appearance

**Action Alert** was designed to look and function the same across platforms, however you have full control over its look and feel. **Action Alert** is fully customizable with user definable appearances for every element of its UI. You can make either *Square* or *Round Rectangle* alert boxes with user defined corner radius or optionally square off individual corners.

## Features

**Action Alert** includes a fully documented API with comments for every feature. The ActionAlert‘s user interface is drawn with vectors and is fully resolution independent.

# iOS Example

Here is an example that show creating and displaying **Action Alerts** quickly via static methods of the `ACAlert` class. More control is available by creating your own instance of `ACAlert` and setting it’s properties directly:

```csharp
using ActionComponents;
…

// Default Alert title and description only
ACAlert.ShowAlert ("ActionAlert", "A cross platform Alert, Dialog and Notification system for iOS and Android.");
…

// Default Alert with icon, title, description and OK button
ACAlert.ShowAlertOK (UIImage.FromFile ("ActionAlert_57.png"), "ActionAlert", "A cross platform Alert, Dialog and Notification system for iOS and Android.");
…

// Default Alert with title, description and custom buttons
_alert = ACAlert.ShowAlert ("ActionAlert", "A cross platform Alert, Dialog and Notification system for iOS and Android.","Cancel,Maybe,OK");

// Respond to any button being tapped
_alert.ButtonReleased += (button) => {
    _alert.Hide();
    ACAlert.ShowAlert(ACAlertLocation.Top, String.Format ("You tapped the {0} button.", button.title),"");
};
…

// Non-modal Activity Alert (spinning gear) with a title only that can be freely moved around the screen by the user
_alert = ACAlert.ShowActivityAlert ("Activity Alert", "", false);
_alert.draggable = true;
…

// Non-modal medium sized Activity Alert (spinning gear) with a title
_alert = ACAlert.ShowActivityAlertMedium ("Waiting...", false);
…

// Non-modal alert with a progress bar (set to 50%), title and description
_alert = ACAlert.ShowProgressAlert ("Progress Alert", "Displays an Alert for a process with a known length and gives feedback to the user.", false);
_alert.progressView.Progress = 0.5f;
```

# Android Examples

And here is the same example **Action Alerts** created in the `OnCreate` method of an Android `Activity`:

```csharp
using ActionComponents;
…

// Default Alert title and description only
ACAlert.ShowAlert (this, "ActionAlert", "A cross platform Alert, Dialog and Notification system for iOS and Android.");
…

// Default Alert with icon, title, description and OK button
ACAlert.ShowAlertOK (this, Resource.Drawable.ActionAlert_57, "ActionAlert", "A cross platform Alert, Dialog and Notification system for iOS and Android.");
…

// Default Alert with title, description and custom buttons
_alert = ACAlert.ShowAlert (this, "ActionAlert", "A cross platform Alert, Dialog and Notification system for iOS and Android.","Cancel,Maybe,OK");

// Respond to any button being tapped
_alert.ButtonReleased += (button) => {
    _alert.Hide();
    ACAlert.ShowAlert(this, ACAlertLocation.Top, String.Format ("You tapped the {0} button.", button.title),"");
};
…

// Non-modal Activity Alert (spinning circular progress bar) with a title only that can be freely moved around the screen by the user
_alert = ACAlert.ShowActivityAlert (this, "Activity Alert", "", false);
_alert.draggable = true;
…

// Non-modal medium sized Activity Alert (spinning circular progress bar) with a title
_alert = ACAlert.ShowActivityAlertMedium (this, "Waiting...", false);
…

// Non-modal alert with a progress bar (set to 50%), title and description
_alert = ACAlert.ShowProgressAlert (this, "Progress Alert", "Displays an Alert for a process with a known length and gives feedback to the user.", false);
_alert.progressView.Progress = 50;
```

**Note**: The addition of the keyword this to the start of every **Action Alert** call in the above code is a reference to the current `Activity` being run and, aside from the difference in referencing an icon on Android, is the only change made from the iOS version of the code.

# Trial Version

The Trial version of **Action Alert** is fully functional but includes a `Toast` style popup. The fully licensed version removes this popup.