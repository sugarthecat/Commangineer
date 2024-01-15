using Microsoft.Xna.Framework.Graphics;
using System.Numerics;

namespace Commangineer.Units
{
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

        public Slot[] Weapons
        {
            get
            {
                return turretSizes;
            }
        }

        public void SetWeapon(int index, Weapon weapon)
        {
            turretSizes[index].AddWeapon(weapon);
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
            Chassis newChassis = new Chassis(name, armour, weight, health, size, new Slot[turretSizes.Length], cost);
            for (int i = 0; i < turretSizes.Length; i++)
            {
                newChassis.turretSizes[i] = new Slot(turretSizes[i]);
            }
            return newChassis;
        }
    }
}