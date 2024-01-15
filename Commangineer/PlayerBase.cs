using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer
{
    /// <summary>
    /// Represents the base player
    /// </summary>
    internal class PlayerBase : TexturedObject
    {
        Vector2 position;
        Texture2D texture;
        public PlayerBase(Point position) {
            this.position = new Vector2(position.X, position.Y);
            texture = Assets.GetTexture("playerBase");
        }
        /// <summary>
        /// Gets the player base size
        /// </summary>
        public Vector2 Size
        {
            get { return new Vector2(4f, 4f); }
        }
        /// <summary>
        /// Gets the player base position
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
        }
        /// <summary>
        /// Gets the player base position as a point
        /// </summary>
        public Point PointPosition
        {
            get { return position.ToPoint(); }
        }
        /// <summary>
        /// Gets the texture of the player base
        /// </summary>
        public Texture2D GetTexture()
        {
            return texture;
        }
        /// <summary>
        /// Checks if the player base contains a point
        /// </summary>
        /// <param name="point">The point to check</param>
        /// <returns>If it contains the point</returns>
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
