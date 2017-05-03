using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Content.Res;
using Android.OS;
using Android.Util;

using FFImageLoading.Forms.Droid;
using Xamarin.Forms;
using System.Threading.Tasks;
using Plugin.Permissions;

namespace Linehaul_Helper.Droid
{
    [Activity(Theme = "@style/MyTheme.Splash", Icon = "@drawable/ic_launcher", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            StartActivity(new Intent(this, typeof(MainActivity)));
            Finish();
        }

        public override void OnBackPressed() { }
    }
}