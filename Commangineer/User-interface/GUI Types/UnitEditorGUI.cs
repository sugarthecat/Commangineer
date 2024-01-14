using Commangineer.Units;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer.GUI_Types
{
    internal class UnitEditorGUI : ScalingGUI
    {

        public UnitEditorGUI() : base(600, 400)
        {
            LoadElements("UnitEditorGUI");
        }
        public override void Update()
        {
            base.Update();

        }
        public UnitTemplate currentUnit
        {
            get
            {
                Slot weaponSlot = new Slot(Unit.turretSize.medium, new Vector2(-0.5f, -0.5f));
                Slot weaponSlot2= new Slot(Unit.turretSize.medium, new Vector2(1f, 1f));
                weaponSlot.AddWeapon(new Weapon("turret", Unit.turretSize.medium, 0.5f, 0.5f, 2, 5, 5, 5));
                weaponSlot2.AddWeapon(new Weapon("turret", Unit.turretSize.medium, 0.5f, 0.5f, 2, 5, 5, 5));
                return new UnitTemplate("Base",
                    new Chassis("Chassis 1",2,2,2,2,new Slot[2] {weaponSlot, weaponSlot2}),
                    MaterialManager.GetMaterial(MaterialType.Scrap),
                    new Engine("Engine",5,5,5,5,5),
                    MaterialManager.GetMaterial(MaterialType.Scrap)
                    );
            }
        }
    }
}