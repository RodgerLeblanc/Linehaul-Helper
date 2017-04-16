using Linehaul_Helper.Helpers;
using Linehaul_Helper.Localization;
using Linehaul_Helper.Models;
using Linehaul_Helper.Services;
using Linehaul_Helper.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Linehaul_Helper.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlatesPage : ContentPage
    {
        public PlatesPage()
        {
            InitializeComponent();

            BindingContext = new PlatesPageViewModel(new PlatesDatabaseService());

            //var assembly = typeof(PlatesPage).GetTypeInfo().Assembly;
            //foreach (var res in assembly.GetManifestResourceNames())
            //{
            //    Debug.WriteLine("found resource: " + res);
            //}
        }
    }
}
