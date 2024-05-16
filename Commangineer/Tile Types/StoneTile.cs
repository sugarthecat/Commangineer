using Commangineer.Floor_Auuki_types;
using Microsoft.Xna.Framework;

namespace Commangineer.Tile_Types
{
    /// <summary>
    /// A stone tile
    /// </summary>
    public class StoneTile : Tile
    {
        // Constructs a new stone tile
        public StoneTile(Point point) : base(Assets.GetTexture("stone"), point, 0.008f)
        {
        }

        /// <summary>
        /// Infects the tile with Auuki
        /// </summary>
        public override void InfectWithAuuki()
        {
            AuukiTile = new RockyFloorAuuki(PointPosition);
        }
    }
}