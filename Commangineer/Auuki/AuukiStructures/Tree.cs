using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer.Auuki.AuukiStructures
{
    internal class Tree : AuukiStructure
    {
        public Tree(Point spawnPosition) : base(spawnPosition, new Point(2, 2), Assets.GetTexture("tree"), 1, 5)
        {

        }
    }
}
