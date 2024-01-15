using Microsoft.Xna.Framework;

namespace Commangineer.Tile_Types
{
    public class StoneTile : Tile
    {
        public StoneTile(Point point) : base(Assets.GetTexture("stone"), point, 0.002f)
        {
        }
    }
}