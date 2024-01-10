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
        private DialogueGUI dialogueGUI;
        private MouseState previousMouseState;
        private KeyboardState previousKeyboardState;
        private bool settingsActive;
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

        public static int GetScreenWidth()
        {
            return instance._graphics.PreferredBackBufferWidth;
        }

        public static int GetScreenHeight()
        {
            return instance._graphics.PreferredBackBufferHeight;
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
            }
            if (newMenu == "dialogue")
            {
                // add onto current gui somehow, maybe add a function to gui to allow loading additional elements? no we need a subgui property for GUIs thats also a gui and then have the draw function check for the subgui as well no wait subgui should be a array of guis so that we can load multiple if needed, actually no a list since we dont know how many
                //currentGUI.AddSubGUI(dialogueGUI);
            }
            if (currentGUI is ScalingGUI)
            {
                ((ScalingGUI)currentGUI).Rescale();
            }
        }

        protected override void Initialize()
        {
            base.Initialize();
            //initialize UIs
            titleScreenGUI = new TitleScreenGUI();
            mainMenuGUI = new MainMenuGUI();
            levelGUI = new LevelGUI();
            currentLevel = new Level(1);
            currentGUI = titleScreenGUI;
            //initialize interface values
            previousKeyboardState = Keyboard.GetState();
            previousMouseState = Mouse.GetState();
            //initialize camera
            Camera.UpdateScale(0);
            LoadContent();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Assets.Setup(Content);
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

                        currentGUI.HandleClick(new Point(mouseState.X, mouseState.Y));
                   }
                }
                if (currentGUI == levelGUI)
                {
                    currentLevel.Update(gameTime.ElapsedGameTime.Milliseconds, keyboardState, previousKeyboardState, mouseState, previousMouseState);
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
            Exit();
        }

        private void RaiseError(string msg, string trace)
        {
            string informationString = DateTime.Now.ToString("MM/dd/yyyy h:mm tt") + " " + msg + " at " + trace;
            lastError = informationString;
            Log.logText(informationString);
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
                currentGUI.Draw(_spriteBatch);
                ToggleSpriteBatch();
                if (currentGUI == levelGUI)
                {
                    ToggleSpriteBatch();
                    currentLevel.Draw(_spriteBatch);
                    ToggleSpriteBatch();
                }

                if (currentGUI is ScalingGUI)
                {
                    ((ScalingGUI)currentGUI).Rescale();
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