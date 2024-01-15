using Microsoft.Xna.Framework;

namespace Commangineer.Auuki.AuukiStructures
{
    /// <summary>
    /// A Auuki Big Tree
    /// </summary>
    public class BigTree : AuukiStructure
    {
        // Creates a Auuki Structure of its type
        public BigTree(Point spawnPosition) : base(spawnPosition, new Point(3, 3), Assets.GetTexture("tree"), 2, 100)
        {
        }
    }
}