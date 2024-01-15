using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer.Auuki
{
    /// <summary>
    /// Different modes the Auuki AI can be in
    /// </summary>
    public enum AuukiAiMode
    {
        Wander

    }

    /// <summary>
    /// A Auuki Creature
    /// </summary>
    public class AuukiCreature : RotatableTexturedObject, AuukiTarget
    {
        protected float speed = 1f;
        protected float direction = 0f;
        private float wanderDistanceLeft = 0f;
        private int health;
        Vector2 position;
        AuukiAiMode behavior = AuukiAiMode.Wander;
        Texture2D texture;
        Vector2 size;
        // Creates a new Auuki creature
        public AuukiCreature(Vector2 position, Vector2 size, Texture2D texture, int health)
        {
            Random genRandom = new Random();
            direction = (float)(genRandom.NextDouble() * 2 * Math.PI);
            wanderDistanceLeft = (float)genRandom.NextDouble() * 10;
            this.position = new Vector2(position.X - size.X / 2, position.Y - size.Y / 2);
            this.health = health;
            this.size = size;
            this.texture = texture;
        }
        // Creates a new Auuki creature with just a position
        public AuukiCreature(Vector2 position)
        {
            this.position = new Vector2(position.X - 0.5f, position.Y - 0.5f);
            size = new Vector2(1f, 1f);
            texture = Assets.GetTexture("default");
        }
        /// <summary>
        /// Damages the Auuki Creature
        /// </summary>
        /// <param name="damage">The amount of damage to deal</param>
        public void Damage(int damage)
        {
            health -= damage;
        }
        /// <summary>
        /// Updates the Auuki Creature
        /// </summary>
        /// <param name="deltaTime">The time since the last update</param>
        /// <param name="level">The current level</param>
        public void Update(float deltaTime, Level level)
        {
            float prevX = position.X;
            float prevY = position.Y;
            position.X += speed * deltaTime * (float)Math.Cos(direction);
            position.Y += speed * deltaTime * (float)Math.Sin(direction);
            if (level.Collides(this))
            {
                position.X = prevX;
                position.Y = prevY;
                if (behavior == AuukiAiMode.Wander)
                {
                    wanderDistanceLeft = -1;
                }
            }
            if (behavior == AuukiAiMode.Wander)
            {
                wanderDistanceLeft -= speed * deltaTime;
                if (wanderDistanceLeft <= 0)
                {
                    Random genRandom = new Random();
                    direction = (float)(genRandom.NextDouble() * 2 * Math.PI);
                    wanderDistanceLeft = (float)genRandom.NextDouble() * 10;
                }
            }
        }
        /// <summary>
        /// Gets the Auuki creature's texture
        /// </summary>
        /// <returns>Auuki creature's texture</returns>
        public Texture2D GetTexture()
        {
            return texture;
        }
        /// <summary>
        /// Gets the Auuki creature's angle
        /// </summary>
        public float Angle
        {
            get
            {
                return direction;
            }
        }
        /// <summary>
        /// Gets the Auuki creature's size
        /// </summary>
        public Vector2 Size
        {
            get
            {
                return size;
            }
        }
        /// <summary>
        /// Checks if the Auuki creature is alive
        /// </summary>
        public bool Alive
        {
            get
            {
                return health >= 0;
            }
        }
        /// <summary>
        /// Gets the Auuki creature's ai mode
        /// </summary>
        public AuukiAiMode Behavior
        {
            get
            {
                return behavior;
            }
            set
            {
                behavior = value;
            }
        }
        /// <summary>
        /// Gets the Auuki creature's position
        /// </summary>
        public Vector2 Position
        {

            get
            {
                return position;
            }
        }
        /// <summary>
        /// Gets the Auuki creature's center position
        /// </summary>
        public Vector2 CenterPosition
        {

            get
            {
                return position+size/2;
            }
        }
    }
}
