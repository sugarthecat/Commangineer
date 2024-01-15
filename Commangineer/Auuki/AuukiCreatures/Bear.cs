using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer.Auuki.AuukiCreatures
{
    /// <summary>
    /// A Auuki bear
    /// </summary>
    internal class Bear : AuukiCreature
    {
        // Creates a new Auuki creature of its type
        public Bear(Vector2 position) : base(position, new Vector2(3,3), Assets.GetTexture("rat"), 20)
        {

        }
    }
}
