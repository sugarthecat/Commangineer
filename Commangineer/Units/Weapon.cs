using Commangineer.Auuki;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Commangineer.Units
{
    public class Weapon
    {
        private string name;
        private double reloadTime;
        private double reloadProgress;
        private int damage;
        private int range;
        private MaterialBalance cost;
        private bool shooting;
        private AuukiTarget target;
        private Texture2D texture;
        private List<BulletFrame> bulletFrames = new List<BulletFrame>();
        public string Name { get => name; set => name = value; }
        public double ReloadTime { get => reloadTime; set => reloadTime = value; }
        public double ReloadProgress { get => reloadProgress; set => reloadProgress = value; }
        public int Damage { get => damage; set => damage = value; }
        public int Range { get => range; set => range = value; }
        public MaterialBalance Cost { get => cost; }
        public bool Shooting { get => shooting; set => shooting = value; }

        public BulletFrame[] BulletFrames
        {
            get
            {
                BulletFrame[] bulletFrameArr = bulletFrames.ToArray();
                return bulletFrameArr;
            }
        }

        public Weapon(string name, double reloadTime, int damage, int range, MaterialBalance cost)
        {
            this.name = name;
            this.reloadTime = reloadTime;
            this.damage = damage;
            this.range = range;
            this.cost = cost;
        }

        public Weapon(Weapon w)
        {
            texture = Assets.GetTexture(w.name);
            this.name = w.Name;
            this.reloadTime = w.ReloadTime;
            this.damage = w.Damage;
            this.range = w.Range;
            this.cost = w.Cost;
        }

        private float angle = (float)Math.PI / 2;

        public float Angle
        {
            get
            {
                return angle;
            }
        }

        public bool HasTarget
        {
            get
            {
                return this.target != null;
            }
        }

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

        public Texture2D GetTexture()
        {
            return texture;
        }

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

        private void Attack(Vector2 weaponPoint, Unit firingUnit)
        {
                 target.Damage(damage, firingUnit);
            bulletFrames.Add(new BulletFrame(weaponPoint, target.CenterPosition));
        }
    }
}