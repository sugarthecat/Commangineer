using Commangineer.GUI_Types;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

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
        protected override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            KeyboardState keyboardState = Keyboard.GetState();
            Keys[] pressedKeys = keyboardState.GetPressedKeys();
            if (windowActive)
            {
                if (previousMouseState.LeftButton != ButtonState.Pressed && mouseState.LeftButton == ButtonState.Pressed)
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

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            //draw GUI spritebatch
            currentGUI.Draw(_spriteBatch);
            _spriteBatch.End();
            if (currentGUI == levelGUI)
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