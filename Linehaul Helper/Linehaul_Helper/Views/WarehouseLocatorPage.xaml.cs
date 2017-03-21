using Linehaul_Helper.ViewModels;
using Plugin.Geolocator;
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
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Linehaul_Helper.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WarehouseLocatorPage : ContentPage
    {
        public WarehouseLocatorPage()
        {
            InitializeComponent();
            BindingContext = new WarehouseLocatorPageViewModel();

            //bool isGeolocationEnabled = false;
            //try
            //{
            //    var locator = CrossGeolocator.Current;
            //    locator.DesiredAccuracy = 50000;
            //    var position = locator.GetPositionAsync(timeoutMilliseconds: 100).Result;
            //    Debug.WriteLine("Position Status: {0}", position.Timestamp);
            //    Debug.WriteLine("Position Latitude: {0}", position.Latitude);
            //    Debug.WriteLine("Position Longitude: {0}", position.Longitude);

            //    isGeolocationEnabled = true;
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine("Unable to get location, may need to increase timeout: " + ex);
            //}

            var stack = new StackLayout { Spacing = 0 };

            //var map = new Map(
            //    MapSpan.FromCenterAndRadius(
            //        new Position(37, -122), Distance.FromMiles(0.3)))
            //{
            //    IsShowingUser = false,
            //    HeightRequest = 100,
            //    WidthRequest = 960,
            //    VerticalOptions = LayoutOptions.FillAndExpand
            //};
            //stack.Children.Add(map);

            Content = stack;

            //Content = new Label() { Text = "Not done yet." };
        }
    }
}
