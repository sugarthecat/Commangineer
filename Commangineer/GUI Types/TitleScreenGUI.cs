using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer.GUI_Types
{
    internal class TitleScreenGUI : GUI
    {
        public TitleScreenGUI() : base()
        {
            AddGuiElement(new GUIElement(Assets.GetTexture2D("background"), new Microsoft.Xna.Framework.Rectangle(0, 0, 100, 100)));

        }
    }
}
