using Microsoft.Xna.Framework.Media;
using System;

namespace Commangineer
{
    public class Music
    {
        private Song gameMusic;
        private TimeSpan songProgress = new TimeSpan(0);

        public Music(Song song)
        {
            gameMusic = song;
        }

        public void Play(bool repeat = false, int volume = 1)
        {
            MediaPlayer.Stop();
            MediaPlayer.Volume = (float)(volume);
            MediaPlayer.Play(gameMusic, songProgress);

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