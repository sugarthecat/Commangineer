using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

namespace Commangineer.Units
{
    /// <summary>
    /// A slot of a unit
    /// </summary>
    public class Slot : RotatableTexturedObject
    {
        private Weapon weapon;
        private Vector2 position;
        private Vector2 offsetPosition;
        private TurretSize turretSize;
        // Constructs a slot
        public Slot(Vector2 turretPosition, TurretSize turretSize)
        {
            offsetPosition = turretPosition;
            this.turretSize = turretSize;
        }

        // Constructs a slot based off a old slot
        public Slot(Slot oldSlot)
        {
            position = oldSlot.position;
            offsetPosition = oldSlot.offsetPosition;
            if (oldSlot.weapon != null)
            {
                weapon = new Weapon(oldSlot.weapon);
            }
        }

        /// <summary>
        /// Gets the slot's angle
        /// </summary>
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
        public MaterialBalance Cost
        {
            get
            {
                if(weapon != null)
                {
                    return weapon.Cost;
                }
                else
                {
                    return new MaterialBalance(0);
                }
            }
        }
        public TurretSize TurretSize
        {
            get
            {
                return turretSize;
            }
        }
        /// <summary>
        /// atempts to add weapon to slot and returns a bool
        /// </summary>
        /// <param name="w">weapon to add to slot</param>
        /// <returns>true if weapon successfully added else false</returns>
        public void AddWeapon(Weapon w)
        {
            weapon = new Weapon(w);
        }

        /// <summary>
        /// Draws the slot
        /// </summary>
        /// <param name="spriteBatch">The sprite batch to be drawn with</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (weapon != null)
            {
                Camera.Draw(spriteBatch, this);
            }
        }

        /// <summary>
        /// Draws bullet frames
        /// </summary>
        /// <param name="spriteBatch">The sprite batch to be drawn with</param>
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

        /// <summary>
        /// Gets the slot's texture
        /// </summary>
        /// <returns>The slot's texture</returns>
        public Texture2D GetTexture()
        {
            Texture2D weaponTexture = weapon.GetTexture();
            return weaponTexture;
        }

        /// <summary>
        /// Updates the slot
        /// </summary>
        /// <param name="time">The time since the last update</param>
        /// <param name="unitPosition">The position of the unit</param>
        /// <param name="rotationAngle">The rotation angle</param>
        /// <param name="level">The current level</param>
        /// <param name="firingUnit">A unit firing</param>
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

        /// <summary>
        /// Gets the position of the slot
        /// </summary>
        public Vector2 Position
        {
            get
            {
                return position - Size / 2;
            }
        }

        /// <summary>
        /// Gets the size of the slot
        /// </summary>
        public Vector2 Size
        {
            get
            {
                if (weapon != null)
                {
                    return weapon.Size;
                }
                return new Vector2(0, 0);
            }
        }
        public Vector2 OffsetPosition
        {
            get
            {
                return offsetPosition;
            }
        }
    }
}