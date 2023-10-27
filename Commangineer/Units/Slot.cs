using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer.Units
{
    internal class Slot
    {

        private Weapon weapon;
        private Unit.turretSize size;

        public Slot(Unit.turretSize s)
        {
            size = s;
        }

        /// <summary>
        /// atempts to add weapon to slot and returns a bool
        /// </summary>
        /// <param name="w">weapon to add to slot</param>
        /// <returns>true if weapon successfully added else false</returns>
        public bool AddWeapon(Weapon w)
        {
            if (w.GetTurretSize() == size && weapon == null)
            {
                weapon = new Weapon(w);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Update(double time)
        {
            weapon.Update(time);
        }
    }
}
