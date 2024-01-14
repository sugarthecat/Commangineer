using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
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
        private Slot[] turretSizes;

        public Chassis(string name, int speed, int armour, int health, int size, Slot[] turretSizes)
        {
            this.name = name;
            this.speed = speed;
            this.armour = armour;
            this.health = health;
            this.size = size;
            this.turretSizes = turretSizes;
        }
        public string Name { get { return name; } }
        public int Speed {  get { return speed; } }
        public int Armour { get { return armour; } }
        public int Health { get { return health; } }
        public int Volume { get { return size; } }
        public Slot[] Weapons { 
            get { 

                return turretSizes; 
            } 
        }
        public Vector2 Size
        {
            get
            {
                return new Vector2(size, size);
            }
        }
        public Slot[] GetTurrets()
        {
            return turretSizes;
        }

        public Chassis Clone()
        {
            Chassis newChassis = new Chassis(name, speed, armour, health, size, new Slot[turretSizes.Length]);
            for(int i = 0; i < turretSizes.Length; i++)
            {
                newChassis.turretSizes[i] = new Slot(turretSizes[i]);
            }
            return newChassis;
        }
    }
}
