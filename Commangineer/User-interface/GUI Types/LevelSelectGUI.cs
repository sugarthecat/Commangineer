namespace Commangineer.GUI_Types
{
    /// <summary>
    /// A GUI + The level
    /// </summary>
    public class LevelSelectGUI : ScalingGUI
    {
        /// <summary>
        /// Loads in the GUI's elements
        /// </summary>
        public LevelSelectGUI() : base(600, 400)
        {
            LoadElements("LevelSelectGUI");
        }
    }
}