using Linehaul_Helper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Xamarin.Forms;

namespace Linehaul_Helper.UWP.Helpers
{
    class ApplicationInfo /* : IApplicationInfo */
    {
        public ApplicationInfo()
        {
        }

        public string ApplicationName()
        {
            return Package.Current.DisplayName;
        }

        public string ApplicationVersion()
        {
            var package = Package.Current;
            var packageId = package.Id;
            var version = packageId.Version;
            return string.Format("{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);
        }
    }
}
