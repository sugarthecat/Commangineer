using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Commangineer
{
    public class GUIElement
    {
        protected Rectangle position;
        protected Texture2D texture;
        protected Texture2D textureHover;
        protected Action activationAction;
        private Color color;
        public GUIElement(Texture2D baseTexture, Rectangle elementPosition)
        {
            position = elementPosition;
            texture = baseTexture;
            textureHover = baseTexture;
            activationAction = delegate { };
            color = Color.White;
        }
        public GUIElement(Texture2D baseTexture, Rectangle elementPosition, Color color)
        {
            position = elementPosition;
            texture = baseTexture;
            textureHover = baseTexture;
            activationAction = delegate { };
            this.color = color;
        }
        public GUIElement(Texture2D baseTexture, Texture2D hoverTexture, Rectangle elementPosition)
        {
            position = elementPosition;
            texture = baseTexture;
            textureHover = hoverTexture;
            activationAction = delegate { };
            color = Color.White;
        }
        public GUIElement(Texture2D baseTexture, Texture2D hoverTexture, Rectangle elementPosition, Action actionOnActivate)
        {

            position = elementPosition;
            texture = baseTexture;
            textureHover = hoverTexture;
            activationAction = actionOnActivate;
            color = Color.White;
        }
        public GUIElement(Texture2D baseTexture, Rectangle elementPosition, Action actionOnActivate)
        {

            position = elementPosition;
            texture = baseTexture;
            textureHover = baseTexture;
            activationAction = actionOnActivate;
            color = Color.White;
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
