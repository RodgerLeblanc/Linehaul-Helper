using Linehaul_Helper.Helpers;
using Linehaul_Helper.Models;
using Linehaul_Helper.ViewModels;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Linehaul_Helper.Views
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlateNumberPage : ContentPage
	{
		public PlateNumberPage(UnitInfo unitInfo)
		{
            InitializeComponent();

			BindingContext = new PlateNumberPageViewModel(unitInfo);
		}
	}
}
