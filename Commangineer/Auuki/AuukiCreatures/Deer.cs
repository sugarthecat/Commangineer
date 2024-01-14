using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer.Auuki.AuukiCreatures
{
    internal class Deer : AuukiCreature
    {
        public Deer(Vector2 position) : base(position, new Vector2(1.5f, 1.5f), Assets.GetTexture("rat"))
        {

        }
    }
}
