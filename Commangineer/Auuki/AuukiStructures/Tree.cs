using Microsoft.Xna.Framework;

namespace Commangineer.Auuki.AuukiStructures
{
    /// <summary>
    /// A Auuki tree
    /// </summary>
    public class Tree : AuukiStructure
    {
        // Creates a Auuki Structure of its type
        public Tree(Point spawnPosition) : base(spawnPosition, new Point(2, 2), Assets.GetTexture("tree"), 1, 800)
        {

        }
    }
}