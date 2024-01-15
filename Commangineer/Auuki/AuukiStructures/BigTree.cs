using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer.Auuki.AuukiStructures
{
    /// <summary>
    /// A Auuki Big Tree
    /// </summary>
    internal class BigTree : AuukiStructure
    {
        // Creates a Auuki Structure of its type
        public BigTree(Point spawnPosition) : base(spawnPosition, new Point(3, 3), Assets.GetTexture("tree"), 2, 100)
        {

        }
    }
}
