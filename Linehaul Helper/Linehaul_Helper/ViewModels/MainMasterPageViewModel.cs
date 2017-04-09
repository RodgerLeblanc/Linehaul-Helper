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
    public class MainMasterPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public MainMasterPageViewModel()
        {
        }

        public ICommand LoadPageCommand
        {
            get
            {
                return new Command<Type>((pageType) =>
                {
                    var page = (Page)Activator.CreateInstance(pageType);
                    MessagingCenter.Send<MainMasterPageViewModel, Page>(this, Commons.Strings.PageSelectedMessage, page);
                });
            }
        }

        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
