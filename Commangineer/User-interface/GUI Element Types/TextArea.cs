using Commangineer.User_Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Commangineer.GUI_Element_Types
{
    /// <summary>
    /// A class representing a area to draw text to
    /// </summary>
    public class TextArea : GUIElement
    {
        private Font font;
        private string text;

        // Constructs a text area
        public TextArea(Rectangle position, Font font, string text) : base(Assets.GetImage("background"), position)
        {
            this.font = font;
            this.text = text;
        }

        /// <summary>
        /// Sets the text area's text
        /// </summary>
        public string Text
        { set { text = value; } }

        /// <summary>
        /// Draws to the text area
        /// </summary>
        /// <param name="spriteBatch">The sprite batch to be drawn with</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            font.DrawText(spriteBatch, text, position);
        }
    }
}