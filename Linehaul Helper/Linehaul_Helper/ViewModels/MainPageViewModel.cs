using Linehaul_Helper.Helpers;
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
    class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public MainPageViewModel()
        {
        }

        public ICommand TappedCommand
        {
            get
            {
                return new Command<Type>((pageType) =>
                {
                    var page = (Page)Activator.CreateInstance(pageType);
                    MessagingCenter.Send<MainPageViewModel, Page>(this, Commons.Strings.PageSelectedMessage, page);
                });
            }
        }

        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
