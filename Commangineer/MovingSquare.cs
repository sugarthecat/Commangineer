﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Commangineer
{
    public class MovingSquare : TexturedObject
    {
        private Vector2 position;

        public MovingSquare()
        {
            position = new Vector2(0, 0);
        }

        public Vector2 Size
        {
            get
            {
                return new Vector2(1, 1);
            }
        }

        public Vector2 Position
        {
            get
            {
                return position;
            }
        }

        public Texture2D GetTexture()
        {
            return Assets.GetTexture("ham");
        }

        public void moveX(float val)
        {
            position.X += val;
        }

        public void moveY(float val)
        {
            position.Y += val;
        }
    }
}