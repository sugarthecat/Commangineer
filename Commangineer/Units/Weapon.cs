using Commangineer.Auuki;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer.Units
{
    internal class Weapon
    {
        private string name;
        private Unit.turretSize turretSize;
        private double reloadTime;
        private double reloadProgress;
        private double attackSpeed;
        private double attackTime;
        private double attackProgress;
        private int damage;
        private int maxAmmo;
        private int ammo;
        private int range;
        private int steamCost;
        private bool shooting;
        private AuukiCreature target;
        private Texture2D texture;
        public string Name { get => name; set => name = value; }
        internal Unit.turretSize TurretSize { get => turretSize; set => turretSize = value; }
        public double ReloadTime { get => reloadTime; set => reloadTime = value; }
        public double ReloadProgress { get => reloadProgress; set => reloadProgress = value; }
        public double AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
        public double AttackTime { get => attackTime; set => attackTime = value; }
        public double AttackProgress { get => attackProgress; set => attackProgress = value; }
        public int Damage { get => damage; set => damage = value; }
        public int MaxAmmo { get => maxAmmo; set => maxAmmo = value; }
        public int Ammo { get => ammo; set => ammo = value; }
        public int Range { get => range; set => range = value; }
        public int SteamCost { get => steamCost; set => steamCost = value; }
        public bool Shooting { get => shooting; set => shooting = value; }

        public Weapon(string name, Unit.turretSize turretSize, double reloadTime, double attackSpeed, int damage, int maxAmmo, int range, int steamCost)
        {
            this.name = name;
            this.turretSize = turretSize;
            this.reloadTime = reloadTime;
            this.attackSpeed = attackSpeed;
            this.attackTime = 1.0d / attackSpeed;
            this.attackProgress = attackTime;
            this.damage = damage;
            this.maxAmmo = maxAmmo;
            this.range = range;
            this.steamCost = steamCost;
        }
        public Weapon(Weapon w)
        {
            texture = Assets.GetTexture(w.name);
            this.name = w.Name;
            this.turretSize = w.TurretSize;
            this.reloadTime = w.ReloadTime;
            this.attackSpeed = w.AttackSpeed;
            this.attackTime = w.AttackTime;
            this.attackProgress = w.AttackProgress;
            this.damage = w.Damage;
            this.maxAmmo = w.MaxAmmo;
            this.range = w.Range;
            this.steamCost = w.SteamCost;
        }
        public Texture2D GetTexture()
        {
            return texture;
        }
        public Unit.turretSize GetTurretSize()
        {
            return turretSize;
        }

        public void Update(float time)
        {
            if (attackProgress > 0.0d)
            {
                attackProgress -= time;
            }

            if (shooting)
            {
                while(attackProgress <= 0.0d)
                {
                    Attack();
                    attackProgress += attackTime;
                }
            }
            else if (attackProgress <= 0.0d)
            {
                attackProgress = 0.0d;
            }
        }
        private void Attack()
        {

        }
    }
}
