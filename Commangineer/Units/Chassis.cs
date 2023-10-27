using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer.Units
{
    internal class Chassis
    {
        private string name;
        private int speed;
        private int armour;
        private int health;
        private int steam;
        private int size;
        private Unit.turretSize[] turretSizes;

        public Chassis(string name, int speed, int armour, int health, int steam, int size, Unit.turretSize[] turretSizes)
        {
            this.name = name;
            this.speed = speed;
            this.armour = armour;
            this.health = health;
            this.steam = steam;
            this.size = size;
            this.turretSizes = turretSizes;
        }

        public string GetName()
        {
            return name;
        }

        public int GetSpeed()
        {
            return speed;
        }

        public int GetArmour()
        {
            return armour;
        }

        public int GetHealth()
        {
            return health;
        }

        public int GetSteam()
        {
            return steam;
        }

        public int GetSize()
        {
            return size;
        }

        public Unit.turretSize[] GetTurrets()
        {
            return turretSizes;
        }


    }
}
