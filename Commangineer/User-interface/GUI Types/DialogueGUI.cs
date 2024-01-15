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
using Commangineer.User_Interface;

namespace Commangineer.GUI_Types
{
    /// <summary>
    /// The GUI object for dialogue
    /// </summary>`
    internal class DialogueGUI : ScalingGUI
    {
        private (GUIElement element, string character) characterOne;
        private (GUIElement element, string character) characterTwo;
        private List<TextArea> textAreas;

        /// <summary>
        /// Loads in the GUI's elements
        /// </summary>
        public DialogueGUI() : base(600, 400)
        {
            textAreas = new List<TextArea>();
            Enabled = false;
            LoadElements("DialogueGUI");
        }

        /// <summary>
        /// Remove current text
        /// </summary>
        private void ClearText()
        {
            foreach (TextArea textElement in textAreas)
            {
                RemoveGuiElement(textElement);
            }
            textAreas.Clear();
        }

        /// <summary>
        /// Changes current text
        /// </summary>
        /// <param name="newText">The new text</param>
        public void ChangeText(string newText)
        {
            ClearText();
            TextArea newTextElement = new TextArea(new Rectangle(100, 300, 550, 50), Assets.GetFont("pixel"), newText);
            textAreas.Add(newTextElement);
            AddGuiElement(newTextElement);
        }

        /// <summary>
        /// Removes a character from the dialogue
        /// </summary>
        /// <param name="position">Which character to remove</param>
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

        /// <summary>
        /// Removes all characters from the dialogue
        /// </summary>
        public void RemoveAllCharacters()
        {
            RemoveCharacter(1);
            RemoveCharacter(2);
        }

        /// <summary>
        /// Swaps a character with another one
        /// </summary>
        /// <param name="toChange">The old character's element and name</param>
        /// <param name="newChar">The new characters name</param>
        /// <param name="elementPosition">The position for the new character</param>
        /// <param name="focused">If the character is in focus or not</param>
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

        /// <summary>
        /// Changes a character with another
        /// </summary>
        /// <param name="position">Which character to change</param>
        /// <param name="newChar">The new character's name</param>
        /// <param name="focused">If the new character is in focus</param>
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

        /// <summary>
        /// Changes the focus of a character
        /// </summary>
        /// <param name="position">Which character to change</param>
        /// <param name="focused">If the character is now focused or not</param>
        public void FocusCharacter(int position, bool focused)
        {
            // Just swaps the character with itself except this time focused
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