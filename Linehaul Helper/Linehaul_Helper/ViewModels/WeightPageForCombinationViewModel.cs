using Linehaul_Helper.Helpers;
using Linehaul_Helper.Localization;
using Linehaul_Helper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Linehaul_Helper.ViewModels
{
    public class WeightPageForCombinationViewModel : BaseViewModel
    {
        private List<PsiKgPair> _psiTableTruck;
        private Dictionary<int, int> _psiTableTruckBase = new Dictionary<int, int>()
        {
            { 0, 0 },
            { 34, 4550 }, { 38, 4950 }, { 54, 6880 },
            { 58, 7130 }, { 61, 7600 }, { 64, 7695 },
            { 70, 9000 }, { 78, 9525 }, { 85, 10433 },
            { 90, 11082 }
        };

        private List<PsiKgPair> _psiTableTrailerGeneric;
        private Dictionary<int, int> _psiTableTrailerGenericBase = new Dictionary<int, int>()
        {
            { 0, 0 },
            { 16, 2268 }, { 25, 3175 }, { 34, 4082 },
            { 43, 4990 }, { 52, 5897 }, { 61, 6804 },
            { 65, 7238 }, { 71, 7711 }, { 80, 8618 },
            { 88, 9525 }, { 95, 10433 }, { 100, 11082 }
        };

        private ImageSource _imageSource;
        private List<int> _numberOfAxles = new List<int>();

        private int _selectedIndexA;
        private int _selectedIndexB;
        private int _selectedIndexC;
        private int _selectedIndexD;

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

            string imageFile = combination.Replace(',', '_') + ".png";
            ImageSource = ImageSourceHelper.GetFromResource(imageFile);

            PsiTableTruck = DictionaryToList(_psiTableTruckBase);
            PsiTableTrailerGeneric = DictionaryToList(_psiTableTrailerGenericBase);

            SelectedIndexA = ApplicationPropertiesHelper.GetProperty(nameof(SelectedIndexA), -1);
            SelectedIndexB = ApplicationPropertiesHelper.GetProperty(nameof(SelectedIndexB), -1);
            SelectedIndexC = ApplicationPropertiesHelper.GetProperty(nameof(SelectedIndexC), -1);
            SelectedIndexD = ApplicationPropertiesHelper.GetProperty(nameof(SelectedIndexD), -1);
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

        public List<PsiKgPair> PsiTableTruck
        {
            get { return _psiTableTruck; }
            set { SetProperty(ref _psiTableTruck, value); }
        }

        public List<PsiKgPair> PsiTableTrailerGeneric
        {
            get { return _psiTableTrailerGeneric; }
            set { SetProperty(ref _psiTableTrailerGeneric, value); }
        }

        public int SelectedIndexA
        {
            get { return _selectedIndexA; }
            set
            {
                SetProperty(ref _selectedIndexA, value);
                AxleWeightA = GetAxleWeight(value, NumberOfAxles[0], PsiTableTruck);
                ApplicationPropertiesHelper.SetProperty(nameof(SelectedIndexA), value);
            }
        }

        public int SelectedIndexB
        {
            get { return _selectedIndexB; }
            set
            {
                SetProperty(ref _selectedIndexB, value);
                AxleWeightB = GetAxleWeight(value, NumberOfAxles[1], PsiTableTrailerGeneric);
                ApplicationPropertiesHelper.SetProperty(nameof(SelectedIndexB), value);
            }
        }

        public int SelectedIndexC
        {
            get { return _selectedIndexC; }
            set
            {
                SetProperty(ref _selectedIndexC, value);
                AxleWeightC = GetAxleWeight(value, NumberOfAxles[2], PsiTableTrailerGeneric);
                ApplicationPropertiesHelper.SetProperty(nameof(SelectedIndexC), value);
            }
        }

        public int SelectedIndexD
        {
            get { return _selectedIndexD; }
            set
            {
                SetProperty(ref _selectedIndexD, value);
                AxleWeightD = GetAxleWeight(value, NumberOfAxles[3], PsiTableTrailerGeneric);
                ApplicationPropertiesHelper.SetProperty(nameof(SelectedIndexD), value);
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

        private int GetAxleWeight(int psi, int numberOfAxle, List<PsiKgPair> psiTable)
        {
            if ((psi < 0) || (psi >= psiTable.Count))
                return 0;

            var psiToKilos = psiTable[psi];
            return psiToKilos.Kilos * numberOfAxle;
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

            KeyValuePair<int, int> oldPair = new KeyValuePair<int, int>(0, 0);
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

        private List<PsiKgPair> DictionaryToList(Dictionary<int, int> table)
        {
            List<PsiKgPair> list = new List<PsiKgPair>();
            
            for (int i = table.FirstOrDefault().Key; i <= table.LastOrDefault().Key; i++)
            {
                list.Add(new PsiKgPair { Psi = i, Kilos = (int)GetWeightFromPsi(i, table) });
            }

            return list;
        }
    }
}
