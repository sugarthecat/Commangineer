﻿using Commangineer.GUI_Element_Types;
using Commangineer.User_Interface;
using Microsoft.Xna.Framework;
using System.Text.RegularExpressions;

namespace Commangineer.GUI_Types
{
    /// <summary>
    /// The GUI object for any critical errors
    /// </summary>`
    public class ErrorGUI : ScalingGUI
    {
        /// <summary>
        /// Creates a new error screen, loading in its elements
        /// </summary>
        public ErrorGUI(string msg, string info) : base(600, 400)
        {
            LoadElements("ErrorGUI");
            int charsPerLine = 50;
            int lineSize = 100 / (msg.Length / charsPerLine + 1);
            string[] words = Regex.Replace(msg, " {2,}", " ").Split(' ');
            string curLine = "";
            int lines = 1;
            foreach (string word in words)
            {
                string tempLine = curLine + word + " ";
                if (tempLine.Length > charsPerLine || word == words[words.Length - 1])
                {
                    AddGuiElement(new TextArea(new Rectangle(25, lines * lineSize, 550, lineSize), Assets.GetFont("pixel"), tempLine));
                    curLine = "";
                    lines++;
                }
                else
                {
                    curLine = tempLine;
                }
            }
            AddGuiElement(new GUIElement(Assets.GetImage("errorMsg"), new Rectangle(100, 150, 400, 100)));
        }
    }
}