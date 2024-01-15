using Microsoft.Xna.Framework;

namespace Commangineer
{
    public interface Target
    {
        public void Damage(int damage);

        public bool Alive { get; }
        public Vector2 CenterPosition { get; }
    }
}