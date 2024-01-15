using Microsoft.Xna.Framework;

namespace Commangineer.Auuki.AuukiStructures
{
    public class Bush : AuukiStructure
    {
        public Bush(Point spawnPosition) : base(spawnPosition, new Point(1, 1), Assets.GetTexture("tree"), 0, 5)
        {
        }
    }
}