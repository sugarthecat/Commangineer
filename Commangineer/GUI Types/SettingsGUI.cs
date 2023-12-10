using Microsoft.Xna.Framework;

namespace Commangineer.GUI_Types
{
    internal class SettingsGUI : ScalingGUI
    {
        public SettingsGUI() : base(600, 400)
        {
            LoadElements("SettingsGUI");
        }
    }
}