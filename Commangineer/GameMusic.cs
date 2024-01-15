using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer
{
    /// <summary>
    /// The type of music which can be played
    /// </summary>
    internal enum MusicType
    {
        Menu,
        Gameplay
    }
    /// <summary>
    /// Handles music for the game
    /// </summary>
    internal static class GameMusic
    {
        static MusicType currentMusic = MusicType.Menu;
        static Music menuMusic;
        static Music[] gameMusic;
        /// <summary>
        /// Sets up game music to play
        /// </summary>
        public static void Setup() {
            menuMusic = Assets.GetMusic("menutheme");
            gameMusic = new Music[6];
            for(int i = 0; i<gameMusic.Length; i++)
            {
                gameMusic[i] = Assets.GetMusic("song"+ (i + 1));
            }
            Play();
        }
        /// <summary>
        /// Starts playing music
        /// </summary>
        private static void Play()
        {
            if(Settings.MusicEnabled)
            {
                if (currentMusic == MusicType.Menu)
                {
                    menuMusic.Play(true);
                }
                if (currentMusic == MusicType.Gameplay)
                {
                    gameMusic[0].Play(true);
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
                gameMusic[0].Stop();
            }
        }
        /// <summary>
        /// Allows changing of the current song via a MusicType
        /// </summary>
        public static MusicType MusicType
        {
            set
            {
                if(currentMusic != value)
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
