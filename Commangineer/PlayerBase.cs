using Commangineer.Units;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Commangineer
{
    public class PlayerBase : TexturedObject, PlayerTarget
    {
        private Vector2 position;
        private int health;
        private Texture2D texture;

        public PlayerBase(Point position)
        {
            this.position = new Vector2(position.X, position.Y);
            texture = Assets.GetTexture("playerBase");
            health = 400;
        }
        public void Damage(int damage)
        {
            health -= damage;
        }
        public bool Alive
        {
            get
            {
                return this.health > 0;
            }
        }
        public Vector2 Size
        {
            get { return new Vector2(4f, 4f); }
        }

        public Vector2 Position
        {
            get { return position; }
        }
        public Vector2 CenterPosition
        {
            get { return position+Size/2; }
        }

        public Point PointPosition
        {
            get { return position.ToPoint(); }
        }

        public Texture2D GetTexture()
        {
            return texture;
        }

        public bool ContainsPoint(Vector2 point)
        {
            Rectangle bounds = new Rectangle(
                (int)position.X,
                (int)position.Y,
                (int)Size.X,
                (int)Size.Y
                );
            return bounds.Contains(point);
        }
    }
}