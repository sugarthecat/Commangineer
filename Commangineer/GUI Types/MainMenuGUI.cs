using Microsoft.Xna.Framework;

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