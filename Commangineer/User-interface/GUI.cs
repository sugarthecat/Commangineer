using Commangineer.GUI_Element_Types;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json.Nodes;

namespace Commangineer.User_Interface
{
    /// <summary>
    /// A graphical user interface to display with the user.
    /// </summary>
    public class GUI
    {
        protected List<GUIElement> elements;
        protected List<GUI> subGUIs;
        private bool enabled;

        /// <summary>
        /// Constructs a GUI
        /// </summary>
        public GUI()
        {
            elements = new List<GUIElement>();
            subGUIs = new List<GUI>();
            enabled = true;
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
        /// Adds a subGUI to subGUIs
        /// </summary>
        /// <param name="newGUI"></param>The new subGui to add
        public void AddSubGUI(GUI newGUI)
        {
            subGUIs.Add(newGUI);
        }

        /// <summary>
        /// Removes a subGUI from subGUIs
        /// </summary>
        /// <param name="oldGUI"></param>The GUI to remove
        public void RemoveSubGUI(GUI oldGUI)
        {
            subGUIs.Remove(oldGUI);
        }

        /// <summary>
        /// Gets a sub gui at the given index
        /// </summary>
        /// <param name="index"></param>The index of the subgui
        /// <returns></returns>
        public GUI GetSubGUI(int index)
        {
            return subGUIs[index];
        }

        /// <summary>
        /// Clears out the GUIs in subGUIs
        /// </summary>
        public void RemoveAllSubGuis()
        {
            subGUIs.Clear();
        }

        /// <summary>
        /// Gets and sets if the gui is enabled
        /// </summary>
        public bool Enabled
        {
            get
            {
                return enabled;
            }
            set
            {
                enabled = value;
            }
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
            for (int i = 0; i < subGUIs.Count; i++)
            {
                if (subGUIs[i].enabled)
                {
                    subGUIs[i].Draw(spriteBatch);
                }
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
        /// Reads a JSON file containing objects to draw and converts it to a JsonNode
        /// </summary>
        public JsonNode ReadAsync(string fileName)
        {
            JsonNode res = null;
            try
            {
                string sources = Assembly.GetExecutingAssembly().Location + "/../Content";
                string text = string.Join("", File.ReadAllLines(sources + "/scenes/" + fileName + "/default.json").Select(x => x.Trim()).ToArray());
                res = JsonNode.Parse(text);
            }
            catch (Exception ex)
            {
                Log.LogText("Error loading file " + ex.Message);
            }
            return res;
        }

        /// <summary>
        /// Updates the GUI
        /// </summary>
        public virtual void Update()
        {
        }

        /// <summary>
        /// Returns a Action dependent on the parameters
        /// </summary>
        /// <param name="actionName"></param> The name of the Action
        /// <param name="actionValue"></param> A variable value for a Action to use
        /// <returns></returns>
        public Action GetAction(string actionName, string actionValue)
        {
            Action res = null;
            switch (actionName)
            {
                case "NavigateToMenu":
                    res = delegate { Commangineer.instance.NavigateToMenu(actionValue); };
                    break;

                case "EmailCrash":
                    res = delegate { Commangineer.instance.EmailCrash(); };
                    break;

                case "Quit":
                    res = delegate { Commangineer.ExitGame(); };
                    break;

                case "ToggleSettings":
                    res = delegate { Commangineer.instance.ToggleSettings(); };
                    break;

                case "ToggleFullscreen":
                    res = delegate { Commangineer.instance.ToggleFullscreen(); };
                    break;

                case "ToggleMusic":
                    res = delegate { Settings.MusicEnabled = !Settings.MusicEnabled; };
                    break;

                case "ExitUnitEditor":
                    res = delegate { Commangineer.Level.ExitUnitEditor(); };
                    break;

                case "SpawnUnit":
                    res = delegate { Commangineer.Level.SpawnUnit(0); };
                    break;

                default:
                    res = delegate { Log.LogText(actionName + "does not have a valid action"); };
                    break;
            }
            return res;
        }

        /// <summary>
        /// Loads in all elements from a file to the screen
        /// </summary>
        /// <param name="fileName"></param> The name of the json file containing the elements
        public void LoadElements(string fileName)
        {
            JsonNode data = ReadAsync(fileName);
            if (data != null)
            {
                foreach (KeyValuePair<string, JsonNode> keyValuePair in (JsonObject)data["objects"])
                {
                    try
                    {
                        JsonObject properties = keyValuePair.Value.AsObject();
                        if (properties["type"].ToString() == "GUIElement")
                        {
                            JsonArray position = properties["position"].AsArray();
                            JsonArray size = properties["size"].AsArray();
                            if (properties.ContainsKey("color"))
                            {
                                Color c;
                                var prop = typeof(Color).GetProperty(properties["color"].ToString());
                                if (prop != null)
                                {
                                    c = (Color)prop.GetValue(null, null);
                                }
                                else
                                {
                                    c = Color.White;
                                }
                                AddGuiElement(new GUIElement(Assets.GetImage(properties["name"].ToString()),
                                    new Rectangle((int)position[0], (int)position[1], (int)size[0], (int)size[1]),
                                    c));
                            }
                            else if (properties.ContainsKey("hoverTexture"))
                            {
                                AddGuiElement(new GUIElement(Assets.GetImage(properties["name"].ToString()),
                                    Assets.GetImage(properties["hoverTexture"].ToString()),
                                    new Rectangle((int)position[0], (int)position[1], (int)size[0], (int)size[1]),
                                    GetAction(properties["actionName"].ToString(), properties["actionValue"].ToString())));
                            }
                            else if (properties.ContainsKey("actionOnActivate"))
                            {
                                AddGuiElement(new GUIElement(Assets.GetImage(properties["name"].ToString()),
                                    new Rectangle((int)position[0], (int)position[1], (int)size[0], (int)size[1])));
                            }
                            else
                            {
                                AddGuiElement(new GUIElement(Assets.GetImage(properties["name"].ToString()),
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
                        Log.LogText("Error reading JSON object from file: " + ex.Message);
                    }
                }
            }
        }
    }
}