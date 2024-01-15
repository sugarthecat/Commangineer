namespace Commangineer.Units
{
    public class Engine
    {
        private string name;
        private int speed;
        private int horsepower;
        private int weight;
        private MaterialBalance materialCost = new MaterialBalance();

        public Engine(string name, MaterialBalance materialCost, int speed, int horsepower, int weight)
        {
            this.name = name;
            this.materialCost = materialCost;
            this.speed = speed;
            this.horsepower = horsepower;
            this.weight = weight;
        }

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

        public Engine Clone()
        {
            return new Engine(name, materialCost, speed, horsepower, weight);
        }
    }
}