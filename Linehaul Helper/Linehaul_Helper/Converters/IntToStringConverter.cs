using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Linehaul_Helper.Converters
{
    class IntToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var number = (int)value;
            return number < 10000 ? value.ToString() : value.ToString().Substring(0, 4);
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int number;
            var worked = Int32.TryParse(value.ToString(), out number);
            while(number > 10000)
                number = number / 10;
            return worked ? number : 0;
        }
    }
}
