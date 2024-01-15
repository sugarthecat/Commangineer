using Microsoft.Xna.Framework;

namespace Commangineer.Auuki.AuukiCreatures
{
    public class Fox : AuukiCreature
    {
        public Fox(Vector2 position) : base(position, new Vector2(2f, 2f), Assets.GetTexture("fox"), 20)
        {
            speed = 2f;
        }
    }
}