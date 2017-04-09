using Linehaul_Helper.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Linehaul_Helper.Views
{
    public partial class SwitchLocationPage : ContentPage
    {
        public SwitchLocationPage()
        {
            InitializeComponent();
            BindingContext = new SwitchLocationPageViewModel();
        }
    }
}
