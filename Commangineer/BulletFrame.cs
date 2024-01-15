using Microsoft.Xna.Framework;

namespace Commangineer
{
    public class BulletFrame
    {
        public Vector2 origin;
        public Vector2 destination;
        public float size;
        public float duration;

        public BulletFrame(Vector2 origin, Vector2 destination, float size = 0.2f, float duration = 0.4f)
        {
            this.origin = origin;
            this.destination = destination;
            this.size = size;
            this.duration = duration;
        }
    }
}