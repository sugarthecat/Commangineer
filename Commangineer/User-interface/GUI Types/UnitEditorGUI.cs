using Commangineer.GUI_Element_Types;
using Commangineer.Units;
using Commangineer.User_Interface;
using Microsoft.Xna.Framework;
using System;

namespace Commangineer.GUI_Types
{
    public class UnitEditorGUI : ScalingGUI
    {
        private Weapon[] weapons;
        private Chassis[] unitBases;
        private Engine[] engines;
        private UnitTemplate[] templates;

        public UnitEditorGUI() : base(600, 400)
        {
            LoadElements("UnitEditorGUI");
            LoadIngredients();
            MaterialType[] materialTypes = Enum.GetValues<MaterialType>();
            for (int i = 0; i<templates.Length; i++)
            {
                int height = 90 + i * 50;
                int leftOffset = 80;
                AddGuiElement(new GUIElement(Assets.GetImage("basicFrame"), new Rectangle(leftOffset, height, materialTypes.Length * 60, 30)));
                for (int j = 0; j < materialTypes.Length; j++)
                {
                    AddGuiElement(new GUIElement(Assets.GetTexture(MaterialManager.GetMaterial(materialTypes[j]).Name), new Rectangle(leftOffset+j * 60, height, 30, 30)));
                    AddGuiElement(new TextArea(new Rectangle(j * 60 + 30 + leftOffset, height, 30, 30), Assets.GetFont("pixel"), templates[i].MaterialCost[materialTypes[j]].ToString()));
                }
                int copy = i;
                AddGuiElement(new Button("forge", new Rectangle(10+leftOffset + materialTypes.Length*60, height,70,30), delegate { Commangineer.Level.SpawnUnit(copy); }));
            }
        }

        private void LoadIngredients()
        {
            //lots of this remains unimplemented, but can be expanded in a future update.
            //time constraints limited unit editor implementation, unit spawn select created instead

            unitBases = new Chassis[] {
            };
            Engine engine1 = new Engine("engine1",
                    new MaterialBalance(20), 
                    2, 50, 20);
            Engine engine2 = new Engine("engine2",
                    new MaterialBalance(5, 20),
                    5, 800, 50);

            Weapon boltShooter = new Weapon("boltShooter", 5f, 15, 8, new MaterialBalance(5));
            Weapon musket = new Weapon("musket", 8f, 20, 12, new MaterialBalance(15));
            Weapon revolver = new Weapon("revolver", 1f, 5, 6, new MaterialBalance(0,8),2f);
            Weapon cannon = new Weapon("cannon", 3f, 200, 8, new MaterialBalance(0, 0, 20), 2f);
            Weapon machineGun = new Weapon("machineGun", 0.1f, 8, 6, new MaterialBalance(0, 0, 20),3f);
            Chassis model1 = new Chassis("model1", 0, 100, 50, 0.9f, new Slot[] { new Slot(new Vector2(0, 0)) }, new MaterialBalance(20));
            Chassis model2 = new Chassis("model2", 2, 300, 400, 1.5f, new Slot[] { 
                new Slot(new Vector2(-0.35f,0.35f)), 
                new Slot(new Vector2(-0.35f, -0.35f)), 
                new Slot(new Vector2(0.4f,0)) }, new MaterialBalance(5, 50));
            Chassis model3 = new Chassis("model3", 5, 300, 2000, 3f, new Slot[] { 
                new Slot(new Vector2(0.3f, 0.3f)), 
                new Slot(new Vector2(1.2f, 0.3f)), 
                new Slot(new Vector2(0.75f, 1.2f)) }, new MaterialBalance(5, 400, 300));
            templates = new UnitTemplate[] {
                new UnitTemplate(model1, engine1, new Weapon[] { boltShooter } ),
                new UnitTemplate(model1, engine2, new Weapon[] { musket } ),
                new UnitTemplate(model2, engine2, new Weapon[] { revolver } ),
                new UnitTemplate(model2, engine2, new Weapon[] { cannon } ),
                new UnitTemplate(model3, engine2, new Weapon[] { machineGun } ),
            };
        }

        public override void Update()
        {
            base.Update();
        }

        public UnitTemplate GetUnit(int unitIndex)
        {
            if(unitIndex >= templates.Length)
            {
                return null;
            }
            return templates[unitIndex];
        }
    }
}