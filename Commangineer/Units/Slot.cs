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

        private Weapon weapon;
        private Unit.turretSize size;
        private Vector2 position;
        private Vector2 offsetPosition;

        public Slot(Unit.turretSize s, Vector2 turretPosition)
        {
            size = s;
            offsetPosition = turretPosition;
        }
        public Slot(Slot oldSlot)
        {
            size = oldSlot.size;
            position = oldSlot.position;
            if(oldSlot.weapon != null) {
                weapon = new Weapon(oldSlot.weapon);
            }
        }
        public float Angle
        {
            get
            {
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
        public void Draw(SpriteBatch spriteBatch)
        {
            if (weapon != null)
            {
                Camera.Draw(spriteBatch, this);
            }
        }
        public Texture2D GetTexture()
        {
            return weapon.GetTexture();
        }
        public void Update(float time, Vector2 unitPosition, float rotationAngle) 
        {
            Vector2 rotatedOffset = new Vector2(
                (float)(offsetPosition.X * Math.Cos(rotationAngle) - offsetPosition.Y * Math.Sin(rotationAngle)),
                (float)(offsetPosition.X * Math.Sin(rotationAngle) + offsetPosition.Y * Math.Cos(rotationAngle))
                );
            position = unitPosition + rotatedOffset;
            if (weapon != null)
            {
                weapon.Update(time);
            }           
        }
        public Vector2 Position
        {
            get
            {
                return position-Size/2;
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
