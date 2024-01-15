using Commangineer.Units;
using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;

namespace Commangineer
{
    /// <summary>
    /// Represents a type of material
    /// </summary>
    public enum MaterialType
    {
        Scrap,
        Iron,
        Tungsten
    }

    /// <summary>
    /// Represents a storage of materials
    /// </summary>
    public struct MaterialBalance
    {
        private Dictionary<MaterialType, int> materialCounts = new Dictionary<MaterialType, int>();

        // Different constructors for the storage
        public MaterialBalance()
        {
            SetupDictionary();
        }

        public MaterialBalance(JsonNode jsonNode)
        {
            SetupDictionary();

            MaterialType[] materialTypes = Enum.GetValues<MaterialType>();
            for (int i = 0; i < materialTypes.Length; i++)
            {
                materialCounts[materialTypes[i]] = (int)jsonNode[MaterialManager.GetMaterial(materialTypes[i]).Name];
            }
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

        /// <summary>
        /// Sets up the material amount dictionary
        /// </summary>
        private void SetupDictionary()
        {
            MaterialType[] materialTypes = Enum.GetValues<MaterialType>();
            for (int i = 0; i < materialTypes.Length; i++)
            {
                materialCounts.Add(materialTypes[i], 0);
            }
        }

        /// <summary>
        /// Adds material balances together
        /// </summary>
        /// <param name="bal1">The first balance</param>
        /// <param name="bal2">The second balance</param>
        /// <returns>A new combined balance</returns>
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

        /// <summary>
        /// Allows for setting and retrieving of material amounts
        /// </summary>
        /// <param name="material">The material</param>
        /// <returns>Amount of the material in storage</returns>
        public int this[MaterialType material]
        {
            get { return materialCounts[material]; }
            set { materialCounts[material] = value; }
        }

        /// <summary>
        /// Gets the amount of different materials in storage
        /// </summary>
        public int MaterialCount
        {
            get
            {
                return materialCounts.Count;
            }
        }

        /// <summary>
        /// Checks if the current storage has more of every resource than another storage
        /// </summary>
        /// <param name="other">The other storage</param>
        /// <returns>If the current storage has more of every resource than another storage</returns>
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

        /// <summary>
        /// Subtracts resources from the current storage for every resource and their amount in another storage
        /// </summary>
        /// <param name="other">The other storage</param>
        public void Remove(MaterialBalance other)
        {
            MaterialType[] materialTypes = Enum.GetValues<MaterialType>();
            for (int i = 0; i < materialTypes.Length; i++)
            {
                this[materialTypes[i]] -= other[materialTypes[i]];
            }
        }
    }

    /// <summary>
    /// Manages in game materials
    /// </summary>
    public static class MaterialManager
    {
        private static Dictionary<MaterialType, Material> materials = new Dictionary<MaterialType, Material>();

        static MaterialManager()
        {
            // The default materials
            AddMaterial(MaterialType.Scrap, 50, 50, 400);
            AddMaterial(MaterialType.Iron, 100, 100, 300);
            AddMaterial(MaterialType.Tungsten, 500, 50, 800);
        }

        /// <summary>
        /// Adds a new material to the list of materials
        /// </summary>
        /// <param name="materialType">The type of material</param>
        /// <param name="strength">How strong a material is</param>
        /// <param name="workability">How easy it is to use a material</param>
        /// <param name="weight">The weight of the material</param>
        private static void AddMaterial(MaterialType materialType, int strength, int workability, int weight)
        {
            materials.Add(materialType, new Material(materialType.ToString().ToLower(), strength, workability, weight));
        }

        /// <summary>
        /// Gets a material object from the list of materials
        /// </summary>
        /// <param name="material">The material to check</param>
        /// <returns>The material object corresponding to the given material type</returns>
        public static Material GetMaterial(MaterialType material)
        {
            return materials[material];
        }
    }
}