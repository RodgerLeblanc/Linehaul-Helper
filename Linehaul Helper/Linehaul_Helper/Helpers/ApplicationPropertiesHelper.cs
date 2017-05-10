using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Linehaul_Helper.Helpers
{
    static class ApplicationPropertiesHelper
    {
        static public T GetProperty<T>(string key, T defaultValue)
        {
            if ((Application.Current.Properties.ContainsKey(key)) && (Application.Current.Properties[key] is T))
                return (T)Application.Current.Properties[key];
            else
                return defaultValue;
        }

        static public async void SetProperty<T>(string key, T value)
        {
            Application.Current.Properties[key] = value;
            await Application.Current.SavePropertiesAsync();
        }
    }
}
