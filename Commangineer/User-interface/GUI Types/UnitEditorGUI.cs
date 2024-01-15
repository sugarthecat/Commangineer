﻿using Commangineer.Units;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer.GUI_Types
{
    /// <summary>
    /// A GUI representing the Unit Editor
    /// </summary>
    internal class UnitEditorGUI : ScalingGUI
    {
        /// <summary>
        /// Loads in the GUI's elements
        /// </summary>
        public UnitEditorGUI() : base(600, 400)
        {
            LoadElements("UnitEditorGUI");
        }
        /// <summary>
        /// Updates the GUI
        /// </summary>
        public override void Update()
        {
            base.Update();

        }
        /// <summary>
        /// Gets the current unit in the Unit Editor GUI
        /// </summary>
        public UnitTemplate currentUnit
        {
            get
            {
                Slot weaponSlot = new Slot(Unit.turretSize.medium, new Vector2(-0.5f, 0));
                weaponSlot.AddWeapon(new Weapon("boltShooter", Unit.turretSize.medium, 0.5f, 0.5f, 2, 5, 15, 5));
                return new UnitTemplate("Base",
                    new Chassis("Chassis 1",200,2,2,2,new Slot[1] {weaponSlot}),
                    MaterialManager.GetMaterial(MaterialType.Scrap),
                    new Engine("Engine",5,5,100,100,5),
                    MaterialManager.GetMaterial(MaterialType.Scrap)
                    );
            }
        }
    }
}