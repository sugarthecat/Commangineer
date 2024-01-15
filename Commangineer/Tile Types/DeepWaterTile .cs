using Microsoft.Xna.Framework;

namespace Commangineer.Tile_Types
{
    public class DeepWaterTile : Tile
    {
        public DeepWaterTile(Point point) : base(Assets.GetTexture("deepwater"), point, 0f)
        {
            isSolid = true;
        }
    }
}