using Microsoft.Xna.Framework.Graphics;

namespace Commangineer.Units
{
    /// <summary>
    /// Represents a material
    /// </summary>
    public class Material
    {
        private string name;
        private int strength;
        private int workability;
        private int weight;
        private Texture2D texture;

        // Constructs a new material
        public Material(string name, int strength, int workability, int weight)
        {
            texture = Assets.GetTexture(name);
            this.name = name;
            this.strength = strength;
            this.workability = workability;
            this.weight = weight;
        }

        // Getters for the properties
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