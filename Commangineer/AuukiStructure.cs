using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer
{
    internal class AuukiStructure : TexturedObject
    {
        private Rectangle bounds;
        private Point position;
        private Point size;
        private Texture2D texture;
        private Tile[] tilesUnder;
        protected AuukiStructure(Point position, Point size, Texture2D texture, int minFloorTier, int respawnTime) { 
            this.position = position;
            this.size = size;
            this.texture = texture;
            tilesUnder = new Tile[size.X * size.Y];
        }

        /// <summary>
        /// Mark a certain tile as underneath the structure. Done on setup by the level.
        /// </summary>
        /// <param name="newTile"></param>
        public void AddTile(Tile newTile)
        {
            tilesUnder[0] = newTile;
        }
        public Point Size
        {
            get
            {
                return size;
            }
        }
        public Point Position
        {
            get
            {
                return position;
            }
        }
        public Texture2D GetTexture()
        {
            return texture;
        }
    }
}
