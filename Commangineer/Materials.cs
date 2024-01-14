using Commangineer.Units;
using System;
using System.Collections.Generic;

namespace Commangineer
{
    public enum MaterialType
    {
        Scrap,
        Iron
    }

    internal class MaterialBalance
    {
        private Dictionary<MaterialType, int> materialCounts = new Dictionary<MaterialType, int>();

        public MaterialBalance()
        {
            MaterialType[] materialTypes = Enum.GetValues<MaterialType>();
            for (int i = 0; i < materialTypes.Length; i++)
            {
                materialCounts.Add(materialTypes[i], 0);
            }
        }

        public int this[MaterialType material]
        {
            get { return materialCounts[material]; }
            set { materialCounts[material] = value; }
        }

        public int MaterialCount
        {
            get
            {
                return materialCounts.Count;
            }
        }

        public bool GreaterThan(MaterialBalance other)
        {
            MaterialType[] materialTypes = Enum.GetValues<MaterialType>();
            for (int i = 0; i < materialTypes.Length; i++)
            {
                if (this[materialTypes[i]] < other[materialTypes[i]])
                {
                    return false;
                }
            }
            return true;
        }

        public void remove(MaterialBalance other)
        {
            MaterialType[] materialTypes = Enum.GetValues<MaterialType>();
            for (int i = 0; i < materialTypes.Length; i++)
            {
                this[materialTypes[i]] -= other[materialTypes[i]];
            }
        }
    }

    internal static class MaterialManager
    {
        private static Dictionary<MaterialType, Material> materials = new Dictionary<MaterialType, Material>();

        static MaterialManager()
        {
            AddMaterial(MaterialType.Scrap, 50, 50, 400);
            AddMaterial(MaterialType.Iron, 100, 100, 400);
        }

        private static void AddMaterial(MaterialType materialType, int strength, int workability, int weight)
        {
            materials.Add(materialType, new Material(materialType.ToString().ToLower(), strength, workability, weight));
        }

        public static Material GetMaterial(MaterialType material)
        {
            return materials[material];
        }
    }
}