using Commangineer.GUI_Element_Types;
using Commangineer.User_Interface;
using Microsoft.Xna.Framework;
using System;

namespace Commangineer.GUI_Types
{
    /// <summary>
    /// A GUI + The level
    /// </summary>
    public class LevelGUI : ScalingGUI
    {
        private MaterialType[] materialTypes = Enum.GetValues<MaterialType>();

        public LevelGUI() : base(600, 400)
        {
            //add resource displays
            // add display for each material
            AddGuiElement(new GUIElement(Assets.GetImage("basicFrame"), new Rectangle(0, 0, materialTypes.Length * 60, 30)));
            for (int i = 0; i < materialTypes.Length; i++)
            {
                AddGuiElement(new GUIElement(Assets.GetTexture(MaterialManager.GetMaterial(materialTypes[i]).Name), new Rectangle(i * 60, 0, 30, 30)));
                AddGuiElement(new TextArea(new Rectangle(i * 60 + 30, 0, 30, 30), Assets.GetFont("pixel"), "2"));
            }
        }

        public void Update(MaterialBalance resources)
        {
            base.Update();
            for (int i = 0; i < materialTypes.Length; i++)
            {
                GUIElement textSpot = GetElement(i * 2 + 2);
                if (textSpot is TextArea)
                {
                    ((TextArea)textSpot).Text = resources[materialTypes[i]].ToString();
                }
            }
        }
    }
}