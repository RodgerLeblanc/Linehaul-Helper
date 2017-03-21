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
    class UnitDefectReportPageViewModel : INotifyPropertyChanged
    {
        int count;
        string countDisplay = "You clicked 0 times.";

        public UnitDefectReportPageViewModel()
        {
            IncreaseCountCommand = new Command(() =>
            { 
                CountDisplay = $"You clicked {++count} times";
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand IncreaseCountCommand { get; }

        public string CountDisplay
        {
            get { return countDisplay; }
            set { countDisplay = value; OnPropertyChanged(); }
        }

        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
