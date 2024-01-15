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
    /// <summary>
    /// A weapon of a unit
    /// </summary>
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
        private AuukiTarget target;
        private Texture2D texture;
        // Getters and setter for the properties
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
        // Creates a new weapon
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
        // Creates a new weapon from a preexisting weapon as a template
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
        private float angle = (float)Math.PI/2;

        /// <summary>
        /// Gets the angle to be used
        /// </summary>
        public float Angle
        {
            get
            {
                return angle;
            }
        }
        /// <summary>
        /// Checks if the weapon has a target
        /// </summary>
        public bool HasTarget
        {
            get
            {
                return this.target != null;
            }
        }
        /// <summary>
        /// Gets the weapon's target
        /// </summary>
        public AuukiTarget Target
        {
            set
            {
                if(target == null) {
                    target = value;
                }
            }
        }
        /// <summary>
        /// Gets the weapon's texture
        /// </summary>
        /// <returns>The texture of the weapon</returns>
        public Texture2D GetTexture()
        {
            return texture;
        }
        /// <summary>
        /// Gets the weapon's turret size
        /// </summary>
        /// <returns>The size of the turret</returns>
        public Unit.turretSize GetTurretSize()
        {
            return turretSize;
        }

        /// <summary>
        /// Updates the weapon
        /// </summary>
        /// <param name="time">Time passed since the last update</param>
        /// <param name="weaponPoint">The new weapon's point</param>
        public void Update(float time, Vector2 weaponPoint)
        {
            if(target != null)
            {

                Vector2 deltaPosition = weaponPoint - target.CenterPosition;
                angle = (float)Math.Atan2(deltaPosition.Y, deltaPosition.X);
                if(deltaPosition.Length() > range)
                {
                    target = null;
                }
            }
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
        /// <summary>
        /// Attacks with a weapon
        /// </summary>
        private void Attack()
        {

        }
    }
}
