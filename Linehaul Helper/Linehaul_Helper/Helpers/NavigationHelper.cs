using Linehaul_Helper.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Linehaul_Helper.Helpers
{
    class NavigationHelper
    {
        public static async Task NavigationPushAsync(Page page)
        {
            try
            {
                await GetNavigationPage().PushAsync(page, false);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error pushing a page: " + ex.Message);
            }
        }

        public static async Task NavigationPopAsync()
        {
            await GetNavigationPage().PopAsync(false);
        }

        private static NavigationPage GetNavigationPage()
        {
            var mainPage = Application.Current.MainPage;

            if (mainPage is MasterDetailPage)
                return ((MasterDetailPage)mainPage).Detail as NavigationPage;
            else if (mainPage is NavigationPage)
                return mainPage as NavigationPage;
            else
                throw new LayoutException("The application isn't a NavigationPage");
        }
    }
}
