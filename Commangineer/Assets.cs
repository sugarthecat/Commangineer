﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Commangineer
{
    /// <summary>
    /// Manages assets for the game
    /// </summary>
    public static class Assets
    {
        private static Dictionary<string, List<Texture2D>> textures;
        private static Dictionary<string, Texture2D> buttons;
        private static Dictionary<string, Texture2D> images;
        private static Dictionary<string, Font> fonts;
        private static ContentManager content;
        private static Random accessRandom;
        /// <summary>
        /// Initializes and loadsthe assets for the game
        /// </summary>
        /// <param name="contentManager">the XNA content manager</param>
        public static void Setup(ContentManager contentManager)
        {
            Debug.WriteLine("Textured.");
            content = contentManager;
            accessRandom = new Random();
            textures = new Dictionary<string, List<Texture2D>>();
            images = new Dictionary<string, Texture2D>();
            buttons = new Dictionary<string, Texture2D>();
            fonts = new Dictionary<string, Font>();
            LoadTextures();
            LoadLevels();
            Debug.WriteLine("Textured.");
        }

        /// <summary>
        /// Loads the levels for the game
        /// </summary>
        public static void LoadLevels()
        {

        }
        /// <summary>
        /// Loads 2d textures for the game, including fonts
        /// </summary>
        public static void LoadTextures()
        {
            //textures.Add("banner", content.wwwwwLoad<Texture2D>("assets/gui/banner"));
            LoadImage("background");
            LoadImage("banner");
            LoadImage("icon");
            LoadImage("smiley");
            LoadTexture("stone", "stone1");
            LoadTexture("stone", "stone2");
            LoadTexture("stone", "stone3");
            LoadTexture("stone", "stone4");
            LoadTexture("grass","grassTemp");
            LoadTexture("weeds","grassOverlay");
            LoadTexture("leaves","leaves");
            LoadTexture("wood", "wood");
            LoadTexture("dirt", "dirt1");
            LoadTexture("dirt", "dirt2");
            LoadTexture("dirt", "dirt3");
            LoadTexture("dirt", "dirt4");
            LoadTexture("water", "water1");
            LoadTexture("water", "water2");
            LoadTexture("water", "water3");
            LoadTexture("water", "water4");
            LoadTexture("tree", "tree1");
            LoadTexture("deepwater", "deepwater1");
            LoadTexture("deepwater", "deepwater2");
            LoadTexture("deepwater", "deepwater3");
            LoadTexture("deepwater", "deepwater4");
            LoadTexture("algae", "algae1");
            LoadTexture("algae", "algae2");
            LoadTexture("algae", "algae3");
            LoadTexture("algae", "algae4");
            LoadTexture("lilypads", "lilypads1");
            LoadTexture("lilypads", "lilypads2");
            LoadButton("generic");
            LoadButton("bigredbutton");
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
        /// Loads the image with the given name
        /// </summary>
        /// <param name="buttonName">the image's name</param>
        public static void LoadImage(string buttonName)
        {
            images.Add(buttonName, content.Load<Texture2D>("assets/" + buttonName));
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
            return textures[name][accessRandom.Next(textures[name].Count)];
        }

        /// <summary>
        /// Gets a 2d image
        /// </summary>
        /// <param name="name">the requested 2d texture's name</param>
        /// <returns>The requested 2d texture</returns>
        public static Texture2D GetImage(string name)
        {
            return images[name];
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