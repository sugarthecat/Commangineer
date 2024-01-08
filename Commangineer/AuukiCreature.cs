﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer
{
    public class AuukiCreature : TexturedObject
    {
        Vector2 position;
        public AuukiCreature(Vector2 position)
        {
            this.position = position;
        }
        public void Update(float deltaTime)
        {
            this.position.X += deltaTime;
            this.position.Y += deltaTime;
        }
        public Texture2D GetTexture()
        {
            return Assets.GetTexture("default");
        }
        public Vector2 Size
        {

            get
            {
                return new Vector2(1f, 1f);
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
