using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Commangineer
{
    public class Commangineer : Game
    {
        public static Commangineer instance;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GUI currentGUI;
        private GUI mainMenuGUI;
        private MouseState previousMouseState;
        private bool settingsActive;
        private bool windowActive;
        public Commangineer()
        {
            instance = this;
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1200;
            _graphics.PreferredBackBufferHeight = 900;
            //_graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            windowActive = false;
            previousMouseState = Mouse.GetState();
            this.Activated += windowOpened;
            this.Deactivated += windwoClosed;
        }
        public void windowOpened(object sendet, EventArgs args)
        {
            windowActive = true;
        }

        public void windwoClosed(object sendet, EventArgs args)
        {
            windowActive = false;
        }

        public int GetScreenWidth()
        {
            return _graphics.PreferredBackBufferWidth;
        }
        public int GetScreenHeight()
        {
            return _graphics.PreferredBackBufferHeight;
        }
        public void ToggleSettings()
        {
            settingsActive = !settingsActive;
        }
        public void ToggleFullscreen()
        {
            _graphics.ToggleFullScreen();
        }
        public void NavigateToMenu(string newMenu)
        {
            if (newMenu == "mainMenu")
            {
                currentGUI = mainMenuGUI;
            }
        }
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
            mainMenuGUI = new GUI();
            currentGUI = mainMenuGUI;
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Assets.Setup(Content);
            //BANNER_TXTR = Content.Load<Texture2D>("assets/banner");
        }

        protected override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            if (windowActive)
            {
                if (previousMouseState.LeftButton != ButtonState.Pressed && mouseState.LeftButton == ButtonState.Pressed)
                {
                        currentGUI.HandleClick(new Point(mouseState.X, mouseState.Y));
                     
                }
            }

            // TODO: Add your update logic here

            previousMouseState = mouseState;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            currentGUI.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}