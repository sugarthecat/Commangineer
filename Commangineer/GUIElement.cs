using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Commangineer
{
    public class GUIElement
    {
        protected Rectangle position;
        private Rectangle originalPosition;
        protected Texture2D texture;
        protected Texture2D textureHover;
        protected Action activationAction;
        private Color color;
        public GUIElement(Texture2D baseTexture, Texture2D hoverTexture, Rectangle elementPosition, Action actionOnActivate)
        {

            position = elementPosition;
            originalPosition = elementPosition;
            texture = baseTexture;
            textureHover = hoverTexture;
            activationAction = actionOnActivate;
            color = Color.White;
        }
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
        public GUIElement(Texture2D baseTexture, Rectangle elementPosition, Action actionOnActivate): this(baseTexture, baseTexture, elementPosition, actionOnActivate)
        {

        }
        
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            MouseState mouseState = Mouse.GetState();
            if (position.Contains(mouseState.X,mouseState.Y))
            {
                spriteBatch.Draw(textureHover, position, color);
            }
            else
            {
                spriteBatch.Draw(texture, position, color);
            }
        }
        public void Translate(int x, int y)
        {
            position.X += x;
            position.Y += y;
        }
        public void Scale(double scaleFac)
        {
            position.X = (int)Math.Floor(position.X * scaleFac);
            position.Y = (int)Math.Floor(position.Y * scaleFac);
            position.Width = (int)Math.Floor(position.Width * scaleFac);
            position.Height = (int)Math.Floor(position.Height * scaleFac);
        }
        public void ScaleX(double scaleFac)
        {
            position.X = (int)Math.Floor(position.X * scaleFac);
            position.Width = (int)Math.Floor(position.Width * scaleFac);
        }
        public void ScaleY(double scaleFac)
        {
            position.Y = (int)Math.Floor(position.Y * scaleFac);
            position.Height = (int)Math.Floor(position.Height * scaleFac);
        }
        public void Restore()
        {
            position.X = originalPosition.X;
            position.Y = originalPosition.Y;
            position.Height = originalPosition.Height;
            position.Width = originalPosition.Width;
        }
        public bool HandleClick(Point mousePoint) {
            if (position.Contains(mousePoint))
            {
                activationAction();
                return true;
            }
            return false;
        }
    }
}
