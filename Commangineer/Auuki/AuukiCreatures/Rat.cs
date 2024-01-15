using Microsoft.Xna.Framework;

namespace Commangineer.Auuki.AuukiCreatures
{
    public class Rat : AuukiCreature
    {
        public Rat(Vector2 position) : base(position, new Vector2(1f, 1f), Assets.GetTexture("rat"), 20)
        {
            damage = 5;
        }
    }
}