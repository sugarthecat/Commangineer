using Commangineer.Floor_Auuki_types;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer.Tile_Types
{
    /// <summary>
    /// A deep water tile
    /// </summary>
    internal class DeepWaterTile : Tile
    {
        // Constructs a new deep water tile
        public DeepWaterTile(Point point) : base(Assets.GetTexture("deepwater"), point, 0f)
        {
            isSolid = true;
        }
    }
}
