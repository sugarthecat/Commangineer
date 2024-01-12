using Commangineer.GUI_Element_Types;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Drawing;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace Commangineer.GUI_Types
{
    /// <summary>
    /// The GUI object for the title screen
    /// </summary>`
    internal class DialogueGUI : ScalingGUI
    {
        private (GUIElement element, string character) characterOne;
        private (GUIElement element, string character) characterTwo;
        private List<TextArea> textAreas;
        public DialogueGUI() : base(600, 400)
        {
            textAreas = new List<TextArea>();
            SetEnabled(false);
            LoadElements("DialogueGUI");
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
            TextArea newTextElement = new TextArea(new Rectangle(100, 300, 550, 50), Assets.GetFont("pixel"), newText);
            textAreas.Add(newTextElement);
            AddGuiElement(newTextElement);
        }

        public void RemoveCharacter(int position)
        {
            if (position == 1)
            {
                RemoveGuiElement(characterOne.element);
            }
            else
            {
                RemoveGuiElement(characterTwo.element);
            }
        }

        public void RemoveAllCharacters()
        {
            RemoveCharacter(1);
            RemoveCharacter(2);
        }

        private void SwapCharacter(ref (GUIElement element, string character) toChange, string newChar, (int,int) elementPosition, bool focused)
        {
            Color toSet = Color.White;
            if (!focused)
            {
                toSet = Color.Gray;
            }
            if (toChange.element != null)
            {
                RemoveGuiElement(toChange.element);
            }
            GUIElement newElement = new GUIElement(Assets.GetImage(newChar.ToString()), new Rectangle(elementPosition.Item1, elementPosition.Item2, 150, 150), toSet);
            AddGuiElement(newElement);
            toChange = (newElement, newChar);
        }

        public void ChangeCharacter(int position, string newChar, bool focused)
        {
            if (position == 1)
            {
                SwapCharacter(ref characterOne, newChar, (0, 280), focused);
            }
            else
            {
                SwapCharacter(ref characterTwo, newChar, (650, 280), focused);
            }
        }

        public void FocusCharacter(int position, bool focused)
        {
            if (position == 1)
            {
                SwapCharacter(ref characterOne, characterOne.character, (0, 280), focused);
            }
            else
            {
                SwapCharacter(ref characterTwo, characterTwo.character, (650, 280), focused);
            }
        }
    }
}