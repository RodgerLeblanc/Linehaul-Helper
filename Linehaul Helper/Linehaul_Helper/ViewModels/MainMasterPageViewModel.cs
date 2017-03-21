using Linehaul_Helper.Helpers;
using Linehaul_Helper.Localization;
using Linehaul_Helper.Models;
using Linehaul_Helper.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Linehaul_Helper.ViewModels
{
    class MainMasterPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public MainMasterPageViewModel()
        {
            LoadPageCommand = new Command<Type>((pageType) =>
            {
                var page = (Page)Activator.CreateInstance(pageType);
                MessagingCenter.Send<MainMasterPageViewModel, Page>(this, Commons.Strings.PageSelectedMessage, page);
            });

            //var itemsSource = new ObservableCollection<MasterPageItem>();
            //itemsSource.Add(new MasterPageItem()
            //{
            //    Title = AppResources.PlatesPageTitle,
            //    ImageSource = "plates_page_icon.png",
            //    TargetType = typeof(PlatesPage),
            //    Command = LoadPageCommand
            //});
            //itemsSource.Add(new MasterPageItem()
            //{
            //    Title = AppResources.WarehouseLocatorPageTitle,
            //    ImageSource = "locator_page_icon.png",
            //    TargetType = typeof(WarehouseLocatorPage),
            //    Command = LoadPageCommand
            //});
            //itemsSource.Add(new MasterPageItem()
            //{
            //    Title = AppResources.PlatesPageTitle,
            //    ImageSource = "message_page_icon.png",
            //    TargetType = typeof(WelcomePage),
            //    Command = LoadPageCommand
            //});
            //ItemsSource = itemsSource;

            //PlatesPageCommand = new Command(() =>
            //{
            //    MessagingCenter.Send<MainMasterPageViewModel, Page>(this, Commons.Strings.PageSelectedMessage, new PlatesPage());
            //});

            //WarehouseLocatorPageCommand = new Command(() =>
            //{
            //    MessagingCenter.Send<MainMasterPageViewModel, Page>(this, Commons.Strings.PageSelectedMessage, new WarehouseLocatorPage());
            //});

            //UnitDefectReportPageCommand = new Command(() =>
            //{
            //    MessagingCenter.Send<MainMasterPageViewModel, Page>(this, Commons.Strings.PageSelectedMessage, new UnitDefectReportPage());
            //});
        }

        public ICommand LoadPageCommand { get; private set; }
        public ICommand PlatesPageCommand { get; private set; }
        public ICommand WarehouseLocatorPageCommand { get; private set; }
        public ICommand UnitDefectReportPageCommand { get; private set; }

        //public ObservableCollection<MasterPageItem> ItemsSource { get; set; }

        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
