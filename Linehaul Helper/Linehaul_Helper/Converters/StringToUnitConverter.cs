using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Linehaul_Helper.Converters
{
    class StringToUnitConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var safeValue = value as string ?? "";
            var reg = Regex.Replace(safeValue, @"/[^0-9]/g", "");

            return reg.Substring(0, Math.Min(reg.Length, 4));
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value as string ?? "";
        }
    }
}
