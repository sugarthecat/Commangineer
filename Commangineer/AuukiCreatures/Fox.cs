using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer.AuukiCreatures
{
    internal class Fox: AuukiCreature
    {
        public Fox(Vector2 position) : base(position, new Vector2(1f,1f), Assets.GetTexture("rat")) { 
        
        }
    }
}
