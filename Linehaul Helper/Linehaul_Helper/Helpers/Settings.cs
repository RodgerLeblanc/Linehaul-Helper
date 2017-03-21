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

    private const string SettingsKey = "settings_key";
    private static readonly string SettingsDefault = String.Empty;

    #endregion


    private static string GeneralSettings
    {
      get
      {
        return AppSettings.GetValueOrDefault<string>(SettingsKey, SettingsDefault);
      }
      set
      {
        AppSettings.AddOrUpdateValue<string>(SettingsKey, value);
      }
    }

        public static GeneralSettingsObject GetGeneralSettingsObject()
        {
            try
            {
                return JsonConvert.DeserializeObject<GeneralSettingsObject>(GeneralSettings);
            }
            catch
            {
                return new GeneralSettingsObject();
            }
        }

        public static void SaveGeneralSettingsObject(GeneralSettingsObject settings)
        {
            try
            {
                GeneralSettings = JsonConvert.SerializeObject(settings);
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Exception while SaveGeneralSettingsObject: " + ex.Message);
            }
        }
  }

    public class GeneralSettingsObject
    {
        public Type LastDetailPageType { get; set; }
        public DateTime UnitInfoListDateTime { get; set; }
    }
}