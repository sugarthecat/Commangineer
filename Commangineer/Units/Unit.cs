using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Commangineer.Units
{
    internal class Unit : TexturedObject
    {
        public enum turretSize
        {
            small,
            medium,
            big
        }
        private string name;
        private int health;
        private int maxHealth;
        private int armour;
        private double speed;
        private Slot[] weapons;
        private Chassis chassis;
        private Material chassisMaterial;
        private Engine engine;
        private Material engineMaterial;

        Point position;
        Point goal;
        List<Point> path = new List<Point>();
        Pathfinding pathing;
        float maxMove = 1;
        float move = 1;
        float maxRescan = 20;
        float rescan = 0;
        bool idle = true;

        enum specialStats
        {
                
        }

        public Unit(string name, Chassis chassis, Material chassisMaterial, Engine engine, Material engineMaterial)
        {
            int weight = 0;
            int horse = 0;
            double speed;

            this.name = name;
            this.chassis = chassis;
            this.chassisMaterial = chassisMaterial;

            this.weapons = new Slot[chassis.GetTurrets().Length];

            for (int i = 0; i < chassis.GetTurrets().Length; i++)
            {
                this.weapons[i] = new Slot(chassis.GetTurrets()[i]);
            }

            this.engine = engine;
            this.engineMaterial = engineMaterial;

            weight += chassis.GetSize() * chassisMaterial.GetWeight();
            weight += engine.GetSize() * engineMaterial.GetWeight();

            horse += engine.GetHorsepower() * engineMaterial.GetStrength();

            speed = (System.Math.Pow((double)horse/(double)weight, (1.0 / 3.0)));

            if(speed > engine.GetSpeed() * engineMaterial.GetWorkability())
            {
                speed = engine.GetSpeed() * engineMaterial.GetWorkability();
            }

            this.speed = speed;
            this.maxHealth = chassis.GetHealth() * chassisMaterial.GetStrength();
            this.health = this.maxHealth;
            this.armour = chassis.GetArmour() * (chassisMaterial.GetWorkability()/ 10);

        }

        public Unit(Unit u, Point pos, Pathfinding pathing)
        {
            this.name = u.name;
            this.health = u.health;
            this.maxHealth = u.maxHealth;
            this.armour = u.armour;
            this.speed = u.speed;
            this.chassis = u.chassis;
            this.chassisMaterial = u.chassisMaterial;
            this.engine = u.engine;
            this.engineMaterial = u.engineMaterial;

            position = pos;
            this.pathing = pathing;

        }

        public void Update(float time)
        {
            foreach (Slot w in weapons)
            {
                w.Update(time);
            }
            if (!idle)
            {
                pathfindingUpdate(time);
            }
        }

        public void pathfindingUpdate(float deltaTime)
        {
            if (rescan <= 0)
            {
                rescan = maxRescan;
                path = pathing.find(position, goal);
            }
            else
            {
                rescan -= deltaTime;
            }
            if (move <= 0)
            {
                move = maxMove;
                if (path.Count > 0)
                {
                    Point temp = Vector2.Normalize(path[0].ToVector2() - position.ToVector2()).ToPoint();
                    moveX(temp.X);
                    moveY(temp.Y);
                    path.RemoveAt(0);
                }
                else
                {
                    idle = true;
                }
            }
            else
            {
                move -= deltaTime;
            }
        }

        public void addGoal(Point g)
        {
            goal = g;
            idle = false;
        }

        public Vector2 Size
        {
            get
            {
                return new Vector2(1, 1);
            }
        }
        public Vector2 Position
        {
            get
            {
                return new Vector2(position.X,position.Y);
            }
        }

        public void Damage(int d)
        {
            d -= armour;
            if(d <= 0)
            {
                d = 1;
            }
             health -= d;
        }

        public Texture2D GetTexture()
        {
            return Assets.GetTexture("wood");
        }

        public void moveX(int val)
        {
            position.X += val;
        }
        public void moveY(int val)
        {
            position.Y += val;
        }

    }
}
