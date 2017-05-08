using Linehaul_Helper.Helpers;
using Linehaul_Helper.Views;
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
    class WeightPageViewModel : INotifyPropertyChanged
    {
        private readonly string _lastPsiValueKey = "lastPsiValue";
        private int _psi;
        private int _weightTwoAxle;
        private int _weightThreeAxle;
        private ICommand _truckSelectedCommand;

        private Dictionary<int, int> _psiToKilos = new Dictionary<int, int>()
        {
            { 16, 2268 }, { 25, 3175 }, { 34, 4082 },
            { 43, 4990 }, { 52, 5897 }, { 61, 6804 },
            { 65, 7238 }, { 71, 7711 }, { 80, 8618 },
            { 100, 10633 }
        };

        public WeightPageViewModel()
        {
            if (Application.Current.Properties.ContainsKey(_lastPsiValueKey))
                Psi = (int)Application.Current.Properties[_lastPsiValueKey];
            else
                Psi = 50;

            TruckSelectedCommand = new Command<string>((c) => 
            {
                var page = new WeightPageForCombination(c);
                MessagingCenter.Send<WeightPageViewModel, Page>(this, Commons.Strings.PageSelectedMessage, page);
            });
        }

        public int Psi
        {
            get
            {
                return _psi;
            }
            set
            {
                _psi = value;
                OnPropertyChanged();

                WeightTwoAxle = 2 * GetWeightFromPsi(_psi);
                WeightThreeAxle = 3 * GetWeightFromPsi(_psi);

                Application.Current.Properties[_lastPsiValueKey] = _psi;
                Application.Current.SavePropertiesAsync();
            }
        }

        public int WeightTwoAxle
        {
            get
            {
                return _weightTwoAxle;
            }
            private set
            {
                _weightTwoAxle = value;
                OnPropertyChanged();
            }
        }

        public int WeightThreeAxle
        {
            get
            {
                return _weightThreeAxle;
            }
            private set
            {
                _weightThreeAxle = value;
                OnPropertyChanged();
            }
        }

        public ICommand TruckSelectedCommand {
            get
            {
                return _truckSelectedCommand;
            }
            private set
            {
                _truckSelectedCommand = value;
                OnPropertyChanged();
            }
        }

        private int GetWeightFromPsi(int psi)
        {
            if (_psiToKilos.ContainsKey(_psi))
                return _psiToKilos[_psi];

            KeyValuePair<int, int> oldPair = new KeyValuePair<int, int>(16, 2268);
            foreach (KeyValuePair<int, int> pair in _psiToKilos)
            {
                if (pair.Key > psi)
                {
                    int psiDifference = oldPair.Key - pair.Key;
                    int weightDifference = oldPair.Value - pair.Value;
                    double kilosPerPsi = weightDifference / psiDifference;
                    return (int)((psi - oldPair.Key) * kilosPerPsi) + oldPair.Value;
                }
                else
                {
                    oldPair = pair;
                }
            }
            return 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
