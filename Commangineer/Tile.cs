using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer
{
    internal class Tile: TexturedObject
    {
        Texture2D texture;
        Point position;
        TileOccupier tileOccupier;
        public Tile(Texture2D texture, Point position)
        {
            tileOccupier = null;
            this.texture = texture;
            this.position = position;
        }
        public bool HasTileOccupier
        {
            get
            {
                return tileOccupier != null;
            }
        }
        public TileOccupier GetTileOccupier()
        {
            return tileOccupier;
        }
        public Texture2D GetTexture()
        {
            return texture;
        }
        public Point GetSize()
        {
            return new Point(1, 1);
        }
        public Point GetPosition()
        {
            return position;
        }
    }
}
