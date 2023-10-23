﻿using Commangineer.GUI_Element_Types;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

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

        /// <summary>
        /// Reads a JSON file containing objects to draw and converts it to a list
        /// </summary>        
        public JsonNode ReadAsync(string fileName)
        {
            JsonNode res = null;
            try
            {
                string sources = Assembly.GetExecutingAssembly().Location + "/../../../../Content";
                string text = String.Join("", File.ReadAllLines(sources + "/scenes/" + fileName + "/default.json").Select(x => x.Trim()).ToArray());
                res = JsonObject.Parse(text);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error loading file " + ex.Message);
            }
            return res;
        }

        public Action GetAction(string t, string v)
        {
            Action res = null;
            switch (t)
            {
                case "NavigateToMenu":
                    res = delegate { Commangineer.instance.NavigateToMenu(v); };
                    break;
                case " ":
                    break;
            }
            return res;
        }

        public void LoadElements(string fileName)
        {
            JsonNode data = ReadAsync(fileName);
            if (data != null)
            {
                foreach (KeyValuePair<string, JsonNode> kvp in (JsonObject)data["objects"])
                {
                    try
                    {
                        JsonObject properties = kvp.Value.AsObject();
                        if (properties["type"].ToString() == "GUIElement")
                        {
                            JsonArray position = properties["position"].AsArray();
                            JsonArray size = properties["size"].AsArray();
                            if (properties.ContainsKey("color"))
                            {
                                Color c;
                                var prop = typeof(Color).GetProperty("");
                                if (prop != null)
                                {
                                    c = (Color)prop.GetValue(null, null);
                                }
                                else
                                {
                                    c = Color.White;
                                }
                                AddGuiElement(new GUIElement(Assets.GetTexture2D(properties["name"].ToString()),
                                    new Rectangle((int)position[0], (int)position[1], (int)size[0], (int)size[1]), 
                                    c));
                            }
                            else if (properties.ContainsKey("hoverTexture"))
                            {
                                AddGuiElement(new GUIElement(Assets.GetTexture2D(properties["name"].ToString()),
                                    Assets.GetTexture2D(properties["hoverTexture"].ToString()),
                                    new Rectangle((int)position[0], (int)position[1], (int)size[0], (int)size[1]),
                                    GetAction(properties["actionName"].ToString(), properties["actionValue"].ToString())));
                            }
                            else if (properties.ContainsKey("actionOnActivate"))
                            {
                                AddGuiElement(new GUIElement(Assets.GetTexture2D(properties["name"].ToString()),
                                    new Rectangle((int)position[0], (int)position[1], (int)size[0], (int)size[1])));
                            }
                            else
                            {
                                AddGuiElement(new GUIElement(Assets.GetTexture2D(properties["name"].ToString()),
                                    new Rectangle((int)position[0], (int)position[1], (int)size[0], (int)size[1])));
                            }
                        }
                        else if (properties["type"].ToString() == "Button")
                        {

                            JsonArray position = properties["position"].AsArray();
                            JsonArray size = properties["size"].AsArray();
                            if (properties.ContainsKey("actionName"))
                            {
                                AddGuiElement(new Button(properties["name"].ToString(),
                                    new Rectangle((int)position[0], (int)position[1], (int)size[0], (int)size[1]),
                                    GetAction(properties["actionName"].ToString(), properties["actionValue"].ToString())));
                            }
                            else
                            {
                                AddGuiElement(new Button(properties["name"].ToString(),
                                new Rectangle((int)position[0], (int)position[1], (int)size[0], (int)size[1])));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Error reading JSON object from file: " + ex.Message);
                    }
                }
            }
        }
    }
}