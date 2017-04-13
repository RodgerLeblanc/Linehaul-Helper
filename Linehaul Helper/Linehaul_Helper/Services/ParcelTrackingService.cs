using HtmlAgilityPack;
using Linehaul_Helper.CustomEventArgs;
using Linehaul_Helper.Exceptions;
using Linehaul_Helper.Interfaces;
using Linehaul_Helper.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Linehaul_Helper.Services
{
    public class ParcelTrackingService : IParcelTrackingService
    {
        private string _baseUrl = "https://www.dicom.com/fr/express/suivi/resultat?__RequestVerificationToken=Ehzl_I2u_ZDWuhkwyC623Mgd2niOtqUlt2CspjLTi8aRh_9iTYT3_nDzVh1N9KhLlQ9y9QiNrKcHpc2pQaRx9NL8Qr81&ReqTrackIds=";
        private string _httpAsString;
        private bool _isBusy = false;

        public ParcelTrackingService()
        {
        }

        public event EventHandler IsBusyChanged;

        public async Task<ParcelTrackingModel> Track(string trackingNumber)
        {
            if (String.IsNullOrEmpty(trackingNumber) || (String.IsNullOrWhiteSpace(trackingNumber)))
                throw new TrackingNotFoundException("Tracking number can't be empty or null");

            SetIsBusy(true);

            var response = await GetResponseFromServer(trackingNumber);

            SetIsBusy(false);
            return await GetTrackingInfos(response);
        }

        private async Task<HttpResponseMessage> GetResponseFromServer(string trackingNumber)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (iPad; U; CPU OS 3_2_1 like Mac OS X; en-us) AppleWebKit/531.21.10 (KHTML, like Gecko) Mobile/7B405");

                    var url = new Uri(_baseUrl + trackingNumber);
                    var response = await client.PostAsync(url, null);
                    return response;
                }
            }
            catch (Exception ex)
            {
                SetIsBusy(false);
                throw new TrackingNotFoundException("Couldn't retrieve the tracking infos.", _httpAsString, ex);
            }
        }

        async private Task<ParcelTrackingModel> GetTrackingInfos(HttpResponseMessage response)
        {
            if ((response == null) || (response.StatusCode != HttpStatusCode.OK))
                throw new TrackingNotFoundException("Couldn't analyse the web page.", _httpAsString);

            _httpAsString = await response.Content.ReadAsStringAsync();

            var doc = new HtmlDocument();
            doc.LoadHtml(_httpAsString);

            List<string> trackingInfos = ScrapHtmlForTrackingInfos(doc);
            if (trackingInfos.Count < 3)
                throw new TrackingNotFoundException("Tracking infos retrieved by scraping html did not contain required informations.", _httpAsString);

            return new ParcelTrackingModel
            {
                TrackingNumber = WebUtility.HtmlDecode(trackingInfos.ElementAtOrDefault(0)),
                Division = WebUtility.HtmlDecode(trackingInfos.ElementAtOrDefault(1)),
                Status = WebUtility.HtmlDecode(trackingInfos.ElementAtOrDefault(2)),
                LastUpdateString = WebUtility.HtmlDecode(trackingInfos.ElementAtOrDefault(3)),
                Notes = WebUtility.HtmlDecode(trackingInfos.ElementAtOrDefault(4))
            };
        }

        private List<string> ScrapHtmlForTrackingInfos(HtmlDocument doc)
        {
            if (doc == null)
                throw new TrackingNotFoundException("Html document received is null", _httpAsString);

            try
            {
                return doc.DocumentNode.Descendants("table").Where(d => d.GetAttributeValue("class", "") == "inlineTable")
                    .FirstOrDefault()
                    .Descendants("tr")
                    .Skip(1)
                    .Where(tr => tr.Elements("td").Count() > 1)
                    .Select(tr => tr.Elements("td")
                        .Where(td => !String.IsNullOrEmpty(td.InnerText.Trim()))
                        .Select(td => td.InnerText.Trim())
                        .ToList())
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new TrackingNotFoundException("Html is not formatted as expected.", _httpAsString, ex);
            }
        }

        private void SetIsBusy(bool isBusy)
        {
            if (_isBusy != isBusy)
            {
                _isBusy = isBusy;
                IsBusyChanged?.Invoke(this, new IsBusyEventArgs { IsBusy = isBusy });
            }
        }
    }
}
