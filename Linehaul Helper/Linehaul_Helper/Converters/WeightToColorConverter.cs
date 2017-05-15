using Linehaul_Helper.CustomControls;
using Linehaul_Helper.Localization;
using Linehaul_Helper.ViewModels;
using Linehaul_Helper.Views;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Linehaul_Helper.Converters
{
    class WeightToColorConverter : IValueConverter
    {
        public Color LegalColor { get; set; } = Color.White;
        public Color IllegalColor { get; set; } = Color.Red;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return IllegalColor;

            int weight = (int)value;
            int maxLegalWeight = GetMaxLegalWeight(parameter);
            return (weight <= maxLegalWeight) ? LegalColor : IllegalColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private int GetMaxLegalWeight(object parameter)
        {
            return parameter is AxleWeight ? ((AxleWeight)parameter).MaxLegalWeight : ((WeightPageForCombinationViewModel)((WeightPageForCombination)parameter).BindingContext).MaxLegalWeight;
        }
    }
}
