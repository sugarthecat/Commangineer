using Microsoft.Xna.Framework;

namespace Commangineer.Tile_Types
{
    /// <summary>
    /// A mountain tile
    /// </summary>
    public class MountainTile : Tile
    {
        // Constructs a new mountain tile
        public MountainTile(Point point) : base(Assets.GetTexture("mountain"), point, 0f)
        {
            isSolid = true;
        }
    }
}