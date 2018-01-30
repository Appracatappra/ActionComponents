# Action Table Component

## Getting Started with Action Table

**Action Table** is available exclusively as part of the Action Component Suite by Appracatappra, LLC. To use an **Action Table** in your mobile app include the `ActionComponent.dll` component and reference the following using statement in your C# code:

```csharp
using ActionComponents;
```

## Creating the Action Table

In iOS, an **Action Table** can either be defined in a .xib file or directly in code such as:

```csharp
ACTableViewController Action Table;
…


// Create new Action Table and set it's style
actionTable = new ACTableViewController (UITableViewStyle.Grouped, new RectangleF(0,0,360,480));
AddSubview (actionTable.TableView);
actionTable.cellSelectionStyle = UITableViewCellSelectionStyle.None;
```

On Android either define your **Action Table** in a `.axml` file or again in code:

```csharp
private ACTableViewController settingsList;
...

// Gain Access to all views and controls in our layout
settingsList = FindViewById<ACTableViewController> (Resource.Id.settingList);

//---------------------------------------------
// Configure the settings list
//---------------------------------------------
settingsList.activity = this;
```

**WARNING!** You must set the activity property of you **Action Table** to the Activity that it is running in first, before using any other properties or methods else it may fail to run or render correctly!

## Providing Data

To provide data for your **Action Table**, respond to the `RequestData` event of the Action Table‘s `dataSource` property. You will create a collection of one or more `ACTableSections` , each containing one or more `ACTableItems` that will define the structure and type of your table such as:

```csharp
// Wire-up data request event
actionTable.dataSource.RequestData += (dataSource) => {
    // Populate table with data
    var section = dataSource.AddSection("Section One");

    // Add items to table
    section.AddItem("Item 1",true);
    section.AddItem("Item 2",true);
};
```

Finally, call the **Action Table‘s** `LoadData()` method to display the table with all of the data that you have defined. Example:

```csharp
// Display table
actionTable.LoadData ();
```

## Adding Accessories

More complex tables such as user settings can be created by **Action Table** with ease by using the helper functions of the `ACTableItem` class such as this for iOS:

```csharp
// Wire-up data request event
actionTable.dataSource.RequestData += (dataSource) => {
    // Populate table with data
    var section = dataSource.AddSection("Accessory Types");

    // Add items to table
    section.AddItem("Switch",true).AddAccessorySwitch(false);
    section.AddItem("Stepper {0}",true).AddAccessoryStepper(1,10,1,1);
    section.AddItem("Slider {0:0}",true).AddAccessorySlider(1,100,50);
    section.AddItem("Image",true).AddAccessoryActionImageView("Icons/my-profile.png");
    section.AddItem("Button",true).AddAccessoryButton(UIButtonType.RoundedRect,50,"OK");
    section.AddItem("Text",true).AddAccessoryTextField(150,"<enter text>","");
};
```

Or this for Android:

```csharp
// Wire-up data request event
settingsList.dataSource.RequestData += (dataSource) => {
    // Populate table with data
    var section = dataSource.AddSection("Accessory Types",Resource.Drawable.gear);

    // Add items to table
    section.AddItem("Switch","Maps to ToggleButton",true).AddAccessorySwitch(false);
    section.AddItem("Stepper {0:0}","Maps to SeekBar",true).AddAccessoryStepper(1,10,1,1);
    section.AddItem("Slider {0:0}","Maps to SeekBar",true).AddAccessorySlider(1,100,50);
    section.AddItem("Image",true).AddAccessoryActionImageView(Resource.Drawable.myprofile);
    section.AddItem("Button",true).AddAccessoryButton(100,"OK");
    section.AddItem("Text",true).AddAccessoryTextField(250,"<enter text>","");
    section.AddItem("More Text",true).AddAccessoryTextField(250,"<more text>","");
};
```

**Note**: Special handling exists for `AddAccessoryStepper` and `AddAccessorySlider`, if the parent `ACTableItem‘s` text contains `“{0}”` or `“{0:0}”` it will be replaced with the value of the accessory as the user interacts with it.

## User Interaction

Handle user interaction with an **Action Table** by responding to an `ACTableViewController`, `ACTableSection` or `ACTableItem‘s` `ItemSelected` event such as:

```csharp
// Wire-up item selection
actionTable.ItemsSelected += (item) => {
    // Display the selected item
    Console.WriteLine("Selected {0}",item.title);
};
```

# Trial Version

The Trial version of Action Table is fully functional however the background is watermarked. The fully licensed version removes this watermark.