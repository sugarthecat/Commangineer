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
    /// <summary>
    /// A class representing in game music
    /// </summary>
    public class Music
    {
        private Song gameMusic;
        TimeSpan songProgress = new TimeSpan(0);
        public Music(Song song)
        {
            gameMusic = song;
        }

        /// <summary>
        /// Plays a song
        /// </summary>
        /// <param name="repeat">If the song should repeat</param>
        /// <param name="volume">How loud the song is</param>
        public void Play(bool repeat = false, int volume = 1)
        {
            MediaPlayer.Stop();
            MediaPlayer.Volume = (float)(volume);
            MediaPlayer.Play(gameMusic,songProgress);
            
            MediaPlayer.IsRepeating = repeat;
        }

        /// <summary>
        /// Stops a song
        /// </summary>
        public void Stop()
        {
            MediaPlayer.Stop();
            songProgress = MediaPlayer.PlayPosition;
        }

        /// <summary>
        /// Gets the current state of the song
        /// </summary>
        /// <returns>The state of the song</returns>
        public MediaState GetState()
        {
            return MediaPlayer.State;
        }
    }
}
