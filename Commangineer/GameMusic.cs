using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer
{
    internal enum MusicType
    {
        Menu,
        Gameplay
    }
    internal static class GameMusic
    {
        static MusicType currentMusic = MusicType.Menu;
        static Music menuMusic;
        static Music[] gameMusic;
        public static void Setup() {
            menuMusic = Assets.GetMusic("menutheme");
            gameMusic = new Music[6];
            for(int i = 0; i<gameMusic.Length; i++)
            {
                gameMusic[i] = Assets.GetMusic("song"+ (i + 1));
            }
            Play();
        }
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
        public static void Update()
        {
            Stop();
            Play();
        }
    }
}
