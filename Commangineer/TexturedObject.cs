using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Commangineer
{
    public interface TexturedObject
    {
        public Texture2D GetTexture();

        public Vector2 Size { get; }

        public Vector2 Position { get; }
    }
}