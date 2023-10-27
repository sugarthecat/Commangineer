using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Commangineer.Units.Unit;

namespace Commangineer.Units
{
    internal class Engine
    {
        private string name;
        private int materialCost;
        private int steam;
        private int speed;
        private int horsepower;
        private int size;

        public Engine(string name, int materialCost, int steam, int speed, int horsepower, int size)
        {
            this.name = name;
            this.materialCost = materialCost;
            this.steam = steam;
            this.speed = speed;
            this.horsepower = horsepower;
            this.size = size;
        }


        public string GetName()
        {
            return name;
        }

        public int GetMaterialCost()
        {
            return materialCost;
        }

        public int GetSpeed()
        {
            return speed;
        }

        public int GetSteam()
        {
            return steam;
        }

        public int GetHorsepower()
        {
            return horsepower;
        }

        public int GetSize()
        {
            return size;
        }

    }
}
