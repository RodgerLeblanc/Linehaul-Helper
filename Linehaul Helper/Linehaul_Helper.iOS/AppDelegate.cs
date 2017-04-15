
using Foundation;
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
            Xamarin.FormsGoogleMaps.Init("AIzaSyBR7pqaFMZqOgnXnmv7CD3Hf7qQoABHOqE");

            LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}
	}
}
