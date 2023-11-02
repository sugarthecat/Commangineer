using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer.Tile_Types
{
    internal class StoneTile : Tile
    {
        public StoneTile(Point point) : base(Assets.GetTexture("stone"), point, 0.01f)
        {

        }
    }
}
