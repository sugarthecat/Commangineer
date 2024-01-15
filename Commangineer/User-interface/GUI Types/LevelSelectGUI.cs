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
        public LevelSelectGUI(int maxLevel) : base(600, 400)
        {
            LoadElements("LevelSelectGUI");
            for (int i = 0; i < 1; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Button nb;
                    Rectangle dimensions = new Rectangle(j * 100 + 60, i * 50 + 125, 75, 75);
                    int level = i * 5 + j + 1;
                    if (level <= maxLevel+1)
                    {
                        nb = new Button("defaultButton", dimensions, GetAction("BeginLevel", level.ToString()));
                        // draw a number over the button

                        //
                    }
                    else
                    {
                        nb = new Button("x", dimensions, GetAction("BeginLevel", "level"));
                    }
                    AddGuiElement(nb);
                }
            }
        }

    }
}