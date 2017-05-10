using Linehaul_Helper.Helpers;
using Linehaul_Helper.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Linehaul_Helper.ViewModels
{
    class WeightPageForCombinationViewModel : BaseViewModel
    {
        private readonly string _lastPsiValueKey = "lastPsiValue";

        private Dictionary<int, int> _psiTableTruck = new Dictionary<int, int>()
        {
            { 0, 0 },
            { 6, 2268 }, { 15, 3175 }, {234, 4082 },
            { 33, 4990 }, { 42, 5897 }, { 51, 6804 },
            { 55, 7238 }, { 61, 7711 }, { 70, 8618 },
            { 78, 9525 }, { 85, 10433 }, { 90, 11082 }
        };

        private Dictionary<int, int> _psiTableTrailerGeneric = new Dictionary<int, int>()
        {
            { 0, 0 },
            { 16, 2268 }, { 25, 3175 }, { 34, 4082 },
            { 43, 4990 }, { 52, 5897 }, { 61, 6804 },
            { 65, 7238 }, { 71, 7711 }, { 80, 8618 },
            { 88, 9525 }, { 95, 10433 }, { 100, 11082 }
        };

        private ImageSource _imageSource;
        private List<int> _numberOfAxles = new List<int>();

        private int _psiA;
        private int _psiB;
        private int _psiC;
        private int _psiD;

        private int _axleWeightA;
        private int _axleWeightB;
        private int _axleWeightC;
        private int _axleWeightD;

        private int _totalWeightAB;
        private int _totalWeightCD;
        private int _totalWeightABCD;

        private string _weightMessage;


        public WeightPageForCombinationViewModel(string combination)
        {
            NumberOfAxles = combination.Split(',')
                                    .Select((a) => Convert.ToInt32(a))
                                    .ToList();

            switch(combination)
            {
                case "2,2,0,0":
                    {
                        ImageSource = ImageSourceHelper.GetFromResource("combo_truck_2axle.png");
                        break;
                    }
                case "2,3,0,0":
                    {
                        ImageSource = ImageSourceHelper.GetFromResource("combo_truck_3axle.png");
                        break;
                    }
                case "2,2,2,2":
                    {
                        ImageSource = ImageSourceHelper.GetFromResource("combo_truck_2axle_2axle.png");
                        break;
                    }
                case "2,2,2,3":
                    {
                        ImageSource = ImageSourceHelper.GetFromResource("combo_truck_2axle_3axle.png");
                        break;
                    }
                case "2,3,2,2":
                    {
                        ImageSource = ImageSourceHelper.GetFromResource("combo_truck_3axle_2axle.png");
                        break;
                    }
                case "2,3,2,3":
                    {
                        ImageSource = ImageSourceHelper.GetFromResource("combo_truck_3axle_3axle.png");
                        break;
                    }
                default:
                    {
                        ImageSource = ImageSourceHelper.GetFromResource("combo_truck_2axle.png");
                        NumberOfAxles = new List<int>() { 2, 2, 0, 0 };
                        break;
                    }
            }

            PsiA = ApplicationPropertiesHelper.GetProperty(_lastPsiValueKey + "A", 60);
            PsiB = ApplicationPropertiesHelper.GetProperty(_lastPsiValueKey + "B", 50);
            PsiC = ApplicationPropertiesHelper.GetProperty(_lastPsiValueKey + "C", 50);
            PsiD = ApplicationPropertiesHelper.GetProperty(_lastPsiValueKey + "D", 50);
        }

        public ImageSource ImageSource
        {
            get { return _imageSource; }
            set { SetProperty(ref _imageSource, value); }
        }

        public List<int> NumberOfAxles
        {
            get { return _numberOfAxles; }
            set { SetProperty(ref _numberOfAxles, value); }
        }

        public int PsiA
        {
            get { return _psiA; }
            set
            {
                SetProperty(ref _psiA, value);
                AxleWeightA = CalculateWeight(NumberOfAxles[0], value, _psiTableTruck);
                ApplicationPropertiesHelper.SetProperty(_lastPsiValueKey + "A", value);
            }
        }

        public int PsiB
        {
            get { return _psiB; }
            set
            {
                SetProperty(ref _psiB, value);
                AxleWeightB = CalculateWeight(NumberOfAxles[1], value, _psiTableTrailerGeneric);
                ApplicationPropertiesHelper.SetProperty(_lastPsiValueKey + "B", value);
            }
        }

        public int PsiC
        {
            get { return _psiC; }
            set
            {
                SetProperty(ref _psiC, value);
                AxleWeightC = CalculateWeight(NumberOfAxles[2], value, _psiTableTrailerGeneric);
                ApplicationPropertiesHelper.SetProperty(_lastPsiValueKey + "C", value);
            }
        }

        public int PsiD
        {
            get { return _psiD; }
            set
            {
                SetProperty(ref _psiD, value);
                AxleWeightD = CalculateWeight(NumberOfAxles[3], value, _psiTableTrailerGeneric);
                ApplicationPropertiesHelper.SetProperty(_lastPsiValueKey + "D", value);
            }
        }

        public int AxleWeightA
        {
            get { return _axleWeightA; }
            set { SetProperty(ref _axleWeightA, value); CalculateWeights(); }
        }

        public int AxleWeightB
        {
            get { return _axleWeightB; }
            set { SetProperty(ref _axleWeightB, value); CalculateWeights(); }
        }

        public int AxleWeightC
        {
            get { return _axleWeightC; }
            set { SetProperty(ref _axleWeightC, value); CalculateWeights(); }
        }

        public int AxleWeightD
        {
            get { return _axleWeightD; }
            set { SetProperty(ref _axleWeightD, value); CalculateWeights(); }
        }

        public int TotalWeightAB
        {
            get { return _totalWeightAB; }
            set { SetProperty(ref _totalWeightAB, value); }
        }

        public int TotalWeightCD
        {
            get { return _totalWeightCD; }
            set { SetProperty(ref _totalWeightCD, value); }
        }

        public int TotalWeightABCD
        {
            get { return _totalWeightABCD; }
            set { SetProperty(ref _totalWeightABCD, value); }
        }

        public string WeightMessage
        {
            get { return _weightMessage; }
            set { SetProperty(ref _weightMessage, value); }
        }

        private void CalculateWeights()
        {
            TotalWeightAB = AxleWeightA + AxleWeightB;
            TotalWeightCD = AxleWeightC + AxleWeightD;
            TotalWeightABCD = 5000 + TotalWeightAB + TotalWeightCD;

            WriteWeightMessage();
        }

        private void WriteWeightMessage()
        {
            //    int twoAxleWeightLimit = 18000;
            //    int threeAxleWeightLimit = 26000;

            //    StringBuilder sb = new StringBuilder();
            //    if (AxleWeightA > twoAxleWeightLimit)
            //    {
            //        sb.AppendLine(String.Format(AppResources.WeightWarningAxleOverweight, "A"));
            //    }
        }

        private double GetWeightFromPsi(int psi, Dictionary<int, int> psiTable)
        {
            if (psiTable.ContainsKey(psi))
                return psiTable[psi];

            KeyValuePair<int, int> oldPair = new KeyValuePair<int, int>(16, 2268);
            foreach (KeyValuePair<int, int> pair in psiTable)
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

        private int CalculateWeight(int numberOfAxle, int psi, Dictionary<int, int> psiTable)
        {
            return (int)(numberOfAxle * GetWeightFromPsi(psi, psiTable));
        }
    }
}
