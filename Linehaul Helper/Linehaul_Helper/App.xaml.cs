﻿using Linehaul_Helper.Helpers;
using Linehaul_Helper.Interfaces;
using Linehaul_Helper.Services;
using Linehaul_Helper.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Linehaul_Helper
{
    public partial class App : Application
    {
        private INavigationService _navigationService;

        public App()
        {
            InitializeComponent();

            _navigationService = new NavigationService();

            SetMainPage();
        }

        public static void SetMainPage()
        {
            //Current.MainPage = new HomeMasterDetail();

            var navigationPage = new NavigationPage(new MainPage());
            Current.MainPage = navigationPage;

            //navigationPage.Pushed += HandleNavigationPagePushPop;
            //navigationPage.Popped += HandleNavigationPagePushPop;
        }

        static void HandleNavigationPagePushPop(object sender, NavigationEventArgs args)
        {
            NavigationPage.SetHasNavigationBar(args.Page, false);

            if (args.Page is MainPage) return;

            args.Page.Focused += (s, a) =>
            {
                NavigationPage.SetHasNavigationBar(args.Page, true);
            };
        }

        protected override void OnStart()
        {
            base.OnStart();

            _navigationService.SubscribeToMessagingCenter();
        }

        protected override void OnSleep()
        {
            _navigationService.UnsubscribeToMessagingCenter();

            base.OnSleep();
        }
    }
}
