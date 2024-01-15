using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Commangineer
{
    /// <summary>
    /// Handles current user settings
    /// </summary>
    public static class Settings
    {
        private static bool musicEnabled = true;
        private static int levelOn = 1;
        private static string settingsFile = Assembly.GetExecutingAssembly().Location + "/../Content/settings.json";

        /// <summary>
        /// Loads in settings from a JSON file in storage
        /// </summary>
        public static void LoadSettings()
        {
            if (File.Exists(settingsFile))
            {
                try
                {
                    string text = String.Join("", File.ReadAllLines(settingsFile).Select(x => x.Trim()).ToArray());
                    JsonObject settingsJson = (JsonObject)JsonObject.Parse(text);
                    //musicEnabled = (bool)settingsJson["musicEnabled"];
                    //levelOn = (int)settingsJson["levelOn"];
                }
                catch (NullReferenceException)
                {
                    Log.LogText("error reading settings file");
                }
            }
            else
            {
                Log.LogText("No settings file found. Defaulted.");
            }
        }

        /// <summary>
        /// Saves settings to a JSON file in storage
        /// </summary>
        public static void SaveSettings()
        {
            Object settingsObject = new { musicEnabled = musicEnabled, levelOn = levelOn };

            File.WriteAllText(settingsFile, JsonSerializer.Serialize<Object>(settingsObject));
        }

        /// <summary>
        /// If the music is enabled
        /// </summary>
        public static bool MusicEnabled
        {
            get
            {
                return musicEnabled;
            }
            set
            {
                musicEnabled = value;
                GameMusic.Update();
            }
        }
    }
}