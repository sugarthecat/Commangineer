using Microsoft.Xna.Framework;

namespace Commangineer.Auuki.AuukiCreatures
{
    /// <summary>
    /// A Auuki bear
    /// </summary>
    public class Bear : AuukiCreature
    {
        // Creates a new Auuki creature of its type
        public Bear(Vector2 position) : base(position, new Vector2(3, 3), Assets.GetTexture("bear"), 500, 200)
        {
            speed = 5f;
            damage = 400;
            attackTime = 0.3f;
        }
    }
}