using Microsoft.Xna.Framework;

namespace Commangineer.Auuki.AuukiStructures
{
    public class Tree : AuukiStructure
    {
        public Tree(Point spawnPosition) : base(spawnPosition, new Point(2, 2), Assets.GetTexture("tree"), 1, 5)
        {
        }
    }
}