using Linehaul_Helper.Helpers;
using Linehaul_Helper.Interfaces;
using Linehaul_Helper.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Linehaul_Helper.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeMasterDetail : MasterDetailPage
    {
        private MainMasterPage _mainMasterPage;

        public HomeMasterDetail()
        {
            InitializeComponent();

            GeneralSettingsObject settings = Settings.GetGeneralSettingsObject();
            if (settings?.LastDetailPageType?.GetTypeInfo()?.BaseType != typeof(ContentPage))
               settings = new GeneralSettingsObject();
            var lastDetailPage = (Page)Activator.CreateInstance(
                settings?.LastDetailPageType ?? typeof(WelcomePage));

            Master = _mainMasterPage = new MainMasterPage();
            Detail = new NavigationPage(lastDetailPage);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<MainMasterPageViewModel, Page>(this, Commons.Strings.PageSelectedMessage, (source, page) =>
            {
                Detail = new NavigationPage(page);
                IsPresented = false;

                try
                {
                    GeneralSettingsObject settings = Settings.GetGeneralSettingsObject() ?? new GeneralSettingsObject();
                    settings.LastDetailPageType = page.GetType();
                    Settings.SaveGeneralSettingsObject(settings);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Exception on trying to save GeneralSettingsObject: " + ex.Message);
                }
            });

            MessagingCenter.Subscribe<MainMasterPageViewModel, Page>(this, Commons.Strings.PageSelectedMessage, async (source, page) =>
            {
                await NavigationHelper.NavigationPushAsync(page);
            });

            MessagingCenter.Subscribe<JobsPageViewModel, Page>(this, Commons.Strings.PageSelectedMessage, async (source, page) =>
            {
                await NavigationHelper.NavigationPushAsync(page);
            });
        }

        protected override void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<MainMasterPageViewModel, Page>(this, Commons.Strings.PageSelectedMessage);
            MessagingCenter.Unsubscribe<JobsPageViewModel, Page>(this, Commons.Strings.PageSelectedMessage);

            base.OnDisappearing();
        }

        protected override bool OnBackButtonPressed()
        {
            if (Application.Current.MainPage is MasterDetailPage)
            {
                var md = Application.Current.MainPage as MasterDetailPage;
                var isNotNull = (md != null);
                var cantGoBack = !(md.Detail is NavigationPage) || 
                                (((NavigationPage)md.Detail).Navigation.NavigationStack.Count == 1 && ((NavigationPage)md.Detail).Navigation.ModalStack.Count == 0);

                if (isNotNull && !md.IsPresented && cantGoBack)
                {
                    DependencyService.Get<INavigationHelper>().NavigateToHomeScreen();
                    return true;
                }
            }

            return base.OnBackButtonPressed();
        }
    }
}