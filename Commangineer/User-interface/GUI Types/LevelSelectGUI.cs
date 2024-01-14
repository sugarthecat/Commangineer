using Microsoft.Xna.Framework.Graphics;

namespace Commangineer.GUI_Types
{
    /// <summary>
    /// A GUI + The level
    /// </summary>
    internal class LevelSelectGUI : ScalingGUI
    {
        public LevelSelectGUI() : base(600,400)
        {
            LoadElements("LevelSelectGUI");
        }

    }
}