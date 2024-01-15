using Commangineer.Units;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Commangineer.Auuki
{
    public enum AuukiAiMode
    {
        Wander,
        Attack
    }

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

        public AuukiCreature(Vector2 position)
        {
            this.position = new Vector2(position.X - 0.5f, position.Y - 0.5f);
            size = new Vector2(1f, 1f);
            texture = Assets.GetTexture("default");
        }

        public void Damage(int damage)
        {
            health -= damage;
        }

        public void Damage(int damage, Unit damageDealer)
        {
            health -= damage;
            if (aggroUnit == null)
            {
                aggroUnit = damageDealer;
            }
        }

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
        private void Attack(PlayerTarget target)
        {
            target.Damage(damage);
        }
        public Texture2D GetTexture()
        {
            return texture;
        }

        public float Angle
        {
            get
            {
                return direction;
            }
        }
        public AuukiAiMode AiMode
        {
            set
            {
                behavior = value;
            }
        }
        public Vector2 Size
        {
            get
            {
                return size;
            }
        }

        public bool Alive
        {
            get
            {
                return health >= 0;
            }
        }

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

        public Vector2 Position
        {
            get
            {
                return position;
            }
        }

        public Vector2 CenterPosition
        {
            get
            {
                return position + size / 2;
            }
        }
    }
}