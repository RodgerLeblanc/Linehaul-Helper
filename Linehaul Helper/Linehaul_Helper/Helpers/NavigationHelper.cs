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
                await GetNavigationPage().PushAsync(page);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error pushing a page: " + ex.Message);
            }
        }

        public static async Task NavigationPopAsync()
        {
            await GetNavigationPage().PopAsync();
        }

        private static NavigationPage GetNavigationPage()
        {
            MasterDetailPage mainPage = Application.Current.MainPage as MasterDetailPage;
            return mainPage.Detail as NavigationPage;
        }
    }
}
