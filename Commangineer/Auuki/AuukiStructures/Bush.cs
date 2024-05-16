using Microsoft.Xna.Framework;

namespace Commangineer.Auuki.AuukiStructures
{
    /// <summary>
    /// A Auuki bush
    /// </summary>
    public class Bush : AuukiStructure
    {
        // Creates a Auuki Structure of its type
        public Bush(Point spawnPosition) : base(spawnPosition, new Point(1, 1), Assets.GetTexture("tree"), 0, 80)
        {
        }
    }
}