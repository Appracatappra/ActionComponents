# Action Toast Component

## Getting Started with Action Toast

**Action Toast** is available exclusively as part of the Action Component Suite by Appracatappra, LLC. To use an **Action Toast** in your mobile app include the `ActionComponents.dll` and reference the following using statement in your C# code:

```csharp
using ActionComponents;
```

## Creating a Action Toast Message

The quickest way to create an display an **Action Toast** message is to use the static `MakeText` method of the `ACToast` class to quickly assemble your popup, then display it:

```csharp
using ActionComponents;
...

// Display message to user
ACToast.MakeText("My message", ACToastLength.Long).Show ();
```

Multiple constructors exist for the `MakeText` method that allow for customization of the popup such as display time, location, appearance, etc. 

Additionally, **Action Toast** include a shortcut `ShowText` method that combines the functionality of `ACToast.MakeText(...).Show();` into a single method call:

```csharp
ACToast.ShowText("My message");
```

## Setting the Display Location

**Action Toast** can be set to display in multiple locations by setting the gravity property:

* **Top**
* **Center**
* **Bottom**
* **Custom** – A user defined location by setting the position property.

All locations take the device’s screen rotation into account so they always display correctly. The additional offset property can be used to nudge the display location as well.

## Setting the Display Lengths

**Action Toast** can be set to display for a given amount of time by either setting the duration property to the number of seconds or setting the length property to to one of the following `ACToastLengths` as:

* **Forever** – the message will display until the user taps it or it is closed programmatically.
* **Short** – display for one second.
* **Medium** – display for two seconds.
* **Long** – display for five seconds.

## Changing the Appearance

**Action Toast** can be further customized by setting it’s appearance property and defining the look and feel of the notification.

# iOS Example

Here is an example of quickly displaying an **Action Toast** message:

```csharp
using ActionComponents;
...

// Display message to user
ACToast.MakeText("My message", ACToastLength.Long).Show ();
```

Additionally, you can use one of the `ShowText` shortcut methods:

```csharp
using ActionComponents;
...

// Display message to user
ACToast.ShowText("Message at Center.", ACToastGravity.Center);
```


# Trial Version

The Trial version of **Action Toast** is fully functional however the background is watermarked. The fully licensed version removes this watermark.