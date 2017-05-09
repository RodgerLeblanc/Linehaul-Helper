using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Linehaul_Helper.ViewModels;

namespace Linehaul_Helper.CustomControls
{
    public partial class AxleWeight : StackLayout
    {
        private AxleWeightViewModel _axleWeightViewModel = null;

        public static readonly BindableProperty NumberOfAxleProperty = BindableProperty.Create(nameof(NumberOfAxle), typeof(int), typeof(AxleWeight), default(int));
        public int NumberOfAxle
        {
            get { return (int)GetValue(NumberOfAxleProperty); }
            set
            {
                SetValue(NumberOfAxleProperty, value);
                if (_axleWeightViewModel != null)
                    _axleWeightViewModel.NumberOfAxle = value;
            }
        }

        public AxleWeight(string axlePosition)
        {
            InitializeComponent();

            BindingContext = _axleWeightViewModel = new AxleWeightViewModel(axlePosition, NumberOfAxle);
        }
    }
}
