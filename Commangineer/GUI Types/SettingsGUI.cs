namespace Commangineer.GUI_Types
{
    internal class SettingsGUI : ScalingGUI
    {
        private bool active = false;

        public SettingsGUI() : base(600, 400)
        {
            LoadElements("SettingsGUI");
        }
        public void Update()
        {
            GetElement(3).Visible = Settings.MusicEnabled;
            GetElement(4).Visible = !Settings.MusicEnabled;
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