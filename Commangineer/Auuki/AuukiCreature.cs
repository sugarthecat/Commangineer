using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer.Auuki
{
    public enum AuukiAiMode
    {
        Wander

    }
    public class AuukiCreature : RotatableTexturedObject
    {
        protected float speed = 1f;
        protected float direction = 0f;
        private float wanderDistanceLeft = 0f;
        Vector2 position;
        AuukiAiMode behavior = AuukiAiMode.Wander;
        Texture2D texture;
        Vector2 size;
        public AuukiCreature(Vector2 position, Vector2 size, Texture2D texture)
        {
            Random genRandom = new Random();
            direction = (float)(genRandom.NextDouble() * 2 * Math.PI);
            wanderDistanceLeft = (float)genRandom.NextDouble() * 10;
            this.position = new Vector2(position.X - size.X / 2, position.Y - size.Y / 2);
            this.size = size;
            this.texture = texture;
        }
        public AuukiCreature(Vector2 position)
        {
            this.position = new Vector2(position.X - 0.5f, position.Y - 0.5f);
            size = new Vector2(1f, 1f);
            texture = Assets.GetTexture("default");
        }

        public void Update(float deltaTime, Level level)
        {
            float prevX = position.X;
            float prevY = position.Y;
            position.X += speed * deltaTime * (float)Math.Cos(direction);
            position.Y += speed * deltaTime * (float)Math.Sin(direction);
            if (level.Collides(this))
            {
                position.X = prevX;
                position.Y = prevY;
                if (behavior == AuukiAiMode.Wander)
                {
                    wanderDistanceLeft = -1;
                }
            }
            if (behavior == AuukiAiMode.Wander)
            {
                wanderDistanceLeft -= speed * deltaTime;
                if (wanderDistanceLeft <= 0)
                {
                    Random genRandom = new Random();
                    direction = (float)(genRandom.NextDouble() * 2 * Math.PI);
                    wanderDistanceLeft = (float)genRandom.NextDouble() * 10;
                }
            }
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
        public Vector2 Size
        {
            get
            {
                return size;
            }
        }
        public AuukiAiMode Behavior
        {
            get
            {
                return behavior;
            }
            set
            {
                behavior = value;
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
