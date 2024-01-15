using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer.Units
{
    /// <summary>
    /// The chassis of a unit
    /// </summary>
    internal class Chassis 
    {
        private string name;
        private int speed;
        private int armour;
        private int health;
        private int steam;
        private int size;
        private Slot[] turretSizes;

        // Constructs a new Chassis
        public Chassis(string name, int speed, int armour, int health, int size, Slot[] turretSizes)
        {
            this.name = name;
            this.speed = speed;
            this.armour = armour;
            this.health = health;
            this.size = size;
            this.turretSizes = turretSizes;
        }
        // Getters for the Chassis
        public string Name { get { return name; } }
        public int Speed {  get { return speed; } }
        public int Armour { get { return armour; } }
        public int Health { get { return health; } }
        public int Volume { get { return size; } }
        /// <summary>
        /// Gets the slots of the chassis
        /// </summary>
        public Slot[] Weapons { 
            get { 

                return turretSizes; 
            } 
        }
        /// <summary>
        /// Gets the size of the chassis
        /// </summary>
        public Vector2 Size
        {
            get
            {
                return new Vector2(size, size);
            }
        }
        /// <summary>
        /// Gets the slots of the chassis
        /// </summary>
        /// <returns>The slots of the chassis</returns>
        public Slot[] GetTurrets()
        {
            return turretSizes;
        }

        /// <summary>
        /// Clones the Chassis
        /// </summary>
        /// <returns>A clone of the Chassis</returns>
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
