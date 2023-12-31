﻿namespace Commangineer.GUI_Types
{
    internal class ScalingGUI : GUI
    {
        private int originalWidth;
        private int originalHeight;

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
    }
}