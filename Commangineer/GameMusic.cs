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
    internal class GameMusic
    {
        MusicType currentMusic = MusicType.Menu;
        Music menuMusic;
        Music[] gameMusic;
        public GameMusic() {
            menuMusic = Assets.GetMusic("menutheme");
            gameMusic = new Music[6];
            for(int i = 0; i<gameMusic.Length; i++)
            {
                gameMusic[i] = Assets.GetMusic("song"+ (i + 1));
            }
        }
        private void Play()
        {
            if(currentMusic == MusicType.Menu)
            {
                menuMusic.Play(true);
            }
            if (currentMusic == MusicType.Gameplay)
            {
                gameMusic[0].Play(true);
            }
        }
        private void Stop()
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
        public MusicType MusicType
        {
            set
            {
                Stop();
                currentMusic = value;
                Play();
            }
        }
    }
}
