﻿namespace Linehaul_Helper.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            Xamarin.FormsMaps.Init("IQnU2ivFJg3N7wPCaT3z~AbFSYix51ZdXXoogdDq0Pw~AkgTiN6Q8R6FX0CM7CfnK9OOORe18JtPJpB9nL_QhbooNd1yC__e9kD4z-ZQfseX");

            this.InitializeComponent();
            LoadApplication(new Linehaul_Helper.App());
        }
    }
}