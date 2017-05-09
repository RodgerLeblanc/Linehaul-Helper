using Linehaul_Helper.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Linehaul_Helper.ViewModels
{
    class WeightPageForSingleCombinationViewModel : BaseViewModel
    {
        private ImageSource _imageSource;
        private List<int> _numberOfAxles = new List<int>();

        public WeightPageForSingleCombinationViewModel(string combination)
        {
            switch(combination)
            {
                case "2":
                    {
                        ImageSource = ImageSourceHelper.GetFromResource("combo_truck_2axle.png");
                        NumberOfAxles = new List<int>() { 2 };
                        break;
                    }
                case "3":
                    {
                        ImageSource = ImageSourceHelper.GetFromResource("combo_truck_3axle.png");
                        NumberOfAxles = new List<int>() { 3 };
                        break;
                    }
                case "22":
                    {
                        ImageSource = ImageSourceHelper.GetFromResource("combo_truck_2axle_2axle.png");
                        NumberOfAxles = new List<int>() { 2, 2 };
                        break;
                    }
                case "23":
                    {
                        ImageSource = ImageSourceHelper.GetFromResource("combo_truck_2axle_3axle.png");
                        NumberOfAxles = new List<int>() { 2, 3 };
                        break;
                    }
                case "32":
                    {
                        ImageSource = ImageSourceHelper.GetFromResource("combo_truck_3axle_2axle.png");
                        NumberOfAxles = new List<int>() { 3, 2 };
                        break;
                    }
                case "33":
                    {
                        ImageSource = ImageSourceHelper.GetFromResource("combo_truck_3axle_3axle.png");
                        NumberOfAxles = new List<int>() { 3, 3 };
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

    }
}
