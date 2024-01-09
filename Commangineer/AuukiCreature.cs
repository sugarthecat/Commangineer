using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer
{
    public class AuukiCreature : RotatableTexturedObject
    {
        protected float speed = 1f;
        protected float direction = 0f;
        Vector2 position;
        Texture2D texture;
        Vector2 size;
        public AuukiCreature(Vector2 position, Vector2 size, Texture2D texture)
        {
            this.position = new Vector2(position.X-size.X/2,position.Y-size.Y/2);
            this.size = size;
            this.texture = texture;
        }
        public AuukiCreature(Vector2 position)
        {
            this.position = new Vector2(position.X - 0.5f, position.Y - 0.5f);
            this.size = new Vector2(1f,1f);
            texture = Assets.GetTexture("default");
        }

        public void Update(float deltaTime)
        {
            this.position.X += speed*deltaTime*(float)Math.Cos(direction);
            this.position.Y += speed*deltaTime*(float)Math.Sin(direction);
            direction += deltaTime * 0.3f;
        }
        public Texture2D GetTexture()
        {
            return texture;
        }
        public float Angle
        {
            get
            {
                return direction;
            }
        }
        public Vector2 RotationOrigin
        {
            get
            {
                return new Vector2(size.X/2f,size.Y/2f);
            }
        }
        public Vector2 Size
        {
            get
            {
                return size;
            }
        }
        public Vector2 Position
        {

            get
            {
                return position;
            }
        }
    }
}
