using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer.GUI_Types
{
    internal class MainMenuGUI : ScalingGUI
    {
        public MainMenuGUI() : base(600, 400)
        {
            AddGuiElement(new GUIElement(Assets.GetImage("background"), new Rectangle(0, 0, 600, 200)));
        }
    }
}