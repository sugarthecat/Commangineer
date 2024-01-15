using Microsoft.Xna.Framework;

namespace Commangineer.Auuki.AuukiStructures
{
    public class BigTree : AuukiStructure
    {
        public BigTree(Point spawnPosition) : base(spawnPosition, new Point(3, 3), Assets.GetTexture("tree"), 2, 100)
        {
        }
    }
}