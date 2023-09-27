using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;

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
        private static ContentManager content;
        public static void Setup(ContentManager contentManager)
        {
            Debug.WriteLine("Textured.");
            content = contentManager;
            textures = new Dictionary<string, Texture2D>();
            buttons = new Dictionary<string, Texture2D>();
            fonts = new Dictionary<string, Font>();
            LoadTextures();
            Debug.WriteLine("Textured.");
        }
        /// <summary>
        /// Loads 2d textures for the game, including fonts
        /// </summary>
        public static void LoadTextures()
        {
            //textures.Add("banner", content.Load<Texture2D>("assets/gui/banner"));
            textures.Add("background", content.Load<Texture2D>("assets/background"));
            textures.Add("banner", content.Load<Texture2D>("assets/banner"));
            textures.Add("icon", content.Load<Texture2D>("assets/icon"));
            loadButton("generic");

        }
        /// <summary>
        /// Gets a font object
        /// </summary>
        /// <param name="fontName">The name of the font to request</param>
        /// <returns>the requested Font object</returns>
        public static Font getFont(string fontName)
        {
            return fonts[fontName];
        }
        /// <summary>
        /// Loads the assets for a button with the given name
        /// </summary>
        /// <param name="buttonName">the button's name</param>
        public static void loadButton(string buttonName)
        {
            buttons.Add(buttonName, content.Load<Texture2D>("assets/buttons/" + buttonName));
        }
        /// <summary>
        /// Gets a 2d texture
        /// </summary>
        /// <param name="name">the requested 2d texture's name</param>
        /// <returns>The requested 2d texture</returns>
        public static Texture2D GetTexture2D(string name)
        {
            Debug.WriteLine(textures.Values);
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
    }
}