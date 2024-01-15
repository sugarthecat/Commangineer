namespace Commangineer.Units
{
    /// <summary>
    /// A unit's engine
    /// </summary>
    public class Engine
    {
        private string name;
        private int speed;
        private int horsepower;
        private int weight;
        private MaterialBalance materialCost = new MaterialBalance();

        // Constructs a new engine
        public Engine(string name, MaterialBalance materialCost, int speed, int horsepower, int weight)
        {
            this.name = name;
            this.materialCost = materialCost;
            this.speed = speed;
            this.horsepower = horsepower;
            this.weight = weight;
        }

        // Getters for the engine's properties
        public string Name
        { get { return name; } }
        public int Speed
        { get { return speed; } }
        public int Horsepower
        { get { return horsepower; } }
        public int Weight
        { get { return weight; } }
        public MaterialBalance Cost
        { get { return materialCost; } }

        /// <summary>
        /// Clones the engine
        /// </summary>
        /// <returns>A clone of the engine</returns>
        public Engine Clone()
        {
            return new Engine(name, materialCost, speed, horsepower, weight);
        }
    }
}