using HtmlAgilityPack;
using Linehaul_Helper.Models;
using Linehaul_Helper.ViewModels;
using Newtonsoft.Json;
using Plugin.Geolocator;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
//using Xamarin.Forms.Maps;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace Linehaul_Helper.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WarehouseLocationPage : ContentPage
    {
        private WarehouseLocationPageViewModel _warehouseLocationPageViewModel;
        public WarehouseLocationPage()
        {
            InitializeComponent();
            BindingContext = _warehouseLocationPageViewModel = new WarehouseLocationPageViewModel();

            _warehouseLocationPageViewModel.WarehouseLocationsChanged += (sender, args) =>
            {
                //LoadMapPinsFromViewModel();
            };

            //SetIsShowingUser();

            //map.MoveToRegion(MapSpan.FromCenterAndRadius(
            //    new Xamarin.Forms.GoogleMaps.Position(45.496080, -73.769532), Distance.FromKilometers(200)));

            //LoadMapPinsFromViewModel();

            //var stack = new StackLayout { Spacing = 0 }; 

            //var map = new Map(
            //    MapSpan.FromCenterAndRadius(
            //        new Position(45.496080, -73.769532), Distance.FromMiles(100)))
            //{
            //    IsShowingUser = true,
            //    HeightRequest = 100,
            //    WidthRequest = 960,
            //    VerticalOptions = LayoutOptions.FillAndExpand
            //};
            //stack.Children.Add(map);

            //var pinPosition = new Position(45.877175, -72.542920); // Latitude, Longitude
            //var pin = new Pin
            //{
            //    Type = PinType.Place,
            //    Position = pinPosition,
            //    Label = "Dicom Drummond - DRU",
            //    Address = "330 RUE ROCHELEAU, DRUMMONDVILLE, QC, J2C7S7"
            //};
            //map.Pins.Add(pin);

            //Content = stack;

            //Content = new Label() { Text = "Not done yet." };
        }

        //private void LoadMapPinsFromViewModel()
        //{
        //    map.Pins.Clear();
        //    List<WarehouseLocation> dicomWarehouseLocations = _warehouseLocationPageViewModel.WarehouseLocations.ToList();
        //    foreach (var location in dicomWarehouseLocations)
        //    {
        //        map.Pins.Add(new Pin
        //        {
        //            Type = PinType.Place,
        //            Position = new Xamarin.Forms.GoogleMaps.Position(location.Position.Latitude, location.Position.Longitude),
        //            Label = location.Name,
        //            Address = location.Address,
        //            Tag = "Tag:" + location.Name
        //        });
        //    }
        //}

        //private void SetIsShowingUser()
        //{
        //    try
        //    {
        //        var locator = CrossGeolocator.Current;
        //        locator.DesiredAccuracy = 50000;
        //        var position = locator.GetPositionAsync(timeoutMilliseconds: 100).Result;
        //        if (position != null)
        //            map.IsShowingUser = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine("Unable to get location, may need to increase timeout: " + ex);
        //    }
        //}
    }
}
