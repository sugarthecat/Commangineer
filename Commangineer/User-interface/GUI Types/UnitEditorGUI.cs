using Commangineer.GUI_Element_Types;
using Commangineer.Units;
using Commangineer.User_Interface;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace Commangineer.GUI_Types
{
    /// <summary>
    /// A GUI representing the Unit Editor
    /// </summary>
    public class UnitEditorGUI : ScalingGUI
    {
        private UnitTemplate template;
        private Chassis[] chasses;
        private int chassisIndex = 0;
        private Engine[] engines;
        private int engineIndex = 0;
        private Weapon[] weapons;
        private Weapon[] selectedWeapons;
        private int[] selectedWeaponsIndex;

        /// <summary>
        /// Loads in the GUI's elements
        /// </summary>
        public UnitEditorGUI() : base(600, 400)
        {
            LoadElements("UnitEditorGUI");
            LoadIngredients();
            AddGuiElement(new Button("forge", new Rectangle(265, 305, 70, 30), delegate { Commangineer.Level.SpawnUnit(); }));
            AddGuiElement(new TextArea(new Rectangle(360, 305, 140, 30), Assets.GetFont("pixel"), "0 kmph"));
            //engine switch buttons
            AddGuiElement(new Button("left", new Rectangle(80, 305, 30, 30), delegate
            {
                engineIndex = (engineIndex + engines.Length - 1) % engines.Length;
                UpdateUnit();
            }));
            AddGuiElement(new Button("right", new Rectangle(220, 305, 30, 30), delegate
            {
                engineIndex = (engineIndex + 1) % engines.Length;
                UpdateUnit();
            }));
            //chassis switch buttons
            AddGuiElement(new Button("left", new Rectangle(80, 200, 30, 60), delegate
            {
                chassisIndex = (chassisIndex + chasses.Length - 1) % chasses.Length;
                UpdateUnit();
            }));
            AddGuiElement(new Button("right", new Rectangle(490, 200, 30, 60), delegate
            {
                chassisIndex = (chassisIndex + 1) % chasses.Length;
                UpdateUnit();
            }));
        }

        /// <summary>
        /// Sets up different parts of the unit
        /// </summary>
        private void LoadIngredients()
        {
            //lots of this remains unimplemented, but can be expanded in a future update.
            //time constraints limited unit editor implementation, unit spawn select created instead

            Engine engine1 = new Engine("engine1",
                    new MaterialBalance(20),
                    15, 250, 20);
            Engine engine2 = new Engine("engine2",
                    new MaterialBalance(60),
                    5, 800, 50);
            Engine engine3 = new Engine("engine3",
                    new MaterialBalance(55, 50),
                    6, 1800, 50);
            Engine engine4 = new Engine("engine4",
                    new MaterialBalance(25, 80,50),
                    7, 4000, 50);
            Weapon boltShooter = new Weapon("boltShooter", 5f, 15, 8, new MaterialBalance(5),TurretSize.Small, 0.8f);
            Weapon musket = new Weapon("musket", 8f, 20, 12, new MaterialBalance(15),TurretSize.Small, 0.8f);
            Weapon revolver = new Weapon("revolver", 1f, 5, 6, new MaterialBalance(0, 8), TurretSize.Small, 0.9f);
            Weapon cannon = new Weapon("cannon", 3f, 200, 8, new MaterialBalance(0, 0, 20), TurretSize.Medium, 1.5f);
            Weapon machineGun = new Weapon("machineGun", 0.1f, 8, 6, new MaterialBalance(0, 0, 20), TurretSize.Big,3f);
            Chassis model1 = new Chassis("model1", 0, 100, 50, 0.9f, new Slot[] { new Slot(new Vector2(0, 0), TurretSize.Small) }, new MaterialBalance(20));
            Chassis model2 = new Chassis("model2", 2, 800, 400, 1.5f, new Slot[] {
                new Slot(new Vector2(-0.35f,0.35f), TurretSize.Small),
                new Slot(new Vector2(-0.35f, -0.35f), TurretSize.Small),
                new Slot(new Vector2(0.4f,0), TurretSize.Medium) }, new MaterialBalance(5, 50));
            Chassis model3 = new Chassis("model3", 5, 4000, 2000, 3f, new Slot[] {
                new Slot(new Vector2(0f,0f), TurretSize.Big),
                new Slot(new Vector2(-0.6f,-0.6f), TurretSize.Small),
                new Slot(new Vector2(-0.6f,0.6f), TurretSize.Small),
                new Slot(new Vector2(0.6f,-0.6f), TurretSize.Small),
                new Slot(new Vector2(0.6f,0.6f), TurretSize.Small) }, new MaterialBalance(5, 400, 300));

            template = new UnitTemplate(model1, engine1, new Weapon[] { boltShooter });
            engines = new Engine[] { engine1, engine2,engine3,engine4 };
            chasses = new Chassis[] { model1, model2, model3 };
            weapons = new Weapon[] { boltShooter, musket, revolver, cannon, machineGun };
            selectedWeapons = new Weapon[] { boltShooter };
            selectedWeaponsIndex = new int[] { 0 };
            UpdateUnit();
        }

        /// <summary>
        /// Updates the GUI
        /// </summary>
        public override void Update()
        {
            ((TextArea)elements[3]).Text = Math.Round(template.Speed * 4, 2) + " kmph";
            base.Update();
        }

        /// <summary>
        /// Updates the display of the Unit in the GUI
        /// </summary>
        public void UpdateUnit()
        {
            if (selectedWeapons.Length != chasses[chassisIndex].Slots.Length)
            {
                selectedWeapons = new Weapon[chasses[chassisIndex].Slots.Length];
                selectedWeaponsIndex = new int[selectedWeapons.Length];
                for (int i = 0; i < selectedWeapons.Length; i++)
                {
                    selectedWeapons[i] = weapons[0];
                    selectedWeaponsIndex[i] = 0;
                }
                for(int i = 0; i<selectedWeapons.Length; i++)
                {
                    if (selectedWeapons[i].TurretSize != chasses[chassisIndex].Slots[i].TurretSize)
                    {
                        SwitchWeapon(i);
                    }
                }
            }
            template = new UnitTemplate(chasses[chassisIndex], engines[engineIndex], selectedWeapons);
            subGUIs = new System.Collections.Generic.List<GUI>() { new UnitTemplateSubGUI(template, delegate(int i) { SwitchWeapon(i); UpdateUnit(); }) };
        }

        /// <summary>
        /// Gets the current unit in the Unit Editor GUI
        /// </summary>
        public UnitTemplate GetUnit()
        {
            return template;
        }
        public void SwitchWeapon(int index)
        {
            if(index <  0 || index >= selectedWeapons.Length)
            {
                //out of range
                return;
            }
            do {
                selectedWeaponsIndex[index]++;
                if (selectedWeaponsIndex[index] >= weapons.Length)
                {
                    selectedWeaponsIndex[index] = selectedWeaponsIndex[index] % weapons.Length;
                }
                selectedWeapons[index] = weapons[selectedWeaponsIndex[index]];
            } while (chasses[chassisIndex].Slots[index].TurretSize != selectedWeapons[index].TurretSize);
        }
    }
}