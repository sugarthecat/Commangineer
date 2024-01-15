using Commangineer.Units;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Commangineer
{
    /// <summary>
    /// Represents the base of the player
    /// </summary>
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
        /// <summary>
        /// Damages the player base
        /// </summary>
        /// <param name="damage"></param>
        public void Damage(int damage)
        {
            health -= damage;
        }
        /// <summary>
        /// Gets if the player base is alive
        /// </summary>
        public bool Alive
        {
            get
            {
                return this.health > 0;
            }
        }
        /// <summary>
        /// Gets the player base size
        /// </summary>
        public Vector2 Size
        {
            get { return new Vector2(4f, 4f); }
        }

        /// <summary>
        /// Gets the player base position
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
        }

        /// <summary>
        /// Gets the player base center position
        /// </summary>
        public Vector2 CenterPosition
        {
            get { return position+Size/2; }
        }

        /// <summary>
        /// Gets the player base position as a point
        /// </summary>
        public Point PointPosition
        {
            get { return position.ToPoint(); }
        }

        /// <summary>
        /// Gets the texture of the player base
        /// </summary>
        public Texture2D GetTexture()
        {
            return texture;
        }

        /// <summary>
        /// Checks if the player base contains a point
        /// </summary>
        /// <param name="point">The point to check</param>
        /// <returns>If it contains the point</returns>
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