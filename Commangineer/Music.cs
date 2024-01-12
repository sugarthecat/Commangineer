using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Microsoft.Xna.Framework.Media;

namespace Commangineer
{
    public class Music
    {
        private Song gameMusic;
        TimeSpan songProgress = new TimeSpan(0);
        public Music(Song song)
        {
            gameMusic = song;
        }

        public void Play(bool repeat = false, int volume = 1)
        {
            MediaPlayer.Stop();
            MediaPlayer.Volume = (float)(volume);
            MediaPlayer.Play(gameMusic,songProgress);
            
            MediaPlayer.IsRepeating = repeat;
        }

        public void Stop()
        {
            MediaPlayer.Stop();
            songProgress = MediaPlayer.PlayPosition;
        }

        public MediaState GetState()
        {
            return MediaPlayer.State;
        }
    }
}
