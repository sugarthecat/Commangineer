using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer.AuukiCreatures
{
    internal class Bear: AuukiCreature
    {
        public Bear(Vector2 position) : base(position, new Vector2(0.5f,0.5f), Assets.GetTexture("rat")) { 
        
        }
    }
}
