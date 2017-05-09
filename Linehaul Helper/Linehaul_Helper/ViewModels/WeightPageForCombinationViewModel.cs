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
        private ImageSource _imageSource;
        private List<int> _numberOfAxles = new List<int>();

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
                case "2,0":
                    {
                        ImageSource = ImageSourceHelper.GetFromResource("combo_truck_2axle.png");
                        break;
                    }
                case "3,0":
                    {
                        ImageSource = ImageSourceHelper.GetFromResource("combo_truck_3axle.png");
                        break;
                    }
                case "2,2":
                    {
                        ImageSource = ImageSourceHelper.GetFromResource("combo_truck_2axle_2axle.png");
                        break;
                    }
                case "2,3":
                    {
                        ImageSource = ImageSourceHelper.GetFromResource("combo_truck_2axle_3axle.png");
                        break;
                    }
                case "3,2":
                    {
                        ImageSource = ImageSourceHelper.GetFromResource("combo_truck_3axle_2axle.png");
                        break;
                    }
                case "3,3":
                    {
                        ImageSource = ImageSourceHelper.GetFromResource("combo_truck_3axle_3axle.png");
                        break;
                    }
                default:
                    {
                        ImageSource = ImageSourceHelper.GetFromResource("combo_truck_2axle.png");
                        NumberOfAxles = new List<int>() { 2 };
                        break;
                    }
            }
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
    }
}
