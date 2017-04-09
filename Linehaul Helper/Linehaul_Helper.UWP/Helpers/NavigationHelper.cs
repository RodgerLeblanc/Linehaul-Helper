using Linehaul_Helper.Interfaces;
using Linehaul_Helper.UWP.Helpers;
using Xamarin.Forms;

[assembly: Dependency(typeof(NavigationHelper))]
namespace Linehaul_Helper.UWP.Helpers
{
    class NavigationHelper : INavigationHelper
    {
        public NavigationHelper() { }

        public void NavigateToHomeScreen() { }
    }
}
