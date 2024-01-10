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

        private void createSound(String name, Vector2 emitterPosition)
        {
            effect = Assets.GetSound(name);
            position = emitterPosition;
            instance = null;
        }

        public void MoveTo(Vector2 newPosition)
        {
            position = newPosition;
        }
        
        public void Play() { Play(Vector2.Zero); }
        public void Play(Vector2 listenerPosition)
        {
            if (instance != null) { Stop(); }
            instance = effect.CreateInstance();
            Double nv = (45 / (Math.Sqrt(Math.Pow(listenerPosition.X - position.X, 2) + Math.Pow(listenerPosition.Y - position.Y, 2))));
            if (nv > 1) { nv = 1; }
            instance.Volume = (float)(nv);
            instance.Play();
        }
        
        public void Stop()
        {
            instance.Stop();
        }

        public SoundState GetState()
        {
            return instance.State;
        }
    }
}
