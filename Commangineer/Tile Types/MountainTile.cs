using Microsoft.Xna.Framework;

namespace Commangineer.Tile_Types
{
    public class MountainTile : Tile
    {
        public MountainTile(Point point) : base(Assets.GetTexture("mountain"), point, 0f)
        {
            isSolid = true;
        }
    }
}