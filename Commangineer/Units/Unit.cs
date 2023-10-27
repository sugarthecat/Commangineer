using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer.Units
{
    internal class Unit
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
        private Slot[] weapons;
        private Chassis chassis;
        private Material chassisMaterial;
        private Engine engine;
        private Material engineMaterial;

        enum specialStats
        {
                
        }

        public Unit(string name, Chassis chassis, Material chassisMaterial, Engine engine, Material engineMaterial)
        {
            int weight = 0;
            int horse = 0;
            double speed;

            this.name = name;
            this.chassis = chassis;
            this.chassisMaterial = chassisMaterial;

            this.weapons = new Slot[chassis.GetTurrets().Length];

            for (int i = 0; i < chassis.GetTurrets().Length; i++)
            {
                this.weapons[i] = new Slot(chassis.GetTurrets()[i]);
            }

            this.engine = engine;
            this.engineMaterial = engineMaterial;

            weight += chassis.GetSize() * chassisMaterial.GetWeight();
            weight += engine.GetSize() * engineMaterial.GetWeight();

            horse += engine.GetHorsepower() * engineMaterial.GetStrength();

            speed = (System.Math.Pow((double)horse/(double)weight, (1.0 / 3.0)));

            if(speed > engine.GetSpeed() * engineMaterial.GetWorkability())
            {
                speed = engine.GetSpeed() * engineMaterial.GetWorkability();
            }

            this.speed = speed;
            this.maxHealth = chassis.GetHealth() * chassisMaterial.GetStrength();
            this.health = this.maxHealth;
            this.armour = chassis.GetArmour() * (chassisMaterial.GetWorkability()/ 10);

        }

        public void Update(double time)
        {
            foreach (Slot w in weapons)
            {
                w.Update(time);
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
    }
}
