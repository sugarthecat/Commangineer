using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer.Tile_Types
{
    /// <summary>
    /// A stone tile
    /// </summary>
    internal class StoneTile : Tile
    {
        // Constructs a new stone tile
        public StoneTile(Point point) : base(Assets.GetTexture("stone"), point, 0.002f)
        {

        }
    }
}
