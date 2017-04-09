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
    class WarehouseLocationPageViewModel : INotifyPropertyChanged
    {
        private IEnumerable<WarehouseLocation> _warehouseLocations;

        public WarehouseLocationPageViewModel()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable<WarehouseLocation> WarehouseLocations
        {
            get { return _warehouseLocations; }
            set { _warehouseLocations = value; OnPropertyChanged(); }
        }

        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
