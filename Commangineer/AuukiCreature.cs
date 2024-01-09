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
    public class AuukiCreature : TexturedObject
    {
        Vector2 position;
        Vector2 size;
        public AuukiCreature(Vector2 position, Vector2 size)
        {
            this.position = new Vector2(position.X-size.X/2,position.Y-size.Y/2);
            this.size = size;
        }
        public AuukiCreature(Vector2 position)
        {
            this.position = position;
            this.size = new Vector2(1f,1f);
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
