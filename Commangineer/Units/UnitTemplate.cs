using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Commangineer.Units
{
    public class UnitTemplate
    {
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

        public Vector2 Size
        {
            get
            {
                return chassis.Size;
            }
        }

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

        public Chassis Chassis
        {
            get
            {
                return chassis.Clone();
            }
        }

        public Engine Engine
        {
            get
            {
                return engine.Clone();
            }
        }

        public void Damage(int d)
        {
            d -= armour;
            if (d <= 0)
            {
                d = 1;
            }
            health -= d;
        }

        public Texture2D GetTexture()
        {
            return Assets.GetTexture("wood");
        }
    }
}