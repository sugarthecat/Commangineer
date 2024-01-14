using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer
{
    internal class PlayerBase : TexturedObject
    {
        Vector2 position;
        Texture2D texture;
        public PlayerBase(Point position) {
            this.position = new Vector2(position.X, position.Y);
            texture = Assets.GetTexture("playerBase");
        }
        public Vector2 Size
        {
            get { return new Vector2(4f, 4f); }
        }
        public Vector2 Position
        {
            get { return position; }
        }
        public Point PointPosition
        {
            get { return position.ToPoint(); }
        }
        public Texture2D GetTexture()
        {
            return texture;
        }
        public bool ContainsPoint(Vector2 point)
        {
            Rectangle bounds = new Rectangle(
                (int)position.X,
                (int)position.Y,
                (int)Size.X,
                (int)Size.Y
                );
            return bounds.Contains(point);
        }
    }
}
