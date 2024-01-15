using Microsoft.Xna.Framework;

namespace Commangineer.Auuki.AuukiCreatures
{
    public class Deer : AuukiCreature
    {
        public Deer(Vector2 position) : base(position, new Vector2(1.5f, 1.5f), Assets.GetTexture("deer"), 100)
        {
            speed = 3f;
        }
    }
}