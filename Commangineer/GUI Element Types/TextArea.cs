
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

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
        public override void Draw(SpriteBatch spriteBatch)
        {
            font.DrawText(spriteBatch, text, position);
        }
    }
}
