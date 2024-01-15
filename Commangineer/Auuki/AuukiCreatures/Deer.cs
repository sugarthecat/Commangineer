using Microsoft.Xna.Framework;

namespace Commangineer.Auuki.AuukiCreatures
{
    /// <summary>
    /// A Auuki deer
    /// </summary>
    public class Deer : AuukiCreature
    {
        // Creates a new Auuki creature of its type
        public Deer(Vector2 position) : base(position, new Vector2(1.5f, 1.5f), Assets.GetTexture("deer"), 100)
        {
            speed = 3f;
        }
    }
}