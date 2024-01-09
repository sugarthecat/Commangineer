using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Commangineer
{
    internal interface RotatableTexturedObject : TexturedObject
    {
        public float Angle { get; }
        public Vector2 RotationOrigin { get; }
    }
}