using Linehaul_Helper.Helpers;
using Linehaul_Helper.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Linehaul_Helper.ViewModels
{
    class PlateNumberPageViewModel : INotifyPropertyChanged
    {
        private ICommand _doneCommand;
        private ICommand _reportErrorCommand;
        private PlateNumberPageModel _plateNumberPageModel;
        private int _fontSizeLarge;
        private int _fontSizeXLarge;

        public event PropertyChangedEventHandler PropertyChanged;

        public PlateNumberPageViewModel(UnitInfo unitInfo)
        {
            PlateNumberPageModel = new PlateNumberPageModel
            {
                UnitNumber = unitInfo.UnitNumber,
                PlateNumber = unitInfo.PlateNumber
            };

            DoneCommand = new Command(async () =>
            {
                await Commons.DetailNavigationPopAsync();
            });

            ReportErrorCommand = new Command(() =>
            {
                throw new NotImplementedException();
            });

            var size = Plugin.XamJam.Screen.CrossScreen.Current.Size;
            FontSizeLarge = (int)(Math.Min(size.Width, size.Height) * 0.1);
            FontSizeXLarge = (int)(Math.Min(size.Width, size.Height) * 0.15);
        }

        public ICommand DoneCommand
        {
            get { return _doneCommand; }
            private set { _doneCommand = value; OnPropertyChanged(); }
        }

        public ICommand ReportErrorCommand
        {
            get { return _reportErrorCommand; }
            private set { _reportErrorCommand = value; OnPropertyChanged(); }
        }

        public PlateNumberPageModel PlateNumberPageModel
        {
            get { return _plateNumberPageModel; }
            private set { _plateNumberPageModel = value; OnPropertyChanged(); }
        }

        public int FontSizeLarge
        {
            get { return _fontSizeLarge; }
            private set { _fontSizeLarge = value; OnPropertyChanged(); }
        }

        public int FontSizeXLarge
        {
            get { return _fontSizeXLarge; }
            private set { _fontSizeXLarge = value; OnPropertyChanged(); }
        }

        void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
