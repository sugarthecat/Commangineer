using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Commangineer
{
    internal interface TexturedObject
    {
        public Texture2D GetTexture();

        public Point GetSize();

        public Point GetPosition();
    }
}