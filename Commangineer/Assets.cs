using Commangineer.GUI_Element_Types;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text.Json.Nodes;

namespace Commangineer
{
    /// <summary>
    /// Manages assets for the game
    /// </summary>
    public static class Assets
    {
        private static Dictionary<string, Texture2D> textures;
        private static Dictionary<string, Texture2D> buttons;
        private static Dictionary<string, Font> fonts;
        private static Dictionary<string, SoundEffect> sounds;
        private static Dictionary<string, Song> music;
        private static ContentManager content;
        public static void Setup(ContentManager contentManager)
        {
            Debug.WriteLine("Textured.");
            content = contentManager;
            textures = new Dictionary<string, Texture2D>();
            buttons = new Dictionary<string, Texture2D>();
            sounds = new Dictionary<string, SoundEffect>();
            music = new Dictionary<string, Song>();
            fonts = new Dictionary<string, Font>();
            LoadTextures();
            Debug.WriteLine("Textured.");
        }
        /// <summary>
        /// Loads 2d textures for the game, including fonts
        /// </summary>
        public static void LoadTextures()
        {
            JsonNode res = null;
            try
            {
                string sources = Assembly.GetExecutingAssembly().Location + "/../../../../Content";
                string text = String.Join("", File.ReadAllLines(sources + "/assets/assetlist.json").Select(x => x.Trim()).ToArray());
                res = JsonObject.Parse(text);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error loading file " + ex.Message);
            }
            if (res != null)
            {
                try
                {
                    foreach (string s in res["textures"].AsArray())
                    {
                        LoadTexture(s);
                    }
                    foreach (string s in res["buttons"].AsArray())
                    {
                        LoadButton(s);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error reading JSON object from file: " + ex.Message);
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
        /// Loads the assets for a button with the given name
        /// </summary>
        /// <param name="buttonName">the button's name</param>
        public static void LoadButton(string buttonName)
        {
            buttons.Add(buttonName, content.Load<Texture2D>("assets/buttons/" + buttonName));
        }
        /// <summary>
        /// Loads the asset for a texture with the given name
        /// </summary>
        /// <param name="textureName">the asset's name</param>
        public static void LoadTexture(string textureName)
        {
            textures.Add(textureName, content.Load<Texture2D>("assets/" + textureName));
        }
        /// <summary>
        /// Gets a 2d texture
        /// </summary>
        /// <param name="name">the requested 2d texture's name</param>
        /// <returns>The requested 2d texture</returns>
        public static Texture2D GetTexture2D(string name)
        {
            return textures[name];
        }
        /// <summary>
        /// Gets a 2d button texture
        /// </summary>
        /// <param name="name">the requested 2d button texture's name</param>
        /// <returns>The requested 2d button texture</returns>
        public static Texture2D GetButtonTexure(string name)
        {
            return buttons[name];
        }
        public static void LoadShaders(ContentManager content)
        {
            /*
             * empty
            */
        }
        public static void LoadSound(string name)
        {
            sounds.Add(name, content.Load<SoundEffect>("audio/Sounds/" + name));
        }
        public static SoundEffect GetSound(string name)
        {
            return sounds[name];
        }
        public static void LoadMusic(string name)
        {
            music.Add(name, content.Load<Song>("audio/Music/" + name));
        }
        public static Song GetMusic(string name)
        {
            return music[name];
        }
    }
}