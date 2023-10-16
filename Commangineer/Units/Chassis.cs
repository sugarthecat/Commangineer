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
        private Unit.turretSize[] turretSizes;

        public Chassis(string name, int speed, int armour, int health, int steam, Unit.turretSize[] turretSizes)
        {
            this.name = name;
            this.speed = speed;
            this.armour = armour;
            this.health = health;
            this.steam = steam;
            this.turretSizes = turretSizes;
        }
    }
}
