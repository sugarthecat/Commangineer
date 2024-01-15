using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer.Auuki.AuukiCreatures
{
    /// <summary>
    /// A Auuki rat
    /// </summary>
    internal class Rat : AuukiCreature
    {
        // Creates a new Auuki creature of its type
        public Rat(Vector2 position) : base(position, new Vector2(1f, 1f), Assets.GetTexture("rat"), 20)
        {

        }
    }
}
