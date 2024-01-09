using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer.AuukiCreatures
{
    internal class Rat: AuukiCreature
    {
        public Rat(Vector2 position) : base(position, new Vector2(1f,1f), Assets.GetTexture("rat")) { 
        
        }
    }
}
