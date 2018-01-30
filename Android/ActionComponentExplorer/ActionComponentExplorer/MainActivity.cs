using Android.App;
using Android.Widget;
using Android.OS;
using ActionComponents;
using Android.Graphics;

namespace ActionComponentExplorer
{
	[Activity(Label = "ActionComponentExplorer", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			//Button button = FindViewById<Button>(Resource.Id.myButton);

			//button.Click += delegate { button.Text = $"{count++} clicks!"; };

			// Access the slider
			var slider = FindViewById<ACSlider>(Resource.Id.brightnessSlider);
			slider.Icon = BitmapFactory.DecodeResource(Resources, Resource.Drawable.iconBrightness);

			// Access text
			var brightnessLevel = FindViewById<TextView>(Resource.Id.brightnessLevel);

			// Wire-up changes
			slider.ValueChanged += (fillPercent) => {
				brightnessLevel.Text = $"{fillPercent}%";
			};
		}
	}
}

