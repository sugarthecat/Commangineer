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
    /// A water tile
    /// </summary>
    internal class WaterTile : Tile
    {
        // Constructs a new water tile
        public WaterTile(Point point) : base(Assets.GetTexture("water"), point, 0.001f)
        {
            isSolid = true;
        }
        /// <summary>
        /// Infects the tile with Auuki
        /// </summary>
        public override void InfectWithAuuki()
        {
            AuukiTile = new ShallowWaterAuuki(PointPosition);
        }
    }
}
