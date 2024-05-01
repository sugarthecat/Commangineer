using Commangineer.GUI_Element_Types;
using Commangineer.Units;
using Commangineer.User_Interface;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;
using System.Linq;

namespace Commangineer.GUI_Types
{
    /// <summary>
    /// A GUI representing the Unit Editor's template preview
    /// </summary>
    public class UnitTemplateSubGUI : ScalingGUI
    {
        
        /// <summary>
        /// Loads in the GUI's elements
        /// </summary>
        public UnitTemplateSubGUI( UnitTemplate template) : base(600, 400)
        {
            MaterialType[] materialTypes = Enum.GetValues<MaterialType>();
            AddGuiElement(new GUIElement(Assets.GetImage("basicFrame"), new Rectangle(180, 50, materialTypes.Length * 80, 40)));
            for (int j = 0; j < materialTypes.Length; j++)
            {
                AddGuiElement(new GUIElement(Assets.GetTexture(MaterialManager.GetMaterial(materialTypes[j]).Name), new Rectangle(180 + j * 80, 50, 40, 40)));
                AddGuiElement(new TextArea(new Rectangle(j * 80 + 40 + 180, 50, 40, 40), Assets.GetFont("pixel"), template.MaterialCost[materialTypes[j]].ToString()));
            }

            //size x and y are the same, so this is okay.
            double scaleFactor = 115d/Math.Sqrt(template.Chassis.Size.X);
            int size = (int)(Math.Sqrt(template.Chassis.Size.X) * 115);
            AddGuiElement(new GUIElement(template.Chassis.Texture, new Rectangle(300 -size/2,200 - size/2,size,size)));

            for(int i = 0; i < template.Slots.Length; i++)
            {
                AddGuiElement(new GUIElement(template.Chassis.Texture, new Rectangle(
                    (int)(300 + scaleFactor * template.Slots[i].Position.X),
                    200, 50, 50)));
            }
        }
    }
}