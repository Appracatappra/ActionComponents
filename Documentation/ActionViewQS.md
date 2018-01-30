# Action View Components
## Getting Started with Action Views

**Action View** is available exclusively as part of the Action Component Suite by Appracatappra, LLC. To use an **Action View** in your mobile app include the `ActionComponent.ddl` and reference the following using statement in your C# code:

```csharp
using ActionComponents;
```

## Using Dragable

Several of the elements of **Action View** feature built-in drag handling with optional constraints on the X and/or Y axis. In the following example a `ACImageView` has been added to our `.xib` file. In code we will make it draggable, lock it’s Y coordinate in place and allow the X coordinate to be drug within a given range:

```csharp
// Set thumb to be draggable and apply limits to it's movement
dragThumb.draggable = true;
dragThumb.xConstraint.constraintType = ACViewDragConstraintType.Constrained;
if (iOSDevice.isPad) {
    dragThumb.xConstraint.minimumValue = 140f;
    dragThumb.xConstraint.maximumValue = 920f;
} else {
    dragThumb.xConstraint.minimumValue = 58f;
    dragThumb.xConstraint.maximumValue = 272f;
}
dragThumb.yConstraint.constraintType = ACViewDragConstraintType.Locked;
```

The drag controls work exactly the same on Android as well.

## User Interaction

Several of the elements of **Action View** were designed to handle interaction via their touched, moved, or released events. The following is an example of handling a user touch on an `ACLabel` added to our `.xib `file:

```csharp
// Show graphic when the label is touched
labelWhyChoose.Touched += (view) => {
    // Define Animation
    UIView.BeginAnimations("ShowWhyChoose");
    UIView.SetAnimationDuration(1.0f);

    // Animate property
    whyChooseBox.Alpha=1.0f;

    // Execute Animation
    UIView.CommitAnimations();
};
```

And here is an example of handling a move event for our `dragThumb` `ACImageView` defined above:

```csharp
// Wire-up the moved event
dragThumb.Moved += (view) => {
    // Convert the Left position into an index
    var index = ThumbPositionToIndex(view.Frame.Left);

    // Use shortcut feature of ACImageView to change image
    // NOTE: Existing image is automatically purged from memory before change
    switch(index){
    case 0:
        acBadge.FromFile ("ACBadge.png");
        banner.FromFile("Banner.png");
        break;
    case 1:
        acBadge.FromFile ("ACBadge2.png");
        banner.FromFile("Banner2.png");
        break;
    case 2:
        acBadge.FromFile ("ACBadge3.png");
        banner.FromFile("Banner3.png");
        break;
    case 3:
        acBadge.FromFile ("ACBadge4.png");
        banner.FromFile("Banner4.png");
        break;
    case 4:
        acBadge.FromFile ("ACBadge5.png");
        banner.FromFile("Banner5.png");
        break;
    }
};
```

Again, user interaction is the same on Android.

## ACViewController

The `ACViewController` is useful when handling the views controlled by a NavBar Action Component. Create your layout in a `.axml` file as usual. Next create a new view controller class to handle the layout and inherit from `ACViewController`, example:

```csharp
using ActionComponents;
...

namespace APTest.Android
{
    public class DocumentViewController : ACViewController 
    {
        #region Private Variables
        // Storage for our UI widgets
        private ACWebView webView;
        ...
        #endregion 

        #region Constructors
        // Required minimal Constructor
        public DocumentViewController (Activity activity, int resourceID) :  base(activity, resourceID) {

        }
        #endregion 

        #region Override Methods
        // Required override of initialize to wire-up your widgets
        public override void Initialize ()
        {
            // Access interface items
            webView = activity.FindViewById<ACWebView> (Resource.Id.webView);
            ...

            //---------------------------------------------
            // Configure Webview
            //---------------------------------------------
            webView.LoadAsset (String.Format ("{0}About.html",currentComponent));
            ...

        }
        #endregion 
    }
}
```

Use the following code to call the controller, load the view it controls and wire-up its widgets (this example is from a NavBar Action Component):

```csharp
// Add document view
navBar.top.AddAutoDisposingButton (Resource.Drawable.book,true,false).RequestNewView += (responder) => {
    // Bring view into existance
    viewDocument = new DocumentViewController(this,Resource.Layout.DocumentView);

    // Attach view to the button
    responder.attachedView = viewDocument.view;
};
```

# Trial Version

The Trial version of **Action Views** are fully functional however the background of several elements are watermarked. The fully licensed version removes these watermarks.