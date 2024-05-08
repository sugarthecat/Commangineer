using Commangineer.Auuki;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Commangineer.Units
{
    /// <summary>
    /// A weapon of a unit
    /// </summary>
    public class Weapon
    {
        private string name;
        private double reloadTime;
        private double reloadProgress;
        private int damage;
        private float size;
        private int range;
        private MaterialBalance cost;
        private bool shooting;
        private AuukiTarget target;
        private Texture2D texture;
        private List<BulletFrame> bulletFrames = new List<BulletFrame>();
        // Getters and setter for the properties
        public string Name { get => name; set => name = value; }
        public double ReloadTime { get => reloadTime; set => reloadTime = value; }
        public double ReloadProgress { get => reloadProgress; set => reloadProgress = value; }
        public int Damage { get => damage; set => damage = value; }
        public int Range { get => range; set => range = value; }
        public MaterialBalance Cost { get => cost; }
        public bool Shooting { get => shooting; set => shooting = value; }
        public TurretSize turretSize;
        /// <summary>
        /// Gets the bullet frames of the weapon
        /// </summary>
        public BulletFrame[] BulletFrames
        {
            get
            {
                BulletFrame[] bulletFrameArr = bulletFrames.ToArray();
                return bulletFrameArr;
            }
        }
        Color bulletColor;
        // Creates a new weapon
        public Weapon(string name, double reloadTime, int damage, int range, MaterialBalance cost,TurretSize turretSize, Color bulletColor,float size = 1f)
        {
            this.name = name;
            this.reloadTime = reloadTime;
            this.damage = damage;
            this.range = range;
            this.cost = cost;
            this.size = size;
            this.turretSize = turretSize;
            this.bulletColor = bulletColor;
        }

        // Creates a new weapon from a preexisting weapon as a template
        public Weapon(Weapon w)
        {
            texture = Assets.GetTexture(w.name);
            name = w.Name;
            reloadTime = w.ReloadTime;
            damage = w.Damage;
            range = w.Range;
            cost = w.Cost;
            size = w.size;
            bulletColor = w.bulletColor;
        }

        private float angle = (float)Math.PI / 2;
        public TurretSize TurretSize
        {
            get
            {
                return turretSize;
            }
        }
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
                if (target == null)
                {
                    target = value;
                }
            }
        }

        public Vector2 Size
        {
            get
            {
                return new Vector2(size, size);
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
        /// Updates the weapon
        /// </summary>
        /// <param name="time">Time passed since the last update</param>
        /// <param name="weaponPoint">The new weapon's point</param>
        /// <param name="level">The current level</param>
        /// <param name="firingUnit">The unit firing</param>
        public void Update(float time, Vector2 weaponPoint, Level level, Unit firingUnit)
        {
            for (int i = 0; i < bulletFrames.Count; i++)
            {
                bulletFrames[i].duration -= time;
                if (bulletFrames[i].duration <= 0)
                {
                    bulletFrames.RemoveAt(i);
                    i--;
                }
            }
            if(target != null && !target.Alive)
            {
                target = null;
            }
            if (target != null)
            {
                Vector2 deltaPosition = weaponPoint - target.CenterPosition;
                angle = (float)Math.Atan2(deltaPosition.Y, deltaPosition.X);
                while (reloadProgress <= 0.0d)
                {
                    Attack(weaponPoint, firingUnit);
                    reloadProgress += reloadTime;
                    if (!target.Alive)
                    {
                        target = level.GetAuukiTarget(weaponPoint, range);
                    }
                }

                if (deltaPosition.Length() > range || target == null || !target.Alive)
                {
                    target = null;
                }
            }
            else if (reloadProgress <= 0.0d)
            {
                reloadProgress = 0.0d;
            }
            if (reloadProgress > 0.0d)
            {
                reloadProgress -= time;
            }
        }

        /// <summary>
        /// Attacks with a weapon
        /// </summary>
        private void Attack(Vector2 weaponPoint, Unit firingUnit)
        {
            target.Damage(damage, firingUnit);
            bulletFrames.Add(new BulletFrame(weaponPoint, target.CenterPosition,bulletColor ));
        }
    }
}