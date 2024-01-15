using Microsoft.Xna.Framework.Graphics;

namespace Commangineer.Units
{
    public class Material
    {
        private string name;
        private int strength;
        private int workability;
        private int weight;
        private Texture2D texture;

        public Material(string name, int strength, int workability, int weight)
        {
            texture = Assets.GetTexture(name);
            this.name = name;
            this.strength = strength;
            this.workability = workability;
            this.weight = weight;
        }

        public Texture2D Texture
        { get { return texture; } }
        public int Strength
        { get { return strength; } }
        public int Workability
        { get { return workability; } }
        public int Weight
        { get { return weight; } }
        public string Name
        { get { return name; } }
    }
}