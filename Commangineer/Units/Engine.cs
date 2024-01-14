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
        public string Name { get {  return name; } }
        public int MaterialCost { get {  return materialCost; } }
        public int Steam { get {  return steam; } }
        public int Speed { get { return speed; } }
        public int Horsepower { get {  return horsepower; } }
        public int Size { get { return size; } }
        public Engine Clone()
        {
            return new Engine(name, materialCost, steam, speed, horsepower, size);
        }
    }
}
