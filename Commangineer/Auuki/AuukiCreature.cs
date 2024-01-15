using Commangineer.Units;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Commangineer.Auuki
{
    /// <summary>
    /// Different modes the Auuki AI can be in
    /// </summary>
    public enum AuukiAiMode
    {
        Wander,
        Attack
    }

    /// <summary>
    /// A Auuki Creature
    /// </summary>
    public class AuukiCreature : RotatableTexturedObject, AuukiTarget
    {
        protected float speed = 1f;
        protected float direction = 0f;
        protected int damage = 0;
        protected float attackTime = 1f;
        private float attackProgress = 0;
        private float wanderDistanceLeft = 0f;
        private int health;
        private Vector2 position;
        private AuukiAiMode behavior = AuukiAiMode.Wander;
        private Texture2D texture;
        private Vector2 size;

        private List<Point> path = new List<Point>();
        private PlayerTarget aggroUnit;

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
        /// Damages the Auuki Creature from a attacking unit
        /// </summary>
        /// <param name="damage">The amount of damage to deal</param>
        /// <param name="damageDealer">The unit attacking</param>
        public void Damage(int damage, Unit damageDealer)
        {
            health -= damage;
            if (aggroUnit == null)
            {
                aggroUnit = damageDealer;
            }
        }

        /// <summary>
        /// Updates the Auuki Creature
        /// </summary>
        /// <param name="deltaTime">The time since the last update</param>
        /// <param name="level">The current level</param>
        public void Update(float deltaTime, Level level)
        {
            attackProgress += deltaTime;
            if (attackProgress > attackTime)
            {
                attackProgress = attackTime;
            }
            if (aggroUnit != null && !aggroUnit.Alive)
            {
                aggroUnit = null;
            }
            if (behavior == AuukiAiMode.Attack || aggroUnit != null)
            {
                if (aggroUnit == null)
                {
                    aggroUnit = level.GetUnitTarget(position, 20);
                }
                if (path.Count == 0)
                {
                    path = Pathfinding.FindPath(position.ToPoint(), aggroUnit.CenterPosition.ToPoint(), new Point((int)Math.Ceiling(Size.X), (int)Math.Ceiling(Size.Y)));
                }

                if (path.Count > 0 || aggroUnit != null)
                {
                    Vector2 nextDestination = Vector2.Zero;
                    if (path.Count > 0)
                    {
                        nextDestination = path[0].ToVector2();
                    }
                    else
                    {
                        nextDestination = aggroUnit.CenterPosition;
                    }

                    Vector2 deltaPosition = nextDestination - position;
                    if (deltaPosition.Length() < speed * deltaTime)
                    {
                        position = nextDestination;
                        if (path.Count > 0)
                        {
                            path.RemoveAt(0);
                        }
                        if(attackProgress == attackTime)
                        {
                            Attack(aggroUnit);
                        }
                    }
                    else
                    {
                        direction = (float)Math.Atan2(deltaPosition.Y, deltaPosition.X);
                    }
                }
            }
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
        /// Makes the Auuki creature attack a player's unit
        /// </summary>
        /// <param name="target">The target to attack</param>
        private void Attack(PlayerTarget target)
        {
            target.Damage(damage);
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
        /// Gets/Sets the Auuki creature's ai mode
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
        /// Gets the position of the Auuki creature
        /// </summary>
        public Vector2 Position
        {
            get
            {
                return position;
            }
        }

        /// <summary>
        /// Gets the center position of the Auuki creature
        /// </summary>
        public Vector2 CenterPosition
        {
            get
            {
                return position + size / 2;
            }
        }
    }
}