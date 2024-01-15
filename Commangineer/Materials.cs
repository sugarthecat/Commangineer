using Commangineer.Units;
using System;
using System.Collections.Generic;

namespace Commangineer
{
    public enum MaterialType
    {
        Scrap,
        Iron,
        Tungsten
    }

    public struct MaterialBalance
    {
        private Dictionary<MaterialType, int> materialCounts = new Dictionary<MaterialType, int>();

        public MaterialBalance()
        {
            SetupDictionary();
        }

        public MaterialBalance(int scrap)
        {
            SetupDictionary();
            materialCounts[MaterialType.Scrap] = scrap;
        }

        public MaterialBalance(int scrap, int iron)
        {
            SetupDictionary();
            materialCounts[MaterialType.Scrap] = scrap;
            materialCounts[MaterialType.Iron] = iron;
        }

        public MaterialBalance(int scrap, int iron, int tungsten)
        {
            SetupDictionary();
            materialCounts[MaterialType.Scrap] = scrap;
            materialCounts[MaterialType.Iron] = iron;
            materialCounts[MaterialType.Tungsten] = tungsten;
        }

        private void SetupDictionary()
        {
            MaterialType[] materialTypes = Enum.GetValues<MaterialType>();
            for (int i = 0; i < materialTypes.Length; i++)
            {
                materialCounts.Add(materialTypes[i], 0);
            }
        }

        public static MaterialBalance operator +(MaterialBalance bal1, MaterialBalance bal2)
        {
            MaterialBalance sum = new MaterialBalance();
            MaterialType[] materialTypes = Enum.GetValues<MaterialType>();

            foreach (MaterialType material in materialTypes)
            {
                sum[material] = bal1[material] + bal2[material];
            }
            return sum;
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

        public void Remove(MaterialBalance other)
        {
            MaterialType[] materialTypes = Enum.GetValues<MaterialType>();
            for (int i = 0; i < materialTypes.Length; i++)
            {
                this[materialTypes[i]] -= other[materialTypes[i]];
            }
        }
    }

    public static class MaterialManager
    {
        private static Dictionary<MaterialType, Material> materials = new Dictionary<MaterialType, Material>();

        static MaterialManager()
        {
            AddMaterial(MaterialType.Scrap, 50, 50, 400);
            AddMaterial(MaterialType.Iron, 100, 100, 300);
            AddMaterial(MaterialType.Tungsten, 500, 50, 800);
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