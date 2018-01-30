# Action Nav Bar Component
## Getting Started with Action Nav Bar

To use an **Action Nav Bar** in your mobile application include the `ActionComponents.dll` and reference the following using statement in your C# code:

```csharp
using ActionComponents;
```

## Adding NavBar to your Mobile App

NavBar was designed to make adding it to your mobile application in Xamarin Studio easy.

### For iOS

Add a `UIView` to your project in a .xib file using Xcode then switch it’s type to `ACNavBar`. Ensure that you pin the **Action Nav Bar** to the left hand side of the screen and make it’s width exactly 80 pixels wide.

**Warning!** Failing to do either of these will result in a **Action Nav Bar** that will not look or function correctly!

### For Android

Open your `Main.axml` file in Visual Studio, insert a `RelativeLayout` then insert a `View` into it, make it 80 pixels wide and pin it to the left hand side of the `RelativeLayout`, make its `Height` fill the parent and change its Class to `ActionComponents.ACNavBar` in the **Source** view.

Next add a few sub views and controls to the `RelativeLayout` under your **Action Nav Bar**, this is done so that the **Action Nav Bar** can “float above and overlap” the sub views that it controls and draw the shadow and **NavBarPointer** over those views.

**Warning!:** Your `ACNavBar` MUST be hosted inside of a `RelativeLayout` or an error will be thrown and you must set your sub views visibility to gone initially for the **Action Nav Bar** to properly control them. The **Action Nav Bar** is itself a type of custom `RelativeLayout`.

### Created from Code

You also have the option of creating your **Action Nav Bar** completely in C# code for either iOS or Android.

## Adding View Buttons to your NavBar

Once you have created your **Action Nav Bar** and added it to your view you can add buttons that will link to views that the **Action Nav Bar** will automatically control and bring to the surface when tapped by the end user.

### iOS Example:

```csharp
//---------------------------------------------
// Add buttons to the top of the bar
//---------------------------------------------
// The first button added to the top collection will automatically be selected
ACNavBarButton home = navBar.top.AddButton (UIImage.FromFile ("Icons/house.png"), true, false);

// Wire up request for this button's view
home.RequestNewView += responder => {
    // Attaching a view to a button will automatically display it under the NavBar
    home.attachedView = homeView;
};

navBar.top.AddAutoDisposingButton (UIImage.FromFile ("Icons/bar-chart.png"), true, false).RequestNewView += responder => {
    // Build new view from a .xib file and attach it to the button it will automatically
    // be displayed under the NavBar
    responder.attachedView = BarChartView.Factory (this);
};

//-----------------------------------------
// Add buttons to the bottom of the bar
//-----------------------------------------
navBar.bottom.AddButton (UIImage.FromFile ("Icons/gear.png"), true, false).RequestNewView += responder => {
    responder.attachedView = SettingsView.Factory (this, navBar);
};
```

### Android Example:

```csharp
// Gain Access to all views and controls in our layout
navBar = FindViewById<ACNavBar> (Resource.Id.navBar);
viewHome = FindViewById<ImageView> (Resource.Id.viewHome);

//---------------------------------------------
// Add buttons to the top of the bar
//---------------------------------------------
// The first button added to the top collection will automatically be selected
ACNavBarButton home = navBar.top.AddButton(Resource.Drawable.house,true,false);

// Wire up request for this button's view
home.RequestNewView += responder => {
    // Attach view to the button
    responder.attachedView=viewHome;
};

// Add Bar Chart
navBar.top.AddAutoDisposingButton (Resource.Drawable.barchart,true,false).RequestNewView += (responder) => {
    // Bring view into existance
    viewBarChart = (View)LayoutInflater.Inflate (Resource.Layout.ViewBarChart,null);

    // Attach view to the button
    responder.attachedView = viewBarChart;
};

//-----------------------------------------
// Add buttons to the bottom of the bar
//-----------------------------------------
navBar.bottom.AddAutoDisposingButton (Resource.Drawable.gear, true, false).RequestNewView += responder => {
    // Bring view into existance
    viewSettings = (View)LayoutInflater.Inflate (Resource.Layout.ViewSettings,null);

    // Attach view to the button
    responder.attachedView=viewSettings;

    // grab show/hide button
    showHideNavBar = FindViewById<Button> (Resource.Id.showHideButton);

    //-----------------------------------------
    // Wireup button action
    //-----------------------------------------
    if (showHideNavBar!=null) {
        showHideNavBar.Click += (sender, e) => {
            //Is the NavBar visible?
            navBar.Hidden=(!navBar.Hidden);
        };
    }
};
```

