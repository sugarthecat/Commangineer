using Commangineer.Units;
using Microsoft.Xna.Framework;

namespace Commangineer.GUI_Types
{
    public class UnitEditorGUI : ScalingGUI
    {
        private Weapon[] weapons;
        private Chassis[] unitBases;
        private Engine[] engines;

        public UnitEditorGUI() : base(600, 400)
        {
            LoadElements("UnitEditorGUI");
            LoadIngredients();
        }

        private void LoadIngredients()
        {
            //lots of this remains unimplemented, but can be expanded in a future update.
            //time constraints limited unit editor implementation, unit spawn select created instead

            weapons = new Weapon[] {
                new Weapon("boltShooter", 5f, 15, 8, new MaterialBalance(5)) };
            unitBases = new Chassis[] {
                new Chassis("model1", 0, 100, 50, 0.9f,
                    new Slot[]{
                                     new Slot( new Vector2(0.45f, 0.45f)) },
                    new MaterialBalance(20))
            };
            engines = new Engine[] {
                new Engine("model1e",
                    new MaterialBalance(20),
                    4, 50, 20)
            };
        }

        public override void Update()
        {
            base.Update();
        }

        public UnitTemplate GetUnit(int unitIndex)
        {
            Chassis finalChassis = unitBases[0].Clone();
            finalChassis.SetWeapon(0, weapons[0]);
            return new UnitTemplate(finalChassis, engines[0]);
        }
    }
}