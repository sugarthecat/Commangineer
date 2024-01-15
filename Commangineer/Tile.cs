using Commangineer.Auuki;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Commangineer
{
    /// <summary>
    /// A class representing a tile, a square space in a level
    /// </summary>
    public class Tile : TexturedObject
    {
        private Texture2D texture;
        private Point position;
        protected AuukiFloor AuukiTile;
        private AuukiStructure occupyingAuukiStructure;
        protected float transmissionChance;
        protected bool isSolid = false;

        public Tile(Texture2D texture, Point position, float transmissionChance)
        {
            AuukiTile = null;
            this.texture = texture;
            this.position = position;
            this.transmissionChance = transmissionChance;
        }

        /// <summary>
        /// Checks if the tile has Auuki on it
        /// </summary>
        public bool HasAuukiTile
        {
            get
            {
                return AuukiTile != null;
            }
        }

        /// <summary>
        /// Checks if the tile has a Auuki Structure on it
        /// </summary>
        public bool HasAuukiStructure
        {
            get
            {
                return (occupyingAuukiStructure != null && occupyingAuukiStructure.Alive);
            }
        }

        /// <summary>
        /// Gets and Sets the current Auuki structure
        /// </summary>
        public AuukiStructure AuukiStructure
        {
            set
            {
                occupyingAuukiStructure = value;
            }
            get
            {
                return occupyingAuukiStructure;
            }
        }

        /// <summary>
        /// Gets the Auuki object of the tile
        /// </summary>
        public AuukiFloor Auuki
        {
            get
            {
                return AuukiTile;
            }
        }

        /// <summary>
        /// Infects the tile with Auuki according to its type
        /// </summary>
        public virtual void InfectWithAuuki()
        {
        }

        /// <summary>
        /// Removes Auuki from the tile
        /// </summary>
        public void RemoveAuuki()
        {
            AuukiTile = null;
        }

        /// <summary>
        /// Attempts to get infected with Auuki
        /// </summary>
        /// <param name="deltaTime">The the length of time it's been attemptedly infected</param>
        public void AttemptInfectWithAuuki(float deltaTime)
        {
            Random infectionChance = new Random();
            if (Math.Pow(1 - transmissionChance, deltaTime) < infectionChance.NextDouble())
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
        /// The size of tile. Always 1,1
        /// </summary>
        public Vector2 Size
        {
            get
            {
                return new Vector2(1, 1);
            }
        }

        /// <summary>
        /// The position of the tile, in vector format
        /// </summary>
        public Vector2 Position
        {
            get
            {
                return new Vector2(position.X, position.Y);
            }
        }

        /// <summary>
        /// The position of the tile, in point format
        /// </summary>
        public Point PointPosition
        {
            get
            {
                return position;
            }
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
                if (!AuukiTile.Alive)
                {
                    RemoveAuuki();
                }
            }
        }

        /// <summary>
        /// Gets if the tile is solid
        /// </summary>
        public bool IsSolid
        {
            get
            {
                return isSolid;
            }
        }
    }
}