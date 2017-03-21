using Linehaul_Helper.Droid;
using Linehaul_Helper.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(NavigationHelper))]
namespace Linehaul_Helper.Droid
{
    public class NavigationHelper : INavigationHelper
    {
        public NavigationHelper() { }

        public void NavigateToHomeScreen()
        {
            var activity = MainActivity.CurrentActivity;
            activity.MoveTaskToBack(true);
        }
    }
}