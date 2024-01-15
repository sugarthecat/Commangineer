using Microsoft.Xna.Framework;

namespace Commangineer.Tile_Types
{
    /// <summary>
    /// A deep water tile
    /// </summary>
    public class DeepWaterTile : Tile
    {
        // Constructs a new deep water tile
        public DeepWaterTile(Point point) : base(Assets.GetTexture("deepwater"), point, 0f)
        {
            isSolid = true;
        }
    }
}