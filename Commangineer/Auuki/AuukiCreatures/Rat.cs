using Microsoft.Xna.Framework;

namespace Commangineer.Auuki.AuukiCreatures
{
    /// <summary>
    /// A Auuki rat
    /// </summary>
    public class Rat : AuukiCreature
    {
        // Creates a new Auuki creature of its type
        public Rat(Vector2 position) : base(position, new Vector2(1f, 1f), Assets.GetTexture("rat"), 20)
        {
            damage = 5;
        }
    }
}