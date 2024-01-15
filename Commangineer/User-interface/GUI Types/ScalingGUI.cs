using Commangineer.User_Interface;

namespace Commangineer.GUI_Types
{
    /// <summary>
    /// A type of GUI which can be scaled
    /// </summary>
    public class ScalingGUI : GUI
    {
        private int originalWidth;
        private int originalHeight;

        /// <summary>
        /// Creates a new Scaling GUI
        /// </summary>
        /// <param name="width">The width of the GUI</param>
        /// <param name="height">The height of the GUI</param>
        public ScalingGUI(int width, int height) : base()
        {
            originalWidth = width;
            originalHeight = height;
            Rescale();
        }

        /// <summary>
        /// Rescale the GUI to fit the current screen
        /// </summary>
        public void Rescale()
        {
            double screenHeightRatio = ((double)Commangineer.GetScreenHeight()) / ((double)originalHeight);
            double screenWidthRatio = ((double)Commangineer.GetScreenWidth()) / ((double)originalWidth);
            for (int i = 0; i < elements.Count; i++)
            {
                elements[i].Restore();
                elements[i].ScaleX(screenWidthRatio);
                elements[i].ScaleY(screenHeightRatio);
            }
        }

        /// <summary>
        /// Updates the current GUI
        /// </summary>
        public override void Update()
        {
            base.Update();
            Rescale();
        }
    }
}