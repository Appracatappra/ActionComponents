using System;
using Android.Content;
using Android.Widget;

namespace ActionComponents
{
	/// <summary>
	/// The Appracatappra License Manager handles verfiying your licensed version of a Appracatappra developer product
	/// purchased from our online store. You will need to provide the customer information used to purchase the product,
	/// the product license that was sent to you in your purchase receipt and a product activation key. You can generate
	/// the activation key at http://appracatappra.com/checkout/activate-license/. You have a limited number of product 
	/// activations so please make a copy of your Activation Key and keep it in a safe place.
	/// 
	/// Typically, you will provide this information to the <c>AppracatappraLicenseManager</c> when the app first starts
	/// in the <c>main</c> method of the <c>Main.cs</c> file.
	/// 
	/// Failure to include the requested information will result in a Toast style popup being displayed that reads,
	/// "Unlicensed Appracatappra Product" whenever an Action Component is used.
	/// </summary>
	public static class AppracatappraLicenseManager
	{
		#region Private Variables
		/// <summary>
		/// The verification key.
		/// </summary>
		private static string key = "0000000000-0000";

		/// <summary>
		/// The developer's first name.
		/// </summary>
		private static string fname = "";

		/// <summary>
		/// The developer's last name.
		/// </summary>
		private static string lname = "";

		/// <summary>
		/// The developer's email address.
		/// </summary>
		private static string email = "";

		/// <summary>
		/// The product license key.
		/// </summary>
		private static string lkey = "";

		/// <summary>
		/// The activation key.
		/// </summary>
		private static string akey = "";
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets or sets the first name of the developer used to purchase the component.
		/// </summary>
		/// <value>The first name.</value>
		public static string FirstName {
			get { return fname; }
			set {
				fname = value;
				UpdateVerification();
			}
		}

		/// <summary>
		/// Gets or sets the last name of the developer used to purchase the component.
		/// </summary>
		/// <value>The last name.</value>
		public static string LastName {
			get { return lname; }
			set {
				lname = value;
				UpdateVerification();
			}
		}

		/// <summary>
		/// Gets or sets the email address of the developer used to purchase the component.
		/// </summary>
		/// <value>The email.</value>
		public static string Email {
			get { return email; }
			set {
				email = value;
				UpdateVerification();
			}
		}

		/// <summary>
		/// Gets or sets the license key that was sent to you when the product was purchased.
		/// </summary>
		/// <value>The license key.</value>
		/// <remarks>Your product license key is also available from http://appracatappra.com/checkout/license-keys/</remarks>
		public static string LicenseKey {
			get { return lkey; }
			set {
				lkey = value;
				UpdateVerification();
			}
		}

		/// <summary>
		/// Gets or sets the activation key generated from your customer information and product license from the
		/// Appracatappra website.
		/// </summary>
		/// <value>The activation key.</value>
		/// <remarks>Visit http://appracatappra.com/checkout/activate-license/ to generate your license key.</remarks>
		public static string ActivationKey {
			get { return akey; }
			set { 
				akey = value; 
				UpdateVerification();
			}
		}

		/// <summary>
		/// Gets a value indicating whether the product license is valid.
		/// </summary>
		/// <value><c>true</c> if license is valid; otherwise, <c>false</c>.</value>
		public static bool LicenseIsValid {
			get { return (key == akey); }
		}

		/// <summary>
		/// Gets the name of the product being licensed.
		/// </summary>
		/// <value>The name of the product.</value>
		public static string ProductName {
			get { return "Action Components"; }
		}
		#endregion

		#region Internal Methods
		/// <summary>
		/// Updates the verification key.
		/// </summary>
		internal static void UpdateVerification() {
			var a = GoogleAnalyticsTrackingKey(FirstName);
			var b = GoogleAnalyticsTrackingKey(LastName);
			var c = GoogleAnalyticsTrackingKey(Email);
			var d = GoogleAnalyticsTrackingKey(LicenseKey);
			key = $"{a}-{b}-{c}-{d}";
		}

		/// <summary>
		/// Generates a Google Analytic tracking key used to track product usage via the Google Analytic dashboard.
		/// </summary>
		/// <returns>The analytics tracking key.</returns>
		/// <param name="track">Track.</param>
		internal static int GoogleAnalyticsTrackingKey(string track) {
			var phrase = "The quick brown fox jumped spryly over the lazy dog's prized bone.";
			var numbers = "9876543210";
			var hash = 0;

			// Generate unique tracking key to register product for Google Analytics
			var salt = numbers + ProductName + phrase;
			var key = track.ToLower();
			for (int i = 0; i < key.Length; ++i) {
				var c = key[i];
				var n = salt.IndexOf(c);
				hash += (i + 1) * n;
			}

			// Return results
			return hash;
		}

		/// <summary>
		/// Validates the license and displays a toast popup if the license isn't valid.
		/// </summary>
		internal static void ValidateLicense(Context context) {
			// Is the license valid?
			if (!LicenseIsValid) {
				// No, display default message
				Android.Widget.Toast.MakeText(context, "Unlicensed Appracatappra Product", Android.Widget.ToastLength.Long).Show();
				Console.WriteLine("ISSUE: Unlicensed Appracatappra Product");
			}
		}
		#endregion
	}
}
