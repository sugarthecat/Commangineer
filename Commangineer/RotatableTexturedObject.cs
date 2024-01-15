using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Commangineer
{
    /// <summary>
    /// Represents a textured object that can be rotated
    /// </summary>
    internal interface RotatableTexturedObject : TexturedObject
    {
        public float Angle { get; }
    }
}