# Activating Action Components License

Thank you for purchasing our [Action Components](http://appracatappra.com/products/action-components/) suite of User Interface and time-saving tools for the Xamarin Platform and Visual Studio. We hope you enjoy using our components and that they become a valuable part of your developer environment. 

Before your copy of **Action Components** can be successfully used in any of your app projects, it will need to be activated using the [Activate License](http://appracatappra.com/checkout/activate-license/) form on the Appracatappra website. 

The activation process needs to only be done once and will result in an **Activation Key** that you will need to use with the `AppracatappraLicenseManager` built into the **Action Components** suite.

Failure to add your activation information to the `AppracatappraLicenseManager` **before** using any **Action Component** will result in the following Toast popup message being displayed:

> Unlicensed Appracatappra Product

The following topics are covered:

* [Activating Your License](#Activating-Your-License)
* [Adding Your License to an App Project](#Adding-Your-License-to-an-App-Project)


<a name="Activating-Your-License"></a>
## Activating Your License

To generate your key, visit [Activate License](http://appracatappra.com/checkout/activate-license/) and fill out the following form:

![](Images/AKey01.png)

The Customer and Product Information **must _exactly_** match (all fields are case sensitive) the information provided when the product was purchased. You can get your Customer Information from the purchase receipt that was emailed to you or by visiting our [Customer Profile](http://appracatappra.com/checkout/customer-profile/) page:

![](Images/AKey04.png)

You can find the Product Information on the purchase receipt that was emailed to you or by visiting [Purchase History](http://appracatappra.com/checkout/purchase-history/) page:

![](Images/AKey02.png)

Click on the **View Details and Downloads** link to view the details of the purchase:

![](Images/AKey03.png)

With the form correctly filled in, click the **Activate** button to generate your **Activation Key**:

![](Images/AKey05.png)

Copy the **Activation Key** and store it in a safe place, you'll need it for any app project that uses **Action Components**. This key is only generated during product activation and you have a limited number of activations. To check on the number of activations, visit our [License Keys](http://appracatappra.com/checkout/license-keys/) page:

![](Images/AKey06.png)

<a name="Adding-Your-License-to-an-App-Project"></a>
## Adding Your License to an App Project

With your **Activation Key** generated and your product marked as **Active** in the [License Keys](http://appracatappra.com/checkout/license-keys/) page, you'll need to set the License Information in the `AppracatappraLicenseManager` before any calls are made to an Action Component. This is typically done in the `Main` method of the `Main.cs` file before the app starts for iOS. For example:

```csharp
using UIKit;
using ActionComponents;

namespace iOSActionTiles
{
	public class Application
	{
		// This is the main entry point of the application.
		static void Main(string[] args)
		{
			// Set license information BEFORE any components are called to suppress the
			// "Unlicensed Appracatappra Product" Toast popup.
			AppracatappraLicenseManager.FirstName = "Jane";
			AppracatappraLicenseManager.LastName = "Doe";
			AppracatappraLicenseManager.Email = "jane.doe@company.com";
			AppracatappraLicenseManager.LicenseKey = "985b00013352366cda67994a1fe775c9";
			AppracatappraLicenseManager.ActivationKey = "274-822-9945-3345";

			// if you want to use a different Application Delegate class from "AppDelegate"
			// you can specify it here.
			UIApplication.Main(args, null, "AppDelegate");
		}
	}
}
```

Or the `OnCreate` method of the `MainActivity.cs` file for Android:

```csharp
using ActionComponents;
...

[Activity(Label = "ActionComponentExplorer", Theme = "@android:style/Theme.NoTitleBar", MainLauncher = true, Icon = "@mipmap/icon")]
public class MainActivity : Activity
{
	int count = 1;

	protected override void OnCreate(Bundle savedInstanceState)
	{
		base.OnCreate(savedInstanceState);
		
		// Set license information BEFORE any components are called to suppress the
		// "Unlicensed Appracatappra Product" Toast popup.
		AppracatappraLicenseManager.FirstName = "Jane";
		AppracatappraLicenseManager.LastName = "Doe";
		AppracatappraLicenseManager.Email = "jane.doe@company.com";
		AppracatappraLicenseManager.LicenseKey = "985b00013352366cda67994a1fe775c9";
		AppracatappraLicenseManager.ActivationKey = "274-822-9945-3345";
		
		...
	}
}
```

The Customer and Product Information provided to the `AppracatappraLicenseManager` **must _exactly_** match (all fields are case sensitive) the information provided when the product was activated above. 

Failure to add your activation information to the `AppracatappraLicenseManager` (or providing invalid information) will result in the following Toast popup message being displayed:

> Unlicensed Appracatappra Product

If you are having issues activating your product license, please contact [Customer Support](http://appracatappra.com/support/).