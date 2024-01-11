namespace Commangineer.GUI_Types
{
    internal class SettingsGUI : ScalingGUI
    {
        private bool active = false;

        public SettingsGUI() : base(600, 400)
        {
            LoadElements("SettingsGUI");
        }

        public bool Active
        {
            get
            {
                return active;
            }
            set
            {
                active = value;
            }
        }
    }
}