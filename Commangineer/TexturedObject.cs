using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer
{
    internal interface TexturedObject
    {
        public Texture2D GetTexture();
        public Point GetSize();
        public Point GetPosition();
    }
}
