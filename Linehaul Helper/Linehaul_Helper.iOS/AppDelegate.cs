
using FFImageLoading.Forms.Touch;
using Foundation;
using Linehaul_Helper.Helpers;
using UIKit;
using Xamarin.Forms;

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

        public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations(UIApplication application, UIWindow forWindow)
        {
            switch (Device.Idiom)
            {
                case TargetIdiom.Phone:
                    return UIInterfaceOrientationMask.Portrait;
                case TargetIdiom.Tablet:
                    return UIInterfaceOrientationMask.Landscape;
                default:
                    return UIInterfaceOrientationMask.Portrait;
            }
        }
    }
}
