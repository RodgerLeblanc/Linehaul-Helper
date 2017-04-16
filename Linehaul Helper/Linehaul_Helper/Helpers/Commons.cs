using System;
using System.Diagnostics;
using System.Reflection;
using Newtonsoft.Json;
using Xamarin.Forms;
using System.Threading.Tasks;
using Linehaul_Helper.Exceptions;

namespace Linehaul_Helper.Helpers
{
    public class Commons
    {
        public class Strings
        {
            public static readonly string PageSelectedMessage = "PageSelectedMessage";

            public static string UnitInfosFolderName = "UnitInfos";
            public static string UnitInfosFileName = "unitinfos.json";
        }

        public enum TypeOfUserEnum { Caregiver, Client };

        public void ShowResources(Type classType)
        {
#if DEBUG
            var assembly = classType.GetTypeInfo().Assembly;
            foreach (var res in assembly.GetManifestResourceNames())
            {
                System.Diagnostics.Debug.WriteLine(classType.Name + " found resource: " + res);
            }
#endif
        }

        async public static Task DetailNavigationPushAsync(Page page)
        {
            if (!(Application.Current?.MainPage is MasterDetailPage))
                throw new LayoutException("The application doesn't have a MasterDetailPage");

            var masterDetail = Application.Current.MainPage as MasterDetailPage;
            var navPage = masterDetail.Detail as NavigationPage;
            await navPage.PushAsync(page);
        }

        public async static Task DetailNavigationPopAsync()
        {
            if (!(Application.Current?.MainPage is MasterDetailPage))
                throw new LayoutException("The application doesn't have a MasterDetailPage");

            var masterDetail = Application.Current.MainPage as MasterDetailPage;
            var navPage = masterDetail.Detail as NavigationPage;
            await navPage.PopAsync();
        }

        async public static Task NavigationPagePushAsync(Page page)
        {
            if (Application.Current?.MainPage is MasterDetailPage)
            {
                await DetailNavigationPushAsync(page);
                return;
            }

            if (!(Application.Current?.MainPage is NavigationPage))
                throw new LayoutException("The application isn't a NavigationPage");

            var navPage = Application.Current.MainPage as NavigationPage;
            await navPage.PushAsync(page);
        }

        public async static Task NavigationPagePopAsync()
        {
            if (Application.Current?.MainPage is MasterDetailPage)
            {
                await DetailNavigationPopAsync();
                return;
            }

            if (!(Application.Current?.MainPage is NavigationPage))
                throw new LayoutException("The application isn't a NavigationPage");

            var navPage = Application.Current.MainPage as NavigationPage;
            await navPage.PopAsync();
        }

        async public static Task DisplayAlert(string title, string message, string cancel)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }

        async public static Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
        }
    }
}