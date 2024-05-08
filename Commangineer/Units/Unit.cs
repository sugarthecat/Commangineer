using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Drawing;

namespace Commangineer.Units
{
    /// <summary>
    /// A unit in game
    /// </summary>
    public class Unit : RotatableTexturedObject, PlayerTarget
    {
        private float angle = 0;
        private int health;
        private int armour;
        private double speed;
        private Slot[] weapons;
        private Chassis chassis;

        private Vector2 position;
        private Vector2 goal;

        /// <summary>
        /// Gets the angle of the unit
        /// </summary>
        public float Angle
        {
            get
            {
                return angle;
            }
        }

        /// <summary>
        /// Gets if the unit is alive
        /// </summary>
        public bool Alive
        {
            get
            {
                return health > 0;
            }
        }

        // Constructs a new unit
        public Unit(UnitTemplate template, Vector2 spawnPosition)
        {
            health = template.Health;
            armour = template.Armour;
            speed = template.Speed;
            chassis = template.Chassis.Clone();
            weapons = new Slot[template.Weapons.Length];
            for (int i = 0; i < template.Weapons.Length; i++)
            {
                weapons[i] = new Slot(template.Weapons[i]);
            }
            goal = spawnPosition;
            position = spawnPosition - Size / 2;
        }

        /// <summary>
        /// Updates the unit
        /// </summary>
        /// <param name="deltaTime">Time since last update</param>
        /// <param name="level">The current level</param>
        public void Update(float deltaTime, Level level)
        {
            level.DestroyTilesUnderUnit(this);
            foreach (Slot w in weapons)
            {
                w.Update(deltaTime, position + Size / 2, Angle, level, this);
            }
            Vector2 deltaPosition = position - goal;
            if (deltaPosition.Length() > deltaTime * (float)speed)
            {
                angle = (float)Math.Atan2(deltaPosition.Y, deltaPosition.X);
            }
            Vector2 previousPosition = position;
            float length = deltaPosition.Length();
            if (length < deltaTime * (float)speed)
            {
                position = goal;
            }
            else
            {
                deltaPosition *= deltaTime * (float)speed / length;
                position = position - deltaPosition;
            }

            if (level.Collides(this))
            {
                position = previousPosition;
            }
        }

        /// <summary>
        /// Checks if the unit is within a boundary
        /// </summary>
        /// <param name="other">The boundary</param>
        /// <returns>If the unit is withing a boundary</returns>
        public bool Intersects(RectangleF other)
        {
            return Bounds.IntersectsWith(other);
        }

        /// <summary>
        /// Checks if the unit is colliding with another unit
        /// </summary>
        /// <param name="other">The other unit</param>
        /// <returns>If the unit is colliding</returns>
        public bool Collides(Unit other)
        {
            Vector2 deltaPosition = CenterPosition - other.CenterPosition;
            return (Size.X + other.Size.X) / 2 > deltaPosition.Length();
        }

        /// <summary>
        /// Gets the bounds of the unit
        /// </summary>
        public RectangleF Bounds
        {
            get
            {
                return new RectangleF(Position.X, position.Y, Size.X, Size.Y);
            }
        }

        /// <summary>
        /// Sets the unit's goal
        /// </summary>
        public Vector2 Goal
        {
            set
            {
                goal = value - Size/2;
            }
        }

        /// <summary>
        /// Gets the unit's size
        /// </summary>
        public Vector2 Size
        {
            get
            {
                return chassis.Size;
            }
        }

        /// <summary>
        /// Gets the unit's position
        /// </summary>
        public Vector2 Position
        {
            get
            {
                return position;
            }
        }

        /// <summary>
        /// Gets the unit's center position
        /// </summary>
        public Vector2 CenterPosition
        {
            get
            {
                return position + (Size / 2);
            }
        }

        /// <summary>
        /// Damages the unit
        /// </summary>
        /// <param name="damage">The amount of damage to deal</param>
        public void Damage(int damage)
        {
            damage -= armour;
            if (damage <= 0)
            {
                damage = 1;
            }
            health -= damage;
        }

        /// <summary>
        /// Gets the unit's texture
        /// </summary>
        /// <returns>The unit's texture</returns>
        public Texture2D GetTexture()
        {
            return chassis.Texture;
        }

        /// <summary>
        /// Draws the unit
        /// </summary>
        /// <param name="spriteBatch">The sprite batch to be drawn with</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            Camera.Draw(spriteBatch, this);
            for (int i = 0; i < weapons.Length; i++)
            {
                weapons[i].Draw(spriteBatch);
            }
        }

        /// <summary>
        /// Draws the bullet frames of the unit
        /// </summary>
        /// <param name="spriteBatch">The sprite batch to be drawn with</param>
        public void DrawBulletFrames(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < weapons.Length; i++)
            {
                weapons[i].DrawBulletFrames(spriteBatch);
            }
        }

        /// <summary>
        /// Draws the selected unit
        /// </summary>
        /// <param name="spriteBatch">The sprite batch to be drawn with</param>
        public void DrawSelection(SpriteBatch spriteBatch)
        {
            Camera.DrawProjected(spriteBatch, Position, Size, Assets.GetImage("selectedUnit"));
        }
    }
}