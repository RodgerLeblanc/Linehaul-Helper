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

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeightPage : ContentPage
    {
        public WeightPage()
        {
            InitializeComponent();
            BindingContext = new WeightPageViewModel();

            //string text = "";
            //if (Application.Current.Properties.ContainsKey("test"))
            //    text = Application.Current.Properties["test"] as string;
            //entryxxx.Text = text;
        }

        //private async void Entry_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    Application.Current.Properties["test"] = e.NewTextValue;
        //    await Application.Current.SavePropertiesAsync();
        //}
    }
}
