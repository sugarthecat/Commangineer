using Commangineer.GUI_Element_Types;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Collections.Generic;

namespace Commangineer.GUI_Types
{
    /// <summary>
    /// The GUI object for the title screen
    /// </summary>`
    internal class DialogueGUI : ScalingGUI
    {
        private GUIElement characterOne;
        private GUIElement characterTwo;
        private List<TextArea> textAreas;
        public DialogueGUI() : base(600, 400)
        {
            SetEnabled(false);
        }

        private void ClearText()
        {
            foreach (TextArea textElement in textAreas)
            {
                RemoveGuiElement(textElement);
            }
            textAreas.Clear();
        }

        public void ChangeText(string newText)
        {
            ClearText();
            TextArea newTextElement = new TextArea(new Rectangle(25, 300, 550, 50), Assets.GetFont("pixel"), newText);
            textAreas.Add(newTextElement);
            AddGuiElement(newTextElement);
        }

        public void ChangeCharacter(int position, Character newChar, bool focused)
        {

        }

        public void FocusCharacter(int position, bool focused)
        {

        }
    }
}