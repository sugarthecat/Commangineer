using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json.Nodes;

namespace Commangineer
{
    public enum Character
    {
        testChar1,
        testChar2,
        testChar3
    }

    /// <summary>
    /// Manages assets for the game
    /// </summary>
    public static class Assets
    {
        private static Dictionary<string, List<Texture2D>> textures;
        private static Dictionary<string, Texture2D> buttons;
        private static Dictionary<string, Texture2D> images;
        private static Dictionary<string, Font> fonts;
        private static Dictionary<string, SoundEffect> sounds;
        private static Dictionary<string, Song> songs;
        private static ContentManager content;
        private static Random accessRandom;

        /// <summary>
        /// Initializes and loadsthe assets for the game
        /// </summary>
        /// <param name="contentManager">the XNA content manager</param>
        public static void Setup(ContentManager contentManager)
        {
            content = contentManager;
            accessRandom = new Random();
            textures = new Dictionary<string, List<Texture2D>>();
            images = new Dictionary<string, Texture2D>();
            buttons = new Dictionary<string, Texture2D>();
            sounds = new Dictionary<string, SoundEffect>();
            songs = new Dictionary<string, Song>();
            fonts = new Dictionary<string, Font>();
            LoadAssets();
            LoadLevels();
            Log.LogText("Textures loaded");
        }

        /// <summary>
        /// Loads the levels for the game
        /// </summary>
        public static void LoadLevels()
        {
        }

        /// <summary>
        /// Loads 2d textures for the game, including fonts, along with audio
        /// </summary>
        public static void LoadAssets()
        {
            JsonNode res = null;
            try
            {
                LoadImage("default");
                string sources = Assembly.GetExecutingAssembly().Location + "/../Content";
                string text = String.Join("", File.ReadAllLines(sources + "/assets/assetlist.json").Select(x => x.Trim()).ToArray());
                res = JsonObject.Parse(text);
            }
            catch (Exception ex)
            {
                Log.LogText("Error loading file " + ex.Message);
            }
            if (res != null)
            {
                try
                {
                    foreach (string s in res["images"].AsArray())
                    {
                        try
                        {
                            LoadImage(s);
                        }
                        catch (Exception e)
                        {
                            Log.LogText("Error loading image " + s + e.Message);
                        }
                    }
                    foreach (JsonNode node in res["textures"].AsArray())
                    {
                        try
                        {
                            LoadTexture((string)node[0], (string)node[1]);
                        }
                        catch
                        {
                            Log.LogText("Error loading texture " + node.ToJsonString());
                        }
                    }
                    foreach (string s in res["buttons"].AsArray())
                    {
                        try
                        {
                            LoadButton(s);
                        }
                        catch
                        {
                            Log.LogText("Error loading button " + s);
                        }
                    }
                    foreach (string s in res["sounds"].AsArray())
                    {
                        try
                        {
                            LoadSound(s);
                        }
                        catch
                        {
                            Log.LogText("Error loading sound " + s);
                        }
                    }
                    foreach (string s in res["music"].AsArray())
                    {
                        try
                        {
                            LoadMusic(s);
                        }
                        catch
                        {
                            Log.LogText("Error loading music " + s);
                        }
                    }
                    foreach (string s in res["fonts"].AsArray())
                    {
                        try
                        {
                            LoadFont(s);
                        }
                        catch
                        {
                            Log.LogText("Error loading font " + s);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.LogText("Error reading JSON object from file: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Gets a font object
        /// </summary>
        /// <param name="fontName">The name of the font to request</param>
        /// <returns>the requested Font object</returns>
        public static Font GetFont(string fontName)
        {
            return fonts[fontName];
        }

        /// <summary>
        /// Loads the assets for a font with the given name
        /// </summary>
        /// <param name="fontName"></param>The font's name
        public static void LoadFont(string fontName)
        {
            string dest = "assets/fonts/" + fontName;
            fonts.Add(fontName, new Font(dest, content));
        }

        /// <summary>
        /// Loads the assets for a button with the given name
        /// </summary>
        /// <param name="buttonName">the button's name</param>
        public static void LoadButton(string buttonName)
        {
            buttons.Add(buttonName, content.Load<Texture2D>("assets/buttons/" + buttonName));
        }

        /// <summary>
        /// Loads the image with the given name
        /// </summary>
        /// <param name="imageName">the image's name</param>
        public static void LoadImage(string imageName)
        {
            images.Add(imageName, content.Load<Texture2D>("assets/images/" + imageName));
        }

        /// <summary>
        /// Loads the asset for a texture with the given name
        /// </summary>
        /// <param name="textureName">the asset's name</param>
        /// <param name="fileName">the asset's name, in the file directory</param>
        public static void LoadTexture(string textureName, string fileName)
        {
            if (!textures.ContainsKey(textureName))
            {
                textures[textureName] = new List<Texture2D>();
            }
            textures[textureName].Add(content.Load<Texture2D>("assets/textures/" + fileName));
        }

        /// <summary>
        /// Gets a 2d texture
        /// </summary>
        /// <param name="name">the requested 2d texture's name</param>
        /// <returns>The requested 2d texture</returns>
        public static Texture2D GetTexture(string name)
        {
            if (textures.ContainsKey(name))
            {
                return textures[name][accessRandom.Next(textures[name].Count)];
            }
            else
            {
                Log.LogText("Texture grab error: " + name);
                return images["default"];
            }
        }

        /// <summary>
        /// Gets a 2d image
        /// </summary>
        /// <param name="name">the requested 2d texture's name</param>
        /// <returns>The requested 2d texture</returns>
        public static Texture2D GetImage(string name)
        {
            if (images.ContainsKey(name))
            {
                return images[name];
            }
            else
            {
                Log.LogText("Image grab error: " + name);
                return images["default"];
            }
        }

        /// <summary>
        /// Gets a 2d button texture
        /// </summary>
        /// <param name="name">the requested 2d button texture's name</param>
        /// <returns>The requested 2d button texture</returns>
        public static Texture2D GetButtonTexure(string name)
        {
            if (buttons.ContainsKey(name))
            {
                return buttons[name];
            }
            else
            {
                Log.LogText("Texture grab error: " + name);
                return images["default"];
            }
        }

        public static void LoadShaders(ContentManager content)
        {
            /*
             * empty
            */
        }

        /// <summary>
        /// Loads the sound with the given name
        /// </summary>
        /// <param name="name">the sound's name</param>
        public static void LoadSound(string name)
        {
            sounds.Add(name, content.Load<SoundEffect>("audio/Sounds/" + name));
        }

        /// <summary>
        /// Gets a sound
        /// </summary>
        /// <param name="name">the requested sounds name</param>
        /// <returns>The requested sound</returns>
        public static SoundEffect GetSound(string name)
        {
            return sounds[name];
        }

        /// <summary>
        /// Loads the song with the given name
        /// </summary>
        /// <param name="name">the song's name</param>
        public static void LoadMusic(string name)
        {
            songs.Add(name, content.Load<Song>("audio/Music/" + name));
        }

        /// <summary>
        /// Gets a song
        /// </summary>
        /// <param name="name">the requested song's name</param>
        /// <returns>The requested song</returns>
        public static Music GetMusic(string name)
        {
            return new Music(songs[name]);
        }
    }
}