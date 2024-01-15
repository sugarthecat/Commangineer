namespace Commangineer
{
    public enum MusicType
    {
        Menu,
        Gameplay
    }

    public static class GameMusic
    {
        private static MusicType currentMusic = MusicType.Menu;
        private static Music menuMusic;
        private static Music gameMusic;

        public static void Setup()
        {
            menuMusic = Assets.GetMusic("menutheme");
            gameMusic = Assets.GetMusic("gametheme");
            Play();
        }

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

        public static void Update()
        {
            Stop();
            Play();
        }
    }
}