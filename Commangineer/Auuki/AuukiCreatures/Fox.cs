using Microsoft.Xna.Framework;

namespace Commangineer.Auuki.AuukiCreatures
{
    /// <summary>
    /// A Auuki fox
    /// </summary>
    public class Fox : AuukiCreature
    {
        // Creates a new Auuki creature of its type
        public Fox(Vector2 position) : base(position, new Vector2(2f, 2f), Assets.GetTexture("fox"), 20)
        {
            speed = 2f;
        }
    }
}