using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer
{
    internal class AuukiStructure : Structure, TexturedObject
    {
        private Rectangle bounds;
        private Texture2D texture;
        protected AuukiStructure(Point position, Point size, Texture2D texture) { 
            bounds = new Rectangle(position, size);
            this.texture = texture;
        }
        public Point GetSize()
        {
            return new Point(bounds.Width, bounds.Height);
        }
        public Point GetPosition()
        {
            return new Point(bounds.X, bounds.Y);
        }
        public Texture2D GetTexture()
        {
            return texture;
        }
    }
}
