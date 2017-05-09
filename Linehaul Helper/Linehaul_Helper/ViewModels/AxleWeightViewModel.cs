using Linehaul_Helper.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Linehaul_Helper.ViewModels
{
    public class AxleWeightViewModel : BaseViewModel
    {
        private readonly string _lastPsiValueKey = "lastPsiValue";

        private string _axlePosition;
        private int _numberOfAxle;
        private int _psi;
        private double _weightPerAxle;
        private int _totalWeight;

        private Dictionary<int, int> _psiToKilos = new Dictionary<int, int>()
        {
            { 0, 0 },
            { 16, 2268 }, { 25, 3175 }, { 34, 4082 },
            { 43, 4990 }, { 52, 5897 }, { 61, 6804 },
            { 65, 7238 }, { 71, 7711 }, { 80, 8618 },
            { 88, 9525 }, { 95, 10433 }, { 100, 11082 }
        };

        public AxleWeightViewModel(string axlePosition, int numberOfAxle)
        {
            AxlePosition = axlePosition;
            NumberOfAxle = numberOfAxle;

            Psi = ApplicationPropertiesHelper.GetProperty(_lastPsiValueKey + _axlePosition, 50);
        }

        public int Psi
        {
            get { return _psi; }
            set
            {
                SetProperty(ref _psi, value);
                CalculateWeights();

                ApplicationPropertiesHelper.SetProperty(_lastPsiValueKey + _axlePosition, _psi);
            }
        }

        public string AxlePosition
        {
            get { return _axlePosition; }
            private set { SetProperty(ref _axlePosition, value); }
        }

        public int NumberOfAxle
        {
            get { return _numberOfAxle; }
            set
            {
                SetProperty(ref _numberOfAxle, value);
                CalculateWeights();
            }
        }

        public double WeightPerAxle
        {
            get { return _weightPerAxle; }
            private set { SetProperty(ref _weightPerAxle, value); }
        }

        public int TotalWeight
        {
            get { return _totalWeight; }
            private set { SetProperty(ref _totalWeight, value); }
        }

        private double GetWeightFromPsi(int psi)
        {
            if (_psiToKilos.ContainsKey(_psi))
                return _psiToKilos[_psi];

            KeyValuePair<int, int> oldPair = new KeyValuePair<int, int>(16, 2268);
            foreach (KeyValuePair<int, int> pair in _psiToKilos)
            {
                if (pair.Key > psi)
                {
                    return CalculateWeightFromPsi(oldPair, pair, psi);
                }
                else
                {
                    oldPair = pair;
                }
            }
            return 0;
        }

        private double CalculateWeightFromPsi(KeyValuePair<int, int> oldPair, KeyValuePair<int, int> pair, int psi)
        {
            try
            {
                int psiDifference = oldPair.Key - pair.Key;
                int weightDifference = oldPair.Value - pair.Value;
                double kilosPerPsi = weightDifference / psiDifference;
                return ((psi - oldPair.Key) * kilosPerPsi) + oldPair.Value;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private void CalculateWeights()
        {
            WeightPerAxle = GetWeightFromPsi(Psi);
            TotalWeight = (int)(NumberOfAxle * WeightPerAxle);
        }
    }
}
