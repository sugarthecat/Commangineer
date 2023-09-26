using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer.GUI_Types
{
    internal class MainMenuGUI : GUI
    {
        public MainMenuGUI() :base()
        {
            AddGuiElement(new GUIElement(Assets.GetTexture2D("background"), new Microsoft.Xna.Framework.Rectangle(0, 0, 100, 100)));
        }
    }
}
