using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace KcmsChallengeAPP.Helpers
{
    public class SettingsPreferences
    {

        public SettingsPreferences()
        {
        }

        //Set Preference Value  
        public static void SetValue(string key, string value)
        {
            Preferences.Set(key, value);
        }

        //Get Preference Value  
        public static string GetValue(string key, string value)
        {
            return Preferences.Get(key, value);
        }

        //Remove Preference Value  
        public static void DeleteValue(string key)
        {
            Preferences.Remove(key);
        }

        //Clear all Preferences Values  
        public static void Clear()
        {
            Preferences.Clear();
        }

        //Bool Preference Values  
        public static bool IsUserLoggedIn => !string.IsNullOrWhiteSpace(Preferences.Get("Usuario",""));
        public static bool IsParceiroLoggedIn => !string.IsNullOrWhiteSpace(Preferences.Get("Parceiro", ""));
        public static bool IsLojistaLoggedIn => !string.IsNullOrWhiteSpace(Preferences.Get("Lojista", ""));
    }
}
