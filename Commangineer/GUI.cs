using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer
{
    public class GUI
    {
        protected List<GUIElement> elements;
        public GUI()
        {
            elements = new List<GUIElement>();
        }
        public void AddGuiElement(GUIElement element)
        {
            elements.Add(element);
        }
        public void RemoveGuiElement(GUIElement element)
        {
            elements.Remove(element);
        }
        public GUIElement GetElement(int index)
        {
            return elements[index];
        }
        public void RemoveAllGuiElements()
        {
            elements.Clear();
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                elements[i].Draw(spriteBatch);
            }
        }
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
