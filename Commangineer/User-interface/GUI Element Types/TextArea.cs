using Commangineer.User_Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Commangineer.GUI_Element_Types
{
    public class TextArea : GUIElement
    {
        private Font font;
        private string text;

        public TextArea(Rectangle position, Font font, string text) : base(Assets.GetImage("background"), position)
        {
            this.font = font;
            this.text = text;
        }

        public string Text
        { set { text = value; } }

        public override void Draw(SpriteBatch spriteBatch)
        {
            font.DrawText(spriteBatch, text, position);
        }
    }
}