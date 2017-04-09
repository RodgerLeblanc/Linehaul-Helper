using Linehaul_Helper.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Linehaul_Helper.ViewModels
{
    class SwitchLocationPageViewModel : INotifyPropertyChanged
    {
        private IEnumerable<SwitchLocation> _switchLocations;

        public SwitchLocationPageViewModel()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable<SwitchLocation> SwitchLocations
        {
            get { return _switchLocations; }
            set { _switchLocations = value; OnPropertyChanged(); }
        }

        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
