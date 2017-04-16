using Linehaul_Helper.Services;
using Linehaul_Helper.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Linehaul_Helper
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            SetMainPage();
        }

        public static void SetMainPage()
        {
            //Current.MainPage = new HomeMasterDetail();

            var navigationService = new NavigationService();
            Current.MainPage = new NavigationPage(new MainPage(navigationService));
        }
    }
}
