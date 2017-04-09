using Linehaul_Helper.Interfaces;
using Xamarin.Forms;
using Linehaul_Helper.iOS.Helpers;

[assembly: Dependency(typeof(NavigationHelper))]
namespace Linehaul_Helper.iOS.Helpers
{
    class NavigationHelper : INavigationHelper
    {
        public NavigationHelper() { }

        public void NavigateToHomeScreen() { }
    }
}