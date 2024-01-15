using Commangineer.GUI_Types;
using Commangineer.User_Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

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
        private TitleScreenGUI titleScreenGUI;
        private LevelGUI levelGUI;
        private SettingsGUI settingsGUI;
        private LevelSelectGUI levelSelectGUI;
        private MouseState previousMouseState;
        private KeyboardState previousKeyboardState;
        private bool windowActive;
        private bool spriteBatchBegun;
        private Level currentLevel;
        private string lastError;
        private static int highestLevel;
        private static bool completedLevel;

        /// <summary>
        /// Sets up basic values of the game
        /// </summary>
        public Commangineer()
        {
            instance = this;
            Window.AllowUserResizing = true;
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

        /// <summary>
        /// Ran when the window is resized, updates corresponding values and GUI's
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnResize(object sender, EventArgs e)
        {
            _graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
            _graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
            _graphics.ApplyChanges();
            currentGUI.Update();
            if(currentGUI == levelGUI)
            {
                Camera.UpdateScale(0);
            }
        }

        /// <summary>
        /// Ran when the window is opened, updates corresponding values
        /// </summary>
        /// <param name="sendet"></param>
        /// <param name="args"></param>
        public void WindowOpened(object sendet, EventArgs args)
        {
            windowActive = true;
        }

        /// <summary>
        /// Ran when the window is closed/minimized, updates corresponding values
        /// </summary>
        /// <param name="sendet"></param>
        /// <param name="args"></param>
        public void WindowClosed(object sendet, EventArgs args)
        {
            windowActive = false;
        }

        /// <summary>
        /// Gets the current level the user is in if any
        /// </summary>
        /// <returns></returns>
        public static Level GetLevel()
        {
            return instance.currentLevel;
        }

        /// <summary>
        /// Activates/deactivates the setting GUI
        /// </summary>
        public void ToggleSettings()
        {
            settingsGUI.Enabled = !settingsGUI.Enabled;
        }

        /// <summary>
        /// Gets the current screen width
        /// </summary>
        /// <returns>The screen's width</returns>
        public static int GetScreenWidth()
        {
            return instance._graphics.PreferredBackBufferWidth;
        }

        /// <summary>
        /// Gets the current screen height
        /// </summary>
        /// <returns>The screen's height</returns>
        public static int GetScreenHeight()
        {
            return instance._graphics.PreferredBackBufferHeight;
        }

        /// <summary>
        /// Switches full screen on or off
        /// </summary>
        public static void ToggleFullscreen()
        {
            instance._graphics.ToggleFullScreen();
        }

        /// <summary>
        /// Allows retrieval of the current level
        /// </summary>
        public static Level Level
        {
            get
            {
                return instance.currentLevel;
            }
        }

        /// <summary>
        /// Switches the current main GUI to a different one
        /// </summary>
        /// <param name="newMenu">The new GUI to go to</param>
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
                switch (newMenu)
                {
                    case "titleScreen":
                        currentGUI = titleScreenGUI;
                        break;

                    case "levelSelect":
                        currentGUI = levelSelectGUI;
                        break;
                }
            }
            currentGUI.Update();
            if (settingsGUI.Enabled)
            {
                settingsGUI.Update();
            }
        }

        /// <summary>
        /// Starts a specified level if available
        /// </summary>
        /// <param name="level"></param>
        public void BeginLevel(int level)
        {
            if (level <= highestLevel+1)
            {
                currentLevel = new Level(level, levelGUI);
                NavigateToMenu("level");
            }
        }

        /// <summary>
        /// Called when a level is won
        /// </summary>
        /// <param name="level">The level beaten</param>
        public static void WinLevel(int level)
        {
            if (level > highestLevel)
            {
                highestLevel = level;
            }
            // Now next time we update we navigate back to level select, can't do it here as it's static
            completedLevel = true;
        }

        /// <summary>
        /// Ran when the game is initializing, sets up additional values
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            //initialize top level game systems
            Settings.LoadSettings();
            GameMusic.Setup();
            GameMusic.MusicType = MusicType.Menu;
            //initialize UIs
            titleScreenGUI = new TitleScreenGUI();
            levelGUI = new LevelGUI();
            currentLevel = new Level(1, levelGUI);
            highestLevel = 0;
            settingsGUI = new SettingsGUI();
            settingsGUI.Enabled = false;
            levelSelectGUI = new LevelSelectGUI(highestLevel);
            currentGUI = titleScreenGUI;
            //initialize interface values
            previousKeyboardState = Keyboard.GetState();
            previousMouseState = Mouse.GetState();
            //initialize camera
            Camera.UpdateScale(0);
            Log.LogText("Game Initialized");
        }

        /// <summary>
        /// Ran when the game first loads in its content, sets up all assets
        /// </summary>
        protected override void LoadContent()
        {
            base.LoadContent();
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Assets.Setup(Content);
        }

        /// <summary>
        /// Ran when the program is exiting, cleans up the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected override void OnExiting(Object sender, EventArgs args)
        {
            base.OnExiting(sender, args);

            Settings.SaveSettings();
        }

        /// <summary>
        /// Exits the game
        /// </summary>
        public static void ExitGame()
        {
            instance.Exit();
        }

        /// <summary>
        /// Opens a link in the user's default browser
        /// </summary>
        /// <param name="url">The link to be opened</param>
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

        /// <summary>
        /// Ran when the game updates, handles all interactions between classes while the game is running
        /// </summary>
        /// <param name="gameTime">The amount of time since the last update in ms</param>
        protected override void Update(GameTime gameTime)
        {
            try
            {
                if (completedLevel)
                {
                    completedLevel = false;
                    NavigateToMenu("levelSelect");
                }
                MouseState mouseState = Mouse.GetState();
                KeyboardState keyboardState = Keyboard.GetState();
                Keys[] pressedKeys = keyboardState.GetPressedKeys();
                if (windowActive)
                {
                    //handle left click
                    if (previousMouseState.LeftButton == ButtonState.Released && mouseState.LeftButton == ButtonState.Pressed && windowActive)
                    {
                        if (settingsGUI.Enabled)
                        {
                            settingsGUI.HandleClick(new Point(mouseState.X, mouseState.Y));
                            settingsGUI.Update();
                        }
                        else
                        {
                            currentGUI.HandleClick(new Point(mouseState.X, mouseState.Y));
                            if (currentGUI == levelGUI)
                            {
                                currentLevel.HandleClick(new Point(mouseState.X, mouseState.Y), keyboardState.IsKeyDown(Keys.LeftShift));
                            }
                        }
                    }
                    //handle right click
                    if (previousMouseState.RightButton == ButtonState.Released && mouseState.RightButton == ButtonState.Pressed && windowActive)
                    {
                        if (currentGUI == levelGUI && !settingsGUI.Enabled)
                        {
                            currentLevel.HandleRightClick(new Point(mouseState.X, mouseState.Y));
                        }
                    }
                    if (previousKeyboardState.IsKeyUp(Keys.Escape) && keyboardState.IsKeyDown(Keys.Escape))
                    {
                        ToggleSettings();
                    }
                }
                if (!settingsGUI.Enabled)
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
                currentGUI.Update();
                previousMouseState = mouseState;
                previousKeyboardState = keyboardState;
                base.Update(gameTime);
            }
            catch (Exception exc)
            {
                RaiseError("A exception has occured while trying to update the game " + exc.Message, exc.StackTrace);
            }
        }

        /// <summary>
        /// Opens a email link intended for when the program crashes
        /// </summary>
        public void EmailCrash()
        {
            OpenUrl("mailto:tnickerson2024@jpkeefehs.org?subject=Commangineer%20Crash%20Report&body=" + lastError);
            ExitGame();
        }

        /// <summary>
        /// Raises a critical error screen when the application can not continue
        /// </summary>
        /// <param name="msg">The message of the error</param>
        /// <param name="trace">The callstack of where the error happened</param>
        private void RaiseError(string msg, string trace)
        {
            string informationString = DateTime.Now.ToString("MM/dd/yyyy h:mm tt") + " " + msg + " at " + trace;
            lastError = informationString;
            Log.LogText(informationString);
            currentGUI = new ErrorGUI(msg, informationString);
        }

        /// <summary>
        /// Toggles the sprite batch to begin or end drawing
        /// </summary>
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

        /// <summary>
        /// Ran when the game draws to the screen, tells all objects to draw
        /// </summary>
        /// <param name="gameTime">The time since that game last drew in ms</param>
        protected override void Draw(GameTime gameTime)
        {
            try
            {
                GraphicsDevice.Clear(Color.Black);
                ToggleSpriteBatch();
                //draw GUI spritebatch
                if (settingsGUI.Enabled)
                {
                    settingsGUI.Draw(_spriteBatch);
                    ToggleSpriteBatch();
                }
                else
                {
                    if (currentGUI == levelGUI)
                    {
                        currentLevel.Draw(_spriteBatch);
                        //flush sprite batch for cleanliness
                        ToggleSpriteBatch();
                        ToggleSpriteBatch();
                    }
                    currentGUI.Draw(_spriteBatch);
                    ToggleSpriteBatch();
                }

                currentGUI.Update();
                if (settingsGUI.Enabled)
                {
                    settingsGUI.Update();
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