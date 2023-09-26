using Commangineer.GUI_Types;
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
        private GUI titleScreenGUI;
        private MouseState previousMouseState;
        private bool settingsActive;
        private bool windowActive;
        public Commangineer()
        {
            instance = this;
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += new EventHandler<EventArgs>(OnResize);
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1200; 
            _graphics.PreferredBackBufferHeight = 900;
            //_graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            windowActive = false;
            previousMouseState = Mouse.GetState();
            this.Activated += WindowOpened;
            this.Deactivated += WindowClosed;
        }
        private void OnResize(object sender, EventArgs e)
        {
            _graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
            _graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
            _graphics.ApplyChanges();
            if (currentGUI is ScalingGUI)
            {
                ((ScalingGUI)currentGUI).Rescale();
            }
        }
        public void WindowOpened(object sendet, EventArgs args)
        {
            windowActive = true;
        }

        public void WindowClosed(object sendet, EventArgs args)
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
            base.Initialize();
            titleScreenGUI = new TitleScreenGUI();
            mainMenuGUI = new MainMenuGUI();
            currentGUI = titleScreenGUI;
            LoadContent();
        }
        protected override void LoadContent()
        {
            base.LoadContent();
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