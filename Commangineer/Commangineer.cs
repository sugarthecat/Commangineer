using Commangineer.GUI_Types;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace Commangineer
{
    /// <summary>
    /// The main handler for the game used in conjunction with all other classes
    /// </summary>
    public class Commangineer : Game
    {
        public static Commangineer instance;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GUI currentGUI;
        private MainMenuGUI mainMenuGUI;
        private TitleScreenGUI titleScreenGUI;
        private LevelGUI levelGUI;
        private SettingsGUI settingsGUI;
        private LevelSelectGUI levelSelectGUI;
        private DialogueGUI dialogueGUI;
        private MouseState previousMouseState;
        private KeyboardState previousKeyboardState;
        private bool windowActive;
        private bool spriteBatchBegun;
        private Level currentLevel;
        private string lastError;
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
            spriteBatchBegun = false;
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

        public static Level GetLevel()
        {
            return instance.currentLevel;
        }
        public void ToggleSettings()
        {
            settingsGUI.Active = !settingsGUI.Active;
        }
        public static int GetScreenWidth()
        {
            return instance._graphics.PreferredBackBufferWidth;
        }

        public static int GetScreenHeight()
        {
            return instance._graphics.PreferredBackBufferHeight;
        }

        public void ToggleFullscreen()
        {
            _graphics.ToggleFullScreen();
        }

        public void NavigateToMenu(string newMenu)
        {
            if (newMenu == "level")
            {
                GameMusic.MusicType = MusicType.Gameplay;
                currentGUI = levelGUI;
            }
            else
            {
                GameMusic.MusicType = MusicType.Menu;
                switch(newMenu)
                {
                    case "titleScreen":
                        currentGUI = titleScreenGUI;
                        break;
                    case "mainMenu":
                        currentGUI = mainMenuGUI;
                        break;
                    case "levelSelect":
                        currentGUI = levelSelectGUI;
                        break;
                }
            }


            if (currentGUI is ScalingGUI)
            {
                ((ScalingGUI)currentGUI).Rescale();
            }
            if (settingsGUI.Active)
            {
                ((ScalingGUI)settingsGUI).Rescale();
            }
        }

        protected override void Initialize()
        {
            base.Initialize();
            //initialize top level game systems
            Settings.LoadSettings();
            GameMusic.Setup();
            GameMusic.MusicType = MusicType.Menu;
            //initialize UIs
            titleScreenGUI = new TitleScreenGUI();
            mainMenuGUI = new MainMenuGUI();
            levelGUI = new LevelGUI();
            settingsGUI = new SettingsGUI();
            levelSelectGUI = new LevelSelectGUI();
            currentLevel = new Level(1);
            currentGUI = titleScreenGUI;
            //initialize interface values
            previousKeyboardState = Keyboard.GetState();
            previousMouseState = Mouse.GetState();
            //initialize camera
            Camera.UpdateScale(0);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Assets.Setup(Content);
        }
        protected override void OnExiting(Object sender, EventArgs args)
        {
            base.OnExiting(sender, args);

            Settings.SaveSettings();
            // Stop the threads
        }
        public static void ExitGame()
        {
            instance.Exit();
        }
        private void OpenUrl(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }
        protected override void Update(GameTime gameTime)
        {
            try
            {
                MouseState mouseState = Mouse.GetState();
                KeyboardState keyboardState = Keyboard.GetState();
                Keys[] pressedKeys = keyboardState.GetPressedKeys();
                if (windowActive)
                {
                   if(previousMouseState.LeftButton == ButtonState.Released && mouseState.LeftButton == ButtonState.Pressed)
                   {
                        if (settingsGUI.Active)
                        {
                            settingsGUI.HandleClick(new Point(mouseState.X, mouseState.Y));
                            ((SettingsGUI)settingsGUI).Rescale();
                        }
                        else
                        {
                            currentGUI.HandleClick(new Point(mouseState.X, mouseState.Y));
                        }
                   }
                   if(previousKeyboardState.IsKeyUp(Keys.Escape) && keyboardState.IsKeyDown(Keys.Escape)) {
                        ToggleSettings();
                   }
                }
                if (!settingsGUI.Active)
                {
                    if (currentGUI == levelGUI)
                    {
                        currentLevel.Update(gameTime.ElapsedGameTime.Milliseconds, keyboardState, previousKeyboardState, mouseState, previousMouseState);
                    }
                }
                else
                {
                    settingsGUI.Update();
                }
                previousMouseState = mouseState;
                previousKeyboardState = keyboardState;
                base.Update(gameTime);
            }
            catch (Exception exc)
            {
                RaiseError("A exception has occured while trying to update the game " + exc.Message, exc.StackTrace);
            }
        }

        public void EmailCrash()
        {
            OpenUrl("mailto:tnickerson2024@jpkeefehs.org?subject=Commangineer%20Crash%20Report&body=" + lastError);
            ExitGame();
        }

        private void RaiseError(string msg, string trace)
        {
            string informationString = DateTime.Now.ToString("MM/dd/yyyy h:mm tt") + " " + msg + " at " + trace;
            lastError = informationString;
            Log.LogText(informationString);
            currentGUI = new ErrorGUI(msg, informationString);
        }

        private void ToggleSpriteBatch()
        {
            spriteBatchBegun = !spriteBatchBegun;
            if (spriteBatchBegun)
            {
                _spriteBatch.Begin();
            }
            else
            {
                _spriteBatch.End();
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            try
            {
                GraphicsDevice.Clear(Color.Black);
                ToggleSpriteBatch();
                //draw GUI spritebatch
                if (settingsGUI.Active)
                {
                    settingsGUI.Draw(_spriteBatch);
                    ToggleSpriteBatch();
                }
                else
                {
                    currentGUI.Draw(_spriteBatch);
                    ToggleSpriteBatch();
                    if (currentGUI == levelGUI)
                    {
                        ToggleSpriteBatch();
                        currentLevel.Draw(_spriteBatch);
                        ToggleSpriteBatch();
                    }
                }

                if (currentGUI is ScalingGUI)
                {
                    ((ScalingGUI)currentGUI).Rescale();
                }
                if (settingsGUI.Active)
                {
                    (settingsGUI).Rescale();
                }
                base.Draw(gameTime);
            }
            catch (Exception exc)
            {
                if (spriteBatchBegun) { _spriteBatch.End(); }
                RaiseError("A exception has occured while trying to draw to the screen: " + exc.Message, exc.StackTrace);
            }
        }
    }
}