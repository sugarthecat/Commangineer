using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer.Units
{
    internal class Engine
    {
        private string name;
        private int materialCost;
        private int steam;
        private int speed;
        private int horsepower;

        public Engine(string name, int materialCost, int steam, int speed, int horsepower)
        {
            this.name = name;
            this.materialCost = materialCost;
            this.steam = steam;
            this.speed = speed;
            this.horsepower = horsepower;
        }
    }
}
