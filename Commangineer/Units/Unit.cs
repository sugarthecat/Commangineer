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
        private int speed;
        private Chassis chassis;
        private Material chassisMaterial;
        private Engine engine;
        private Material engineMaterial;

        enum specialStats
        {

        }

        public Unit(string name, Chassis chassis, Material chassisMaterial, Engine engine, Material engineMaterial)
        {
            this.name = name;
            this.chassis = chassis;
            this.chassisMaterial = chassisMaterial;
            this.engine = engine;
            this.engineMaterial = engineMaterial;
        }
    }
}
