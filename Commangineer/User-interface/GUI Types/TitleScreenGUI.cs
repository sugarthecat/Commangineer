namespace Commangineer.GUI_Types
{
    /// <summary>
    /// The GUI object for the title screen
    /// </summary>
    public class TitleScreenGUI : ScalingGUI
    {
        /// <summary>
        /// Loads in the GUI's elements
        /// </summary>
        public TitleScreenGUI() : base(600, 400)
        {
            LoadElements("TitleScreenGUI");
        }
    }
}