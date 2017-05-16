using Linehaul_Helper.Services;
using Linehaul_Helper.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Linehaul_Helper.Views
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ParcelTrackingPage : ContentPage
    {
        public ParcelTrackingPage()
        {
            InitializeComponent();
            BindingContext = new ParcelTrackingPageViewModel(new ParcelTrackingService());
        }
    }
}
