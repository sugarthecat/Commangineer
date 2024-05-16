using Commangineer.GUI_Element_Types;
using Commangineer.Units;
using Commangineer.User_Interface;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace Commangineer.GUI_Types
{
    /// <summary>
    /// A GUI representing the Unit Editor
    /// </summary>
    public class UnitEditorAlertSubGUI : ScalingGUI
    {

        /// <summary>
        /// Loads in the GUI's elements
        /// </summary>
        public UnitEditorAlertSubGUI(string message, Color alertColor) : base(600, 400)
        {
            AddGuiElement(new GUIElement(Assets.GetImage("basicFrame"), new Rectangle(250, 270, 100, 25)));
            AddGuiElement(new TextArea(new Rectangle(255, 270, 90, 25), Assets.GetFont("pixel"), message, alertColor));
        }

    }
}