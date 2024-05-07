using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Commangineer.Units
{
    public enum TurretSize
    {
        Small,
        Medium,
        Big
    }
    /// <summary>
    /// A template for a unit
    /// </summary>
    public class UnitTemplate
    {
        /// <summary>
        /// The size of a unit's turret
        /// </summary>

        private int health;
        private int maxHealth;
        private int armour;
        private double speed;
        private Chassis chassis;
        private Engine engine;
        private Weapon[] weapons;

        public UnitTemplate(Chassis chassis, Engine engine, Weapon[] weapons)
        {
            int weight = chassis.Weight + engine.Weight;
            double speed;

            this.chassis = chassis.Clone();
            this.engine = engine.Clone();
            int horse = engine.Horsepower;

            speed = Math.Pow((double)horse / weight, 0.3d);

            if (speed > engine.Speed)
            {
                speed = engine.Speed;
            }
            this.weapons = new Weapon[chassis.Slots.Length];
            for (int i = 0; i < weapons.Length; i++)
            {
                this.weapons[i] = new Weapon(weapons[i]);
            }

            this.speed = speed;
            maxHealth = chassis.Health;
            health = maxHealth;
            armour = chassis.Armour;
        }

        public Slot[] Slots
        {
            get
            {
                return chassis.Slots;
            }
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
                for (int i = 0; i < weapons.Length; i++)
                {
                    materialBalance += weapons[i].Cost;
                }
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
        {
            get
            {
                Slot[] slots = new Slot[weapons.Length];
                for (int i = 0; i < chassis.Slots.Length; i++)
                {
                    slots[i] = new Slot(chassis.Slots[i]);
                    slots[i].AddWeapon(weapons[i]);
                }
                return slots;
            }
        }

        /// <summary>
        /// Gets the Chassis
        /// </summary>
        public Chassis Chassis
        {
            get
            {
                Chassis outChassis = chassis.Clone();
                for (int i = 0; i < weapons.Length; i++)
                {
                    outChassis.SetWeapon(i, weapons[i]);
                }
                return outChassis;
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