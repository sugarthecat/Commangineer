using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Commangineer
{
    internal static class Settings
    {
        private static bool musicEnabled = true;
        private static string settingsFile = Assembly.GetExecutingAssembly().Location + "/../Content/settings.json";
        public static void LoadSettings()
        {
            
            if (File.Exists(settingsFile))
            {
                try
                {
                    string text = String.Join("", File.ReadAllLines(settingsFile).Select(x => x.Trim()).ToArray());
                    JsonObject settingsJson = (JsonObject)JsonObject.Parse(text);
                    musicEnabled = (bool)settingsJson["musicEnabled"];
                }
                catch(NullReferenceException)
                {
                    Log.LogText("error reading settings file");
                }
            }
            else
            {
                Log.LogText("No settings file found. Defaulted.");
            }
        }
        public static void SaveSettings()
        {
            Object settingsObject = new {musicEnabled = musicEnabled};
            
            File.WriteAllText(settingsFile, JsonSerializer.Serialize<Object>(settingsObject));
        }
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