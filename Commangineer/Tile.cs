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
        private FloorAuuki AuukiTile;
        protected float transmissionChance;
        bool isPath;

        public Tile(Texture2D texture, Point position, float transmissionChance)
        {
            AuukiTile = null;
            this.texture = texture;
            this.position = position;
            this.transmissionChance = transmissionChance;
            isPath = true;
        }

        public bool HasAuuki
        {
            get
            {
                return AuukiTile != null;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public FloorAuuki GetAuuki()
        {
            return AuukiTile;
        }

        /// <summary>
        /// Infects the tile with Auuki according to its type
        /// </summary>
        public void InfectWithAuuki()
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

        public bool getIsPath()
        {
            return isPath;
        }

        /// <summary>
        /// Updates any time-based stats of the tile
        /// </summary>
        /// <param name="deltaTime">the seconds passed since the last update</param>
        public void Update(float deltaTime)
        {
            if (HasAuuki)
            {
                AuukiTile.Update(deltaTime);
            }
        }
    }
}