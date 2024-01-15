using Commangineer.Floor_Auuki_types;
using Microsoft.Xna.Framework;

namespace Commangineer.Tile_Types
{
    public class DirtTile : Tile
    {
        public DirtTile(Point point) : base(Assets.GetTexture("dirt"), point, 0.005f)
        {
        }

        public override void InfectWithAuuki()
        {
            AuukiTile = new WoodlandFloorAuuki(PointPosition);
        }

        //infectWithAuuki
    }
}