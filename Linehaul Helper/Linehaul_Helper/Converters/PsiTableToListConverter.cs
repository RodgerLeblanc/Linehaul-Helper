using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Linehaul_Helper.Converters
{
    public class PsiTableToListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return new List<string>();

            Dictionary<int, int> psiTable = (Dictionary<int, int>)value;
            List<string> list = new List<string>();
            foreach(KeyValuePair<int, int> pair in psiTable)
            {
                list.Add($"{pair.Key} ({pair.Value}kg)");
            }
            return list;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
