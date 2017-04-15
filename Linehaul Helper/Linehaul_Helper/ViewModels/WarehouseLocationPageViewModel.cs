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
            WarehouseLocations = new List<WarehouseLocation>
            {
                new WarehouseLocation
                {
                    Name = "Dicom Drummondville",
                    Description = "DRU",
                    Position = new Position(45.877198, -72.543418),
                    Address = "330 RUE ROCHELEAU, DRUMMONDVILLE, QC, J2C7S7"
                },
                new WarehouseLocation
                {
                    Name = "Dicom Trois-Rivieres",
                    Description = "TRS",
                    Position = new Position(46.345096, -72.638812),
                    Address = "3700 L-P NORMAND, TROIS-RIVIERES, QC"
                },
                new WarehouseLocation
                {
                    Name = "Dicom Quebec",
                    Description = "QUE",
                    Position = new Position(46.792657, -71.323205),
                    Address = "5150 JOHN-MOLSON, QUEBEC, QC, G1X3X4"
                },
                new WarehouseLocation
                {
                    Name = "Dicom Cote-De-Liesse",
                    Description = "CDL",
                    Position = new Position(45.463108, -73.722380),
                    Address = "10755 COTE DE LIESSE OUEST, MONTREAL, QC, H9P1A7"
                }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangedEventHandler WarehouseLocationsChanged;

        public IEnumerable<WarehouseLocation> WarehouseLocations
        {
            get { return _warehouseLocations; }
            set
            {
                _warehouseLocations = value;
                OnPropertyChanged();
                OnWarehouseLocationsChanged();
            }
        }

        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        void OnWarehouseLocationsChanged() =>
            WarehouseLocationsChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(WarehouseLocations)));
    }
}
