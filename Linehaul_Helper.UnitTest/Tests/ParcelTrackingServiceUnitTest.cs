using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Linehaul_Helper.Services;
using Linehaul_Helper.CustomEventArgs;
using Linehaul_Helper.Exceptions;
using Linehaul_Helper.Interfaces;
using System.Threading.Tasks;
using Linehaul_Helper.Models;
using System.Net.Http;
using HtmlAgilityPack;

namespace Linehaul_Helper.UnitTest.Tests
{
    /// <summary>
    /// Summary description for ParcelTrackingServiceUnitTest
    /// </summary>
    [TestClass]
    public class ParcelTrackingServiceUnitTest
    {
        [TestMethod]
        public void TrackShouldSetIsBusyToTrueThenFalse()
        {
            // Assign
            var isBusyChangedToTrue = false;
            var isBusyChangedToFalse = false;

            var pts = new ParcelTrackingService();
            pts.IsBusyChanged += (sender, args) =>
            {
                var ibea = args as IsBusyEventArgs;
                if (ibea.IsBusy)
                    isBusyChangedToTrue = true;
                else
                    isBusyChangedToFalse = true;
            };

            // Act
            var trackingNumber = "1234567";
            var result = pts.Track(trackingNumber).Result;

            // Assert
            Assert.IsTrue(isBusyChangedToTrue);
            Assert.IsTrue(isBusyChangedToFalse);
        }

        [TestMethod]
        public void TrackShouldReturnAParcelTrackingModel()
        {
            var pts = new DummyParcelTrackingService();
            var trackingNumber = "1234567";
            var result = pts.Track(trackingNumber).Result;
            Assert.IsInstanceOfType(result, typeof(ParcelTrackingModel));
        }

        [TestMethod]
        [ExpectedException(typeof(TrackingNotFoundException))]
        public void TrackShouldThrowAnExceptionWhenNotConnected()
        {
            var pts = new NoInternetParcelTrackingService();
            var trackingNumber = "1234567";
            var result = pts.Track(trackingNumber).Result;
        }

        [TestMethod]
        public void DicomWebsiteShouldReturnHtmlFormattedForScrapHtmlForTrackingInfosMethod()
        {
            var pts = new ParcelTrackingService();
            PrivateObject obj = new PrivateObject(pts);
            var trackingNumber = "1234567";

            var task = obj.Invoke("GetResponseFromServer", trackingNumber) as Task<HttpResponseMessage>;
            var response = task.Result;

            var httpAsString = response.Content.ReadAsStringAsync().Result;
            var doc = new HtmlDocument();
            doc.LoadHtml(httpAsString);

            var trackingInfos = obj.Invoke("ScrapHtmlForTrackingInfos", doc);

            Assert.IsInstanceOfType(trackingInfos, typeof(List<string>));
        }

        private class DummyParcelTrackingService : IParcelTrackingService
        {
            public event EventHandler IsBusyChanged;

            public Task<ParcelTrackingModel> Track(string trackingNumber)
            {
                return Task.FromResult<ParcelTrackingModel>(new ParcelTrackingModel());
            }
        }

        private class NoInternetParcelTrackingService : IParcelTrackingService
        {
            public event EventHandler IsBusyChanged;

            public Task<ParcelTrackingModel> Track(string trackingNumber)
            {
                throw new TrackingNotFoundException("No internet");
            }
        }
    }
}
