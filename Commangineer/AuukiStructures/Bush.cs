using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer.AuukiStructures
{
    internal class Bush : AuukiStructure
    {
        public Bush(Point spawnPosition) : base(spawnPosition,new Point(1,1),Assets.GetTexture("tree"),0)
        {

        }
    }
}
