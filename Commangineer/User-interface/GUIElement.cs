using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

namespace Commangineer.User_Interface
{
    /// <summary>
    /// A element within a GUI
    /// </summary>
    public class GUIElement
    {
        protected Rectangle position;
        private Rectangle originalPosition;
        protected Texture2D texture;
        protected Texture2D textureHover;
        protected Action activationAction;
        private Color color;
        private bool visible = true;

        /// <summary>
        /// Creates a new GUI element
        /// </summary>
        /// <param name="baseTexture">The base texture to be used</param>
        /// <param name="hoverTexture">The hover texture if applicable to be used</param>
        /// <param name="elementPosition">The position of the element</param>
        /// <param name="actionOnActivate">A action to trigger if applicable</param>
        public GUIElement(Texture2D baseTexture, Texture2D hoverTexture, Rectangle elementPosition, Action actionOnActivate)
        {
            position = elementPosition;
            originalPosition = elementPosition;
            texture = baseTexture;
            textureHover = hoverTexture;
            activationAction = actionOnActivate;
            color = Color.White;
        }

        // Different constructors for different scenarios

        public GUIElement(Texture2D baseTexture, Rectangle elementPosition) : this(baseTexture, baseTexture, elementPosition)
        {
        }

        public GUIElement(Texture2D baseTexture, Rectangle elementPosition, Color color) : this(baseTexture, baseTexture, elementPosition)
        {
            this.color = color;
        }

        public GUIElement(Texture2D baseTexture, Texture2D hoverTexture, Rectangle elementPosition) : this(baseTexture, baseTexture, elementPosition, delegate { })
        {
        }

        public GUIElement(Texture2D baseTexture, Rectangle elementPosition, Action actionOnActivate) : this(baseTexture, baseTexture, elementPosition, actionOnActivate)
        {
        }

        /// <summary>
        /// Sets the visibility of the element
        /// </summary>
        public bool Visible
        {
            set { visible = value; }
        }

        /// <summary>
        /// Draws the element to the screen
        /// </summary>
        /// <param name="spriteBatch">The sprite batch to be drawn with</param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (visible)
            {
                MouseState mouseState = Mouse.GetState();
                if (position.Contains(mouseState.X, mouseState.Y))
                {
                    spriteBatch.Draw(textureHover, position, color);
                }
                else
                {
                    spriteBatch.Draw(texture, position, color);
                }
            }
        }

        /// <summary>
        /// Moves the element by a certain offset
        /// </summary>
        /// <param name="x">The X translation amount, in pixels</param>
        /// <param name="y">The Y translation amount, in pixels</param>
        public void Translate(int x, int y)
        {
            position.X += x;
            position.Y += y;
        }

        /// <summary>
        /// Scales the element by a given factor
        /// </summary>
        /// <param name="scaleFac">The factor to scale by</param>
        public void Scale(double scaleFac)
        {
            position.X = (int)Math.Floor(position.X * scaleFac);
            position.Y = (int)Math.Floor(position.Y * scaleFac);
            position.Width = (int)Math.Floor(position.Width * scaleFac);
            position.Height = (int)Math.Floor(position.Height * scaleFac);
        }

        /// <summary>
        /// Horizontally scales the element by a given factor
        /// </summary>
        /// <param name="scaleFac">The scale factor</param>
        public void ScaleX(double scaleFac)
        {
            position.X = (int)Math.Floor(position.X * scaleFac);
            position.Width = (int)Math.Floor(position.Width * scaleFac);
        }

        /// <summary>
        /// Vertically scales the element by a given factor
        /// </summary>
        /// <param name="scaleFac">The scale factor</param>
        public void ScaleY(double scaleFac)
        {
            position.Y = (int)Math.Floor(position.Y * scaleFac);
            position.Height = (int)Math.Floor(position.Height * scaleFac);
        }

        /// <summary>
        /// Returns to the original X,Y,Width and Height before any transformations
        /// </summary>
        public void Restore()
        {
            position.X = originalPosition.X;
            position.Y = originalPosition.Y;
            position.Height = originalPosition.Height;
            position.Width = originalPosition.Width;
        }

        /// <summary>
        /// Checks if a element was clicked
        /// </summary>
        /// <param name="mousePoint">The position the mouse clicked at</param>
        /// <returns>If the element was clicked</returns>
        public bool HandleClick(Point mousePoint)
        {
            if (position.Contains(mousePoint))
            {
                activationAction();
                return true;
            }
            return false;
        }
    }
}