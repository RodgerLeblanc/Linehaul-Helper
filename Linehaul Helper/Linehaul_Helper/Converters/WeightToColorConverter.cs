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
        public Color LegalColor { get; set; } = (Color)Label.TextColorProperty.DefaultValue;
        public Color IllegalColor { get; set; } = Color.Red;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return IllegalColor;

            int weight = (int)value;
            int maxLegalWeight = (int)parameter;
            return (weight <= maxLegalWeight) ? LegalColor : IllegalColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
