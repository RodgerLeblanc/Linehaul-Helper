// Helpers/Settings.cs
using Linehaul_Helper.Models;
using Newtonsoft.Json;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace Linehaul_Helper.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters  
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string UnitInfoListDateTimeKey = "unitInfoListDateTime_key";
        private static readonly DateTime UnitInfoListDateTimeDefault = new DateTime(1970, 1, 1);
        private const string LastTrackingNumberKey = "lastTrackingNumber_key";
        private static readonly string LastTrackingNumberDefault = "";

        #endregion

        public static DateTime UnitInfoListDateTime
        {
            get
            {
                return AppSettings.GetValueOrDefault<DateTime>(UnitInfoListDateTimeKey, UnitInfoListDateTimeDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue<DateTime>(UnitInfoListDateTimeKey, value);
            }
        }

        public static string LastTrackingNumber
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(LastTrackingNumberKey, LastTrackingNumberDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>(LastTrackingNumberKey, value);
            }
        }

  }

    public class GeneralSettingsObject
    {
        public DateTime UnitInfoListDateTime { get; set; }
    }
}