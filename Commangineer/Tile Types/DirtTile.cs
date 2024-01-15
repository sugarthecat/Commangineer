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
    /// A dirt tile
    /// </summary>
    internal class DirtTile : Tile
    {
        // Constructs a new dirt tile
        public DirtTile(Point point) : base(Assets.GetTexture("dirt"), point, 0.005f)
        {

        }
        /// <summary>
        /// Infects the tile with Auuki
        /// </summary>
        public override void InfectWithAuuki()
        {
            AuukiTile = new WoodlandFloorAuuki(PointPosition);
        }
    }
}
