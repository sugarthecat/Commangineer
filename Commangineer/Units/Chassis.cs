using Microsoft.Xna.Framework.Graphics;
using System.Numerics;

namespace Commangineer.Units
{
    /// <summary>
    /// The chassis of a unit
    /// </summary>
    public class Chassis
    {
        private string name;
        private int weight;
        private int armour;
        private int health;
        private int steam;
        private float size;
        private Slot[] turretSizes;
        private MaterialBalance cost;
        private Texture2D texture;

        // Constructs a new Chassis
        public Chassis(string name, int armour, int weight, int health, float size, Slot[] turretSizes, MaterialBalance cost)
        {
            texture = Assets.GetTexture(name);
            this.name = name;
            this.armour = armour;
            this.health = health;
            this.weight = weight;
            this.size = size;
            this.turretSizes = turretSizes;
            this.cost = cost;
        }
        // Getters for the Chassis
        public string Name
        { get { return name; } }
        public int Armour
        { get { return armour; } }
        public int Health
        { get { return health; } }
        public int Weight
        { get { return weight; } }
        public Texture2D Texture
        { get { return texture; } }
        public MaterialBalance Cost
        { get { return cost; } }

        /// <summary>
        /// Gets the slots of the chassis
        /// </summary>
        public Slot[] Slots
        {
            get
            {
                return turretSizes;
            }
        }

        /// <summary>
        /// Sets the weapon of a chassis
        /// </summary>
        /// <param name="index">Which weapon slot to use</param>
        /// <param name="weapon">Which weapon to use</param>
        public void SetWeapon(int index, Weapon weapon)
        {
            turretSizes[index].AddWeapon(weapon);
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
        /// Clones the chassis
        /// </summary>
        /// <returns>A clone of the chassis</returns>
        public Chassis Clone()
        {
            Chassis newChassis = new Chassis(name, armour, weight, health, size, new Slot[turretSizes.Length], cost);
            for (int i = 0; i < turretSizes.Length; i++)
            {
                newChassis.turretSizes[i] = new Slot(turretSizes[i]);
            }
            return newChassis;
        }
    }
}