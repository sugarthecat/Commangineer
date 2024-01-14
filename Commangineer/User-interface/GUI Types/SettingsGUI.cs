using Commangineer;
using Commangineer.User_Interface;
namespace Commangineer.GUI_Types
{
    internal class SettingsGUI : ScalingGUI
    {
        private bool active = false;

        public SettingsGUI() : base(600, 400)
        {
            LoadElements("SettingsGUI");
        }
        public override void Update()
        {
            base.Update();
            GetElement(3).Visible = Settings.MusicEnabled;
            GetElement(4).Visible = !Settings.MusicEnabled;
        }
    }
}