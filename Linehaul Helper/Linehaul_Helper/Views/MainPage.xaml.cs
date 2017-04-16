using Linehaul_Helper.Helpers;
using Linehaul_Helper.Interfaces;
using Linehaul_Helper.Services;
using Linehaul_Helper.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Linehaul_Helper.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private INavigationService _navigationService;

        public MainPage(INavigationService navigationService)
        {
            _navigationService = navigationService;

            NavigationPage.SetHasNavigationBar(this, false);

            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _navigationService.SubscribeToMessagingCenter();
        }

        protected override void OnDisappearing()
        {
            _navigationService.UnsubscribeToMessagingCenter();

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
