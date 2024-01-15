using Commangineer.Floor_Auuki_types;
using Microsoft.Xna.Framework;

namespace Commangineer.Tile_Types
{
    public class WaterTile : Tile
    {
        public WaterTile(Point point) : base(Assets.GetTexture("water"), point, 0.001f)
        {
            isSolid = true;
        }

        public override void InfectWithAuuki()
        {
            AuukiTile = new ShallowWaterAuuki(PointPosition);
        }
    }
}