using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Linehaul_Helper.ViewModels;
using Linehaul_Helper.Helpers;

namespace Linehaul_Helper.CustomControls
{
    public partial class AxleWeight : StackLayout
    {
        private readonly string _lastPsiValueKey = "lastPsiValue";

        private Dictionary<int, int> _psiToKilos = new Dictionary<int, int>()
        {
            { 0, 0 },
            { 16, 2268 }, { 25, 3175 }, { 34, 4082 },
            { 43, 4990 }, { 52, 5897 }, { 61, 6804 },
            { 65, 7238 }, { 71, 7711 }, { 80, 8618 },
            { 88, 9525 }, { 95, 10433 }, { 100, 11082 }
        };

        public static readonly BindableProperty NumberOfAxleProperty = BindableProperty.Create(nameof(NumberOfAxle), typeof(int), typeof(AxleWeight), default(int));
        public int NumberOfAxle
        {
            get { return (int)GetValue(NumberOfAxleProperty); }
            set
            {
                SetValue(NumberOfAxleProperty, value);
                CalculateWeight();
            }
        }

        public static readonly BindableProperty PsiProperty = BindableProperty.Create(nameof(Psi), typeof(int), typeof(AxleWeight), default(int), BindingMode.TwoWay);
        public int Psi
        {
            get { return (int)GetValue(PsiProperty); }
            set
            {
                SetValue(PsiProperty, value);
                CalculateWeight();
                ApplicationPropertiesHelper.SetProperty(_lastPsiValueKey + Position, value);
            }
        }

        public static readonly BindableProperty PositionProperty = BindableProperty.Create(nameof(Position), typeof(string), typeof(AxleWeight), default(string));
        public string Position
        {
            get { return (string)GetValue(PositionProperty); }
            set
            {
                SetValue(PositionProperty, value);
            }
        }

        public static readonly BindableProperty WeightProperty = BindableProperty.Create(nameof(Weight), typeof(int), typeof(AxleWeight), default(int), BindingMode.OneWay);
        public int Weight
        {
            get { return (int)GetValue(WeightProperty); }
            private set
            {
                SetValue(WeightProperty, value);
            }
        }

        public AxleWeight()
        {
            InitializeComponent();
            Psi = ApplicationPropertiesHelper.GetProperty(_lastPsiValueKey + Position, 50);
        }

        private double GetWeightFromPsi(int psi)
        {
            if (_psiToKilos.ContainsKey(psi))
                return _psiToKilos[psi];

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

        private void CalculateWeight()
        {
            Weight = (int)(NumberOfAxle * GetWeightFromPsi(Psi));
        }
    }
}
