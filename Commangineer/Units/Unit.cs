using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using Point = Microsoft.Xna.Framework.Point;
namespace Commangineer.Units
{
    /// <summary>
    /// A unit in game
    /// </summary>
    internal class Unit : RotatableTexturedObject
    {
        /// <summary>
        /// The unit's turret size
        /// </summary>
        public enum turretSize
        {
            small,
            medium,
            big
        }

        private float angle = 0;
        private string name;
        private int health;
        private int maxHealth;
        private int armour;
        private double speed;
        private Slot[] weapons;
        private Chassis chassis;
        private Material chassisMaterial;
        private Engine engine;
        private Material engineMaterial;

        private Vector2 position;
        private Vector2 goal;
        private List<Point> path = new List<Point>();
        private float maxMove = 1;
        private float move = 1;
        private float maxRescan = 20;
        private float rescan = 0;

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

        public Unit(UnitTemplate template, Vector2 spawnPosition)
        {
            name = template.Name;
            health = template.Health;
            maxHealth = template.MaxHealth;
            armour = template.Armour;
            speed = template.Speed;
            chassis = template.Chassis;
            engine = template.Engine;
            weapons = template.Weapons;
            goal = spawnPosition- new Vector2(0,Size.Y);
            position = spawnPosition;
        }

        /// <summary>
        /// Gets the cost of a material
        /// </summary>
        public MaterialBalance MaterialCost
        {
            get
            {
                MaterialBalance materialBalance = new MaterialBalance();
                return materialBalance;
            }
        }

        /// <summary>
        /// Updates the unit
        /// </summary>
        /// <param name="deltaTime">Time since last update</param>
        /// <param name="level">The current level</param>
        public void Update(float deltaTime, Level level)
        {
            foreach (Slot w in weapons)
            {
                w.Update(deltaTime, position+Size/2, Angle, level);
            }
            Vector2 deltaPosition = position - goal;
            angle = (float)Math.Atan2(deltaPosition.Y, deltaPosition.X);
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
        /// Gets the bounds of the unit
        /// </summary>
        public RectangleF Bounds
        {
            get
            {
                return new RectangleF(Position.X,position.Y, Size.X,Size.Y);
            }
        }

        /// <summary>
        /// Sets the unit's goal
        /// </summary>
        public Vector2 Goal
        {
            set
            {
                goal = value;
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
            return Assets.GetTexture("wood");
        }

        /// <summary>
        /// Draws the unit
        /// </summary>
        /// <param name="spriteBatch">The sprite batch to be drawn with</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            Camera.Draw(spriteBatch, this);
            for(int i = 0; i<weapons.Length; i++)
            {
                weapons[i].Draw(spriteBatch);
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