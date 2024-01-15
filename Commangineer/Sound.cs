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

namespace Commangineer
{
    /// <summary>
    /// Represents a in game sound
    /// </summary>
    public class Sound
    {
        private SoundEffect effect;
        private Vector2 position;
        private SoundEffectInstance instance;

        public Sound(String name)
        {
            createSound(name, Vector2.Zero);
        }

        public Sound(String name, Vector2 emitterPosition)
        {
            createSound(name, emitterPosition);
        }

        /// <summary>
        /// Called by constructures to set up sound
        /// </summary>
        /// <param name="name">Asset name of the sound</param>
        /// <param name="emitterPosition">The 2D position of the sound</param>
        private void createSound(String name, Vector2 emitterPosition)
        {
            effect = Assets.GetSound(name);
            position = emitterPosition;
            instance = null;
        }

        /// <summary>
        /// Changes the sound's position
        /// </summary>
        /// <param name="newPosition">The new position of the sound</param>
        public void MoveTo(Vector2 newPosition)
        {
            position = newPosition;
        }
        
        /// <summary>
        /// Neccessary method to set a default value for the listenerPosition
        /// </summary>
        public void Play() { Play(Vector2.Zero); }
        /// <summary>
        /// Plays a sound
        /// </summary>
        /// <param name="listenerPosition">The location of where the user would be hearing things</param>
        public void Play(Vector2 listenerPosition)
        {
            if (instance != null) { Stop(); }
            instance = effect.CreateInstance();
            Double nv = (45 / (Math.Sqrt(Math.Pow(listenerPosition.X - position.X, 2) + Math.Pow(listenerPosition.Y - position.Y, 2))));
            if (nv > 1) { nv = 1; }
            instance.Volume = (float)(nv);
            instance.Play();
        }
        
        /// <summary>
        /// Stops a sound from playing
        /// </summary>
        public void Stop()
        {
            instance.Stop();
        }

        /// <summary>
        /// Gets the state of the Sound
        /// </summary>
        /// <returns>The sound state of the sound</returns>
        public SoundState GetState()
        {
            return instance.State;
        }
    }
}
