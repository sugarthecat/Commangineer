using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Commangineer.Units.Unit;

namespace Commangineer.Units
{
    /// <summary>
    /// A unit's engine
    /// </summary>
    internal class Engine
    {
        private string name;
        private int materialCost;
        private int steam;
        private int speed;
        private int horsepower;
        private int size;

        // Constructs a new engine
        public Engine(string name, int materialCost, int steam, int speed, int horsepower, int size)
        {
            this.name = name;
            this.materialCost = materialCost;
            this.steam = steam;
            this.speed = speed;
            this.horsepower = horsepower;
            this.size = size;
        }

        // Getters for the engine's properties
        public string Name { get {  return name; } }
        public int MaterialCost { get {  return materialCost; } }
        public int Steam { get {  return steam; } }
        public int Speed { get { return speed; } }
        public int Horsepower { get {  return horsepower; } }
        public int Size { get { return size; } }

        /// <summary>
        /// Clones the engine
        /// </summary>
        /// <returns>A clone of the engine</returns>
        public Engine Clone()
        {
            return new Engine(name, materialCost, steam, speed, horsepower, size);
        }
    }
}
