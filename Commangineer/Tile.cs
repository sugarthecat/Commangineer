using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer
{
    internal class Tile
    {
        Texture2D texture;
        TileOccupier tileOccupier;
        public Tile(Texture2D texture){
            tileOccupier = null;
            this.texture = texture;
        }
        public bool HasTileOccupier
        {
            get
            {
                return tileOccupier != null;
            }
        }
        public void Draw(SpriteBatch spriteBatch, Rectangle position)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
