using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer.Auuki.AuukiStructures
{
    /// <summary>
    /// A Auuki bush
    /// </summary>
    internal class Bush : AuukiStructure
    {
        // Creates a Auuki Structure of its type
        public Bush(Point spawnPosition) : base(spawnPosition, new Point(1, 1), Assets.GetTexture("tree"), 0, 5)
        {

        }
    }
}
