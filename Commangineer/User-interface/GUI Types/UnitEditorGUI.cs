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
                    new MaterialBalance(25, 80, 50),
                    7, 4000, 50);
            Color yellowBullet = new Color(255, 255, 0);
            Color redBullet = new Color(255, 50, 0);
            Color blueBullet = new Color(0,150,255);
            Weapon boltShooter = new Weapon("boltShooter", 5f, 15, 10, new MaterialBalance(5),TurretSize.Small,yellowBullet, 0.8f);
            Weapon musket = new Weapon("musket", 8f, 40, 12, new MaterialBalance(15), TurretSize.Small, yellowBullet, 0.8f);
            Weapon revolver = new Weapon("revolver", 1f, 5, 8, new MaterialBalance(4, 8), TurretSize.Small, yellowBullet, 0.9f);
            Weapon miniFlame = new Weapon("miniFlamethrower", 0.2f, 2, 5, new MaterialBalance(12,20), TurretSize.Small, redBullet, 1f);
            Weapon cannon = new Weapon("cannon", 3f, 200, 16, new MaterialBalance(40, 30,0), TurretSize.Medium, yellowBullet, 1.5f);
            Weapon peppergun = new Weapon("peppergun", 0.2f, 5, 12, new MaterialBalance(60, 20, 0), TurretSize.Medium, yellowBullet, 1.6f);
            Weapon flamethrower = new Weapon("flamethrower", 0.15f, 8, 7, new MaterialBalance(60, 20, 20), TurretSize.Medium, redBullet, 1.6f);
            Weapon machineGun = new Weapon("machineGun", 0.1f, 10, 8, new MaterialBalance(50,100,0), TurretSize.Big, yellowBullet, 3f);
            Weapon arcThrower = new Weapon("arcProjector",1.5f, 400, 14, new MaterialBalance(20, 70, 50), TurretSize.Big, blueBullet, 2.8f);
            Chassis model1 = new Chassis("model1", 0, 100, 50, 0.9f, new Slot[] { new Slot(new Vector2(0, 0), TurretSize.Small) }, new MaterialBalance(20));
            Chassis model4 = new Chassis("model4", 0, 100, 50, 1.3f, new Slot[] {
                new Slot(new Vector2(-0.4f, 0), TurretSize.Small),
                new Slot(new Vector2(0.35f, 0), TurretSize.Medium)
            }, new MaterialBalance(50, 20));
            Chassis model2 = new Chassis("model2", 2, 800, 400, 1.5f, new Slot[] {
                new Slot(new Vector2(-0.35f,0.35f), TurretSize.Small),
                new Slot(new Vector2(-0.35f, -0.35f), TurretSize.Small),
                new Slot(new Vector2(0.4f,0), TurretSize.Medium) }, new MaterialBalance(15, 50));
            Chassis model3 = new Chassis("model3", 5, 4000, 2000, 3f, new Slot[] {
                new Slot(new Vector2(0f,0f), TurretSize.Big),
                new Slot(new Vector2(-0.65f,-0.65f), TurretSize.Small),
                new Slot(new Vector2(-0.65f,0.65f), TurretSize.Small),
                new Slot(new Vector2(0.65f,-0.65f), TurretSize.Small),
                new Slot(new Vector2(0.65f,0.65f), TurretSize.Small) }, new MaterialBalance(15, 400, 100));
            Chassis model5 = new Chassis("model5", 5, 4000, 2000, 3.5f, new Slot[] {
                new Slot(new Vector2(-0.45f,-0.45f), TurretSize.Medium),
                new Slot(new Vector2(-0.45f,0.45f), TurretSize.Medium),
                new Slot(new Vector2(0.45f,-0.45f), TurretSize.Medium),
                new Slot(new Vector2(0.45f,0.45f), TurretSize.Medium) }, new MaterialBalance(15, 300, 200));

            template = new UnitTemplate(model1, engine1, new Weapon[] { boltShooter });
            engines = new Engine[] { engine1, engine2,engine3,engine4 };
            chasses = new Chassis[] { model1, model4, model2, model3};
            weapons = new Weapon[] { boltShooter, musket, revolver, miniFlame, cannon, peppergun, flamethrower, machineGun ,arcThrower};
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