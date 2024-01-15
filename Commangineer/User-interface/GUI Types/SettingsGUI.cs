using Commangineer;
using Commangineer.User_Interface;
namespace Commangineer.GUI_Types
{
    internal class SettingsGUI : ScalingGUI
    {
        /// <summary>
        /// Loads in the GUI's elements
        /// </summary>
        public SettingsGUI() : base(600, 400)
        {
            LoadElements("SettingsGUI");
        }
        /// <summary>
        /// Updates the Settings GUI
        /// </summary>
        public override void Update()
        {
            base.Update();
            GetElement(3).Visible = Settings.MusicEnabled;
            GetElement(4).Visible = !Settings.MusicEnabled;
        }
    }
}