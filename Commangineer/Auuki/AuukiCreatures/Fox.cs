using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer.Auuki.AuukiCreatures
{
    /// <summary>
    /// A Auuki fox
    /// </summary>
    internal class Fox : AuukiCreature
    {
        // Creates a new Auuki creature of its type
        public Fox(Vector2 position) : base(position, new Vector2(2f, 2f), Assets.GetTexture("rat"), 20)
        {

        }
    }
}
