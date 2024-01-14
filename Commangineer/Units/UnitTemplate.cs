using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Commangineer.Units
{
    internal class UnitTemplate
    {
        public enum turretSize
        {
            small,
            medium,
            big
        }
        private string name;
        private int health;
        private int maxHealth;
        private int armour;
        private double speed;
        private Chassis chassis;
        private Material chassisMaterial;
        private Engine engine;
        private Material engineMaterial;

        public UnitTemplate(string name, Chassis chassis, Material chassisMaterial, Engine engine, Material engineMaterial)
        {
            int weight = 0;
            int horse = 0;
            double speed;

            this.name = name;
            this.chassis = chassis;
            this.chassisMaterial = chassisMaterial;


            this.engine = engine;
            this.engineMaterial = engineMaterial;

            weight += chassis.Volume * chassisMaterial.Weight;
            weight += engine.Size * engineMaterial.Weight;

            horse += engine.Horsepower * engineMaterial.Strength;

            speed = (System.Math.Pow((double)horse/(double)weight, (1.0 / 3.0)));

            if(speed > engine.Speed * engineMaterial.Workability)
            {
                speed = engine.Speed * engineMaterial.Workability;
            }

            this.speed = speed;
            this.maxHealth = chassis.Health * chassisMaterial.Strength;
            this.health = this.maxHealth;
            this.armour = chassis.Armour * (chassisMaterial.Workability/ 10);

        }

        public MaterialBalance MaterialCost
        {
            get
            {
                MaterialBalance materialBalance = new MaterialBalance();
                materialBalance[MaterialType.Scrap] = 4;
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
        public string Name {  get { return name; } }
        public int Health { get { return health; } }
        public int MaxHealth { get { return maxHealth; } }
        public int Armour {  get { return armour; } }
        public double Speed { get { return speed; } }
        public Slot[] Weapons { get { return chassis.Weapons; } }
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
            if(d <= 0)
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
