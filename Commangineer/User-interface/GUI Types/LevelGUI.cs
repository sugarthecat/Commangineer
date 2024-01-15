using Commangineer.GUI_Element_Types;
using Commangineer.User_Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.ComponentModel;

namespace Commangineer.GUI_Types
{
    /// <summary>
    /// A GUI + The level
    /// </summary>
    internal class LevelGUI : ScalingGUI
    {
        MaterialType[] materialTypes = Enum.GetValues<MaterialType>();

        /// <summary>
        /// Creates the Level GUI and loads in basic elements
        /// </summary>
        public LevelGUI() : base(600,400)
        {
            //add resource displays
            // add display for each material
            AddGuiElement(new GUIElement(Assets.GetImage("default"), new Rectangle(0, 0, materialTypes.Length*60, 30)));
            for (int i = 0; i < materialTypes.Length; i++)
            {
                AddGuiElement(new GUIElement(Assets.GetTexture(MaterialManager.GetMaterial(materialTypes[i]).Name), new Rectangle(i * 60, 0, 30, 30)));
                AddGuiElement(new TextArea(new Rectangle(i*60+30,0,30,30),Assets.GetFont("pixel"),"2"));
            }
        }
        /// <summary>
        /// Updates the Level GUI
        /// </summary>
        /// <param name="resources">Resources the user has access to</param>
        public void Update(MaterialBalance resources)
        {
            base.Update();
            for(int i = 0;i < materialTypes.Length;i++)
            {
                GUIElement textSpot = GetElement(i*2+2);
                if(textSpot is TextArea)
                {
                    ((TextArea)textSpot).Text = resources[materialTypes[i]].ToString();
                }
            }
        }
    }
}