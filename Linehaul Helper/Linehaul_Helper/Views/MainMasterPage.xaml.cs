using Linehaul_Helper.Helpers;
using Linehaul_Helper.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Linehaul_Helper.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMasterPage : ContentPage
    {

        public MainMasterPage()
        {
            InitializeComponent();
            BindingContext = new MainMasterPageViewModel();
        }
    }
}
