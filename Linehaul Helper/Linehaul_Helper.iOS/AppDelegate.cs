
using FFImageLoading.Forms.Touch;
using Foundation;
using Linehaul_Helper.Helpers;
using UIKit;

namespace Linehaul_Helper.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();
            Xamarin.FormsMaps.Init();
            Xamarin.FormsGoogleMaps.Init(ApiKeys.GoogleMaps);
            CachedImageRenderer.Init();

            LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}
	}
}
