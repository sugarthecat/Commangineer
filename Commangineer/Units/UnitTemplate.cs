using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Commangineer.Units
{
    /// <summary>
    /// A template for a unit
    /// </summary>
    public class UnitTemplate
    {
        /// <summary>
        /// The size of a unit's turret
        /// </summary>
        public enum turretSize
        {
            small,
            medium,
            big
        }

        private int health;
        private int maxHealth;
        private int armour;
        private double speed;
        private Chassis chassis;
        private Engine engine;

        // Creates a new unit template
        public UnitTemplate(Chassis chassis, Engine engine)
        {
            int weight = chassis.Weight + engine.Weight;
            double speed;

            this.chassis = chassis;
            this.engine = engine;
            int horse = engine.Horsepower;

            speed = Math.Pow((double)horse / weight, 0.3d);

            if (speed > engine.Speed)
            {
                speed = engine.Speed;
            }

            this.speed = speed;
            this.maxHealth = chassis.Health;
            this.health = this.maxHealth;
            this.armour = chassis.Armour;
        }

        /// <summary>
        /// Gets the cost for a material
        /// </summary>
        public MaterialBalance MaterialCost
        {
            get
            {
                MaterialBalance materialBalance = new MaterialBalance();
                materialBalance += engine.Cost;
                materialBalance += chassis.Cost;
                return materialBalance;
            }
        }

        /// <summary>
        /// Gets the size
        /// </summary>
        public Vector2 Size
        {
            get
            {
                return chassis.Size;
            }
        }

        // Getters for the unit
        public int Health
        { get { return health; } }
        public int MaxHealth
        { get { return maxHealth; } }
        public int Armour
        { get { return armour; } }
        public double Speed
        { get { return speed; } }
        public Slot[] Weapons
        { get { return chassis.Weapons; } }

        /// <summary>
        /// Gets the Chassis
        /// </summary>
        public Chassis Chassis
        {
            get
            {
                return chassis.Clone();
            }
        }

        /// <summary>
        /// Gets the Engine
        /// </summary>
        public Engine Engine
        {
            get
            {
                return engine.Clone();
            }
        }

        /// <summary>
        /// Damages health
        /// </summary>
        /// <param name="d">Amount of damage</param>
        public void Damage(int d)
        {
            d -= armour;
            if (d <= 0)
            {
                d = 1;
            }
            health -= d;
        }

        /// <summary>
        /// Gets the current texture
        /// </summary>
        /// <returns>The current texture</returns>
        public Texture2D GetTexture()
        {
            return Assets.GetTexture("wood");
        }
    }
}