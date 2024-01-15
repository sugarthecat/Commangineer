using Commangineer.Units;
using Microsoft.Xna.Framework;

namespace Commangineer.GUI_Types
{
    /// <summary>
    /// A GUI representing the Unit Editor
    /// </summary>
    public class UnitEditorGUI : ScalingGUI
    {
        private Weapon[] weapons;
        private Chassis[] unitBases;
        private Engine[] engines;

        /// <summary>
        /// Loads in the GUI's elements
        /// </summary>
        public UnitEditorGUI() : base(600, 400)
        {
            LoadElements("UnitEditorGUI");
            LoadIngredients();
        }

        /// <summary>
        /// Sets up different parts of the unit
        /// </summary>
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
        public UnitTemplate GetUnit(int unitIndex)
        {
            Chassis finalChassis = unitBases[0].Clone();
            finalChassis.SetWeapon(0, weapons[0]);
            return new UnitTemplate(finalChassis, engines[0]);
        }
    }
}