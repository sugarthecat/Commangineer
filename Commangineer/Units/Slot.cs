using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Commangineer.Units
{
    public class Slot : RotatableTexturedObject
    {
        private Weapon weapon;
        private Vector2 position;
        private Vector2 offsetPosition;

        public Slot(Vector2 turretPosition)
        {
            offsetPosition = turretPosition;
        }

        public Slot(Slot oldSlot)
        {
            position = oldSlot.position;
            if (oldSlot.weapon != null)
            {
                weapon = new Weapon(oldSlot.weapon);
            }
        }

        public float Angle
        {
            get
            {
                if (weapon != null)
                {
                    return weapon.Angle;
                }
                return 0;
            }
        }

        /// <summary>
        /// atempts to add weapon to slot and returns a bool
        /// </summary>
        /// <param name="w">weapon to add to slot</param>
        /// <returns>true if weapon successfully added else false</returns>
        public bool AddWeapon(Weapon w)
        {
            if (weapon == null)
            {
                weapon = new Weapon(w);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (weapon != null)
            {
                Camera.Draw(spriteBatch, this);
            }
        }

        public void DrawBulletFrames(SpriteBatch spriteBatch)
        {
            if (weapon != null)
            {
                BulletFrame[] frames = weapon.BulletFrames;
                for (int i = 0; i < frames.Length; i++)
                {
                    Camera.Draw(spriteBatch, frames[i]);
                }
            }
        }

        public Texture2D GetTexture()
        {
            return weapon.GetTexture();
        }

        public void Update(float time, Vector2 unitPosition, float rotationAngle, Level level, Unit firingUnit)
        {
            Vector2 rotatedOffset = new Vector2(
                (float)(offsetPosition.X * Math.Cos(rotationAngle) - offsetPosition.Y * Math.Sin(rotationAngle)),
                (float)(offsetPosition.X * Math.Sin(rotationAngle) + offsetPosition.Y * Math.Cos(rotationAngle))
                );
            position = unitPosition + rotatedOffset;
            if (weapon != null)
            {
                if (!weapon.HasTarget)
                {
                    weapon.Target = level.GetAuukiTarget(position, weapon.Range);
                }
                weapon.Update(time, position, level, firingUnit);
            }
        }

        public Vector2 Position
        {
            get
            {
                return position - Size / 2;
            }
        }

        public Vector2 Size
        {
            get
            {
                return new Vector2(1, 1);
            }
        }
    }
}