## Adding Tool Buttons to your NavBar

Just like view buttons above, you can add tool buttons to your **Action Nav Bar** to respond to user interaction.

### iOS Example:

```csharp
//--------------------------------------------
// Add buttons to the middle of the bar
//--------------------------------------------
navBar.middle.AddTool (UIImage.FromFile ("Icons/printer.png"), true, false).Touched += responder => {
    // Display Alert Dialog Box
    using (var alert = new UIAlertView ("NavBar", "Sorry but printing is not available at this time.", null, "OK", null)) {
        alert.Show ();    
    }

    // Display warning notification in NavBar
    if (warning != null) warning.Hidden = false;
};
```

### Android Example:

```csharp
public string dialogMessage="";
...

//--------------------------------------------
// Add buttons to the middle of the bar
//--------------------------------------------
navBar.middle.AddTool (Resource.Drawable.printer,true,false).Touched+= (responder) => {
    // Inform user (dialogMessage defined as a global variable)
    dialogMessage="Sorry but printing is not supported at this time";
    ShowDialog (DialogLongMessage);

    // Display warning notification in NavBar
    if (warning != null) warning.Hidden = false;
};
... 

// Build dialog box when requested
protected override Dialog OnCreateDialog (int id)
{
    Dialog alert = null;
    AlertDialog.Builder builder;

    base.OnCreateDialog (id);

    // Build requested dialog type
    switch (id){
    case DialogLongMessage:
        builder = new AlertDialog.Builder(this);
        builder.SetIcon (Android.Resource.Attribute.AlertDialogIcon);
        builder.SetTitle ("NavBar");
        builder.SetMessage(dialogMessage);
        builder.SetPositiveButton ("OK", delegate(object sender, DialogClickEventArgs e) {
            // Ignore for now
        });
        alert=builder.Create ();
        break;
    case DialogLongMessageOkCancel:
        builder = new AlertDialog.Builder(this);
        builder.SetIcon (Android.Resource.Attribute.AlertDialogIcon);
        builder.SetTitle ("NavBar");
        builder.SetMessage(dialogMessage);
        builder.SetPositiveButton ("OK", delegate(object sender, DialogClickEventArgs e) {
            // Ignore for now
        });
        builder.SetNegativeButton ("Cancel", delegate(object sender, DialogClickEventArgs e) {
            // Ignore for now
        });
        alert=builder.Create ();
        break;
    }

    // Return dialog
    return alert;
}
```

## Adding Notification Icons to your NavBar

**Action Nav Bar** also provides support for non-touchable notifications that can be displayed in any region of the bar: **top**, **middle** or **bottom**.

### iOS Example:

```csharp
warning = navBar.bottom.AddNotification (UIImage.FromFile ("Icons/warning.png"), null, true);
```

### Android Example:

```csharp
warning = navBar.bottom.AddNotification (Resource.Drawable.warning, null, true);
```

## Initially Displaying your NavBar

Once you have loaded your **Action Nav Bar** and added the buttons, tools, and/or notifications required by you mobile app, you will need to do the following to initially display it.

### iOS Example:

```csharp
// Request that the initial view being controlled by the NavBar be displayed
navBar.DisplayDefaultView ();
```

### Android Example:

```csharp
protected override void OnStart ()
{
    base.OnStart ();

    //-----------------------------------------
    // Ask the Nav Bar to display the first view
    //-----------------------------------------
    navBar.DisplayDefaultView();
}
```

## Building Sub UIViews for NavBar to Control in iOS

The **Action Nav Bar** **Button** and **AutoDisposingButton** types can automatically control a `UIView` attached to them when they issue a `RequestNewView` event. To create subviews in Xcode for these button types do the following:

First, add a `UIView` to the project and call it “SettingsView” which will create a `SettingsView.xib` file.

Next, add a C# class named “SettingsView.cs”, and add the following code to it:

