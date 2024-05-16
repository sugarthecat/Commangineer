using Commangineer.GUI_Element_Types;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace Commangineer.GUI_Types
{
    /// <summary>
    /// A GUI + The level
    /// </summary>
    internal class LevelSelectGUI : ScalingGUI
    {
        /// <summary>
        /// Loads in the GUI's elements
        /// </summary>
        public LevelSelectGUI() : base(600, 400)
        {
            LoadElements("LevelSelectGUI");
            for (int i = 0; i < 1; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Button nb;
                    Rectangle dimensions = new Rectangle(j * 100 + 60, i * 50 + 125, 75, 75);
                    int level = i * 5 + j + 1;
                    if (level <= Settings.LevelOn)
                    {
                        nb = new Button("defaultButton", dimensions, GetAction("BeginLevel", level.ToString()));
                        AddGuiElement(nb);
                        Rectangle textAreaDimensions = new Rectangle(dimensions.X+10,dimensions.Y+10, dimensions.Width-20, dimensions.Height-20);
                        AddGuiElement(new TextArea(textAreaDimensions, Assets.GetFont("pixel"), level.ToString(), Color.Black));
                    }
                    else
                    {
                        nb = new Button("x", dimensions);
                        AddGuiElement(nb);
                    }
                }
            }
        }

    }
}