using System;
using System.IO;
using System.Linq;
using System.Net.Http;
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
        private static bool musicEnabled = false;
        private static int levelOn = 1;
        private static string username = "Recruit";
        private static string settingsFile = Assembly.GetExecutingAssembly().Location + "/../Content/settings.json";
        private static int userID = -1;
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
                    levelOn = (int)settingsJson["levelOn"];
                    userID = (int)settingsJson["userID"];
                    LoadRuntimeSettings();
                }
                catch (NullReferenceException)
                {
                    getNewUserId();
                    Log.LogText("error reading settings file");
                }
            }
            else
            {
                musicEnabled = true;
                getNewUserId();
                Log.LogText("No settings file found. Defaulted.");
            }
        }

        private static HttpClient sharedClient = new()
        {
            BaseAddress = new Uri("http://ktprog.com/commangineer/"),
        };

        /// <summary>
        /// Gets a new User ID 
        /// </summary>
        private static async void getNewUserId()
        {
            using HttpResponseMessage response = await sharedClient.GetAsync("getUserId.php");


            string stringResponse = await response.Content.ReadAsStringAsync();
            JsonNode jsonResponse = JsonObject.Parse(stringResponse);
            JsonObject objectResponse = jsonResponse.AsObject();
            if (objectResponse.ContainsKey("newID"))
            {
                userID = (int)objectResponse["newID"];
            }
            Log.LogText($"{jsonResponse}\n");
        }

        /// <summary>
        /// loads runtime settings
        /// </summary>
        private static async void LoadRuntimeSettings()
        {
            try
            {
                using HttpResponseMessage response = await sharedClient.GetAsync("getSettings.php?userid=" + userID);


                string stringResponse = await response.Content.ReadAsStringAsync();
                JsonNode jsonResponse = JsonObject.Parse(stringResponse);
                JsonObject objectResponse = jsonResponse.AsObject();
                Log.LogText(stringResponse);
                if (objectResponse.ContainsKey("audio"))
                {
                    musicEnabled = (int)objectResponse["audio"] == 0;
                    GameMusic.Update();
                }
                if (objectResponse.ContainsKey("name"))
                {
                    username = (string)objectResponse["name"];
                    while (username != username.Replace("1 ", " "))
                    {
                        username = username.Replace("  ", " ");
                    }
                    Log.LogText("Username: " + username);
                }
            }
            catch
            {
                //no
            }
        }

        /// <summary>
        /// Saves settings to a JSON file in storage
        /// </summary>
        public static void SaveSettings()
        {
            Object settingsObject = new { userID = userID, levelOn = levelOn };
            File.WriteAllText(settingsFile, JsonSerializer.Serialize<Object>(settingsObject));

        }
        /// <summary>
        /// saves runtime settings
        /// </summary>
        private static async void SaveRuntimeSettings()
        {
            try
            {
                string[] parts = username.Split(' ');
                string rank = "Recruit";
                if (parts.Length > 0)
                {
                    rank = parts[0];
                }
                string firstName = "Cobalt";
                if (parts.Length > 1)
                {
                    firstName = parts[1];
                }
                string lastName = "Hammer";
                if (parts.Length > 2)
                {
                    lastName = parts[2];
                }
                string getString = "setSettings.php?userid=" + userID + "&rank=" + rank + "&fname=" + firstName + "&lname=" + lastName + "&audio=" + (musicEnabled ? 0 : 1);
                Log.LogText("Requesting " + getString);
                using HttpResponseMessage response = await sharedClient.GetAsync(getString);


                string stringResponse = await response.Content.ReadAsStringAsync();
                Log.LogText("sent request");
            }catch(Exception ex)
            {
                Log.LogText(ex.Message);
            }
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
                try
                {
                    SaveRuntimeSettings();
                }
                catch (Exception ex)
                {
                    Log.LogText(ex.Message);
                }
            }
        }
        public static int LevelOn
        {
            get { return levelOn; }
            set { 
                levelOn = value;
                try
                {
                    SaveRuntimeSettings();
                }catch (Exception ex)
                {
                    Log.LogText(ex.Message);
                }
            }
        }
    }
}