using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer.Units
{
    internal class Slot : RotatableTexturedObject
    {
        /// <summary>
        /// A slot of a unit
        /// </summary>
        private Weapon weapon;
        private Unit.turretSize size;
        private Vector2 position;
        private Vector2 offsetPosition;

        // Constructs a slot
        public Slot(Unit.turretSize s, Vector2 turretPosition)
        {
            size = s;
            offsetPosition = turretPosition;
        }
        // Constructs a slot based off a old slot
        public Slot(Slot oldSlot)
        {
            size = oldSlot.size;
            position = oldSlot.position;
            if(oldSlot.weapon != null) {
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
                if(weapon != null)
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
        /// <summary>
        /// Draws the slot
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (weapon != null)
            {
                Camera.Draw(spriteBatch, this);
            }
        }
        /// <summary>
        /// Gets the slot's texture
        /// </summary>
        /// <returns>The slot's texture</returns>
        public Texture2D GetTexture()
        {
            return weapon.GetTexture();
        }
        /// <summary>
        /// Updates the slot
        /// </summary>
        /// <param name="time">The time since the last update</param>
        /// <param name="unitPosition">The position of the unit</param>
        /// <param name="rotationAngle">The rotation angle</param>
        /// <param name="level">The current level</param>
        public void Update(float time, Vector2 unitPosition, float rotationAngle, Level level) 
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
                    weapon.Target = level.GetTarget(position, weapon.Range);
                }
                weapon.Update(time, position);
            }           
        }
        /// <summary>
        /// Gets the position of the slot
        /// </summary>
        public Vector2 Position
        {
            get
            {
                return position-Size/2;
            }
        }
        /// <summary>
        /// Gets the size of the slot
        /// </summary>
        public Vector2 Size
        {
            get
            {
                return new Vector2(1, 1);
            }
        }

    }
}
