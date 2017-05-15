using HtmlAgilityPack;
using Linehaul_Helper.Models;
using Linehaul_Helper.Services;
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
using Xamarin.Forms.Maps;
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
            BindingContext = _warehouseLocationPageViewModel = new WarehouseLocationPageViewModel(new WarehouseDatabaseService());

            _warehouseLocationPageViewModel.WarehouseLocationsChanged += (sender, args) =>
            {
                LoadMapPinsFromViewModel();
            };

            map.MoveToRegion(MapSpan.FromCenterAndRadius(
                new Xamarin.Forms.Maps.Position(45.496080, -73.769532), Distance.FromKilometers(800)));
        }

        private void LoadMapPinsFromViewModel()
        {
            map.Pins.Clear();
            List<WarehouseLocation> dicomWarehouseLocations = _warehouseLocationPageViewModel.WarehouseLocations?.ToList() ?? new List<WarehouseLocation>();
            foreach (var location in dicomWarehouseLocations)
            {
                map.Pins.Add(new Pin
                {
                    Type = PinType.Place,
                    Position = new Xamarin.Forms.Maps.Position(location.Position.Latitude, location.Position.Longitude),
                    Label = location.Name,
                    Address = location.Address
                });
            }
        }

        private async Task SetIsShowingUser()
        {
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50000;
                var position = await locator.GetPositionAsync(timeoutMilliseconds: 10000);
                if (position != null)
                {
                    map.IsShowingUser = true;
                    map.MoveToRegion(MapSpan.FromCenterAndRadius(
                        new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude), Distance.FromKilometers(50)));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to get location, may need to increase timeout: " + ex);
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            LoadMapPinsFromViewModel();

            _warehouseLocationPageViewModel.GetWarehouseLocations();

            await SetIsShowingUser();
        }
    }
}
