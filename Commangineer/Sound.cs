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

        public Sound(String name)
        {
            Assets.LoadSound(name);
            effect = Assets.GetSound(name);
        }

        public void Play()
        {
            SoundEffectInstance sfi = effect.CreateInstance();
            Vector2 listener = new Vector2(0,0);
            Vector2 emitter = new Vector2(0,0);
            Double nv = (45 / (Math.Sqrt(Math.Pow(listener.X - emitter.X, 2) + Math.Pow(listener.Y - emitter.Y, 2))));
            if (nv > 1) { nv = 1; }
            sfi.Volume = (float)(nv);
            Debug.WriteLine(sfi.Volume);
            sfi.Play();
        }
    }
}
