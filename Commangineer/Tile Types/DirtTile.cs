using Commangineer.Floor_Auuki_types;
using Microsoft.Xna.Framework;

namespace Commangineer.Tile_Types
{
    /// <summary>
    /// A dirt tile
    /// </summary>
    public class DirtTile : Tile
    {
        // Constructs a new dirt tile
        public DirtTile(Point point) : base(Assets.GetTexture("dirt"), point, 0.015f)
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