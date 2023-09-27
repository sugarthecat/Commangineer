using Commangineer.GUI_Element_Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer.GUI_Types
{
    internal class TitleScreenGUI : ScalingGUI
    {
        public TitleScreenGUI() : base(600,400)
        {
            AddGuiElement(new GUIElement(Assets.GetTexture2D("background"), new Microsoft.Xna.Framework.Rectangle(0, 0, 600, 400)));
            AddGuiElement(new GUIElement(Assets.GetTexture2D("banner"), new Microsoft.Xna.Framework.Rectangle(100, 50, 400, 150)));
            AddGuiElement(new GUIElement(Assets.GetTexture2D("icon"), new Microsoft.Xna.Framework.Rectangle(250, 250, 100, 100)));
            AddGuiElement(new Button("generic", new Microsoft.Xna.Framework.Rectangle(100, 300, 50,50), delegate { Commangineer.instance.NavigateToMenu("mainMenu"); }));

        }
    }
}
