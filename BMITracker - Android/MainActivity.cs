using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace BMITracker___Android
{
	[Activity(Label = "BMITracker___Android", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		//Client ID from from step 1. point 6
		public static string clientId = "<your client id here>";
		public static string commonAuthority = "https://login.windows.net/common";
		//Redirect URI from step 1. point 5<br />
		public static Uri returnUri = new Uri("<your return URI here>");
		//Graph URI if you've given permission to Azure Active Directory in step 1. point 6
		const string graphResourceUri = "https://graph.windows.net";
		public static string graphApiVersion = "2013-11-08";
		//AuthenticationResult will hold the result after authentication completes
		AuthenticationResult authResult = null;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button>(Resource.Id.MyButton);

			button.Click += async (sender, args) =>
			{
				var authContext = new AuthenticationContext(commonAuthority);
				var cacheItems = authContext.TokenCache.ReadItems();
				if (authContext.TokenCache.ReadItems().Count() > 0)
					authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);
				authResult = await authContext.AcquireTokenAsync(graphResourceUri, clientId, returnUri, new PlatformParameters(this));
			};
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);
			AuthenticationAgentContinuationHelper.SetAuthenticationAgentContinuationEventArgs(requestCode, resultCode, data);
		}
	}
}

