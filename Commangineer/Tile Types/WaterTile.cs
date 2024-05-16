using Commangineer.Floor_Auuki_types;
using Microsoft.Xna.Framework;

namespace Commangineer.Tile_Types
{
    /// <summary>
    /// A water tile
    /// </summary>
    public class WaterTile : Tile
    {
        // Constructs a new water tile
        public WaterTile(Point point) : base(Assets.GetTexture("water"), point, 0.004f)
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