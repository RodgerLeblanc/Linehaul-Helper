using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Linehaul_Helper.Interfaces;
using Android.Content.PM;

namespace Linehaul_Helper.Droid.Helpers
{
    class ApplicationInfo : IApplicationInfo
    {
        public ApplicationInfo()
        {
        }

        public string ApplicationName()
        {
            var applicationInfo = Application.Context.ApplicationInfo;
            var localizationStringId = applicationInfo.LabelRes;
            return localizationStringId == 0 ? applicationInfo.NonLocalizedLabel.ToString() : Application.Context.GetString(localizationStringId);
        }

        public string ApplicationVersion()
        {
            return Application.Context.PackageManager.GetPackageInfo(
              Application.Context.PackageName, PackageInfoFlags.MetaData).VersionName;
        }
    }
}