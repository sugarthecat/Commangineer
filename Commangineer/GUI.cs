using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Commangineer
{
    /// <summary>
    /// A graphical user interface to display with the user.
    /// </summary>
    public class GUI
    {
        protected List<GUIElement> elements;

        public GUI()
        {
            elements = new List<GUIElement>();
        }

        /// <summary>
        /// Adds a GUIElement to the GUI
        /// </summary>
        /// <param name="element">The GUIElement to add</param>
        public void AddGuiElement(GUIElement element)
        {
            elements.Add(element);
        }

        /// <summary>
        /// Removes a GUIElement from the GUI
        /// </summary>
        /// <param name="element">the GUIElement</param>
        public void RemoveGuiElement(GUIElement element)
        {
            elements.Remove(element);
        }

        /// <summary>
        /// Gets a GUIElement at index
        /// </summary>
        /// <param name="index">The index to get the GUIElement from</param>
        /// <returns>The GUIElement</returns>
        public GUIElement GetElement(int index)
        {
            return elements[index];
        }

        /// <summary>
        /// Clears out the GUIElements in the GUI
        /// </summary>
        public void RemoveAllGuiElements()
        {
            elements.Clear();
        }

        /// <summary>
        /// Draws all elements within the GUI
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                elements[i].Draw(spriteBatch);
            }
        }

        /// <summary>
        /// Handles a click at a given point
        /// </summary>
        /// <param name="clickPoint">The point that has been clicked</param>
        /// <returns>If an action has occured</returns>
        public virtual bool HandleClick(Point clickPoint)
        {
            bool activated = false;
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].HandleClick(clickPoint))
                {
                    activated = true;
                };
            }
            return activated;
        }
    }
}