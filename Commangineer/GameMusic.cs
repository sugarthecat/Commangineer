namespace Commangineer
{
    /// <summary>
    /// The type of music which can be played
    /// </summary>
    public enum MusicType
    {
        Menu,
        Gameplay
    }

    /// <summary>
    /// Handles music for the game
    /// </summary>
    public static class GameMusic
    {
        private static MusicType currentMusic = MusicType.Menu;
        private static Music menuMusic;
        private static Music gameMusic;

        /// <summary>
        /// Sets up game music to play
        /// </summary>
        public static void Setup()
        {
            menuMusic = Assets.GetMusic("menutheme");
            gameMusic = Assets.GetMusic("gametheme");
            Play();
        }

        /// <summary>
        /// Starts playing music
        /// </summary>
        private static void Play()
        {
            if (Settings.MusicEnabled)
            {
                if (currentMusic == MusicType.Menu)
                {
                    menuMusic.Play(true);
                }
                if (currentMusic == MusicType.Gameplay)
                {
                    gameMusic.Play(true);
                }
            }
        }

        /// <summary>
        /// Stops playing music
        /// </summary>
        private static void Stop()
        {
            if (currentMusic == MusicType.Menu)
            {
                menuMusic.Stop();
            }
            if (currentMusic == MusicType.Gameplay)
            {
                gameMusic.Stop();
            }
        }

        /// <summary>
        /// Allows changing of the current song via a MusicType
        /// </summary>
        public static MusicType MusicType
        {
            set
            {
                if (currentMusic != value)
                {
                    Stop();
                    currentMusic = value;
                    Play();
                }
            }
        }

        /// <summary>
        /// Updates the music handler
        /// </summary>
        public static void Update()
        {
            Stop();
            Play();
        }
    }
}