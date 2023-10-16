using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer.Units
{
    internal class Weapon
    {
        private Unit.turretSize turretSize;
        private string name;
        private float reloadTime;
        private float reloadProgress;
        private float attackSpeed;
        private float attackProgress;
        int damage;
        int maxAmmo;
        int ammo;
        int range;
        int steamCost;

        public Weapon(string name, float reloadTime, float attackSpeed, int damage, int maxAmmo, int range, int steamCost)
        {
            this.name = name;
            this.reloadTime = reloadTime;
            this.attackSpeed = attackSpeed;
            this.damage = damage;
            this.maxAmmo = maxAmmo;
            this.range = range;
            this.steamCost = steamCost;
        }

        public Unit.turretSize GetTurretSize()
        {
            return turretSize;
        }
    }
}
