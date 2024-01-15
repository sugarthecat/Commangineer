using Microsoft.Xna.Framework;

namespace Commangineer.Auuki.AuukiCreatures
{
    public class Bear : AuukiCreature
    {
        public Bear(Vector2 position) : base(position, new Vector2(3, 3), Assets.GetTexture("bear"), 2000)
        {
            speed = 5f;
            damage = 400;
            attackTime = 0.3f;
        }
    }
}