```csharp
using System;
using System.Drawing;
using Foundation;
using UIKit;
using CoreGraphics;
using ObjCRuntime;

using ActionComponents;

namespace NavBarTester
{
    [Register ("SettingsView")]
    partial class SettingsView : UIView
    {
        #region Public Properties
        public ACNavBar bar;
        #endregion 

        #region Static Class Methods
        /// <summary>
        /// The Factory assembles a <see cref="NavBarTester.SettingsView"/> from the SettingsView.xib file and
        /// instantiates it
        /// </summary>
        /// <param name="controller">The <see cref="MonoTouch.UIKit.UIViewController"/> this view will be attached to</param>
        /// <returns>A fully instantiated <see cref="NavBarTester.SettingsView"/></returns>
        public static SettingsView Factory(UIViewController controller, ACNavBar bar){
            //Load the xib for the view
            var nibObjects = NSBundle.MainBundle.LoadNib("SettingsView", controller, null);

            //Assemble a view from the xib
            SettingsView view=(SettingsView)Runtime.GetNSObject(nibObjects.ValueAt(0));
            view.bar = bar;
            view.Frame = iOSDevice.AvailableScreenBounds;
            view.Initialize ();

            //Return fully instantiated view
            return view;
        }
        #endregion 

        #region Constructors
        public SettingsView() : base() {

        }

        public SettingsView(NSCoder coder): base(coder){

        }

        public SettingsView(NSObjectFlag flag): base(flag){

        }

        public SettingsView(RectangleF bounds): base(bounds){

        }

        public SettingsView(IntPtr ptr): base(ptr){

        }

        /// <summary>
        /// Initialize this instance.
        /// </summary>
        /// <remarks>Use this method to fully initialize your view setting up any outlets or actions
        /// specified in your .xib file</remarks>
        public void Initialize(){
            //TODO: Add you initialization code here, wire-up controls, events, etc.

        }
        #endregion
    }
}
```

**NOTE**: Without this file, you’ll not be able to create outlets in Xcode for you new view. Now build your project and double-click the `SettingsView.xib` file, switch the type of the default view to `SettingsView`, and then add any needed elements and outlets to it. Save the file and return to Visual Studio.

Finally, edit the `Initialize` method of the `SettingsView.cs` file and wire-up any outlets you’ve created in Xcode. Now your view is ready to use in your **Action Nav Bar**.

## Ensuring Your NavBar Behaves as Expected on Android

As noted above you _MUST_ set any view that will be controlled by the **Action Nav Bar** visibility to gone initially. This is a requirement as the **Action Nav Bar** controls views within its same `RelativeLayout` by adjusting their visibility.

If you wish to build the sub views as different `.axml` files you can, create an `AddAutoDisposingButton` and listen to it’s `RequestNewView` event, load your subview using `subView = (View)LayoutInflater.Inflate (Resource.Layout.subView,null);` (or use a `ACViewController` see it’s documentation for more details) and attach the subview to the `ACNavBarButton‘s` attachedView property.

You also need to make sure that you override your Action’s `OnStart` method and call the **NavBar‘s** `DisplayDefaultView` to ensure that the **Action Nav Bar** is correctly configured and that the required sub view is displayed. Example:

```csharp
protected override void OnStart ()
{
    base.OnStart ();

    //-----------------------------------------
    // Ask the Nav Bar to display the first view
    //-----------------------------------------
    navBar.DisplayDefaultView();
}
```

## Saving and Restoring NavBar State on Android

You are responsible for saving and restoring the state of your **Action Nav Bar** in response to things like device rotation. For any property that you wish to remember, override you Action’s `OnSaveInstanceState` and save the properties to the Action’s Bundle. Example:

```csharp
protected override void OnSaveInstanceState (Bundle outState)
{
    //Save the NavBar's selected button before the state change
    outState.PutInt("SelectedButton",navBar.SelectedButtonId ());
    outState.PutBoolean("Hidden",navBar.Hidden);

    base.OnSaveInstanceState (outState);
}
```

During your Action’s `OnCreate` method check for the existence of the Bundle file and reset your NavBar to the saved properties. Example:

```csharp
...
//Are we rehydrating after a state change?
if (bundle!=null) {
    //Yes, attempt to restore the previously selected NavBar button
    navBar.rehydrationId=bundle.GetInt ("SelectedButton");
    navBar.Hidden=bundle.GetBoolean("Hidden");
}
```

# Trial Version

The Trial version of **Action Nav Bar** is fully functional however the bar background is watermarked. The fully licensed version removes this watermark.