using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Linehaul_Helper.ViewModels;
using Linehaul_Helper.Interfaces;
using Linehaul_Helper.Models;
using System.Threading.Tasks;
using System.Threading;
using Xamarin.Forms;
using Linehaul_Helper.Helpers;

namespace Linehaul_Helper.UnitTest.Tests
{
    [TestClass]
    public class ParcelTrackingViewModelUnitTest
    {
        //[TestMethod]
        //public void TestMethod1()
        //{
        //}

        private class DummyParcelTrackingService : IParcelTrackingService
        {
            public event EventHandler IsBusyChanged;

            public Task<ParcelTrackingModel> Track(string trackingNumber)
            {
                return Task.FromResult<ParcelTrackingModel>(new ParcelTrackingModel());
            }
        }
    }
}
