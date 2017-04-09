using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Linehaul_Helper.Interfaces;

namespace Linehaul_Helper.iOS.Helpers
{
    class ApplicationInfo : IApplicationInfo
    {
        readonly NSString _buildKey;
        readonly NSString _versionKey;
        readonly NSString _displayName;

        public ApplicationInfo()
        {
            _buildKey = new NSString("CFBundleVersion");
            _versionKey = new NSString("CFBundleShortVersionString");
            _displayName = new NSString("CFBundleDisplayName");
        }

        public string ApplicationName()
        {
            return NSBundle.MainBundle.InfoDictionary.ValueForKey(_displayName).ToString();
        }

        public string ApplicationVersion()
        {
            var build = NSBundle.MainBundle.InfoDictionary.ValueForKey(_buildKey);
            var version = NSBundle.MainBundle.InfoDictionary.ValueForKey(_versionKey);
            return string.Format("{0}.{1}", version, build);
        }
    }
}