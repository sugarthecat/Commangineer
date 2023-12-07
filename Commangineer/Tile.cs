using Commangineer.Floor_Auuki_types;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Commangineer
{
    internal class Tile : TexturedObject
    {
        private Texture2D texture;
        private Point position;
        protected AuukiFloor AuukiTile;
        private AuukiStructure OccupyingAuukiStructure;
        protected float transmissionChance;

        public Tile(Texture2D texture, Point position, float transmissionChance)
        {
            AuukiTile = null;
            this.texture = texture;
            this.position = position;
            this.transmissionChance = transmissionChance;
        }

        public bool HasAuukiTile
        {
            get
            {
                return AuukiTile != null;
            }
        }

        public bool HasAuukiStructure
        {
            get
            {
                return OccupyingAuukiStructure != null;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public AuukiFloor GetAuukiTile()
        {
            return AuukiTile;
        }

        public void SetAuukiStructure(AuukiStructure structure)
        {
            OccupyingAuukiStructure = structure;
        }
        public AuukiStructure GetAuukiStructure()
        {
            return OccupyingAuukiStructure;
        }
        /// <summary>
        /// Infects the tile with Auuki according to its type
        /// </summary>
        public virtual void InfectWithAuuki()
        {
            AuukiTile = new WoodlandFloorAuuki(position);
        }

        /// <summary>
        /// Attempts to get infected with Auuki
        /// </summary>
        /// <param name="deltaTime">The the length of time it's been attemptedly infected</param>
        public void AttemptInfectWithAuuki(float deltaTime)
        {
            Random infectionChance = new Random();
            if(Math.Pow(1 - transmissionChance, deltaTime) < infectionChance.NextDouble())
            {
                InfectWithAuuki();
            }
        }

        /// <summary>
        /// Gets the texture for the tile
        /// </summary>
        /// <returns>the tile's texture</returns>
        public Texture2D GetTexture()
        {
            return texture;
        }

        /// <summary>
        /// Gets the size of the tile
        /// </summary>
        /// <returns>The size of the tile, as a point. always (1,1).</returns>
        public Point GetSize()
        {
            return new Point(1, 1);
        }

        /// <summary>
        /// Gets the position of the tile
        /// </summary>
        /// <returns>The position of the tile, as a point</returns>
        public Point GetPosition()
        {
            return position;
        }

        /// <summary>
        /// Updates any time-based stats of the tile
        /// </summary>
        /// <param name="deltaTime">the seconds passed since the last update</param>
        public void Update(float deltaTime)
        {
            if (HasAuukiTile)
            {
                AuukiTile.Update(deltaTime);
            }
        }
    }
}