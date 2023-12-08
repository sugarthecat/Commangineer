using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Commangineer.GUI_Element_Types
{
    public class Button : GUIElement
    {
        /// <summary>
        /// Creates a button without a click action
        /// </summary>
        /// <param name="buttonName">The name of the asset to get for the button</param>
        /// <param name="position">A rectangle representing the position of the button</param>
        public Button(string buttonName, Rectangle position) : base(Assets.GetButtonTexure(buttonName), position)
        {
        }

        /// <summary>
        /// Creates a button with an action on click
        /// </summary>
        /// <param name="buttonName">The name of the asset to get for the button</param>
        /// <param name="position">A rectangle representing the position of the button</param>
        /// <param name="action">The action to be performed on click</param>
        public Button(string buttonName, Rectangle position, Action action) : base(Assets.GetButtonTexure(buttonName), position, action)
        {

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            MouseState mouseState = Mouse.GetState();
            if (position.Contains(mouseState.X, mouseState.Y))
            {
                spriteBatch.Draw(texture, position, new Rectangle(0, 0, texture.Width / 2, texture.Height), Color.White);
            }
            else
            {
                spriteBatch.Draw(texture, position, new Rectangle(texture.Width / 2, 0, texture.Width / 2, texture.Height), Color.White);
            }
        }
    }
}