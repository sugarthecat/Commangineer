using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer.Units
{
    internal class Material
    {
        private string name;
        private int strength;
        private int workability;
        private int weight;

        public Material(string name, int strength, int workability, int weight)
        {
            this.name = name;
            this.strength = strength;
            this.workability = workability;
            this.weight = weight;
        }
    }
}
