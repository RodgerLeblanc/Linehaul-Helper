using HtmlAgilityPack;
using Linehaul_Helper.ViewModels;
using Newtonsoft.Json;
using Plugin.Geolocator;
using System;
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
        public WarehouseLocationPage()
        {
            InitializeComponent();
            BindingContext = new WarehouseLocationPageViewModel();

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

            //var stack = new StackLayout { Spacing = 0 };

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

            //Content = stack;

            //Content = new Label() { Text = "Not done yet." };
        }

        async private void Button_Clicked(object sender, EventArgs e)
        {
            HttpClient _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (iPad; U; CPU OS 3_2_1 like Mac OS X; en-us) AppleWebKit/531.21.10 (KHTML, like Gecko) Mobile/7B405");
            var url = new Uri("https://www.dicom.com/fr/express/suivi/resultat?__RequestVerificationToken=Ehzl_I2u_ZDWuhkwyC623Mgd2niOtqUlt2CspjLTi8aRh_9iTYT3_nDzVh1N9KhLlQ9y9QiNrKcHpc2pQaRx9NL8Qr81&ReqTrackIds=" + entry.Text);
            var response = await _client.PostAsync(url, null);

            if ((response != null) && (response.StatusCode == HttpStatusCode.OK))
            {
                var httpAsString = await response.Content.ReadAsStringAsync();

                var htmlSource = new HtmlWebViewSource();
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(httpAsString);

                List<string> trackingInfos = doc.DocumentNode.Descendants("table").Where(d => d.GetAttributeValue("class", "") == "inlineTable")
                    .FirstOrDefault()
                    .Descendants("tr")
                    .Skip(1)
                    .Where(tr => tr.Elements("td").Count() > 1)
                    .Select(tr => tr.Elements("td")
                        .Where(td => !String.IsNullOrEmpty(td.InnerText.Trim()))
                        .Select(td => td.InnerText.Trim())
                        .ToList())
                    .FirstOrDefault();

                string title = trackingInfos.ElementAtOrDefault(0);
                string message = $"Tracking # {trackingInfos.ElementAtOrDefault(0)} status is : {trackingInfos.ElementAtOrDefault(1)}.\n" +
                    $"{WebUtility.HtmlDecode(trackingInfos.ElementAtOrDefault(2))}"; 

                await Application.Current.MainPage.DisplayAlert(title, message, "Ok");
            }
        }
    }
}
