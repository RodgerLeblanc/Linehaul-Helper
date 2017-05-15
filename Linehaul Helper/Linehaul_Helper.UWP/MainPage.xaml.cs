using Windows.Foundation;
using Windows.UI.ViewManagement;

namespace Linehaul_Helper.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            Xamarin.FormsMaps.Init("IQnU2ivFJg3N7wPCaT3z~AbFSYix51ZdXXoogdDq0Pw~AkgTiN6Q8R6FX0CM7CfnK9OOORe18JtPJpB9nL_QhbooNd1yC__e9kD4z-ZQfseX");

            this.InitializeComponent();

            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(350, 600));
            ApplicationView.PreferredLaunchViewSize = new Size(350, 600);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            LoadApplication(new Linehaul_Helper.App());
        }
    }
}