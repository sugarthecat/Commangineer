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
        private MainMenuGUI mainMenuGUI;
        private TitleScreenGUI titleScreenGUI;
        private LevelGUI levelGUI;
        private MouseState previousMouseState;
        private bool settingsActive;
        private bool windowActive;
        private Level currentLevel;
        public Commangineer()
        {
            instance = this;
            Window.ClientSizeChanged += new EventHandler<EventArgs>(OnResize);
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
            _graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
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
            if (newMenu == "titleScreen")
            {
                currentGUI = titleScreenGUI;
            }
            if (newMenu == "level")
            {
                currentGUI = levelGUI;
                currentLevel = new Level(2);
            }
            if (currentGUI is ScalingGUI)
            {
                ((ScalingGUI)currentGUI).Rescale();
            }
        }
        protected override void Initialize()
        {
            base.Initialize();
            titleScreenGUI = new TitleScreenGUI();
            mainMenuGUI = new MainMenuGUI();
            levelGUI = new LevelGUI();
            currentGUI = titleScreenGUI;
            LoadContent();
        }
        protected override void LoadContent()
        {
            base.LoadContent();
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Assets.Setup(Content);
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
            if(currentGUI == levelGUI)
            {
                currentLevel.Update(gameTime.ElapsedGameTime.Milliseconds);
            }
            previousMouseState = mouseState;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            //draw GUI spritebatch
            currentGUI.Draw(_spriteBatch);
            _spriteBatch.End();
            if(currentGUI == levelGUI)
            {
                _spriteBatch.Begin();
                currentLevel.Draw(_spriteBatch);
                _spriteBatch.End();
            }

            if (currentGUI is ScalingGUI)
            {
                ((ScalingGUI)currentGUI).Rescale();
            }
            base.Draw(gameTime);
        }
    }
}