using Commangineer.GUI_Element_Types;
using Microsoft.Xna.Framework;

namespace Commangineer.GUI_Types
{
    /// <summary>
    /// The GUI object for the title screen
    /// </summary>
    internal class TitleScreenGUI : ScalingGUI
    {
        public TitleScreenGUI() : base(600,400)
        {
            AddGuiElement(new GUIElement(Assets.GetImage("background"), new Rectangle(0, 0, 600, 400)));
            AddGuiElement(new GUIElement(Assets.GetImage("banner"), new Rectangle(100, 50, 400, 150)));
            AddGuiElement(new GUIElement(Assets.GetImage("icon"), new Rectangle(250, 250, 100, 100)));
            AddGuiElement(new GUIElement(Assets.GetImage("smiley"), new Rectangle(350, 250, 100, 100)));
            AddGuiElement(new Button("generic", new Rectangle(100, 300, 50, 50), delegate { Commangineer.instance.NavigateToMenu("mainMenu"); }));
            AddGuiElement(new Button("bigredbutton", new Rectangle(450, 300, 50, 50), delegate { Commangineer.instance.NavigateToMenu("level"); }));
        }
    }
}