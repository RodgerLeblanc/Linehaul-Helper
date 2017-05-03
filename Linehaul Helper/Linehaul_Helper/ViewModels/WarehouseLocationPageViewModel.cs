using Linehaul_Helper.Interfaces;
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
        private IWarehouseDatabaseService _dbService;
        private IList<WarehouseLocation> _warehouseLocations;

        public WarehouseLocationPageViewModel(IWarehouseDatabaseService dbService)
        {
            _dbService = dbService ?? throw new ArgumentNullException();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangedEventHandler WarehouseLocationsChanged;

        public IList<WarehouseLocation> WarehouseLocations
        {
            get { return _warehouseLocations; }
            set
            {
                _warehouseLocations = value;
                OnPropertyChanged();
                OnWarehouseLocationsChanged();
            }
        }

        public async void GetWarehouseLocations()
        {
            WarehouseLocations = await _dbService.GetWarehouseLocations();
        }

        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        void OnWarehouseLocationsChanged() =>
            WarehouseLocationsChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(WarehouseLocations)));
    }
}
