using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Drawing;

namespace Commangineer.Units
{
    public class Unit : RotatableTexturedObject, PlayerTarget
    {
        private float angle = 0;
        private int health;
        private int armour;
        private double speed;
        private Slot[] weapons;
        private Chassis chassis;

        private Vector2 position;
        private Vector2 goal;

        public float Angle
        {
            get
            {
                return angle;
            }
        }

        public bool Alive
        {
            get
            {
                return health > 0;
            }
        }

        public Unit(UnitTemplate template, Vector2 spawnPosition)
        {
            health = template.Health;
            armour = template.Armour;
            speed = template.Speed;
            chassis = template.Chassis;
            weapons = template.Weapons;
            goal = spawnPosition - new Vector2(0, Size.Y);
            position = spawnPosition;
        }

        public void Update(float deltaTime, Level level)
        {
            level.DestroyTilesUnderUnit(this);
            foreach (Slot w in weapons)
            {
                w.Update(deltaTime, position + Size / 2, Angle, level,this);
            }
            Vector2 deltaPosition = position - goal;
            if (deltaPosition.Length() > deltaTime * (float)speed)
            {
                angle = (float)Math.Atan2(deltaPosition.Y, deltaPosition.X);
            }
            Vector2 previousPosition = position;
            float length = deltaPosition.Length();
            if (length < deltaTime * (float)speed)
            {
                position = goal;
            }
            else
            {
                deltaPosition *= deltaTime * (float)speed / length;
                position = position - deltaPosition;
            }
            if (level.Collides(this))
            {
                position = previousPosition;
            }
        }

        public bool Intersects(RectangleF other)
        {
            return Bounds.IntersectsWith(other);
        }

        public bool Collides(Unit other)
        {
            Vector2 deltaPosition = CenterPosition - other.CenterPosition;
            return (Size.X + other.Size.X) / 2 > deltaPosition.Length();
        }

        public RectangleF Bounds
        {
            get
            {
                return new RectangleF(Position.X, position.Y, Size.X, Size.Y);
            }
        }

        public Vector2 Goal
        {
            set
            {
                goal = value;
            }
        }

        public Vector2 Size
        {
            get
            {
                return chassis.Size;
            }
        }

        public Vector2 Position
        {
            get
            {
                return position;
            }
        }

        public Vector2 CenterPosition
        {
            get
            {
                return position + Size / 2;
            }
        }

        public void Damage(int damage)
        {
            damage -= armour;
            if (damage <= 0)
            {
                damage = 1;
            }
            health -= damage;
        }

        public Texture2D GetTexture()
        {
            return chassis.Texture;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Camera.Draw(spriteBatch, this);
            for (int i = 0; i < weapons.Length; i++)
            {
                weapons[i].Draw(spriteBatch);
            }
        }

        public void DrawBulletFrames(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < weapons.Length; i++)
            {
                weapons[i].DrawBulletFrames(spriteBatch);
            }
        }

        public void DrawSelection(SpriteBatch spriteBatch)
        {
            Camera.DrawProjected(spriteBatch, Position, Size, Assets.GetImage("selectedUnit"));
        }
    }
}